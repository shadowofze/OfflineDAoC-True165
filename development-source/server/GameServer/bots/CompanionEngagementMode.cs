using System.Runtime.CompilerServices;
using DOL.AI.Brain;

namespace DOL.GS
{
    public static class CompanionEngagementMode
    {
        public const int DefensiveRadius = 350;
        /// <summary>Pet pulling: monsters on the player's pet are left to it (and its damage shield) above this health.</summary>
        public const int PetPullPackHoldAbovePercent = 85;
        /// <summary>Pet pulling: the pet is healed once a monster on it is at or below this health (has lost ~5%).</summary>
        public const int PetPullHealPetAtOrBelowPercent = 95;

        private sealed class Mode { public bool Defensive; public bool PetPulling; public GameLiving Pull; }
        private static readonly ConditionalWeakTable<GamePlayer, Mode> Modes = new();
        public static void Set(GamePlayer player, bool defensive)
        {
            Mode mode = Modes.GetOrCreateValue(player);
            mode.Defensive = defensive;
            mode.Pull = null;
            CompanionPvpEngagement.Reset(player);
            // The new client's group-window stance button shows it (op 30 of the DebugMode 'H' packet: 1 defensive,
            // 0 aggressive; owner 2026-10-30). The original client keeps /aggressive and /defensive.
            Hire165.Send(player, 30, [(byte)(defensive ? 1 : 0)]);
        }

        /// <summary>
        /// /petpull (owner 2026-11-04, SmartAI pet pulling, class templates section 6b): the player's pet gathers the
        /// monsters and holds them while its damage shield works. The player's companions then:
        /// - leave every monster on the pet alone above 85% health; at 85% or below casters and healers join (the AoE
        ///   window), while melee, tanks and companion pets never take the pack off the pet;
        /// - fight a monster that breaks off the pet (toward the group) as normal;
        /// - heal the pet only once a monster on it has lost about 5% (95% or below), so the pet keeps the aggro.
        /// Works with /defensive or /aggressive, and for every companion (SmartAI or not). The player's own pet is never limited.
        /// </summary>
        public static void SetPetPull(GamePlayer player, bool on) => Modes.GetOrCreateValue(player).PetPulling = on;

        public static bool IsPetPulling(GamePlayer player) => player != null && Modes.TryGetValue(player, out Mode mode) && mode.PetPulling;

        /// <summary>The player whose companion (or companion's pet) this is; null for anything else.</summary>
        private static GamePlayer CompanionLeader(GameLiving actor)
        {
            // Include companion pets, never human pets or autonomous bots.
            for (int depth = 0; depth < 8 && actor != null; depth++)
            {
                if (actor is GameBot bot)
                {
                    GamePlayer leader = bot.PlayerGroupLeader ?? bot.Owner;
                    return bot.IsPlayerCompanion && leader != null && bot.Group != null && bot.Group == leader.Group ? leader : null;
                }
                actor = (actor as GameNPC)?.Brain is IControlledBrain controlled ? controlled.Owner : null;
            }
            return null;
        }

        /// <summary>The companion's player when /defensive or /petpull limits what it may fight (its aggro and current
        /// target are then pruned with Allows each think); null otherwise.</summary>
        public static GamePlayer RestrictingLeader(GameLiving actor)
        {
            GamePlayer leader = CompanionLeader(actor);
            return leader != null && Modes.TryGetValue(leader, out Mode mode) && (mode.Defensive || mode.PetPulling) ? leader : null;
        }

        public static GamePlayer DefensiveLeader(GameLiving actor)
        {
            GamePlayer leader = CompanionLeader(actor);
            return leader != null && Modes.TryGetValue(leader, out Mode mode) && mode.Defensive ? leader : null;
        }

        public static bool Allows(GameLiving actor, GameLiving target)
        {
            GamePlayer leader = CompanionLeader(actor);
            if (leader == null || !Modes.TryGetValue(leader, out Mode mode))
                return true;
            if (mode.PetPulling && target != null && !PetPullAllows(actor, leader, target))
                return false;
            return !mode.Defensive || target != null && target.CurrentRegionID == leader.CurrentRegionID &&
                (leader.IsWithinRadius(target, DefensiveRadius) || CompanionPvpEngagement.Defending(actor, target) ||
                 CompanionPvpEngagement.FrontierRoamerInRange(leader, target, true));
        }

        /// <summary>The pet-pull rule for one actor and monster (see SetPetPull).</summary>
        private static bool PetPullAllows(GameLiving actor, GamePlayer leader, GameLiving target) =>
            PetPullAllows(target.TargetObject is GameLiving victim && PlayerLedPullCoordinator.PlayerPetOwner(victim) == leader,
                target.HealthPercent, actor is GameBot bot && IsRangedSupport(bot));

        /// <summary>Pure: a monster on the pet is left alone above 85%, and then only casters and healers join.</summary>
        public static bool PetPullAllows(bool onPlayersPet, int monsterHealthPercent, bool rangedCasterOrHealer) =>
            !onPlayersPet || monsterHealthPercent <= PetPullPackHoldAbovePercent && rangedCasterOrHealer;

        /// <summary>A caster or healer that fights from range (not melee, a tank, or a pet).</summary>
        public static bool IsRangedSupport(GameBot bot)
        {
            if (bot.CharacterClass == null)
                return false;
            BotPartyRole role = BotPartyRoles.For((eCharacterClass)bot.CharacterClass.ID);
            return role == BotPartyRole.Support || role != BotPartyRole.Tank && BotSpellPower.IsOffensiveCaster(bot);
        }

        /// <summary>True when a companion should not heal this pet yet: pet pulling, the player's pet, and monsters on
        /// it but none has lost ~5% (so the pet keeps the aggro).</summary>
        public static bool PetPullHoldsPetHeal(GamePlayer leader, GameNPC pet)
        {
            if (!IsPetPulling(leader) || pet == null || PlayerLedPullCoordinator.PlayerPetOwner(pet) != leader)
                return false;
            var attackers = pet.attackComponent?.AttackerTracker?.Attackers;
            if (attackers == null)
                return false;
            bool any = false;
            int lowest = 100;
            foreach (GameLiving a in attackers)
            {
                if (a is not { IsAlive: true } || a.TargetObject != pet)
                    continue;
                any = true;
                if (a.HealthPercent < lowest)
                    lowest = a.HealthPercent;
            }
            return HoldPetHeal(any, lowest);
        }

        /// <summary>Pet pulling: below this health the pet is going down fast and may get a big heal (an improvised figure;
        /// the templates say "heavy multi-mob spike or rapid health drop").</summary>
        public const int PetPullBigHealBelowPercent = 50;

        /// <summary>The companion's player when /petpull is on, or null.</summary>
        public static GamePlayer PetPullLeader(GameLiving actor)
        {
            GamePlayer leader = CompanionLeader(actor);
            return IsPetPulling(leader) ? leader : null;
        }

        /// <summary>A monster the player's pet is holding (its target is the player's pet).</summary>
        public static bool IsOnPetOf(GamePlayer leader, GameLiving monster) =>
            leader != null && monster?.TargetObject is GameLiving victim && PlayerLedPullCoordinator.PlayerPetOwner(victim) == leader;

        /// <summary>Pet pulling, stay put: the companion does not walk toward a monster the pet holds, nor to the pet itself
        /// (the pet brings the monsters to the group). Monsters that break off are chased as normal.</summary>
        public static bool PetPullStaysPut(GameLiving actor, GameLiving target)
        {
            GamePlayer leader = PetPullLeader(actor);
            return leader != null && target != null && (IsOnPetOf(leader, target) || PlayerLedPullCoordinator.PlayerPetOwner(target) == leader);
        }

        /// <summary>Pet pulling: a monster on the pet above 85% must not be reached by an area spell (treated as a
        /// mesmerized monster is by the blast check).</summary>
        public static bool PetPullProtects(GameLiving actor, GameLiving monster)
        {
            GamePlayer leader = PetPullLeader(actor);
            return leader != null && IsOnPetOf(leader, monster) && monster.HealthPercent > PetPullPackHoldAbovePercent;
        }

        /// <summary>Pet pulling, AoE only: a harmful spell at a monster the pet holds must be an area damage spell
        /// (no single-target nukes, DoTs, debuffs or crowd control on the pack).</summary>
        public static bool PetPullBlocksSpell(GameBot bot, Spell spell)
        {
            if (spell is not { IsHarmful: true } || bot?.TargetObject is not GameLiving target)
                return false;
            GamePlayer leader = PetPullLeader(bot);
            return leader != null && !PetPullSpellAllowed(IsOnPetOf(leader, target), spell.Radius, PlayerLedPullCoordinator.IsCrowdControl(spell));
        }

        /// <summary>Pure: on the pet's pack only area damage spells (radius, not crowd control); elsewhere anything.</summary>
        public static bool PetPullSpellAllowed(bool targetOnPet, int radius, bool crowdControl) =>
            !targetOnPet || radius > 0 && !crowdControl;

        /// <summary>Pet pulling, smallest heal: the player's pet gets the efficient heal unless it is going down fast.</summary>
        public static bool PetPullSmallHeal(GamePlayer leader, GameLiving healTarget) =>
            IsPetPulling(leader) && healTarget != null && PlayerLedPullCoordinator.PlayerPetOwner(healTarget) == leader &&
            UseSmallHeal(healTarget.HealthPercent);

        /// <summary>Pure: the small heal while the pet is at 50% or more.</summary>
        public static bool UseSmallHeal(int petHealthPercent) => petHealthPercent >= PetPullBigHealBelowPercent;

        /// <summary>Pure: hold the pet's heal while monsters are on it and none is at 95% or below.</summary>
        public static bool HoldPetHeal(bool monstersOnPet, int lowestMonsterHealthPercent) =>
            monstersOnPet && lowestMonsterHealthPercent > PetPullHealPetAtOrBelowPercent;

        public static void RememberPull(GamePlayer player, GameLiving target)
        {
            if (player != null && Modes.TryGetValue(player, out Mode mode) && mode.Defensive) mode.Pull = target;
        }
        public static GameLiving NearbyPull(GameBot bot)
        {
            GamePlayer player = DefensiveLeader(bot);
            if (player == null || !Modes.TryGetValue(player, out Mode mode)) return null;
            GameLiving target = mode.Pull;
            if (target?.IsAlive != true || target.ObjectState != GameObject.eObjectState.Active ||
                target.CurrentRegionID != player.CurrentRegionID)
            {
                mode.Pull = null;
                return null;
            }
            return Allows(bot, target) ? target : null;
        }
    }
}
