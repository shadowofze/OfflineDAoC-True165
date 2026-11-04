namespace DOL.GS.SmartAI
{
    /// <summary>
    /// The one gate every SmartAI hook checks first (plan Phase 3, architecture section 2.2, tier 1).
    /// Off when the launcher's "Use SmartAI" is off, or when SmartAI turned itself off for this session after repeated
    /// errors (SmartAiFallback). While it is off no SmartAI code runs: the hooks are a single static check, and every
    /// bot uses the legacy BotBrain exactly as before.
    /// </summary>
    public static class SmartAiConfig
    {
        public static bool IsEnabled => SmartAiFallback.IsActive;
    }
}
