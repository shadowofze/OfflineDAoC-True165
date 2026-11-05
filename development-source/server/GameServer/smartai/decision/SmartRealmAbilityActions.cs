using DOL.GS.Effects;
using DOL.GS.RealmAbilities;
using DOL.GS.Spells;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI's own Mastery of Concentration and Bunker of Faith for bots (owner 2026-11-04). Their realm ability
    /// handlers are player-only (Mastery of Concentration returns for a non-player; Bunker of Faith dereferences a player
    /// that a bot isn't), and they are left untouched: this class does for a bot what each handler does for a player,
    /// with the handler's own numbers.
    /// - Mastery of Concentration: the player preconditions (alive, standing, not mezzed or stunned), any earlier one
    ///   cancelled, the cast animation, the handler's reuse timer (GetReUseDelay), and the same effect for the handler's
    ///   15 seconds. The spell code already reads that effect for any caster (no interrupts while it lasts).
    /// - Bunker of Faith: the same preconditions, the handler's own absorb spell (CreateSpell: 50% armor absorption,
    ///   30 seconds) on every living group member (no range, exactly as for a player) who has no other barrier
    ///   (RA_DAMAGE_DECREASE), the handler's animations and reuse timer.
    /// Used only for bots SmartAI drives, when their spec's ability list names them (SmartRealmAbilities).
    /// </summary>
    public static class SmartRealmAbilityActions
    {
        public const string MasteryOfConcentration = "AtlasOF_MasteryOfConcentration";
        public const string BunkerOfFaith = "AtlasOF_BunkerOfFaith";
        public const int BunkerOfFaithDurationMs = 30_000;

        public static bool Handles(string key) => key is MasteryOfConcentration or BunkerOfFaith;

        /// <summary>The handler's preconditions for a bot (DEAD | SITTING | MEZZED | STUNNED).</summary>
        private static bool CanAct(GameLiving bot) => bot.IsAlive && !bot.IsSitting && !bot.IsMezzed && !bot.IsStunned;

        /// <summary>Uses the ability. True when it was used (its reuse timer started).</summary>
        public static bool TryExecute(GameBot bot, Ability ability)
        {
            if (bot == null || ability == null || !CanAct(bot) || bot.GetSkillDisabledDuration(ability) > 0)
                return false;
            return ability.KeyName switch
            {
                MasteryOfConcentration => MoC(bot, ability),
                BunkerOfFaith => BoF(bot, ability),
                _ => false,
            };
        }

        private static bool MoC(GameBot bot, Ability ability)
        {
            if (ability is not TimedRealmAbility ra)
                return false;
            EffectListService.TryCancelFirstEffectOfTypeOnTarget(bot, eEffect.MasteryOfConcentration);
            ra.SendCasterSpellEffectAndCastMessage(bot, 7007, true);
            ra.DisableSkill(bot);
            ECSGameEffectFactory.Create(new(bot, AtlasOF_MasteryofConcentration.Duration, 1),
                static (in ECSGameEffectInitParams i) => new MasteryOfConcentrationECSEffect(i));
            return true;
        }

        private static bool BoF(GameBot bot, Ability ability)
        {
            if (ability is not BarrierOfFortitudeAbility bof)
                return false;
            if (bot.Group == null)
                Shield(bot, bot, bof);
            else
            {
                foreach (GameLiving member in bot.Group.GetMembersInTheGroup())
                {
                    // As the player handler: every living member, wherever they are (its range check compares the
                    // caster with itself, which is how the ability behaves for players; owner 2026-11-04: match it).
                    if (member is { IsAlive: true })
                        Shield(bot, member, bof);
                }
            }
            bof.DisableSkill(bot);
            return true;
        }

        private static void Shield(GameBot caster, GameLiving member, BarrierOfFortitudeAbility bof)
        {
            bool success = !member.TempProperties.GetProperty<bool>(BarrierOfFortitudeAbility.BofBaSb);
            bof.SendCasterSpellEffect(member, 7009, success);
            bof.SendCasterSpellEffect(member, 1486, success);
            bof.SendCasterSpellEffect(member, 10535, success);
            if (success && bof.CreateSpell(caster) is SpellHandler spell)
                ECSGameEffectFactory.Create(new(member, BunkerOfFaithDurationMs, 1, spell),
                    static (in ECSGameEffectInitParams i) => new BunkerOfFaithECSEffect(i));
        }
    }
}