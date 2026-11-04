# Document 6: SmartAI Comprehensive Test Plan (`SMARTAI_TEST_PLAN.md`)

> [!IMPORTANT]
> **Testing Mandate:** In accordance with project rule #13, all automated unit and integration tests must run in isolation within `development-source/server/Tests/` using NUnit (`Dawn of Light.sln`). In-game verification must be executed without destabilizing world database state.

---

## 1. Test Architecture & Automated Test Suites

SmartAI tests are organized into 10 structured NUnit test fixtures under `development-source/server/Tests/UnitTests/SmartAI/`:

```
Tests/UnitTests/SmartAI/
├── UT_SmartAiLegacyFallback.cs          # Suite 1: Legacy Parity & Fallback
├── UT_LauncherSmartAiConfig.cs          # Suite 2: Launcher & SQLite Persistence
├── UT_SmartAiMarkdownParser.cs          # Suite 3: Markdown Parser, Validator & Hot-Reload
├── UT_SmartAiDecisionEngine.cs          # Suite 4: Rule Gates & Utility Scoring
├── UT_SmartAiCombatExpansion.cs         # Suite 5: Positional Styles, Abilities & RAs
├── UT_SmartAiGroupConcurrency.cs        # Suite 6: Blackboard Thread-Safety & Snapshots
├── UT_SmartAiCommunication.cs           # Suite 7: Bot Observations & Conflict Resolution
├── UT_SmartAiGameModes.cs               # Suite 8: PvE, PvP, RvR Mode Adaptation
├── UT_SmartAiPersonalities.cs           # Suite 9: Personality Profiles & Bounds
└── UT_SmartAiPerformanceBenchmark.cs    # Suite 10: Latency, Allocations & Anti-Oscillation
```

---

## 2. Test Suite Specifications

### Suite 1: Legacy AI Parity & Fallback Verification
- **Purpose**: Verify that when SmartAI is toggled OFF (or when a class Markdown file is not yet authored), bot behavior remains 100% identical to legacy v0.2.
- **Test Cases**:
  - `Test_GlobalToggleOff_ExecutesZeroSmartAiCode`: Set `SmartAiProperties.USE_SMART_AI = false`. Tick `BotBrain.Think()` across 1,000 simulated combat cycles. Assert that `SmartAiBrainDriver.TryThinkCombat` immediately returns `false` and zero SmartAI instances or allocations occur.
  - `Test_MissingClassMarkdown_TriggersPerBotFallback`: Set `USE_SMART_AI = true`. Populate the registry with `cleric.md` only. Place a Cleric and an unauthored class (e.g. `Theurgist`) in combat. Assert that the Cleric uses SmartAI, the Theurgist safely executes legacy `BotBrain`, and no exceptions are thrown.
  - `Test_EngineException_TripsCircuitBreakerAndFallsBack`: Mock an intentional null dereference inside a custom utility scorer. Verify that the exception is caught, logged once, and the bot seamlessly completes the tick via legacy `BotBrain`.

### Suite 2: Launcher Integration & Configuration Persistence
- **Purpose**: Validate SQLite property persistence, UI state synchronization, and default safety handling.
- **Test Cases**:
  - `Test_UseSmartAi_DefaultsToFalseOnCleanDb`: Initialize an empty in-memory SQLite database without a `ServerProperty` table. Run `ReadOptInSwitch`. Assert that it returns `false`.
  - `Test_ReadOptInSwitch_NeverDefaultsToTrueOnNullOrMissing`: Verify that `null`, empty string, whitespace, or invalid strings (`"GARBAGE"`) return `false`.
  - `Test_PersistSmartAiSetting_UpdatesRowCorrectly`: Toggle `Use SmartAI` to ON and OFF. Verify that the SQL query upserts `Category = 'autonomous'`, `Key = 'use_smart_ai'`, `Value = 'True'` / `'False'`.
  - `Test_ServerPropertyCleanUp_DoesNotPurgeSmartAi`: Run `Properties.CleanUpDatabase()` on a test database. Verify that `use_smart_ai` is preserved because it is registered on `SmartAiProperties`.

### Suite 3: Markdown Parser, Validator & Hot-Reload Engine
- **Purpose**: Ensure that human-authored Markdown specifications parse cleanly, validate references, and support live hot-reloading.
- **Test Cases**:
  - `Test_ValidMarkdown_ParsesAll16RequiredFields`: Feed the sample `cleric.md` through `SmartAiMarkdownParser`. Verify all 16 fields, YAML frontmatter, role, and match specs are correctly mapped into `SmartSpecDefinition`.
  - `Test_SpecMatching_ResolvesHiredCompanionByTrainedPoints`: Construct a simulated `GameBot` with 45 points in `Rejuvenation` and 20 in `Enhancement`. Verify that `SmartAiSpecRegistry.ResolveSpec` selects `Rejuvenation Cleric`.
  - `Test_SpecMatching_ResolvesAutonomousBotBySpecType`: Construct a bot with `BotSpec.SpecType = eSpecType.RejuvCleric`. Verify that it resolves correctly even when spec points are tied at low levels.
  - `Test_SpecValidator_FlagsUnknownSpellLines`: Provide a Markdown file referencing a non-existent spell line `"FakeLine"`. Assert that `SmartAiSpecValidator` reports an error with line number and refuses registration.
  - `Test_HotReload_AtomicallySwapsRegistryWithoutDowntime`: Mutate an existing spec on disk and invoke `/smartai reload`. Verify that the active pointer is swapped atomically without impacting concurrent readers.

### Suite 4: Hybrid Decision Engine & Rule Gate Integrity
- **Purpose**: Guarantee that hard gameplay rules (especially crowd-control respect and player authority) can never be overridden by utility math.
- **Test Cases**:
  - `Test_RuleGate_MezzedTargetNeverDamaged`: Create a combat state with two hostiles: Enemy A (`IsMezzed = true`) and Enemy B (`IsMezzed = false`). Verify that all direct damage nukes, dots, and melee styles on Enemy A receive a utility score of `-Infinity` and are rejected at Stage 2.
  - `Test_RuleGate_PassiveStanceSuppressesAllOffense`: Set `bot.Stance = eBotStance.Passive`. Verify that only heals and cures are evaluated; offensive actions are completely gated out.
  - `Test_RuleGate_QuickCastAllowedWhenInterrupted`: Simulate a Cleric under active melee interrupt (`IsBeingInterrupted = true`). Verify that regular cast-time spells are rejected, but `QuickCast` + `HealBig` is allowed.
  - `Test_UtilityScorer_AppliesMarkdownPriorities`: Validate that an action with Priority 90 in Markdown consistently scores higher than an action with Priority 40 under identical baseline conditions.

### Suite 5: Combat Expansion (Styles, Abilities & RAs)
- **Purpose**: Verify that positional style checks, stealth openers, and active RAs function correctly.
- **Test Cases**:
  - `Test_PositionalStyle_BackstabEligibleInRearArc`: Position an Infiltrator bot at an angle of `180°` (relative rear) to an enemy whose `TargetObject` is a friendly Paladin. Verify that `SmartStyleSelector` selects a positional back style (e.g. `Perforate Artery` / `Backstab`).
  - `Test_PositionalStyle_IneligibleWhenHoldingAggro`: Change the enemy's `TargetObject` to the Infiltrator itself. Verify that rear-arc styles are disqualified and anytime/defensive styles are selected.
  - `Test_HiredCompanion_UsesPurchasedRealmAbilities`: Set `OfflineHiredCompanionRecord.RealmAbilities` to `"Purge:1;MoC:2"`. Stun the companion in combat. Verify that `Purge` is executed and cooldown is stamped.

### Suite 6: Group Blackboard & Concurrency Safety
- **Purpose**: Validate zero lock contention and thread safety across parallel worker threads.
- **Test Cases**:
  - `Test_ParallelReaders_NeverSeeTornSnapshot`: Launch 8 background threads simultaneously reading `SmartGroupTacticalSnapshot` while the pre-pass thread publishes double-buffered updates 100 times per second. Verify zero torn reads or `NullReferenceExceptions`.
  - `Test_CCClaim_PreventsDoubleMezzing`: Simulate two Crowd Control bots (e.g. Sorcerer and Bard) ticking concurrently against 3 incoming adds. Verify that both bots claim distinct targets via atomic CAS claims and never double-cast on the same mob.
  - `Test_GroupDisband_CleansUpStateWithoutMemoryLeak`: Create a `Group`, tick SmartAI, then call `Group.DisbandGroup()`. Verify that `SmartGroupTacticalState` is unlinked and reclaimed by GC.

### Suite 7: Bot-to-Bot Communication & Conflict Resolution
- **Purpose**: Test structured observations, authority arbitration, and group chat callout rate limiting.
- **Test Cases**:
  - `Test_ObservationQueue_EnqueuesAndDrainsCorrectly`: Have 3 bots enqueue `RecommendTargetSwitch`, `HeavyGroupDamageWarning`, and `CrowdControlOpportunity`. Verify all are dequeued during the pre-pass.
  - `Test_Arbitration_HumanOrderAlwaysOverridesBots`: Simulate a bot recommending target switch while the human owner has an active `/assist` target. Verify the human target wins.
  - `Test_CalloutPolicy_ThrottlesGroupMessagesTo8Seconds`: Enqueue 5 urgent warnings in a 2-second span. Verify that only the highest-urgency warning emits a `/g` message, and subsequent messages are muted for 8 seconds.

### Suite 8: Game Mode Intelligence (PvE, PvP, RvR)
- **Purpose**: Verify that the same class specialization shifts behavior appropriately across game modes.
- **Test Cases**:
  - `Test_RvRMode_PrioritizesEnemyHealersAndSpreads`: Set active region to Old Frontiers (`Region 100`). Verify that `SmartUtilityScorer` increases targeting score for enemy healers by `+35` and increases caster spread distance.
  - `Test_PvEMode_FocusesTankTauntAndAddParking`: Set active zone to a PvE dungeon. Verify that tanks prioritize `StylesTaunt` and crowd-controllers prioritize parking un-targeted adds.
  - `Test_PvPMode_KitesMeleePursuers`: Simulate a Wizard facing an enemy Mercenary in 1v1. Verify that the Wizard casts Snare/Root and retreats to max range before casting.

### Suite 9: Personality Profiles & Multiplier Bounds
- **Purpose**: Verify that personality profiles alter decision flavors without violating safety boundaries.
- **Test Cases**:
  - `Test_PersonalityMultipliers_StayWithinBounds`: Verify that all 6 profiles (`Aggressive`, `Defensive`, `Tactical`, `Opportunistic`, `Supportive`, `Conservative`) apply multipliers strictly between `0.80x` and `1.25x`.
  - `Test_AggressiveProfile_PushesFinishingBlow`: Compare an `Aggressive` vs. `Conservative` DPS bot on a target at `18%` HP. Verify the aggressive bot commits offensive cooldowns while the conservative bot conserves mana.

### Suite 10: Performance, Latency & Anti-Oscillation Benchmark
- **Purpose**: Enforce strict performance budgets and anti-jitter hysteresis.
- **Test Cases**:
  - `Test_DecisionLatency_Under50Microseconds`: Measure execution time of `SmartAiBrainDriver.TryThinkCombat` across 10,000 iterations. Assert average time is `< 0.05 ms` (`50 microseconds`) per bot.
  - `Test_ZeroGarbageCollectionPerTick`: Run 1,000 decision ticks with a memory allocation profiler (`GC.GetAllocatedBytesForCurrentThread`). Assert 0 heap bytes allocated per tick by using structs and pooled memory.
  - `Test_TargetStickiness_PreventsRapidTargetSwitching`: Alternate the calculated threat of two targets by small margins (`±2%`) every tick. Verify that the bot holds its target for at least `3.5 seconds` (`PrimaryTargetLockUntilTick`) before switching.

---

## 3. In-Game Verification & Performance Acceptance Criteria

| Benchmark Metric | Measurement Method | Strict Acceptance Threshold |
|---|---|---|
| **Server Tick Latency** | `bot-ai-delay.json` (`BotAiDelayReport.AverageMs`) | Must remain **`< 1.5 ms`** across 100+ active bots. |
| **Peak Tick Stall** | `AiTurnStallMonitor` | Zero `SERVER_STALL` warnings (`>= 1000 ms`) caused by SmartAI. |
| **Target Switching Frequency** | In-combat combat log audit | No bot switches targets more frequently than once every **3.5 seconds** unless current target dies or is crowd-controlled. |
| **Command Jitter** | Godot client visualization | Zero visual turning/movement oscillation while kiting or positioning. |
| **Mez Break Rate** | Combat simulation | **0.0% accidental mez breaks** across 500 test encounters. |
