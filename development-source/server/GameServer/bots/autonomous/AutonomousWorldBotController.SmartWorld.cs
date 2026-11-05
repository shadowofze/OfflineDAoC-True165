using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DOL.AI.Brain;
using DOL.GS.SmartAI;

namespace DOL.GS
{
    /// <summary>
    /// SmartAI world logic for autonomous groups (owner 2026-11-05; SmartAI on only - with it off nothing here runs):
    /// the zone levels and camp area search built with the catalog (SmartWorldKnowledge), the camp choice
    /// (SmartCampChooser), learning from kills and deaths (SmartWorldLearning) and route guarding for members who
    /// travel on their own.
    /// </summary>
    public sealed partial class AutonomousWorldBotController
    {
        /// <summary>Catalog worker: hands the snapshot to SmartWorldKnowledge (no live objects).</summary>
        private static void BuildSmartWorldKnowledge(CampMonster[] snapshot, CampCatalogCell[] cells)
        {
            if (!SmartAiConfig.IsEnabled)
                return;
            try
            {
                var monsters = new SmartWorldKnowledge.Monster[snapshot.Length];
                for (int i = 0; i < snapshot.Length; i++)
                {
                    CampMonster m = snapshot[i];
                    string zone = m.CurrentZone?.Description ?? m.CurrentZone?.ZoneRegion?.Description ?? string.Empty;
                    monsters[i] = new(m.Name, m.CurrentRegionID, zone, m.CurrentZone?.IsDungeon == true ||
                        m.CurrentZone?.ZoneRegion?.IsDungeon == true, m.X, m.Y, m.Z, m.EffectiveLevel, m.AggroRange, m.Aggressive);
                }
                SmartWorldKnowledge.CampSite[] camps = cells
                    .Select(c => new SmartWorldKnowledge.CampSite(c.Id, c.MonsterName, c.RegionId, c.X, c.Y, c.Z))
                    .ToArray();
                SmartWorldKnowledge.Rebuild(monsters, camps, SmartWorldConfig.Current.CampSearchRadius);
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("world knowledge", e);
            }
        }

        /// <summary>
        /// SmartAI group camp choice over the planner's legal camps. Null when SmartAI does not apply here (the caller
        /// keeps the legacy draw).
        /// </summary>
        private AutonomousBotDecisionEngine.Camp SmartChooseGroupCamp(AutonomousBotDecisionEngine.Camp[] legalCells,
            Dictionary<string, CampDestination> destinations, int planningLevel, SmartPullBand band, out string why)
        {
            why = null;
            if (band == null || legalCells.Length == 0 || !SmartAiConfig.IsEnabled)
                return null;
            try
            {
                var candidates = new List<SmartCampChooser.Candidate>(legalCells.Length);
                var byId = new Dictionary<string, AutonomousBotDecisionEngine.Camp>(StringComparer.OrdinalIgnoreCase);
                foreach (AutonomousBotDecisionEngine.Camp camp in legalCells)
                {
                    if (!destinations.TryGetValue(camp.Id, out CampDestination d))
                        continue;
                    candidates.Add(new(camp.Id, camp.ZoneName, camp.RegionId, d.X, d.Y, camp.AverageMobLevel, camp.LiveMobCount));
                    byId[camp.Id] = camp;
                }
                SmartCampChooser.Choice choice = SmartCampChooser.Choose(candidates, planningLevel, band, Random.Shared);
                if (choice == null || !byId.TryGetValue(choice.Camp.Id, out AutonomousBotDecisionEngine.Camp chosen))
                    return null;
                why = $"SmartAI camp: {chosen.MonsterName} L{chosen.AverageMobLevel} in {chosen.ZoneName}; {choice.Why}; {band}";
                Log.Info($"SMARTAI_GROUP_CAMP group={_groupDirective?.GroupId} camp=\"{chosen.Id}\" {why}");
                return chosen;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("group camp choice", e);
                return null;
            }
        }

        /// <summary>A grouped bot under SmartAI: its kills and deaths teach SmartWorldLearning.</summary>
        private static bool LearnsFrom(GameBot bot) =>
            SmartAiConfig.IsEnabled && bot?.IsAutonomousWorldBot == true && bot.Group?.MemberCount > 1;

        public static void SmartLearnKill(GameBot bot)
        {
            if (!LearnsFrom(bot))
                return;
            try { SmartWorldLearning.RecordKill(bot.PersistentRecord?.CurrentCampId); }
            catch (Exception e) { SmartAiFallback.ReportFailure("learning (kill)", e); }
        }

        public static void SmartLearnDeath(GameBot bot)
        {
            if (!LearnsFrom(bot))
                return;
            try
            {
                string campId = bot.PersistentRecord?.CurrentCampId;
                bool atCamp = !string.IsNullOrWhiteSpace(campId) &&
                              SmartWorldKnowledge.TryGetSpot(campId, out var spot) && spot.Region == bot.CurrentRegionID &&
                              Vector2.Distance(new(spot.X, spot.Y), new(bot.X, bot.Y)) <= SmartWorldLearning.AtCampRadius;
                SmartWorldLearning.RecordDeath(campId, atCamp, bot.CurrentRegionID, bot.X, bot.Y);
            }
            catch (Exception e) { SmartAiFallback.ReportFailure("learning (death)", e); }
        }

        /// <summary>
        /// A SmartAI group member walking on its own (to the meeting point, back after a death, or far behind its
        /// leader) looks ahead for aggressive monsters and goes around them; it never pulls (only the party's puller
        /// does). Returns what GuardOpenWorldTravel returns.
        /// </summary>
        private bool GuardLoneMemberTravel(GameBot bot, Vector3 destination, GameBot leader)
        {
            if (!SmartAiConfig.IsEnabled || _groupDirective?.IsDynamic != true)
                return false;
            long now = GameLoop.GameLoopTime;
            // Walking a detour of our own: keep it until reached (or 20 s), whatever the destination does meanwhile
            // (a formation slot moves with the leader, and a new destination would drop a shared detour waypoint).
            if (_loneDetour is Vector3 detour)
            {
                if (now > _loneDetourUntil || bot.InCombat ||
                    Vector2.Distance(new(detour.X, detour.Y), new(bot.X, bot.Y)) <= 80)
                    _loneDetour = null;
                else
                {
                    IssuePath(bot, detour);
                    return true;
                }
            }
            if (leader != null && leader != bot && leader.CurrentRegionID == bot.CurrentRegionID &&
                Vector2.Distance(new(leader.X, leader.Y), new(bot.X, bot.Y)) < SmartWorldConfig.Current.LoneMemberDistance)
                return false;
            try
            {
                Vector3? before = _routeRecoveryWaypoint;
                if (GuardOpenWorldTravel(bot, destination, loneMember: true))
                    return true;
                if (_routeRecoveryWaypoint is Vector3 set && set != before)
                {
                    _loneDetour = set;
                    _loneDetourUntil = now + 20_000;
                    IssuePath(bot, set);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("lone member route guard", e);
                return false;
            }
        }

        private Vector3? _loneDetour;
        private long _loneDetourUntil;

        // ---- binding at the meet-up town

        private string _boundForGroup;
        private long _bindTripUntil;
        /// <summary>A bind stone farther than this from the meeting point is not the town's stone.</summary>
        public const int MeetupBindReach = 2_500;
        public const long BindTripMilliseconds = 60_000;

        /// <summary>
        /// SmartAI (owner 2026-11-05: "bind at the closest bind stone where they meet up"): a member that has reached
        /// the meeting point of a new PvE party binds at that town's stone, as a player would - it walks into the
        /// stone's bind area and binds where it stands. A member who dies on the long walk to the camp then releases
        /// near the party's road, not at whatever stone is nearest its body. One try per party (60 s), skipped when
        /// the town has no stone of its realm within 2,500 units or the member is already bound there. True while the
        /// member walks to the stone.
        /// </summary>
        private bool SmartBindAtMeetup(GameBot bot, AutonomousBotGroupCoordinator.Directive directive)
        {
            if (!SmartAiConfig.IsEnabled || directive == null || directive.ObjectiveKind != eAutonomousObjectiveKind.GroupPve ||
                directive.Phase != "Meeting up" || bot.PersistentRecord is not { } record ||
                string.Equals(_boundForGroup, directive.GroupId, StringComparison.Ordinal) ||
                bot.CurrentRegionID != directive.RendezvousRegion || bot.InCombat)
                return false;
            try
            {
                Point3D stone = BotReleaseBindPoints.Nearest(directive.RendezvousRegion, (int)directive.Rendezvous.X,
                    (int)directive.Rendezvous.Y, bot.Realm);
                long now = GameLoop.GameLoopTime;
                bool done = stone == null ||
                            Vector2.Distance(new(stone.X, stone.Y), new(directive.Rendezvous.X, directive.Rendezvous.Y)) > MeetupBindReach ||
                            record.BindRegionId == directive.RendezvousRegion &&
                            Vector2.Distance(new(stone.X, stone.Y), new(record.BindX, record.BindY)) <= 800;
                if (done)
                {
                    _boundForGroup = directive.GroupId;
                    _bindTripUntil = 0;
                    return false;
                }
                if (InOwnBindArea(bot))
                {
                    record.BindRegionId = bot.CurrentRegionID;
                    record.BindX = bot.X;
                    record.BindY = bot.Y;
                    record.BindZ = bot.Z;
                    bot.MarkAutonomousStateDirty();
                    _boundForGroup = directive.GroupId;
                    _bindTripUntil = 0;
                    Log.Info($"SMARTAI_BIND bot={bot.Name} group={directive.GroupId} town=\"{directive.RendezvousName}\" " +
                             $"at={bot.CurrentRegionID}:{bot.X},{bot.Y},{bot.Z}");
                    return false;
                }
                if (_bindTripUntil == 0)
                    _bindTripUntil = now + BindTripMilliseconds;
                if (now > _bindTripUntil)
                {
                    Log.Info($"SMARTAI_BIND_GAVE_UP bot={bot.Name} group={directive.GroupId} town=\"{directive.RendezvousName}\"");
                    _boundForGroup = directive.GroupId;
                    _bindTripUntil = 0;
                    return false;
                }
                // Stand beside the stone (its collision is not in the navmesh), on the meeting point's side.
                Vector2 away = new(directive.Rendezvous.X - stone.X, directive.Rendezvous.Y - stone.Y);
                away = away.LengthSquared() > 1 ? Vector2.Normalize(away) : new Vector2(1, 0);
                Vector3 target = new(stone.X + away.X * 160, stone.Y + away.Y * 160, stone.Z);
                IssuePath(bot, target);
                SetStatus(bot, "Binding at the meeting town", directive.SharedGoal,
                    $"Binding at {directive.RendezvousName}'s bind stone before the party sets out");
                return true;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("meet-up bind", e);
                _boundForGroup = directive.GroupId;
                return false;
            }
        }

        private static bool InOwnBindArea(GameBot bot)
        {
            if (bot.CurrentZone?.GetAreasOfSpot(bot) is not { } areas)
                return false;
            foreach (IArea area in areas)
                if (area is Area.BindArea bind && bind.BindPoint is { } point &&
                    (point.Realm == 0 || point.Realm == (int)bot.Realm))
                    return true;
            return false;
        }

        // ---- after a resurrection

        private Vector3? _reviveRetreat;
        private long _reviveRetreatUntil;
        public const long ReviveRetreatMilliseconds = 25_000;

        private static int GroupLevel(GameBot bot) =>
            bot.Group?.GetMembersInTheGroup() is { Count: > 0 } members
                ? (int)Math.Round(members.Average(m => m.Level)) : bot.Level;

        /// <summary>Raised beside aggressive monsters: remember the nearest safe spot to walk to (SmartAI only).</summary>
        public void SmartAfterRevive(GameBot bot)
        {
            if (!SmartAiConfig.IsEnabled || bot?.IsAutonomousWorldBot != true || bot.IsPlayerLedGroup ||
                bot.CurrentRegion == null || bot.CurrentZone?.IsDungeon == true)
                return;
            try
            {
                Vector3 here = new(bot.X, bot.Y, bot.Z);
                if (!AutonomousSmartSafeSpot.TryFind(bot.CurrentRegion, here, here, GroupLevel(bot), out Vector3 spot,
                        out bool safe, out int threats))
                {
                    if (!safe)
                        Log.Info($"SMARTAI_REVIVE_NO_SAFE_SPOT bot={bot.Name} threats={threats} at={bot.CurrentRegionID}:{bot.X},{bot.Y}");
                    return;
                }
                _reviveRetreat = spot;
                _reviveRetreatUntil = GameLoop.GameLoopTime + ReviveRetreatMilliseconds;
                Log.Info($"SMARTAI_REVIVE_RETREAT bot={bot.Name} threats={threats} from={bot.X},{bot.Y} to={(int)spot.X},{(int)spot.Y}");
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("revive retreat", e);
            }
        }

        /// <summary>Walks a raised bot to its safe spot; true while it is on the way. A fight ends the retreat.</summary>
        private bool SmartReviveRetreat(GameBot bot)
        {
            if (_reviveRetreat is not Vector3 spot)
                return false;
            if (!bot.IsAlive || bot.InCombat || bot.IsAttacking || (bot.Brain as BotBrain)?.HasAggro == true ||
                GameLoop.GameLoopTime > _reviveRetreatUntil ||
                Vector2.Distance(new(spot.X, spot.Y), new(bot.X, bot.Y)) <= 80)
            {
                _reviveRetreat = null;
                return false;
            }
            IssuePath(bot, spot);
            SetStatus(bot, "Moving out of harm's way", GoalText(),
                "Raised beside aggressive monsters; stepping out of their aggro before recovering");
            return true;
        }
    }
}
