using System.Collections.Generic;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// Optional party-chat callouts from SmartAI bots to their player (plan Phase 8, architecture section 4.3):
    /// - only in a group with a human player, and only while smart_ai_group_callouts is on;
    /// - at most one per group every 8 seconds, and the same bot never repeats the same kind within 30 seconds;
    /// - only for an observation the arbitration led with, urgency 70 or more.
    /// Sent as ordinary party chat ([Party] Name: "...").
    /// </summary>
    public sealed class SmartGroupCalloutPolicy
    {
        public const long GroupIntervalMs = 8_000;
        public const long RepeatIntervalMs = 30_000;
        public const int MinUrgency = 70;

        private long _nextAllowed;
        private readonly Dictionary<(int PosterId, SmartObservationKind Kind), long> _lastSaid = new();

        /// <summary>Whether this observation may be called out now; records it when so. Single-threaded (the pre-pass).</summary>
        public bool TryAllow(in SmartBotObservation o, long now)
        {
            if (o.Urgency < MinUrgency || now < _nextAllowed || Text(o, "x") == null)
                return false;
            if (_lastSaid.TryGetValue((o.PosterId, o.Kind), out long last) && now - last < RepeatIntervalMs)
                return false;
            _nextAllowed = now + GroupIntervalMs;
            _lastSaid[(o.PosterId, o.Kind)] = now;
            return true;
        }

        /// <summary>The line a bot says for an observation, or null for kinds it keeps to itself.</summary>
        public static string Text(in SmartBotObservation o, string targetName) => o.Kind switch
        {
            SmartObservationKind.HealerUnderAttack => targetName != null ? $"{targetName} is on me, peel!" : "I'm being attacked, peel!",
            SmartObservationKind.HeavyGroupDamageWarning => "Heavy damage, I can't keep up!",
            SmartObservationKind.HealerLowMana => "Low on power, slow down!",
            SmartObservationKind.TargetVulnerable or SmartObservationKind.RecommendTargetSwitch =>
                targetName != null ? $"Switching to {targetName}!" : null,
            SmartObservationKind.RecommendRetreat => "We're losing this, pull back!",
            _ => null,
        };
    }
}
