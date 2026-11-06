using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// Stage 1: what a bot sees at the start of a SmartAI combat think (architecture section 3.1). A plain value, built
    /// once per think from live state and then only read, so the planning stages are pure and testable.
    /// Game mode (Phase 9) and the group blackboard (Phase 7) join it later.
    /// </summary>
    public readonly struct SmartBotPerception
    {
        /// <summary>Below this a caster counts as out of mana.</summary>
        public const int OutOfManaPercent = 10;

        public int SelfHealthPercent { get; init; }
        public int SelfManaPercent { get; init; }
        public int SelfEndurancePercent { get; init; }
        /// <summary>The lowest health of the bot and its group members in support reach (100 = nobody hurt).</summary>
        public int GroupMinHealthPercent { get; init; }
        public int MembersBelow65 { get; init; }
        public int MembersBelow40 { get; init; }
        /// <summary>Other group members in reach who are healing (IsActiveHealer). 0: this bot is the group's healer.</summary>
        public int OtherHealers { get; init; }
        public bool InCombat { get; init; }
        public bool Silenced { get; init; }
        /// <summary>Stunned or mesmerized: the bot can't act.</summary>
        public bool Incapacitated { get; init; }
        public bool HasTarget { get; init; }
        public int TargetHealthPercent { get; init; }
        /// <summary>The target is mesmerized or breakably rooted.</summary>
        public bool TargetControlled { get; init; }
        /// <summary>The group's strategy from its snapshot (Phase 8).</summary>
        public SmartGroupStrategy GroupStrategy { get; init; }
        /// <summary>PvE, PvP or RvR (Phase 9).</summary>
        public SmartGameMode Mode { get; init; }
        /// <summary>The bot's personality (Phase 10); null is neutral.</summary>
        public SmartPersonalityProfile Personality { get; init; }
        /// <summary>The offensive mana floor when the personality replaces the spec's (0 = the spec's).</summary>
        public int ManaFloorOverride { get; init; }
        /// <summary>The target is the group's primary target.</summary>
        public bool TargetIsGroupTarget { get; init; }
        /// <summary>What the bot chose in its last think, if within the commitment window (Phase 11).</summary>
        public SmartActionFamily? StickyFamily { get; init; }
        /// <summary>Enemies in melee reach swinging at this bot.</summary>
        public int MeleeAttackersOnSelf { get; init; }
        /// <summary>Something other than the bot itself is interrupting it (hits, attacks on it).</summary>
        public bool BeingInterrupted { get; init; }
        /// <summary>The bot has power to cast (a caster class).</summary>
        public bool IsCaster { get; init; }

        public static SmartBotPerception Build(BotBrain brain, GameBot bot, GameLiving target)
        {
            int min = bot.HealthPercent, below65 = 0, below40 = 0, healers = 0;
            Count(bot, ref min, ref below65, ref below40);
            if (bot.Group != null)
            {
                foreach (GameLiving member in bot.Group.GetMembersInTheGroup())
                {
                    if (member == bot || member is not { IsAlive: true } || !brain.SmartAiInSupportReach(member))
                        continue;
                    Count(member, ref min, ref below65, ref below40);
                    if (IsActiveHealer(member))
                        healers++;
                }
            }

            bool hasTarget = target is { IsAlive: true };
            return new SmartBotPerception
            {
                SelfHealthPercent = bot.HealthPercent,
                SelfManaPercent = bot.MaxMana > 0 ? bot.ManaPercent : 100,
                SelfEndurancePercent = bot.EndurancePercent,
                GroupMinHealthPercent = min,
                MembersBelow65 = below65,
                MembersBelow40 = below40,
                OtherHealers = healers,
                InCombat = bot.InCombat,
                Silenced = bot.IsSilenced,
                Incapacitated = bot.IsStunned || bot.IsMezzed,
                HasTarget = hasTarget,
                TargetHealthPercent = hasTarget ? target.HealthPercent : 0,
                // Nobody breaks a groupmate's mez or root; a bot alone breaks its own on purpose (SmartKite).
                TargetControlled = hasTarget && BotBrain.IsBreakablyControlled(target) && !SmartKite.SoloMayBreakHold(bot),
                MeleeAttackersOnSelf = brain.SmartAiMeleeAttackersOnMe(),
                BeingInterrupted = bot.IsBeingInterruptedByOther,
                IsCaster = bot.MaxMana > 0,
            };
        }

        /// <summary>The 1.65 classes whose job includes healing the group.</summary>
        public static bool IsHealingClass(int classId) => (eCharacterClass)classId is
            eCharacterClass.Cleric or eCharacterClass.Friar or eCharacterClass.Healer or eCharacterClass.Shaman or
            eCharacterClass.Druid or eCharacterClass.Bard;

        /// <summary>
        /// A group member who is healing: a healing class, able to act (not stunned or mezzed, power left), and - for a
        /// SmartAI bot - a healer spec (a Smite or Enhancement Cleric is not the group's healer). A player or a bot without
        /// a SmartAI spec counts by class.
        /// </summary>
        public static bool IsActiveHealer(GameLiving member)
        {
            int classId = member switch
            {
                GamePlayer p => p.CharacterClass?.ID ?? 0,
                GameBot b => b.CharacterClass?.ID ?? 0,
                _ => 0,
            };
            if (!IsHealingClass(classId) || member.IsStunned || member.IsMezzed ||
                member.MaxMana > 0 && member.ManaPercent < OutOfManaPercent)
                return false;
            if (member is GameBot bot && SmartAiConfig.IsEnabled && SmartAiSpecRegistry.Active.Resolve(bot) is { } spec)
                return spec.Role.Contains("Healer", System.StringComparison.OrdinalIgnoreCase);
            return true;
        }

        private static void Count(GameLiving living, ref int min, ref int below65, ref int below40)
        {
            int hp = living.HealthPercent;
            if (hp < min)
                min = hp;
            if (hp < 65)
                below65++;
            if (hp < 40)
                below40++;
        }
    }
}
