using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// What SmartAI groups know about the world before they pick a camp (owner 2026-11-05: "a config for suggested level
    /// ranges for all dungeons and areas ... then an area search x units away"). Rebuilt with the camp catalog, on the
    /// catalog's background worker, from the same snapshot of live monsters; nothing here touches a live object.
    /// - Zone levels: the live monster levels per zone (min, 10th percentile, median, 90th percentile, max), written
    ///   to smartai-world.json's "generated" section.
    /// - Camp threats: for every camp, the aggressive monsters of other kinds within the search radius (their level,
    ///   aggro range and distance from the camp spot). The camp chooser turns them into a safety score.
    /// </summary>
    public static class SmartWorldKnowledge
    {
        /// <summary>A live monster as the snapshot saw it.</summary>
        public readonly record struct Monster(string Name, ushort Region, string ZoneName, bool Dungeon, int X, int Y, int Z,
            int Level, int AggroRange, bool Aggressive);

        /// <summary>A camp: its id, its monster and the spot the group goes to.</summary>
        public readonly record struct CampSite(string Id, string MonsterName, ushort Region, int X, int Y, int Z);

        /// <summary>An aggressive monster near a camp.</summary>
        public readonly record struct Threat(int Level, int AggroRange, int Distance);

        private sealed record Published(Dictionary<string, Threat[]> Threats, Dictionary<string, SmartWorldConfig.ZoneGenerated> Zones,
            Dictionary<string, (ushort Region, int X, int Y)> Spots, Dictionary<(ushort, int, int), int[]> Hazards);

        private static Published _published = new(new(), new(StringComparer.OrdinalIgnoreCase), new(StringComparer.OrdinalIgnoreCase), new());

        /// <summary>The route planner's cell (SmartRoutePlanner.CellSize).</summary>
        public const int HazardCell = SmartRoutePlanner.CellSize;

        /// <summary>How many aggressive outdoor monsters of at least <paramref name="minLevel"/> spawn in a route
        /// planner cell.</summary>
        public static int HardAggressiveIn(ushort region, int cellX, int cellY, int minLevel)
        {
            if (!Volatile.Read(ref _published).Hazards.TryGetValue((region, cellX, cellY), out int[] levels))
                return 0;
            int n = 0;
            foreach (int level in levels)
                if (level >= minLevel)
                    n++;
            return n;
        }

        /// <summary>Pure: the levels of the aggressive outdoor monsters per route planner cell.</summary>
        public static Dictionary<(ushort, int, int), int[]> BuildHazards(IReadOnlyList<Monster> monsters)
        {
            var cells = new Dictionary<(ushort, int, int), List<int>>();
            foreach (Monster m in monsters)
            {
                if (!m.Aggressive || m.AggroRange <= 0 || m.Dungeon)
                    continue;
                var key = (m.Region, (int)Math.Floor(m.X / (double)HazardCell), (int)Math.Floor(m.Y / (double)HazardCell));
                if (!cells.TryGetValue(key, out List<int> list))
                    cells[key] = list = new List<int>();
                list.Add(m.Level);
            }
            return cells.ToDictionary(p => p.Key, p => p.Value.ToArray());
        }

        private const int Cell = 2048;
        private const int MaxThreatsPerCamp = 16;
        private const int MaxHeightDifference = 600;

        public static bool HasData => Volatile.Read(ref _published).Threats.Count > 0;

        /// <summary>The aggressive monsters near a camp (nearest first), or none.</summary>
        public static Threat[] ThreatsNear(string campId) =>
            campId != null && Volatile.Read(ref _published).Threats.TryGetValue(campId, out Threat[] t) ? t : Array.Empty<Threat>();

        /// <summary>Where a camp is, for telling a death at the camp from one on the way.</summary>
        public static bool TryGetSpot(string campId, out (ushort Region, int X, int Y) spot)
        {
            spot = default;
            return campId != null && Volatile.Read(ref _published).Spots.TryGetValue(campId, out spot);
        }

        public static SmartWorldConfig.ZoneGenerated ZoneLevels(string zone) =>
            zone != null && Volatile.Read(ref _published).Zones.TryGetValue(zone, out var z) ? z : SmartWorldConfig.GeneratedFor(zone);

        /// <summary>Rebuilds from a snapshot and publishes; writes the generated zone levels to the config file.</summary>
        public static void Rebuild(IReadOnlyList<Monster> monsters, IReadOnlyList<CampSite> camps, int searchRadius, bool writeFile = true)
        {
            Dictionary<string, SmartWorldConfig.ZoneGenerated> zones = BuildZoneLevels(monsters);
            Dictionary<string, Threat[]> threats = BuildThreats(monsters, camps, searchRadius);
            var spots = new Dictionary<string, (ushort, int, int)>(StringComparer.OrdinalIgnoreCase);
            foreach (CampSite c in camps)
                spots[c.Id] = (c.Region, c.X, c.Y);
            Volatile.Write(ref _published, new Published(threats, zones, spots, BuildHazards(monsters)));
            if (writeFile)
                SmartWorldConfig.WriteGenerated(zones);
        }

        /// <summary>Pure: level percentiles per zone name.</summary>
        public static Dictionary<string, SmartWorldConfig.ZoneGenerated> BuildZoneLevels(IReadOnlyList<Monster> monsters)
        {
            var result = new Dictionary<string, SmartWorldConfig.ZoneGenerated>(StringComparer.OrdinalIgnoreCase);
            foreach (IGrouping<string, Monster> zone in monsters.Where(m => !string.IsNullOrWhiteSpace(m.ZoneName))
                         .GroupBy(m => m.ZoneName, StringComparer.OrdinalIgnoreCase))
            {
                int[] levels = zone.Select(m => m.Level).OrderBy(l => l).ToArray();
                Monster first = zone.First();
                result[zone.Key] = new SmartWorldConfig.ZoneGenerated
                {
                    Region = first.Region,
                    Dungeon = first.Dungeon,
                    Min = levels[0],
                    Low = Percentile(levels, 0.10),
                    Typical = Percentile(levels, 0.50),
                    High = Percentile(levels, 0.90),
                    Max = levels[^1],
                    Monsters = levels.Length,
                };
            }
            return result;
        }

        private static int Percentile(int[] sorted, double p) =>
            sorted[Math.Clamp((int)Math.Floor(p * (sorted.Length - 1) + 0.5), 0, sorted.Length - 1)];

        /// <summary>Pure: the area search around each camp (a grid keeps it to the nearby cells).</summary>
        public static Dictionary<string, Threat[]> BuildThreats(IReadOnlyList<Monster> monsters, IReadOnlyList<CampSite> camps, int searchRadius)
        {
            var grid = new Dictionary<(ushort, int, int), List<Monster>>();
            foreach (Monster m in monsters)
            {
                if (!m.Aggressive || m.AggroRange <= 0)
                    continue;
                var key = (m.Region, Floor(m.X), Floor(m.Y));
                if (!grid.TryGetValue(key, out List<Monster> list))
                    grid[key] = list = new List<Monster>();
                list.Add(m);
            }
            var result = new Dictionary<string, Threat[]>(StringComparer.OrdinalIgnoreCase);
            int reach = searchRadius / Cell + 1;
            long r2 = (long)searchRadius * searchRadius;
            var found = new List<Threat>();
            foreach (CampSite camp in camps)
            {
                found.Clear();
                int cx = Floor(camp.X), cy = Floor(camp.Y);
                for (int dx = -reach; dx <= reach; dx++)
                for (int dy = -reach; dy <= reach; dy++)
                {
                    if (!grid.TryGetValue((camp.Region, cx + dx, cy + dy), out List<Monster> list))
                        continue;
                    foreach (Monster m in list)
                    {
                        if (string.Equals(m.Name, camp.MonsterName, StringComparison.OrdinalIgnoreCase) ||
                            Math.Abs(m.Z - camp.Z) > MaxHeightDifference)
                            continue;
                        long ddx = m.X - camp.X, ddy = m.Y - camp.Y;
                        long d2 = ddx * ddx + ddy * ddy;
                        if (d2 <= r2)
                            found.Add(new Threat(m.Level, m.AggroRange, (int)Math.Sqrt(d2)));
                    }
                }
                if (found.Count > 0)
                    result[camp.Id] = found.OrderBy(t => t.Distance).Take(MaxThreatsPerCamp).ToArray();
            }
            return result;
        }

        private static int Floor(int v) => (int)Math.Floor(v / (double)Cell);
    }
}
