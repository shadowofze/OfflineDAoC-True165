using System.Collections.Generic;

namespace DOL.GS.SmartAI
{
    public enum SmartEngagementStatus : byte { Disengaged, Committed }

    /// <summary>One injured member, for triage order: lowest health first.</summary>
    public sealed record SmartHealPriority(GameLiving Member, int HealthPercent, int Attackers);

    /// <summary>
    /// A group's tactical view for one tick (plan Phase 7, architecture section 4.2), the parts built so far:
    /// main assist, primary target (with its hold), injured members in triage order, engagement status.
    /// Immutable: SmartAiCoordinator builds a new one on the game loop thread before the brains think and swaps it in
    /// whole, so every bot of the group reads the same view and never a half-written one. Enemy classes, interrupt
    /// rotations, positioning and strategy come with Phases 8 and 9.
    /// </summary>
    public sealed class SmartGroupTacticalSnapshot
    {
        public static readonly SmartGroupTacticalSnapshot Empty = new();

        /// <summary>Counts up with every publish (diagnostics, and the readers' consistency check in tests).</summary>
        public long Version { get; init; }
        /// <summary>The game loop time it was built at.</summary>
        public long Tick { get; init; }
        public GameLiving MainAssist { get; init; }
        public string MainAssistReason { get; init; } = string.Empty;
        public GameLiving PrimaryTarget { get; init; }
        /// <summary>The primary target is held until then unless it dies, is held by crowd control or leaves reach, or a
        /// human main assist picks another (architecture section 8: anti-thrashing).</summary>
        public long PrimaryTargetLockUntil { get; init; }
        public SmartEngagementStatus Engagement { get; init; }
        public IReadOnlyList<SmartHealPriority> HealingPriorities { get; init; } = [];
        public int GroupMinHealthPercent { get; init; } = 100;
        /// <summary>The arbitration's strategy (Phase 8), held for StrategyHoldMs after it was last decided.</summary>
        public SmartGroupStrategy Strategy { get; init; }
        public string StrategyReason { get; init; } = string.Empty;
        public long StrategyUntil { get; init; }
        /// <summary>Protect the backline: the enemy on the group's healer.</summary>
        public GameLiving PeelTarget { get; init; }
        /// <summary>The group's fight: PvE, PvP or RvR (Phase 9).</summary>
        public SmartGameMode Mode { get; init; }
        /// <summary>Observations the last pre-pass arbitrated.</summary>
        public int ObservationsRead { get; init; }
        public int MemberCount { get; init; }
        /// <summary>For tests: a value derived from the others, so a torn read would show.</summary>
        public long Checksum { get; init; }

        /// <summary>Built this tick or the one before (older means the group stopped publishing).</summary>
        public bool IsFresh(long now) => Version > 0 && now - Tick <= SmartAiCoordinator.SnapshotFreshMs;
    }
}
