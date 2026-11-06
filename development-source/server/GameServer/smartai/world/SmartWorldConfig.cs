using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using DOL.Logging;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// state-165/smartai-world.json (owner 2026-11-05): the zone and dungeon level ranges SmartAI groups narrow their
    /// camp search with, and the travel/camp tunables. Three sections:
    /// - "settings": tunables, written once with the defaults; the owner's edits are kept.
    /// - "overrides": per zone name, the owner's own level range or "skip": true. Never written by the server.
    /// - "generated": per zone, the live monster levels, rewritten each time the camp catalog is rebuilt.
    /// The server re-reads the file when it changes, so edits apply without a restart. A file that does not parse is
    /// left alone (the defaults are used) and a warning is logged.
    /// </summary>
    public static class SmartWorldConfig
    {
        private static readonly Logger Log = LoggerManager.Create(typeof(SmartWorldConfig));
        public const string FileName = "smartai-world.json";

        public sealed class Settings
        {
            /// <summary>The area search: monsters this far from a camp are looked at (units).</summary>
            public int CampSearchRadius { get; set; } = 2500;
            /// <summary>Extra clearance beyond a monster's aggro range that counts as "inside" it (units).</summary>
            public int AggroMargin { get; set; } = 350;
            /// <summary>A travelling group leader keeps this much more room, for the members walking beside it.</summary>
            public int FormationClearance { get; set; } = 250;
            /// <summary>A member farther than this from its leader is travelling alone and looks out for itself.</summary>
            public int LoneMemberDistance { get; set; } = 1500;
            /// <summary>Learned results lose half their weight in this many days.</summary>
            public double LearningHalfLifeDays { get; set; } = 7;
            /// <summary>A zone is left out when its typical monster is more than this many levels above what the group
            /// may pull (the group would walk among higher monsters to reach its few fitting camps).</summary>
            public int ZoneTypicalAboveBand { get; set; } = 2;
            /// <summary>Crowding: other parties' camps within this radius of a camp count against it (units).</summary>
            public int GroupCrowdRadius { get; set; } = 3000;
            /// <summary>A camp with this many other parties' camps within GroupCrowdRadius is left out (while any
            /// camp with fewer is left).</summary>
            public int MaxOtherGroupsNearCamp { get; set; } = 2;
            /// <summary>A party standing at its camp this long, every member alive and ready, with nothing pulled, has
            /// stalled: it learns that and moves to another camp (seconds; 0 turns the check off).</summary>
            public int CampStallSeconds { get; set; } = 120;
        }

        public sealed class ZoneOverride
        {
            public int? Min { get; set; }
            public int? Max { get; set; }
            public bool Skip { get; set; }
            public string Note { get; set; }
        }

        public sealed class ZoneGenerated
        {
            public ushort Region { get; set; }
            public bool Dungeon { get; set; }
            public int Min { get; set; }
            /// <summary>10th percentile of the live monster levels.</summary>
            public int Low { get; set; }
            /// <summary>Median.</summary>
            public int Typical { get; set; }
            /// <summary>90th percentile.</summary>
            public int High { get; set; }
            public int Max { get; set; }
            public int Monsters { get; set; }
        }

        public sealed class Document
        {
            public string About { get; set; } =
                "SmartAI world config. Edit 'settings' and 'overrides' (per zone: min, max, skip, note); 'generated' is " +
                "rewritten from the live monsters. Delete this file to get the defaults back.";
            public Settings Settings { get; set; } = new();
            public SortedDictionary<string, ZoneOverride> Overrides { get; set; } = new(StringComparer.OrdinalIgnoreCase);
            public SortedDictionary<string, ZoneGenerated> Generated { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        }

        private static readonly JsonSerializerOptions Json = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
        };

        private static readonly object Sync = new();
        private static Document _current = new();
        private static DateTime _loadedStamp;
        private static string _path;
        private static long _nextCheck;

        public static string FilePath => _path ??= Classic165Mode.StatePath(FileName);

        /// <summary>For tests: a file elsewhere (null back to the state folder).</summary>
        public static void UseFileForTests(string path)
        {
            lock (Sync)
            {
                _path = path;
                _current = new();
                _loadedStamp = default;
                _nextCheck = 0;
            }
        }

        public static Settings Current
        {
            get
            {
                Reload();
                return _current.Settings ?? new Settings();
            }
        }

        /// <summary>The owner's override for a zone, or null.</summary>
        public static ZoneOverride OverrideFor(string zone)
        {
            Reload();
            return zone != null && _current.Overrides != null && _current.Overrides.TryGetValue(zone, out ZoneOverride o) ? o : null;
        }

        public static ZoneGenerated GeneratedFor(string zone)
        {
            Reload();
            return zone != null && _current.Generated != null && _current.Generated.TryGetValue(zone, out ZoneGenerated g) ? g : null;
        }

        /// <summary>Re-reads the file when it changed on disk.</summary>
        public static void Reload()
        {
            long now = Environment.TickCount64;
            if (now < System.Threading.Volatile.Read(ref _nextCheck))
                return;
            System.Threading.Volatile.Write(ref _nextCheck, now + 5_000);
            try
            {
                string path = FilePath;
                if (!File.Exists(path))
                    return;
                DateTime stamp = File.GetLastWriteTimeUtc(path);
                if (stamp == _loadedStamp)
                    return;
                lock (Sync)
                {
                    if (stamp == _loadedStamp)
                        return;
                    Document doc = JsonSerializer.Deserialize<Document>(File.ReadAllText(path), Json) ?? new Document();
                    Normalise(doc);
                    _current = doc;
                    _loadedStamp = stamp;
                }
            }
            catch (Exception e)
            {
                // Keep what was loaded; never overwrite a file the owner is editing.
                _loadedStamp = File.Exists(FilePath) ? File.GetLastWriteTimeUtc(FilePath) : default;
                Log.Warn($"SmartAI world config {FilePath} could not be read; keeping the previous settings", e);
            }
        }

        private static void Normalise(Document doc)
        {
            doc.Settings ??= new Settings();
            doc.Overrides = new SortedDictionary<string, ZoneOverride>(doc.Overrides ?? new(), StringComparer.OrdinalIgnoreCase);
            doc.Generated = new SortedDictionary<string, ZoneGenerated>(doc.Generated ?? new(), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>Rewrites the generated section, keeping the owner's settings and overrides. Skipped when the file
        /// exists but does not parse (the owner is mid-edit).</summary>
        public static void WriteGenerated(IReadOnlyDictionary<string, ZoneGenerated> zones)
        {
            try
            {
                lock (Sync)
                {
                    string path = FilePath;
                    Document doc;
                    if (File.Exists(path))
                    {
                        try { doc = JsonSerializer.Deserialize<Document>(File.ReadAllText(path), Json) ?? new Document(); }
                        catch (JsonException e)
                        {
                            Log.Warn($"SmartAI world config {path} does not parse; the generated zone levels were not written", e);
                            return;
                        }
                    }
                    else
                        doc = new Document();
                    Normalise(doc);
                    var generated = new SortedDictionary<string, ZoneGenerated>(zones.ToDictionary(p => p.Key, p => p.Value),
                        StringComparer.OrdinalIgnoreCase);
                    string before = JsonSerializer.Serialize(doc.Generated, Json);
                    string after = JsonSerializer.Serialize(generated, Json);
                    if (before == after && File.Exists(path))
                        return;
                    doc.Generated = generated;
                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                    string temporary = path + ".tmp";
                    File.WriteAllText(temporary, JsonSerializer.Serialize(doc, Json));
                    File.Move(temporary, path, true);
                    _current = doc;
                    _loadedStamp = File.GetLastWriteTimeUtc(path);
                }
            }
            catch (Exception e)
            {
                Log.Warn("SmartAI world config could not be written", e);
            }
        }
    }
}
