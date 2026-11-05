using System;
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
            if (CheckSpells(eCheckSpellType.Offensive))
                return true;
            // Nothing could start: out of spell range, walk in as the legacy offense does (ApproachForOffensiveSpell
            // also keeps /petpull's stay-put). Without this a caster whose spec bans melee would stand still for good.
            return ApproachForOffensiveSpell(target);
        }

        /// <summary>
        /// SmartAI heal thresholds (owner 2026-11-04): the legacy triage chose the target and a direct heal; a bot whose
        /// spec gives section 12 thresholds casts the heal those name for the target's health instead, when it is ready
        /// (CheckHealSpell: known, power, not disabled). Cures, heals over time and resurrections are not touched, and
        /// neither is a target outside every band. Any SmartAI error keeps the legacy choice.
        /// </summary>
        internal Spell SmartAiHealChoice(GameLiving target, Spell chosen)
        {
            if (!DOL.GS.SmartAI.SmartAiConfig.IsEnabled || target == null || chosen == null || BotBody == null ||
                chosen != BotBody.HealEfficient && chosen != BotBody.HealBig && chosen != BotBody.HealInstant &&
                chosen != BotBody.HealGroup && chosen != BotBody.HealInstantGroup)
                return chosen;
            try
            {
                if (DOL.GS.SmartAI.SmartAiSpecRegistry.Active.Resolve(BotBody) is not { } spec)
                    return chosen;
                DOL.GS.SmartAI.SmartSpecProfile profile = DOL.GS.SmartAI.SmartSpecProfile.For(spec);
                if (profile.HealBands.Count == 0 && profile.GroupHeal == null)
                    return chosen;
                string kind = DOL.GS.SmartAI.SmartHealThresholds.Choose(profile.HealBands, profile.GroupHeal, target.HealthPercent,
                    MembersBelow, heal => CheckHealSpell(HealOfKind(heal)));
                return kind == null ? chosen : HealOfKind(kind);
            }
            catch (Exception e)
            {
                DOL.GS.SmartAI.SmartAiFallback.ReportFailure("heal threshold choice", e);
                return chosen;
            }
        }

        private Spell HealOfKind(string kind) => kind switch
        {
            "HealEfficient" => BotBody.HealEfficient,
            "HealBig" => BotBody.HealBig,
            "HealInstant" => BotBody.HealInstant,
            "HealGroup" => BotBody.HealGroup,
            "HealInstantGroup" => BotBody.HealInstantGroup,
            "HealOverTime" => BotBody.HealOverTime,
            _ => null,
        };

        /// <summary>Group members (and the bot) alive, in support reach, under this health.</summary>
        private int MembersBelow(int percent)
        {
            if (Body.Group == null)
                return Body.HealthPercent < percent ? 1 : 0;
            int n = 0;
            foreach (GameLiving m in Body.Group.GetMembersInTheGroup())
                if (m is { IsAlive: true } && InSupportReach(m) && m.HealthPercent < percent)
                    n++;
            return n;
        }

        /// <summary>The legacy Guard / Protect / Intercept upkeep (once a second), for a SmartAI bot whatever action it took
        /// this think; the legacy brain runs it only inside its full offense.</summary>
        internal void SmartAiMaintainPeel() => MaintainTankPeel();

        /// <summary>
        /// Interrupts: an enemy in this bot's fight (its aggro list) that is casting harm, in range and fightable (not held
        /// by crowd control, allowed by CanAggroTarget), nearest first; null when the bot has no instant harmful spell or
        /// nobody is casting. The instant offensive spells come first in CheckSpells, so they land the interrupt.
        /// </summary>
        internal GameLiving SmartAiCastingEnemy(int range)
        {
            if (!Body.CanCastInstantHarmfulSpells || Body.IsCasting)
                return null;
            GameLiving best = null;
            int bestDistance = int.MaxValue;
            foreach (var entry in AggroList)
            {
                GameLiving enemy = entry.Key;
                if (enemy is not { IsAlive: true, IsCasting: true } ||
                    enemy.castingComponent?.SpellHandler?.Spell is not { IsHarmful: true } ||
                    IsBreakablyControlled(enemy) || enemy.CurrentRegionID != Body.CurrentRegionID)
                    continue;
                int distance = (int)Body.GetDistanceTo(enemy);
                if (distance > range || distance >= bestDistance || !CanAggroTarget(enemy))
                    continue;
                best = enemy;
                bestDistance = distance;
            }
            return best;
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
