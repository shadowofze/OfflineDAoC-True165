using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI caster kiting, alone in the open world (owner 2026-11-05: "I would attempt to cast a few damage spells on
    /// them and cast root and get more distance to cast my best repeatable damage spells, if I didn't have root but had
    /// stun I would try to stun them and then continue to cast my best repeatable damage spell ... the bot would need to
    /// pre-plan how they would run away to get more distance (this won't work in a dungeon)").
    ///
    /// The plan, per fight:
    /// 1. Far out, the caster nukes as usual (the strongest damage spell that is ready).
    /// 2. Escape planned at the start: the first think on a new target picks the clear way back (straight away from the
    ///    monster, or up to 90 degrees to a side) with no other aggressive monster near it, on the navmesh.
    /// 3. As the monster comes, the caster casts its root early enough to land before the monster reaches it (its cast
    ///    time at the monster's speed), or its stun when it has no root (or the monster is immune to roots).
    /// 4. Rooted: the caster backs off along the planned way until it is a good nuke range away, then turns and nukes;
    ///    the root breaks on damage, as for a player, and the monster comes again: back to step 3.
    /// 5. Stunned: the caster stays and nukes while the stun lasts.
    /// Never in a dungeon, a group, against players, or while a monster's hits keep it from casting (CasterHeldInMelee).
    /// Spells, ranges, cast times, immunities and breaks are the game's own; only the bot's choices change.
    /// Movement: one walk order, re-issued only when the bot stopped short, and the attack follow is stopped first, so
    /// nothing else moves the bot meanwhile (the rubberbanding causes in SmartFlank).
    /// </summary>
    public static class SmartKite
    {
        public const int GapMargin = 150;
        public const int MinimumGap = 600;
        public const int MaximumGap = 1_300;
        public const int MaximumRetreatStep = 1_000;
        public const int RetreatArrival = 80;
        public const long RetreatMilliseconds = 7_000;
        public const long ReorderMilliseconds = 1_500;
        /// <summary>Seconds of margin added to the control spell's cast time when timing it against an approaching monster.</summary>
        public const double ControlLeadSeconds = 0.6;
        public const int ThreatMargin = 250;
        public static readonly int[] EscapeAngles = [0, 30, -30, 60, -60, 90, -90];

        private sealed class State
        {
            public GameLiving Target;
            public int PreferredAngle;
            public bool Retreating;
            public Vector3 RetreatPoint;
            public long RetreatUntil, NextOrder;
            // Theurgist ice pets: where this fight's pets were called, and the step to the next spot.
            public readonly System.Collections.Generic.List<Vector2> PetSpots = new();
            public bool Spreading;
            public Vector3 SpreadPoint;
            public long SpreadUntil;
        }

        // ---- Theurgist ice pets (owner 2026-11-05: "cast one ice pet, run in whatever direction about 400 units then
        // cast another pet then run about 400 more units and cast another, typically this helps keep the pets from
        // trying to melee if the monster they are attacking comes too close ... make sure I wasn't running into any
        // aggressive monsters"). An ice pet melees once its target is within its attack range (TheurgistIcePetBrain)
        // and stays where it was called (64 units in front of the caster), so pets called 400 units apart can't all be
        // reached at once: most keep casting.

        public const int PetSpreadDistance = 400;
        public const long SpreadMilliseconds = 5_000;
        /// <summary>Sideways first (the caster keeps its distance to the monster), then wider, then straight away.</summary>
        public static readonly int[] SpreadAngles = [90, -90, 60, -60, 120, -120, 0, 30, -30];

        public static bool IsIcePet(Spell spell) =>
            spell is { SpellType: eSpellType.SummonTheurgistPet } && spell.Name.Contains("ice", StringComparison.OrdinalIgnoreCase);

        /// <summary>Pure: this spot is far enough from every earlier pet of the fight to call the next one here.</summary>
        public static bool FreshPetSpot(Vector2 here, System.Collections.Generic.IReadOnlyList<Vector2> spots, int spread) =>
            spots.All(p => Vector2.Distance(p, here) >= spread - 50);

        private static Spell ReadyIcePet(GameBot bot, int distance) => bot.Spells?
            .Where(s => IsIcePet(s) && s.Level <= bot.Level && bot.Mana >= bot.PowerCost(s) &&
                        bot.GetSkillDisabledDuration(s) == 0 && s.Range >= distance &&
                        DOL.GS.Spells.SummonTheurgistPet.PetLimitAllowsAnother(bot.PetCount, GS.ServerProperties.Properties.THEURGIST_PET_CAP))
            .OrderByDescending(s => s.Level).FirstOrDefault();

        /// <summary>The ice pet plan for a Theurgist alone: call a pet, step about 400 units (clear of other aggressive
        /// monsters), call the next. True while it owns the think.</summary>
        private static bool IcePetSpread(BotBrain brain, GameBot bot, GameLiving target, State s, long now, int distance, out string note)
        {
            note = null;
            if (s.Spreading)
            {
                if (now > s.SpreadUntil || brain.CasterHeldInMelee() ||
                    Vector2.Distance(new(bot.X, bot.Y), new(s.SpreadPoint.X, s.SpreadPoint.Y)) <= RetreatArrival)
                {
                    s.Spreading = false;
                    bot.StopMovingOnPath();
                    bot.StopMoving();
                    bot.TurnTo(target);
                }
                else
                {
                    if (!bot.IsMoving && now >= s.NextOrder)
                    {
                        s.NextOrder = now + ReorderMilliseconds;
                        bot.PathTo(s.SpreadPoint, bot.MaxSpeed);
                    }
                    note = $"ice pets: moving {PetSpreadDistance} to call pet {s.PetSpots.Count + 1}";
                    return true;
                }
            }
            if (bot.IsCasting || bot.castingComponent?.HasPendingSkillRequests == true || brain.CasterHeldInMelee())
                return false;
            Spell pet = ReadyIcePet(bot, distance);
            if (pet == null)
                return false;
            Vector2 here = new(bot.X, bot.Y);
            if (!FreshPetSpot(here, s.PetSpots, PetSpreadDistance) &&
                TryPoint(bot, target, PetSpreadDistance, SpreadAngles, s.PreferredAngle, false,
                    p => FreshPetSpot(new(p.X, p.Y), s.PetSpots, PetSpreadDistance) &&
                         Vector2.Distance(new(p.X, p.Y), new(target.X, target.Y)) >= Math.Max(350, distance - 150),
                    out Vector3 spot, out _))
            {
                s.Spreading = true;
                s.SpreadPoint = spot;
                s.SpreadUntil = now + SpreadMilliseconds;
                s.NextOrder = now + ReorderMilliseconds;
                if (bot.attackComponent?.AttackState == true)
                    bot.StopAttack();
                bot.StopFollowing();
                bot.PathTo(spot, bot.MaxSpeed);
                Count(Reason.PetSpread, now);
                note = $"ice pets: stepping {PetSpreadDistance} away from pet {s.PetSpots.Count}";
                return true;
            }
            // A fresh spot, the first pet, or nowhere clear to step to: call it here.
            if (bot.attackComponent?.AttackState == true)
                bot.StopAttack();
            bot.TargetObject = target;
            if (!brain.SmartAiCastSpell(pet))
                return false;
            s.PetSpots.Add(here);
            Count(Reason.IcePet, now);
            note = $"ice pets: {pet.Name} #{s.PetSpots.Count} on {target.Name} at {distance}";
            return true;
        }

        private static readonly ConditionalWeakTable<GameBot, State> States = new();

        // ---- pure rules

        public static bool IsRoot(Spell spell) => spell != null && spell.Target == eSpellTarget.ENEMY && spell.Value >= 99 &&
            spell.SpellType is eSpellType.SpeedDecrease or eSpellType.UnbreakableSpeedDecrease;

        public static bool IsStun(Spell spell) => spell is { SpellType: eSpellType.Stun, Target: eSpellTarget.ENEMY };

        /// <summary>Pure: how close the monster may come before the control spell must start, so it lands first.</summary>
        public static int ControlDistance(int castMilliseconds, int monsterSpeed, int meleeRange) =>
            Math.Clamp(meleeRange + (int)(Math.Max(100, monsterSpeed) * (castMilliseconds / 1000.0 + ControlLeadSeconds)), 250, 1_400);

        /// <summary>Pure: how far from the rooted monster the caster backs off: its best nuke's range, less a margin.</summary>
        public static int DesiredGap(int nukeRange) => Math.Clamp(nukeRange - GapMargin, MinimumGap, MaximumGap);

        /// <summary>Pure: the escape point, <paramref name="step"/> units from the caster, turned <paramref name="angle"/>
        /// degrees from straight away from the monster.</summary>
        public static Vector2 EscapePoint(Vector2 caster, Vector2 monster, int angle, float step)
        {
            Vector2 away = caster - monster;
            if (away.LengthSquared() < 1)
                away = new Vector2(1, 0);
            away = Vector2.Normalize(away);
            double r = angle * Math.PI / 180;
            Vector2 turned = new((float)(away.X * Math.Cos(r) - away.Y * Math.Sin(r)), (float)(away.X * Math.Sin(r) + away.Y * Math.Cos(r)));
            return caster + turned * step;
        }

        public static bool Applies(bool smartAi, bool alone, bool offensiveCaster, bool openWorld, bool targetIsMonster) =>
            smartAi && alone && offensiveCaster && openWorld && targetIsMonster;

        /// <summary>A solo SmartAI bot may break a hold on its own target (nobody else's crowd control to keep).</summary>
        public static bool SoloMayBreakHold(GameBot bot) =>
            SmartAiConfig.IsEnabled && bot?.IsAutonomousWorldBot == true && !(bot.Group?.MemberCount > 1) && !bot.IsPlayerLedGroup;

        // ---- the think

        /// <summary>True when the kite owns this think (a control spell cast, or backing off).</summary>
        public static bool Think(BotBrain brain, GameBot bot, GameLiving target, out string note)
        {
            note = null;
            if (bot == null || target is not { IsAlive: true } ||
                !Applies(SoloMayBreakHold(bot), true, BotSpellPower.IsOffensiveCaster(bot),
                    bot.CurrentZone is { IsDungeon: false } && bot.CurrentRegion is { IsDungeon: false },
                    target is GameNPC and not GameBot && !BotPvpCrowdControl.PlayerLike(target)) ||
                AutonomousObjectiveAssignments.Is(bot, eAutonomousObjectiveKind.RvR))
                return false;
            State s = States.GetOrCreateValue(bot);
            long now = GameLoop.GameLoopTime;
            if (!ReferenceEquals(s.Target, target))
            {
                s.Target = target;
                s.Retreating = false;
                s.Spreading = false;
                s.PetSpots.Clear();
                s.PreferredAngle = PlanEscapeAngle(bot, target);
                Count(Reason.Planned, now);
            }
            bool rooted = IsRootedNow(target);
            bool held = target.IsStunned || target.IsMezzed;
            int distance = (int)bot.GetDistanceTo(target);
            int gap = DesiredGap(BestNukeRange(bot));

            if (s.Retreating)
            {
                Vector2 here = new(bot.X, bot.Y);
                if (!rooted || distance >= gap || now > s.RetreatUntil ||
                    Vector2.Distance(here, new(s.RetreatPoint.X, s.RetreatPoint.Y)) <= RetreatArrival)
                {
                    s.Retreating = false;
                    bot.StopMovingOnPath();
                    bot.StopMoving();
                    bot.TurnTo(target);
                    Count(Reason.RetreatDone, now);
                    return false;
                }
                if (!bot.IsMoving && now >= s.NextOrder)
                {
                    s.NextOrder = now + ReorderMilliseconds;
                    bot.PathTo(s.RetreatPoint, bot.MaxSpeed);
                }
                note = $"kite: backing off from rooted {target.Name} ({distance}/{gap})";
                return true;
            }
            bool theurgist = bot.CharacterClass?.ID == (int)eCharacterClass.Theurgist;
            if (theurgist && s.Spreading && IcePetSpread(brain, bot, target, s, now, distance, out note))
                return true;
            if (bot.IsCasting || bot.castingComponent?.HasPendingSkillRequests == true)
                return false;

            if (rooted && distance < gap - 100)
            {
                if (TryRetreatPoint(bot, target, gap - distance + 100, s.PreferredAngle, out Vector3 point, out int angle))
                {
                    s.PreferredAngle = angle;
                    s.Retreating = true;
                    s.RetreatPoint = point;
                    s.RetreatUntil = now + RetreatMilliseconds;
                    s.NextOrder = now + ReorderMilliseconds;
                    if (bot.attackComponent?.AttackState == true)
                        bot.StopAttack();
                    bot.StopFollowing();
                    bot.PathTo(point, bot.MaxSpeed);
                    Count(Reason.Retreat, now);
                    note = $"kite: {target.Name} rooted at {distance}; backing off to {gap}";
                    return true;
                }
                Count(Reason.NoEscape, now);
                return false;
            }
            if (rooted || held)
                return theurgist && IcePetSpread(brain, bot, target, s, now, distance, out note);
            // Only against a monster on its way to this caster, and while a cast can still start.
            if (target.TargetObject != bot || brain.CasterHeldInMelee())
                return theurgist && IcePetSpread(brain, bot, target, s, now, distance, out note);
            Spell control = ChooseControl(bot, target, distance);
            int trigger = control == null ? 0 : ControlDistance(control.CastTime, target.MaxSpeed, bot.MeleeAttackRange);
            if (control == null || distance > trigger || distance > control.Range)
                return theurgist && IcePetSpread(brain, bot, target, s, now, distance, out note);
            if (bot.attackComponent?.AttackState == true)
                bot.StopAttack();
            bot.TargetObject = target;
            if (!brain.SmartAiCastOffensive(control))
                return false;
            Count(IsRoot(control) ? Reason.Root : Reason.Stun, now);
            note = $"kite: {control.Name} on {target.Name} at {distance} (cast before it reaches {trigger})";
            return true;
        }

        private static bool IsRootedNow(GameLiving target) =>
            EffectListService.GetEffectOnTarget(target, eEffect.MovementSpeedDebuff) is ECSGameSpellEffect e &&
            e.SpellHandler?.Spell is { Value: >= 99 };

        /// <summary>The root it can cast now (not immune), else the stun, else none.</summary>
        private static Spell ChooseControl(GameBot bot, GameLiving target, int distance)
        {
            if (bot.Spells == null || target.HasAbility(Abilities.CCImmunity))
                return null;
            bool rootImmune = target.HasAbility(Abilities.RootImmunity) ||
                              target.effectListComponent.ContainsEffectForEffectType(eEffect.SnareImmunity);
            bool stunImmune = target.HasAbility(Abilities.StunImmunity) ||
                              target.effectListComponent.ContainsEffectForEffectType(eEffect.StunImmunity) ||
                              target.effectListComponent.ContainsEffectForEffectType(eEffect.NPCStunImmunity);
            Spell Ready(Func<Spell, bool> kind) => bot.Spells
                .Where(s => kind(s) && s.Level <= bot.Level && bot.Mana >= bot.PowerCost(s) &&
                            bot.GetSkillDisabledDuration(s) == 0 && s.Range >= Math.Min(distance, 600))
                .OrderBy(s => s.CastTime).ThenByDescending(s => s.Level).FirstOrDefault();
            return (rootImmune ? null : Ready(IsRoot)) ?? (stunImmune ? null : Ready(IsStun));
        }

        private static int BestNukeRange(GameBot bot) =>
            bot.Spells?.Where(s => BotCasterPriority.IsDamage(s) && s.Target == eSpellTarget.ENEMY && s.Range > 0 && s.Level <= bot.Level)
                .Select(s => s.Range).DefaultIfEmpty(1_500).Max() ?? 1_500;

        /// <summary>Pre-plan: the first clear escape angle from where the caster stands when the fight starts.</summary>
        private static int PlanEscapeAngle(GameBot bot, GameLiving target) =>
            TryRetreatPoint(bot, target, 600, 0, out _, out int angle) ? angle : 0;

        /// <summary>A reachable point <paramref name="step"/> units back, its way and itself clear of other aggressive
        /// monsters; the preferred angle first.</summary>
        private static bool TryRetreatPoint(GameBot bot, GameLiving target, int step, int preferred, out Vector3 point, out int angle) =>
            TryPoint(bot, target, step, EscapeAngles, preferred, true, null, out point, out angle);

        /// <summary>A reachable point <paramref name="step"/> units from the caster at one of <paramref name="angles"/>
        /// (degrees from straight away from the monster), its way and itself clear of other aggressive monsters' aggro.
        /// <paramref name="openGap"/>: it must also take the caster well away from the monster.</summary>
        private static bool TryPoint(GameBot bot, GameLiving target, int step, int[] angles, int preferred, bool openGap,
            Func<Vector3, bool> also, out Vector3 point, out int angle)
        {
            point = default;
            angle = 0;
            IPathfindingMgr nav = PathfindingProvider.Instance;
            Zone zone = bot.CurrentZone;
            if (nav?.IsAvailable != true || zone == null || !nav.HasNavmesh(zone))
                return false;
            float length = Math.Clamp(step, 250, MaximumRetreatStep);
            Vector2 caster = new(bot.X, bot.Y), monster = new(target.X, target.Y);
            Vector3 from = new(bot.X, bot.Y, bot.Z);
            GameNPC[] others = bot.GetNPCsInRadius((ushort)(length + 1_200))
                .Where(npc => npc != target && npc.IsAlive && npc is not GameBot &&
                              npc.Brain is StandardMobBrain mob && mob.AggroRange > 0 && mob.CanAggroTarget(bot))
                .ToArray();
            foreach (int a in angles.OrderBy(x => x == preferred ? 0 : 1))
            {
                Vector2 p = EscapePoint(caster, monster, a, length);
                if (!AutonomousNavigationSurface.TryFloor(nav, zone, new Vector3(p.X, p.Y, bot.Z), out Vector3 floor) ||
                    bot.CurrentRegion?.GetZone((int)floor.X, (int)floor.Y) != zone ||
                    openGap && Vector2.Distance(new(floor.X, floor.Y), monster) < Vector2.Distance(caster, monster) + length * 0.5f ||
                    also != null && !also(floor))
                    continue;
                bool clear = true;
                foreach (GameNPC npc in others)
                {
                    int reach = ((StandardMobBrain)npc.Brain).AggroRange + ThreatMargin;
                    if (Vector2.Distance(new(npc.X, npc.Y), new(floor.X, floor.Y)) < reach ||
                        DistanceToSegment(new(npc.X, npc.Y), caster, new(floor.X, floor.Y)) < reach)
                    {
                        clear = false;
                        break;
                    }
                }
                if (!clear || !AutonomousZoneItinerary.HasCompleteCorridor(nav, zone, from, floor))
                    continue;
                point = floor;
                angle = a;
                return true;
            }
            return false;
        }

        private static float DistanceToSegment(Vector2 p, Vector2 a, Vector2 b)
        {
            Vector2 ab = b - a;
            float t = ab.LengthSquared() < 1 ? 0 : Math.Clamp(Vector2.Dot(p - a, ab) / ab.LengthSquared(), 0, 1);
            return Vector2.Distance(p, a + ab * t);
        }

        // ---- diagnostics: what kiting did, logged every 10 minutes

        public enum Reason { Planned, Root, Stun, Retreat, RetreatDone, NoEscape, IcePet, PetSpread }

        private static readonly long[] Counts = new long[Enum.GetValues<Reason>().Length];
        private static long _nextReportTick;
        private static readonly DOL.Logging.Logger Log = DOL.Logging.LoggerManager.Create(typeof(SmartKite));

        private static void Count(Reason reason, long now)
        {
            System.Threading.Interlocked.Increment(ref Counts[(int)reason]);
            long next = System.Threading.Interlocked.Read(ref _nextReportTick);
            if (now < next || System.Threading.Interlocked.CompareExchange(ref _nextReportTick, now + 10 * 60_000L, next) != next)
                return;
            if (next == 0)
                return;
            var parts = new System.Text.StringBuilder("SMARTAI_KITE last10min");
            foreach (Reason r in Enum.GetValues<Reason>())
                parts.Append(' ').Append(r).Append('=').Append(System.Threading.Interlocked.Exchange(ref Counts[(int)r], 0));
            Log.Info(parts.ToString());
        }
    }
}
