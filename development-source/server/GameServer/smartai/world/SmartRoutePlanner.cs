using System;
using System.Collections.Generic;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI route planning (owner 2026-11-05: "Setup Routes"). A long walk is planned over a coarse grid of the zone
    /// (1,024-unit cells), each cell weighted by its danger: the deaths SmartAI learned there and the hard aggressive
    /// monsters that spawn in it. When the straight way crosses danger, A* finds a cheaper way and the bot walks it as
    /// a few waypoints (the navmesh still finds the path between them, and the look-ahead route guard still goes around
    /// single monsters on each leg). Pure: no world objects; the controller supplies the danger and checks each leg on
    /// the navmesh. All weights are improvised.
    /// Run 4 (2026-11-05): a level 7 Theurgist walking back to its party died four times in 15 minutes on the same
    /// Salisbury Plains square; 105 of 157 grouped deaths were members walking alone to their party.
    /// </summary>
    public static class SmartRoutePlanner
    {
        public const int CellSize = 1024;
        /// <summary>A cell's danger multiplies the cost of walking through it by (1 + DangerWeight x danger).</summary>
        public const double DangerWeight = 4.0;
        /// <summary>Straight-line danger below this is not worth a detour.</summary>
        public const double MinStraightDanger = 1.0;
        /// <summary>A planned way longer than this many times the straight distance is not taken.</summary>
        public const double MaxLengthRatio = 2.2;
        /// <summary>The planned way must carry at most this share of the straight way's danger.</summary>
        public const double MaxDangerShare = 0.5;

        public readonly record struct Bounds(int MinX, int MinY, int MaxX, int MaxY)
        {
            public bool Contains(int x, int y) => x >= MinX && y >= MinY && x < MaxX && y < MaxY;
        }

        public sealed record Plan(IReadOnlyList<(int X, int Y)> Waypoints, double StraightDanger, double PlannedDanger,
            double StraightLength, double PlannedLength);

        public static int CellOf(int v) => (int)Math.Floor(v / (double)CellSize);
        private static (int X, int Y) Center(int cx, int cy) => (cx * CellSize + CellSize / 2, cy * CellSize + CellSize / 2);

        /// <summary>Pure: the danger the straight segment collects (each cell it passes counted once).</summary>
        public static double SegmentDanger(int x0, int y0, int x1, int y1, Func<int, int, double> danger)
        {
            double length = Math.Sqrt((double)(x1 - x0) * (x1 - x0) + (double)(y1 - y0) * (y1 - y0));
            int steps = Math.Max(1, (int)Math.Ceiling(length / (CellSize / 4.0)));
            var seen = new HashSet<(int, int)>();
            double sum = 0;
            for (int i = 0; i <= steps; i++)
            {
                double t = i / (double)steps;
                int cx = CellOf((int)(x0 + (x1 - x0) * t)), cy = CellOf((int)(y0 + (y1 - y0) * t));
                if (seen.Add((cx, cy)))
                    sum += Math.Max(0, danger(cx, cy));
            }
            return sum;
        }

        /// <summary>
        /// Pure: a safer way from start to destination inside the bounds (world units), or null when the straight way is
        /// safe enough, no way is found, or the safer way is much longer or not much safer. The waypoints exclude the
        /// start and the destination.
        /// </summary>
        public static Plan Find(int sx, int sy, int dx, int dy, Bounds bounds, Func<int, int, double> danger, int maxCells = 20_000)
        {
            double straightLength = Math.Sqrt((double)(dx - sx) * (dx - sx) + (double)(dy - sy) * (dy - sy));
            double straightDanger = SegmentDanger(sx, sy, dx, dy, danger);
            if (straightDanger < MinStraightDanger)
                return null;
            int minCx = CellOf(bounds.MinX), minCy = CellOf(bounds.MinY), maxCx = CellOf(bounds.MaxX - 1), maxCy = CellOf(bounds.MaxY - 1);
            int width = maxCx - minCx + 1, height = maxCy - minCy + 1;
            if (width <= 0 || height <= 0 || (long)width * height > maxCells)
                return null;
            (int X, int Y) start = (CellOf(sx), CellOf(sy)), goal = (CellOf(dx), CellOf(dy));
            if (start.X < minCx || start.X > maxCx || start.Y < minCy || start.Y > maxCy ||
                goal.X < minCx || goal.X > maxCx || goal.Y < minCy || goal.Y > maxCy)
                return null;

            int Index(int x, int y) => (y - minCy) * width + (x - minCx);
            var cost = new double[width * height];
            var from = new int[width * height];
            Array.Fill(cost, double.MaxValue);
            Array.Fill(from, -1);
            var cellDanger = new double[width * height];
            for (int y = minCy; y <= maxCy; y++)
            for (int x = minCx; x <= maxCx; x++)
                cellDanger[Index(x, y)] = Math.Max(0, danger(x, y));
            // Inside the grid every later lookup reads the cache (the string pulling below asks many times).
            Func<int, int, double> cached = (x, y) => x >= minCx && x <= maxCx && y >= minCy && y <= maxCy
                ? cellDanger[Index(x, y)] : Math.Max(0, danger(x, y));
            var open = new PriorityQueue<int, double>();
            int s = Index(start.X, start.Y), g = Index(goal.X, goal.Y);
            cost[s] = 0;
            open.Enqueue(s, 0);
            while (open.TryDequeue(out int current, out double priority))
            {
                if (current == g)
                    break;
                int cx = current % width + minCx, cy = current / width + minCy;
                if (priority - Heuristic(cx, cy) > cost[current] + 1e-9)
                    continue;
                for (int ox = -1; ox <= 1; ox++)
                for (int oy = -1; oy <= 1; oy++)
                {
                    if (ox == 0 && oy == 0)
                        continue;
                    int nx = cx + ox, ny = cy + oy;
                    if (nx < minCx || nx > maxCx || ny < minCy || ny > maxCy)
                        continue;
                    int n = Index(nx, ny);
                    double step = (ox != 0 && oy != 0 ? Math.Sqrt(2) : 1) * (1 + DangerWeight * cellDanger[n]);
                    double next = cost[current] + step;
                    if (next < cost[n])
                    {
                        cost[n] = next;
                        from[n] = current;
                        open.Enqueue(n, next + Heuristic(nx, ny));
                    }
                }
            }
            if (from[g] < 0 && g != s)
                return null;

            // The cell path, start to goal, then pulled taut: from each point, the farthest later cell reachable by a
            // straight line that collects no more danger than the cells it skips.
            var cells = new List<(int X, int Y)>();
            for (int i = g; i >= 0; i = from[i])
            {
                cells.Add((i % width + minCx, i / width + minCy));
                if (i == s)
                    break;
            }
            cells.Reverse();
            var points = new List<(int X, int Y)> { (sx, sy) };
            for (int i = 1; i < cells.Count - 1; i++)
                points.Add(Center(cells[i].X, cells[i].Y));
            points.Add((dx, dy));
            var waypoints = new List<(int X, int Y)>();
            int at = 0;
            while (at < points.Count - 1)
            {
                int best = at + 1;
                for (int j = points.Count - 1; j > at + 1; j--)
                {
                    double skipped = 0;
                    for (int k = at; k <= j; k++)
                        skipped += cellDanger[Index(CellOf(points[k].X), CellOf(points[k].Y))];
                    if (SegmentDanger(points[at].X, points[at].Y, points[j].X, points[j].Y, cached) <= skipped + 0.25)
                    {
                        best = j;
                        break;
                    }
                }
                if (best < points.Count - 1)
                    waypoints.Add(points[best]);
                at = best;
            }
            if (waypoints.Count == 0)
                return null;

            double plannedLength = 0, plannedDanger = 0;
            (int X, int Y) prev = (sx, sy);
            foreach ((int X, int Y) w in waypoints)
            {
                plannedLength += Distance(prev, w);
                plannedDanger += SegmentDanger(prev.X, prev.Y, w.X, w.Y, cached);
                prev = w;
            }
            plannedLength += Distance(prev, (dx, dy));
            plannedDanger += SegmentDanger(prev.X, prev.Y, dx, dy, cached);
            if (plannedLength > MaxLengthRatio * Math.Max(straightLength, CellSize) || plannedDanger > MaxDangerShare * straightDanger)
                return null;
            return new Plan(waypoints, straightDanger, plannedDanger, straightLength, plannedLength);

            double Heuristic(int x, int y)
            {
                double ddx = Math.Abs(x - goal.X), ddy = Math.Abs(y - goal.Y);
                return Math.Max(ddx, ddy) + (Math.Sqrt(2) - 1) * Math.Min(ddx, ddy);
            }
        }

        private static double Distance((int X, int Y) a, (int X, int Y) b) =>
            Math.Sqrt((double)(a.X - b.X) * (a.X - b.X) + (double)(a.Y - b.Y) * (a.Y - b.Y));
    }
}
