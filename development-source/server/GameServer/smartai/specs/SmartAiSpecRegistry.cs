using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using DOL.Events;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// The loaded SmartAI class files (plan Phase 4, CLASS_TEMPLATE_DESIGN.md sections 2 and 5).
    /// - Files: runtime/server/smartai/classes/&lt;realm&gt;/&lt;class&gt;.md, loaded at server start while SmartAI is on.
    /// - A file with errors is left out, so its class keeps the legacy AI; the other classes load.
    /// - /smartai reload builds a new registry and swaps it in atomically. When any file has errors it keeps the
    ///   current one, unless forced.
    /// - Readers take Active once per think: an immutable registry, safe from every worker thread.
    /// </summary>
    public sealed class SmartAiSpecRegistry
    {
        private static readonly Logging.Logger Log = Logging.LoggerManager.Create(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly SmartAiSpecRegistry Empty = new(
            FrozenDictionary<int, SmartClassDefinition>.Empty, string.Empty, DateTime.MinValue);

        private static SmartAiSpecRegistry _active = Empty;

        /// <summary>The registry in use. Never null.</summary>
        public static SmartAiSpecRegistry Active => Volatile.Read(ref _active);

        public static string DefaultDirectory => Path.Combine(AppContext.BaseDirectory, "smartai", "classes");

        public FrozenDictionary<int, SmartClassDefinition> Classes { get; }
        public FrozenDictionary<(int ClassId, string SpecName), SmartSpecDefinition> Specs { get; }
        public string Directory { get; }
        public DateTime LoadedUtc { get; }

        private SmartAiSpecRegistry(FrozenDictionary<int, SmartClassDefinition> classes, string directory, DateTime loadedUtc)
        {
            Classes = classes;
            Specs = classes.Values
                .SelectMany(c => c.Specs.Select(s => (Key: (c.ClassId, s.Name), Spec: s)))
                .ToFrozenDictionary(p => p.Key, p => p.Spec);
            Directory = directory;
            LoadedUtc = loadedUtc;
        }

        public sealed record LoadReport(int FilesRead, int ClassesLoaded, int SpecsLoaded, IReadOnlyList<SmartAiIssue> Issues,
            bool Activated)
        {
            public int Errors => Issues.Count(i => i.Severity == SmartAiIssueSeverity.Error);
            public int Warnings => Issues.Count(i => i.Severity == SmartAiIssueSeverity.Warning);

            public string Summary =>
                $"{ClassesLoaded} class(es), {SpecsLoaded} spec(s) from {FilesRead} file(s); {Errors} error(s), {Warnings} warning(s)" +
                (Activated ? string.Empty : "; the previous registry was kept");
        }

        /// <summary>Reads and validates every *.md under the directory (a missing directory is an empty registry).</summary>
        public static SmartAiSpecRegistry Build(string directory, ISmartAiGameData game, out LoadReport report)
        {
            var issues = new List<SmartAiIssue>();
            var classes = new Dictionary<int, SmartClassDefinition>();
            int files = 0;

            if (System.IO.Directory.Exists(directory))
            {
                foreach (string path in System.IO.Directory.EnumerateFiles(directory, "*.md", SearchOption.AllDirectories)
                             .OrderBy(p => p, StringComparer.OrdinalIgnoreCase))
                {
                    files++;
                    string source = Path.GetRelativePath(directory, path).Replace('\\', '/');
                    string text;
                    try
                    {
                        text = File.ReadAllText(path);
                    }
                    catch (Exception e)
                    {
                        issues.Add(new(SmartAiIssueSeverity.Error, source, 0, $"cannot be read: {e.Message}"));
                        continue;
                    }

                    var fileIssues = new List<SmartAiIssue>();
                    SmartClassDefinition definition;
                    try
                    {
                        SmartAiMarkdownParser.Result parsed = SmartAiMarkdownParser.Parse(text, source);
                        fileIssues.AddRange(parsed.Issues);
                        definition = parsed.Class;
                        string folder = Path.GetDirectoryName(Path.GetRelativePath(directory, path));
                        SmartAiSpecValidator.Validate(definition, game, fileIssues, string.IsNullOrEmpty(folder) ? null : Path.GetFileName(folder));
                    }
                    catch (Exception e)
                    {
                        // The parser and validator report content problems as issues; this is a bug, never a reason to stop.
                        fileIssues.Add(new(SmartAiIssueSeverity.Error, source, 0, $"could not be loaded: {e.Message}"));
                        definition = null;
                    }

                    if (definition != null && classes.TryGetValue(definition.ClassId, out SmartClassDefinition earlier))
                        fileIssues.Add(new(SmartAiIssueSeverity.Error, source, 0, $"class {definition.ClassName} is already defined by {earlier.Source}"));

                    issues.AddRange(fileIssues);
                    if (definition != null && !fileIssues.Any(i => i.Severity == SmartAiIssueSeverity.Error))
                        classes[definition.ClassId] = definition;
                }
            }

            var registry = new SmartAiSpecRegistry(classes.ToFrozenDictionary(), directory, DateTime.UtcNow);
            report = new LoadReport(files, classes.Count, registry.Specs.Count, issues, Activated: false);
            return registry;
        }

        /// <summary>Builds a registry and makes it active. With errors it keeps the active one unless force is set; the
        /// valid files then load and the broken ones fall back to the legacy AI.</summary>
        public static LoadReport Reload(string directory, ISmartAiGameData game, bool force)
        {
            SmartAiSpecRegistry staged = Build(directory, game, out LoadReport report);
            bool activate = force || report.Errors == 0;
            if (activate)
                Interlocked.Exchange(ref _active, staged);
            return report with { Activated = activate };
        }

        /// <summary>Back to no specs (tests, or SmartAI turned off).</summary>
        public static void Clear() => Interlocked.Exchange(ref _active, Empty);

        /// <summary>The spec for a bot, or null for the legacy AI (no file for its class, or no spec matches).</summary>
        public SmartSpecDefinition Resolve(GameBot bot) => Resolve(bot, out _);

        /// <summary>As Resolve, saying how it matched (for /smartai spec).</summary>
        public SmartSpecDefinition Resolve(GameBot bot, out string how)
        {
            how = "no SmartAI class file for its class";
            if (bot?.CharacterClass == null || !Classes.TryGetValue(bot.CharacterClass.ID, out SmartClassDefinition definition))
                return null;
            // A hired companion is what its player trained: its rolled spec type (a random autonomous build it never
            // follows) must not decide. Only an autonomous gamebot's rolled type counts.
            string specType = !HiredCompanions165.IsHired(bot) && bot.BotSpec is { } botSpec && botSpec.SpecType != eSpecType.None
                ? botSpec.SpecType.ToString() : null;
            return ResolveSpec(definition, bot.GetBaseSpecLevel, specType, out how);
        }

        public static SmartSpecDefinition ResolveSpec(SmartClassDefinition definition, Func<string, int> specLevel, string specTypeName) =>
            ResolveSpec(definition, specLevel, specTypeName, out _);

        /// <summary>
        /// The matching algorithm (CLASS_TEMPLATE_DESIGN.md section 2.2), on its own for tests.
        /// 1. Trained points: the spec whose primary_line has the most points invested, if that line holds at least
        ///    min_point_ratio of the points in the class's primary lines. Points invested in a line at level L are
        ///    L(L+1)/2 - 1 (level 1 is free).
        /// 2. Otherwise an autonomous gamebot's rolled spec type against spec_type_aliases (a hired companion has none:
        ///    the caller passes null).
        /// 3. Otherwise the most-trained primary line, below its ratio (a hybrid build still fights as its main line).
        /// 4. Otherwise null: nothing trained, or a tie at the top.
        /// </summary>
        public static SmartSpecDefinition ResolveSpec(SmartClassDefinition definition, Func<string, int> specLevel, string specTypeName,
            out string how)
        {
            how = "no spec matches";
            if (definition == null)
                return null;

            SmartSpecDefinition best = null;
            long bestPoints = 0, total = 0;
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (SmartSpecDefinition spec in definition.Specs)
            {
                string key = spec.Match?.PrimaryLineKey;
                if (string.IsNullOrEmpty(key) || !seen.Add(key))
                    continue;
                long points = PointsInvested(specLevel?.Invoke(key) ?? 0);
                total += points;
                if (points > bestPoints)
                {
                    bestPoints = points;
                    best = spec;
                }
                else if (points == bestPoints)
                    best = null; // tied at the top: the points don't decide
            }
            double ratio = total > 0 ? (double)bestPoints / total : 0;
            if (best != null && ratio >= best.Match.MinPointRatio)
            {
                how = $"trained points ({best.Match.PrimaryLine} holds {ratio:P0} of the points, at least {best.Match.MinPointRatio:P0})";
                return best;
            }

            if (!string.IsNullOrEmpty(specTypeName))
            {
                foreach (SmartSpecDefinition spec in definition.Specs)
                {
                    if (spec.Match?.SpecTypeAliases.Any(a => a.Equals(specTypeName, StringComparison.OrdinalIgnoreCase)) == true)
                    {
                        how = $"rolled spec type {specTypeName}";
                        return spec;
                    }
                }
            }

            if (best != null)
            {
                how = $"most-trained line ({best.Match.PrimaryLine} holds {ratio:P0} of the points, below its {best.Match.MinPointRatio:P0})";
                return best;
            }
            how = total == 0 ? "no primary line trained past 1" : "a tie between the most-trained lines";
            return null;
        }

        public static long PointsInvested(int level) => level <= 1 ? 0 : (long)level * (level + 1) / 2 - 1;

        [GameServerStartedEvent]
        public static void OnServerStarted(DOLEvent e, object sender, EventArgs args)
        {
            if (!SmartAiProperties.USE_SMART_AI)
                return;
            try
            {
                LogReport(Reload(DefaultDirectory, SmartAiServerGameData.Instance, force: true));
            }
            catch (Exception ex)
            {
                SmartAiFallback.ReportFailure("loading the class files", ex);
                if (Log.IsErrorEnabled)
                    Log.Error("SmartAI class files could not be loaded; every bot uses the legacy bot AI.", ex);
            }
        }

        public static void LogReport(LoadReport report)
        {
            if (Log.IsInfoEnabled)
                Log.Info($"SmartAI class files ({DefaultDirectory}): {report.Summary}");
            foreach (SmartAiIssue issue in report.Issues)
            {
                if (issue.Severity == SmartAiIssueSeverity.Error && Log.IsErrorEnabled)
                    Log.Error($"SmartAI Spec Validator: {issue}");
                else if (issue.Severity == SmartAiIssueSeverity.Warning && Log.IsWarnEnabled)
                    Log.Warn($"SmartAI Spec Validator: {issue}");
            }
        }
    }
}
