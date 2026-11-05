using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// The combat seam between BotBrain and SmartAI (plan Phase 3, architecture sections 2.1 and 2.2).
    /// BotBrain's AGGRO state calls TryThinkCombat once per think, after its own guards (leaving combat, passive stance).
    /// - Returns true: SmartAI acted this think, and the legacy combat code is skipped.
    /// - Returns false: the legacy combat code runs, exactly as without SmartAI.
    /// It returns false when SmartAI is off, the bot's breaker is open, the bot has no SmartAI spec, or SmartAI failed.
    /// The same seam serves hired companions and autonomous gamebots; the decision engine applies the player rules to
    /// both (PlayerRules165), with no bot-only shortcuts.
    /// </summary>
    public static class SmartAiBrainDriver
    {
        private static readonly Logging.Logger Log = Logging.LoggerManager.Create(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Per-bot breakers. Weak keys: a bot that leaves the world takes its breaker with it.</summary>
        private static readonly ConditionalWeakTable<GameBot, SmartAiCircuitBreaker> Breakers = new();

        /// <summary>At most one error line per interval for the whole server, so a broken rule can't flood the log.</summary>
        private const long ErrorLogIntervalMs = 10_000;
        private static long _nextErrorLogAt;

        public static bool TryThinkCombat(BotBrain brain)
        {
            if (!SmartAiConfig.IsEnabled)
                return false;

            GameBot bot = brain?.BotBody;
            if (bot == null)
                return false;

            long now = GameLoop.GameLoopTime;
            if (Breakers.TryGetValue(bot, out SmartAiCircuitBreaker open) && open.IsOpen(now))
                return false;

            long started = System.Diagnostics.Stopwatch.GetTimestamp();
            try
            {
                bool handled = ThinkCombatTurn(brain, bot);
                SmartAiDiagnostics.RecordThink(System.Diagnostics.Stopwatch.GetTimestamp() - started);
                return handled;
            }
            catch (Exception e)
            {
                SmartAiCircuitBreaker breaker = Breakers.GetValue(bot, static _ => new SmartAiCircuitBreaker());
                breaker.Trip(now);
                LogFailure(bot, breaker, e, now);
                SmartAiFallback.ReportFailure($"{bot.Name} ({bot.GetType().Name}) combat think", e);
                return false;
            }
        }

        /// <summary>The breaker of a bot, if it ever failed (diagnostics, /smartai debug).</summary>
        public static bool TryGetBreaker(GameBot bot, out SmartAiCircuitBreaker breaker)
        {
            breaker = null;
            return bot != null && Breakers.TryGetValue(bot, out breaker);
        }

        /// <summary>One SmartAI combat think: the bot's spec from its class file (Phase 4), then the decision engine
        /// (Phase 5). A bot whose class has no file, or whose build matches no spec, keeps the legacy combat.</summary>
        private static bool ThinkCombatTurn(BotBrain brain, GameBot bot)
        {
            SmartSpecDefinition spec = SmartAiSpecRegistry.Active.Resolve(bot, out string how);
            if (spec == null)
            {
                // Only for a class that has a file: why this bot has no spec (untrained, a tie) is worth seeing.
                if (bot.CharacterClass != null && SmartAiSpecRegistry.Active.Classes.ContainsKey(bot.CharacterClass.ID))
                    RecordLegacy(bot, "(no spec)", $"no spec: {how}");
                SmartGroupCombatLog.Think(bot, new SmartDecision("(no spec)", "Legacy", 0, $"no spec: {how}", GameLoop.GameLoopTime));
                return false;
            }
            SmartSpecProfile profile = SmartSpecProfile.For(spec);
            long now = GameLoop.GameLoopTime;
            SmartGroupTacticalState groupState = bot.Group != null ? SmartGroupTacticalState.For(bot.Group) : null;
            groupState?.Touch(now);
            SmartGroupTacticalSnapshot snapshot = groupState?.Current ?? SmartGroupTacticalSnapshot.Empty;
            bool fresh = snapshot.IsFresh(now);

            SmartPersonality personality = SmartPersonalityRegistry.Resolve(bot, spec);
            SmartPersonalityProfile character = SmartPersonalityProfile.For(personality);
            GameLiving groupTarget = fresh ? GroupTarget(brain, bot, profile, character, snapshot) : null;
            // Interrupts first: an enemy of this fight casting harm, for a bot with an instant harmful spell.
            GameLiving target = brain.SmartAiCastingEnemy(SmartAbilityPolicy.InterruptRange) ?? groupTarget ?? brain.SmartAiNextAttackTarget();
            SmartBotPerception perception = SmartBotPerception.Build(brain, bot, target) with
            {
                GroupStrategy = fresh ? snapshot.Strategy : SmartGroupStrategy.StandardAssistTrain,
                Mode = SmartGameModeDetector.Detect(bot, target),
                Personality = character,
                // The personality's mana reserve applies only when it isn't the spec's own default (whose floor the
                // template already gives).
                ManaFloorOverride = personality != SmartPersonality.Auto && personality != SmartPersonalityRegistry.SpecDefault(spec)
                    ? character.ManaReserve : 0,
                TargetIsGroupTarget = target != null && target == snapshot.PrimaryTarget,
                // Phase 11: what this bot chose in its last think, within the commitment window (anti-flip-flop).
                StickyFamily = SmartAiDiagnostics.TryGetRing(bot, out SmartAiDiagnostics.DecisionRing ring) &&
                    ring.Last is { Family: { } lastFamily } last && now - last.GameLoopTime <= SmartUtilityScorer.StickinessWindowMs
                        ? lastFamily : null,
            };
            if (groupState != null)
                SmartObservationProducer.Observe(bot, profile, perception, target, groupState, snapshot, now);

            // Realm abilities first (Phase 6): instant, and Purge is for the moment the bot is mezzed or stunned.
            if (SmartRealmAbilities.TryUse(bot, profile, perception, out SmartAbilityRule used))
            {
                var ability = new SmartDecision(spec.Name, used.Key.Replace("AtlasOF_", ""), used.Priority, used.Source, GameLoop.GameLoopTime);
                SmartAiDiagnostics.Record(bot, ability);
                SmartGroupCombatLog.Think(bot, ability, perception, target, default);
                return true;
            }

            // An order or a pull hold stays with the legacy brain.
            string legacyReason = brain.SmartAiLegacyOwnsTurnReason ?? (perception.Incapacitated ? "stunned or mezzed" : null);
            if (legacyReason != null)
            {
                RecordLegacy(bot, spec.Name, legacyReason);
                SmartGroupCombatLog.Think(bot, new SmartDecision(spec.Name, "Legacy", 0, legacyReason, now), perception, target, default);
                return false;
            }

            Span<SmartActionCandidate> buffer = stackalloc SmartActionCandidate[SmartCandidateGenerator.FamilyCount];
            int count = SmartCandidateGenerator.Generate(profile, perception, buffer);
            bool handled = SmartActionExecutor.Execute(brain, target, profile, perception, buffer[..count], out SmartDecision decision);
            SmartGroupCombatLog.Think(bot, decision, perception, target, buffer[..count]);
            // Sitting between heals is recorded once per sit, not every think it stays seated.
            if (decision.Action != "Sit" || !SmartAiDiagnostics.TryGetRing(bot, out SmartAiDiagnostics.DecisionRing sitRing) ||
                sitRing.Last?.Action != "Sit")
                SmartAiDiagnostics.Record(bot, decision);
            // Guard / Protect / Intercept upkeep whatever the action (the legacy brain only ran it in its full offense).
            brain.SmartAiMaintainPeel();
            return handled;
        }

        /// <summary>
        /// The group's target from its snapshot, when this bot may fight it (alive, not held by crowd control, allowed by
        /// the bot's own rules: CanAggroTarget checks companion engagement mode and con). Phase 7: the primary target,
        /// keeping the group on one. Phase 8: while the backline is protected, a peeler spec takes the healer's attacker.
        /// </summary>
        private static GameLiving GroupTarget(BotBrain brain, GameBot bot, SmartSpecProfile profile, SmartPersonalityProfile character,
            SmartGroupTacticalSnapshot snapshot)
        {
            // A Defensive personality peels whatever its role (Phase 10); a healer never leaves triage to peel.
            bool peels = SmartObservationProducer.IsPeelerSpec(profile) || character.Peels && !SmartObservationProducer.IsHealerSpec(profile);
            if (snapshot.Strategy == SmartGroupStrategy.ProtectBackline && peels &&
                Fightable(brain, bot, snapshot.PeelTarget))
                return snapshot.PeelTarget;
            return Fightable(brain, bot, snapshot.PrimaryTarget) ? snapshot.PrimaryTarget : null;
        }

        private static bool Fightable(BotBrain brain, GameBot bot, GameLiving target) =>
            target is { IsAlive: true } && !BotBrain.IsBreakablyControlled(target) &&
            target.CurrentRegionID == bot.CurrentRegionID && brain.CanAggroTarget(target);

        /// <summary>Records a think handed to the legacy brain, with why (once per reason in a row, so the ring keeps
        /// its decisions instead of filling with the same line).</summary>
        private static void RecordLegacy(GameBot bot, string spec, string reason)
        {
            if (SmartAiDiagnostics.TryGetRing(bot, out SmartAiDiagnostics.DecisionRing ring) &&
                ring.Last is { Action: "Legacy" } last && last.Note == reason)
                return;
            SmartAiDiagnostics.Record(bot, new SmartDecision(spec, "Legacy", 0, reason, GameLoop.GameLoopTime));
        }

        /// <summary>A class ability the bot used outside the decision engine (Engage), in its decision ring for
        /// /smartai debug; nothing is recorded while SmartAI is off.</summary>
        public static void RecordAbility(GameBot bot, string action, string note)
        {
            if (!SmartAiConfig.IsEnabled || bot == null)
                return;
            string spec = SmartAiSpecRegistry.Active.Resolve(bot)?.Name ?? "(no spec)";
            SmartAiDiagnostics.Record(bot, new SmartDecision(spec, action, 0, note, GameLoop.GameLoopTime));
        }

        /// <summary>Each bot's last SmartAI decision (from its decision ring).</summary>
        public static bool TryGetLastDecision(GameBot bot, out SmartDecision decision)
        {
            decision = SmartAiDiagnostics.TryGetRing(bot, out SmartAiDiagnostics.DecisionRing ring) ? ring.Last : null;
            return decision != null;
        }

        private static void LogFailure(GameBot bot, SmartAiCircuitBreaker breaker, Exception e, long now)
        {
            long next = Interlocked.Read(ref _nextErrorLogAt);
            if (now < next || Interlocked.CompareExchange(ref _nextErrorLogAt, now + ErrorLogIntervalMs, next) != next)
                return;

            if (Log.IsErrorEnabled)
                Log.Error($"SmartAI failed for {bot.Name} (class {bot.CharacterClass?.Name}, failure {breaker.Trips}); " +
                          $"it uses the legacy bot AI for {SmartAiCircuitBreaker.OpenMs / 1000} s. " +
                          "Further SmartAI errors are not logged for 10 s.", e);
        }
    }
}
