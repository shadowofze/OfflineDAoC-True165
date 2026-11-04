using System;

namespace DOL.GS.SmartAI
{
    public enum SmartPersonality : byte { Auto, Aggressive, Defensive, Tactical, Opportunistic, Supportive, Conservative }

    /// <summary>
    /// One personality's weights (plan Phase 10, architecture section 5.1). Percent integers, no floats:
    /// - Offense / Defense: multipliers on the offensive and the healing/buff families.
    /// - ManaReserve: the offensive mana floor when the personality is not the spec's own default.
    /// - The bonuses: Opportunistic +25% offense on a target under 35%, Supportive +25% triage, Tactical +20% offense on
    ///   the group's target, Defensive peels like a tank (+30% in the table, as a peeler role here).
    /// Every final multiplier is held to 80-125% (section 5.1), and no personality passes a hard rule gate: an
    /// Aggressive Cleric still heals first when its spec says so, and nobody breaks a mez.
    /// </summary>
    public sealed record SmartPersonalityProfile(SmartPersonality Personality, int Offense, int Defense, int ManaReserve,
        int VulnerableOffenseBonus, int TriageBonus, int GroupTargetBonus, bool Peels)
    {
        public const int MinMultiplier = 80, MaxMultiplier = 125;
        public const int VulnerableBelowPercent = 35;

        public static readonly SmartPersonalityProfile Neutral = new(SmartPersonality.Auto, 100, 100, 0, 0, 0, 0, false);

        private static readonly SmartPersonalityProfile[] All =
        [
            Neutral,
            new(SmartPersonality.Aggressive, 120, 85, 15, 0, 0, 0, false),
            new(SmartPersonality.Defensive, 85, 125, 30, 0, 0, 0, true),
            new(SmartPersonality.Tactical, 105, 105, 20, 0, 0, 20, false),
            new(SmartPersonality.Opportunistic, 115, 95, 20, 25, 0, 0, false),
            new(SmartPersonality.Supportive, 80, 120, 35, 0, 25, 0, false),
            new(SmartPersonality.Conservative, 90, 115, 35, 0, 0, 0, false),
        ];

        public static SmartPersonalityProfile For(SmartPersonality personality) => All[(int)personality];

        public static bool TryParse(string text, out SmartPersonality personality)
        {
            personality = SmartPersonality.Auto;
            return !string.IsNullOrWhiteSpace(text) && !int.TryParse(text, out _) &&
                   Enum.TryParse(text.Trim(), true, out personality) && Enum.IsDefined(personality);
        }

        /// <summary>The multiplier for a family on this think, held to 80-125%.</summary>
        public int Multiplier(SmartActionFamily family, in SmartBotPerception p)
        {
            int m;
            if (family is SmartActionFamily.OffenseSpells or SmartActionFamily.OffenseFull)
            {
                m = Offense;
                if (VulnerableOffenseBonus > 0 && p.HasTarget && p.TargetHealthPercent < VulnerableBelowPercent)
                    m = m * (100 + VulnerableOffenseBonus) / 100;
                if (GroupTargetBonus > 0 && p.TargetIsGroupTarget)
                    m = m * (100 + GroupTargetBonus) / 100;
            }
            else
            {
                m = Defense;
                if (family == SmartActionFamily.Triage && TriageBonus > 0)
                    m = m * (100 + TriageBonus) / 100;
            }
            return Math.Clamp(m, MinMultiplier, MaxMultiplier);
        }
    }
}
