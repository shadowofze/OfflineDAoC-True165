using DOL.Database;
using DOL.Database.Attributes;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// One bot's SmartAI overrides (plan Phase 1, architecture section 5.2). A side table keyed by the gamebot's BotId,
    /// so the launcher's offline_world_bots roster table is untouched (the HiredCompanions165 pattern). GameServer.InitDB
    /// registers it by reflection and creates the table on the first start.
    /// A bot without a row uses "Auto" for both: its specialization's default personality and role.
    /// </summary>
    [DataTable(TableName = "offline_bot_smartai_profiles")]
    public sealed class OfflineBotSmartAiProfileRecord : DataObject
    {
        public const string Auto = "Auto";

        [PrimaryKey] public long BotId { get; set; }

        /// <summary>"Auto", or one of Aggressive, Defensive, Tactical, Opportunistic, Supportive, Conservative.</summary>
        [DataElement(AllowDbNull = false)] public string Personality { get; set; } = Auto;

        /// <summary>"Auto", or one of MainAssist, Tank, Healer, CrowdControl, Damage.</summary>
        [DataElement(AllowDbNull = false)] public string PreferredRole { get; set; } = Auto;

        /// <summary>When the row last changed, UTC ISO 8601 ("O").</summary>
        [DataElement(AllowDbNull = false)] public string UpdatedUtc { get; set; } = string.Empty;
    }
}
