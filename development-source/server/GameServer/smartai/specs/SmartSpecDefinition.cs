using System;
using System.Collections.Generic;

namespace DOL.GS.SmartAI
{
    /// <summary>One class file (runtime/server/smartai/classes/&lt;realm&gt;/&lt;class&gt;.md), parsed (plan Phase 4,
    /// CLASS_TEMPLATE_DESIGN.md). Immutable once the registry is built; the decision engine only reads it.</summary>
    public sealed class SmartClassDefinition
    {
        public string Source { get; init; } = string.Empty;
        public int SchemaVersion { get; init; }
        public string ClassName { get; init; } = string.Empty;
        public int ClassId { get; init; }
        public string Realm { get; init; } = string.Empty;
        public IReadOnlyList<string> PrimaryStats { get; init; } = [];
        public string DefaultPersonality { get; init; } = string.Empty;
        /// <summary>"## Shared Class Capabilities" and any other class-level text, as written.</summary>
        public IReadOnlyList<string> SharedLines { get; init; } = [];
        public IReadOnlyList<SmartSpecDefinition> Specs { get; init; } = [];
    }

    /// <summary>One "## Specialization N: Name" block.</summary>
    public sealed class SmartSpecDefinition
    {
        public string Name { get; init; } = string.Empty;
        public int Line { get; init; }
        /// <summary>As written (the templates use more role names than the design doc's eight; Phase 5 maps them).</summary>
        public string Role { get; init; } = string.Empty;
        /// <summary>The spec's default personality, else the class's.</summary>
        public string DefaultPersonality { get; init; } = string.Empty;
        public SmartMatchSpecs Match { get; init; }
        /// <summary>Sections by their number key: "1".."5", "6a", "6b", "6c", "7".."17".</summary>
        public IReadOnlyDictionary<string, SmartSpecSection> Sections { get; init; } =
            new Dictionary<string, SmartSpecSection>(StringComparer.OrdinalIgnoreCase);

        public SmartSpecSection Section(string key) => Sections.TryGetValue(key, out SmartSpecSection s) ? s : null;

        public IReadOnlyList<SmartPriorityEntry> OffensivePriorities => Section(SmartSpecSectionKeys.Offensive)?.Priorities ?? [];
        public IReadOnlyList<SmartPriorityEntry> DefensivePriorities => Section(SmartSpecSectionKeys.Defensive)?.Priorities ?? [];
        public IReadOnlyList<SmartPriorityEntry> TargetPriorities => Section(SmartSpecSectionKeys.Target)?.Priorities ?? [];
        public IReadOnlyList<SmartPriorityEntry> AbilityPriorities => Section(SmartSpecSectionKeys.Abilities)?.Priorities ?? [];
        public IReadOnlyList<SmartRealmAbilityStep> RealmAbilityOrder { get; init; } = [];

        /// <summary>Section 14 as "What the AI MUST NOT Do" (hard rule gates) rather than "SHOULD NOT" (score penalties).
        /// The templates use both.</summary>
        public bool ProhibitionsAreHard { get; init; }
        public IReadOnlyList<string> Prohibitions => Section(SmartSpecSectionKeys.Prohibitions)?.Bullets ?? [];

        /// <summary>The "Mana Floor" percentage of section 12, if it gives one.</summary>
        public int? ManaFloorPercent { get; init; }

        /// <summary>The "Sit Between Heals" percentage of section 12, if it gives one: in a PvE fight the bot sits to
        /// regain power while every group member is above it (SmartCombatSit).</summary>
        public int? SitBetweenHealsAbovePercent { get; init; }
    }

    /// <summary>The ```yaml match_specs``` block.</summary>
    public sealed class SmartMatchSpecs
    {
        /// <summary>As written, e.g. "Body_Magic".</summary>
        public string PrimaryLine { get; init; } = string.Empty;
        /// <summary>The server's spec line key (Specs constant value, e.g. "Body Magic"), set by the validator.</summary>
        public string PrimaryLineKey { get; set; } = string.Empty;
        public double MinPointRatio { get; init; }
        /// <summary>Optional: the line that tells apart specs sharing a primary_line (a Mercenary's Shields or Parry
        /// beside its Dual Wield). Empty: the pure spec of that line.</summary>
        public string SecondaryLine { get; init; } = string.Empty;
        /// <summary>The server's key of SecondaryLine, set by the validator.</summary>
        public string SecondaryLineKey { get; set; } = string.Empty;
        /// <summary>Optional level range of the secondary line (0 = not set): a Thane's Shields above 35 is the Shield
        /// spec, 2-35 the Battle Hybrid (owner 2026-11-04). Without a range, the most points on the line decide.</summary>
        public int SecondaryMinLevel { get; init; }
        public int SecondaryMaxLevel { get; init; }
        public bool HasSecondaryRange => SecondaryMinLevel > 0 || SecondaryMaxLevel > 0;
        /// <summary>The range as levels: unset bounds are 2 (trained past 1) and no upper limit.</summary>
        public int SecondaryLow => SecondaryMinLevel > 0 ? SecondaryMinLevel : 2;
        public int SecondaryHigh => SecondaryMaxLevel > 0 ? SecondaryMaxLevel : int.MaxValue;
        public string WeaponType { get; init; } = string.Empty;
        public IReadOnlyList<string> SpecTypeAliases { get; init; } = [];
        public int Line { get; init; }
    }

    public sealed class SmartSpecSection
    {
        public string Key { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public int Line { get; init; }
        /// <summary>The body as written (trimmed lines, blanks removed).</summary>
        public IReadOnlyList<string> Lines { get; init; } = [];
        /// <summary>Top-level "- ..." bullets, without the dash.</summary>
        public IReadOnlyList<string> Bullets { get; init; } = [];
        /// <summary>"- **Key**: value" bullets.</summary>
        public IReadOnlyDictionary<string, string> Values { get; init; } =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        /// <summary>"N. Name: Priority P (condition)" lines.</summary>
        public IReadOnlyList<SmartPriorityEntry> Priorities { get; init; } = [];
    }

    /// <summary>"1. Direct Damage (Smite): Priority 10 (Only if ...)". Priority 0 means disabled.</summary>
    public sealed record SmartPriorityEntry(int Rank, string Name, int Priority, string Condition, int Line);

    /// <summary>"1. **Purge 1** (`AtlasOF_Purge`, Level 1): ..."</summary>
    public sealed record SmartRealmAbilityStep(int Rank, string Key, int Level, string Name, int Line);

    public static class SmartSpecSectionKeys
    {
        public const string IntendedRole = "1", RoleSummary = "2", Philosophy = "3", RvR = "4", PvP = "5",
            PvEStandard = "6a", PvEPetPulling = "6b", SoloLeveling = "6c", Offensive = "7", Defensive = "8", Target = "9",
            Positioning = "10", Abilities = "11", Resources = "12", Should = "13", Prohibitions = "14",
            GroupOrTargetMatrix = "15", SituationalOrFallback = "16", RealmAbilities = "17";

        /// <summary>Every spec must have all of these. 15 and 16 come in two forms (Group Responsibilities / Target
        /// Selection Matrix, Situational Responses / Fallback Behavior); either counts.</summary>
        public static readonly string[] Required =
        [
            IntendedRole, RoleSummary, Philosophy, RvR, PvP, PvEStandard, PvEPetPulling, SoloLeveling, Offensive,
            Defensive, Target, Positioning, Abilities, Resources, Should, Prohibitions, GroupOrTargetMatrix,
            SituationalOrFallback, RealmAbilities,
        ];

        /// <summary>The sections whose numbered lines are "Name: Priority N" lists.</summary>
        public static readonly string[] PriorityLists = [Offensive, Defensive, Target, Abilities];
    }

    public enum SmartAiIssueSeverity { Warning, Error }

    /// <summary>A parse or validation finding, printed like "albion/cleric.md (Line 42): ...".</summary>
    public sealed record SmartAiIssue(SmartAiIssueSeverity Severity, string Source, int Line, string Message)
    {
        public override string ToString() =>
            $"{(Severity == SmartAiIssueSeverity.Error ? "ERROR" : "warning")} {Source}{(Line > 0 ? $" (Line {Line})" : string.Empty)}: {Message}";
    }
}
