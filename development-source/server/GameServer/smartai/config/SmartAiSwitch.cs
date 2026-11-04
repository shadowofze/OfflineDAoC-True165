#nullable enable
using System;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// The "Use SmartAI" switch's one definition: its ServerProperty row and how a stored value reads. Linked into the
    /// launcher (OfflineDaoc.Launcher.csproj), so the server and the launcher can never disagree on the key or on what
    /// counts as "on". Dependency-free on purpose.
    /// SmartAI is strictly opt-in: only an explicit "True" turns it on. A missing row, an empty or unreadable value
    /// reads as off, so the legacy bot AI is always the fallback (LAUNCHER_INTEGRATION.md 2.2).
    /// </summary>
    public static class SmartAiSwitch
    {
        public const string Category = "autonomous";
        public const string UseSmartAiKey = "use_smart_ai";
        public const string UseSmartAiDescription =
            "Enable SmartAI tactical intelligence for hired companions and gamebots (off: the legacy bot AI). Set in the Offline DAoC launcher.";

        /// <summary>Only an explicit true (any case, surrounding spaces ignored) is on.</summary>
        public static bool IsOn(string? raw) => string.Equals(raw?.Trim(), "true", StringComparison.OrdinalIgnoreCase);

        /// <summary>The stored form, matching how the server writes bool properties.</summary>
        public static string ToStored(bool on) => on ? "True" : "False";
    }
}
