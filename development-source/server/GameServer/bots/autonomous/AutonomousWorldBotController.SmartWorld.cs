using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    }
}
