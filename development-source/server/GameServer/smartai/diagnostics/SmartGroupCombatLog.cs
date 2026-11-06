using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DOL.AI.Brain;
using DOL.GS.Styles;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// /smartai debuggroup (owner 2026-11-05): records how bot groups fight, to review and improve their combat. One
    /// file per group in runtime/logs/smart_ai_groups. A fight is written only once it is complete: from the first
    /// blow until every member (and their pets) has been out of combat for a few seconds, with no held (mezzed,
    /// rooted) enemy still near. A fight cut short (the group breaks up, recording is turned off, the server stops) is
    /// dropped, never written in part.
    ///
    /// Each fight gives the group's makeup (name, kind, class, level, spec, role, personality, health and power), the
    /// quiet time before the pull and what the group did in it, a timeline (SmartAI decisions when they change, with
    /// the options it weighed; casts; melee; group target calls; deaths; sitting) and a summary per member with flags
    /// (a caster meleeing, a member who never acted). Kept short for reading and for review.
    ///
    /// Off, every hook is one volatile read. Recording only reads game state; it never changes what a bot does.
    /// </summary>
    public static class SmartGroupCombatLog
    {
        private static readonly Logging.Logger Log = Logging.LoggerManager.Create(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>How often the groups are checked (single-threaded pre-pass).</summary>
        public const long TickMs = 500;
        /// <summary>A member who attacked or was attacked this recently is fighting.</summary>
        public const long CombatWindowMs = 5000;
        /// <summary>Events this long before the detected start still belong to the fight.</summary>
        public const long PreStartSlackMs = 1500;
        /// <summary>A fight with no blow for this long is closed even if an enemy is still listed.</summary>
        public const long StaleCloseMs = 90_000;
        /// <summary>A fight this long is closed and written (with a note), so the file never waits forever.</summary>
        public const long MaxFightMs = 15 * 60_000;
        public const int MaxTimelineLines = 120;
        /// <summary>Held enemies within this of a member keep the fight open.</summary>
        public const int HeldEnemyRange = 2500;

        public enum Scope { Off, AllGroups, ChosenGroups }

        private static volatile Scope _scope = Scope.Off;
        /// <summary>Keyed by the Group, or by the GameBot for a bot fighting alone.</summary>
        private static readonly ConcurrentDictionary<object, GroupLog> Logs = new();
        private static readonly ConditionalWeakTable<Group, object> Chosen = new();
        private static long _nextTick;
        private static int _groupNumber;
        private static int _soloNumber;
        private static int _written, _discarded;
        private static Task _writes = Task.CompletedTask;
        private static readonly object WriteSync = new();
        private static readonly string SessionStamp = SessionStart().ToString("yyyyMMdd-HHmmss");

        public static bool IsOn => _scope != Scope.Off;
        public static Scope CurrentScope => _scope;
        public static int FightsWritten => Volatile.Read(ref _written);
        public static int FightsDiscarded => Volatile.Read(ref _discarded);
        public static int GroupsTracked => Logs.Count;
        public static string Directory => Path.Combine(SmartAiSessionLog.Directory, "smart_ai_groups");
        /// <summary>Fights of bots fighting alone (owner 2026-11-05), recorded with "all".</summary>
        public static string SoloDirectory => Path.Combine(SmartAiSessionLog.Directory, "smart_ai_solo");

        // ---- switching

        /// <summary>Every group with a bot in it, and every bot fighting alone.</summary>
        public static void RecordAll()
        {
            _scope = Scope.AllGroups;
        }

        /// <summary>/smartai debugfights: on when off, off when on. True when recording is now on.</summary>
        public static bool Toggle()
        {
            if (_scope != Scope.Off)
            {
                Stop();
                return false;
            }
            RecordAll();
            return true;
        }

        /// <summary>Adds one group (the caller's) to the recorded groups; other groups stop being recorded unless
        /// "all" is on.</summary>
        public static void RecordGroup(Group group)
        {
            if (group == null)
                return;
            Chosen.AddOrUpdate(group, null);
            if (_scope == Scope.Off)
                _scope = Scope.ChosenGroups;
            Logs.GetOrAdd(group, _ => new GroupLog(new Party(group), Interlocked.Increment(ref _groupNumber)));
        }

        /// <summary>Stops recording; fights in progress are dropped (they are not complete).</summary>
        public static void Stop()
        {
            _scope = Scope.Off;
            foreach (GroupLog log in Logs.Values)
                if (log.DropFight())
                    Interlocked.Increment(ref _discarded);
            Logs.Clear();
            Chosen.Clear();
        }

        public static string FileOf(Group group) =>
            group != null && Logs.TryGetValue(group, out GroupLog log) ? log.FilePath : null;

        public static int SolosTracked => Logs.Values.Count(l => l.Party.IsSolo);

        // ---- hooks (any thread)

        /// <summary>A SmartAI think: its decision, what the bot saw and the options it weighed.</summary>
        public static void Think(GameBot bot, SmartDecision decision, in SmartBotPerception p, GameLiving target,
            ReadOnlySpan<SmartActionCandidate> options)
        {
            if (_scope == Scope.Off || decision == null || Find(bot) is not { } log)
                return;
            string opts = null;
            if (options.Length > 1)
            {
                var sb = new StringBuilder();
                foreach (SmartActionCandidate c in options)
                    sb.Append(sb.Length > 0 ? ", " : string.Empty).Append(Short(c.Family)).Append(' ').Append(c.Score);
                opts = sb.ToString();
            }
            log.Think(bot, decision, p, target, opts);
        }

        /// <summary>A SmartAI think without a perception (no spec).</summary>
        public static void Think(GameBot bot, SmartDecision decision)
        {
            if (_scope == Scope.Off || decision == null || Find(bot) is not { } log)
                return;
            log.Think(bot, decision, null, null, null);
        }

        public static void Cast(GameBot bot, Spell spell, GameObject target, bool started)
        {
            if (_scope == Scope.Off || spell == null || Find(bot) is not { } log)
                return;
            log.Cast(bot, spell, target as GameLiving, started);
        }

        public static void Attack(GameBot bot, GameObject target)
        {
            if (_scope == Scope.Off || target is not GameLiving living || Find(bot) is not { } log)
                return;
            log.Attack(bot, living);
        }

        /// <summary>A spell that finished casting (SpellHandler.FinishSpellCast): any member, player or bot.</summary>
        public static void SpellLanded(GameLiving caster, Spell spell, GameLiving target)
        {
            if (_scope == Scope.Off || spell == null || caster is not (GamePlayer or GameBot) || Find(caster) is not { } log)
                return;
            log.SpellLanded(caster, spell, target);
        }

        /// <summary>One weapon swing (WeaponAction): a member's swing (style, position) or a member defending
        /// (parry, block, evade) against an enemy.</summary>
        public static void Swing(AttackData ad)
        {
            if (_scope == Scope.Off || ad?.Attacker == null || ad.Target == null)
                return;
            if (ad.Attacker is GamePlayer or GameBot && Find(ad.Attacker) is { } mine)
                mine.Swing(ad);
            if (ad.Target is GamePlayer or GameBot && ad.AttackResult is eAttackResult.Parried or eAttackResult.Blocked or eAttackResult.Evaded &&
                Find(ad.Target) is { } theirs)
                theirs.Defend(ad);
        }

        // ---- melee position and styles (the 1.65 style arcs, StyleProcessor)

        public enum Position { Front, Side, Back }

        /// <summary>Where the attacker stands seen from the target: back 150-210, side 45-150 and 210-315, else front.</summary>
        public static Position PositionOf(float angleFromTarget) =>
            angleFromTarget is > 150 and < 210 ? Position.Back
            : angleFromTarget is >= 45 and <= 150 or >= 210 and <= 315 ? Position.Side
            : Position.Front;

        public static string StyleKind(Style style) => style.OpeningRequirementType switch
        {
            Style.eOpening.Positional => ((Style.eOpeningPosition)style.OpeningRequirementValue).ToString().ToLowerInvariant(),
            Style.eOpening.Defensive => "reactive after " + style.AttackResultRequirement.ToString().ToLowerInvariant(),
            _ => style.OpeningRequirementValue != 0 ? "chain" : style.AttackResultRequirement != Style.eAttackResultRequirement.Any
                ? "after own " + style.AttackResultRequirement.ToString().ToLowerInvariant() : "anytime",
        };

        /// <summary>The reaction a defense opens: parry, block or evade.</summary>
        public static Style.eAttackResultRequirement ReactionTo(eAttackResult result) => result switch
        {
            eAttackResult.Parried => Style.eAttackResultRequirement.Parry,
            eAttackResult.Blocked => Style.eAttackResultRequirement.Block,
            eAttackResult.Evaded => Style.eAttackResultRequirement.Evade,
            _ => Style.eAttackResultRequirement.Any,
        };

        /// <summary>A reactive style this bot knows for that defense (players: unknown, null).</summary>
        private static Style ReactiveStyleFor(GameLiving member, Style.eAttackResultRequirement reaction) =>
            member is GameBot bot ? bot.StylesDefensive?.FirstOrDefault(s => s.AttackResultRequirement == reaction) : null;

        /// <summary>Registers the group of a member that is in a fight (hit, attacking, casting, thinking).</summary>
        public static void Touch(GameLiving member)
        {
            if (_scope != Scope.Off)
                Find(member);
        }

        private static GroupLog Find(GameLiving member)
        {
            Group group = member?.Group;
            if (group == null)
            {
                // A bot fighting alone: recorded with "all" (smart_ai_solo), never a player.
                if (_scope != Scope.AllGroups || member is not GameBot solo)
                    return null;
                return Logs.TryGetValue(solo, out GroupLog own) ? own
                    : Logs.GetOrAdd(solo, _ => new GroupLog(new Party(solo), Interlocked.Increment(ref _soloNumber)));
            }
            if (Logs.TryGetValue(group, out GroupLog log))
                return log;
            if (_scope == Scope.ChosenGroups && !Chosen.TryGetValue(group, out _))
                return null;
            // every group with a bot in it; a players-only group is not recorded
            if (!group.GetMembersInTheGroup().Any(m => m is GameBot))
                return null;
            return Logs.GetOrAdd(group, _ => new GroupLog(new Party(group), Interlocked.Increment(ref _groupNumber)));
        }

        // ---- the pre-pass (single thread, NpcService)

        public static void Tick()
        {
            if (_scope == Scope.Off)
                return;
            long now = GameLoop.GameLoopTime;
            if (now < _nextTick)
                return;
            _nextTick = now + TickMs;
            foreach (KeyValuePair<object, GroupLog> pair in Logs)
            {
                try
                {
                    if (pair.Value.Party.MemberCount == 0)
                    {
                        if (pair.Value.DropFight())
                            Interlocked.Increment(ref _discarded);
                        Logs.TryRemove(pair.Key, out _);
                        continue;
                    }
                    if (pair.Value.Update(now) is { } text)
                    {
                        Interlocked.Increment(ref _written);
                        Write(pair.Value, text);
                    }
                }
                catch (Exception e)
                {
                    // A recorder error never touches the game: drop that group's fight and say so once in a while.
                    pair.Value.DropFight();
                    Log.Warn($"SmartAI group log: {e.GetType().Name} {e.Message}");
                }
            }
        }

        private static void Write(GroupLog log, string text)
        {
            string path = log.FilePath;
            string header = log.TakeFileHeader();
            lock (WriteSync)
            {
                _writes = _writes.ContinueWith(_ =>
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                        if (header != null && !File.Exists(path))
                            File.AppendAllText(path, header, Encoding.UTF8);
                        File.AppendAllText(path, text, Encoding.UTF8);
                    }
                    catch (Exception e)
                    {
                        Log.Warn($"SmartAI group log could not be written ({path}): {e.Message}");
                    }
                }, TaskScheduler.Default);
            }
        }

        public static string Short(SmartActionFamily family) => family switch
        {
            SmartActionFamily.Triage => "Heal",
            SmartActionFamily.Support => "Buff",
            SmartActionFamily.OffenseSpells => "Spells",
            SmartActionFamily.OffenseFull => "Melee",
            _ => family.ToString(),
        };

        internal static string Kind(GameLiving m) => m switch
        {
            GamePlayer => "player",
            GameBot b when HiredCompanions165.IsHired(b) => "hired",
            GameBot { IsAutonomousWorldBot: true } => "gamebot",
            GameBot { IsTemporaryGroupHelper: true } => "helper",
            GameBot => "bot",
            _ => "npc",
        };

        /// <summary>A bot whose melee is a mistake: a list caster, or a spec that bans melee.</summary>
        internal static bool MeleeIsWrong(GameBot bot, out string why)
        {
            why = null;
            if (BotSpellPower.IsOffensiveCaster(bot))
                why = "list caster";
            // The ban is a group rule (SmartActionExecutor): a bot alone may defend itself.
            else if (bot.Group?.MemberCount > 1 && SmartAiConfig.IsEnabled && SmartAiSpecRegistry.Active.Resolve(bot) is { } spec &&
                     SmartSpecProfile.For(spec).MeleeBanned)
                why = "spec bans melee";
            return why != null;
        }

        private static DateTime SessionStart()
        {
            try { return Process.GetCurrentProcess().StartTime; }
            catch { return DateTime.Now; }
        }

        public static string SafeName(string name)
        {
            var sb = new StringBuilder();
            foreach (char c in name ?? "group")
                sb.Append(char.IsLetterOrDigit(c) ? c : '_');
            return sb.Length == 0 ? "group" : sb.ToString();
        }

        // ---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Who a log follows: a group, or one bot fighting on its own (/smartai debugfights records both). Offers the
        /// three things the log reads from a group. A solo party ends when its bot is gone or joins a group.
        /// </summary>
        internal sealed class Party
        {
            private readonly WeakReference<Group> _group;
            private readonly WeakReference<GameBot> _solo;

            public Party(Group group) => _group = new WeakReference<Group>(group);
            public Party(GameBot solo) => _solo = new WeakReference<GameBot>(solo);

            public bool IsSolo => _solo != null;

            /// <summary>The group itself (null for a solo bot or a group that is gone).</summary>
            public Group Group => _group != null && _group.TryGetTarget(out Group g) ? g : null;

            public bool TryGet(out Party party)
            {
                party = this;
                return _group != null ? _group.TryGetTarget(out _) : _solo.TryGetTarget(out _);
            }

            public List<GameLiving> GetMembersInTheGroup()
            {
                if (_group != null)
                    return _group.TryGetTarget(out Group g) ? g.GetMembersInTheGroup() : new List<GameLiving>();
                return _solo.TryGetTarget(out GameBot b) && b.Group == null ? new List<GameLiving> { b } : new List<GameLiving>();
            }

            public int MemberCount => _group != null
                ? (_group.TryGetTarget(out Group g) ? g.MemberCount : 0)
                : (_solo.TryGetTarget(out GameBot b) && b.Group == null && b.ObjectState == GameObject.eObjectState.Active ? 1 : 0);

            public GameLiving LivingLeader => _group != null
                ? (_group.TryGetTarget(out Group g) ? g.LivingLeader : null)
                : (_solo.TryGetTarget(out GameBot b) ? b : null);
        }

        /// <summary>One group (or solo bot): the fight in progress (or none), the quiet time since the last one, its file.</summary>
        private sealed class GroupLog
        {
            private readonly object _sync = new();
            private readonly Party _group;
            public Party Party => _group;
            public readonly int Number;
            public readonly string FilePath;
            private bool _headerWritten;
            private int _fights;

            private Fight _fight;
            /// <summary>Events seen before the tick noticed the fight (the first think can come first).</summary>
            private readonly List<(long T, string Text, GameLiving Who)> _early = new();
            private readonly Dictionary<GameLiving, (string Key, int Repeats)> _earlyKeys = new();

            // quiet time since the last fight
            private long _lastEnd;
            private long _quietMs, _quietMovingMs, _quietRestingMs;
            private int _quietCasts;
            private readonly Dictionary<string, long> _quietActivity = new();

            public GroupLog(Party group, int number)
            {
                _group = group;
                Number = number;
                string leader = SafeName(group.LivingLeader?.Name);
                FilePath = group.IsSolo
                    ? Path.Combine(SoloDirectory, $"{SessionStamp}_s{number:000}_{leader}.log")
                    : Path.Combine(Directory, $"{SessionStamp}_g{number:00}_{leader}.log");
            }

            public string TakeFileHeader()
            {
                lock (_sync)
                {
                    if (_headerWritten)
                        return null;
                    _headerWritten = true;
                    if (_group.IsSolo)
                        return $"SmartAI solo combat log: {_group.LivingLeader?.Name ?? "?"} fighting alone. Server started " +
                               $"{SessionStamp}. SmartAI {(SmartAiConfig.IsEnabled ? "on" : "off")}.{Environment.NewLine}" +
                               "Only complete fights are written (from the first blow until the bot is out of combat)." +
                               Environment.NewLine + "Times are seconds from the start of the fight. ! marks something worth a look." +
                               Environment.NewLine;
                    Party g = _group;
                    return $"SmartAI group combat log: group {Number}, led by {g?.LivingLeader?.Name ?? "?"}. Server started " +
                           $"{SessionStamp}. SmartAI {(SmartAiConfig.IsEnabled ? "on" : "off")}.{Environment.NewLine}" +
                           "Only complete fights are written (from the first blow until the whole group is out of combat)." +
                           Environment.NewLine + "Times are seconds from the start of the fight. ! marks something worth a look." +
                           Environment.NewLine;
                }
            }

            public bool DropFight()
            {
                lock (_sync)
                {
                    bool had = _fight != null;
                    _fight = null;
                    _early.Clear();
                    _earlyKeys.Clear();
                    return had;
                }
            }

            // ---- events

            public void Think(GameBot bot, SmartDecision d, SmartBotPerception? p, GameLiving target, string options)
            {
                long now = GameLoop.GameLoopTime;
                lock (_sync)
                {
                    Member m = _fight?.MemberOf(bot);
                    if (m != null)
                    {
                        m.Thinks++;
                        string family = d.Action;
                        m.Actions[family] = m.Actions.GetValueOrDefault(family) + 1;
                        if (d.Action == "Legacy" && d.Note.Length > 0)
                            m.LegacyReasons[d.Note] = m.LegacyReasons.GetValueOrDefault(d.Note) + 1;
                        m.LastThink = d.Action + (d.Note.Length > 0 ? $" ({Trim(d.Note, 40)})" : string.Empty);
                    }
                    // the timeline gets a decision only when it changes (action, or the reason of a legacy hand-off)
                    string key = d.Action + "|" + (d.Action is "Legacy" or "Hold" ? d.Note : string.Empty);
                    Dictionary<GameLiving, (string Key, int Repeats)> keys = _fight?.ThinkKeys ?? _earlyKeys;
                    if (keys.TryGetValue(bot, out var last) && last.Key == key)
                    {
                        keys[bot] = (key, last.Repeats + 1);
                        return;
                    }
                    keys[bot] = (key, 0);
                    var sb = new StringBuilder();
                    sb.Append(bot.Name).Append(' ').Append(d.Action);
                    if (d.Score > 0)
                        sb.Append(' ').Append(d.Score);
                    if (d.Action is "Legacy" or "Hold" or "Sit" or "Support turn" && d.Note.Length > 0)
                        sb.Append(": ").Append(Trim(d.Note, 70));
                    if (options != null)
                        sb.Append(" | options ").Append(options);
                    if (p is { } pv)
                    {
                        sb.Append(" | grp low ").Append(pv.GroupMinHealthPercent).Append("% pow ").Append(pv.SelfManaPercent).Append('%');
                        if (target is { } t)
                            sb.Append(" tgt ").Append(t.Name).Append(' ').Append(t.HealthPercent).Append("% @").Append((int)bot.GetDistanceTo(t));
                        if (pv.Mode != SmartGameMode.PvE)
                            sb.Append(' ').Append(pv.Mode);
                    }
                    Add(now, sb.ToString(), bot);
                }
            }

            public void Cast(GameBot bot, Spell spell, GameLiving target, bool started)
            {
                long now = GameLoop.GameLoopTime;
                lock (_sync)
                {
                    Member m = _fight?.MemberOf(bot);
                    if (_fight == null && _lastEnd > 0 && started)
                        _quietCasts++;
                    if (!started)
                    {
                        if (m != null)
                            m.Refused++;
                        return;
                    }
                    // started casts are counted here; the timeline line comes when the spell lands (SpellLanded)
                    if (m != null)
                        m.Casts++;
                }
            }

            public void SpellLanded(GameLiving caster, Spell spell, GameLiving target)
            {
                long now = GameLoop.GameLoopTime;
                lock (_sync)
                {
                    Member m = _fight?.MemberOf(caster);
                    if (m != null)
                    {
                        m.Landed++;
                        if (spell.IsHealing)
                            m.Heals++;
                        if (caster is GamePlayer)
                            m.Casts++;
                    }
                    if (target != null && spell.IsHarmful)
                        _fight?.SeeEnemy(target, now);
                    string on = target == null || target == caster ? string.Empty
                        : $" > {target.Name} {target.HealthPercent}%" + (target.CurrentRegionID == caster.CurrentRegionID ? $" @{(int)caster.GetDistanceTo(target)}" : string.Empty);
                    Add(now, $"{caster.Name} {spell.Name}{on} (pow {caster.ManaPercent}%)", caster);
                }
            }

            public void Swing(AttackData ad)
            {
                long now = GameLoop.GameLoopTime;
                GameLiving me = ad.Attacker, target = ad.Target;
                bool ranged = ad.AttackType == AttackData.eAttackType.Ranged;
                Position pos = PositionOf(target.GetAngle(me));
                lock (_sync)
                {
                    Member m = _fight?.MemberOf(me);
                    if (m == null)
                        return;
                    _fight.SeeEnemy(target, now);
                    if (ranged)
                    {
                        m.Shots++;
                        return;
                    }
                    m.Swings++;
                    m.Positions[(int)pos]++;
                    string result = ad.AttackResult switch
                    {
                        eAttackResult.HitStyle or eAttackResult.HitUnstyled => $"hit {ad.Damage + ad.CriticalDamage}",
                        _ => ad.AttackResult.ToString().ToLowerInvariant(),
                    };

                    // a reaction this member could make: its last parry, block or evade against this target
                    (GameLiving From, Style.eAttackResultRequirement Kind, long At)? pending = m.PendingReaction;
                    bool reactionOpen = pending is { } pr && pr.From == target && now - pr.At <= 6000;
                    m.PendingReaction = null;

                    Style style = ad.Style;
                    if (style != null)
                    {
                        bool executed = ad.AttackResult == eAttackResult.HitStyle;
                        string kind = StyleKind(style);
                        if (executed)
                        {
                            m.Styles[style.Name] = m.Styles.GetValueOrDefault(style.Name) + 1;
                            if (style.OpeningRequirementType == Style.eOpening.Defensive)
                                m.ReactionsUsed++;
                            Add(now, $"{me.Name} {style.Name} ({kind}) on {target.Name} from {pos.ToString().ToLowerInvariant()}: {result}", me);
                        }
                        else if (ad.AttackResult == eAttackResult.HitUnstyled)
                        {
                            // the swing landed but the style did not fire: its opening was not met
                            m.StyleFailed++;
                            string wrongPlace = style.OpeningRequirementType == Style.eOpening.Positional ? $", was at the {pos.ToString().ToLowerInvariant()}" : string.Empty;
                            Add(now, $"! {me.Name} {style.Name} ({kind}) did not fire{wrongPlace}; end {me.EndurancePercent}%", me);
                        }
                        else
                            m.StyleDefended++;
                    }
                    if (reactionOpen && (style == null || style.OpeningRequirementType != Style.eOpening.Defensive || ad.AttackResult != eAttackResult.HitStyle))
                    {
                        Style known = ReactiveStyleFor(me, pending.Value.Kind);
                        if (known != null)
                        {
                            m.ReactionsMissed++;
                            Add(now, $"! {me.Name} {Past(pending.Value.Kind)} {target.Name} but swung {(style != null ? style.Name : "unstyled")}, not {known.Name}", me);
                        }
                    }
                }
            }

            public void Defend(AttackData ad)
            {
                long now = GameLoop.GameLoopTime;
                lock (_sync)
                {
                    Member m = _fight?.MemberOf(ad.Target);
                    if (m == null)
                        return;
                    switch (ad.AttackResult)
                    {
                        case eAttackResult.Parried: m.Parries++; break;
                        case eAttackResult.Blocked: m.Blocks++; break;
                        case eAttackResult.Evaded: m.Evades++; break;
                    }
                    Style.eAttackResultRequirement kind = ReactionTo(ad.AttackResult);
                    m.ReactionWindows++;
                    m.PendingReaction = (ad.Attacker, kind, now);
                }
            }

            private static string Past(Style.eAttackResultRequirement kind) => kind switch
            {
                Style.eAttackResultRequirement.Parry => "parried",
                Style.eAttackResultRequirement.Block => "blocked",
                Style.eAttackResultRequirement.Evade => "evaded",
                _ => "defended against",
            };

            public void Attack(GameBot bot, GameLiving target)
            {
                long now = GameLoop.GameLoopTime;
                bool ranged = bot.ActiveWeaponSlot == eActiveWeaponSlot.Distance;
                string why = null;
                bool wrong = !ranged && MeleeIsWrong(bot, out why);
                lock (_sync)
                {
                    Member m = _fight?.MemberOf(bot);
                    if (m != null)
                    {
                        if (ranged) m.Ranged++; else m.Melee++;
                        if (wrong) m.WrongMelee++;
                    }
                    _fight?.SeeEnemy(target, now);
                    string dist = target.CurrentRegionID == bot.CurrentRegionID ? $" @{(int)bot.GetDistanceTo(target)}" : string.Empty;
                    string text = $"{bot.Name} {(ranged ? "starts shooting" : "starts melee on")} {target.Name} {target.HealthPercent}%{dist}";
                    if (wrong)
                        text = $"! {text} - {why}; pow {bot.ManaPercent}%; last think: {m?.LastThink ?? "none (legacy AI)"}";
                    Add(now, text, bot);
                }
            }

            private void Add(long now, string text, GameLiving who)
            {
                if (_fight != null)
                    _fight.Add(now, text);
                else if (_early.Count < 40)
                    _early.Add((now, text, who));
            }

            // ---- the tick: start, follow and end fights

            /// <summary>Returns the text of a fight that just ended, or null.</summary>
            public string Update(long now)
            {
                if (!_group.TryGet(out Party group))
                    return null;
                List<GameLiving> members = group.GetMembersInTheGroup();
                long lastBlow = 0;
                bool fighting = false;
                foreach (GameLiving m in members)
                {
                    long blow = LastBlow(m);
                    if (blow > lastBlow)
                        lastBlow = blow;
                    fighting |= InCombat(m, now);
                }

                lock (_sync)
                {
                    if (_fight == null)
                    {
                        if (!fighting)
                        {
                            Quiet(group, members, now);
                            if (now - (_early.Count > 0 ? _early[^1].T : 0) > CombatWindowMs)
                            {
                                _early.Clear();
                                _earlyKeys.Clear();
                            }
                            return null;
                        }
                        StartFight(group, members, now);
                    }

                    Fight f = _fight;
                    f.Observe(group, members, now);
                    if (lastBlow > f.LastBlow)
                        f.LastBlow = lastBlow;
                    FightEnd state = EndOf(fighting, f.HeldEnemyNear(members), now, f.Start, f.LastBlow);
                    if (state == FightEnd.Ongoing)
                        return null;

                    // complete: the whole group is out of combat
                    string note = state switch
                    {
                        FightEnd.TooLong => $"closed after {MaxFightMs / 60000} minutes (still fighting)",
                        FightEnd.Stale => $"closed: no blow for {StaleCloseMs / 1000} s with an enemy still listed",
                        _ => null,
                    };
                    string text = f.Render(group, members, now, ++_fights, note);
                    _fight = null;
                    _lastEnd = Math.Max(f.LastBlow, f.Start);
                    _quietMs = _quietMovingMs = _quietRestingMs = 0;
                    _quietCasts = 0;
                    _quietActivity.Clear();
                    return text;
                }
            }

            private void StartFight(Party group, List<GameLiving> members, long now)
            {
                // the first blow: the earliest recent blow of any member
                long start = now;
                foreach (GameLiving m in members)
                {
                    long blow = LastBlow(m);
                    if (blow > 0 && now - blow <= CombatWindowMs && blow < start)
                        start = blow;
                }
                var quiet = _lastEnd > 0
                    ? new QuietSummary(Math.Max(0, start - _lastEnd), _quietMovingMs, _quietRestingMs, _quietCasts, LowestPower(members),
                        _quietActivity.OrderByDescending(a => a.Value).Take(3).Select(a => $"{Trim(a.Key, 50)} {a.Value / 1000}s").ToArray())
                    : null;
                _fight = new Fight(start, quiet);
                foreach (GameLiving m in members)
                    _fight.AddMember(m, joined: false);
                foreach (var e in _early)
                    if (e.T >= start - PreStartSlackMs)
                        _fight.Add(e.T, e.Text);
                foreach (var k in _earlyKeys)
                    _fight.ThinkKeys[k.Key] = k.Value;
                _early.Clear();
                _earlyKeys.Clear();
            }

            private void Quiet(Party group, List<GameLiving> members, long now)
            {
                if (_lastEnd == 0)
                    return;
                GameLiving leader = group.LivingLeader;
                _quietMs += TickMs;
                if (members.Any(m => m is GameBot { IsRecoveryResting: true } || m.IsSitting))
                    _quietRestingMs += TickMs;
                else if (leader?.IsMoving == true)
                    _quietMovingMs += TickMs;
                if (leader is GameBot lb && lb.PersistentRecord is { } rec)
                {
                    string activity = string.IsNullOrWhiteSpace(rec.CurrentGoal) ? rec.Activity : rec.CurrentGoal;
                    if (!string.IsNullOrWhiteSpace(activity))
                        _quietActivity[activity] = _quietActivity.GetValueOrDefault(activity) + TickMs;
                }
            }
        }

        private sealed record QuietSummary(long Ms, long MovingMs, long RestingMs, int Casts, string LowestPowerAtPull, string[] Activities);

        /// <summary>"Name 62%": the member lowest on power (casters only), or null.</summary>
        private static string LowestPower(List<GameLiving> members)
        {
            GameLiving low = members.Where(m => m.IsAlive && m.MaxMana > 0).OrderBy(m => m.ManaPercent).FirstOrDefault();
            return low == null ? null : $"{low.Name} {low.ManaPercent}%";
        }

        private sealed class Member
        {
            public GameLiving Living;
            public string Name, Kind, Class, Spec, Role, Personality;
            public int Level, StartHp, StartPow, MinHp = 100, MinPow = 100;
            public bool Joined, Died, WasAlive = true, Seated;
            public int Thinks, Casts, Landed, Heals, Refused, Melee, Ranged, WrongMelee, Sits;
            public int Swings, Shots, StyleFailed, StyleDefended, Parries, Blocks, Evades;
            public int ReactionWindows, ReactionsUsed, ReactionsMissed;
            public readonly int[] Positions = new int[3];
            public readonly Dictionary<string, int> Styles = new();
            public (GameLiving From, Style.eAttackResultRequirement Kind, long At)? PendingReaction;
            public long SeatedSince, SeatedMs;
            public string LastThink;
            public readonly Dictionary<string, int> Actions = new();
            public readonly Dictionary<string, int> LegacyReasons = new();
        }

        private sealed class Enemy
        {
            public GameLiving Living;
            public string Name;
            public int Level;
            public long Seen;
            public long? Died;
        }

        /// <summary>One fight in progress.</summary>
        private sealed class Fight
        {
            public readonly long Start;
            public long LastBlow;
            public readonly QuietSummary Quiet;
            public readonly DateTime StartedAt = DateTime.Now;
            public readonly List<Member> Members = new();
            public readonly Dictionary<GameLiving, Enemy> Enemies = new();
            public readonly Dictionary<GameLiving, (string Key, int Repeats)> ThinkKeys = new();
            private readonly List<(long T, string Text)> _timeline = new();
            private int _dropped;
            private string _zone;
            private string _groupView = string.Empty;
            private int _groupLow = 100;
            private string _groupLowWho;

            public Fight(long start, QuietSummary quiet)
            {
                Start = start;
                LastBlow = start;
                Quiet = quiet;
            }

            public Member MemberOf(GameLiving living)
            {
                foreach (Member m in Members)
                    if (m.Living == living)
                        return m;
                return null;
            }

            public void Add(long t, string text)
            {
                if (_timeline.Count >= MaxTimelineLines)
                {
                    _dropped++;
                    return;
                }
                _timeline.Add((t, text));
            }

            public void AddMember(GameLiving living, bool joined)
            {
                GameBot bot = living as GameBot;
                SmartSpecDefinition spec = bot != null && SmartAiConfig.IsEnabled ? SmartAiSpecRegistry.Active.Resolve(bot) : null;
                string role = spec?.Role;
                if (string.IsNullOrEmpty(role))
                {
                    var cls = living switch { GamePlayer p => p.CharacterClass, GameBot b => b.CharacterClass, _ => null };
                    role = cls == null ? "?" : BotPartyRoles.For((eCharacterClass)cls.ID).ToString();
                }
                Members.Add(new Member
                {
                    Living = living,
                    Name = living.Name,
                    Kind = Kind(living),
                    Class = living switch { GamePlayer p => p.CharacterClass?.Name, GameBot b => b.CharacterClass?.Name, _ => null } ?? "?",
                    Level = living.Level,
                    Spec = bot == null ? "-" : !SmartAiConfig.IsEnabled ? "legacy AI (SmartAI off)" : spec?.Name ?? "legacy AI (no spec)",
                    Role = role,
                    Personality = bot != null && spec != null ? SmartPersonalityRegistry.Resolve(bot, spec).ToString() : "-",
                    StartHp = living.HealthPercent,
                    StartPow = living.MaxMana > 0 ? living.ManaPercent : -1,
                    MinHp = living.HealthPercent,
                    MinPow = living.MaxMana > 0 ? living.ManaPercent : 100,
                    WasAlive = living.IsAlive,
                    Joined = joined,
                });
            }

            public void SeeEnemy(GameLiving enemy, long now)
            {
                if (enemy == null || Enemies.ContainsKey(enemy))
                    return;
                Enemies[enemy] = new Enemy { Living = enemy, Name = enemy.Name, Level = enemy.Level, Seen = now };
            }

            /// <summary>Each tick: members' health and power, deaths, sitting, enemies and their deaths, the group view.</summary>
            public void Observe(Party group, List<GameLiving> members, long now)
            {
                _zone ??= group.LivingLeader?.CurrentZone?.Description;
                foreach (GameLiving l in members)
                {
                    Member m = MemberOf(l);
                    if (m == null)
                    {
                        AddMember(l, joined: true);
                        m = Members[^1];
                        Add(now, $"{l.Name} joins the group");
                    }
                    if (l.IsAlive)
                    {
                        m.MinHp = Math.Min(m.MinHp, l.HealthPercent);
                        if (l.MaxMana > 0)
                            m.MinPow = Math.Min(m.MinPow, l.ManaPercent);
                        if (l.HealthPercent < _groupLow)
                        {
                            _groupLow = l.HealthPercent;
                            _groupLowWho = l.Name;
                        }
                    }
                    if (m.WasAlive && !l.IsAlive)
                    {
                        m.Died = true;
                        Add(now, $"! {l.Name} dies");
                    }
                    else if (!m.WasAlive && l.IsAlive)
                        Add(now, $"{l.Name} is back on its feet");
                    m.WasAlive = l.IsAlive;

                    bool seated = l is GameBot { IsCombatSeated: true };
                    if (seated != m.Seated)
                    {
                        if (seated)
                        {
                            m.Sits++;
                            m.SeatedSince = now;
                        }
                        else
                            m.SeatedMs += now - m.SeatedSince;
                        Add(now, $"{l.Name} {(seated ? "sits" : "stands")} (pow {l.ManaPercent}%)");
                        m.Seated = seated;
                    }

                    if (l.attackComponent?.AttackerTracker?.Attackers is { } attackers)
                        foreach (GameLiving a in attackers)
                            if (a is { IsAlive: true } && !Enemies.ContainsKey(a))
                            {
                                SeeEnemy(a, now);
                                Add(now, $"{a.Name} (L{a.Level}) attacks {l.Name}");
                            }
                    if (l.IsAttacking && l.TargetObject is GameLiving target && target.IsAlive && !Enemies.ContainsKey(target))
                    {
                        SeeEnemy(target, now);
                        if (l is GamePlayer)
                            Add(now, $"{l.Name} attacks {target.Name} (L{target.Level})");
                    }
                }
                foreach (Enemy e in Enemies.Values)
                    if (e.Died == null && !e.Living.IsAlive)
                    {
                        e.Died = now;
                        Add(now, $"{e.Name} dies");
                    }

                if (group.Group is { } realGroup && SmartGroupTacticalState.TryGet(realGroup, out SmartGroupTacticalState state) &&
                    state.Current is { Version: > 0 } s &&
                    s.IsFresh(now))
                {
                    string view = $"target {s.PrimaryTarget?.Name ?? "none"}, assist {s.MainAssist?.Name ?? "none"}, {s.Strategy}" +
                                  (s.PeelTarget != null ? $", peel {s.PeelTarget.Name}" : string.Empty);
                    if (view != _groupView)
                    {
                        _groupView = view;
                        Add(now, $"[group] {view}" + (s.StrategyReason.Length > 0 ? $" ({Trim(s.StrategyReason, 50)})" : string.Empty));
                    }
                }
            }

            /// <summary>A mezzed or rooted enemy still near the group: the fight isn't over.</summary>
            public bool HeldEnemyNear(List<GameLiving> members)
            {
                foreach (Enemy e in Enemies.Values)
                {
                    GameLiving x = e.Living;
                    if (!x.IsAlive || x.ObjectState != GameObject.eObjectState.Active || !(x.IsMezzed || BotBrain.IsBreakablyControlled(x)))
                        continue;
                    foreach (GameLiving m in members)
                        if (m.IsAlive && m.CurrentRegionID == x.CurrentRegionID && m.IsWithinRadius(x, HeldEnemyRange))
                            return true;
                }
                return false;
            }

            public string Render(Party group, List<GameLiving> members, long now, int number, string note)
            {
                long end = Math.Max(LastBlow, Start);
                foreach (Member m in Members)
                    if (m.Seated)
                        m.SeatedMs += now - m.SeatedSince;
                var nl = Environment.NewLine;
                var sb = new StringBuilder();
                int kills = Enemies.Values.Count(e => e.Died != null);
                int deaths = Members.Count(m => m.Died);
                sb.Append(nl).Append($"=== Fight {number} | {StartedAt:yyyy-MM-dd HH:mm:ss} | {(end - Start) / 1000.0:0}s | {_zone ?? "?"} | ")
                  .Append($"kills {kills}/{Enemies.Count}, deaths {deaths}, lowest health {_groupLow}%{(_groupLowWho != null ? $" ({_groupLowWho})" : string.Empty)} ===").Append(nl);
                if (note != null)
                    sb.Append("! ").Append(note).Append(nl);

                // before the pull
                if (Quiet == null)
                    sb.Append("Before: first fight recorded for this group.").Append(nl);
                else
                {
                    long other = Math.Max(0, Quiet.Ms - Quiet.MovingMs - Quiet.RestingMs);
                    sb.Append($"Before: {(Quiet.Ms >= 60_000 ? "! " : string.Empty)}{Quiet.Ms / 1000}s since the last fight " +
                              $"(resting {Quiet.RestingMs / 1000}s, leader moving {Quiet.MovingMs / 1000}s, other {other / 1000}s; " +
                              $"{Quiet.Casts} casts)");
                    if (Quiet.LowestPowerAtPull != null)
                        sb.Append("; lowest power at the pull: ").Append(Quiet.LowestPowerAtPull);
                    if (Quiet.Activities.Length > 0)
                        sb.Append("; leader goal: ").Append(string.Join(", ", Quiet.Activities));
                    sb.Append(nl);
                }

                // makeup
                sb.Append("Group (start hp/pow -> end, lowest):").Append(nl);
                foreach (Member m in Members)
                {
                    GameLiving l = m.Living;
                    string pow = m.StartPow < 0 ? string.Empty : $" pow {m.StartPow}->{(l.MaxMana > 0 ? l.ManaPercent : 0)}% (min {m.MinPow})";
                    sb.Append($"  {m.Name}{(l == group.LivingLeader ? "*" : string.Empty)} | {m.Kind} | {m.Class} {m.Level} | {m.Spec} | {m.Role}" +
                              (m.Personality != "-" ? $" | {m.Personality}" : string.Empty) +
                              $" | hp {m.StartHp}->{l.HealthPercent}% (min {m.MinHp}){pow}" +
                              (m.Died ? " | DIED" : string.Empty) + (m.Joined ? " | joined mid-fight" : string.Empty) + (!members.Contains(l) ? " | left" : string.Empty))
                      .Append(nl);
                }

                // enemies
                if (Enemies.Count > 0)
                    sb.Append("Enemies: ").Append(string.Join(", ", Enemies.Values.OrderBy(e => e.Seen).Select(e =>
                        $"{e.Name} L{e.Level} {(e.Died is long d ? $"died +{(d - Start) / 1000.0:0.0}" : e.Living.IsAlive ? $"alive {e.Living.HealthPercent}%" : "gone")}"))).Append(nl);

                // timeline
                sb.Append("Timeline:").Append(nl);
                foreach (var (t, text) in _timeline.OrderBy(e => e.T))
                    sb.Append($"  {(t - Start) / 1000.0,6:+0.0;-0.0}  {text}").Append(nl);
                if (_dropped > 0)
                    sb.Append($"  ... {_dropped} more events not shown").Append(nl);

                // summary
                sb.Append("Summary:").Append(nl);
                foreach (Member m in Members)
                {
                    bool isBot = m.Living is GameBot;
                    var parts = new List<string>();
                    if (isBot)
                        parts.Add(m.Thinks > 0
                            ? $"thinks {m.Thinks}: " + string.Join(", ", m.Actions.OrderByDescending(a => a.Value).Select(a => $"{a.Key} {a.Value}"))
                            : "no SmartAI thinks");
                    if (m.Casts > 0 || m.Landed > 0 || m.Refused > 0)
                    {
                        int interrupted = isBot ? Math.Max(0, m.Casts - m.Landed) : 0;
                        parts.Add($"spells {m.Landed} landed (heals {m.Heals})" +
                                  (interrupted > 0 ? $", {interrupted} not finished" : string.Empty) +
                                  (m.Refused > 0 ? $", {m.Refused} refused" : string.Empty));
                    }
                    if (m.Swings > 0)
                    {
                        string styles = m.Styles.Count == 0 ? "no styles"
                            : "styles " + string.Join(", ", m.Styles.OrderByDescending(s => s.Value).Take(5).Select(s => $"{s.Key} {s.Value}"));
                        parts.Add($"swings {m.Swings} (front {m.Positions[0]}/side {m.Positions[1]}/back {m.Positions[2]}), {styles}" +
                                  (m.StyleFailed > 0 ? $", {m.StyleFailed} styles did not fire" : string.Empty));
                    }
                    if (m.Shots > 0)
                        parts.Add($"shots {m.Shots}");
                    if (m.ReactionWindows > 0)
                        parts.Add($"defended: parry {m.Parries}, block {m.Blocks}, evade {m.Evades}; reactive styles used {m.ReactionsUsed}" +
                                  (m.ReactionsMissed > 0 ? $", missed {m.ReactionsMissed} it knew" : string.Empty));
                    if (m.Sits > 0)
                        parts.Add($"sat {m.Sits}x {m.SeatedMs / 1000}s");
                    if (parts.Count == 0)
                        continue;
                    sb.Append($"  {m.Name}: {string.Join(" | ", parts)}").Append(nl);
                    foreach (var r in m.LegacyReasons.OrderByDescending(r => r.Value).Take(3))
                        sb.Append($"      legacy {r.Value}x: {Trim(r.Key, 90)}").Append(nl);
                }

                // flags
                var flags = new List<string>();
                foreach (Member m in Members.Where(m => m.Living is GameBot))
                {
                    if (m.WrongMelee > 0)
                        // Attack starts, not swings: a bot that stops and restarts its attack every think shows many
                        // starts and few swings.
                        flags.Add($"{m.Name} ({m.Class}) started melee {m.WrongMelee}x ({m.Swings} swings)");
                    if (m.Casts == 0 && m.Landed == 0 && m.Melee == 0 && m.Ranged == 0 && m.Swings == 0 && m.Shots == 0 && !m.Died)
                        flags.Add($"{m.Name} did nothing (no cast, no attack)");
                    if (m.ReactionsMissed > 0)
                        flags.Add($"{m.Name} missed {m.ReactionsMissed} reactive style(s) it knew");
                    if (m.Living is GameBot sb2 && m.Swings >= 6 && (sb2.CanUseSideStyles && m.Positions[1] + m.Positions[2] > 0 || sb2.CanUseBackStyles && m.Positions[2] > 0) &&
                        !m.Styles.Keys.Any(n => (sb2.StylesSide?.Any(s => s.Name == n) ?? false) || (sb2.StylesBack?.Any(s => s.Name == n) ?? false)))
                        flags.Add($"{m.Name} swung from the side/back but used no positional style");
                    if (m.Thinks > 0 && m.Actions.GetValueOrDefault("Legacy") * 2 > m.Thinks)
                        flags.Add($"{m.Name} handed most thinks to the legacy AI");
                    if (m.Refused >= 5)
                        flags.Add($"{m.Name}: {m.Refused} casts refused");
                }
                if (Quiet is { Ms: >= 60_000 })
                    flags.Add($"{Quiet.Ms / 1000}s between fights");
                if (flags.Count > 0)
                    sb.Append("Flags: ").Append(string.Join("; ", flags)).Append(nl);
                return sb.ToString();
            }
        }

        // ---- shared helpers

        public enum FightEnd { Ongoing, Complete, Stale, TooLong }

        /// <summary>
        /// Whether a fight is over, on its own for tests. It is complete only when no member (or pet) is in combat and no
        /// held enemy is near. Two safety closes keep a file from waiting forever: no blow for StaleCloseMs while an
        /// enemy is still listed, and a fight longer than MaxFightMs; both are written with a note.
        /// </summary>
        public static FightEnd EndOf(bool anyMemberFighting, bool heldEnemyNear, long now, long start, long lastBlow)
        {
            if (now - start > MaxFightMs)
                return FightEnd.TooLong;
            if (!anyMemberFighting && !heldEnemyNear)
                return FightEnd.Complete;
            return now - lastBlow > StaleCloseMs ? FightEnd.Stale : FightEnd.Ongoing;
        }

        /// <summary>A member fought this recently (its own or its pet's last blow), on its own for tests.</summary>
        public static bool RecentBlow(long now, long lastBlow) => lastBlow > 0 && now - lastBlow < CombatWindowMs;

        private static long LastBlow(GameLiving m)
        {
            long t = Math.Max(m.LastAttackTick, m.LastAttackedByEnemyTick);
            GameNPC pet = m.ControlledBrain?.Body;
            if (pet != null)
                t = Math.Max(t, Math.Max(pet.LastAttackTick, pet.LastAttackedByEnemyTick));
            return t;
        }

        /// <summary>The member, or its pet, fought in the last few seconds, is attacking, or is casting harm.</summary>
        private static bool InCombat(GameLiving m, long now)
        {
            if (!m.IsAlive)
                return false;
            if (RecentBlow(now, LastBlow(m)))
                return true;
            if (m.IsAttacking && m.TargetObject is GameLiving { IsAlive: true })
                return true;
            if (m.IsCasting && m.castingComponent?.SpellHandler?.Spell?.IsHarmful == true)
                return true;
            GameNPC pet = m.ControlledBrain?.Body;
            return pet is { IsAlive: true } && (pet.IsAttacking && pet.TargetObject is GameLiving { IsAlive: true });
        }

        private static string Trim(string s, int max) => s == null ? string.Empty : s.Length <= max ? s : s[..(max - 1)] + "~";
    }
}
