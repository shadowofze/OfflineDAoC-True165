using DOL.GS;
using DOL.GS.SkillHandler;

namespace DOL.AI.Brain
{
    /// <summary>
    /// Engage against archers (owner 2026-11-04): a bot with Engage (Shields 7) that an archer is shooting faces the
    /// archer and engages it, the way a player /faces and engages. Player rules, unchanged: the Engage effect and the
    /// block code are the player's (95% block against the engaged archer, 5 endurance a block, no engage on a target
    /// attacked in the last 5 seconds, no swinging while engaged), and the bot needs what a player needs - a shield in
    /// the left hand, a one-handed (or empty) main hand, not the bow out, standing, endurance for a block.
    /// The bot then walks to the archer when it may (its engagement mode allows the archer, /petpull's stay-put does
    /// not hold it) and drops the engage at melee range, which starts its attack, as the player's effect does.
    /// An engage is dropped as soon as a melee enemy is on the bot, the archer stops shooting it, dies or leaves.
    /// Used by every bot (gamebots and hired companions, SmartAI on or off) that has the ability.
    /// </summary>
    public partial class BotBrain
    {
        /// <summary>How far an archer may be for the bot to engage it (a long bow shot).</summary>
        public const int EngageArcherRange = 2_300;

        /// <summary>
        /// Engage upkeep and start, before the combat think. True: the bot is engaging this think (the rest of the combat
        /// think is skipped, so nothing swings and cancels the engage); false: the normal combat think runs.
        /// </summary>
        internal bool TryEngageArcher()
        {
            if (BotBody == null || !Body.HasAbility(Abilities.Engage))
                return false;

            if (Body.IsEngaging)
                return KeepEngaging();

            if (!CanEngageNow() || ActiveOrderedPullTarget != null || IsHealer || HoldsExclusiveSupportRole() ||
                Body.IsCasting || MeleeEnemyOnMe() != null)
                return false;
            // Already trading blows with its target: keep fighting.
            if (Body.TargetObject is GameLiving current && current.IsAlive && Body.IsWithinRadius(current, Body.MeleeAttackRange))
                return false;

            GameLiving archer = ArcherShootingMe();
            if (archer == null ||
                archer.LastAttackedByEnemyTick > GameLoop.GameLoopTime - EngageAbilityHandler.ENGAGE_ATTACK_DELAY_TICK)
                return false;

            Body.StopAttack();
            Body.TargetObject = archer;
            if (!Body.IsMoving)
                Body.TurnTo(archer);
            var engage = new EngageECSGameEffect(new ECSGameEffectInitParams(Body, 0, 1, null));
            if (!engage.Start() || !Body.IsEngaging)
                return false;
            DOL.GS.SmartAI.SmartAiBrainDriver.RecordAbility(BotBody, "Engage", $"engaging archer {archer.Name}");
            CloseOnArcher(archer);
            return true;
        }

        private bool KeepEngaging()
        {
            EngageECSGameEffect engage = EffectListService.GetEffectOnTarget(Body, eEffect.Engage) as EngageECSGameEffect;
            GameLiving archer = engage?.EngageTarget;
            bool keep = archer != null && CanEngageNow() && IsShootingMe(archer) && MeleeEnemyOnMe() == null &&
                        !Body.IsWithinRadius(archer, Body.MeleeAttackRange);
            if (!keep)
            {
                // Dropping it starts the attack on the bot's target (the player effect's own end), so at melee range the
                // bot swings at the archer straight away.
                if (engage != null)
                    engage.Cancel(false, true);
                else
                    Body.CancelEngageEffect();
                return false;
            }

            Body.TargetObject = archer;
            if (!Body.IsMoving)
                Body.TurnTo(archer);
            CloseOnArcher(archer);
            return true;
        }

        /// <summary>The player's own requirements to hold an engage.</summary>
        private bool CanEngageNow() =>
            Body.IsAlive && !Body.IsSitting && !Body.IsIncapacitated &&
            Body.ActiveWeaponSlot != eActiveWeaponSlot.Distance &&
            Body.ActiveLeftWeapon?.Object_Type == (int)eObjectType.Shield &&
            (Body.ActiveWeapon == null || Body.ActiveWeapon.Item_Type is Slot.RIGHTHAND or Slot.LEFTHAND) &&
            Body.Endurance >= EngageAbilityHandler.ENGAGE_ENDURANCE_COST;

        /// <summary>An enemy with its bow out, attacking this bot, in range and attackable (nearest first).</summary>
        private GameLiving ArcherShootingMe()
        {
            GameLiving best = null;
            int bestDistance = int.MaxValue;
            foreach (var entry in AggroList)
            {
                GameLiving enemy = entry.Key;
                if (!IsShootingMe(enemy) || !GameServer.ServerRules.IsAllowedToAttack(Body, enemy, true))
                    continue;
                int distance = (int)Body.GetDistanceTo(enemy);
                if (distance < bestDistance)
                {
                    best = enemy;
                    bestDistance = distance;
                }
            }
            return best;
        }

        private bool IsShootingMe(GameLiving enemy) =>
            enemy is { IsAlive: true } && enemy.CurrentRegionID == Body.CurrentRegionID &&
            enemy.ActiveWeaponSlot == eActiveWeaponSlot.Distance && enemy.attackComponent?.AttackState == true &&
            enemy.TargetObject == Body && Body.IsWithinRadius(enemy, EngageArcherRange);

        /// <summary>An enemy in melee reach, swinging at this bot (not with a bow).</summary>
        private GameLiving MeleeEnemyOnMe()
        {
            foreach (var entry in AggroList)
            {
                GameLiving enemy = entry.Key;
                if (enemy is { IsAlive: true } && enemy.TargetObject == Body &&
                    enemy.ActiveWeaponSlot != eActiveWeaponSlot.Distance && enemy.attackComponent?.AttackState == true &&
                    enemy.CurrentRegionID == Body.CurrentRegionID && Body.IsWithinRadius(enemy, Body.MeleeAttackRange + 50))
                    return enemy;
            }
            return null;
        }

        /// <summary>Walk to the archer, shield up, when the bot may fight it and is not told to stay put.</summary>
        private void CloseOnArcher(GameLiving archer)
        {
            if (Body.IsCrowdControlled || !CanAggroTarget(archer) || CompanionEngagementMode.PetPullStaysPut(Body, archer))
                return;
            BotBody.WakeRecoveryRest();
            Body.Follow(archer, (short)System.Math.Max(50, Body.MeleeAttackRange - 50), (short)EngageArcherRange);
        }
    }
}