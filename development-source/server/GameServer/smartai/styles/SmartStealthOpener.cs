using System;
using System.Runtime.CompilerServices;
using DOL.GS.Styles;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI stealth openers against monsters (owner 2026-11-06: "see if the infiltrator, nightshade, and shadowblade
    /// is doing their backstab from stealth"). Run 8: none did - a bot only hid to ambush an enemy player or bot
    /// (BotRvrAmbush), so Backstab, Perforate Artery and the other stealth openers were never used in PvE, where 1.65
    /// assassins opened every fight with them.
    ///
    /// An Infiltrator, Nightshade or Shadowblade SmartAI drives, with Stealth trained and a stealth opener it can use
    /// (trained, weapon, endurance), whose target is a monster not on it, hides and walks in: to the arc the opener
    /// needs (back or side) or into reach. Alone it opens on a fresh monster; in a group only on one the party is
    /// already fighting (it never pulls - the puller does). Arrived, it keeps hiding (SmartStyleSelector) and the swing
    /// opens with the opener and unstealths it, as for a player. Stealth speed, detection and every style rule are the
    /// game's own; nothing in a fight changes. A walk-in that takes more than GiveUpMilliseconds is dropped for that
    /// target. The numbers are improvised.
    /// </summary>
    public static class SmartStealthOpener
    {
        /// <summary>Farthest a bot starts hiding for an opener (stealthed walking is slow).</summary>
        public const int ApproachRange = 1_200;
        public const long GiveUpMilliseconds = 15_000;
        /// <summary>No walk-in so soon after the bot was hit.</summary>
        public const long HitQuietMilliseconds = 5_000;

        private sealed class State
        {
            public GameLiving Target;
            public long Since;
            public bool GaveUp;
            public bool Arrived;
        }

        private static readonly ConditionalWeakTable<GameBot, State> States = new();

        // ---- diagnostics, every 10 minutes
        public enum Reason { Approach, Arrived, GaveUp }

        private static readonly long[] Counts = new long[Enum.GetValues<Reason>().Length];
        private static long _nextReportTick;
        private static readonly DOL.Logging.Logger Log = DOL.Logging.LoggerManager.Create(typeof(SmartStealthOpener));

        private static void Count(Reason reason, long now)
        {
            System.Threading.Interlocked.Increment(ref Counts[(int)reason]);
            long next = System.Threading.Interlocked.Read(ref _nextReportTick);
            if (now < next || System.Threading.Interlocked.CompareExchange(ref _nextReportTick, now + SmartFlank.ReportMilliseconds, next) != next)
                return;
            if (next == 0)
                return;
            var parts = new System.Text.StringBuilder("SMARTAI_STEALTH_OPENER last10min");
            foreach (Reason r in Enum.GetValues<Reason>())
                parts.Append(' ').Append(r).Append('=').Append(System.Threading.Interlocked.Exchange(ref Counts[(int)r], 0));
            Log.Info(parts.ToString());
        }

        /// <summary>Pure: a walk-in for a stealth opener applies.</summary>
        public static bool Applies(bool smartAi, bool assassin, bool stealthTrained, bool hasOpener, bool targetIsMonster,
            bool targetOnMe, bool grouped, bool targetInCombat, bool botBusy, bool recentlyHit) =>
            smartAi && assassin && stealthTrained && hasOpener && targetIsMonster && !targetOnMe && !botBusy && !recentlyHit &&
            (!grouped || targetInCombat);

        /// <summary>Pure: the bot is where its opener can go off (in reach, and in the arc a positional opener needs).</summary>
        public static bool InPlace(bool inReach, SmartFlank.Position need, float angle) =>
            inReach && (need == SmartFlank.Position.None || SmartFlank.InArc(need, angle, 0));

        /// <summary>The best stealth opener the bot can use now, its position aside (trained, weapon, endurance).</summary>
        public static Style BestOpener(GameBot bot)
        {
            if (bot?.Styles == null)
                return null;
            Style best = null;
            foreach (Style style in bot.Styles)
            {
                if (style is not { StealthRequirement: true } || style.Level > bot.Level ||
                    !BotMeleeStylePolicy.MatchesWeapon(style, bot.ActiveWeapon, bot.Inventory.GetItem(eInventorySlot.LeftHandWeapon), bot.ActiveWeaponSlot) ||
                    !StyleProcessor.CheckEnduranceCost(bot, BotMeleeStylePolicy.Weapon(bot, style), style))
                    continue;
                if (BotMeleeStylePolicy.Better(style, best))
                    best = style;
            }
            return best;
        }

        /// <summary>The arc a style's opening needs (None when it has no positional opening).</summary>
        public static SmartFlank.Position PositionOf(Style style) =>
            style is not { OpeningRequirementType: Style.eOpening.Positional } ? SmartFlank.Position.None :
            style.OpeningRequirementValue == (int)Style.eOpeningPosition.Back ? SmartFlank.Position.Back :
            style.OpeningRequirementValue == (int)Style.eOpeningPosition.Side ? SmartFlank.Position.Side : SmartFlank.Position.None;

        /// <summary>
        /// Hides and walks the bot in for its opener; true while it does (the brain's think stops there). False - the
        /// fight goes on as before - when this doesn't apply, the bot is in place, or the walk-in was given up.
        /// </summary>
        public static bool TryApproach(GameBot bot, GameLiving target)
        {
            if (bot == null || target == null || !SmartAiConfig.IsEnabled || !bot.IsAutonomousWorldBot || bot.IsPlayerLedGroup)
                return false;
            try
            {
                long now = GameLoop.GameLoopTime;
                State s = States.GetOrCreateValue(bot);
                if (!ReferenceEquals(s.Target, target))
                {
                    s.Target = target;
                    s.Since = now;
                    s.GaveUp = false;
                    s.Arrived = false;
                }
                if (s.GaveUp || s.Arrived)
                    return false;
                if (!target.IsAlive || bot.CharacterClass == null || target.CurrentRegionID != bot.CurrentRegionID ||
                    !bot.IsWithinRadius(target, ApproachRange))
                    return false;
                // An attack order with no blow yet (walking in) may still turn into a stealth opener; a fight may not.
                bool busy = !bot.IsAlive || bot.InCombat || bot.IsCasting || bot.IsCrowdControlled || bot.IsOnHorse ||
                            bot.castingComponent?.HasPendingSkillRequests == true || GameRelic.IsPlayerCarryingRelic(bot);
                long lastHit = bot.LastAttackedByEnemyTick;
                Style opener = null;
                bool cheapChecks = BotPoisonSupply.IsAssassin((eCharacterClass)bot.CharacterClass.ID) &&
                                   (bot.GetSpecializationByName(Specs.Stealth)?.Level ?? 0) > 0;
                if (cheapChecks)
                    opener = BestOpener(bot);
                if (!Applies(SmartStyleSelector.Drives(bot), cheapChecks, cheapChecks, opener != null,
                        target is GameNPC and not GameBot, target.TargetObject == bot, bot.Group?.MemberCount > 1,
                        target.InCombat, busy, lastHit > 0 && now - lastHit < HitQuietMilliseconds))
                    return false;
                SmartFlank.Position need = PositionOf(opener);
                float angle = target.GetAngle(bot);
                int reach = Math.Max(60, bot.MeleeAttackRange - 30);
                if (InPlace(bot.IsWithinRadius(target, reach), need, angle))
                {
                    s.Arrived = true;
                    Count(Reason.Arrived, now);
                    return false;
                }
                if (now - s.Since > GiveUpMilliseconds)
                {
                    s.GaveUp = true;
                    Count(Reason.GaveUp, now);
                    return false;
                }
                if (!bot.IsStealthed)
                {
                    bot.Stealth(true);
                    if (!bot.IsStealthed)
                        return false; // a running song or a pulse legally prevents it
                    Count(Reason.Approach, now);
                }
                bot.WakeRecoveryRest();
                if (bot.IsAttacking)
                    bot.StopAttack(); // the walk-in, not the attack's follow, moves the bot until it is in place
                if (need == SmartFlank.Position.None || target.IsMoving)
                    bot.Follow(target, (short)reach, (short)(ApproachRange * 2));
                else
                {
                    float slotAngle = SmartFlank.SlotAngle(need, angle);
                    ushort heading = (ushort)((target.Heading + (int)(slotAngle * 4096 / 360)) & 0xFFF);
                    Point2D p = target.GetPointFromHeading(heading, reach);
                    bot.StopFollowing();
                    bot.PathTo(new Point3D(p.X, p.Y, target.Z), bot.MaxSpeed);
                }
                return true;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("stealth opener", e);
                return false;
            }
        }
    }
}
