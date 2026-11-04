using System;
using System.Collections.Concurrent;
using System.Reflection;
using DOL.Database;
using DOL.Events;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// Bot personalities (plan Phase 10, architecture section 5.2).
    /// - Saved overrides: offline_bot_smartai_profiles, loaded at server start, cached in memory, written when a player
    ///   (or GM) sets one.
    /// - Without one, "Auto": a hired companion takes its spec's default personality; an autonomous gamebot keeps its
    ///   spec's default 60% of the time and otherwise gets a fixed variation from its id (SplitMix64), so world bots
    ///   differ but never change between sessions. A healer spec's variation is never Aggressive or Opportunistic.
    /// </summary>
    public static class SmartPersonalityRegistry
    {
        private static readonly Logging.Logger Log = Logging.LoggerManager.Create(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ConcurrentDictionary<long, OfflineBotSmartAiProfileRecord> Records = new();

        private static readonly SmartPersonality[] Variations =
        [
            SmartPersonality.Aggressive, SmartPersonality.Defensive, SmartPersonality.Tactical,
            SmartPersonality.Opportunistic, SmartPersonality.Supportive, SmartPersonality.Conservative,
        ];
        private static readonly SmartPersonality[] HealerVariations =
        [
            SmartPersonality.Defensive, SmartPersonality.Tactical, SmartPersonality.Supportive, SmartPersonality.Conservative,
        ];

        [GameServerStartedEvent]
        public static void OnServerStarted(DOLEvent e, object sender, EventArgs args)
        {
            try
            {
                Records.Clear();
                foreach (OfflineBotSmartAiProfileRecord r in DOLDB<OfflineBotSmartAiProfileRecord>.SelectAllObjects())
                    Records[r.BotId] = r;
            }
            catch (Exception ex)
            {
                Log.Error("SmartAI personalities could not be loaded; bots use their spec defaults.", ex);
            }
        }

        /// <summary>The saved override, or Auto.</summary>
        public static SmartPersonality Saved(long botId) =>
            Records.TryGetValue(botId, out OfflineBotSmartAiProfileRecord r) && SmartPersonalityProfile.TryParse(r.Personality, out SmartPersonality p)
                ? p : SmartPersonality.Auto;

        /// <summary>Saves an override (Auto clears it back to the default). Returns false if it couldn't be saved.</summary>
        public static bool Set(long botId, SmartPersonality personality)
        {
            try
            {
                bool exists = Records.TryGetValue(botId, out OfflineBotSmartAiProfileRecord record);
                record ??= new OfflineBotSmartAiProfileRecord { BotId = botId };
                record.Personality = personality.ToString();
                record.UpdatedUtc = DateTime.UtcNow.ToString("O");
                bool ok = exists && record.IsPersisted ? GameServer.Database.SaveObject(record) : GameServer.Database.AddObject(record);
                if (ok)
                    Records[botId] = record;
                return ok;
            }
            catch (Exception ex)
            {
                Log.Error($"SmartAI personality of bot {botId} could not be saved.", ex);
                return false;
            }
        }

        /// <summary>The personality this bot fights with for this spec.</summary>
        public static SmartPersonality Resolve(GameBot bot, SmartSpecDefinition spec)
        {
            SmartPersonality saved = Saved(bot.DatabaseID);
            if (saved != SmartPersonality.Auto)
                return saved;
            SmartPersonality spec165 = SpecDefault(spec);
            return bot.IsAutonomousWorldBot && !HiredCompanions165.IsHired(bot)
                ? Variation(bot.DatabaseID, spec165, spec != null && spec.Role.Contains("Healer", StringComparison.OrdinalIgnoreCase))
                : spec165;
        }

        public static SmartPersonality SpecDefault(SmartSpecDefinition spec) =>
            spec != null && SmartPersonalityProfile.TryParse(spec.DefaultPersonality, out SmartPersonality p) ? p : SmartPersonality.Auto;

        /// <summary>An autonomous bot's fixed personality: the spec default 60% of the time, else a variation.</summary>
        public static SmartPersonality Variation(long botId, SmartPersonality specDefault, bool healer)
        {
            ulong h = SplitMix64((ulong)botId);
            if (h % 100 < 60)
                return specDefault;
            SmartPersonality[] pool = healer ? HealerVariations : Variations;
            return pool[(int)((h >> 32) % (ulong)pool.Length)];
        }

        public static ulong SplitMix64(ulong x)
        {
            x += 0x9E3779B97F4A7C15UL;
            x = (x ^ (x >> 30)) * 0xBF58476D1CE4E5B9UL;
            x = (x ^ (x >> 27)) * 0x94D049BB133111EBUL;
            return x ^ (x >> 31);
        }
    }
}
