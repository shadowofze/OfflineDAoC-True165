using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using DOL.Logging;

namespace DOL.GS
{
    public class GameLoopThreadPoolWatchdog
    {
        private static readonly Logger log = LoggerManager.Create(MethodBase.GetCurrentMethod().DeclaringType);

        public const long IDLE_CYCLE = -1;             // Represents an idle worker thread cycle, used to detect stuck threads.
        private const int WORKER_TIMEOUT_MS = 7500;    // Slow-worker diagnostic threshold, not permission to interrupt native work.

        private bool _running;
        private Thread _watchdogThread;
        private Thread[] _workers;
        private long[] _workerCycle;
        private Action<List<int>> _restartWorkersAction;
        private Func<int, string> _describeWorker;

        // Run 7 (2026-11-05): two workers hung for good and the warning named only the threads. The warning now says
        // which step and item each one is on, and repeats every StuckRepeatMs (up to StuckRepeats times) while it
        // stays stuck: the same item each time means it is stuck on that item, a changing one that it is slow.
        public const long StuckRepeatMs = 30_000;
        public const int StuckRepeats = 10;

        public GameLoopThreadPoolWatchdog(Thread[] workers, long[] workerCycle, Action<List<int>> restartWorkersAction,
            Func<int, string> describeWorker = null)
        {
            _workers = workers ?? throw new ArgumentNullException(nameof(workers));
            _workerCycle = workerCycle ?? throw new ArgumentNullException(nameof(workerCycle));
            _restartWorkersAction = restartWorkersAction ?? throw new ArgumentNullException(nameof(restartWorkersAction));
            _describeWorker = describeWorker;
        }

        private string Describe(int worker)
        {
            try { return _describeWorker?.Invoke(worker) ?? "unknown"; }
            catch (Exception e) { return $"unknown ({e.GetType().Name})"; }
        }

        /// <summary>Pure: repeat the stuck warning now (stuck since <paramref name="since"/>, last said at <paramref name="said"/>).</summary>
        public static bool RepeatStuckWarning(long since, long said, int repeats, long now) =>
            repeats < StuckRepeats && now - said >= StuckRepeatMs && now >= since;

        public void Start()
        {
            if (_running)
                return;

            _running = true;

            _watchdogThread = new(WatchdogLoop)
            {
                Name = $"{GameLoop.THREAD_NAME}_Watchdog",
                Priority = ThreadPriority.Lowest,
                IsBackground = true,
            };
            _watchdogThread.Start();
        }

        public void Stop()
        {
            if (!_running)
                return;

            _running = false;

            if (_watchdogThread != Thread.CurrentThread && _watchdogThread.IsAlive)
                _watchdogThread.Join();

            _watchdogThread = null;
        }

        private void WatchdogLoop()
        {
            List<int> _workersToRestart = new();
            long[] lastReportedCycle = new long[_workers.Length];
            Array.Fill(lastReportedCycle, IDLE_CYCLE);
            long[] stuckSince = new long[_workers.Length], stuckSaid = new long[_workers.Length];
            int[] stuckRepeats = new int[_workers.Length];

            while (Volatile.Read(ref _running))
            {
                try
                {
                    for (int i = 0; i < _workers.Length; i++)
                    {
                        Thread worker = _workers[i];

                        // Thread is null if it was removed by the watchdog. At this point it's already being replaced, hopefully.
                        if (worker == null)
                            continue;

                        if (worker.Join(100))
                        {
                            if (log.IsWarnEnabled)
                                log.Warn($"Watchdog: Thread \"{worker.Name}\" has exited unexpectedly. Restarting...");

                            _workersToRestart.Add(i);
                        }
                        else
                        {
                            long cycle = Volatile.Read(ref _workerCycle[i]);

                            if (cycle > IDLE_CYCLE)
                            {
                                // A slow inventory/native operation may recover normally.
                                // Thread.Interrupt cannot safely cancel an arbitrary actor
                                // action; it can instead land in the following idle wait.
                                if (worker.Join(WORKER_TIMEOUT_MS))
                                {
                                    if (log.IsWarnEnabled)
                                        log.Warn($"Watchdog: Thread \"{worker.Name}\" has exited unexpectedly. Restarting...");

                                    _workersToRestart.Add(i);
                                }
                                else if (Volatile.Read(ref _workerCycle[i]) == cycle)
                                {
                                    long now = Environment.TickCount64;
                                    if (lastReportedCycle[i] != cycle)
                                    {
                                        stuckSince[i] = stuckSaid[i] = now;
                                        stuckRepeats[i] = 0;
                                        if (log.IsWarnEnabled)
                                            log.Warn($"Watchdog: Thread \"{worker.Name}\" is taking too long; preserving its in-flight work and waiting for completion. " +
                                                     $"Working on: {Describe(i)}. (If the server stays frozen, Task Manager > right-click the server > Create dump file records every thread.)");
                                    }
                                    else if (RepeatStuckWarning(stuckSince[i], stuckSaid[i], stuckRepeats[i], now))
                                    {
                                        stuckSaid[i] = now;
                                        stuckRepeats[i]++;
                                        if (log.IsWarnEnabled)
                                            log.Warn($"Watchdog: Thread \"{worker.Name}\" still busy after {(now - stuckSince[i]) / 1000 + WORKER_TIMEOUT_MS / 1000}s. Working on: {Describe(i)}");
                                    }
                                    lastReportedCycle[i] = cycle;
                                }
                            }
                        }
                    }

                    if (_workersToRestart.Count == 0)
                        continue;

                    _restartWorkersAction(_workersToRestart);
                    _workersToRestart.Clear();
                }
                catch (ThreadInterruptedException)
                {
                    if (log.IsWarnEnabled)
                        log.Warn($"Thread \"{Thread.CurrentThread.Name}\" was interrupted");
                }
                catch (Exception e)
                {
                    if (log.IsErrorEnabled)
                        log.Error($"Thread \"{Thread.CurrentThread.Name}\"", e);
                }
            }

            if (log.IsInfoEnabled)
                log.Info($"Thread \"{Thread.CurrentThread.Name}\" is stopping");
        }
    }
}
