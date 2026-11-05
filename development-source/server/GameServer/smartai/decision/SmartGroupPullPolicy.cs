using System;
using System.Collections.Generic;
using DOL.Database;

namespace DOL.GS.SmartAI
{
    /// <summary>The con colours a SmartAI group may pull, relative to its average level (owner 2026-11-05).</summary>
    public sealed record SmartPullBand(ConColor Lowest, ConColor Highest, int GearScore, string Why)
    {
        /// <summary>The monster level fits the band, seen from the group's average level.</summary>
        public bool Allows(int groupLevel, int monsterLevel)
        {
            ConColor con = ConLevels.GetConColor(ConLevels.GetConLevel(Math.Max(0, groupLevel), Math.Max(0, monsterLevel)));
            return con >= Lowest && con <= Highest;
        }

        /// <summary>The highest level in the band, or 0 when none fits.</summary>
        public int Select(IEnumerable<int> levels, int groupLevel)
        {
            int best = 0;
            foreach (int level in levels ?? Array.Empty<int>())
                if (level > best && Allows(groupLevel, level))
                    best = level;
            return best;
        }

        /// <summary>The highest monster level the band allows, for the goal text.</summary>
        public int HighestLevel(int groupLevel)
        {
            int best = Math.Max(1, groupLevel);
            for (int level = Math.Max(1, groupLevel); level <= groupLevel + 15; level++)
                if (Allows(groupLevel, level))
                    best = level;
            return best;
        }

        public override string ToString() => $"gear {GearScore}, pull {Lowest.ToString().ToLowerInvariant()}-{Highest.ToString().ToLowerInvariant()} ({Why})";
    }

    /// <summary>
    /// SmartAI gear score and pull level (owner 2026-11-05: "implement some type of gear score system for the group or
    /// tank to determine the level to pull, lower levels will be harder to fight higher monsters"). All improvised:
    /// no period source scores gear; this only chooses which camp a SmartAI group walks to and never touches a monster
    /// or combat number.
    /// - Gear score, per member, 0-100: the six armour pieces and the main weapon (the weapon counts double). Each item
    ///   scores its level against the wearer's level (capped at 125% for an item above it), times its quality and its
    ///   condition. An empty slot scores 0. Jewellery, cloaks and shields are not scored.
    /// - The group score is the members' average; the tank's score counts double, since the tank takes the hits.
    /// - The pull band is the highest con colour the group may pull, from the group size, then lowered for low level,
    ///   weak gear, a missing tank or healer, and recent wipes; the lowest colour is one below it.
    /// </summary>
    public static class SmartGroupPullPolicy
    {
        private static readonly eInventorySlot[] ArmourSlots =
        {
            eInventorySlot.HeadArmor, eInventorySlot.HandsArmor, eInventorySlot.FeetArmor,
            eInventorySlot.TorsoArmor, eInventorySlot.LegsArmor, eInventorySlot.ArmsArmor,
        };

        /// <summary>One equipped item as the score sees it.</summary>
        public readonly record struct GearItem(int Level, int Quality, int Condition, int MaxCondition);

        /// <summary>Pure: one item's score, 0-125.</summary>
        public static double ItemScore(GearItem? item, int wearerLevel)
        {
            if (item is not { } i)
                return 0;
            double level = Math.Clamp(i.Level / (double)Math.Max(1, wearerLevel), 0, 1.25);
            double quality = Math.Clamp(i.Quality, 0, 100) / 100.0;
            double condition = i.MaxCondition > 0 ? Math.Clamp(i.Condition / (double)i.MaxCondition, 0, 1) : 1;
            return 100 * level * quality * condition;
        }

        /// <summary>Pure: a member's score from its six armour pieces and its weapon (weight 2), 0-100.</summary>
        public static int MemberScore(IReadOnlyList<GearItem?> armour, GearItem? weapon, int wearerLevel)
        {
            double sum = 0;
            int weight = 0;
            for (int i = 0; i < ArmourSlots.Length; i++)
            {
                sum += ItemScore(i < (armour?.Count ?? 0) ? armour[i] : null, wearerLevel);
                weight++;
            }
            sum += 2 * ItemScore(weapon, wearerLevel);
            weight += 2;
            return (int)Math.Round(Math.Min(100, sum / weight));
        }

        public static int MemberScore(GameLiving member)
        {
            if (member?.Inventory is not { } inv)
                return 0;
            var armour = new GearItem?[ArmourSlots.Length];
            for (int i = 0; i < ArmourSlots.Length; i++)
                armour[i] = Gear(inv.GetItem(ArmourSlots[i]));
            DbInventoryItem weapon = member.ActiveWeapon ??
                inv.GetItem(eInventorySlot.TwoHandWeapon) ?? inv.GetItem(eInventorySlot.RightHandWeapon) ??
                inv.GetItem(eInventorySlot.DistanceWeapon);
            return MemberScore(armour, Gear(weapon), member.Level);
        }

        private static GearItem? Gear(DbInventoryItem item) =>
            item == null ? null : new GearItem(item.Level, item.Quality, item.Condition, item.MaxCondition);

        /// <summary>Pure: the group score; a tank's score counts twice.</summary>
        public static int GroupScore(IReadOnlyList<(int Score, bool Tank)> members)
        {
            if (members == null || members.Count == 0)
                return 0;
            double sum = 0;
            int weight = 0;
            foreach ((int score, bool tank) in members)
            {
                int w = tank ? 2 : 1;
                sum += score * w;
                weight += w;
            }
            return (int)Math.Round(sum / weight);
        }

        /// <summary>
        /// Pure: the pull band.
        /// - Size: 8 purple, 6-7 red, 4-5 orange, smaller yellow.
        /// - Level (lower levels are hit harder by a higher monster; below 10 each level is a whole colour): below 10
        ///   two colours down, 10-19 one down.
        /// - Gear: 90+ as is, 75-89 one down, 55-74 two down, below 55 three down.
        /// - No tank one down, no healer one down; recent wipes one down each (at most two).
        /// - Never above purple or below blue for the top; the bottom is one colour below the top (green under blue).
        /// </summary>
        public static SmartPullBand Band(int groupSize, int averageLevel, int gearScore, bool hasTank, bool hasHealer, int wipes)
        {
            int top = groupSize >= 8 ? 3 : groupSize >= 6 ? 2 : groupSize >= 4 ? 1 : 0;
            var why = new List<string> { $"size {groupSize}" };
            int levelSteps = averageLevel < 10 ? 2 : averageLevel < 20 ? 1 : 0;
            if (levelSteps > 0)
                why.Add($"level {averageLevel} -{levelSteps}");
            int gearSteps = gearScore >= 90 ? 0 : gearScore >= 75 ? 1 : gearScore >= 55 ? 2 : 3;
            if (gearSteps > 0)
                why.Add($"gear -{gearSteps}");
            top -= levelSteps + gearSteps;
            if (!hasTank) { top--; why.Add("no tank -1"); }
            if (!hasHealer) { top--; why.Add("no healer -1"); }
            int wipeSteps = Math.Clamp(wipes, 0, 2);
            if (wipeSteps > 0) { top -= wipeSteps; why.Add($"wipes -{wipeSteps}"); }
            top = Math.Clamp(top, (int)ConColor.BLUE, (int)ConColor.PURPLE);
            int bottom = Math.Max((int)ConColor.GREEN, top - 1);
            return new SmartPullBand((ConColor)bottom, (ConColor)top, gearScore, string.Join(", ", why));
        }

        /// <summary>The band for a live group, or null when SmartAI is off or there is no group.</summary>
        public static SmartPullBand ForGroup(Group group, int averageLevel, int wipePenalty)
        {
            if (!SmartAiConfig.IsEnabled || group == null)
                return null;
            try
            {
                var scores = new List<(int, bool)>();
                bool tank = false, healer = false;
                foreach (GameLiving m in group.GetMembersInTheGroup())
                {
                    if (m == null)
                        continue;
                    eCharacterClass cls = m is GameBot b && b.CharacterClass != null ? (eCharacterClass)b.CharacterClass.ID
                        : m is GamePlayer p && p.CharacterClass != null ? (eCharacterClass)p.CharacterClass.ID : eCharacterClass.Unknown;
                    bool isTank = BotPartyRoles.For(cls) == BotPartyRole.Tank;
                    tank |= isTank;
                    healer |= BotPartyRoles.IsHealingClass(cls);
                    scores.Add((MemberScore(m), isTank));
                }
                return Band(scores.Count, averageLevel, GroupScore(scores), tank, healer, wipePenalty);
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("group pull band", e);
                return null;
            }
        }
    }
}
