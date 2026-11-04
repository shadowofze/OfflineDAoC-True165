# Document 3: OfflineDAoC Launcher Integration Plan (`LAUNCHER_INTEGRATION.md`)

> [!IMPORTANT]
> **Verification Standard:** All code locations, UI coordinate systems, SQLite database schemas, and startup methods in this document were verified directly against [`development-source/tools/OfflineDaoc.Launcher/MainForm.cs`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs) (3,158 lines), [`TrueMode.cs`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/TrueMode.cs), [`BotGoalsSettingsControl.cs`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/BotGoalsSettingsControl.cs), and [`ServerProperties.cs`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/serverproperty/ServerProperties.cs).

---

## 1. Investigation of Existing Launcher Tabs & Settings Architecture

### 1.1 Resolution of the "Bot Options" Tab Location
Investigation of `MainForm.cs` and the entire repository confirms that **no tab is literally titled `"Bot Options"`**. Instead, launcher controls are organized across 8 specific tabs ([`MainForm.cs:473–585`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L473-L585)):

1. `Getting Started`
2. `Server Overview`
3. `Bot Breakdown`
4. `Add Bots`
5. **`Bot Goals Setting`** ([`BotGoalsSettingsControl.cs`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/BotGoalsSettingsControl.cs)): Contains interactive slider controls governing population goals (e.g., PvE exploration, RvR battlegrounds, dungeons). This tab writes float/integer weights directly to JSON at `runtime/server/state-165/bot-goals.json`.
6. `Realm Exchange`
7. **`Options`** ([`MainForm.cs:525–585`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L525-L585)): Built by `BuildOptionsPanel()`. This tab hosts all persistent boolean server feature checkboxes (`_makeMeGm`, `_rvrAnnouncements`, `_pveAnnouncements`).
8. `Diagnostics`

**Architectural Recommendation:** The new toggle named **`Use SmartAI`** belongs directly in the **`Options`** tab alongside `_rvrAnnouncements` and `_pveAnnouncements`. If desired, a section header or label group `"Bot Intelligence"` can be added inside the `Options` panel.

### 1.2 Comparison of Existing Configuration Mechanisms

We audited all 5 configuration stores in OfflineDAoC to determine the least disruptive storage mechanism:

| Storage Mechanism | How It Works & Where Used | Fit for `Use SmartAI` | Verdict |
|---|---|---|---|
| **A. `ServerProperty` SQLite Table** | Native DOL database table (`Category`, `` `Key` ``, `Value`). Queried during `GameServer.Start()` via `Properties.InitProperties()` and `ServerProperties`. | **Perfect Fit**: Native to both Launcher SQLite connection and GameServer startup pipeline. Non-disruptive, survives launcher reboots. | **RECOMMENDED SOLUTION** |
| **B. `offline_local_options` SQLite Table** | Launcher-only table (`Key`, `Value`). Used exclusively for `MakeMeGM` (`MainForm.cs:1542–1558`). | **Not Viable**: `GameServer` has zero references to `offline_local_options`; it is only read by the launcher. | Reject |
| **C. `bot-goals.json`** | JSON file managed by `BotGoalsSettingsControl.cs` in `runtime/server/state-165/`. | **Not Appropriate**: Dedicated solely to autonomous population goal weight sliders (exploration, dungeons, relic raids). | Reject |
| **D. `runtime/true165/true165-rules.json`** | Hardcoded True 1.65 server rules applied at server startup by `TrueMode.Refresh()`. | **DANGEROUS**: Writing user options here would cause `TrueMode.Refresh()` to overwrite player settings or trigger unnecessary database content rebuilds. | Reject |
| **E. Command-Line Argument** | Arguments passed to `CoreServer.exe` by `MainForm.StartServer()`. | **Fragile**: Does not persist if the user launches `CoreServer.exe` directly or via batch script (`START OFFLINE DAOC TRUE 1.65.cmd`). | Reject |

---

## 2. Recommended Implementation: The `Use SmartAI` Toggle

### 2.1 SQLite Schema & Key Definition
In SQLite table `ServerProperty`:
- **`Category`**: `'autonomous'`
- **`Key`**: `'use_smart_ai'`
- **`Value`**: `'True'` or `'False'`
- **`DefaultValue`**: `'False'` (SmartAI is strictly opt-in; legacy AI remains default)

### 2.2 Critical Bug Prevention: `ReadOptInSwitch` vs `ReadSwitch`

> [!CAUTION]
> **Verified Pitfall in `MainForm.cs`:**
> The existing method `MainForm.ReadSwitch` ([`MainForm.cs:1765–1773`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L1765-L1773)) contains the following logic:
> ```csharp
> private static bool ReadSwitch(SqliteConnection connection, string key)
> {
>     string raw = ReadServerProperty(connection, key);
>     return raw == null || !string.Equals(raw.Trim(), "false", StringComparison.OrdinalIgnoreCase);
> }
> ```
> Notice that `raw == null` returns **`true`**! If a row is missing, `ReadSwitch` defaults to **`true`** (used for existing features that are enabled by default).
>
> If `ReadSwitch` were used for `use_smart_ai`, **any clean database or missing row would accidentally enable SmartAI by default**, violating Requirement 2.D and 3!

**Required Fix:** Implement a dedicated helper `ReadOptInSwitch`:
```csharp
private static bool ReadOptInSwitch(SqliteConnection connection, string key)
{
    string raw = ReadServerProperty(connection, key);
    return string.Equals(raw?.Trim(), "true", StringComparison.OrdinalIgnoreCase);
}
```
This guarantees that missing rows, empty strings, nulls, or corrupted values **always evaluate to `false` (Legacy AI fallback)**.

---

## 3. Step-by-Step Code Changes

### Step 1: Launcher UI Declaration (`MainForm.cs`)

In [`MainForm.cs`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L165-L175), declare the new toggle field alongside existing options:
```csharp
// Around line 170 in MainForm.cs
private CheckBox _makeMeGm;
private CheckBox _rvrAnnouncements;
private CheckBox _pveAnnouncements;
private CheckBox _useSmartAi; // <--- NEW TOGGLE
```

### Step 2: Build Options Panel Layout Adjustment (`MainForm.cs`)

In [`MainForm.BuildOptionsPanel()`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L525-L585):
1. Increase `optionsLayout.RowCount` from `6` to `7` (or `8` with spacer).
2. Adjust `optionsCard.Height` / `InsetPanel.Height` from `330` to `370` to accommodate the new row cleanly without scrollbars.
3. Instantiate and wire up `_useSmartAi`:

```csharp
// Inside BuildOptionsPanel():
_useSmartAi = new CheckBox
{
    Text = "Use SmartAI", // <--- EXACT STRING REQUIRED BY SPEC
    AutoSize = true,
    Font = ThemeControls.BodyFont(10.5f, FontStyle.Bold),
    ForeColor = ThemeControls.TextPrimary,
    BackColor = Color.Transparent,
    Margin = new Padding(0, 4, 0, 8),
    Cursor = Cursors.Hand,
};

Label smartAiDescription = new Label
{
    Text = "Enables advanced combat coordination, specialization intelligence, and tactical bot-to-bot communication for Hired Companions and World Bots. When unchecked, legacy AI is used.",
    AutoSize = true,
    Font = ThemeControls.BodyFont(8.5f),
    ForeColor = ThemeControls.TextMuted,
    Margin = new Padding(22, 0, 0, 10),
};

_useSmartAi.CheckedChanged += (s, e) =>
{
    if (_suppressEvents) return;
    PersistSmartAiSetting(_useSmartAi.Checked);
};

// Add to optionsLayout:
optionsLayout.Controls.Add(_useSmartAi);
optionsLayout.Controls.Add(smartAiDescription);
```

### Step 3: Persistence Logic (`MainForm.cs`)

Implement `PersistSmartAiSetting` mirroring `PersistAnnouncementSetting` ([`MainForm.cs:1560–1585`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L1560-L1585)):

```csharp
private void PersistSmartAiSetting(bool enabled)
{
    // Do not permit edits while server is running
    if (!string.Equals(ServerState, "Stopped", StringComparison.OrdinalIgnoreCase))
        return;

    try
    {
        string dbPath = GetDatabasePath();
        if (!File.Exists(dbPath)) return;

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        connection.Open();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO ServerProperty (Category, `Key`, Value, DefaultValue, Description)
            VALUES ('autonomous', 'use_smart_ai', @val, 'False', 'Enable SmartAI tactical intelligence for hired companions and bots.')
            ON CONFLICT(`Key`) DO UPDATE SET Value = @val;
        ";
        cmd.Parameters.AddWithValue("@val", enabled ? "True" : "False");
        cmd.ExecuteNonQuery();

        LogInfo($"SmartAI setting updated: Use SmartAI = {enabled}");
    }
    catch (Exception ex)
    {
        LogError("Failed to persist SmartAI setting", ex);
    }
}
```

### Step 4: Loading State on Launcher Startup (`MainForm.cs`)

In [`MainForm.ReadSnapshot()`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L1740-L1763):
```csharp
// Read opt-in switch safely
bool smartAiEnabled = ReadOptInSwitch(connection, "use_smart_ai");
```
And in [`MainForm.UpdateUiFromSnapshot()`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/tools/OfflineDaoc.Launcher/MainForm.cs#L1820-L1850):
```csharp
_suppressEvents = true;
try
{
    _useSmartAi.Checked = snapshot.SmartAiEnabled;
    // Disable checkbox while server is running, matching existing toggles
    _useSmartAi.Enabled = string.Equals(snapshot.ServerState, "Stopped", StringComparison.OrdinalIgnoreCase);
}
finally
{
    _suppressEvents = false;
}
```

### Step 5: Server-Side Property Registration (`GameServer/smartai/SmartAiProperties.cs`)

To ensure `Properties.InitProperties()` automatically binds this setting during server boot, and to **prevent `Properties.CleanUpDatabase()` ([`ServerProperties.cs:2677–2721`](file:///C:/OfflineDAoC-True165-MP/OfflineDAoC-True165-MP/development-source/server/GameServer/serverproperty/ServerProperties.cs#L2677-L2721)) from purging it as an unrecognized rogue property**, declare the property in the server:

```csharp
namespace DOL.GS.SmartAI
{
    public static class SmartAiProperties
    {
        [ServerProperty("autonomous", "use_smart_ai", "Enable SmartAI tactical intelligence for hired companions and bots.", false)]
        public static bool USE_SMART_AI = false;

        [ServerProperty("autonomous", "smart_ai_group_callouts", "Enable concise /g chat callouts from SmartAI companions.", true)]
        public static bool SMART_AI_GROUP_CALLOUTS = true;

        [ServerProperty("autonomous", "smart_ai_debug_logging", "Enable detailed SmartAI decision ring-buffer logging.", false)]
        public static bool SMART_AI_DEBUG_LOGGING = false;
    }
}
```

---

## 4. Startup & Failure Modes (Verification Matrix)

| Scenario | Launcher Behavior | Server Startup Behavior | Active Bot AI Mode |
|---|---|---|---|
| **User checks `Use SmartAI` and clicks START SERVER** | Upserts `use_smart_ai = 'True'` in SQLite; disables checkbox while server runs. | `Properties.InitProperties()` loads `USE_SMART_AI = true`. `SmartAiConfig.Initialize()` initializes spec registry and group coordinator. | **SmartAI Active** (with per-bot fallback for unauthored classes) |
| **User unchecks `Use SmartAI` and clicks START SERVER** | Upserts `use_smart_ai = 'False'` in SQLite. | `USE_SMART_AI = false`. SmartAI coordinator and driver return `false` on first branch. | **100% Legacy AI** (Identical to v0.2) |
| **Fresh Database (Clean Install / Reset)** | Row does not exist; `ReadOptInSwitch` returns `false`. Checkbox displays unchecked. | Row does not exist; `Properties.InitProperties()` inserts default row `'False'`. | **100% Legacy AI Fallback** |
| **Corrupted / Invalid Value (e.g. `Value = 'GARBAGE'`)** | `ReadOptInSwitch` returns `false`. Checkbox displays unchecked. | `bool.TryParse` fails; DOL property loader falls back to `defaultValue = false`. | **100% Legacy AI Fallback** |
| **Launcher crashed while server was running** | On next launch, inspects PID or socket. Re-enables toggle once server process exits. | Server completes normal shutdown via `GameServer.Stop()`. | Preserved safely |
| **Server started directly via CLI / batch script** | Launcher not involved. | Reads `use_smart_ai` directly from SQLite `ServerProperty` table. | Behaves identically to launcher boot |

---

## 5. Verification Checklist for Claude Code
1. [ ] Checkbox text is exactly `"Use SmartAI"`.
2. [ ] Checkbox is placed in `MainForm.BuildOptionsPanel()` (`Options` tab).
3. [ ] `ReadOptInSwitch` is implemented and used (NOT `ReadSwitch`).
4. [ ] Toggle cannot be modified while `ServerState != "Stopped"`.
5. [ ] `SmartAiProperties.USE_SMART_AI` is registered with `[ServerProperty]`.
6. [ ] `runtime/true165/true165-rules.json` is **NOT modified** (ensuring user choice is never overwritten by content refreshes).
7. [ ] Unit test in `Tests/UnitTests/SmartAI/UT_LauncherSmartAiConfig.cs` verifies all failure modes.
