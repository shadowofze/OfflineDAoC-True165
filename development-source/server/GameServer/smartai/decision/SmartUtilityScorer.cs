namespace DOL.GS.SmartAI
{
    /// <summary>
    /// Stage 4: a candidate's score (architecture section 3.1, the parts that exist so far):
    /// spec base priority + context urgency, times the resource factor. Personality (Phase 10), game mode (Phase 9),
    /// group claims (Phase 7) and target stickiness (Phase 11) join later. All integers, no allocation.
    /// </summary>
    public static class SmartUtilityScorer
    {
        public const int EmergencyTriageBonus = 40;
        public const int WoundedTriageBonus = 20;
        public const int GroupTriageBonus = 30;
        public const int PlayerBurstTriageBonus = 10;
        /// <summary>Anti-oscillation (architecture section 8): the action chosen in the last think, within the window,
        /// keeps +15, so two near-equal choices don't alternate every think.</summary>
        public const int StickinessBonus = 15;
        public const long StickinessWindowMs = 1_500;

        public static int Score(SmartActionFamily family, int basePriority, SmartSpecProfile profile, in SmartBotPerception p)
        {
            int score = basePriority;
            if (family == SmartActionFamily.Triage)
            {
                if (p.MembersBelow40 > 0)
                    score += EmergencyTriageBonus;
                else if (p.MembersBelow65 > 0)
                    score += WoundedTriageBonus;
                // The group called emergency triage (Phase 8): everyone allowed to heal puts healing first.
                if (p.GroupStrategy == SmartGroupStrategy.EmergencyTriage)
                    score += GroupTriageBonus;
                // Against players (Phase 9) damage comes in bursts: a wounded member is healed sooner than in PvE.
                if (p.Mode != SmartGameMode.PvE && p.MembersBelow65 > 0)
                    score += PlayerBurstTriageBonus;
            }
            // Personality (Phase 10): a bounded multiplier, 80-125%. Not in an emergency (someone under 40%): then the
            // spec's own order stands, so an Aggressive Cleric still heals a dying tank before it smites (section 5.1).
            if (p.Personality != null && p.MembersBelow40 == 0)
                score = score * p.Personality.Multiplier(family, p) / 100;
            // Resource: offensive spells below the mana floor score a quarter; heals and buffs never do.
            int floor = p.ManaFloorOverride > 0 ? p.ManaFloorOverride : profile.ManaFloorPercent;
            if (family == SmartActionFamily.OffenseSpells && floor > 0 && p.SelfManaPercent < floor)
                score /= 4;
            // Stickiness (Phase 11), after everything else, and never in an emergency: then the spec's order decides.
            if (p.StickyFamily == family && p.MembersBelow40 == 0)
                score += StickinessBonus;
            return score;
        }
    }
}
