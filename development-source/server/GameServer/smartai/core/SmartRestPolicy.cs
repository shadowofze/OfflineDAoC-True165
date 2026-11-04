using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI resting for healers and casters in a player's group (owner 2026-11-04). The legacy rule waits for 45%
    /// power and wakes the moment the player moves; with SmartAI on, a healer or caster companion:
    /// - sits when its power is under 90% (or the legacy health/endurance thresholds call for it) and nobody in the
    ///   group is fighting, once its player has stopped and it is back in formation (within 350);
    /// - keeps resting until full while its player stays within 800 units, even if the player moves about;
    /// - stands and follows as normal when the player goes more than 800 units away, or when any combat starts (the
    ///   existing combat wake-ups are untouched).
    /// Off, or for other bots, the legacy rule applies unchanged. Regeneration itself is the game's (no numbers change).
    /// </summary>
    public static class SmartRestPolicy
    {
        public const int PowerRestBelowPercent = 90;
        public const int FollowBeyondDistance = 800;
        /// <summary>It sits down only once back in formation (the legacy rest point); it keeps resting out to 800.</summary>
        public const int StartWithinDistance = 350;

        /// <summary>A bot this rule is for: SmartAI on, in a player's group, a healer or caster with power.</summary>
        public static bool Applies(GameBot bot) =>
            SmartAiConfig.IsEnabled && bot is { IsPlayerLedGroup: true, MaxMana: > 0 } && IsHealerOrCaster(bot);

        public static bool IsHealerOrCaster(GameBot bot)
        {
            if (bot.CharacterClass == null)
                return false;
            var c = (eCharacterClass)bot.CharacterClass.ID;
            return BotPartyRoles.For(c) == BotPartyRole.Support || BotPartyRoles.IsHealingClass(c) ||
                   SmartBotPerception.IsHealingClass(bot.CharacterClass.ID) || BotSpellPower.IsOffensiveCaster(bot);
        }

        /// <summary>The rule, on its own for tests.</summary>
        public static bool ShouldRest(bool combat, bool leaderPresent, int leaderDistance, bool leaderMoving, bool alreadyResting,
            int health, int mana, int endurance)
        {
            if (combat || !leaderPresent || leaderDistance > FollowBeyondDistance)
                return false;
            if (alreadyResting)
                return !AutonomousRestPolicy.IsFullyRecovered((byte)health, (byte)mana, (byte)endurance, true);
            return !leaderMoving && leaderDistance <= StartWithinDistance &&
                   (mana < PowerRestBelowPercent || AutonomousRestPolicy.NeedsRecovery((byte)health, (byte)mana, (byte)endurance, true));
        }

        /// <summary>The rule for a live bot following its player.</summary>
        public static bool ShouldRest(GameBot bot, GamePlayer leader)
        {
            bool present = leader is { IsAlive: true } && leader.CurrentRegionID == bot.CurrentRegionID;
            return ShouldRest(BotRestRecovery.BlocksRest(bot), present, present ? (int)bot.GetDistanceTo(leader) : int.MaxValue,
                present && leader.IsMoving, bot.IsRecoveryResting, bot.HealthPercent, bot.ManaPercent, bot.EndurancePercent);
        }
    }
}
