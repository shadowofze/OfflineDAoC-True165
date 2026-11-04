using System.Threading;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// One bot's SmartAI circuit breaker (architecture section 2.2, tier 2). After a SmartAI error the bot uses the legacy
    /// BotBrain for OpenMs, then SmartAI tries it again. Times are GameLoop.GameLoopTime milliseconds, passed in so the
    /// breaker is testable. Thread-safe: a bot thinks on one worker at a time, but the reads stay atomic anyway.
    /// </summary>
    public sealed class SmartAiCircuitBreaker
    {
        public const long OpenMs = 10_000;

        private long _openUntil;
        private int _trips;

        /// <summary>True while the bot must use the legacy brain.</summary>
        public bool IsOpen(long nowMs) => nowMs < Interlocked.Read(ref _openUntil);

        /// <summary>How many times this bot's SmartAI has failed (diagnostics).</summary>
        public int Trips => Volatile.Read(ref _trips);

        public void Trip(long nowMs)
        {
            Interlocked.Exchange(ref _openUntil, nowMs + OpenMs);
            Interlocked.Increment(ref _trips);
        }
    }
}
