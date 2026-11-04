namespace DOL.GS.SmartAI
{
    public enum SmartObservationKind : byte
    {
        RecommendTargetSwitch,
        TargetVulnerable,
        HeavyGroupDamageWarning,
        HealerUnderAttack,
        HealerLowMana,
        CrowdControlOpportunity,
        EnemyDangerousCast,
        RecommendRetreat,
        RecommendPressAdvantage,
    }

    /// <summary>
    /// One structured message a SmartAI bot posts to its group during its think (plan Phase 8, architecture section
    /// 4.3). No strings, no chat: the coordinator reads them on the next pre-pass and decides. Poster and Target are the
    /// livings involved (object, so the arbitration is testable without game objects); Urgency is 0-100; Weight is the
    /// poster's authority in percent (the main assist counts more).
    /// </summary>
    public readonly record struct SmartBotObservation(SmartObservationKind Kind, object Poster, object Target, int Urgency,
        int Weight, int PosterId, long Tick)
    {
        public int Score => Urgency * Weight / 100;
    }
}
