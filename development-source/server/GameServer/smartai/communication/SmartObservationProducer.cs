using System;
using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// What a SmartAI bot reports to its group during its think (plan Phase 8). Deliberately few, simple triggers:
    /// - a healer spec (role names "Healer"): HealerUnderAttack when something hits it in melee (80, +5 per extra
    ///   attacker, the attacker as target), HeavyGroupDamageWarning when two members are under 40% or three under 65%
    ///   (85), HealerLowMana under 20% power in a fight (70);
    /// - any spec: TargetVulnerable when its own target is under 25% health and isn't the group's target
    ///   (urgency 100 minus that health; it finishes kills).
    /// The main assist's posts weigh 130%. The group state caps a bot at two posts per tick.
    /// </summary>
    public static class SmartObservationProducer
    {
        public const int VulnerableBelowPercent = 25;

        public static bool IsHealerSpec(SmartSpecProfile profile) =>
            profile.Spec.Role.Contains("Healer", StringComparison.OrdinalIgnoreCase);

        /// <summary>A spec that peels for the backline when the healer is attacked (the template role names).</summary>
        public static bool IsPeelerSpec(SmartSpecProfile profile)
        {
            string role = profile.Spec.Role;
            return !IsHealerSpec(profile) && (role.Contains("Tank", StringComparison.OrdinalIgnoreCase) ||
                role.Contains("Support", StringComparison.OrdinalIgnoreCase) || role.Contains("Buffer", StringComparison.OrdinalIgnoreCase) ||
                role.Contains("Hybrid", StringComparison.OrdinalIgnoreCase) || role.Contains("CrowdControl", StringComparison.OrdinalIgnoreCase) ||
                role.Contains("Peel", StringComparison.OrdinalIgnoreCase) || role.Contains("Guard", StringComparison.OrdinalIgnoreCase));
        }

        public static void Observe(GameBot bot, SmartSpecProfile profile, in SmartBotPerception p, GameLiving target,
            SmartGroupTacticalState state, SmartGroupTacticalSnapshot snapshot, long now)
        {
            int weight = snapshot.MainAssist == bot ? 130 : 100;
            int id = bot.ObjectID;

            if (IsHealerSpec(profile))
            {
                int melee = bot.attackComponent?.AttackerTracker?.MeleeCount ?? 0;
                if (melee > 0 && p.InCombat)
                    state.Post(new SmartBotObservation(SmartObservationKind.HealerUnderAttack, bot, OneAttacker(bot),
                        Math.Min(95, 80 + 5 * (melee - 1)), weight, id, now));
                if (p.MembersBelow40 >= 2 || p.MembersBelow65 >= 3)
                    state.Post(new SmartBotObservation(SmartObservationKind.HeavyGroupDamageWarning, bot, null, 85, weight, id, now));
                if (p.InCombat && p.SelfManaPercent < 20)
                    state.Post(new SmartBotObservation(SmartObservationKind.HealerLowMana, bot, null, 70, weight, id, now));
            }

            if (p.HasTarget && !p.TargetControlled && p.TargetHealthPercent < VulnerableBelowPercent &&
                target != null && target != snapshot.PrimaryTarget)
                state.Post(new SmartBotObservation(SmartObservationKind.TargetVulnerable, bot, target,
                    100 - p.TargetHealthPercent, weight, id, now));
        }

        private static GameLiving OneAttacker(GameBot bot)
        {
            var attackers = bot.attackComponent?.AttackerTracker?.Attackers;
            if (attackers == null)
                return null;
            foreach (GameLiving a in attackers)
                if (a is { IsAlive: true } && a.TargetObject == bot && !BotBrain.IsBreakablyControlled(a))
                    return a;
            return null;
        }
    }
}
