using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DOL.GS.SmartAI
{
    /// <summary>What the validator needs to know about the game. The server's is SmartAiServerGameData; tests use fakes.</summary>
    public interface ISmartAiGameData
    {
        /// <summary>A spec line as a template writes it ("Body_Magic", "Body Magic") to the server's key ("Body Magic").</summary>
        bool TryResolveSpecLine(string written, out string key);
        /// <summary>An eSpecType name (autonomous bots' rolled build).</summary>
        bool IsSpecType(string name);
        /// <summary>The eCharacterClass name of an id.</summary>
        bool TryGetClassName(int classId, out string name);
        /// <summary>A realm ability key; null when the game data can't tell (not loaded).</summary>
        bool? IsRealmAbility(string key);
    }

    /// <summary>
    /// Structural checks of a parsed class file (CLASS_TEMPLATE_DESIGN.md section 5.2).
    /// Errors keep the file out of the registry (its class falls back to the legacy AI):
    /// - front matter: schema_version 1, a class_id that is that class_name;
    /// - each specialization: a name, a role, match_specs with a known primary_line and 0 &lt; min_point_ratio ≤ 1,
    ///   every required section.
    /// Warnings load anyway: unknown spec-type aliases or realm ability keys, two specs sharing a primary line,
    /// a realm folder that isn't the class's realm.
    /// </summary>
    public static class SmartAiSpecValidator
    {
        public const int SupportedSchemaVersion = 1;

        public static void Validate(SmartClassDefinition c, ISmartAiGameData game, List<SmartAiIssue> issues, string realmFolder = null)
        {
            string src = c.Source;
            void Warn(int line, string message) => issues.Add(new(SmartAiIssueSeverity.Warning, src, line, message));
            void Error(int line, string message) => issues.Add(new(SmartAiIssueSeverity.Error, src, line, message));

            if (c.SchemaVersion != SupportedSchemaVersion)
                Error(0, $"schema_version {c.SchemaVersion} is not supported (expected {SupportedSchemaVersion})");
            if (c.ClassName.Length == 0)
                Error(0, "class_name is missing");
            if (!game.TryGetClassName(c.ClassId, out string className))
                Error(0, $"class_id {c.ClassId} is not a class");
            else if (!className.Equals(c.ClassName, StringComparison.OrdinalIgnoreCase))
                Error(0, $"class_id {c.ClassId} is {className}, not '{c.ClassName}'");
            if (realmFolder != null && c.Realm.Length > 0 && !realmFolder.Equals(c.Realm, StringComparison.OrdinalIgnoreCase))
                Warn(0, $"realm '{c.Realm}' is in the '{realmFolder}' folder");
            if (c.Specs.Count == 0)
                Error(0, "no '## Specialization N: Name' sections");

            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var lines = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (SmartSpecDefinition s in c.Specs)
            {
                string who = s.Name.Length > 0 ? $"'{s.Name}'" : "a specialization";
                if (s.Name.Length == 0)
                    Error(s.Line, "a specialization has no name");
                else if (!names.Add(s.Name))
                    Error(s.Line, $"{who} appears twice");
                if (s.Role.Length == 0)
                    Error(s.Line, $"{who} has no '- **role**:' line");

                foreach (string key in SmartSpecSectionKeys.Required)
                    if (!s.Sections.ContainsKey(key))
                        Error(s.Line, $"{who} is missing section {key}");

                SmartMatchSpecs m = s.Match;
                if (m == null)
                {
                    Error(s.Line, $"{who} has no ```yaml match_specs``` block");
                    continue;
                }
                if (m.PrimaryLine.Length == 0)
                    Error(m.Line, $"{who}: match_specs has no primary_line");
                else if (!game.TryResolveSpecLine(m.PrimaryLine, out string key))
                    Error(m.Line, $"{who}: unknown spec line '{m.PrimaryLine}'");
                else
                {
                    m.PrimaryLineKey = key;
                    if (lines.TryGetValue(key, out string other))
                        Warn(m.Line, $"{who} and '{other}' share primary_line '{m.PrimaryLine}'; a bot with that top line gets '{other}'");
                    else
                        lines[key] = s.Name;
                }
                if (double.IsNaN(m.MinPointRatio) || m.MinPointRatio <= 0 || m.MinPointRatio > 1)
                    Error(m.Line, $"{who}: min_point_ratio must be above 0 and at most 1");
                foreach (string alias in m.SpecTypeAliases.Where(a => !game.IsSpecType(a)))
                    Warn(m.Line, $"{who}: spec_type_aliases '{alias}' is not a bot spec type, so it never matches");

                foreach (SmartRealmAbilityStep step in s.RealmAbilityOrder)
                    if (game.IsRealmAbility(step.Key) == false)
                        Warn(step.Line, $"{who}: unknown realm ability '{step.Key}'");
            }
        }
    }

    /// <summary>The validator's view of the running server.</summary>
    public sealed class SmartAiServerGameData : ISmartAiGameData
    {
        public static readonly SmartAiServerGameData Instance = new();

        private readonly Dictionary<string, string> _specLines;

        private SmartAiServerGameData()
        {
            _specLines = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (FieldInfo field in typeof(Specs).GetFields(BindingFlags.Public | BindingFlags.Static)
                         .Where(f => f.IsLiteral && f.FieldType == typeof(string)))
            {
                string value = (string)field.GetRawConstantValue();
                _specLines.TryAdd(Normalize(field.Name), value);
                _specLines.TryAdd(Normalize(value), value);
            }
        }

        public bool TryResolveSpecLine(string written, out string key) => _specLines.TryGetValue(Normalize(written), out key);

        public bool IsSpecType(string name) =>
            Enum.TryParse(name, true, out eSpecType type) && type != eSpecType.None && Enum.IsDefined(type);

        public bool TryGetClassName(int classId, out string name)
        {
            name = null;
            if (classId <= 0 || classId > byte.MaxValue || !Enum.IsDefined(typeof(eCharacterClass), (byte)classId))
                return false;
            name = ((eCharacterClass)(byte)classId).ToString();
            return true;
        }

        public bool? IsRealmAbility(string key) =>
            string.IsNullOrEmpty(key) ? false : SkillBase.GetAbility(key, 1) is DOL.GS.RealmAbilities.RealmAbility;

        private static string Normalize(string s) => (s ?? string.Empty).Replace("_", "").Replace(" ", "").Trim();
    }
}
