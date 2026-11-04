using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 4: the class-file parser, validator, spec matching and the registry swap.</summary>
[TestFixture, NonParallelizable]
public class UT_SmartAiSpecRegistry
{
    /// <summary>The server's spec lines, classes and spec types; realm abilities unknown (no game database in tests).</summary>
    private sealed class TestGameData : ISmartAiGameData
    {
        public static readonly TestGameData Instance = new();
        public bool TryResolveSpecLine(string written, out string key) => SmartAiServerGameData.Instance.TryResolveSpecLine(written, out key);
        public bool IsSpecType(string name) => SmartAiServerGameData.Instance.IsSpecType(name);
        public bool TryGetClassName(int classId, out string name) => SmartAiServerGameData.Instance.TryGetClassName(classId, out name);
        public bool? IsRealmAbility(string key) => null;
    }

    private static string Sections(string extra14 = "### 14. What the AI MUST NOT Do\n- MUST NOT melee.\n") =>
        "### 1. Intended Role\nHealer.\n### 2. Intended Role Summary\nHeals.\n### 3. Combat Philosophy\nStay alive.\n" +
        "### 4. Mode: RvR\n- x\n### 5. Mode: PvP\n- x\n### 6a. Mode: PvE Standard\n- x\n### 6b. Mode: PvE Pet Pulling\n- x\n" +
        "### 6c. Mode: Solo Leveling & Dungeon Strategy\n- x\n" +
        "### 7. Offensive Priorities\n1. Direct Damage (Smite): Priority 10 (Only if all group members > 90% HP)\n2. Melee Attack: Priority 0 (Strictly disabled)\n" +
        "### 8. Defensive Priorities\n1. Self-Cast Absorb Bubble: Priority 95 (Keep active at all times)\n" +
        "### 9. Target Priorities\n1. Lowest HP Group Member: Priority 100\n" +
        "### 10. Positioning Preferences\n- **Anchor**: Backline\n" +
        "### 11. Ability Usage Priorities\n1. `Purge` (AtlasOF_Purge): Priority 100 (Use immediately if stunned)\n" +
        "### 12. Resource Management\n- **Conservative Mana Floor**: 30% (Never exhaust mana)\n" +
        "### 13. What the AI SHOULD Do\n- Cure first.\n" + extra14 +
        "### 15. Group Responsibilities\n- Main healer.\n### 16. Situational Responses\n- **When Silenced**: hide.\n" +
        "### 17. Realm Ability Progression Order\n1. **Purge 1** (`AtlasOF_Purge`, Level 1): CC cleanse.\n14. **Augmented Acuity 3–4**: End-game.\n";

    private static string ClericText(string front = null, string rejuvYaml = null) =>
        (front ?? "---\nschema_version: 1\nclass_name: \"Cleric\"\nclass_id: 6\nrealm: \"Albion\"\nprimary_stats: [\"Piety\", \"Constitution\"]\ndefault_personality: \"Supportive\"\n---\n") +
        "\n# Class: Cleric\n\n## Shared Class Capabilities\n- **Armor Type**: Chain.\n\n---\n\n" +
        "## Specialization 1: Rejuvenation Cleric\n- **role**: PrimaryHealer\n- **default_personality**: Supportive\n\n" +
        "```yaml\n" + (rejuvYaml ?? "match_specs:\n  primary_line: \"Rejuvenation\"\n  min_point_ratio: 0.50\n  spec_type_aliases: [\"RejuvCleric\"]\n") + "```\n\n" +
        Sections() + "\n---\n\n" +
        "## Specialization 2: Smite Cleric\n- **role**: CasterDps\n\n" +
        "```yaml\nmatch_specs:\n  primary_line: \"Smite\"\n  min_point_ratio: 0.50\n  spec_type_aliases:\n    - \"SmiteCleric\"\n```\n\n" +
        Sections("### 14. What the AI SHOULD NOT Do\n- Should not melee.\n");

    private static (SmartClassDefinition Class, List<SmartAiIssue> Issues) ParseAndValidate(string text, string source = "albion/cleric.md")
    {
        SmartAiMarkdownParser.Result parsed = SmartAiMarkdownParser.Parse(text, source);
        var issues = parsed.Issues.ToList();
        SmartAiSpecValidator.Validate(parsed.Class, TestGameData.Instance, issues, "albion");
        return (parsed.Class, issues);
    }

    private static string[] Errors(List<SmartAiIssue> issues) =>
        issues.Where(i => i.Severity == SmartAiIssueSeverity.Error).Select(i => i.ToString()).ToArray();

    [TearDown] public void Cleanup() => SmartAiSpecRegistry.Clear();

    [Test]
    public void ValidFile_ParsesEveryPart()
    {
        var (c, issues) = ParseAndValidate(ClericText());
        Assert.That(Errors(issues), Is.Empty);
        Assert.That(c.ClassName, Is.EqualTo("Cleric"));
        Assert.That(c.ClassId, Is.EqualTo(6));
        Assert.That(c.Realm, Is.EqualTo("Albion"));
        Assert.That(c.PrimaryStats, Is.EqualTo(new[] { "Piety", "Constitution" }));
        Assert.That(c.Specs.Select(s => s.Name), Is.EqualTo(new[] { "Rejuvenation Cleric", "Smite Cleric" }));

        SmartSpecDefinition rejuv = c.Specs[0];
        Assert.That(rejuv.Role, Is.EqualTo("PrimaryHealer"));
        Assert.That(rejuv.DefaultPersonality, Is.EqualTo("Supportive"));
        Assert.That(rejuv.Match.PrimaryLine, Is.EqualTo("Rejuvenation"));
        Assert.That(rejuv.Match.PrimaryLineKey, Is.EqualTo("Rejuvenation"));
        Assert.That(rejuv.Match.MinPointRatio, Is.EqualTo(0.5));
        Assert.That(rejuv.Match.SpecTypeAliases, Is.EqualTo(new[] { "RejuvCleric" }));
        Assert.That(SmartSpecSectionKeys.Required.All(k => rejuv.Sections.ContainsKey(k)), Is.True);

        Assert.That(rejuv.OffensivePriorities.Select(p => (p.Rank, p.Name, p.Priority)),
            Is.EqualTo(new[] { (1, "Direct Damage (Smite)", 10), (2, "Melee Attack", 0) }));
        Assert.That(rejuv.OffensivePriorities[0].Condition, Is.EqualTo("Only if all group members > 90% HP"));
        Assert.That(rejuv.AbilityPriorities.Single().Name, Is.EqualTo("`Purge` (AtlasOF_Purge)"));
        Assert.That(rejuv.RealmAbilityOrder.Select(r => (r.Key, r.Level)), Is.EqualTo(new[] { ("AtlasOF_Purge", 1) }),
            "Lines without a (`Key`, Level N) are not steps");
        Assert.That(rejuv.ManaFloorPercent, Is.EqualTo(30));
        Assert.That(rejuv.Section("10").Values["Anchor"], Is.EqualTo("Backline"));
        Assert.That(rejuv.ProhibitionsAreHard, Is.True);
        Assert.That(rejuv.Prohibitions, Is.EqualTo(new[] { "MUST NOT melee." }));

        SmartSpecDefinition smite = c.Specs[1];
        Assert.That(smite.Match.SpecTypeAliases, Is.EqualTo(new[] { "SmiteCleric" }), "Dash lists work too");
        Assert.That(smite.DefaultPersonality, Is.EqualTo("Supportive"), "A spec without its own default takes the class's");
        Assert.That(smite.ProhibitionsAreHard, Is.False, "SHOULD NOT is the soft form");
    }

    [Test]
    public void SpecLinesResolveFromFieldNamesOrValues()
    {
        Assert.That(SmartAiServerGameData.Instance.TryResolveSpecLine("Body_Magic", out string a) ? a : null, Is.EqualTo("Body Magic"));
        Assert.That(SmartAiServerGameData.Instance.TryResolveSpecLine("Body Magic", out string b) ? b : null, Is.EqualTo("Body Magic"));
        Assert.That(SmartAiServerGameData.Instance.TryResolveSpecLine("Longbow", out string c) ? c : null, Is.EqualTo("Longbows"));
        Assert.That(SmartAiServerGameData.Instance.TryResolveSpecLine("FakeSpells", out _), Is.False);
    }

    [Test]
    public void Mojibake_IsRepairedWithAWarning()
    {
        string text = ClericText().Replace("Stay alive.", "1350â€“1500 units, 90Â° arc.");
        var (c, issues) = ParseAndValidate(text);
        Assert.That(Errors(issues), Is.Empty);
        Assert.That(c.Specs[0].Section("3").Lines.Single(), Is.EqualTo("1350–1500 units, 90° arc."));
        Assert.That(issues.Any(i => i.Severity == SmartAiIssueSeverity.Warning && i.Message.Contains("garbled")), Is.True);
    }

    [Test]
    public void CrlfFiles_ParseTheSame()
    {
        var (c, issues) = ParseAndValidate(ClericText().Replace("\n", "\r\n"));
        Assert.That(Errors(issues), Is.Empty);
        Assert.That(c.Specs.Count, Is.EqualTo(2));
    }

    [TestCase("", "missing front matter")]
    [TestCase("---\nschema_version: 1\nclass_name: \"Cleric\"\nrealm: \"Albion\"\n---\n", "class_id 0 is not a class")]
    [TestCase("---\nschema_version: 2\nclass_name: \"Cleric\"\nclass_id: 6\nrealm: \"Albion\"\n---\n", "schema_version 2 is not supported")]
    [TestCase("---\nschema_version: 1\nclass_name: \"Cleric\"\nclass_id: 7\nrealm: \"Albion\"\n---\n", "class_id 7 is Wizard")]
    [TestCase("---\nschema_version: 1\nclass_name: \"Cleric\"\nclass_id: 999\nrealm: \"Albion\"\n---\n", "class_id 999 is not a class")]
    public void CorruptFrontMatter_IsAnError(string front, string expected)
    {
        var (_, issues) = ParseAndValidate(ClericText(front: front));
        Assert.That(Errors(issues), Has.Some.Contains(expected));
    }

    [Test]
    public void UnclosedFrontMatter_IsAnError()
    {
        SmartAiMarkdownParser.Result parsed = SmartAiMarkdownParser.Parse("---\nschema_version: 1\nclass_name: \"Cleric\"\n", "x.md");
        Assert.That(parsed.HasErrors, Is.True);
        Assert.That(parsed.Issues.Select(i => i.Message), Has.Some.Contains("front matter is not closed"));
    }

    [TestCase("match_specs:\n  primary_line: \"FakeSpells\"\n  min_point_ratio: 0.5\n", "unknown spec line 'FakeSpells'")]
    [TestCase("match_specs:\n  primary_line: \"Rejuvenation\"\n  min_point_ratio: 1.5\n", "min_point_ratio must be above 0")]
    [TestCase("match_specs:\n  primary_line: \"Rejuvenation\"\n  min_point_ratio: lots\n", "min_point_ratio 'lots' is not a number")]
    [TestCase("match_specs:\n  min_point_ratio: 0.5\n", "match_specs has no primary_line")]
    public void CorruptMatchSpecs_IsAnError(string yaml, string expected)
    {
        var (_, issues) = ParseAndValidate(ClericText(rejuvYaml: yaml));
        Assert.That(Errors(issues), Has.Some.Contains(expected));
    }

    [Test]
    public void MissingSectionsAndDuplicates_AreErrors()
    {
        string text = ClericText()
            .Replace("### 9. Target Priorities\n1. Lowest HP Group Member: Priority 100\n", "")
            .Replace("## Specialization 2: Smite Cleric", "## Specialization 2: Rejuvenation Cleric");
        var (_, issues) = ParseAndValidate(text);
        Assert.That(Errors(issues), Has.Some.Contains("is missing section 9"));
        Assert.That(Errors(issues), Has.Some.Contains("'Rejuvenation Cleric' appears twice"));
    }

    [Test]
    public void UnclosedCodeBlock_IsAnErrorNotACrash()
    {
        string text = ClericText().Replace("  spec_type_aliases: [\"RejuvCleric\"]\n```", "  spec_type_aliases: [\"RejuvCleric\"]\n");
        Assert.DoesNotThrow(() => ParseAndValidate(text));
        Assert.That(Errors(ParseAndValidate(text).Issues), Has.Some.Contains("not closed"));
    }

    [Test]
    public void SoftProblems_AreWarnings()
    {
        string text = ClericText()
            .Replace("[\"RejuvCleric\"]", "[\"RejuvCleric\", \"NotASpecType\"]")
            .Replace("1. Lowest HP Group Member: Priority 100", "1. Lowest HP Group Member, whenever");
        var (_, issues) = ParseAndValidate(text);
        Assert.That(Errors(issues), Is.Empty);
        string[] warnings = issues.Where(i => i.Severity == SmartAiIssueSeverity.Warning).Select(i => i.Message).ToArray();
        Assert.That(warnings, Has.Some.Contains("'NotASpecType' is not a bot spec type"));
        Assert.That(warnings, Has.Some.Contains("has no 'Priority N'"));
    }

    [Test]
    public void IssueText_NamesFileAndLine()
    {
        var issue = new SmartAiIssue(SmartAiIssueSeverity.Error, "albion/cleric.md", 42, "Unknown spell line 'FakeSpells'!");
        Assert.That(issue.ToString(), Is.EqualTo("ERROR albion/cleric.md (Line 42): Unknown spell line 'FakeSpells'!"));
    }

    // Spec matching -------------------------------------------------------------------------------------------------

    private static SmartClassDefinition Cleric() => ParseAndValidate(ClericText()).Class;

    private static Func<string, int> Levels(int rejuv, int smite) => key => key switch
    {
        "Rejuvenation" => rejuv,
        "Smite" => smite,
        _ => 0,
    };

    [Test]
    public void Resolve_TrainedPointsPickTheTopLine()
    {
        Assert.That(SmartAiSpecRegistry.ResolveSpec(Cleric(), Levels(42, 7), null)?.Name, Is.EqualTo("Rejuvenation Cleric"));
        Assert.That(SmartAiSpecRegistry.ResolveSpec(Cleric(), Levels(10, 40), "RejuvCleric")?.Name, Is.EqualTo("Smite Cleric"),
            "Trained points beat the rolled spec type");
    }

    [Test]
    public void Resolve_TiedOrUntrainedFallsBackToTheSpecType()
    {
        Assert.That(SmartAiSpecRegistry.ResolveSpec(Cleric(), Levels(1, 1), "SmiteCleric")?.Name, Is.EqualTo("Smite Cleric"));
        Assert.That(SmartAiSpecRegistry.ResolveSpec(Cleric(), Levels(5, 5), "smiteCLERIC")?.Name, Is.EqualTo("Smite Cleric"));
    }

    [Test]
    public void Resolve_BelowTheRatioFallsBackToTheSpecType()
    {
        // Rejuvenation 20 (209 points) vs Smite 19 (189): 0.525 of the points, above 0.50.
        Assert.That(SmartAiSpecRegistry.ResolveSpec(Cleric(), Levels(20, 19), null)?.Name, Is.EqualTo("Rejuvenation Cleric"));
        // Rejuvenation 20 vs Smite 19 with a 0.60 floor: no points match, so the spec type decides.
        SmartClassDefinition strict = ParseAndValidate(ClericText(rejuvYaml:
            "match_specs:\n  primary_line: \"Rejuvenation\"\n  min_point_ratio: 0.60\n  spec_type_aliases: [\"RejuvCleric\"]\n")).Class;
        Assert.That(SmartAiSpecRegistry.ResolveSpec(strict, Levels(20, 19), "SmiteCleric")?.Name, Is.EqualTo("Smite Cleric"),
            "A gamebot's rolled type decides before the below-ratio fallback");
        Assert.That(SmartAiSpecRegistry.ResolveSpec(strict, Levels(20, 19), null, out string how)?.Name, Is.EqualTo("Rejuvenation Cleric"),
            "No rolled type (a hired companion): the most-trained line still wins");
        Assert.That(how, Does.StartWith("most-trained line"));
    }

    [Test]
    public void Resolve_NothingMatches_IsLegacy()
    {
        Assert.That(SmartAiSpecRegistry.ResolveSpec(Cleric(), Levels(1, 1), null, out string how), Is.Null);
        Assert.That(how, Is.EqualTo("no primary line trained past 1"));
        Assert.That(SmartAiSpecRegistry.ResolveSpec(Cleric(), Levels(1, 1), "FireWiz"), Is.Null);
        Assert.That(SmartAiSpecRegistry.ResolveSpec(Cleric(), Levels(10, 10), null, out how), Is.Null);
        Assert.That(how, Is.EqualTo("a tie between the most-trained lines"));
        Assert.That(SmartAiSpecRegistry.ResolveSpec(null, Levels(40, 1), null), Is.Null);
    }

    /// <summary>The bug of 2026-11-04: a companion trained Enhancement 10 (Isolde) or untrained came out as Smite from
    /// its random rolled spec type. A companion's spec is its training only.</summary>
    [Test]
    public void Resolve_CompanionTrainingDecides_NotTheRolledType()
    {
        // Three-spec file: Rejuvenation, Smite and Enhancement.
        SmartClassDefinition three = ParseAndValidate(ClericText() + "\n---\n\n## Specialization 3: Enhancement Cleric\n- **role**: HybridSupport\n\n" +
            "```yaml\nmatch_specs:\n  primary_line: \"Enhancement\"\n  min_point_ratio: 0.45\n  spec_type_aliases: [\"EnhanceCleric\"]\n```\n\n" +
            Sections()).Class;
        Func<string, int> isolde = key => key == "Enhancement" ? 10 : 1;
        Assert.That(SmartAiSpecRegistry.ResolveSpec(three, isolde, null, out string how)?.Name, Is.EqualTo("Enhancement Cleric"), how);
        Assert.That(how, Does.StartWith("trained points"));
        Func<string, int> healBot = key => key == "Smite" ? 8 : 1;
        Assert.That(SmartAiSpecRegistry.ResolveSpec(three, healBot, null)?.Name, Is.EqualTo("Smite Cleric"));
        Func<string, int> untrained = _ => 1;
        Assert.That(SmartAiSpecRegistry.ResolveSpec(three, untrained, null), Is.Null, "An untrained companion: the legacy AI, not a guess");
    }

    /// <summary>secondary_line (owner 2026-11-04): specs on one primary line told apart by their second line.</summary>
    [Test]
    public void Resolve_SecondaryLineSeparatesSpecsOnOneLine()
    {
        string Spec(int n, string name, string secondary) =>
            $"## Specialization {n}: {name}\n- **role**: MeleeDPS\n\n```yaml\nmatch_specs:\n  primary_line: \"Dual_Wield\"\n" +
            (secondary == null ? "" : $"  secondary_line: \"{secondary}\"\n") +
            "  min_point_ratio: 0.40\n  spec_type_aliases: []\n```\n\n" + Sections() + "\n---\n\n";
        string text = "---\nschema_version: 1\nclass_name: \"Mercenary\"\nclass_id: 11\nrealm: \"Albion\"\n---\n\n" +
            Spec(1, "Pure Dual Wield", null) + Spec(2, "Shield Flanker", "Shields") + Spec(3, "High Parry Duelist", "Parry");
        var (merc, issues) = ParseAndValidate(text, "albion/mercenary.md");
        Assert.That(Errors(issues), Is.Empty);
        Assert.That(issues.Where(i => i.Message.Contains("share primary_line")), Is.Empty, "Told apart: no warning");

        Func<string, int> Levels(int dw, int shields, int parry) => key => key switch
        {
            "Dual Wield" => dw, "Shields" => shields, "Parry" => parry, _ => 1,
        };
        Assert.That(SmartAiSpecRegistry.ResolveSpec(merc, Levels(40, 1, 1), null)?.Name, Is.EqualTo("Pure Dual Wield"));
        Assert.That(SmartAiSpecRegistry.ResolveSpec(merc, Levels(40, 30, 1), null)?.Name, Is.EqualTo("Shield Flanker"));
        Assert.That(SmartAiSpecRegistry.ResolveSpec(merc, Levels(40, 10, 30), null)?.Name, Is.EqualTo("High Parry Duelist"));
    }

    /// <summary>The owner's Thane rule (2026-11-04): Shields above 35 the Shield Thane, none the Two-Handed Thane, some
    /// Shields up to 35 the Battle Hybrid.</summary>
    [Test]
    public void Resolve_SecondaryLevelRanges_TheThaneRule()
    {
        string Spec(int n, string name, string extra) =>
            $"## Specialization {n}: {name}\n- **role**: DefensiveTank\n\n```yaml\nmatch_specs:\n  primary_line: \"Stormcalling\"\n" + extra +
            "  min_point_ratio: 0.40\n  spec_type_aliases: []\n```\n\n" + Sections() + "\n---\n\n";
        string text = "---\nschema_version: 1\nclass_name: \"Thane\"\nclass_id: 21\nrealm: \"Midgard\"\n---\n\n" +
            Spec(1, "Stormcalling Shield", "  secondary_line: \"Shields\"\n  secondary_min_level: 36   # above 35\n") +
            Spec(2, "Two-Handed Thunder", "") +
            Spec(3, "Battle Hybrid", "  secondary_line: \"Shields\"\n  secondary_min_level: 2\n  secondary_max_level: 35\n");
        var (thane, issues) = ParseAndValidate(text, "midgard/thane.md");
        Assert.That(Errors(issues), Is.Empty);
        Assert.That(issues.Where(i => i.Message.Contains("primary_line")), Is.Empty, "The ranges don't overlap: no warning");

        string Thane(int shields) => SmartAiSpecRegistry.ResolveSpec(thane, key => key switch
        {
            "Stormcalling" => 48, "Shields" => shields, _ => 1,
        }, null)?.Name;
        Assert.That(Thane(42), Is.EqualTo("Stormcalling Shield"));
        Assert.That(Thane(36), Is.EqualTo("Stormcalling Shield"));
        Assert.That(Thane(35), Is.EqualTo("Battle Hybrid"));
        Assert.That(Thane(2), Is.EqualTo("Battle Hybrid"));
        Assert.That(Thane(1), Is.EqualTo("Two-Handed Thunder"), "No Shields trained");
    }

    [Test]
    public void Validator_OverlappingSecondaryRangesWarn()
    {
        string text = ClericText(rejuvYaml: "match_specs:\n  primary_line: \"Smite\"\n  secondary_line: \"Rejuvenation\"\n  secondary_min_level: 10\n  min_point_ratio: 0.5\n")
            .Replace("  primary_line: \"Smite\"\n  min_point_ratio: 0.50", "  primary_line: \"Smite\"\n  secondary_line: \"Rejuvenation\"\n  secondary_max_level: 20\n  min_point_ratio: 0.50");
        var (_, issues) = ParseAndValidate(text);
        Assert.That(issues.Select(i => i.Message), Has.Some.Contains("overlapping secondary_line"));
        string bad = ClericText(rejuvYaml: "match_specs:\n  primary_line: \"Rejuvenation\"\n  secondary_line: \"Smite\"\n  secondary_min_level: 30\n  secondary_max_level: 10\n  min_point_ratio: 0.5\n");
        Assert.That(Errors(ParseAndValidate(bad).Issues), Has.Some.Contains("is above secondary_max_level"));
    }

    [Test]
    public void Validator_UnknownSecondaryLineIsAnError()
    {
        string text = ClericText(rejuvYaml: "match_specs:\n  primary_line: \"Rejuvenation\"\n  secondary_line: \"FakeLine\"\n  min_point_ratio: 0.5\n");
        Assert.That(Errors(ParseAndValidate(text).Issues), Has.Some.Contains("unknown secondary_line 'FakeLine'"));
    }

    [Test]
    public void PointsInvested_FollowsTheSpecCost()
    {
        Assert.That(SmartAiSpecRegistry.PointsInvested(0), Is.Zero);
        Assert.That(SmartAiSpecRegistry.PointsInvested(1), Is.Zero);
        Assert.That(SmartAiSpecRegistry.PointsInvested(2), Is.EqualTo(2));
        Assert.That(SmartAiSpecRegistry.PointsInvested(50), Is.EqualTo(1274));
    }

    // Registry ------------------------------------------------------------------------------------------------------

    private string _dir;

    [SetUp] public void Setup()
    {
        _dir = Path.Combine(Path.GetTempPath(), "smartai-registry-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Path.Combine(_dir, "albion"));
    }

    [TearDown] public void RemoveDirectory()
    {
        if (Directory.Exists(_dir))
            Directory.Delete(_dir, true);
    }

    private static string Wizard() =>
        ClericText(front: "---\nschema_version: 1\nclass_name: \"Wizard\"\nclass_id: 7\nrealm: \"Albion\"\n---\n")
            .Replace("\"Rejuvenation\"", "\"Fire_Magic\"").Replace("\"Smite\"", "\"Cold_Magic\"");

    [Test]
    public void Registry_LoadsValidFilesAndSkipsBrokenOnes()
    {
        File.WriteAllText(Path.Combine(_dir, "albion", "cleric.md"), ClericText());
        File.WriteAllText(Path.Combine(_dir, "albion", "wizard.md"), Wizard().Replace("class_id: 7", "class_id: 999"));

        SmartAiSpecRegistry.LoadReport report = SmartAiSpecRegistry.Reload(_dir, TestGameData.Instance, force: true);
        Assert.That(report.Activated, Is.True);
        Assert.That(report.FilesRead, Is.EqualTo(2));
        Assert.That(report.ClassesLoaded, Is.EqualTo(1));
        Assert.That(report.Errors, Is.GreaterThan(0));
        Assert.That(SmartAiSpecRegistry.Active.Classes.Keys, Is.EquivalentTo(new[] { 6 }));
        Assert.That(SmartAiSpecRegistry.Active.Specs.ContainsKey((6, "Smite Cleric")), Is.True);
    }

    [Test]
    public void Registry_ReloadWithErrorsKeepsTheCurrentOne()
    {
        File.WriteAllText(Path.Combine(_dir, "albion", "cleric.md"), ClericText());
        SmartAiSpecRegistry.Reload(_dir, TestGameData.Instance, force: false);
        SmartAiSpecRegistry before = SmartAiSpecRegistry.Active;
        Assert.That(before.Classes.Count, Is.EqualTo(1));

        File.WriteAllText(Path.Combine(_dir, "albion", "wizard.md"), Wizard());
        File.WriteAllText(Path.Combine(_dir, "albion", "cleric.md"), "garbage");
        SmartAiSpecRegistry.LoadReport report = SmartAiSpecRegistry.Reload(_dir, TestGameData.Instance, force: false);
        Assert.That(report.Activated, Is.False);
        Assert.That(SmartAiSpecRegistry.Active, Is.SameAs(before), "A bad edit never takes SmartAI away mid-session");

        report = SmartAiSpecRegistry.Reload(_dir, TestGameData.Instance, force: true);
        Assert.That(report.Activated, Is.True);
        Assert.That(SmartAiSpecRegistry.Active.Classes.Keys, Is.EquivalentTo(new[] { 7 }), "Forced: the valid wizard loads, the broken cleric falls back");
    }

    [Test]
    public void Registry_TwoFilesForOneClass_SecondIsAnError()
    {
        File.WriteAllText(Path.Combine(_dir, "albion", "cleric.md"), ClericText());
        File.WriteAllText(Path.Combine(_dir, "albion", "cleric-copy.md"), ClericText());
        SmartAiSpecRegistry.LoadReport report = SmartAiSpecRegistry.Reload(_dir, TestGameData.Instance, force: true);
        Assert.That(report.ClassesLoaded, Is.EqualTo(1));
        Assert.That(report.Issues.Single(i => i.Severity == SmartAiIssueSeverity.Error).Message, Does.Contain("already defined"));
    }

    [Test]
    public void Registry_MissingDirectoryIsEmpty()
    {
        SmartAiSpecRegistry.LoadReport report = SmartAiSpecRegistry.Reload(Path.Combine(_dir, "nope"), TestGameData.Instance, force: true);
        Assert.That(report.FilesRead, Is.Zero);
        Assert.That(SmartAiSpecRegistry.Active.Classes, Is.Empty);
    }

    // The shipped class file --------------------------------------------------------------------------------------

    private static string RepoFile(params string[] parts)
    {
        for (DirectoryInfo d = new(TestContext.CurrentContext.TestDirectory); d != null; d = d.Parent)
            if (Directory.Exists(Path.Combine(d.FullName, "development-source")) && Directory.Exists(Path.Combine(d.FullName, "runtime")))
                return Path.Combine(new[] { d.FullName }.Concat(parts).ToArray());
        return null;
    }

    [Test]
    public void ShippedClericFile_LoadsWithNoErrors()
    {
        string dir = RepoFile("runtime", "server", "smartai", "classes");
        if (dir == null || !Directory.Exists(dir))
            Assert.Ignore("runtime/server/smartai/classes is not beside this build");

        SmartAiSpecRegistry registry = SmartAiSpecRegistry.Build(dir, TestGameData.Instance, out SmartAiSpecRegistry.LoadReport report);
        TestContext.Out.WriteLine(report.Summary);
        foreach (SmartAiIssue issue in report.Issues)
            TestContext.Out.WriteLine(issue);
        Assert.That(report.Errors, Is.Zero);
        Assert.That(registry.Classes.ContainsKey(6), Is.True);
        Assert.That(registry.Classes[6].Specs.Select(s => s.Name),
            Is.EqualTo(new[] { "Rejuvenation Cleric", "Enhancement Cleric", "Smite Cleric" }));
        Assert.That(report.Issues.Any(i => i.Message.Contains("garbled")), Is.False, "The shipped file is clean UTF-8");
    }

    /// <summary>The staged templates (runtime/config/smartai/specs), as a report: run explicitly before moving one in.</summary>
    [Test, Explicit("Report on the staged class templates")]
    public void StagedTemplates_Report()
    {
        string dir = RepoFile("runtime", "config", "smartai", "specs");
        if (dir == null || !Directory.Exists(dir))
            Assert.Ignore("runtime/config/smartai/specs is not beside this build");
        SmartAiSpecRegistry.Build(dir, TestGameData.Instance, out SmartAiSpecRegistry.LoadReport report);
        TestContext.Out.WriteLine(report.Summary);
        foreach (SmartAiIssue issue in report.Issues.OrderByDescending(i => i.Severity).ThenBy(i => i.Source))
            TestContext.Out.WriteLine(issue);
    }
}
