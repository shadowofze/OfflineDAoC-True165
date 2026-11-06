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
                // Crowding: leave out camps where other parties already camp close by (owner 2026-11-05).
                SmartWorldConfig.Settings settings = SmartWorldConfig.Current;
                Group own = _groupDirective?.Leader?.Group;
                IReadOnlyList<SmartCampChooser.Candidate> pool = SmartCampChooser.Uncrowded(candidates,
                    c => AutonomousBotGroupCoordinator.OtherPartiesCampingNear(own, c.Region, c.X, c.Y, settings.GroupCrowdRadius),
                    settings.MaxOtherGroupsNearCamp, out int crowded);
                SmartCampChooser.Choice choice = SmartCampChooser.Choose(pool, planningLevel, band, Random.Shared);
                if (choice == null || !byId.TryGetValue(choice.Camp.Id, out AutonomousBotDecisionEngine.Camp chosen))
                    return null;
                why = $"SmartAI camp: {chosen.MonsterName} L{chosen.AverageMobLevel} in {chosen.ZoneName}; {choice.Why}" +
                      (crowded > 0 ? $"; {crowded} camp(s) skipped for other parties nearby" : string.Empty) + $"; {band}";
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

        /// <summary>The camp a grouped bot is working: its own record (only the planning leader has one), else the
        /// party's shared camp. Run 3 (2026-11-05) filed 39 of 42 camp deaths without a camp because members had none.</summary>
        private static string CampOf(GameBot bot)
        {
            string own = bot.PersistentRecord?.CurrentCampId;
            if (!string.IsNullOrWhiteSpace(own))
                return own;
            return (bot.Brain as BotBrain)?.AutonomousController?._groupDirective?.Camp?.Id;
        }

        public static void SmartLearnKill(GameBot bot)
        {
            if (LearnsSolo(bot))
            {
                try { SmartWorldLearning.RecordSolo(SoloSpecKey(bot), died: false); }
                catch (Exception e) { SmartAiFallback.ReportFailure("learning (solo kill)", e); }
                return;
            }
            if (!LearnsFrom(bot))
                return;
            try { SmartWorldLearning.RecordKill(CampOf(bot)); }
            catch (Exception e) { SmartAiFallback.ReportFailure("learning (kill)", e); }
        }

        /// <summary>A SmartAI world bot fighting alone: its kills and deaths teach its spec's solo con ceiling.</summary>
        private static bool LearnsSolo(GameBot bot) =>
            SmartAiConfig.IsEnabled && bot?.IsAutonomousWorldBot == true && !(bot.Group?.MemberCount > 1) && !bot.IsPlayerLedGroup &&
            !AutonomousObjectiveAssignments.Is(bot, eAutonomousObjectiveKind.RvR);

        /// <summary>"Bard / Regrowth Bard": the class and the SmartAI spec it plays (the class alone without one).</summary>
        public static string SoloSpecKey(GameBot bot)
        {
            string spec = SmartAiSpecRegistry.Active.Resolve(bot)?.Name;
            return string.IsNullOrEmpty(spec) ? bot.ClassName : $"{bot.ClassName} / {spec}";
        }

        /// <summary>
        /// SmartAI: the solo con ceiling, lowered for a spec that dies too often alone (SmartWorldLearning.SoloConSteps),
        /// never below green.
        /// </summary>
        private ConColor SmartSoloConCeiling(GameBot bot, ConColor ceiling)
        {
            if (!LearnsSolo(bot))
                return ceiling;
            try
            {
                string key = SoloSpecKey(bot);
                int steps = SmartWorldLearning.SoloConStepsFor(key, out double kills, out double deaths);
                if (steps <= 0)
                    return ceiling;
                ConColor lowered = (ConColor)Math.Max((int)ConColor.GREEN, (int)ceiling - steps);
                if (lowered != ceiling && GameLoop.GameLoopTime >= _nextSoloCeilingLog)
                {
                    _nextSoloCeilingLog = GameLoop.GameLoopTime + 10 * 60_000;
                    Log.Info($"SMARTAI_SOLO_CEILING bot={bot.Name} spec=\"{key}\" kills={kills:0.#} deaths={deaths:0.#} " +
                             $"ceiling={ceiling.ToString().ToLowerInvariant()}->{lowered.ToString().ToLowerInvariant()}");
                }
                return lowered;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("solo con ceiling", e);
                return ceiling;
            }
        }

        private long _nextSoloCeilingLog;

        public static void SmartLearnDeath(GameBot bot)
        {
            // Solo bots teach the areas only (where travelling is deadly, for the route planner): run 4 (2026-11-05) had
            // 1,604 of its 1,885 deaths in solo bots. Their camps stay out of the group camp statistics.
            bool solo = SmartAiConfig.IsEnabled && bot?.IsAutonomousWorldBot == true && !(bot.Group?.MemberCount > 1);
            if (!LearnsFrom(bot) && !solo)
                return;
            try
            {
                string campId = CampOf(bot);
                bool atCamp = !string.IsNullOrWhiteSpace(campId) &&
                              SmartWorldKnowledge.TryGetSpot(campId, out var spot) && spot.Region == bot.CurrentRegionID &&
                              Vector2.Distance(new(spot.X, spot.Y), new(bot.X, bot.Y)) <= SmartWorldLearning.AtCampRadius;
                if (solo)
                {
                    if (!atCamp && bot.CurrentRegion?.IsDungeon != true)
                        SmartWorldLearning.RecordDeath(null, false, bot.CurrentRegionID, bot.X, bot.Y, victimLevel: bot.Level);
                    if (LearnsSolo(bot))
                        SmartWorldLearning.RecordSolo(SoloSpecKey(bot), died: true);
                    return;
                }
                SmartWorldLearning.RecordDeath(campId, atCamp, bot.CurrentRegionID, bot.X, bot.Y, victimLevel: bot.Level);
            }
            catch (Exception e) { SmartAiFallback.ReportFailure("learning (death)", e); }
        }

        // ---- solo death loops (SmartDeathLoop; run 6, 2026-11-05)

        private readonly List<SmartDeathLoop.Death> _smartSoloDeaths = new();
        private readonly List<SmartDeathLoop.Area> _smartLoopAreas = new();

        /// <summary>A solo SmartAI bot's death: three in one area within 30 minutes make it a loop area for an hour.</summary>
        private void SmartObserveSoloDeath(GameBot bot, string goalAtDeath, string killer)
        {
            if (!LearnsSolo(bot) || bot.LastDeathSpot is not { Region: > 0 } spot)
                return;
            try
            {
                long now = GameLoop.GameLoopTime;
                if (SmartDeathLoop.RecordDeath(_smartSoloDeaths, new(spot.Region, spot.X, spot.Y, now)) is not { } area)
                    return;
                SmartDeathLoop.Remember(_smartLoopAreas, area, now);
                Log.Info($"SMARTAI_SOLO_LOOP bot={bot.Name} level={bot.Level} class=\"{bot.ClassName}\" area={area.Region}:{area.X},{area.Y} " +
                         $"deaths={SmartDeathLoop.LoopDeaths}+ in {SmartDeathLoop.LoopWindowMilliseconds / 60_000} min killer=\"{killer}\" " +
                         $"goal=\"{goalAtDeath}\" avoid_min={SmartDeathLoop.AvoidMilliseconds / 60_000}");
            }
            catch (Exception e) { SmartAiFallback.ReportFailure("solo death loop", e); }
        }

        /// <summary>
        /// A solo SmartAI bot takes no camp in its own region whose straight way passes one of its loop areas (all of
        /// them when every camp does).
        /// </summary>
        private AutonomousBotDecisionEngine.Camp[] SmartAvoidLoopAreas(GameBot bot, AutonomousBotDecisionEngine.Camp[] camps,
            Dictionary<string, CampDestination> destinations)
        {
            if (!LearnsSolo(bot) || _smartLoopAreas.Count == 0 || camps.Length <= 1)
                return camps;
            try
            {
                long now = GameLoop.GameLoopTime;
                AutonomousBotDecisionEngine.Camp[] clear = camps.Where(camp =>
                    !destinations.TryGetValue(camp.Id, out CampDestination d) || d.RegionId != bot.CurrentRegionID ||
                    SmartDeathLoop.Crossed(_smartLoopAreas, d.RegionId, bot.X, bot.Y, d.X, d.Y, now) == null).ToArray();
                if (clear.Length == 0 || clear.Length == camps.Length)
                    return camps;
                Log.Info($"SMARTAI_SOLO_LOOP_CAMPS bot={bot.Name} skipped={camps.Length - clear.Length} kept={clear.Length}");
                return clear;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("solo death loop (camps)", e);
                return camps;
            }
        }

        /// <summary>
        /// SmartAI: a walk to a merchant or other service follows a planned way around danger too (run 6: those walks
        /// were never planned). False when a solo bot's straight way passes one of its loop areas and no planned way goes
        /// around it: the bot then skips the trip. <paramref name="leg"/> is where to walk now (a waypoint or the point).
        /// </summary>
        private bool SmartServiceLeg(GameBot bot, Vector3 point, string service, out Vector3 leg)
        {
            leg = point;
            if (!SmartRoutesApply(bot))
                return true;
            try
            {
                leg = SmartRouteTarget(bot, point, plan: true);
                if (_smartRoute != null || !LearnsSolo(bot))
                    return true;
                long now = GameLoop.GameLoopTime;
                if (SmartDeathLoop.Crossed(_smartLoopAreas, bot.CurrentRegionID, bot.X, bot.Y, (int)point.X, (int)point.Y, now) is not { } area)
                    return true;
                Log.Info($"SMARTAI_SOLO_LOOP_SKIP bot={bot.Name} service=\"{service}\" area={area.Region}:{area.X},{area.Y} " +
                         $"from={bot.X},{bot.Y} to={(int)point.X},{(int)point.Y}");
                leg = point;
                return false;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("service route", e);
                leg = point;
                return true;
            }
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

        // ---- the camp's setup spot

        public const int SetupSpotArrivalRadius = 150;

        /// <summary>
        /// SmartAI (owner 2026-11-05: "groups need to pick a safe spot to setup camp at and have the puller pull back
        /// to that camp so they don't keep getting attacked by respawns"): the nearest reachable spot within 1,500 units
        /// of the spawns that no aggressive monster's aggro reaches (AutonomousSmartSafeSpot - the camp's own
        /// monsters included when they are aggressive). The party walks to it and stands there; every PvE party of
        /// three or more already pulls from range back to itself (AutonomousDefensivePull). Outdoors only; the camp is
        /// unchanged when the spawn point is already safe or no spot is found.
        /// </summary>
        private CampDestination SmartCampSetupSpot(CampDestination camp, int groupLevel)
        {
            if (!SmartAiConfig.IsEnabled || camp == null || camp.IsDungeon || camp.SetupSpot)
                return camp;
            try
            {
                Region region = WorldMgr.GetRegion(camp.RegionId);
                if (region == null || region.IsDungeon)
                    return camp;
                Vector3 anchor = new(camp.X, camp.Y, camp.Z);
                if (!AutonomousSmartSafeSpot.TryFind(region, anchor, anchor, groupLevel, out Vector3 spot, out bool safe, out int threats))
                {
                    Log.Info($"SMARTAI_CAMP_SETUP camp=\"{camp.Id}\" spot=spawns reason={(safe ? "already-safe" : "no-safe-spot")} threats={threats}");
                    return camp;
                }
                Log.Info($"SMARTAI_CAMP_SETUP camp=\"{camp.Id}\" spot={(int)spot.X},{(int)spot.Y} moved={(int)Vector3.Distance(anchor, spot)} threats={threats}");
                return camp with
                {
                    X = (int)Math.Round(spot.X), Y = (int)Math.Round(spot.Y), Z = (int)Math.Round(spot.Z), SetupSpot = true,
                };
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("camp setup spot", e);
                return camp;
            }
        }

        // ---- route planning (SmartRoutePlanner)

        private List<Vector3> _smartRoute;
        private int _smartRouteNext;
        private Vector3 _smartRouteDestination;
        private ushort _smartRouteRegion;
        private long _smartRouteLegSince;
        private Vector3? _smartRouteTriedDestination;
        private long _smartRouteTriedTick;
        /// <summary>Walks shorter than this are not planned.</summary>
        public const int SmartRouteMinDistance = 3_000;
        /// <summary>A destination this close to the planned one keeps the plan (a formation slot moves with its leader).</summary>
        public const int SmartRouteSameDestination = 1_500;
        public const int SmartRouteWaypointReach = 300;
        public const long SmartRouteRetryMilliseconds = 15_000;
        /// <summary>A leg not finished in this time drops the plan (the bot then walks straight, guarded as before).</summary>
        public const long SmartRouteLegMilliseconds = 90_000;
        /// <summary>Each hard aggressive spawn in a cell adds this much danger; each learned death there adds 1.</summary>
        public const double SmartRouteSpawnDanger = 0.5;

        /// <summary>Pure: the lowest level of a monster that counts as a hard one for a traveller (orange and up for
        /// low levels; improvised for higher levels, where con steps are wider).</summary>
        public static int SmartRouteHardLevel(int travellerLevel) => travellerLevel + 2 + travellerLevel / 10;

        private bool SmartRoutesApply(GameBot bot) =>
            SmartAiConfig.IsEnabled && bot?.IsAutonomousWorldBot == true && !bot.IsPlayerLedGroup && !bot.IsOnStableMasterRoute &&
            bot.CurrentZone is { IsDungeon: false } && bot.CurrentRegion is { IsDungeon: false } &&
            !AutonomousObjectiveAssignments.Is(bot, eAutonomousObjectiveKind.RvR) && AutonomousRealmRaid.GetView(bot.Group) == null;

        /// <summary>
        /// SmartAI (owner 2026-11-05, "Setup Routes"): where a bot walking to <paramref name="destination"/> should head
        /// now: the next waypoint of a planned way around learned deaths and hard aggressive spawns, or the destination
        /// itself. With <paramref name="plan"/>, a long walk (3,000+ units, same zone) to a new destination is planned.
        /// </summary>
        private Vector3 SmartRouteTarget(GameBot bot, Vector3 destination, bool plan)
        {
            if (!SmartRoutesApply(bot))
            {
                _smartRoute = null;
                return destination;
            }
            try
            {
                long now = GameLoop.GameLoopTime;
                Vector2 here = new(bot.X, bot.Y), to = new(destination.X, destination.Y);
                if (_smartRoute != null && _smartRouteRegion == bot.CurrentRegionID &&
                    Vector2.Distance(new(_smartRouteDestination.X, _smartRouteDestination.Y), to) <= SmartRouteSameDestination)
                {
                    while (_smartRouteNext < _smartRoute.Count &&
                           Vector2.Distance(here, new(_smartRoute[_smartRouteNext].X, _smartRoute[_smartRouteNext].Y)) <= SmartRouteWaypointReach)
                    {
                        _smartRouteNext++;
                        _smartRouteLegSince = now;
                    }
                    if (_smartRouteNext >= _smartRoute.Count)
                    {
                        _smartRoute = null;
                        return destination;
                    }
                    if (now - _smartRouteLegSince > SmartRouteLegMilliseconds && !bot.InCombat)
                    {
                        Log.Info($"SMARTAI_ROUTE_DROPPED bot={bot.Name} leg={_smartRouteNext + 1}/{_smartRoute.Count} reason=slow-leg");
                        _smartRoute = null;
                        return destination;
                    }
                    return _smartRoute[_smartRouteNext];
                }
                // Another destination: a short hop (a detour, a formation step) leaves the plan alone; a long walk
                // elsewhere replaces it.
                if (!plan || Vector2.Distance(here, to) < SmartRouteMinDistance)
                    return destination;
                _smartRoute = null;
                if (_smartRouteTriedDestination is Vector3 tried &&
                    Vector2.Distance(new(tried.X, tried.Y), to) <= SmartRouteSameDestination &&
                    now - _smartRouteTriedTick < SmartRouteRetryMilliseconds)
                    return destination;
                _smartRouteTriedDestination = destination;
                _smartRouteTriedTick = now;
                List<Vector3> route = PlanSmartRoute(bot, destination, out string why, out string declined, out double straightDanger);
                if (route == null)
                {
                    CountRouteNone(declined, now);
                    if (straightDanger >= SmartRoutePlanner.MinStraightDanger)
                        Log.Info($"SMARTAI_ROUTE_NONE bot={bot.Name} level={bot.Level} reason={declined} danger={straightDanger:0.#} " +
                                 $"zone=\"{bot.CurrentZone?.Description}\" from={bot.X},{bot.Y} to={(int)destination.X},{(int)destination.Y} " +
                                 $"length={Vector2.Distance(here, to):0}");
                    return destination;
                }
                _smartRoute = route;
                _smartRouteNext = 0;
                _smartRouteDestination = destination;
                _smartRouteRegion = bot.CurrentRegionID;
                _smartRouteLegSince = now;
                Log.Info($"SMARTAI_ROUTE bot={bot.Name} level={bot.Level} group={(_groupDirective?.IsDynamic == true ? _groupDirective.GroupId : "solo")} " +
                         $"zone=\"{bot.CurrentZone?.Description}\" from={bot.X},{bot.Y} to={(int)destination.X},{(int)destination.Y} " +
                         $"waypoints={route.Count} {why}");
                return route[0];
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("route planning", e);
                _smartRoute = null;
                return destination;
            }
        }

        /// <summary>
        /// The plan in world points on the navmesh, every leg walkable; null when there is none worth taking, with
        /// <paramref name="declined"/> saying why (SmartRoutePlanner.Find's reasons, or "zone", "no-navmesh", "no-floor",
        /// "waypoint-off-mesh", "end-corridor") and <paramref name="straightDanger"/> the straight way's danger.
        /// </summary>
        private static List<Vector3> PlanSmartRoute(GameBot bot, Vector3 destination, out string why, out string declined,
            out double straightDanger)
        {
            why = null;
            declined = null;
            straightDanger = 0;
            Zone zone = bot.CurrentZone;
            Region region = bot.CurrentRegion;
            if (zone == null || region == null)
            {
                declined = "zone";
                return null;
            }
            ushort regionId = bot.CurrentRegionID;
            int level = bot.Level;
            int hard = SmartRouteHardLevel(level);
            DateTime utc = DateTime.UtcNow;
            int half = SmartRoutePlanner.CellSize / 2;
            double Danger(int cx, int cy) =>
                SmartRouteSpawnDanger * SmartWorldKnowledge.HardAggressiveIn(regionId, cx, cy, hard) +
                SmartWorldLearning.AreaDanger(regionId, cx * SmartRoutePlanner.CellSize + half, cy * SmartRoutePlanner.CellSize + half, level, utc);
            straightDanger = SmartRoutePlanner.SegmentDanger(bot.X, bot.Y, (int)destination.X, (int)destination.Y, Danger);
            if (region.GetZone((int)destination.X, (int)destination.Y) != zone)
            {
                declined = "zone";
                return null;
            }
            IPathfindingMgr nav = PathfindingProvider.Instance;
            if (!nav.IsAvailable || !nav.HasNavmesh(zone))
            {
                declined = "no-navmesh";
                return null;
            }
            var bounds = new SmartRoutePlanner.Bounds(zone.XOffset, zone.YOffset, zone.XOffset + zone.Width, zone.YOffset + zone.Height);
            SmartRoutePlanner.Plan plan = SmartRoutePlanner.Find(bot.X, bot.Y, (int)destination.X, (int)destination.Y, bounds, Danger,
                out declined, out straightDanger);
            if (plan == null)
                return null;
            if (!AutonomousNavigationSurface.TryFloor(nav, zone, new(bot.X, bot.Y, bot.Z), out Vector3 previous))
            {
                declined = "no-floor";
                return null;
            }
            var points = new List<Vector3>(plan.Waypoints.Count);
            float z = bot.Z;
            foreach ((int x, int y) in plan.Waypoints)
            {
                Vector3? floor = nav.GetClosestPoint(zone, new Vector3(x, y, z), 384, 384, 2_000, nav.DefaultFilters);
                if (floor is not Vector3 point || region.GetZone((int)point.X, (int)point.Y) != zone ||
                    !AutonomousZoneItinerary.HasCompleteCorridor(nav, zone, previous, point))
                {
                    declined = "waypoint-off-mesh";
                    return null;
                }
                points.Add(point);
                previous = point;
                z = point.Z;
            }
            Vector3 end = AutonomousNavigationSurface.TryFloor(nav, zone, destination, out Vector3 endFloor) ? endFloor : destination;
            if (!AutonomousZoneItinerary.HasCompleteCorridor(nav, zone, previous, end))
            {
                declined = "end-corridor";
                return null;
            }
            why = $"danger {plan.StraightDanger:0.#}->{plan.PlannedDanger:0.#} length {plan.StraightLength:0}->{plan.PlannedLength:0}";
            return points;
        }

        // ---- declined plans (run 6, 2026-11-05: the bots that died 12-39 times on one square planned 0-4 routes, and no
        // line said why): every declined plan is counted by reason, logged every 10 minutes; a declined walk whose
        // straight way crosses danger is logged on its own (the retry delay above keeps it to one line per walk).

        private static readonly string[] RouteNoneReasons =
            { "safe", "grid", "outside", "no-path", "straight", "longer", "not-safer", "zone", "no-navmesh", "no-floor", "waypoint-off-mesh", "end-corridor" };
        private static readonly long[] RouteNoneCounts = new long[RouteNoneReasons.Length];
        private static long _routeNoneReportTick;

        private static void CountRouteNone(string reason, long now)
        {
            int i = Array.IndexOf(RouteNoneReasons, reason);
            if (i >= 0)
                System.Threading.Interlocked.Increment(ref RouteNoneCounts[i]);
            long next = System.Threading.Interlocked.Read(ref _routeNoneReportTick);
            if (now < next || System.Threading.Interlocked.CompareExchange(ref _routeNoneReportTick, now + 10 * 60_000L, next) != next)
                return;
            if (next == 0)
                return; // the first call only starts the clock
            var line = new System.Text.StringBuilder("SMARTAI_ROUTE_NONE last10min");
            for (int r = 0; r < RouteNoneReasons.Length; r++)
                line.Append(' ').Append(RouteNoneReasons[r]).Append('=').Append(System.Threading.Interlocked.Exchange(ref RouteNoneCounts[r], 0));
            Log.Info(line.ToString());
        }

        // ---- camp stalls

        private long _smartReadySinceTick;
        /// <summary>The leader counts as at its camp within this distance of the camp spot.</summary>
        public const int StallCampReach = 2_500;

        /// <summary>
        /// Pure: whether a party that has stood ready at its camp since <paramref name="readySince"/> has stalled.
        /// </summary>
        public static bool IsCampStall(long readySince, long now, int stallSeconds) =>
            stallSeconds > 0 && readySince > 0 && now - readySince >= stallSeconds * 1000L;

        /// <summary>
        /// SmartAI (owner 2026-11-05, "Hibernia is slacking"): the leader of a grinding party that stands at its camp
        /// with every member alive, out of combat and ready (the 90/80/80 pull threshold) for CampStallSeconds, with
        /// nothing pulled, records a stall for the camp and leaves it for another. In run 4 nine parties (eight
        /// Hibernian, most at Lough Derg water beetles) stood like that until the 15-minute stuck watchdog sent a
        /// member to its capital. True when the camp was left.
        /// </summary>
        private bool SmartCampStalled(GameBot bot)
        {
            if (!SmartAiConfig.IsEnabled || _groupDirective is not { IsDynamic: true } directive || directive.Leader != bot ||
                directive.ObjectiveKind != eAutonomousObjectiveKind.GroupPve || directive.Phase != "Grinding" ||
                directive.Camp is not { } camp || bot.Group == null)
            {
                _smartReadySinceTick = 0;
                return false;
            }
            try
            {
                long now = GameLoop.GameLoopTime;
                bool ready = bot.CurrentRegionID == camp.RegionId && Distance(bot.X, bot.Y, camp.X, camp.Y) <= StallCampReach;
                if (ready)
                {
                    foreach (GameLiving member in bot.Group.GetMembersInTheGroup())
                    {
                        if (member == null || !member.IsAlive || member.InCombat || member.IsAttacking ||
                            member is GameBot { IsReturningAfterRelease: true } ||
                            !AutonomousGroupRecoveryState.ResourcesReady(member.HealthPercent, member.ManaPercent,
                                member.EndurancePercent, member.MaxMana > 0))
                        {
                            ready = false;
                            break;
                        }
                    }
                }
                if (!ready)
                {
                    _smartReadySinceTick = 0;
                    return false;
                }
                if (_smartReadySinceTick == 0)
                    _smartReadySinceTick = now;
                int stallSeconds = SmartWorldConfig.Current.CampStallSeconds;
                if (!IsCampStall(_smartReadySinceTick, now, stallSeconds))
                    return false;
                _smartReadySinceTick = 0;
                Log.Warn($"SMARTAI_CAMP_STALL group={directive.GroupId} camp=\"{camp.Id}\" target=\"{camp.MonsterName}\" " +
                         $"zone=\"{camp.ZoneName}\" seconds={stallSeconds} at={bot.X},{bot.Y},{bot.Z}");
                SmartWorldLearning.RecordStall(camp.Id);
                string reason = $"SmartAI: the party stood ready at the camp for {stallSeconds}s and nothing was pulled";
                if (_camp != null && string.Equals(_camp.Id, camp.Id, StringComparison.OrdinalIgnoreCase))
                    AbandonCamp(bot, reason);
                else
                    AutonomousBotGroupCoordinator.RejectUnreachableCamp(bot, camp.Id, reason);
                return true;
            }
            catch (Exception e)
            {
                SmartAiFallback.ReportFailure("camp stall", e);
                return false;
            }
        }

        /// <summary>The party's ranged pull gave a target up as unreachable: SmartAI learning marks the camp.</summary>
        private void SmartLearnPullRelease(GameBot bot)
        {
            if (!AutonomousDefensivePull.TakeNewRelease(bot?.Group) || !LearnsFrom(bot))
                return;
            try
            {
                string campId = CampOf(bot);
                Log.Info($"SMARTAI_CAMP_PULL_RELEASED bot={bot.Name} camp=\"{campId}\"");
                SmartWorldLearning.RecordStall(campId);
            }
            catch (Exception e) { SmartAiFallback.ReportFailure("learning (pull release)", e); }
        }

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

        // ---- solo bots rest before they travel

        private bool _smartTravelResting;

        /// <summary>
        /// Pure: a solo bot too weak to set off: below 60% health, or a power user below 40% power. Once it has sat
        /// down it rests on to the pull threshold (90% health, 80% power and endurance), as a group does.
        /// </summary>
        public static bool SoloTooWeakToTravel(int health, int power, int endurance, bool usesPower, bool resting) =>
            resting ? !AutonomousGroupRecoveryState.ResourcesReady(health, power, endurance, usesPower)
                    : health < 60 || usesPower && power < 40;

        private static bool UsesPowerToFight(GameBot bot) =>
            bot.MaxMana > 0 && (BotSpellPower.IsOffensiveCaster(bot) ||
                                bot.CharacterClass != null && BotPartyRoles.IsHealingClass((eCharacterClass)bot.CharacterClass.ID));

        /// <summary>
        /// SmartAI (2026-11-05 run 5): a solo bot below that sits down before walking anywhere. Released bots come back
        /// with a third of their health and power, and walked straight to a healer or a vendor: 35 of 35 solo casters who
        /// started a fight on the way to a healer died, and 21 of 21 on the way to a vendor. Not while something stops it
        /// resting (a fight, a cast, aggro), nor during the step out of harm's way after a resurrection. True while it
        /// rests (the walk waits).
        /// </summary>
        private bool SmartRestBeforeTravel(GameBot bot)
        {
            if (!SmartAiConfig.IsEnabled || bot?.IsAutonomousWorldBot != true || bot.Group != null || bot.IsPlayerLedGroup ||
                _reviveRetreat != null || bot.InCombat || bot.IsOnStableMasterRoute || !bot.IsAlive)
            {
                _smartTravelResting = false;
                return false;
            }
            bool usesPower = UsesPowerToFight(bot);
            if (!SoloTooWeakToTravel(bot.HealthPercent, bot.ManaPercent, bot.EndurancePercent, usesPower, _smartTravelResting))
            {
                if (_smartTravelResting)
                {
                    _smartTravelResting = false;
                    bot.WakeRecoveryRest();
                }
                return false;
            }
            if (BotRestRecovery.BlocksRest(bot))
                return false;
            bot.StopMovingOnPath();
            bot.StopMoving();
            if (!_smartTravelResting)
                Log.Info($"SMARTAI_REST_BEFORE_TRAVEL bot={bot.Name} level={bot.Level} class=\"{bot.ClassName}\" hp={bot.HealthPercent}% " +
                         $"power={bot.ManaPercent}% goal=\"{bot.PersistentRecord?.CurrentGoal}\"");
            _smartTravelResting = true;
            bot.BeginRecoveryRest();
            SetStatus(bot, "Resting before travelling", GoalText(),
                $"Recovering before the walk: HP {bot.HealthPercent}% • power {bot.ManaPercent}% • endurance {bot.EndurancePercent}%");
            return true;
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
