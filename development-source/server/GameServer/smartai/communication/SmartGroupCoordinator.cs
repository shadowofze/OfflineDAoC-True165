using System.Collections.Generic;

namespace DOL.GS.SmartAI
{
    public enum SmartGroupStrategy : byte { StandardAssistTrain, EmergencyTriage, ProtectBackline, RetreatAdvised }

    /// <summary>What the arbitration of one tick decided.</summary>
    public sealed record SmartArbitration(SmartGroupStrategy Strategy, object TargetSwitch, object PeelTarget,
        SmartBotObservation? Lead, string Reason)
    {
        public static readonly SmartArbitration None = new(SmartGroupStrategy.StandardAssistTrain, null, null, null, string.Empty);
    }

    /// <summary>What the arbitration needs to know about the group.</summary>
    public readonly record struct SmartArbitrationContext(bool PlayerLed, bool HumanMainAssistHasTarget, object CurrentTarget,
        long CurrentTargetLockUntil, long Now);

    /// <summary>
    /// Resolves a tick's observations deterministically, on the game loop thread (plan Phase 8, architecture 4.4):
    /// - Tier 0, the human: a human main assist with a target is never overruled on targets, and a player's group never
    ///   retreats (companions don't abandon a fighting player: a retreat call becomes emergency triage).
    /// - Tier 1, survival: a healer under attack (urgency 80+) protects the backline: its attacker becomes the peel
    ///   target. Heavy damage (80+) means emergency triage. These override offensive suggestions.
    /// - Tier 2, target calls: the best target switch (urgency x authority) replaces the group's target only once its
    ///   hold has run out and it beats the current target's standing score by at least 20.
    /// Ties go to the higher score, then the lower poster id, so the result never depends on posting order.
    /// </summary>
    public static class SmartGroupCoordinator
    {
        /// <summary>The standing score of the group's current target.</summary>
        public const int CurrentTargetScore = 50;
        /// <summary>A switch must beat the current target by this much.</summary>
        public const int SwitchMargin = 20;
        public const int SurvivalUrgency = 80;

        public static SmartArbitration Arbitrate(IReadOnlyList<SmartBotObservation> observations, SmartArbitrationContext c)
        {
            if (observations.Count == 0)
                return SmartArbitration.None;

            SmartBotObservation? underAttack = Best(observations, SmartObservationKind.HealerUnderAttack, SurvivalUrgency);
            SmartBotObservation? heavy = Best(observations, SmartObservationKind.HeavyGroupDamageWarning, SurvivalUrgency);
            SmartBotObservation? retreat = Best(observations, SmartObservationKind.RecommendRetreat, 0);

            // Tier 1: survival.
            if (underAttack is { } ua)
                return new SmartArbitration(SmartGroupStrategy.ProtectBackline, null, ua.Target, ua, "healer under attack");
            if (retreat is { } r)
                return c.PlayerLed
                    ? new SmartArbitration(SmartGroupStrategy.EmergencyTriage, null, null, r, "retreat advised; the player's group holds")
                    : new SmartArbitration(SmartGroupStrategy.RetreatAdvised, null, null, r, "retreat advised");
            if (heavy is { } h)
                return new SmartArbitration(SmartGroupStrategy.EmergencyTriage, null, null, h, "heavy group damage");

            // Tier 2: target calls (tier 0: never over a human main assist's own target).
            SmartBotObservation? best = null;
            foreach (SmartBotObservation o in observations)
            {
                if (o.Kind is not (SmartObservationKind.RecommendTargetSwitch or SmartObservationKind.TargetVulnerable) ||
                    o.Target == null || ReferenceEquals(o.Target, c.CurrentTarget))
                    continue;
                if (best == null || Better(o, best.Value))
                    best = o;
            }
            if (best is { } b && !c.HumanMainAssistHasTarget &&
                (c.CurrentTarget == null || c.Now >= c.CurrentTargetLockUntil && b.Score >= CurrentTargetScore + SwitchMargin))
                return new SmartArbitration(SmartGroupStrategy.StandardAssistTrain, b.Target, null, b, "target switch");

            // Nothing decisive: the loudest remaining observation still leads (for callouts).
            SmartBotObservation? lead = null;
            foreach (SmartBotObservation o in observations)
                if (lead == null || Better(o, lead.Value))
                    lead = o;
            return new SmartArbitration(SmartGroupStrategy.StandardAssistTrain, null, null, lead, string.Empty);
        }

        private static SmartBotObservation? Best(IReadOnlyList<SmartBotObservation> observations, SmartObservationKind kind, int minUrgency)
        {
            SmartBotObservation? best = null;
            foreach (SmartBotObservation o in observations)
                if (o.Kind == kind && o.Urgency >= minUrgency && (best == null || Better(o, best.Value)))
                    best = o;
            return best;
        }

        private static bool Better(SmartBotObservation a, SmartBotObservation b) =>
            a.Score != b.Score ? a.Score > b.Score : a.PosterId < b.PosterId;
    }
}
