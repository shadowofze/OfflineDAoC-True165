using System;
using System.Text.RegularExpressions;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// Setting a bot's personality (plan Phase 10): "/smartai personality &lt;name&gt;" on a targeted companion, or saying
    /// "Cedric personality defensive" near it (CompanionChat165). A player sets only their own hired companions; a GM any
    /// bot. The choice is saved (offline_bot_smartai_profiles) and takes effect at the bot's next think.
    /// </summary>
    public static class SmartPersonalityCommands
    {
        private static readonly Regex Voice = new(@"\bpersonality\s+(?<name>[a-z]+)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public const string Names = "auto, aggressive, defensive, tactical, opportunistic, supportive, conservative";

        /// <summary>The personality named in a spoken line ("... personality aggressive"), if any.</summary>
        public static bool TryParseVoice(string message, out SmartPersonality personality)
        {
            personality = SmartPersonality.Auto;
            Match m = Voice.Match(message ?? string.Empty);
            return m.Success && SmartPersonalityProfile.TryParse(m.Groups["name"].Value, out personality);
        }

        /// <summary>A companion's spoken answer to a personality order, or false if the line isn't one.</summary>
        public static bool TryVoice(GameBot bot, string message, out string answer)
        {
            answer = null;
            if (!TryParseVoice(message, out SmartPersonality personality))
                return false;
            answer = SmartPersonalityRegistry.Set(bot.DatabaseID, personality) ? Spoken(personality) : "I could not remember that, sorry.";
            return true;
        }

        public static string Spoken(SmartPersonality p) => p switch
        {
            SmartPersonality.Aggressive => "I will press the attack.",
            SmartPersonality.Defensive => "I will guard the group.",
            SmartPersonality.Tactical => "I will fight smart and stay on our target.",
            SmartPersonality.Opportunistic => "I will strike where they are weak.",
            SmartPersonality.Supportive => "I will look after the group first.",
            SmartPersonality.Conservative => "I will play it safe.",
            _ => "I will fight my own way.",
        } + (SmartAiConfig.IsEnabled ? string.Empty : " (SmartAI is off: this applies when Use SmartAI is on.)");

        /// <summary>True when this player may set this bot's personality.</summary>
        public static bool MaySet(GamePlayer player, GameBot bot)
        {
            if (player == null || bot == null)
                return false;
            if (player.Client?.Account?.PrivLevel >= (uint)ePrivLevel.GM)
                return true;
            foreach (OfflineHiredCompanionRecord hired in HiredCompanions165.RosterOf(player))
                if (hired.BotId == bot.DatabaseID)
                    return true;
            return false;
        }
    }
}
