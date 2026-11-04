using System;
using System.Runtime.CompilerServices;
using DOL.GS.RealmAbilities;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI's realm abilities (plan Phase 6), for every bot it drives: hired companions (the abilities their player
    /// chose in the companion window) and gamebots alike.
    /// - Only the actives whose handlers work for a bot: the four gamebots already use (Purge, Ignore Pain, Second Wind,
    ///   First Aid) plus Mystic Crystal Lore (its handler only messages players). Mastery of Concentration and Bunker of
    ///   Faith are player-only in their handlers (they do nothing, or fail, for a bot) and stay out.
    /// - A spec fires one only when its section 11 lists it, the line's condition holds, and the ability's own hard
    ///   precondition holds (no Purge without crowd control on the bot...). One use per second at most.
    /// - The ability executes through its own handler (cost, reuse timer and effects unchanged).
    /// </summary>
    public static class SmartRealmAbilities
    {
        public const string Mcl = "AtlasOF_MCL";
        public const long CheckIntervalMs = 1_000;

        private static readonly string[] Safe =
        [
            AutonomousBotRealmAbilities.Purge, AutonomousBotRealmAbilities.PurgeReduced,
            AutonomousBotRealmAbilities.IgnorePain, AutonomousBotRealmAbilities.IgnorePainTank,
            AutonomousBotRealmAbilities.SecondWind, AutonomousBotRealmAbilities.FirstAid, Mcl,
        ];

        public static bool IsSafe(string key) => Array.IndexOf(Safe, key) >= 0;

        /// <summary>The ability's own precondition, before the template's condition.</summary>
        public static bool Wanted(string key, in SmartBotPerception p) => key switch
        {
            AutonomousBotRealmAbilities.Purge or AutonomousBotRealmAbilities.PurgeReduced => p.Incapacitated,
            AutonomousBotRealmAbilities.IgnorePain or AutonomousBotRealmAbilities.IgnorePainTank =>
                p.InCombat && !p.Incapacitated && p.SelfHealthPercent < 50,
            AutonomousBotRealmAbilities.SecondWind => p.InCombat && !p.Incapacitated && p.SelfEndurancePercent < 25,
            AutonomousBotRealmAbilities.FirstAid => !p.InCombat && p.SelfHealthPercent < 75,
            Mcl => !p.InCombat && p.SelfManaPercent < 75,
            _ => false,
        };

        /// <summary>The first ability of the spec's list that is wanted now (pure, for tests).</summary>
        public static SmartAbilityRule Choose(SmartSpecProfile profile, in SmartBotPerception p, Func<string, bool> ready)
        {
            foreach (SmartAbilityRule rule in profile.Abilities)
            {
                if (Wanted(rule.Key, p) && rule.Condition.Holds(p) && ready(rule.Key))
                    return rule;
            }
            return null;
        }

        private sealed class Gate { public long NextCheck; }
        private static readonly ConditionalWeakTable<GameBot, Gate> Gates = new();

        /// <summary>Fires the chosen ability. True when one was used (its reuse timer started).</summary>
        public static bool TryUse(GameBot bot, SmartSpecProfile profile, in SmartBotPerception p, out SmartAbilityRule used)
        {
            used = null;
            if (profile.Abilities.Count == 0)
                return false;
            Gate gate = Gates.GetValue(bot, static _ => new Gate());
            long now = GameLoop.GameLoopTime;
            if (now < gate.NextCheck)
                return false;
            gate.NextCheck = now + CheckIntervalMs;

            SmartAbilityRule rule = Choose(profile, p, key => bot.GetAbility(key) is TimedRealmAbility ra && bot.GetSkillDisabledDuration(ra) <= 0);
            if (rule == null || bot.GetAbility(rule.Key) is not TimedRealmAbility ability)
                return false;
            ability.Execute(bot);
            // A Purge with nothing to remove only blocks itself for a moment; only a real use starts the long timer.
            if (bot.GetSkillDisabledDuration(ability) <= 1_000)
                return false;
            used = rule;
            return true;
        }
    }
}
