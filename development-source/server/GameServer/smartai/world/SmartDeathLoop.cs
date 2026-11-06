using System;
using System.Collections.Generic;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI solo death loops (run 6, 2026-11-05). Bots released at their bind, rested to full, walked out and died in
    /// the same 2,048-unit square every three minutes for two hours: a level 6 Blademaster 36 times to the lunantishee
    /// north of its bind in Lough Derg, five Albion bots 21-39 times each to pixie scouts 4,000 units from theirs. Most
    /// were walking to a merchant between tasks, a walk the route planner never saw, and they chose the same merchant
    /// again after every death.
    ///
    /// A bot that dies LoopDeaths times within LoopWindowMilliseconds within LoopRadius of one spot has a loop area
    /// there for AvoidMilliseconds. While it lasts the bot takes no camp and no service trip whose straight way passes
    /// within AvoidRadius of it, unless a planned route goes around it. Nothing in a fight changes; all the numbers are
    /// improvised.
    /// </summary>
    public static class SmartDeathLoop
    {
        public const int LoopDeaths = 3;
        public const int LoopRadius = 2_048;
        public const long LoopWindowMilliseconds = 30 * 60_000;
        public const long AvoidMilliseconds = 60 * 60_000;
        public const int AvoidRadius = 2_500;
        public const int MaxRemembered = 16;

        public readonly record struct Death(ushort Region, int X, int Y, long Tick);

        public readonly record struct Area(ushort Region, int X, int Y, long Until);

        /// <summary>
        /// Pure: records a death (dropping ones older than the window, keeping at most MaxRemembered) and returns the
        /// loop area it closes, if this death makes LoopDeaths within LoopRadius of it in the window. The area's centre
        /// is the mean of those deaths.
        /// </summary>
        public static Area? RecordDeath(List<Death> deaths, Death death)
        {
            deaths.RemoveAll(d => death.Tick - d.Tick > LoopWindowMilliseconds || d.Tick > death.Tick);
            deaths.Add(death);
            while (deaths.Count > MaxRemembered)
                deaths.RemoveAt(0);
            long sumX = 0, sumY = 0;
            int count = 0;
            foreach (Death d in deaths)
            {
                if (d.Region != death.Region || Distance(d.X, d.Y, death.X, death.Y) > LoopRadius)
                    continue;
                sumX += d.X;
                sumY += d.Y;
                count++;
            }
            if (count < LoopDeaths)
                return null;
            return new Area(death.Region, (int)(sumX / count), (int)(sumY / count), death.Tick + AvoidMilliseconds);
        }

        /// <summary>Pure: keeps the areas still in force, one per spot (a newer area within LoopRadius replaces an older one).</summary>
        public static void Remember(List<Area> areas, Area area, long now)
        {
            areas.RemoveAll(a => a.Until <= now ||
                                 a.Region == area.Region && Distance(a.X, a.Y, area.X, area.Y) <= LoopRadius);
            areas.Add(area);
            while (areas.Count > MaxRemembered)
                areas.RemoveAt(0);
        }

        /// <summary>Pure: the area in force (if any) that the straight way from (x0, y0) to (x1, y1) in a region passes.</summary>
        public static Area? Crossed(IReadOnlyList<Area> areas, ushort region, int x0, int y0, int x1, int y1, long now)
        {
            if (areas == null)
                return null;
            foreach (Area a in areas)
                if (a.Until > now && a.Region == region && SegmentDistance(x0, y0, x1, y1, a.X, a.Y) <= AvoidRadius)
                    return a;
            return null;
        }

        /// <summary>Pure: the shortest distance from (px, py) to the segment (x0, y0)-(x1, y1).</summary>
        public static double SegmentDistance(int x0, int y0, int x1, int y1, int px, int py)
        {
            double dx = x1 - x0, dy = y1 - y0;
            double lengthSquared = dx * dx + dy * dy;
            double t = lengthSquared <= 0 ? 0 : Math.Clamp(((px - x0) * dx + (py - y0) * dy) / lengthSquared, 0, 1);
            double cx = x0 + t * dx - px, cy = y0 + t * dy - py;
            return Math.Sqrt(cx * cx + cy * cy);
        }

        private static double Distance(int x0, int y0, int x1, int y1) =>
            Math.Sqrt((double)(x1 - x0) * (x1 - x0) + (double)(y1 - y0) * (y1 - y0));
    }
}
