using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI's diagnostics (plan Phase 11, architecture section 8): in memory, no disk I/O on the combat path.
    /// - Each bot keeps its last 32 decisions in a ring (/smartai debug).
    /// - Every SmartAI combat think is timed: count, average and slowest (/smartai status).
    /// - smart_ai_debug_logging (off by default) also writes each decision to the server log.
    /// </summary>
    public static class SmartAiDiagnostics
    {
        private static readonly Logging.Logger Log = Logging.LoggerManager.Create(MethodBase.GetCurrentMethod().DeclaringType);

        public const int RingCapacity = 32;

        /// <summary>A bot's last decisions, oldest overwritten. One writer (the bot's think) at a time.</summary>
        public sealed class DecisionRing
        {
            private readonly SmartDecision[] _items = new SmartDecision[RingCapacity];
            private int _next;
            private int _count;
            private readonly Lock _lock = new();

            public void Add(SmartDecision decision)
            {
                lock (_lock)
                {
                    _items[_next] = decision;
                    _next = (_next + 1) % RingCapacity;
                    if (_count < RingCapacity)
                        _count++;
                }
            }

            public int Count
            {
                get { lock (_lock) return _count; }
            }

            /// <summary>Newest first, at most n.</summary>
            public SmartDecision[] Latest(int n)
            {
                lock (_lock)
                {
                    n = Math.Clamp(n, 0, _count);
                    var result = new SmartDecision[n];
                    for (int i = 0; i < n; i++)
                        result[i] = _items[(_next - 1 - i + RingCapacity * 2) % RingCapacity];
                    return result;
                }
            }

            public SmartDecision Last
            {
                get { lock (_lock) return _count == 0 ? null : _items[(_next - 1 + RingCapacity) % RingCapacity]; }
            }
        }

        private static readonly ConditionalWeakTable<GameBot, DecisionRing> Rings = new();

        private static long _thinks;
        private static long _ticksTotal;
        private static long _ticksMax;

        public static void Record(GameBot bot, SmartDecision decision)
        {
            if (bot == null || decision == null)
                return;
            Rings.GetValue(bot, static _ => new DecisionRing()).Add(decision);
            if (SmartAiProperties.SMART_AI_DEBUG_LOGGING && Log.IsInfoEnabled)
                Log.Info($"SMARTAI_DECISION bot=\"{bot.Name}\" spec=\"{decision.Spec}\" action={decision.Action} score={decision.Score} note=\"{decision.Note}\"");
        }

        public static bool TryGetRing(GameBot bot, out DecisionRing ring)
        {
            ring = null;
            return bot != null && Rings.TryGetValue(bot, out ring);
        }

        /// <summary>Times one SmartAI combat think (Stopwatch ticks).</summary>
        public static void RecordThink(long elapsedTicks)
        {
            Interlocked.Increment(ref _thinks);
            Interlocked.Add(ref _ticksTotal, elapsedTicks);
            long max;
            while (elapsedTicks > (max = Interlocked.Read(ref _ticksMax)) &&
                   Interlocked.CompareExchange(ref _ticksMax, elapsedTicks, max) != max)
            {
            }
        }

        public static long Thinks => Interlocked.Read(ref _thinks);

        public static double AverageMicroseconds
        {
            get
            {
                long n = Interlocked.Read(ref _thinks);
                return n == 0 ? 0 : Interlocked.Read(ref _ticksTotal) * 1_000_000.0 / Stopwatch.Frequency / n;
            }
        }

        public static double SlowestMicroseconds => Interlocked.Read(ref _ticksMax) * 1_000_000.0 / Stopwatch.Frequency;

        public static void ResetTimings()
        {
            Interlocked.Exchange(ref _thinks, 0);
            Interlocked.Exchange(ref _ticksTotal, 0);
            Interlocked.Exchange(ref _ticksMax, 0);
        }
    }
}
