using System;
using DOL.GS.Styles;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI's melee styles (plan Phase 6, architecture section 3.1 stage 5).
    /// - A bot SmartAI drives may use positional styles (back, side, front) and stealth openers. The legacy selector
    ///   leaves them out for every bot.
    /// - Nothing here bends a style rule. StyleProcessor.CanUseStyle checks the position (back 150-210 degrees, side
    ///   45-150 and 210-315, front 315-45, as 1.62+) and the stealth requirement at every swing, as for a player. The
    ///   swing itself unstealths the bot (WeaponAction), as it does a player.
    /// - "SmartAI drives the bot": SmartAI is on and the bot's class file has a spec for it.
    /// </summary>
    public static class SmartStyleSelector
    {
        /// <summary>How close a stealthed bot keeps hiding for its opener; farther, the legacy brain unstealths it.</summary>
        public const int StealthOpenerRange = 300;

        public static bool Drives(GameBot bot)
        {
            if (!SmartAiConfig.IsEnabled || bot == null)
                return false;
            try
            {
                return SmartAiSpecRegistry.Active.Resolve(bot) != null;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("style selection", e);
                return false;
            }
        }

        /// <summary>
        /// A stealthed bot in reach of its target keeps hiding for a stealth opener it can use (trained, weapon,
        /// endurance). Whether the opener's position fits is checked again at the swing; if it doesn't, the bot swings
        /// another style and the swing unstealths it.
        /// </summary>
        public static bool KeepsStealthForOpener(GameBot bot, GameLiving target)
        {
            if (bot is not { IsStealthed: true } || target is not { IsAlive: true } || bot.Styles == null ||
                !bot.IsWithinRadius(target, StealthOpenerRange) || !Drives(bot))
                return false;
            foreach (Style style in bot.Styles)
            {
                if (style is { StealthRequirement: true } && style.Level <= bot.Level &&
                    BotMeleeStylePolicy.MatchesWeapon(style, bot.ActiveWeapon, bot.Inventory.GetItem(eInventorySlot.LeftHandWeapon), bot.ActiveWeaponSlot) &&
                    StyleProcessor.CheckEnduranceCost(bot, BotMeleeStylePolicy.Weapon(bot, style), style))
                    return true;
            }
            return false;
        }
    }
}
