using System;
using System.Collections.Generic;
using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>A group member as the main-assist election sees it.</summary>
    public readonly record struct SmartMemberRole(bool IsHuman, bool IsGroupLeader, bool IsAlive, bool IsTank, bool IsSupport);

    /// <summary>
    /// Builds a group's snapshot on the game loop thread (plan Phase 7). The choices are pure functions, tested on
    /// their own:
    /// - Main assist (architecture section 4.4, tier 0 and 2): the human group leader when there is one (the owner's
    ///   authority), else the first living tank bot, else the first living bot that is not a support, else the first
    ///   living member.
    /// - Primary target: what the main assist is fighting, else the enemy most members are fighting; held 3.5 s
    ///   against switching unless it dies, is held by crowd control, leaves reach, or a human main assist picks another.
    /// </summary>
    public static class SmartGroupSnapshotBuilder
    {
        public const long TargetHoldMs = 3_500;
        public const int MaxHealPriorities = 8;
        public const int TargetReach = 2_500;

        public static int ElectMainAssist(IReadOnlyList<SmartMemberRole> members)
        {
            for (int i = 0; i < members.Count; i++)
                if (members[i] is { IsHuman: true, IsGroupLeader: true, IsAlive: true })
                    return i;
            for (int i = 0; i < members.Count; i++)
                if (members[i] is { IsHuman: false, IsAlive: true, IsTank: true })
                    return i;
            for (int i = 0; i < members.Count; i++)
                if (members[i] is { IsHuman: false, IsAlive: true, IsSupport: false })
                    return i;
            for (int i = 0; i < members.Count; i++)
                if (members[i].IsAlive)
                    return i;
            return -1;
        }

        /// <summary>The primary target after hysteresis: keep the previous while held and still valid, unless a human
        /// main assist chose another; otherwise take the candidate (starting a new hold when it changes).</summary>
        public static (T Target, long LockUntil) HoldTarget<T>(T previous, long previousLockUntil, bool previousValid,
            T candidate, bool candidateFromHuman, long now) where T : class
        {
            if (previous != null && previousValid)
            {
                if (candidate == null || ReferenceEquals(candidate, previous))
                    return (previous, previousLockUntil);
                if (!candidateFromHuman && now < previousLockUntil)
                    return (previous, previousLockUntil);
            }
            if (candidate == null)
                return (null, 0);
            return ReferenceEquals(candidate, previous) ? (candidate, previousLockUntil) : (candidate, now + TargetHoldMs);
        }

        public const long StrategyHoldMs = 3_000;

        /// <summary>The arbitration's input from the group and its last snapshot.</summary>
        public static SmartArbitrationContext Context(Group group, SmartGroupTacticalSnapshot previous, long now) => new(
            group.GetPlayersInTheGroup().Count > 0,
            previous.MainAssist is GamePlayer && MainAssistTarget(previous.MainAssist) != null,
            previous.PrimaryTarget,
            previous.PrimaryTargetLockUntil,
            now);

        public static SmartGroupTacticalSnapshot Build(Group group, SmartGroupTacticalSnapshot previous, long now) =>
            Build(group, previous, now, SmartArbitration.None, 0);

        public static SmartGroupTacticalSnapshot Build(Group group, SmartGroupTacticalSnapshot previous, long now,
            SmartArbitration arbitration, int observationsRead)
        {
            List<GameLiving> members = group.GetMembersInTheGroup();
            if (members.Count == 0)
                return new SmartGroupTacticalSnapshot { Version = previous.Version + 1, Tick = now };
            var roles = new SmartMemberRole[members.Count];
            GamePlayer leader = group.Leader;
            int minHealth = 100;
            bool committed = false;
            var injured = new List<SmartHealPriority>();
            for (int i = 0; i < members.Count; i++)
            {
                GameLiving m = members[i];
                GameBot bot = m as GameBot;
                roles[i] = new SmartMemberRole(m is GamePlayer, m == leader, m.IsAlive, bot != null && BotPartyRoles.IsTank(bot),
                    bot != null && BotPartyRoles.IsSupport(bot));
                if (!m.IsAlive)
                    continue;
                committed |= m.InCombat;
                int hp = m.HealthPercent;
                minHealth = Math.Min(minHealth, hp);
                if (hp < 100)
                    injured.Add(new SmartHealPriority(m, hp, m.attackComponent?.AttackerTracker?.Count ?? 0));
            }
            injured.Sort(static (a, b) => a.HealthPercent != b.HealthPercent ? a.HealthPercent.CompareTo(b.HealthPercent) : b.Attackers.CompareTo(a.Attackers));
            if (injured.Count > MaxHealPriorities)
                injured.RemoveRange(MaxHealPriorities, injured.Count - MaxHealPriorities);

            int maIndex = ElectMainAssist(roles);
            GameLiving ma = maIndex >= 0 ? members[maIndex] : null;
            string reason = maIndex < 0 ? "none" : roles[maIndex].IsHuman ? "human leader" : roles[maIndex].IsTank ? "tank" :
                !roles[maIndex].IsSupport ? "attacker" : "first member";

            // Phase 9: against players, the class priority picks the target (healers and crowd controllers first).
            GameLiving maTarget = MainAssistTarget(ma);
            (GameLiving bestEnemy, int playerEnemies) = BestPlayerEnemy(members);
            SmartGameMode mode = SmartGameModeDetector.Classify(AutonomousWorldBotController.IsBattleground(ma ?? members[0]), playerEnemies);
            GameLiving candidate = ChooseCandidate(ma is GamePlayer, maTarget, SmartEnemyPriority.Of(maTarget),
                bestEnemy, SmartEnemyPriority.Of(bestEnemy), MostFoughtEnemy(members));
            bool previousValid = ValidTarget(ma, previous.PrimaryTarget);
            (GameLiving primary, long lockUntil) = HoldTarget(previous.PrimaryTarget, previous.PrimaryTargetLockUntil, previousValid,
                candidate, ma is GamePlayer && candidate != null && candidate == MainAssistTarget(ma), now);

            // Phase 8: an accepted target switch (the arbitration already applied the hold and the human's authority).
            if (arbitration.TargetSwitch is GameLiving switched && ValidTarget(ma ?? members[0], switched) && switched != primary)
            {
                primary = switched;
                lockUntil = now + TargetHoldMs;
            }

            // The strategy holds a while after it was last decided, so a bot's think rhythm doesn't make it flicker.
            SmartGroupStrategy strategy = arbitration.Strategy;
            string strategyReason = arbitration.Reason;
            long strategyUntil = now + StrategyHoldMs;
            GameLiving peel = arbitration.PeelTarget as GameLiving;
            if (strategy == SmartGroupStrategy.StandardAssistTrain && previous.Strategy != SmartGroupStrategy.StandardAssistTrain &&
                now < previous.StrategyUntil)
            {
                strategy = previous.Strategy;
                strategyReason = previous.StrategyReason;
                strategyUntil = previous.StrategyUntil;
                peel = previous.PeelTarget;
            }
            if (peel != null && !ValidTarget(ma ?? members[0], peel))
                peel = null;

            var snapshot = new SmartGroupTacticalSnapshot
            {
                Version = previous.Version + 1,
                Tick = now,
                MainAssist = ma,
                MainAssistReason = reason,
                PrimaryTarget = primary,
                PrimaryTargetLockUntil = lockUntil,
                Engagement = committed ? SmartEngagementStatus.Committed : SmartEngagementStatus.Disengaged,
                HealingPriorities = injured,
                GroupMinHealthPercent = minHealth,
                MemberCount = members.Count,
                Strategy = strategy,
                StrategyReason = strategyReason,
                StrategyUntil = strategy == SmartGroupStrategy.StandardAssistTrain ? 0 : strategyUntil,
                PeelTarget = peel,
                Mode = mode,
                ObservationsRead = observationsRead,
            };
            return snapshot;
        }

        /// <summary>
        /// The group's target candidate (Phase 9): a human main assist's own target always; otherwise the highest-priority
        /// player-like enemy when it outranks the main assist's target; otherwise the main assist's target, else the
        /// enemy most of the group fights.
        /// </summary>
        public static T ChooseCandidate<T>(bool humanMainAssist, T maTarget, int maTargetPriority, T bestEnemy, int bestPriority,
            T mostFought) where T : class
        {
            if (humanMainAssist && maTarget != null)
                return maTarget;
            if (bestEnemy != null && (maTarget == null || bestPriority > maTargetPriority))
                return bestEnemy;
            return maTarget ?? mostFought;
        }

        /// <summary>The player-like enemy the group fights (its targets and its attackers) with the highest class
        /// priority, the most hurt on a tie; and how many player-like enemies there are.</summary>
        private static (GameLiving Best, int Count) BestPlayerEnemy(List<GameLiving> members)
        {
            var seen = new HashSet<GameLiving>();
            GameLiving best = null;
            int bestPriority = -1, count = 0;
            void Consider(GameLiving from, GameLiving enemy)
            {
                if (enemy == null || !seen.Add(enemy) || !SmartGameModeDetector.IsPlayerLikeEnemy(from, enemy) || !ValidTarget(from, enemy))
                    return;
                count++;
                int priority = SmartEnemyPriority.Of(enemy);
                if (priority > bestPriority || priority == bestPriority && enemy.HealthPercent < best.HealthPercent)
                {
                    best = enemy;
                    bestPriority = priority;
                }
            }
            foreach (GameLiving m in members)
            {
                if (m is not { IsAlive: true })
                    continue;
                Consider(m, m.TargetObject as GameLiving);
                var attackers = m.attackComponent?.AttackerTracker?.Attackers;
                if (attackers != null)
                    foreach (GameLiving a in attackers)
                        Consider(m, a);
            }
            return (best, count);
        }

        /// <summary>What the main assist is fighting: attacking, or casting harm (not crowd control) at.</summary>
        private static GameLiving MainAssistTarget(GameLiving ma)
        {
            if (ma is not { IsAlive: true } || ma.TargetObject is not GameLiving target || !ValidTarget(ma, target))
                return null;
            bool fighting = ma is GamePlayer player ? PlayerLedPullCoordinator.LeaderAttacks(player) : ma.IsAttacking || ma.InCombat;
            return fighting ? target : null;
        }

        private static GameLiving MostFoughtEnemy(List<GameLiving> members)
        {
            GameLiving best = null;
            int bestCount = 0;
            foreach (GameLiving m in members)
            {
                if (m is not { IsAlive: true, InCombat: true } || m.TargetObject is not GameLiving t || !ValidTarget(m, t))
                    continue;
                int count = 0;
                foreach (GameLiving other in members)
                    if (other.IsAlive && other.TargetObject == t)
                        count++;
                if (count > bestCount)
                {
                    best = t;
                    bestCount = count;
                }
            }
            return best;
        }

        private static bool ValidTarget(GameLiving from, GameLiving target) =>
            from != null && target is { IsAlive: true } && target.ObjectState == GameObject.eObjectState.Active &&
            target.CurrentRegionID == from.CurrentRegionID && from.IsWithinRadius(target, TargetReach) &&
            !BotBrain.IsBreakablyControlled(target) && GameServer.ServerRules.IsAllowedToAttack(from, target, true);
    }
}
