namespace DOL.GS.SmartAI
{
    /// <summary>
    /// Stage 2: the hard rules, applied before any scoring (architecture section 3.1). A family a gate closes is never a
    /// candidate, however high the spec ranks it.
    /// - No offense on a mesmerized or breakably rooted target (never break a mez).
    /// - No melee for a spec that bans it (priority 0 melee, or a hard MUST NOT).
    /// - No spells while silenced.
    /// The owner's orders, casts in progress and passive stance are the legacy brain's, before SmartAI runs
    /// (BotBrain.SmartAiLegacyOwnsTurn and the AGGRO state's own checks).
    /// </summary>
    public static class SmartRuleGateEvaluator
    {
        public static bool Allows(SmartActionFamily family, SmartSpecProfile profile, in SmartBotPerception p) => family switch
        {
            SmartActionFamily.Triage => !p.Silenced,
            SmartActionFamily.Support => !p.Silenced,
            SmartActionFamily.OffenseSpells => !p.Silenced && p.HasTarget && !p.TargetControlled,
            SmartActionFamily.OffenseFull => !profile.MeleeBanned && p.HasTarget && !p.TargetControlled,
            _ => false,
        };
    }
}
