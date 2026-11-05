using DOL.GS.Spells;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// QuickCast for SmartAI bots (owner 2026-11-04: "add quickcast for the list casters only").
    /// - Who: the 1.65 list casters whose class has QuickCast in the game data (CharacterQuickcastUserCareer): Wizard,
    ///   Sorcerer, Theurgist, Cabalist, Runemaster, Spiritmaster, Bonedancer, Eldritch, Enchanter, Mentalist, Animist -
    ///   and only when the bot really has the ability. No healer, hybrid or Necromancer.
    /// - The player rules, unchanged: the same effect (the next spell takes 2 seconds and can't be interrupted, at double
    ///   power), not while casting or in melee attack mode, and 30 seconds before the next QuickCast once a quick-cast
    ///   spell went off (the spell handler's own lockout, now applied to these bots as to players).
    /// - When (SmartAI): about to cast offensively while something is hitting the bot or a melee enemy is on it, with
    ///   power for a doubled cost (at least 20%; an improvised floor). A Mentalist also quick-casts its heal under the
    ///   same pressure when a group member or itself is below 75% (owner 2026-11-04).
    /// Without SmartAI, or for any other bot, QuickCast stays off as before (GameBot.CastSpell clears it).
    /// </summary>
    public static class SmartQuickCast
    {
        public const int MinManaPercent = 20;

        /// <summary>A Mentalist quick-casts a heal under attack when a group member (or itself) is below this (improvised).</summary>
        public const int MentalistHealPercent = 75;

        /// <summary>Pure: a 1.65 list caster class.</summary>
        public static bool IsListCaster(int classId) => (eCharacterClass)classId is
            eCharacterClass.Wizard or eCharacterClass.Sorcerer or eCharacterClass.Theurgist or eCharacterClass.Cabalist or
            eCharacterClass.Runemaster or eCharacterClass.Spiritmaster or eCharacterClass.Bonedancer or
            eCharacterClass.Eldritch or eCharacterClass.Enchanter or eCharacterClass.Mentalist or eCharacterClass.Animist;

        /// <summary>The spell code honours this caster's QuickCast effect (a player, or a SmartAI list caster bot).</summary>
        public static bool Honours(GameLiving caster) =>
            caster is not GameBot bot ||
            SmartAiConfig.IsEnabled && bot.CharacterClass != null && IsListCaster(bot.CharacterClass.ID) &&
            bot.HasAbility(Abilities.Quickcast);

        /// <summary>Pure: the moment calls for QuickCast.</summary>
        public static bool Wanted(bool underPressure, int manaPercent, bool inMeleeAttackMode, bool lockedOut) =>
            underPressure && manaPercent >= MinManaPercent && !inMeleeAttackMode && !lockedOut;

        /// <summary>Arms QuickCast for the bot's next spell when it is wanted. True when armed (or already armed).</summary>
        public static bool TryArm(GameBot bot, bool underPressure)
        {
            if (bot == null || bot is not { IsAlive: true } || !Honours(bot) || bot.CurrentSpellHandler != null)
                return false;
            if (EffectListService.GetAbilityEffectOnTarget(bot, eEffect.QuickCast) != null)
                return true;
            Ability qc = bot.GetAbility(Abilities.Quickcast);
            long sinceLast = bot.CurrentRegion.Time - bot.TempProperties.GetProperty<long>(GamePlayer.QUICK_CAST_CHANGE_TICK);
            bool lockedOut = qc == null || bot.GetSkillDisabledDuration(SkillBase.GetAbility(Abilities.Quickcast)) > 0 ||
                             bot.GetSkillDisabledDuration(qc) > 0 ||
                             bot.TempProperties.GetProperty<long>(GamePlayer.QUICK_CAST_CHANGE_TICK) != 0 &&
                             sinceLast < QuickCastAbilityHandlerDisable;
            if (!Wanted(underPressure, bot.MaxMana > 0 ? bot.ManaPercent : 0, bot.attackComponent?.AttackState == true, lockedOut))
                return false;
            ECSGameEffectFactory.Create(new(bot, QuickCastECSGameEffect.DURATION, 1),
                static (in ECSGameEffectInitParams i) => new QuickCastECSGameEffect(i));
            return true;
        }

        private const int QuickCastAbilityHandlerDisable = SkillHandler.QuickCastAbilityHandler.DISABLE_DURATION;
    }
}