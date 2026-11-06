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
            /// <summary>Times a party stalled here: a pull it could not make, or standing ready with nothing pulled.</summary>
            public double Stalls { get; set; }
            public DateTime Updated { get; set; }
        }

        public sealed class AreaStats
        {
            public double Deaths { get; set; }
            /// <summary>The victims' levels, summed and faded like Deaths (their average is LevelSum / Deaths).</summary>
            public double LevelSum { get; set; }
            public DateTime Updated { get; set; }
        }

        /// <summary>
        /// Pure: how much an area's learned deaths count for a traveller of <paramref name="travellerLevel"/>: fully,
        /// unless the victims averaged 5 or more levels below it, then a fifth (a level 40 walks where level 5s die).
        /// Areas learned before victim levels were kept count fully.
        /// </summary>
        public static double DeathsFor(double deaths, double levelSum, int travellerLevel)
        {
            if (deaths <= 0)
                return 0;
            if (levelSum <= 0)
                return deaths;
            double victims = levelSum / deaths;
            return victims + 5 <= travellerLevel ? deaths * 0.2 : deaths;
        }

        /// <summary>Faded deaths learned in the area square holding (x, y), as they count for a traveller of that level.</summary>
        public static double AreaDanger(ushort region, int x, int y, int travellerLevel, DateTime? nowUtc = null)
        {
            DateTime now = nowUtc ?? DateTime.UtcNow;
            lock (Sync)
            {
                if (!_doc.Areas.TryGetValue(AreaKey(region, x, y), out AreaStats a))
                    return 0;
                double h = HalfLifeDays;
                return DeathsFor(Fade(a.Deaths, a.Updated, now, h), Fade(a.LevelSum, a.Updated, now, h), travellerLevel);
            }
        }

        public sealed class Document
        {
            public string About { get; set; } =
                "Learned by SmartAI bots: kills and deaths per camp, deaths per area (region:x:y in 2048-unit squares), and " +
                "solo kills and deaths per class and spec. Values fade with the half-life in smartai-world.json. Delete this " +
                "file to forget everything.";
            public Dictionary<string, CampStats> Camps { get; set; } = new(StringComparer.OrdinalIgnoreCase);
            public Dictionary<string, AreaStats> Areas { get; set; } = new(StringComparer.Ordinal);
            public Dictionary<string, SoloStats> SoloSpecs { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>One class and spec fighting alone ("Bard / Regrowth Bard").</summary>
        public sealed class SoloStats
        {
            public double Kills { get; set; }
            public double Deaths { get; set; }
            public DateTime Updated { get; set; }
        }

        // ---- solo con ceiling (owner 2026-11-05: "look at solo casters and regrowth bards"). Run 5: Regrowth Bards died in
        // 21% of their solo fights, Mentalists 22-26%, Ice Theurgists 24%, Nurture Druids 9%. A spec that dies too often
        // alone fights a lower con alone: improvised thresholds, nothing in a fight changes.

        /// <summary>Solo deaths per kill from which a spec fights one con lower alone (yellow to blue).</summary>
        public const double SoloOneStepRate = 0.15;
        /// <summary>... and two cons lower (to green).</summary>
        public const double SoloTwoStepRate = 0.40;
        /// <summary>Kills and deaths (faded) before the rate counts.</summary>
        public const double SoloMinimumEvidence = 20;

        /// <summary>Pure: how many con steps lower a spec fights alone, from its faded solo kills and deaths.</summary>
        public static int SoloConSteps(double kills, double deaths)
        {
            if (kills + deaths < SoloMinimumEvidence)
                return 0;
            double rate = deaths / Math.Max(1, kills);
            return rate >= SoloTwoStepRate ? 2 : rate >= SoloOneStepRate ? 1 : 0;
        }

        public static void RecordSolo(string spec, bool died, DateTime? nowUtc = null)
        {
            if (string.IsNullOrWhiteSpace(spec))
                return;
            DateTime now = nowUtc ?? DateTime.UtcNow;
            lock (Sync)
            {
                if (!_doc.SoloSpecs.TryGetValue(spec, out SoloStats s))
                    _doc.SoloSpecs[spec] = s = new SoloStats();
                double h = HalfLifeDays;
                s.Kills = Fade(s.Kills, s.Updated, now, h);
                s.Deaths = Fade(s.Deaths, s.Updated, now, h);
                s.Updated = now;
                if (died) s.Deaths++; else s.Kills++;
                _dirty = true;
            }
        }

        /// <summary>The spec's solo con steps now, with its faded kills and deaths for the logs.</summary>
        public static int SoloConStepsFor(string spec, out double kills, out double deaths, DateTime? nowUtc = null)
        {
            kills = deaths = 0;
            if (string.IsNullOrWhiteSpace(spec))
                return 0;
            DateTime now = nowUtc ?? DateTime.UtcNow;
            lock (Sync)
            {
                if (!_doc.SoloSpecs.TryGetValue(spec, out SoloStats s))
                    return 0;
                double h = HalfLifeDays;
                kills = Fade(s.Kills, s.Updated, now, h);
                deaths = Fade(s.Deaths, s.Updated, now, h);
                return SoloConSteps(kills, deaths);
            }
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
            s.Stalls = Fade(s.Stalls, s.Updated, now, h);
            s.Updated = now;
        }

        /// <summary>A party stalled at this camp (owner 2026-11-05: Hibernian parties froze at water camps).</summary>
        public static void RecordStall(string campId, DateTime? nowUtc = null)
        {
            if (string.IsNullOrWhiteSpace(campId))
                return;
            DateTime now = nowUtc ?? DateTime.UtcNow;
            lock (Sync)
            {
                CampStats s = Camp(campId);
                Touch(s, now);
                s.Stalls++;
                _dirty = true;
            }
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
        public static void RecordDeath(string campId, bool atCamp, ushort region, int x, int y, DateTime? nowUtc = null, int victimLevel = 0)
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
                    a.LevelSum = Fade(a.LevelSum, a.Updated, now, HalfLifeDays) + Math.Max(0, victimLevel);
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

        /// <summary>Pure: the camp factor from faded counts (1.0 with no history). Each stall costs a lot (one: 0.5x,
        /// three: 0.25x, the floor), since a stalled party earns nothing for minutes.</summary>
        public static double CampFactor(double kills, double deaths, double travelDeaths, double stalls = 0)
        {
            double rate = (deaths + BaselineDeathsPerKill * PriorKills) / (kills + PriorKills);
            double atCamp = Math.Clamp(BaselineDeathsPerKill / Math.Max(0.0001, rate), 0.2, 1.5);
            double road = Math.Max(0.4, 1 / (1 + 0.25 * travelDeaths));
            double stalled = Math.Max(0.25, 1 / (1 + Math.Max(0, stalls)));
            return atCamp * road * stalled;
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
                double kills = 0, deaths = 0, travel = 0, stalls = 0;
                if (campId != null && _doc.Camps.TryGetValue(campId, out CampStats s))
                {
                    kills = Fade(s.Kills, s.Updated, now, h);
                    deaths = Fade(s.Deaths, s.Updated, now, h);
                    travel = Fade(s.TravelDeaths, s.Updated, now, h);
                    stalls = Fade(s.Stalls, s.Updated, now, h);
                }
                double area = 0;
                int ax = (int)Math.Floor(x / (double)AreaSize), ay = (int)Math.Floor(y / (double)AreaSize);
                for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (_doc.Areas.TryGetValue($"{region}:{ax + dx}:{ay + dy}", out AreaStats a))
                        area += Fade(a.Deaths, a.Updated, now, h);
                double factor = CampFactor(kills, deaths, travel, stalls) * AreaFactor(area);
                why = kills + deaths + travel + stalls + area < 0.05
                    ? "no history"
                    : $"learned {kills:0.#} kills, {deaths:0.#} deaths, {travel:0.#} on the road, {area:0.#} nearby" +
                      (stalls >= 0.05 ? $", {stalls:0.#} stalls" : string.Empty);
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
                        SoloSpecs = new Dictionary<string, SoloStats>(doc.SoloSpecs ?? new(), StringComparer.OrdinalIgnoreCase),
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
                         Fade(p.Value.Kills + p.Value.Deaths + p.Value.TravelDeaths + p.Value.Stalls, p.Value.Updated, now, h) < 0.05)
                         .Select(p => p.Key).ToArray())
                _doc.Camps.Remove(id);
            foreach (string id in _doc.Areas.Where(p => Fade(p.Value.Deaths, p.Value.Updated, now, h) < 0.05)
                         .Select(p => p.Key).ToArray())
                _doc.Areas.Remove(id);
            foreach (string id in _doc.SoloSpecs.Where(p => Fade(p.Value.Kills + p.Value.Deaths, p.Value.Updated, now, h) < 0.05)
                         .Select(p => p.Key).ToArray())
                _doc.SoloSpecs.Remove(id);
        }
    }
}
