using System;
using System.Reflection;
using DOL.Events;
using DOL.GS.ServerProperties;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI server properties (plan Phase 1). Properties.InitProperties finds every static [ServerProperty] field
    /// by reflection, so declaring them here registers them, creates their ServerProperty rows with the defaults on a
    /// clean database, and keeps Properties.CleanUpDatabase from removing the launcher's use_smart_ai row as rogue.
    /// </summary>
    public static class SmartAiProperties
    {
        /// <summary>Master switch, set by the launcher's "Use SmartAI" option. Off by default: the legacy bot AI.</summary>
        [ServerProperty(SmartAiSwitch.Category, SmartAiSwitch.UseSmartAiKey, SmartAiSwitch.UseSmartAiDescription, false)]
        public static bool USE_SMART_AI = false;

        /// <summary>Short /g callouts from SmartAI companions (rate limited; architecture section 9.2).</summary>
        [ServerProperty("autonomous", "smart_ai_group_callouts", "Enable concise /g chat callouts from SmartAI companions.", true)]
        public static bool SMART_AI_GROUP_CALLOUTS = true;

        /// <summary>The in-memory decision ring buffer for /smartai debug. Off by default.</summary>
        [ServerProperty("autonomous", "smart_ai_debug_logging", "Enable detailed SmartAI decision ring-buffer logging.", false)]
        public static bool SMART_AI_DEBUG_LOGGING = false;

        /// <summary>
        /// Players (not only GMs) may use the SmartAI tools: /smartai status, spec, group, debug and reload. They only read
        /// SmartAI's state (reload re-reads the class files), so they are no cheat; on for testing as a normal character,
        /// whom monsters fight (owner 2026-11-04).
        /// </summary>
        [ServerProperty("autonomous", "smart_ai_player_tools", "Let players (not only GMs) use the /smartai status, spec, group, debug and reload tools.", true)]
        public static bool SMART_AI_PLAYER_TOOLS = true;
    }

    /// <summary>
    /// The session-wide SmartAI fallback. SmartAI runs only while the switch is on and this fallback hasn't tripped.
    /// - SmartAI code reports each caught error with ReportFailure.
    /// - When errors keep coming (FailureLimit within FailureWindowMs), SmartAI turns itself off for the rest of this
    ///   server session, and every bot goes back to the legacy bot AI.
    /// - The saved setting is left alone: the player's choice in the launcher is never rewritten by the server.
    /// The per-bot circuit breaker of the architecture (section 2.2, 10 s) sits under this one, in the brain driver.
    /// </summary>
    public static class SmartAiFallback
    {
        private static readonly Logging.Logger Log = Logging.LoggerManager.Create(MethodBase.GetCurrentMethod().DeclaringType);

        public const int FailureLimit = 25;
        public const long FailureWindowMs = 60_000;

        private static readonly object Sync = new();
        private static readonly long[] RecentFailures = new long[FailureLimit];
        private static int _failureCount;
        private static int _nextSlot;
        private static volatile bool _tripped;
        private static string _tripReason = string.Empty;

        /// <summary>True when SmartAI should run: the switch is on and the session fallback hasn't tripped.</summary>
        public static bool IsActive => SmartAiProperties.USE_SMART_AI && !_tripped;

        /// <summary>True once SmartAI has turned itself off for this session.</summary>
        public static bool IsTripped => _tripped;

        public static string TripReason
        {
            get { lock (Sync) return _tripReason; }
        }

        /// <summary>Records one SmartAI error. Returns true when this error tripped the session fallback.</summary>
        public static bool ReportFailure(string context, Exception exception = null) =>
            ReportFailure(context, exception, Environment.TickCount64);

        /// <summary>As ReportFailure, at a given time (ms, Environment.TickCount64's clock); for tests.</summary>
        public static bool ReportFailure(string context, Exception exception, long nowMs)
        {
            if (_tripped)
                return false;

            lock (Sync)
            {
                if (_tripped)
                    return false;

                RecentFailures[_nextSlot] = nowMs;
                _nextSlot = (_nextSlot + 1) % FailureLimit;
                if (_failureCount < FailureLimit)
                    _failureCount++;

                // The ring is full and its oldest entry (the next slot) is inside the window: too many errors too fast.
                if (_failureCount < FailureLimit || nowMs - RecentFailures[_nextSlot] > FailureWindowMs)
                    return false;

                _tripReason = $"{FailureLimit} SmartAI errors within {FailureWindowMs / 1000} s (last: {context})";
                _tripped = true;
            }

            if (Log.IsErrorEnabled)
                Log.Error($"SmartAI turned off for this session, all bots use the legacy bot AI: {_tripReason}. " +
                          "The launcher setting is unchanged.", exception);
            return true;
        }

        /// <summary>Clears the fallback (a server start, or a test).</summary>
        public static void Reset()
        {
            lock (Sync)
            {
                Array.Clear(RecentFailures);
                _failureCount = 0;
                _nextSlot = 0;
                _tripReason = string.Empty;
                _tripped = false;
            }
        }

        [GameServerStartedEvent]
        public static void OnServerStarted(DOLEvent e, object sender, EventArgs args)
        {
            Reset();
            if (Log.IsInfoEnabled)
                Log.Info(SmartAiProperties.USE_SMART_AI
                    ? "SmartAI: on (Use SmartAI is set in the launcher)."
                    : "SmartAI: off, the legacy bot AI is used.");
        }
    }
}
