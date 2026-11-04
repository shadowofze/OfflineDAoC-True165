# Claude Code Handoff & Implementation Guide: SmartAI System

> [!IMPORTANT]
> **Developer Execution Blueprint:** This guide contains the exact, step-by-step procedure to transition all architectural designs, data structures, and 39 class templates from the research environment into **Claude Code** to build, compile, and verify the **SmartAI** system in `OfflineDAoC-True165-MP`.

---

## 1. Handoff Architecture & File Locations

All reference materials, design blueprints, and specification templates have been prepared and audited in the artifact directory:
`C:\Users\Darrell\.gemini\antigravity\brain\b627e32e-11a5-40d9-90cf-95877d87d2f0\`

### Package Contents
1. **Core Architecture Documents (7 Files)**:
   - [`PROJECT_ANALYSIS.md`](file:///C:/Users/Darrell/.gemini/antigravity/brain/b627e32e-11a5-40d9-90cf-95877d87d2f0/PROJECT_ANALYSIS.md): Comprehensive system audit, existing AI bottlenecks, and integration seam inventory.
   - [`SMARTAI_ARCHITECTURE.md`](file:///C:/Users/Darrell/.gemini/antigravity/brain/b627e32e-11a5-40d9-90cf-95877d87d2f0/SMARTAI_ARCHITECTURE.md): 5-stage decision pipeline, mathematical utility formulation ($U(a)$), and Blackboard coordination architecture.
   - [`LAUNCHER_INTEGRATION.md`](file:///C:/Users/Darrell/.gemini/antigravity/brain/b627e32e-11a5-40d9-90cf-95877d87d2f0/LAUNCHER_INTEGRATION.md): WPF UI toggle specification, `config.ini` schema, command-line arguments, and dynamic server injection.
   - [`CLASS_TEMPLATE_DESIGN.md`](file:///C:/Users/Darrell/.gemini/antigravity/brain/b627e32e-11a5-40d9-90cf-95877d87d2f0/CLASS_TEMPLATE_DESIGN.md): 17-field Markdown specification schema, Pet Pulling tactics, and Solo Leveling/Dungeon rules.
   - [`CLAUDE_CODE_IMPLEMENTATION_PLAN.md`](file:///C:/Users/Darrell/.gemini/antigravity/brain/b627e32e-11a5-40d9-90cf-95877d87d2f0/CLAUDE_CODE_IMPLEMENTATION_PLAN.md): 11-phase developer execution manual with exact method anchors, new files, and completion criteria.
   - [`SMARTAI_TEST_PLAN.md`](file:///C:/Users/Darrell/.gemini/antigravity/brain/b627e32e-11a5-40d9-90cf-95877d87d2f0/SMARTAI_TEST_PLAN.md): NUnit test specifications, performance benchmarks (<0.05 ms / bot), and combat regression scenarios.
   - [`PROJECT_FILE_REFERENCE.md`](file:///C:/Users/Darrell/.gemini/antigravity/brain/b627e32e-11a5-40d9-90cf-95877d87d2f0/PROJECT_FILE_REFERENCE.md): Line-by-line codebase map for quick lookup.

2. **Class Templates (39 Files — 117 Complete Specializations)**:
   - Full 17-field specifications covering RvR, PvP, PvE Standard, PvE Pet Pulling, Solo Leveling & Dungeon Strategy, and Old Frontiers Realm Ability progression for every class in Albion, Midgard, and Hibernia.

---

## 2. Step 1: Stage Blueprints into the Repository

Before launching Claude Code, stage the blueprints and templates into the target repository so Claude Code can read them directly from its workspace.

Run the following command in **PowerShell** (from any directory):

```powershell
# Define source artifacts and target repo paths
$sourceDir = "C:\Users\Darrell\.gemini\antigravity\brain\b627e32e-11a5-40d9-90cf-95877d87d2f0"
$repoDir   = "C:\OfflineDAoC-True165-MP\OfflineDAoC-True165-MP"

# 1. Create target directories in repository
$docsDir  = "$repoDir\docs\smartai"
$specsDir = "$repoDir\runtime\config\smartai\specs"

New-Item -ItemType Directory -Path $docsDir -Force | Out-Null
New-Item -ItemType Directory -Path $specsDir -Force | Out-Null

# 2. Copy the 7 Architecture Blueprints to docs/smartai/
Copy-Item "$sourceDir\*.md" -Destination $docsDir -Exclude "*_CLASS_TEMPLATE.md" -Force
Write-Host "Copied 7 Architecture Blueprints to $docsDir"

# 3. Copy the 39 Class Templates to runtime/config/smartai/specs/
Copy-Item "$sourceDir\*_CLASS_TEMPLATE.md" -Destination $specsDir -Force
Write-Host "Copied 39 Class Templates to $specsDir"

# Also copy class templates to docs for reference
New-Item -ItemType Directory -Path "$docsDir\specs" -Force | Out-Null
Copy-Item "$sourceDir\*_CLASS_TEMPLATE.md" -Destination "$docsDir\specs" -Force
Write-Host "Blueprints staged successfully for Claude Code!"
```

---

## 3. Step 2: Launching Claude Code

1. Open Windows Terminal / PowerShell.
2. Navigate to the repository root directory:
   ```powershell
   cd C:\OfflineDAoC-True165-MP\OfflineDAoC-True165-MP
   ```
3. Start Claude Code:
   ```bash
   claude
   ```

> [!TIP]
> **Context Window Management**: Do not instruct Claude Code to implement all 11 phases in a single prompt. Doing so exceeds Claude's single-turn token generation capacity and results in truncated code or stubbed implementations. Instead, use the **6 Sequential Batch Prompts** outlined below.

---

## 4. Step 3: Phased Prompt Sequence for Claude Code

Execute the following prompts sequentially in Claude Code. After each batch, allow Claude Code to complete the code changes, compile, and confirm test passes before issuing the next prompt.

### Prompt 1: Context Ingestion & Project Initialization (Phases 1 & 2)

```text
Hello Claude! We are implementing the SmartAI system for OfflineDAoC-True165-MP (.NET 10, C# 13).
All architectural blueprints, design documents, and class templates are already staged in the repository under:
  - docs/smartai/CLAUDE_CODE_IMPLEMENTATION_PLAN.md
  - docs/smartai/SMARTAI_ARCHITECTURE.md
  - docs/smartai/LAUNCHER_INTEGRATION.md
  - docs/smartai/CLASS_TEMPLATE_DESIGN.md
  - runtime/config/smartai/specs/ (all 39 class template markdown files)

Please read docs/smartai/CLAUDE_CODE_IMPLEMENTATION_PLAN.md (Sections 1 & 2) and execute Phase 1 and Phase 2:
1. Phase 1: Implement the configuration properties in `development-source/server/GameServer/smartai/config/SmartAiProperties.cs` and the database/side-table entity in `GameServer/smartai/persistence/OfflineBotSmartAiProfileRecord.cs`. Register properties with ServerProperties.
2. Phase 2: Implement the Launcher UI toggle ("Enable SmartAI") in `development-source/tools/OfflineDaoc.Launcher/` (MainWindow.xaml, ConfigManager.cs, and server process launch arguments). Add fallback capability if SmartAI encounters errors.

Ensure the solution builds cleanly with:
dotnet build "development-source/server/Dawn of Light.sln" -c Release
```

---

### Prompt 2: Core Hooks, Fallback Seam & Spec Parser (Phases 3 & 4)

```text
Great job on Phases 1 and 2! Now proceed to Phase 3 and Phase 4 of docs/smartai/CLAUDE_CODE_IMPLEMENTATION_PLAN.md:

1. Phase 3: Core Hooks & Fallback Seam
   - Create `GameServer/smartai/core/SmartAiBrainDriver.cs` implementing the `ISmartAiBrain` interface.
   - Insert the non-invasive seam hook into `BotBrain.cs` (`Think()` method) and `CompanionBrain.cs`:
     If `SmartAiProperties.ENABLE_SMART_AI` is true and bot has an active SmartAI driver, delegate to `SmartAiBrainDriver.Think()`; on exception or if disabled, cleanly fallback to legacy behavior without crashing.
2. Phase 4: Markdown Parser & Spec Registry
   - Implement `GameServer/smartai/specs/SmartSpecDefinition.cs` (holding all 17 fields, including RvR, PvP, PvE Standard, PvE Pet Pulling, Solo Leveling/Dungeon Strategy, and Old Frontiers Realm Abilities).
   - Implement `GameServer/smartai/specs/SmartAiMarkdownParser.cs` to parse YAML frontmatter and Markdown sections from `runtime/config/smartai/specs/`.
   - Implement `GameServer/smartai/specs/SmartAiSpecRegistry.cs` with thread-safe loading, caching, and hot-reload file watching.
   - Wire `SmartAiSpecRegistry.Initialize()` into server startup (`GameServer.cs`).

Compile and verify that the parser loads the 39 templates cleanly during server initialization.
```

---

### Prompt 3: Hybrid Decision Engine Core & Combat Expansion (Phases 5 & 6)

```text
Now proceed to Phase 5 and Phase 6 of docs/smartai/CLAUDE_CODE_IMPLEMENTATION_PLAN.md:

1. Phase 5: Hybrid Decision Engine Core
   - Implement the 5-stage pipeline in `GameServer/smartai/decision/`:
     - Stage 1: `SmartBotPerception.cs` (Spatial, resource, and group scanning).
     - Stage 2: `SmartRuleGateEvaluator.cs` (Hard veto gates: e.g. breaking mez, casting while moving, shooting engaged targets).
     - Stage 3: `SmartCandidateGenerator.cs` (Action candidates from class spec).
     - Stage 4: `SmartUtilityScorer.cs` (Mathematical utility evaluation: U(a) = B_spec * W_dist * W_res * W_tactical * W_pers - C_risk + H_hysteresis).
     - Stage 5: `SmartActionExecutor.cs` (Deterministic execution).
2. Phase 6: Native Combat Expansion (Styles, Abilities, RAs)
   - Implement `GameServer/smartai/styles/SmartStyleSelector.cs` for opening, positional, and reaction styles.
   - Implement Class Ability handling (`Engage`, `Berserk`, `Protect`, `Guard`).
   - Implement Old Frontiers Realm Ability execution using the progression order defined in Section 17 of each spec (`AtlasOF_*` keys, e.g. `Purge`, `BoF`, `MoC`, `IP`).

Verify the build and ensure the decision loop executes in <0.05 ms per bot.
```

---

### Prompt 4: Blackboard Tactical Coordination & Pet Pulling (Phases 7 & 8)

```text
Proceed to Phase 7 and Phase 8 of docs/smartai/CLAUDE_CODE_IMPLEMENTATION_PLAN.md:

1. Phase 7: Shared Group Tactical State (Blackboard)
   - Implement `GameServer/smartai/communication/SmartGroupTacticalState.cs` and `SmartGroupCoordinator.cs`.
   - Implement shared tactical state tracking: Main Assist (MA) target, crowd-controlled targets (Mez/Stun lock flags), incoming adds, camp anchor position, and group engagement status.
2. Phase 8: Inter-Bot Communication & Pet Pulling Engine
   - Implement tactical coordination for Pet Pulling mode (`GroupEngagementStatus == PetPulling`):
     - Pet is the primary anchor tank; damage shield does the work.
     - Tanks and melee DPS do NOT taunt the main pack off the pet.
     - Break-off protection: Bots immediately peel and attack mobs that break off from the pet toward squishies.
     - Smart Healing: Healers wait until incoming mobs lose ~5% HP before casting small heals on the pet.
     - 85% Mob Health AoE trigger: AoE casters hold area spells until pack reaches 85% health, then unleash AoE nukes.
     - Group stays put at camp anchor (strict stationary discipline; no backpedaling for range).
   - Implement triage and callout policies via `SmartGroupCalloutPolicy.cs`.

Ensure changes compile cleanly with the existing companion and autonomous group systems.
```

---

### Prompt 5: Game Modes, Solo Leveling, Dungeon Strategy & Personalities (Phases 9 & 10)

```text
Proceed to Phase 9 and Phase 10 of docs/smartai/CLAUDE_CODE_IMPLEMENTATION_PLAN.md:

1. Phase 9: Game Mode Intelligence (RvR, PvP, PvE Standard, and Solo Leveling/Dungeon Strategy)
   - Read Section 6c of all class specs (`### 6c. Mode: Solo Leveling & Dungeon Strategy`).
   - Implement environment detection (Dungeon vs. Outdoor zone):
     - Pure Casters (Wizard, Sorcerer, Theurgist, Runemaster, Eldritch, Mentalist): Prioritize outdoor open world; enforce solo dungeon embargo (cannot solo in dungeons without a tank and healer due to tight corridors and cast interrupts).
     - Pet Casters (Cabalist, Necromancer, Spiritmaster, Bonedancer, Enchanter, Animist): Exempt from dungeon embargo; use pet door-blocking and corridor threat anchoring to solo safely.
     - Heavy Tanks & Flankers: Implement corridor pull isolation (pull single mobs 500 units back into cleared hallways to prevent patrol adds).
     - Stealthers & Archers: Open-world sniping vs. dungeon stealth infiltration.
     - Rest protocol: Sit when HP < 50%, Mana < 20%, or End < 20% post-combat; scan 1000 units for patrols before sitting.
2. Phase 10: Personalities & Player Commands
   - Implement the 6 personality profiles in `GameServer/smartai/personalities/`:
     - `Aggressive`, `Defensive`, `Tactical`, `Opportunistic`, `Supportive`, `Conservative`.
   - Wire player commands (`/smartai personality <name>`, `/companion personality <name>`).

Compile and verify that bots switch tactical weights dynamically based on personality and zone environment.
```

---

### Prompt 6: Diagnostics, Verification, Test Suite & Deployment (Phase 11)

```text
Proceed to the final phase: Phase 11 of docs/smartai/CLAUDE_CODE_IMPLEMENTATION_PLAN.md and deploy runtime binaries:

1. Diagnostics & Telemetry:
   - Implement `GameServer/smartai/diagnostics/SmartAiDiagnostics.cs` (in-memory ring buffer of last 32 decisions per bot).
   - Add `/smartai status`, `/smartai inspect <bot>`, and `/smartai reload` GM/player commands.
2. Unit Tests:
   - Create tests in `development-source/server/Tests/UnitTests/SmartAI/`:
     - `UT_SmartAiDecisionEngine.cs` (Rule gate verification, utility scoring).
     - `UT_SmartAiGroupCoordination.cs` (Pet pulling triggers, 85% AoE window, break-off add defense).
     - `UT_SmartAiPerformance.cs` (Ensure execution time < 0.05 ms per bot under 50-bot load).
3. Build & Rule #11 Deployment:
   - Build the solution:
     dotnet build "development-source/server/Dawn of Light.sln" -c Release
   - Execute Rule #11 Deployment (copy GameServer.dll to the 3 mandatory runtime destinations):
     - Copy to runtime/server/GameServer.dll
     - Copy to runtime/server/lib/GameServer.dll
     - Copy to runtime/server/win-x64/GameServer.dll
4. Run the NUnit test suite and confirm all tests pass!
```

---

## 5. Step 4: Compilation, Deployment & Rule #11

When Claude Code completes the code changes, ensure the server build and deployment follow project rules:

### 1. Build Command
```powershell
dotnet build "development-source/server/Dawn of Light.sln" -c Release
```

### 2. Mandatory 3-Destination Deployment (Rule #11)
Every time `GameServer.dll` is compiled, it must be deployed to all three runtime locations. You can run this PowerShell snippet to execute the deployment:

```powershell
$builtDll = "C:\OfflineDAoC-True165-MP\OfflineDAoC-True165-MP\development-source\server\GameServer\bin\Release\net10.0\GameServer.dll"
$destinations = @(
    "C:\OfflineDAoC-True165-MP\OfflineDAoC-True165-MP\runtime\server\GameServer.dll",
    "C:\OfflineDAoC-True165-MP\OfflineDAoC-True165-MP\runtime\server\lib\GameServer.dll",
    "C:\OfflineDAoC-True165-MP\OfflineDAoC-True165-MP\runtime\server\win-x64\GameServer.dll"
)

foreach ($dest in $destinations) {
    if (Test-Path $dest) {
        Copy-Item $dest "$dest.bak" -Force
    }
    Copy-Item $builtDll $dest -Force
    Write-Host "Deployed GameServer.dll to $dest"
}
```

---

## 6. Step 5: In-Game & Launcher Verification Checklist

After Claude Code finishes deployment, perform this quick in-game sanity test:

1. **Launcher Toggle Check**:
   - Open the Offline DAoC Launcher.
   - Verify the **"Enable SmartAI Engine"** checkbox is visible in the Settings/Bot tab.
   - Ensure checking the box writes `UseSmartAI = true` into `config.ini`.
2. **Server Startup Log Check**:
   - Launch the server via the launcher.
   - Check the console output:
     ```text
     [SmartAI] Initializing SmartAI Engine v1.0...
     [SmartAI] Loaded 39 class templates (117 specializations) from runtime/config/smartai/specs/
     [SmartAI] Decision engine initialized (5-stage pipeline, fallback active).
     ```
3. **In-Game Command Check**:
   - Log into a character in-game.
   - Type `/smartai status` — verify server reports `SmartAI: Enabled (Active Bots: X)`.
   - Spawn a companion (`/companion summon`) or encounter an autonomous world bot.
   - Target the bot and type `/smartai inspect` — verify the active specialization, utility score breakdown, and personality profile are displayed.
4. **Pet Pulling Test**:
   - Group with a pet caster (Cabalist, Necromancer, Enchanter, Spiritmaster).
   - Type `/say petpull` or configure the companion to pull.
   - Verify:
     - The pet pulls hostiles back to the parked group.
     - Group members hold still at camp (no fleeing for range).
     - Tanks hold threat off the pet until mobs drop below 85% HP or a mob breaks off.
     - Casters unleash AoE spells at ~85% mob health.
5. **Solo Leveling & Dungeon Behavior Test**:
   - Observe a solo bot in a dungeon vs outdoors:
     - Pure casters avoid entering dungeons alone, staying outdoors to kite.
     - Pet casters enter dungeons and park their pets at doorway chokepoints to door-block.
     - Melee tanks pull single mobs back into cleared corridors.

---

## 7. Troubleshooting & Recovery

- **Reverting to Legacy AI**: If any unexpected runtime error occurs, uncheck "Enable SmartAI" in the Launcher, or type `/smartai disable` in-game. The server instantly reverts to legacy `BotBrain` with zero downtime or restarts required.
- **Hot-Reloading Templates**: If you edit any `.md` file in `runtime/config/smartai/specs/`, type `/smartai reload` in-game to reload the spec without restarting the server.
- **Diagnostic Trace**: If a bot behaves unexpectedly, type `/smartai trace <botName>` to inspect its last 32 decisions and rule gate evaluations.
