using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using DOL.Events;
using DOL.Logging;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// What SmartAI groups learn from playing (owner 2026-11-05: "have the bot AI keep learning and adjusting its logic
    /// in config files for best results"; owner chose: apply automatically, within limits). Saved to
    /// state-165/smartai-learning.json, so it survives restarts; delete the file to start over.
    /// - Per camp: kills, deaths at the camp and deaths on the way to it, by grouped bots.
    /// - Per area (a 2,048-unit square of a region): deaths of grouped bots travelling there.
    /// - Old results fade: every count loses half its weight in LearningHalfLifeDays (smartai-world.json).
    /// - Use (SmartCampChooser): a camp's weight is scaled by how deadly it has been per kill (0.2x to 1.5x), by deaths
    ///   on its road (down to 0.4x) and by deaths in the areas around it (down to 0.5x). Nothing in the world changes;
    ///   all the factors are improvised.
    /// Only recorded and used with SmartAI on.
    /// </summary>
    public static class SmartWorldLearning
    {
        private static readonly Logger Log = LoggerManager.Create(typeof(SmartWorldLearning));
        public const string FileName = "smartai-learning.json";
        public const int AreaSize = 2048;
        /// <summary>A death this close to the camp spot counts as at the camp.</summary>
        public const int AtCampRadius = 2500;
        /// <summary>The usual deaths per kill: a camp with no history scores 1.0.</summary>
        public const double BaselineDeathsPerKill = 0.15;
        private const double PriorKills = 5;

        public sealed class CampStats
        {
            public double Kills { get; set; }
            public double Deaths { get; set; }
            public double TravelDeaths { get; set; }
            public DateTime Updated { get; set; }
        }

        public sealed class AreaStats
        {
            public double Deaths { get; set; }
            public DateTime Updated { get; set; }
        }

        public sealed class Document
        {
            public string About { get; set; } =
                "Learned by SmartAI groups: kills and deaths per camp, deaths per area (region:x:y in 2048-unit squares). " +
                "Values fade with the half-life in smartai-world.json. Delete this file to forget everything.";
            public Dictionary<string, CampStats> Camps { get; set; } = new(StringComparer.OrdinalIgnoreCase);
            public Dictionary<string, AreaStats> Areas { get; set; } = new(StringComparer.Ordinal);
        }

        private static readonly JsonSerializerOptions Json = new() { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private static readonly object Sync = new();
        private static Document _doc = new();
        private static bool _dirty;
        private static Timer _timer;
        private static string _path;

        public static string FilePath => _path ??= Classic165Mode.StatePath(FileName);

        public static void UseFileForTests(string path)
        {
            lock (Sync)
            {
                _path = path;
                _doc = new Document();
                _dirty = false;
            }
        }

        private static double HalfLifeDays => Math.Max(0.5, SmartWorldConfig.Current.LearningHalfLifeDays);

        /// <summary>Pure: a count that was <paramref name="value"/> at <paramref name="then"/>, faded to <paramref name="now"/>.</summary>
        public static double Fade(double value, DateTime then, DateTime now, double halfLifeDays)
        {
            if (value <= 0 || then == default || now <= then)
                return Math.Max(0, value);
            return value * Math.Pow(0.5, (now - then).TotalDays / Math.Max(0.01, halfLifeDays));
        }

        private static void Touch(CampStats s, DateTime now)
        {
            double h = HalfLifeDays;
            s.Kills = Fade(s.Kills, s.Updated, now, h);
            s.Deaths = Fade(s.Deaths, s.Updated, now, h);
            s.TravelDeaths = Fade(s.TravelDeaths, s.Updated, now, h);
            s.Updated = now;
        }

        public static string AreaKey(ushort region, int x, int y) =>
            $"{region}:{(int)Math.Floor(x / (double)AreaSize)}:{(int)Math.Floor(y / (double)AreaSize)}";

        public static void RecordKill(string campId, DateTime? nowUtc = null)
        {
            if (string.IsNullOrWhiteSpace(campId))
                return;
            DateTime now = nowUtc ?? DateTime.UtcNow;
            lock (Sync)
            {
                CampStats s = Camp(campId);
                Touch(s, now);
                s.Kills++;
                _dirty = true;
            }
        }

        /// <summary>A grouped bot died: at its camp, or on the way (which also marks the area).</summary>
        public static void RecordDeath(string campId, bool atCamp, ushort region, int x, int y, DateTime? nowUtc = null)
        {
            DateTime now = nowUtc ?? DateTime.UtcNow;
            lock (Sync)
            {
                if (!string.IsNullOrWhiteSpace(campId))
                {
                    CampStats s = Camp(campId);
                    Touch(s, now);
                    if (atCamp) s.Deaths++; else s.TravelDeaths++;
                }
                if (!atCamp && region > 0)
                {
                    string key = AreaKey(region, x, y);
                    if (!_doc.Areas.TryGetValue(key, out AreaStats a))
                        _doc.Areas[key] = a = new AreaStats();
                    a.Deaths = Fade(a.Deaths, a.Updated, now, HalfLifeDays) + 1;
                    a.Updated = now;
                }
                _dirty = true;
            }
        }

        private static CampStats Camp(string id)
        {
            if (!_doc.Camps.TryGetValue(id, out CampStats s))
                _doc.Camps[id] = s = new CampStats();
            return s;
        }

        /// <summary>Pure: the camp factor from faded counts (1.0 with no history).</summary>
        public static double CampFactor(double kills, double deaths, double travelDeaths)
        {
            double rate = (deaths + BaselineDeathsPerKill * PriorKills) / (kills + PriorKills);
            double atCamp = Math.Clamp(BaselineDeathsPerKill / Math.Max(0.0001, rate), 0.2, 1.5);
            double road = Math.Max(0.4, 1 / (1 + 0.25 * travelDeaths));
            return atCamp * road;
        }

        /// <summary>Pure: the area factor from faded deaths in the nine squares around a camp.</summary>
        public static double AreaFactor(double deaths) => Math.Max(0.5, 1 / (1 + 0.2 * deaths));

        /// <summary>This camp's learned factor now, with a short reason for the logs.</summary>
        public static double FactorFor(string campId, ushort region, int x, int y, out string why, DateTime? nowUtc = null)
        {
            DateTime now = nowUtc ?? DateTime.UtcNow;
            double h = HalfLifeDays;
            lock (Sync)
            {
                double kills = 0, deaths = 0, travel = 0;
                if (campId != null && _doc.Camps.TryGetValue(campId, out CampStats s))
                {
                    kills = Fade(s.Kills, s.Updated, now, h);
                    deaths = Fade(s.Deaths, s.Updated, now, h);
                    travel = Fade(s.TravelDeaths, s.Updated, now, h);
                }
                double area = 0;
                int ax = (int)Math.Floor(x / (double)AreaSize), ay = (int)Math.Floor(y / (double)AreaSize);
                for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (_doc.Areas.TryGetValue($"{region}:{ax + dx}:{ay + dy}", out AreaStats a))
                        area += Fade(a.Deaths, a.Updated, now, h);
                double factor = CampFactor(kills, deaths, travel) * AreaFactor(area);
                why = kills + deaths + travel + area < 0.05
                    ? "no history"
                    : $"learned {kills:0.#} kills, {deaths:0.#} deaths, {travel:0.#} on the road, {area:0.#} nearby";
                return factor;
            }
        }

        // ---- file

        [GameServerStartedEvent]
        public static void OnStarted(DOLEvent e, object sender, EventArgs args)
        {
            Load();
            _timer?.Dispose();
            _timer = new Timer(_ => Save(), null, 120_000, 120_000);
        }

        [GameServerStoppedEvent]
        public static void OnStopped(DOLEvent e, object sender, EventArgs args)
        {
            _timer?.Dispose();
            _timer = null;
            Save();
        }

        public static void Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return;
                Document doc = JsonSerializer.Deserialize<Document>(File.ReadAllText(FilePath), Json) ?? new Document();
                lock (Sync)
                {
                    _doc = new Document
                    {
                        Camps = new Dictionary<string, CampStats>(doc.Camps ?? new(), StringComparer.OrdinalIgnoreCase),
                        Areas = new Dictionary<string, AreaStats>(doc.Areas ?? new(), StringComparer.Ordinal),
                    };
                    _dirty = false;
                }
            }
            catch (Exception e)
            {
                Log.Warn($"SmartAI learning file {FilePath} could not be read; starting with no history (the file is left as it is)", e);
                lock (Sync)
                {
                    _doc = new Document();
                    // Don't overwrite a file that may only be mid-edit until something new is learned.
                    _dirty = false;
                }
            }
        }

        public static void Save()
        {
            string text;
            lock (Sync)
            {
                if (!_dirty)
                    return;
                Prune(DateTime.UtcNow);
                text = JsonSerializer.Serialize(_doc, Json);
                _dirty = false;
            }
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
                string temporary = FilePath + ".tmp";
                File.WriteAllText(temporary, text);
                File.Move(temporary, FilePath, true);
            }
            catch (Exception e)
            {
                Log.Warn($"SmartAI learning file {FilePath} could not be written", e);
                lock (Sync) _dirty = true;
            }
        }

        /// <summary>Drops what has faded to almost nothing, so the file does not grow without end.</summary>
        private static void Prune(DateTime now)
        {
            double h = HalfLifeDays;
            foreach (string id in _doc.Camps.Where(p =>
                         Fade(p.Value.Kills + p.Value.Deaths + p.Value.TravelDeaths, p.Value.Updated, now, h) < 0.05)
                         .Select(p => p.Key).ToArray())
                _doc.Camps.Remove(id);
            foreach (string id in _doc.Areas.Where(p => Fade(p.Value.Deaths, p.Value.Updated, now, h) < 0.05)
                         .Select(p => p.Key).ToArray())
                _doc.Areas.Remove(id);
        }
    }
}
