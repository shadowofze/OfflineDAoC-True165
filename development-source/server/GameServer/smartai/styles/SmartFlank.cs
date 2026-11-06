using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using DOL.GS.Styles;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI flanking for positional styles (owner 2026-11-05: "implement the flanking / style positions ... figure out
    /// the rubberbanding risk"). Run 4: 88% of melee swings came from the target's front, so back and side styles
    /// (Backstab, Perforate Artery, Dragonfly's side chains...) almost never fired.
    ///
    /// A melee bot SmartAI drives, in a group, whose target is a monster that stands still and attacks someone else,
    /// moves to the arc its best positional style needs (back 150-210 degrees, side 45-150 / 210-315, as
    /// StyleProcessor checks) and fights from there. Nothing in a style or combat rule changes: the position is checked
    /// again at every swing, as for a player.
    ///
    /// The rubberbanding risk, from the movement code, and what this does about each:
    /// 1. Two movement owners. A walk order from the AI turn and the attack's follow (NpcMovementComponent.FollowTick)
    ///    each moved the bot toward their own point, and clients saw it snap between them. Here the flank spot is the
    ///    follow's own destination (like a formation slot), so the follow is the only thing moving the bot.
    /// 2. Orders restarted every turn. Stopping and re-following each think made the server stop and restart the bot
    ///    (BotBrain, "IsMeleeChasing"). Nothing here stops or re-issues the follow.
    /// 3. A spot that moves a little all the time (the monster turning a few degrees toward its target). The spot is
    ///    kept while it is within SlotTolerance of the last one, and a bot already inside the arc (with a margin) does
    ///    not move at all.
    /// 4. Circling a monster that turns to face you. No flanking against the monster's own target, a moving monster, a
    ///    player (they turn freely) or when alone (the monster faces you), and a bot that doesn't reach the arc in
    ///    GiveUpMilliseconds gives up on that target for RetryMilliseconds.
    /// 5. Walking through the monster. From the front, the bot goes to the nearer side first, then to the back.
    /// </summary>
    public static class SmartFlank
    {
        public enum Position { None, Side, Back }

        /// <summary>Degrees kept inside the style arc before the bot counts as there (the arc edges flicker).</summary>
        public const int ArcMargin = 10;
        public const float SlotTolerance = 40;
        public const int ArrivedTolerance = 12;
        public const long GiveUpMilliseconds = 6_000;
        public const long RetryMilliseconds = 15_000;
        private const long WantCacheMilliseconds = 1_000;

        private sealed class State
        {
            public GameLiving Target;
            public long LastInArcTick;
            public long BlockedUntil;
            public Vector2 LastSlot;
            public bool HasSlot;
            public Position Want;
            public bool Drives;
            public long WantTick = long.MinValue;
        }

        private static readonly ConditionalWeakTable<GameNPC, State> States = new();

        // ---- diagnostics: why melee follow checks did or did not flank, logged every 10 minutes (run 5 showed no
        // change in swing positions, and nothing said which condition stopped it).

        public enum Reason { NoSpec, NoPositionalStyle, Alone, NotMonster, TargetMoving, TargetOnMe, InArc, GaveUp, Flanking }

        private static readonly long[] Counts = new long[Enum.GetValues<Reason>().Length];
        private static long _nextReportTick;
        public const long ReportMilliseconds = 10 * 60_000;
        private static readonly DOL.Logging.Logger Log = DOL.Logging.LoggerManager.Create(typeof(SmartFlank));

        private static void Count(Reason reason, long now)
        {
            System.Threading.Interlocked.Increment(ref Counts[(int)reason]);
            long next = System.Threading.Interlocked.Read(ref _nextReportTick);
            if (now < next || System.Threading.Interlocked.CompareExchange(ref _nextReportTick, now + ReportMilliseconds, next) != next)
                return;
            if (next == 0)
                return; // the first call only starts the clock
            var parts = new System.Text.StringBuilder("SMARTAI_FLANK last10min");
            foreach (Reason r in Enum.GetValues<Reason>())
                parts.Append(' ').Append(r).Append('=').Append(System.Threading.Interlocked.Exchange(ref Counts[(int)r], 0));
            Log.Info(parts.ToString());
        }

        /// <summary>Pure: the attacker is inside the style's arc, <paramref name="margin"/> degrees in from its edges.</summary>
        public static bool InArc(Position position, float angle, int margin) => position switch
        {
            Position.Back => angle > 150 + margin && angle < 210 - margin,
            Position.Side => angle >= 45 + margin && angle <= 150 - margin || angle >= 210 + margin && angle <= 315 - margin,
            _ => true,
        };

        /// <summary>
        /// Pure: where to stand, in degrees from the target's facing (0 = in front). The middle of the arc on the bot's
        /// side; for the back from the front half, the nearer side first.
        /// </summary>
        public static float SlotAngle(Position position, float current)
        {
            float side = current <= 180 ? 97.5f : 262.5f;
            if (position == Position.Side)
                return side;
            bool frontHalf = current < 90 || current > 270;
            return frontHalf ? side : 180;
        }

        /// <summary>Pure: flanking applies to this fight.</summary>
        public static bool Applies(bool smartAi, bool grouped, bool targetIsMonster, bool targetMoving, bool targetOnMe, bool melee) =>
            smartAi && grouped && targetIsMonster && !targetMoving && !targetOnMe && melee;

        /// <summary>
        /// The follow destination for a flanking bot: true with the spot to walk to; false to follow as usual (no
        /// flanking here, or already in the arc).
        /// </summary>
        public static bool TryGetSlot(GameNPC owner, GameLiving target, int distance, out Vector3 slot)
        {
            slot = default;
            if (owner is not GameBot bot || !SmartAiConfig.IsEnabled || target == null || !bot.IsAttacking ||
                bot.TargetObject != target || bot.ActiveWeaponSlot == eActiveWeaponSlot.Distance)
                return false;
            try
            {
                State s = States.GetOrCreateValue(bot);
                long now = GameLoop.GameLoopTime;
                if (!ReferenceEquals(s.Target, target))
                {
                    s.Target = target;
                    s.LastInArcTick = now;
                    s.BlockedUntil = 0;
                    s.HasSlot = false;
                }
                if (now < s.BlockedUntil)
                    return false;
                if (now - s.WantTick > WantCacheMilliseconds)
                {
                    s.WantTick = now;
                    s.Drives = SmartStyleSelector.Drives(bot);
                    s.Want = s.Drives ? WantedPosition(bot) : Position.None;
                }
                Reason why = !s.Drives ? Reason.NoSpec : s.Want == Position.None ? Reason.NoPositionalStyle :
                    !(bot.Group?.MemberCount > 1) ? Reason.Alone : target is not GameNPC || target is GameBot ? Reason.NotMonster :
                    target.IsMoving ? Reason.TargetMoving : target.TargetObject == bot ? Reason.TargetOnMe : Reason.Flanking;
                if (why != Reason.Flanking)
                {
                    Count(why, now);
                    s.HasSlot = false;
                    s.LastInArcTick = now;
                    return false;
                }
                float angle = target.GetAngle(bot);
                if (InArc(s.Want, angle, ArcMargin))
                {
                    Count(Reason.InArc, now);
                    s.LastInArcTick = now;
                    s.HasSlot = false;
                    return false;
                }
                if (now - s.LastInArcTick > GiveUpMilliseconds)
                {
                    Count(Reason.GaveUp, now);
                    s.BlockedUntil = now + RetryMilliseconds;
                    s.HasSlot = false;
                    return false;
                }
                Count(Reason.Flanking, now);
                float slotAngle = SlotAngle(s.Want, angle);
                ushort heading = (ushort)((target.Heading + (int)(slotAngle * 4096 / 360)) & 0xFFF);
                Point2D p = target.GetPointFromHeading(heading, Math.Max(40, distance));
                Vector2 spot = new(p.X, p.Y);
                if (s.HasSlot && Vector2.Distance(spot, s.LastSlot) <= SlotTolerance)
                    spot = s.LastSlot;
                else
                {
                    s.LastSlot = spot;
                    s.HasSlot = true;
                }
                slot = new Vector3(spot.X, spot.Y, target.Z);
                return true;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("flanking", e);
                return false;
            }
        }

        /// <summary>The arc of the bot's positional styles it can use now (trained, weapon, endurance): back first.</summary>
        public static Position WantedPosition(GameBot bot)
        {
            if (bot?.Styles == null)
                return Position.None;
            Position best = Position.None;
            foreach (Style style in bot.Styles)
            {
                if (style is not { OpeningRequirementType: Style.eOpening.Positional } || style.StealthRequirement ||
                    style.Level > bot.Level ||
                    !BotMeleeStylePolicy.MatchesWeapon(style, bot.ActiveWeapon, bot.Inventory.GetItem(eInventorySlot.LeftHandWeapon), bot.ActiveWeaponSlot) ||
                    !StyleProcessor.CheckEnduranceCost(bot, BotMeleeStylePolicy.Weapon(bot, style), style))
                    continue;
                if (style.OpeningRequirementValue == (int)Style.eOpeningPosition.Back)
                    return Position.Back;
                if (style.OpeningRequirementValue == (int)Style.eOpeningPosition.Side)
                    best = Position.Side;
            }
            return best;
        }
    }
}
