using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// A log of every /smartai command and its answer, one file per server session (owner 2026-11-04: to hand the
    /// output over for review). Created at the first /smartai command of the session, in runtime/logs, as
    /// smartai-session-&lt;server start time&gt;.log. Only commands write to it, never the combat path. Never shipped:
    /// the release build leaves *.log out.
    /// </summary>
    public static class SmartAiSessionLog
    {
        private static readonly Logging.Logger Log = Logging.LoggerManager.Create(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly object Sync = new();
        private static string _path;
        private static bool _failed;

        public static string Directory => Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "logs"));

        /// <summary>This session's file (named by the server's start time).</summary>
        public static string FilePath
        {
            get
            {
                lock (Sync)
                    return _path ??= Path.Combine(Directory, $"smartai-session-{SessionStart():yyyyMMdd-HHmmss}.log");
            }
        }

        public static void Command(string player, string commandLine) =>
            Write($"{Environment.NewLine}[{DateTime.Now:HH:mm:ss}] {player}: {commandLine}");

        public static void Output(string text) => Write("    " + text);

        private static void Write(string line)
        {
            if (_failed)
                return;
            try
            {
                lock (Sync)
                {
                    string path = FilePath;
                    if (!File.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));
                        File.AppendAllText(path, Header(), Encoding.UTF8);
                    }
                    File.AppendAllText(path, line + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch (Exception e)
            {
                // A log that can't be written never breaks the command; say so once.
                _failed = true;
                Log.Warn($"SmartAI session log could not be written ({_path}): {e.Message}");
            }
        }

        private static string Header()
        {
            SmartAiSpecRegistry registry = SmartAiSpecRegistry.Active;
            return $"SmartAI session log{Environment.NewLine}" +
                   $"Server started {SessionStart():yyyy-MM-dd HH:mm:ss}; log created {DateTime.Now:yyyy-MM-dd HH:mm:ss}.{Environment.NewLine}" +
                   $"SmartAI {(SmartAiProperties.USE_SMART_AI ? "on" : "off")}{(SmartAiFallback.IsTripped ? $" (turned off for this session: {SmartAiFallback.TripReason})" : string.Empty)}; " +
                   $"{registry.Classes.Count} class file(s), {registry.Specs.Count} spec(s) loaded from {SmartAiSpecRegistry.DefaultDirectory}.{Environment.NewLine}" +
                   $"Server: {typeof(SmartAiSessionLog).Assembly.Location} " +
                   $"({File.GetLastWriteTime(typeof(SmartAiSessionLog).Assembly.Location):yyyy-MM-dd HH:mm}).{Environment.NewLine}";
        }

        private static DateTime SessionStart()
        {
            try
            {
                return Process.GetCurrentProcess().StartTime;
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}
