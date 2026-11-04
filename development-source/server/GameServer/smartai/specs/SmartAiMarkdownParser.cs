using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// Reads a SmartAI class file (CLASS_TEMPLATE_DESIGN.md section 5.1): a line reader with no outside dependencies.
    /// - Front matter between the first two "---" lines: "key: value", lists as ["a", "b"].
    /// - "## Specialization N: Name" starts a specialization ("N" optional); other "## " headings are class-level text.
    /// - In a specialization: "- **role**: X", "- **default_personality**: X", one ```yaml match_specs``` block, and
    ///   "### N. Title" sections ("6a", "6b"...; text after the number is the title).
    /// - Sections 7, 8, 9 and 11 compile "N. Name: Priority P (condition)" lines; section 17 compiles
    ///   "N. **Name** (`AtlasOF_Key`, Level L)" lines.
    /// It never throws on content: everything it can't read becomes an issue, and structural checks against the game
    /// (spec lines, classes, realm abilities) are the validator's.
    /// </summary>
    public static class SmartAiMarkdownParser
    {
        public sealed record Result(SmartClassDefinition Class, IReadOnlyList<SmartAiIssue> Issues)
        {
            public bool HasErrors => Issues.Any(i => i.Severity == SmartAiIssueSeverity.Error);
        }

        // UTF-8 text that went through Windows-1252 once (all that occurs in the staged templates: en dash, em dash, degree).
        private static readonly (string Bad, string Good)[] Mojibake =
        [
            ("â€“", "–"),
            ("â€”", "—"),
            ("Â°", "°"),
        ];

        private static readonly Regex SpecHeading = new(@"^##\s+Specialization(?:\s+\d+)?\s*:\s*(?<name>.+?)\s*$", RegexOptions.Compiled);
        private static readonly Regex ClassHeading = new(@"^##\s+(?<title>.+?)\s*$", RegexOptions.Compiled);
        private static readonly Regex SectionHeading = new(@"^###\s+(?:(?<key>\d+[a-z]?)\.\s*)?(?<title>.+?)\s*$", RegexOptions.Compiled);
        private static readonly Regex SpecMeta = new(@"^-\s+\*\*(?<key>role|default_personality)\*\*\s*:\s*(?<value>.+?)\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex KeyValueBullet = new(@"^-\s+\*\*(?<key>[^*]+)\*\*\s*:\s*(?<value>.*?)\s*$", RegexOptions.Compiled);
        private static readonly Regex Numbered = new(@"^(?<rank>\d+)\.\s+(?<body>.+)$", RegexOptions.Compiled);
        private static readonly Regex PriorityLine = new(@"^(?<name>.+?)\s*:\s*Priority\s+(?<p>-?\d+)\s*(?:\((?<cond>.*)\))?\s*\.?\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RealmAbilityLine = new(@"^\*\*(?<name>[^*]+)\*\*\s*\(\s*`(?<key>[A-Za-z0-9_]+)`\s*,\s*Level\s*(?<level>\d+)\s*\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex Percent = new(@"(?<n>\d{1,3})\s*%", RegexOptions.Compiled);

        public static Result Parse(string text, string source)
        {
            var issues = new List<SmartAiIssue>();
            void Warn(int line, string message) => issues.Add(new(SmartAiIssueSeverity.Warning, source, line, message));
            void Error(int line, string message) => issues.Add(new(SmartAiIssueSeverity.Error, source, line, message));

            text ??= string.Empty;
            int repaired = 0;
            foreach ((string bad, string good) in Mojibake)
            {
                int count = CountOf(text, bad);
                if (count == 0)
                    continue;
                repaired += count;
                text = text.Replace(bad, good, StringComparison.Ordinal);
            }
            if (repaired > 0)
                Warn(0, $"repaired {repaired} garbled character(s) (UTF-8 read as Windows-1252); save the file as UTF-8");

            string[] lines = text.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
            int i = 0;

            // Front matter.
            var front = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            while (i < lines.Length && lines[i].Trim().Length == 0)
                i++;
            if (i < lines.Length && lines[i].Trim() == "---")
            {
                int start = i++;
                bool closed = false;
                for (; i < lines.Length; i++)
                {
                    string l = lines[i].Trim();
                    if (l == "---")
                    {
                        closed = true;
                        i++;
                        break;
                    }
                    if (l.Length == 0 || l.StartsWith('#'))
                        continue;
                    int colon = l.IndexOf(':');
                    if (colon <= 0)
                        Warn(i + 1, $"front matter line not understood: '{l}'");
                    else
                        front[l[..colon].Trim()] = l[(colon + 1)..].Trim();
                }
                if (!closed)
                    Error(start + 1, "front matter is not closed with '---'");
            }
            else
                Error(0, "missing front matter (the file must start with '---')");

            var shared = new List<string>();
            var specs = new List<SpecBuilder>();
            SpecBuilder spec = null;
            SectionBuilder section = null;

            for (; i < lines.Length; i++)
            {
                int lineNo = i + 1;
                string raw = lines[i];
                string l = raw.Trim();

                if (l.StartsWith("```", StringComparison.Ordinal))
                {
                    // A fenced block: match_specs inside a specialization, otherwise kept as text.
                    string lang = l[3..].Trim();
                    int fenceLine = lineNo;
                    var body = new List<(int, string)>();
                    for (i++; i < lines.Length && !lines[i].Trim().StartsWith("```", StringComparison.Ordinal); i++)
                        body.Add((i + 1, lines[i]));
                    if (i >= lines.Length)
                        Error(fenceLine, "code block is not closed with ```");
                    if (spec != null && section == null && lang.Equals("yaml", StringComparison.OrdinalIgnoreCase))
                    {
                        if (spec.Match != null)
                            Warn(fenceLine, $"'{spec.Name}': a second yaml block; the first match_specs is used");
                        else
                            spec.Match = ParseMatchSpecs(body, fenceLine, Warn, Error, spec.Name);
                    }
                    else if (section != null)
                        section.Lines.AddRange(body.Select(b => (b.Item1, b.Item2.Trim())).Where(b => b.Item2.Length > 0));
                    continue;
                }

                Match m = SpecHeading.Match(l);
                if (m.Success)
                {
                    spec = new SpecBuilder { Name = m.Groups["name"].Value, Line = lineNo };
                    specs.Add(spec);
                    section = null;
                    continue;
                }
                if (l.StartsWith("## ", StringComparison.Ordinal))
                {
                    spec = null;
                    section = null;
                    shared.Add(l);
                    continue;
                }
                if (l.StartsWith("### ", StringComparison.Ordinal))
                {
                    if (spec == null)
                    {
                        shared.Add(l);
                        continue;
                    }
                    m = SectionHeading.Match(l);
                    string key = m.Groups["key"].Success ? m.Groups["key"].Value.ToLowerInvariant() : m.Groups["title"].Value;
                    if (!m.Groups["key"].Success)
                        Warn(lineNo, $"'{spec.Name}': section '{key}' has no number; it is kept but the engine won't use it");
                    if (spec.Sections.ContainsKey(key))
                    {
                        Warn(lineNo, $"'{spec.Name}': section {key} appears twice; the first is used");
                        section = new SectionBuilder { Key = key, Title = m.Groups["title"].Value, Line = lineNo }; // discarded
                        continue;
                    }
                    section = new SectionBuilder { Key = key, Title = m.Groups["title"].Value, Line = lineNo };
                    spec.Sections[key] = section;
                    continue;
                }
                if (l.StartsWith("# ", StringComparison.Ordinal) || l == "---" || l.Length == 0)
                    continue;

                if (spec == null)
                {
                    shared.Add(l);
                    continue;
                }
                if (section == null)
                {
                    m = SpecMeta.Match(l);
                    if (m.Success)
                    {
                        if (m.Groups["key"].Value.Equals("role", StringComparison.OrdinalIgnoreCase))
                            spec.Role = m.Groups["value"].Value.Trim('`', '"');
                        else
                            spec.DefaultPersonality = m.Groups["value"].Value.Trim('`', '"');
                    }
                    continue;
                }
                section.Lines.Add((lineNo, l));
            }

            var definition = new SmartClassDefinition
            {
                Source = source,
                SchemaVersion = int.TryParse(Unquote(front.GetValueOrDefault("schema_version")), out int schema) ? schema : 0,
                ClassName = Unquote(front.GetValueOrDefault("class_name")),
                ClassId = int.TryParse(Unquote(front.GetValueOrDefault("class_id")), out int classId) ? classId : 0,
                Realm = Unquote(front.GetValueOrDefault("realm")),
                PrimaryStats = ParseList(front.GetValueOrDefault("primary_stats")),
                DefaultPersonality = Unquote(front.GetValueOrDefault("default_personality")),
                SharedLines = shared,
                Specs = specs.Select(s => s.Build(front, Warn)).ToList(),
            };
            return new Result(definition, issues);
        }

        private static SmartMatchSpecs ParseMatchSpecs(List<(int Line, string Text)> body, int fenceLine,
            Action<int, string> warn, Action<int, string> error, string specName)
        {
            string primary = string.Empty, secondary = string.Empty, weapon = string.Empty;
            int secondaryMin = 0, secondaryMax = 0;
            double ratio = double.NaN;
            var aliases = new List<string>();
            bool sawRoot = false;
            List<string> openList = null;

            foreach ((int line, string rawText) in body)
            {
                string t = rawText.Trim();
                if (t.Length == 0 || t.StartsWith('#'))
                    continue;
                int hash = t.IndexOf(" #", StringComparison.Ordinal);
                if (hash >= 0)
                    t = t[..hash].TrimEnd();
                if (t.StartsWith("- ", StringComparison.Ordinal) && openList != null)
                {
                    openList.Add(Unquote(t[2..]));
                    continue;
                }
                openList = null;
                int colon = t.IndexOf(':');
                if (colon <= 0)
                {
                    warn(line, $"'{specName}': match_specs line not understood: '{t}'");
                    continue;
                }
                string key = t[..colon].Trim();
                string value = t[(colon + 1)..].Trim();
                switch (key.ToLowerInvariant())
                {
                    case "match_specs":
                        sawRoot = true;
                        break;
                    case "primary_line":
                        primary = Unquote(value);
                        break;
                    case "secondary_line":
                        secondary = Unquote(value);
                        break;
                    case "secondary_min_level":
                    case "secondary_max_level":
                        if (!int.TryParse(Unquote(value), NumberStyles.Integer, CultureInfo.InvariantCulture, out int level) || level < 1 || level > 50)
                            error(line, $"'{specName}': {key} '{value}' must be a level from 1 to 50");
                        else if (key.Equals("secondary_min_level", StringComparison.OrdinalIgnoreCase))
                            secondaryMin = level;
                        else
                            secondaryMax = level;
                        break;
                    case "min_point_ratio":
                        if (!double.TryParse(Unquote(value), NumberStyles.Float, CultureInfo.InvariantCulture, out ratio))
                            error(line, $"'{specName}': min_point_ratio '{value}' is not a number");
                        break;
                    case "weapon_type":
                        weapon = Unquote(value);
                        break;
                    case "spec_type_aliases":
                        if (value.Length == 0)
                            openList = aliases;
                        else
                            aliases.AddRange(ParseList(value));
                        break;
                    default:
                        warn(line, $"'{specName}': unknown match_specs key '{key}'");
                        break;
                }
            }
            if (!sawRoot)
                warn(fenceLine, $"'{specName}': the yaml block has no 'match_specs:' line");
            return new SmartMatchSpecs
            {
                PrimaryLine = primary,
                SecondaryLine = secondary,
                SecondaryMinLevel = secondaryMin,
                SecondaryMaxLevel = secondaryMax,
                MinPointRatio = ratio,
                WeaponType = weapon,
                SpecTypeAliases = aliases,
                Line = fenceLine,
            };
        }

        private sealed class SpecBuilder
        {
            public string Name = string.Empty;
            public int Line;
            public string Role = string.Empty;
            public string DefaultPersonality = string.Empty;
            public SmartMatchSpecs Match;
            public readonly Dictionary<string, SectionBuilder> Sections = new(StringComparer.OrdinalIgnoreCase);

            public SmartSpecDefinition Build(Dictionary<string, string> front, Action<int, string> warn)
            {
                var sections = Sections.ToDictionary(p => p.Key, p => p.Value.Build(Name, warn), StringComparer.OrdinalIgnoreCase);
                var realmAbilities = new List<SmartRealmAbilityStep>();
                if (Sections.TryGetValue(SmartSpecSectionKeys.RealmAbilities, out SectionBuilder ra))
                {
                    foreach ((int line, string text) in ra.Lines)
                    {
                        Match n = Numbered.Match(text);
                        if (!n.Success)
                            continue;
                        Match r = RealmAbilityLine.Match(n.Groups["body"].Value);
                        if (r.Success)
                            realmAbilities.Add(new SmartRealmAbilityStep(int.Parse(n.Groups["rank"].Value, CultureInfo.InvariantCulture),
                                r.Groups["key"].Value, int.Parse(r.Groups["level"].Value, CultureInfo.InvariantCulture),
                                r.Groups["name"].Value.Trim(), line));
                    }
                }

                int? manaFloor = null;
                if (sections.TryGetValue(SmartSpecSectionKeys.Resources, out SmartSpecSection resources))
                {
                    foreach ((string key, string value) in resources.Values)
                    {
                        if (key.Contains("Mana Floor", StringComparison.OrdinalIgnoreCase) && Percent.Match(value) is { Success: true } pm)
                        {
                            manaFloor = int.Parse(pm.Groups["n"].Value, CultureInfo.InvariantCulture);
                            break;
                        }
                    }
                }

                string prohibitionTitle = Sections.TryGetValue(SmartSpecSectionKeys.Prohibitions, out SectionBuilder p) ? p.Title : string.Empty;
                return new SmartSpecDefinition
                {
                    Name = Name,
                    Line = Line,
                    Role = Role,
                    DefaultPersonality = DefaultPersonality.Length > 0 ? DefaultPersonality : Unquote(front.GetValueOrDefault("default_personality")),
                    Match = Match,
                    Sections = sections,
                    RealmAbilityOrder = realmAbilities,
                    ProhibitionsAreHard = prohibitionTitle.Contains("MUST NOT", StringComparison.OrdinalIgnoreCase),
                    ManaFloorPercent = manaFloor,
                };
            }
        }

        private sealed class SectionBuilder
        {
            public string Key = string.Empty;
            public string Title = string.Empty;
            public int Line;
            public readonly List<(int Line, string Text)> Lines = [];

            public SmartSpecSection Build(string specName, Action<int, string> warn)
            {
                var bullets = new List<string>();
                var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var priorities = new List<SmartPriorityEntry>();
                bool priorityList = Array.IndexOf(SmartSpecSectionKeys.PriorityLists, Key) >= 0;

                foreach ((int line, string text) in Lines)
                {
                    if (text.StartsWith("- ", StringComparison.Ordinal))
                    {
                        bullets.Add(text[2..].Trim());
                        Match kv = KeyValueBullet.Match(text);
                        if (kv.Success)
                            values.TryAdd(kv.Groups["key"].Value.Trim(), kv.Groups["value"].Value);
                        continue;
                    }
                    if (!priorityList)
                        continue;
                    Match n = Numbered.Match(text);
                    if (!n.Success)
                        continue;
                    Match pm = PriorityLine.Match(n.Groups["body"].Value);
                    if (pm.Success)
                        priorities.Add(new SmartPriorityEntry(int.Parse(n.Groups["rank"].Value, CultureInfo.InvariantCulture),
                            pm.Groups["name"].Value.Trim(), int.Parse(pm.Groups["p"].Value, CultureInfo.InvariantCulture),
                            pm.Groups["cond"].Success ? pm.Groups["cond"].Value.Trim() : string.Empty, line));
                    else
                        warn(line, $"'{specName}': section {Key} line has no 'Priority N': '{text}'");
                }

                return new SmartSpecSection
                {
                    Key = Key,
                    Title = Title,
                    Line = Line,
                    Lines = Lines.Select(l => l.Text).ToList(),
                    Bullets = bullets,
                    Values = values,
                    Priorities = priorities,
                };
            }
        }

        private static string Unquote(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            value = value.Trim();
            if (value.Length >= 2 && (value[0] == '"' && value[^1] == '"' || value[0] == '\'' && value[^1] == '\''))
                value = value[1..^1];
            return value.Trim();
        }

        private static List<string> ParseList(string value)
        {
            value = value?.Trim() ?? string.Empty;
            if (value.StartsWith('[') && value.EndsWith(']'))
                value = value[1..^1];
            return value.Split(',').Select(Unquote).Where(v => v.Length > 0).ToList();
        }

        private static int CountOf(string text, string part)
        {
            int count = 0;
            for (int at = text.IndexOf(part, StringComparison.Ordinal); at >= 0; at = text.IndexOf(part, at + part.Length, StringComparison.Ordinal))
                count++;
            return count;
        }
    }
}
