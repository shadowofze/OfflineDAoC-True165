# Document 7: OfflineDAoC True 1.65 — Indexed Project File Reference (`PROJECT_FILE_REFERENCE.md`)

> [!IMPORTANT]
> **Reference Guarantee:** All paths, classes, methods, and line ranges in this document were verified directly against `C:\OfflineDAoC-True165-MP\OfflineDAoC-True165-MP`. This index is designed as a direct lookup manual for **Claude Code**.

---

## 1. True 1.65 Mode & Hired Companions (`development-source/server/GameServer/mode165/`)

| File Path & Line Range | Class / Interface | Key Methods & Purpose |
|---|---|---|
| [`Classic165Mode.cs:15–21`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/Classic165Mode.cs#L15-L21) | `Classic165Mode` | `IsOn`: Global switch (`Properties.CLASSIC165_MODE`).<br>`IsFreeshardScript(Type)`: Blocks non-period scripts.<br>`GmOnlyCommands`: Lists restricted GM commands (`/spawn`, `/raid`, etc.). |
| [`PlayerRules165.cs:10–20`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/PlayerRules165.cs#L10-L20) | `PlayerRules165` | `IsPlayerLike(GameLiving)`: True for `GamePlayer` and `GameBot`.<br>`IsPlayerRulesBot(GameLiving)`: Exempts bots from mob penalties (e.g. interrupt timer). |
| [`HiredCompanions165.cs:15–33`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/HiredCompanions165.cs#L15-L33) | `OfflineHiredCompanionRecord` | `[DataTable(TableName = "offline_hired_companions")]`: Side-table for persistent player-owned companions (`BotId`, `OwnerCharacterID`, `Row`, `WithOwner`, `RealmAbilities`). |
| [`HiredCompanions165.cs:45–260`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/HiredCompanions165.cs#L45-L260) | `HiredCompanions165` | `IsHired(long botId)`: Gating helper.<br>`Hire(...)`, `Summon(...)`, `Dismiss(...)`, `Release(...)`.<br>`CostCopper = [0, 2k, 6k, 20k, 60k, 150k, 300k]`. |
| [`Hire165.cs:10–120`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/Hire165.cs#L10-L120) | `Hire165` | `SlotLevels = [5, 10, 15, 20, 30, 40, 45]`: Unlock levels for the 7 companion slots.<br>`Design` struct and character validation. |
| [`CompanionWindows165.cs:20–350`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/CompanionWindows165.cs#L20-L350) | `CompanionWindows165` | Manages DebugMode `0x21` `'H'` packet protocol for companion sheet, training, respec, bag, and vault windows. |
| [`CompanionChat165.cs:15–120`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/CompanionChat165.cs#L15-L120) | `CompanionChat165` | Voice and group chat command parser (`follow`, `stay`, `passive`, `defensive`, `aggressive`, `assist`, `buffs`, etc.). |
| [`CompanionGearVault165.cs:15–90`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/CompanionGearVault165.cs#L15-L90) | `CompanionGearVault165` | 40-slot shared gear vault per player account (`offline_companion_vault`). |
| [`BaseClassBots165.cs:16–76`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/mode165/BaseClassBots165.cs#L16-L76) | `BaseClassBots165` | Base class lifecycle for autonomous bots: L1–4 base class training, L5 trainer promotion to `PlannedClassId`. |

---

## 2. Bot Core & Brain Systems (`development-source/server/GameServer/bots/`)

| File Path & Line Range | Class / Interface | Key Methods & Purpose |
|---|---|---|
| [`GameBot.cs:32–5638`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/GameBot.cs#L32) | `GameBot` | Primary bot entity (inherits `GameNPC`, implements `IGamePlayer`). Holds cached spell buckets (`L2440–2559`), style buckets (`L2560–2657`), `CompanionTrain`/`CompanionRespec` (`L3614–3642`), `SpendSpecPoints` (`L3939–4004`), `CastSpell` (`L3018–3090`), `StartAttack` (`L3092–3114`), `PathTo`/`WalkTo` (`L5425–5468`). |
| [`BotBrain.cs:24–5792`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/BotBrain.cs#L24) | `BotBrain` | Main tactical brain. FSM states (`IDLE`, `FOLLOW`, `AGGRO`, `PASSIVE`, `L2975–3309`). Combat cascade (`AttackMostWanted`, `CheckHeals`, `CheckSpells`, `UseAbilities`). |
| `BotBrain.*.cs` (8 partials) | `BotBrain` (Partials) | `.Animations.cs`, `.CastingInterrupts.cs`, `.CrowdControlRespect.cs`, `.DungeonPullCommitment.cs`, `.GroupCures.cs`, `.GroupSupport.cs`, `.Interrupts.cs`, `.SelfPreservation.cs`. |
| [`BotMeleeStylePolicy.cs:7–69`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/BotMeleeStylePolicy.cs#L7-L69) | `BotMeleeStylePolicy` | `Select(bot, lastAttack)`: Style selection. *Note: Line 9 hard-blocks positional/stealth styles in legacy AI.* |
| [`BotDefensiveAbilityPolicy.cs:15–80`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/BotDefensiveAbilityPolicy.cs#L15-L80) | `BotDefensiveAbilityPolicy` | Manages `Guard`, `Protect`, and `Intercept` pairing between tanks and squishies. |
| [`PlayerLedPullCoordinator.cs:10–140`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/PlayerLedPullCoordinator.cs#L10-L140) | `PlayerLedPullCoordinator` | Coordinates player-ordered pulls for companion groups (`/say pull`, `Hold`, etc.). |
| [`CompanionFollowPolicy.cs:10–90`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/CompanionFollowPolicy.cs#L10-L90) | `CompanionFollowPolicy` | Slot-based follow formation offsets (`FollowSlotOffset`) around the group anchor. |
| [`BotDatabase.cs:15–120`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/BotDatabase.cs#L15-L120) | `DBBot` | Legacy `/bot` database table (`Bots`), GM-only in True 1.65. |

---

## 3. Autonomous World Bot Architecture (`development-source/server/GameServer/bots/autonomous/`)

| File Path & Line Range | Class / Interface | Key Methods & Purpose |
|---|---|---|
| [`AutonomousWorldBotController.cs:421–448`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/autonomous/AutonomousWorldBotController.cs#L421-L448) | `AutonomousWorldBotController` | High-level world bot driver. **Returns `false` in combat**, yielding all combat decisions back to `BotBrain`! |
| [`AutonomousPopulationController.cs:46–120`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/autonomous/AutonomousPopulationController.cs#L46-L120) | `AutonomousPopulationController` | Controls active population count, login/logout cycles, and polls `offline_bot_commands`. |
| [`AutonomousBotRegistry.cs:20–250`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/autonomous/AutonomousBotRegistry.cs#L20-L250) | `AutonomousBotRegistry` | Active bot registry, spatial lookup, and pre-tick brain scheduling. |
| [`AutonomousBotGroupCoordinator.cs:25–380`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/autonomous/AutonomousBotGroupCoordinator.cs#L25-L380) | `AutonomousBotGroupCoordinator` | Strategic group coordinator for autonomous bot camps, dungeon marching, and group formation. |
| [`AutonomousRvrDirector.cs:20–310`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/autonomous/AutonomousRvrDirector.cs#L20-L310) | `AutonomousRvrDirector` | Strategic RvR coordinator: frontier keep sieges, defense, and battleground population. |
| [`AutonomousRealmRaid.cs`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/autonomous/AutonomousRealmRaid.cs) / `RealmWarbandSupport` | `AutonomousRealmRaid` | Manages 24–48 bot warbands for keep takes and Relic Raids. |
| [`AutonomousBotRealmAbilities.cs:18–298`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/autonomous/AutonomousBotRealmAbilities.cs#L18-L298) | `AutonomousBotRealmAbilities` | Autonomous bot RA planning and active RA firing (`Purge`, `IgnorePain`, `SecondWind`, `FirstAid`). |
| [`AutonomousBotStatusPersistence.cs:15–226`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/autonomous/AutonomousBotStatusPersistence.cs#L15-L226) | `AutonomousBotStatusPersistence` | Batched background writer for `offline_world_bots` (16 rows/sec off the game loop). |

---

## 4. Class Specializations & Build Plans (`development-source/server/GameServer/bots/specs/`)

| File Path & Line Range | Class / Interface | Key Methods & Purpose |
|---|---|---|
| [`BotSpec.cs:5–136`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/specs/BotSpec.cs#L5-L136) | `BotSpec` | Base specification class.<br>`GetSpec(charClass, specType)`: Switches across 40 classes.<br>`GetSpecializationChoices(charClass)`: Selectable `eSpecType[]`. |
| [`BotSpecType.cs:11–116`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/specs/BotSpecType.cs#L11-L116) | `eSpecType` | Enum of all specialization archetypes across the three realms. |
| [`SpecLine.cs:3–15`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/specs/SpecLine.cs#L3-L15) | `BotSpecLine` | Struct holding `Spec` (name), `SpecCap`, and `levelRatio`. |
| [`BotLifetimeBuild.cs:8–65`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/BotLifetimeBuild.cs#L8-L65) | `BotLifetimeBuild` | JSON serialization/restoration of bot spec plans in `OfflineWorldBotRecord`. |
| `specs/Albion/*.cs` (14 files) | `*BotSpec` | `Armsman`, `Cabalist`, `Cleric`, `Friar`, `Infiltrator`, `Mercenary`, `Minstrel`, `Necromancer`, `Paladin`, `Reaver`, `Scout`, `Sorcerer`, `Theurgist`, `Wizard`. |
| `specs/Hibernia/*.cs` (14 files) | `*BotSpec` | `Animist`, `Bard`, `Blademaster`, `Champion`, `Druid`, `Eldritch`, `Enchanter`, `Hero`, `Mentalist`, `Nightshade`, `Ranger`, `Valewalker`, `Warden`, *(and custom `Sluaghbinder`)*. |
| `specs/Midgard/*.cs` (12 files) | `*BotSpec` | `Berserker`, `Bonedancer`, `Healer`, `Hunter`, `Runemaster`, `Savage`, `Shadowblade`, `Shaman`, `Skald`, `Spiritmaster`, `Thane`, `Warrior`. |

---

## 5. ECS GameLoop, Concurrency & Services

| File Path & Line Range | Class / Interface | Key Methods & Purpose |
|---|---|---|
| [`GameLoop.cs:27–173`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/Managers/GameLoop/GameLoop.cs#L27-L173) | `GameLoop` | Monotonic 30 TPS ECS game loop (`TickDuration = 33.33 ms`). Sequenced 17-stage pipeline. |
| [`GameLoopThreadPoolMultiThreaded.cs:13–368`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/Managers/GameLoop/GameLoopThreadPoolMultiThreaded.cs#L13-L368) | `GameLoopThreadPoolMultiThreaded` | Dynamic work-stealing thread pool running `brain.Think()` in parallel across CPU cores. |
| [`NpcService.cs:25–50`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/ECS-Services/NpcService.cs#L25-L50) | `NpcService` | Runs serial pre-tick hooks (`L28–31`) followed by parallel `brain.Think()` fan-out (`L46`). |
| [`BotAiDelayReport.cs:12–39`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/Managers/GameLoop/BotAiDelayReport.cs#L12-L39) | `BotAiDelayReport` | Atomic telemetry writer for `bot-ai-delay.json` (tracks average & peak AI latency). |
| [`AiTurnStallMonitor.cs:14–72`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/Managers/GameLoop/AiTurnStallMonitor.cs#L14-L72) | `AiTurnStallMonitor` | Tracks individual brain turns `>= 100 ms` and logs `SERVER_STALL` warnings `>= 1000 ms`. |
| [`Group.cs:20–758`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/gameutils/Group.cs#L20-L758) | `Group` | Group management. `_groupMembersLock` protects membership; `GetMembersInTheGroup()` returns pooled snapshot. |
| [`CastingComponent.cs:25–605`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/ECS-Components/CastingComponent.cs#L25-L605) | `CastingComponent` | Manages active and queued spell handlers, interrupt checking, and cast timing. |
| [`AttackComponent.cs:30–280`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/ECS-Components/AttackComponent.cs#L30-L280) | `AttackComponent` | Manages melee/ranged attack state, target tracking, and weapon swinging. |
| [`StyleComponent.cs:15–120`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/ECS-Components/StyleComponent.cs#L15-L120) | `StyleComponent` | Holds `NextCombatStyle` and `NextCombatBackupStyle`. Read by `AttackService.Tick()`. |
| [`StyleProcessor.cs:81–145`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/skillhandler/StyleProcessor.cs#L81-L145) | `StyleProcessor` | `CanUseStyle(lastAttack, bot, style, weapon)`: Validates weapon, opening, endurance, and target angle. |
| [`EffectListComponent.cs:20–180`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/ECS-Components/EffectListComponent.cs#L20-L180) | `EffectListComponent` | Tracks active effects: `ContainsEffectForEffectType(eEffect.Mez / Stun / Disease / Poison)`. |

---

## 6. Server Startup, Properties & Launcher

| File Path & Line Range | Class / Component | Key Responsibilities |
|---|---|---|
| [`GameServer.cs:284–576`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/GameServer.cs#L284-L576) | `GameServer` | Server startup sequence: `InitDB()`, `Properties.InitProperties()`, `Classic165Mode.OnPropertiesLoaded()`, `GameLoop.Init()`. |
| [`ServerProperties.cs:14–2723`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/serverproperty/ServerProperties.cs#L14-L2723) | `ServerProperties.Properties` | Reflection-scanned static properties bound to the SQLite `ServerProperty` table. |
| [`MainForm.cs:473–585`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L473-L585) | `MainForm` (Launcher) | Builds the 8 launcher tabs, including `Options` (`BuildOptionsPanel`) and `Bot Goals Setting`. |
| [`MainForm.cs:1540–1850`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L1540-L1850) | `MainForm` (Options) | SQLite persistence helpers: `ReadSnapshot()`, `ReadSwitch()`, `PersistAnnouncementSetting()`. |
| [`BotGoalsSettingsControl.cs`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/BotGoalsSettingsControl.cs) | `BotGoalsSettingsControl` | Interactive sliders writing autonomous population goal weights to `bot-goals.json`. |

---

## 7. Database Tables & Configuration Stores

| Table / File Name | Database / Location | Purpose & Schema | Read / Written By |
|---|---|---|---|
| **`ServerProperty`** | `opendaoc-165.sqlite3.db` | Server configuration (`Category`, `` `Key` ``, `Value`, `DefaultValue`). Holds `use_smart_ai`. | Read by `Properties.InitProperties()`; written by Launcher `MainForm.cs`. |
| **`offline_bot_smartai_profiles`** | `opendaoc-165.sqlite3.db` | **New SmartAI Table**: Per-bot personality overrides (`BotId`, `Personality`, `PreferredRole`, `UpdatedUtc`). | Read/written by `SmartAiPersonalityRegistry`. |
| **`offline_hired_companions`** | `opendaoc-165.sqlite3.db` | Hired companion roster (`BotId`, `OwnerCharacterID`, `Row`, `WithOwner`, `RealmAbilities`). | `HiredCompanions165.cs`. |
| **`offline_world_bots`** | `opendaoc-165.sqlite3.db` | Full autonomous bot roster (`BotId`, `Level`, `PlannedClassId`, `SerializedBuildPlan`, `Coordinates`). | `AutonomousBotRegistry.cs` / `MainForm.cs`. |
| **`offline_local_options`** | `opendaoc-165.sqlite3.db` | Launcher UI persistence (`Key`, `Value`). Used only for `MakeMeGM`. | Launcher only (`MainForm.cs`). |
| **`runtime/server/state-165/bot-goals.json`** | File System (JSON) | Autonomous population goal weights (exploration, RvR, dungeons). | `BotGoalsSettingsControl.cs` / `AutonomousBotGoalPolicy.cs`. |
| **`runtime/true165/true165-rules.json`** | File System (JSON) | Enforced True 1.65 rules applied by `TrueMode.Refresh()`. | `TrueMode.cs`. |

---

## 8. Discovered Codebase Bugs & Implementation Caveats

| Issue / Caveat | Exact File & Line Reference | Impact & SmartAI Workaround |
|---|---|---|
| **Split-Lock Data Race in `Group`** | [`Group.cs:714, 722`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/gameutils/Group.cs#L712-L726) | `IsGroupInCombat()` and `IsInTheGroup()` lock `_groupMembers` instead of `_groupMembersLock`, racing with `AddMember`/`RemoveMember`. SmartAI reads `SmartGroupTacticalSnapshot` instead of querying `Group` in combat loops. |
| **`Group.MakeLeader` Crash on Bot** | [`Group.cs:461`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/gameutils/Group.cs#L440-L463) | Dereferences `(LivingLeader as GamePlayer).Name`, throwing `NullReferenceException` if a bot is made group leader. SmartAI manages an internal `TacticalLeader` without calling `Group.MakeLeader(bot)`. |
| **`ReadSwitch` Defaults Missing to True** | [`MainForm.cs:1765–1773`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L1765-L1773) | `raw == null` returns `true`! SmartAI uses `ReadOptInSwitch`, which returns `false` on missing or null rows. |
| **Positional Styles Blocked in Melee Policy** | [`BotMeleeStylePolicy.cs:9–10`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/BotMeleeStylePolicy.cs#L9-L10) | Hardcodes `OpeningRequirementType != Positional`. SmartAI provides `SmartStyleSelector` to unlock rear/side style chains. |
| **ListCaster Spells Collapse Ranks** | [`GameBot.cs:4407–4442`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/GameBot.cs#L4407-L4442) | Overwrites lower-level spells of identical cast-time/type, leaving `HealBig == null` on certain specs. SmartAI checks `HealBig ?? HealEfficient`. |
| **`BotSpec.SpecName` Threading Race** | [`BotSpec.cs:7`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/specs/BotSpec.cs#L7) | `public static string SpecName;` is mutated across threads in instance constructors. SmartAI stores spec name in instance records. |
| **Broken `SuppSpirit` Spec Ratios** | [`Spiritmaster.cs:41, 55–56`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/specs/Midgard/Spiritmaster.cs#L41-L56) | Variances 4–5 set `Suppression` ratio to `0.0f` (blocking training prior to L50), variance 7 swaps ratios, and `Is2H` is omitted. |
| **Missing Weapon Lines on Friar & Warden** | [`Friar.cs:81–87`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/specs/Albion/Friar.cs#L81-L87) / [`Warden.cs:51–57`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/specs/Hibernia/Warden.cs#L51-L57) | Friar variance 7 omits `Staff`, and Warden variance 3 omits `WeaponOneType`. SmartAI Markdown specs correctly define required lines. |
| **Half-Level Spec Bonus on Incremental Train** | [`GameBot.cs:4015–4018`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/bots/GameBot.cs#L4006-L4020) | Only awards half-level points (`level > 40`) when `botCreation == true`, shortchanging autonomous bots leveling 1 level at a time. |
