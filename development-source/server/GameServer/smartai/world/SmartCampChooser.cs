using System;
using System.Collections.Generic;
using System.Linq;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// How a SmartAI group picks its camp (owner 2026-11-05). The legacy planner has already filtered the camps by
    /// realm, reach, level band, crowding and death memory; this replaces its last step (a draw weighted only by live
    /// spawns, at a single level) with:
    /// 1. Zones first: camps in zones that suit the group (smartai-world.json: the owner's range or skip, else the
    ///    zone's typical monster no more than ZoneTypicalAboveBand levels above the band's top). All camps when none.
    /// 2. A weight per camp = live spawns (capped at 12) x level x safety x learned:
    ///    - level: each level above the lowest candidate adds 25% (more experience per kill);
    ///    - safety, from the area search: every aggressive monster of another kind whose aggro reaches the camp spot
    ///      scales it by its con to the group (purple 0.15, red 0.3, orange 0.6, yellow 0.85, easier 0.95); one
    ///      within the search radius but out of aggro range, red or harder, by 0.9 (the pulls walk near it);
    ///    - learned: SmartWorldLearning.FactorFor.
    /// 3. A weighted draw, so groups still spread over the good camps.
    /// All factors are improvised; no monster or combat number changes.
    /// </summary>
    public static class SmartCampChooser
    {
        public readonly record struct Candidate(string Id, string ZoneName, ushort Region, int X, int Y, int Level, int LiveMobs);

        public sealed record Choice(Candidate Camp, double Weight, double Safety, double Learned, string Why);

        /// <summary>Pure: the safety factor of a camp from its nearby threats, seen from the group's level.</summary>
        public static double Safety(IReadOnlyList<SmartWorldKnowledge.Threat> threats, int groupLevel, int aggroMargin, out int inAggro)
        {
            double safety = 1;
            inAggro = 0;
            foreach (SmartWorldKnowledge.Threat t in threats ?? Array.Empty<SmartWorldKnowledge.Threat>())
            {
                ConColor con = ConLevels.GetConColor(ConLevels.GetConLevel(Math.Max(0, groupLevel), Math.Max(0, t.Level)));
                if (con == ConColor.GREY)
                    continue;
                if (t.Distance <= t.AggroRange + aggroMargin)
                {
                    inAggro++;
                    safety *= con switch
                    {
                        ConColor.PURPLE => 0.15,
                        ConColor.RED => 0.3,
                        ConColor.ORANGE => 0.6,
                        ConColor.YELLOW => 0.85,
                        _ => 0.95,
                    };
                }
                else if (con >= ConColor.RED)
                    safety *= 0.9;
            }
            return Math.Max(0.01, safety);
        }

        /// <summary>Pure: whether a zone suits the group.</summary>
        public static bool ZoneFits(SmartWorldConfig.ZoneOverride manual, SmartWorldConfig.ZoneGenerated generated,
            int bandLowLevel, int bandHighLevel, int typicalAboveBand)
        {
            if (manual != null)
            {
                if (manual.Skip)
                    return false;
                if (manual.Min.HasValue || manual.Max.HasValue)
                    return (manual.Min ?? 0) <= bandHighLevel && (manual.Max ?? int.MaxValue) >= bandLowLevel;
            }
            return generated == null || generated.Typical <= bandHighLevel + typicalAboveBand;
        }

        /// <summary>Pure: the weights, given the per-camp safety and learned factors.</summary>
        public static double Weight(int liveMobs, int level, int lowestLevel, double safety, double learned) =>
            Math.Min(12, Math.Max(1, liveMobs)) * (1 + 0.25 * Math.Max(0, level - lowestLevel)) * safety * learned;

        /// <summary>Scores the candidates and draws one; null when there are none.</summary>
        public static Choice Choose(IReadOnlyList<Candidate> candidates, int groupLevel, SmartPullBand band, Random random)
        {
            if (candidates == null || candidates.Count == 0)
                return null;
            SmartWorldConfig.Settings settings = SmartWorldConfig.Current;
            int bandLow = int.MaxValue, bandHigh = 0;
            for (int level = 1; level <= groupLevel + 15; level++)
            {
                if (band == null || !band.Allows(groupLevel, level))
                    continue;
                bandLow = Math.Min(bandLow, level);
                bandHigh = Math.Max(bandHigh, level);
            }
            if (bandHigh == 0)
            {
                bandLow = groupLevel;
                bandHigh = groupLevel;
            }

            Candidate[] pool = candidates.Where(c => ZoneFits(SmartWorldConfig.OverrideFor(c.ZoneName),
                SmartWorldKnowledge.ZoneLevels(c.ZoneName), bandLow, bandHigh, settings.ZoneTypicalAboveBand)).ToArray();
            bool zonesNarrowed = pool.Length > 0 && pool.Length < candidates.Count;
            if (pool.Length == 0)
                pool = candidates.ToArray();

            int lowest = pool.Min(c => c.Level);
            var scored = new List<Choice>(pool.Length);
            foreach (Candidate c in pool)
            {
                double safety = Safety(SmartWorldKnowledge.ThreatsNear(c.Id), groupLevel, settings.AggroMargin, out int inAggro);
                double learned = SmartWorldLearning.FactorFor(c.Id, c.Region, c.X, c.Y, out string learnedWhy);
                double weight = Weight(c.LiveMobs, c.Level, lowest, safety, learned);
                scored.Add(new Choice(c, weight, safety, learned,
                    $"safety {safety:0.00} ({inAggro} aggressive in reach), {learnedWhy}, weight {weight:0.0}" +
                    (zonesNarrowed ? $", {pool.Length} of {candidates.Count} camps in suitable zones" : string.Empty)));
            }
            double total = scored.Sum(s => s.Weight);
            if (total <= 0)
                return scored.OrderByDescending(s => s.Weight).First();
            double roll = (random ?? Random.Shared).NextDouble() * total;
            foreach (Choice s in scored)
            {
                roll -= s.Weight;
                if (roll <= 0)
                    return s;
            }
            return scored[^1];
        }
    }
}
