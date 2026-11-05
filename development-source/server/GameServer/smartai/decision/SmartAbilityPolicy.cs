using System;
using System.Text.RegularExpressions;
using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI's class abilities (owner 2026-11-04): the legacy handlers do the work (Guard, Protect, Intercept, Berserk
    /// keep their rules, reuse timers and effects); SmartAI decides where and when.
    /// - Guard and Protect: a spec whose lines put them on the healer ("Guard on Primary Cleric: Priority 100 (Keep
    ///   active 100% of time)") keeps them on the group's healer instead of the member being hit.
    /// - Berserk (Vendo Form): never below 40% health (the Berserker template), and in PvE not on a monster that is
    ///   nearly dead (under 30%; an improvised figure - the template says "priority target").
    /// - Interrupts: a caster with an instant harmful spell turns it on an enemy in its fight that is casting harm.
    /// - Stag, Triple Wield, Flurry, Dirty Tricks: not spent on a nearly dead monster (AllowsCombatCooldown).
    /// Engage (Shields 7) against archers is BotBrain.TryEngageArcher, for every bot.
    /// </summary>
    public static class SmartAbilityPolicy
    {
        public const int BerserkMinSelfHealthPercent = 40;
        public const int BerserkMinMonsterHealthPercent = 30;
        public const int InterruptRange = 1_500;

        private static readonly Regex HealerWords = new(@"\b(healer|cleric|druid|shaman|friar|bard|priest)s?\b",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>Pure: a priority line that puts this ability on the healer.</summary>
        public static bool LineOnHealer(string ability, string lineName, string condition) =>
            Regex.IsMatch(lineName, $@"\b{ability}\b", RegexOptions.IgnoreCase) &&
            (HealerWords.IsMatch(lineName) || HealerWords.IsMatch(condition ?? string.Empty));

        /// <summary>Pure: Berserk is worth it now.</summary>
        public static bool BerserkAllowed(int selfHealthPercent, int targetHealthPercent, bool targetIsPlayerLike) =>
            selfHealthPercent >= BerserkMinSelfHealthPercent && (targetIsPlayerLike || targetHealthPercent >= BerserkMinMonsterHealthPercent);

        /// <summary>The bot's SmartAI profile when SmartAI drives it, else null.</summary>
        private static SmartSpecProfile Driving(GameBot bot)
        {
            if (!SmartAiConfig.IsEnabled || bot == null)
                return null;
            try
            {
                return SmartAiSpecRegistry.Active.Resolve(bot) is { } spec ? SmartSpecProfile.For(spec) : null;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("ability policy", e);
                return null;
            }
        }

        /// <summary>Pure: a long-reuse combat ability (Stag, Triple Wield) is worth it on this target - not on a monster
        /// that is nearly dead (the same improvised 30% as Berserk); a player-like enemy any time.</summary>
        public static bool CombatCooldownAllowed(int targetHealthPercent, bool targetIsPlayerLike) =>
            targetIsPlayerLike || targetHealthPercent >= BerserkMinMonsterHealthPercent;

        /// <summary>The legacy Stag / Triple Wield use, gated for a bot SmartAI drives (true: as legacy).</summary>
        public static bool AllowsCombatCooldown(GameBot bot, GameLiving target)
        {
            if (Driving(bot) == null)
                return true;
            return target != null && CombatCooldownAllowed(target.HealthPercent, SmartGameModeDetector.IsPlayerLikeEnemy(bot, target));
        }

        /// <summary>Guard goes on the healer for this bot (SmartAI drives it and its spec says so).</summary>
        public static bool GuardsHealer(GameBot bot) => Driving(bot)?.GuardsHealer == true;

        /// <summary>Protect goes on the healer for this bot.</summary>
        public static bool ProtectsHealer(GameBot bot) => Driving(bot)?.ProtectsHealer == true;

        /// <summary>The legacy Berserk use, gated for a bot SmartAI drives (true: as legacy).</summary>
        public static bool AllowsBerserk(GameBot bot, GameLiving target)
        {
            if (Driving(bot) == null)
                return true;
            return target != null && BerserkAllowed(bot.HealthPercent, target.HealthPercent,
                SmartGameModeDetector.IsPlayerLikeEnemy(bot, target));
        }
    }
}
