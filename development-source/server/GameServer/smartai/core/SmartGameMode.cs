using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    public enum SmartGameMode : byte { PvE, PvP, RvR }

    /// <summary>
    /// Which fight a bot is in (plan Phase 9, architecture sections 3.1 and 6):
    /// - PvE: every enemy is a monster (also in the frontier).
    /// - PvP: a player-like enemy (a player, a gamebot or companion of another realm, or a pet of one), outside a
    ///   large frontier fight.
    /// - RvR: player-like enemies in the frontier or a battleground, three or more of them.
    /// Classify is pure; Detect reads the bot's target and attackers.
    /// </summary>
    public static class SmartGameModeDetector
    {
        /// <summary>Player-like enemies that make a frontier fight RvR rather than a skirmish.</summary>
        public const int RvrEnemyThreshold = 3;

        public static SmartGameMode Classify(bool inRvrArea, int playerLikeEnemies) =>
            playerLikeEnemies <= 0 ? SmartGameMode.PvE
            : inRvrArea && playerLikeEnemies >= RvrEnemyThreshold ? SmartGameMode.RvR
            : SmartGameMode.PvP;

        public static SmartGameMode Detect(GameBot bot, GameLiving target)
        {
            int enemies = 0;
            if (IsPlayerLikeEnemy(bot, target))
                enemies++;
            var attackers = bot.attackComponent?.AttackerTracker?.Attackers;
            if (attackers != null)
            {
                foreach (GameLiving a in attackers)
                    if (a != target && IsPlayerLikeEnemy(bot, a))
                        enemies++;
            }
            if (enemies > 0 && enemies < RvrEnemyThreshold && bot.Group != null)
            {
                // Count the group's other player-like enemies too: a frontier fight is the group's, not one bot's.
                foreach (GameLiving member in bot.Group.GetMembersInTheGroup())
                    if (member != bot && member.TargetObject is GameLiving t && t != target && IsPlayerLikeEnemy(bot, t))
                        enemies++;
            }
            return Classify(AutonomousWorldBotController.IsBattleground(bot), enemies);
        }

        /// <summary>A player, gamebot or companion of another realm, or a pet one of them controls.</summary>
        public static bool IsPlayerLikeEnemy(GameLiving self, GameLiving other)
        {
            if (other is not { IsAlive: true } || other.Realm == self.Realm)
                return false;
            GameLiving owner = other is GameNPC { Brain: IControlledBrain controlled } ? controlled.GetLivingOwner() ?? other : other;
            return PlayerRules165.IsPlayerLike(owner) || owner is GamePlayer;
        }
    }

    /// <summary>
    /// Enemy target order in PvP and RvR (architecture section 6, RvR target prioritization): healers and crowd
    /// controllers first, then casters, other melee, and armoured tanks last. By 1.65 class.
    /// </summary>
    public static class SmartEnemyPriority
    {
        public const int Healer = 100, CrowdControl = 100, Caster = 70, Melee = 40, Tank = 20, Monster = 0;

        public static int ForClass(int classId) => (eCharacterClass)classId switch
        {
            eCharacterClass.Cleric or eCharacterClass.Friar or eCharacterClass.Healer or eCharacterClass.Shaman or
                eCharacterClass.Druid or eCharacterClass.Bard => Healer,
            eCharacterClass.Sorcerer or eCharacterClass.Minstrel or eCharacterClass.Mentalist => CrowdControl,
            eCharacterClass.Wizard or eCharacterClass.Theurgist or eCharacterClass.Cabalist or eCharacterClass.Necromancer or
                eCharacterClass.Runemaster or eCharacterClass.Spiritmaster or eCharacterClass.Bonedancer or
                eCharacterClass.Eldritch or eCharacterClass.Enchanter or eCharacterClass.Animist => Caster,
            eCharacterClass.Armsman or eCharacterClass.Paladin or eCharacterClass.Warrior or eCharacterClass.Hero => Tank,
            _ => Melee,
        };

        /// <summary>A living's priority: its class for a player or bot, Monster for anything else.</summary>
        public static int Of(GameLiving living) => living switch
        {
            GamePlayer p when p.CharacterClass != null => ForClass(p.CharacterClass.ID),
            GameBot b when b.CharacterClass != null => ForClass(b.CharacterClass.ID),
            _ => Monster,
        };
    }
}
