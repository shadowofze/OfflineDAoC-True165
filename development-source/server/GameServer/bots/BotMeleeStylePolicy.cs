using DOL.Database;
using DOL.GS.Styles;

namespace DOL.GS
{
    /// <summary>Shared companion/world-bot selection; native openings and costs remain authoritative.</summary>
    public static class BotMeleeStylePolicy
    {
        public static bool IsEligible(Style style) => IsEligible(style, false);

        /// <summary>The legacy selector leaves positional styles and stealth openers out; a bot SmartAI drives may use
        /// them (SmartStyleSelector). StyleProcessor.CanUseStyle still checks position and stealth at every swing.</summary>
        public static bool IsEligible(Style style, bool smartAi) => style != null &&
            (smartAi || style.OpeningRequirementType != Style.eOpening.Positional && !style.StealthRequirement);

        public static bool Better(Style candidate, Style current) => current == null ||
            candidate.GrowthRate > current.GrowthRate ||
            candidate.GrowthRate == current.GrowthRate && candidate.Level > current.Level;

        public static DbInventoryItem Weapon(GameBot bot, Style style) =>
            style?.WeaponTypeRequirement == (int)eObjectType.Shield && bot.ActiveWeaponSlot == eActiveWeaponSlot.Standard
                ? bot.Inventory.GetItem(eInventorySlot.LeftHandWeapon) : bot.ActiveWeapon;

        public static bool Usable(GameBot bot, Style style, AttackData lastAttack) => Usable(bot, style, lastAttack, false);

        public static bool Usable(GameBot bot, Style style, AttackData lastAttack, bool smartAi) =>
            IsEligible(style, smartAi) && style.Level <= bot.Level &&
            MatchesWeapon(style, bot.ActiveWeapon, bot.Inventory.GetItem(eInventorySlot.LeftHandWeapon), bot.ActiveWeaponSlot) &&
            StyleProcessor.CheckEnduranceCost(bot, Weapon(bot, style), style) &&
            bot.CheckStyleStun(style) &&
            StyleProcessor.CanUseStyle(lastAttack, bot, style, Weapon(bot, style));

        public static bool MatchesWeapon(Style style, DbInventoryItem primary, DbInventoryItem left, eActiveWeaponSlot slot)
        {
            if (style == null || primary == null || slot == eActiveWeaponSlot.Distance) return false;
            if (style.WeaponTypeRequirement == Style.SpecialWeaponType.DualWield)
            {
                if (slot != eActiveWeaponSlot.Standard || left == null ||
                    primary.Item_Type is not Slot.RIGHTHAND and not Slot.LEFTHAND ||
                    left.Object_Type == (int)eObjectType.Shield) return false;
                if (style.Spec == Specs.HandToHand)
                    return primary.Object_Type == (int)eObjectType.HandToHand && left.Object_Type == (int)eObjectType.HandToHand;
                if (style.Spec == Specs.Left_Axe) return left.Object_Type is (int)eObjectType.LeftAxe or (int)eObjectType.Axe;
                return GlobalConstants.IsWeapon(left.Object_Type);
            }
            if (style.WeaponTypeRequirement == Style.SpecialWeaponType.AnyWeapon)
                return GlobalConstants.IsWeapon(primary.Object_Type) && primary.Object_Type != (int)eObjectType.Shield;
            if (style.WeaponTypeRequirement == (int)eObjectType.Shield)
                return slot == eActiveWeaponSlot.Standard && left?.Object_Type == (int)eObjectType.Shield;
            eObjectType type = primary.Object_Type == (int)eObjectType.LeftAxe ? eObjectType.Axe : (eObjectType)primary.Object_Type;
            return GameServer.ServerRules.IsObjectTypesEqual((eObjectType)style.WeaponTypeRequirement, type);
        }

        public static Style Select(GameBot bot, AttackData lastAttack, bool preserveQueued = true)
        {
            if (bot?.Styles == null || !BotWeaponStats.FitsConfiguredSlot(bot, bot.ActiveWeapon,
                    bot.ActiveWeaponSlot == eActiveWeaponSlot.TwoHanded ? eInventorySlot.TwoHandWeapon : eInventorySlot.RightHandWeapon) ||
                bot.ActiveWeaponSlot == eActiveWeaponSlot.Distance)
                return null;
            bool smartAi = SmartAI.SmartStyleSelector.Drives(bot);
            // Preserve a deliberate tank taunt, but revalidate at the actual swing.
            Style queued = bot.styleComponent.NextCombatStyle;
            bool queuedUsable = preserveQueued && Usable(bot, queued, lastAttack, smartAi);
            if (queuedUsable && (!smartAi || bot.StylesTaunt?.Contains(queued) == true)) return queued;
            Style best = null;
            Style anytime = null;
            Style opener = null;
            Style positional = null;
            foreach (Style style in bot.Styles)
            {
                if (!Usable(bot, style, lastAttack, smartAi)) continue;
                if (Better(style, best)) best = style;
                if (SavageBotCombatPolicy.IsReliableAnytimeStyle(style) && Better(style, anytime)) anytime = style;
                if (style.StealthRequirement && Better(style, opener)) opener = style;
                else if (style.OpeningRequirementType == Style.eOpening.Positional && Better(style, positional)) positional = style;
            }
            bot.styleComponent.NextCombatBackupStyle = anytime;
            return Choose(smartAi, queuedUsable ? queued : null, opener, positional, best);
        }

        /// <summary>
        /// Pure: the style a SmartAI bot swings. Usable here means its opening is met now (StyleProcessor.CanUseStyle).
        /// A stealth opener the bot is hidden for comes first, then a positional style it stands in the arc for (run 8,
        /// 2026-11-06: Nightshades behind their target swung Bumblebee's Sting 82 times and Wasp's Sting 26 - the
        /// queued or higher-growth anytime style won, and the walk round the monster was wasted), then the queued
        /// style, then the best by growth rate. The legacy brain keeps queued-then-best.
        /// </summary>
        public static Style Choose(bool smartAi, Style queued, Style opener, Style positional, Style best) =>
            smartAi ? opener ?? positional ?? queued ?? best : queued ?? best;
    }
}
