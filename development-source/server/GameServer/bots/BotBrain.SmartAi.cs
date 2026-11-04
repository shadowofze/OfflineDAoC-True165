using DOL.GS;

namespace DOL.AI.Brain
{
    /// <summary>
    /// SmartAI's building blocks (plan Phase 5): the legacy bot actions, unchanged, so a SmartAI bot heals, buffs and
    /// fights through exactly the same code as every other bot (player rules: range, LOS, costs, mez respect, the
    /// owner's orders). SmartAI only decides which of them to run, and in what order. Used only while SmartAI is on.
    /// </summary>
    public partial class BotBrain
    {
        /// <summary>
        /// Why the legacy brain must keep this turn (an order or a hold it is carrying out), or null. A cast in progress
        /// and the support role are not reasons: SmartAI's actions see a cast through themselves (they return "acted"
        /// while casting), and a support bot gets SmartAI's choice plus the legacy support upkeep (SmartAiSupportUpkeep).
        /// Owner test 2026-11-04: the support role made every Cleric companion in a player's group skip SmartAI.
        /// </summary>
        internal string SmartAiLegacyOwnsTurnReason =>
            ActiveOrderedPullTarget != null ? "an ordered pull" :
            _returnToFormationAfterPull ? "returning to formation after a pull" :
            AutonomousDefensivePull.Hold(BotBody) ? "a defensive pull hold" :
            SoloRangedPull165.Hold(BotBody) ? "a solo ranged pull hold" : null;

        /// <summary>The bot holds its group's support role (healer/buffer beside combat partners): no melee, formation.</summary>
        internal bool SmartAiExclusiveSupport => HoldsExclusiveSupportRole();

        /// <summary>
        /// The legacy support turn's upkeep after SmartAI acted for a support bot: the Druid's pet order, no melee and
        /// no pet fight (unless SmartAI chose the full offense), and in a player's group the formation follow.
        /// HoldSupportCombat's cancel of a harmful cast is left out: a SmartAI support spec may smite.
        /// </summary>
        internal void SmartAiSupportUpkeep(bool fighting)
        {
            if (CompanionFollowPolicy.SendsDruidPet(BotBody))
                TryCommandCompanionDruidPet(CalculateNextAttackTarget());
            if (!fighting)
            {
                Body.StopAttack();
                if (!UsesDefensiveOnlyPet)
                    Body.ControlledBrain?.Disengage();
            }
            if (BotBody.IsPlayerLedGroup)
            {
                if (!Body.IsCasting && Body.castingComponent?.HasPendingSkillRequests != true)
                    FollowFormation();
                else
                    FollowDuringMobileSong();
            }
        }

        /// <summary>The whole legacy support turn (when the spec offers nothing to do). allowHeals false keeps its triage
        /// out (a spec whose heal conditions are not met).</summary>
        internal void SmartAiLegacySupport(bool allowHeals)
        {
            if (!allowHeals)
                AlreadyCheckedHeals = true;
            PerformGroupSupport();
        }

        /// <summary>A group member heals and buffs may be aimed at (same reach as the legacy triage).</summary>
        internal bool SmartAiInSupportReach(GameLiving member) => InSupportReach(member);

        /// <summary>The legacy attack target choice (passes over mezzed and breakably rooted monsters while others fight).</summary>
        internal GameLiving SmartAiNextAttackTarget() => CalculateNextAttackTarget();

        /// <summary>Heals, cures and resurrections: the legacy triage.</summary>
        internal bool SmartAiTriage() => CheckHeals();

        /// <summary>The legacy defensive and buff spells, without the triage that CheckSpells runs first.</summary>
        internal bool SmartAiSupport()
        {
            if (Body.IsCasting || Body.castingComponent?.HasPendingSkillRequests == true)
                return true;
            // A support bot keeps the legacy support duties in their legacy order after triage: crowd control, add
            // control, Celerity, then defensive spells.
            if (HoldsExclusiveSupportRole() && (TryPvpCrowdControl() || TryPveAddControl() || TryGroupCelerity()))
                return true;
            return Body.CanCastMiscSpells && CheckDefensiveSpells(Body.MiscSpells);
        }

        /// <summary>The legacy offensive spells on a target, without melee. allowHeals false keeps the triage that
        /// CheckSpells runs first out of this think (a spec whose heal conditions are not met).</summary>
        internal bool SmartAiOffensiveSpells(GameLiving target, bool allowHeals)
        {
            if (target is not { IsAlive: true })
                return false;
            // A cast in progress is seen through; its target is not changed under it.
            if (Body.IsCasting || Body.castingComponent?.HasPendingSkillRequests == true)
                return true;
            if (!allowHeals)
                AlreadyCheckedHeals = true;
            Body.TargetObject = target;
            if (Body.attackComponent?.AttackState == true)
                Body.StopAttack();
            return CheckSpells(eCheckSpellType.Offensive);
        }

        /// <summary>The whole legacy offense (target, pets, taunts, spells, bow and melee).</summary>
        internal void SmartAiFullOffense(bool allowHeals)
        {
            if (!allowHeals)
                AlreadyCheckedHeals = true;
            AttackMostWanted();
        }
    }
}
