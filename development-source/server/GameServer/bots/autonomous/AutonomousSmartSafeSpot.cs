using System;
using System.Collections.Generic;
using System.Numerics;
using DOL.AI.Brain;
using DOL.GS.SmartAI;

namespace DOL.GS
{
    /// <summary>
    /// A spot near a place that no aggressive monster's aggro reaches (owner 2026-11-05: "if someone was resurrected
    /// they should attempt to move out of harms way if there are aggressive monsters near by"). SmartAI only.
    /// - Threats: live aggressive monsters (aggro level and range above 0) within the camp search radius, not grey to
    ///   the group, not already fighting.
    /// - A spot is safe when it is outside every threat's aggro range plus the aggro margin (smartai-world.json).
    /// - Candidates: the place itself, then rings of 500, 1,000 and 1,500 units in eight directions; each must be on
    ///   the navmesh and reachable from the start.
    /// Nothing is moved by this class; callers walk there.
    /// </summary>
    public static class AutonomousSmartSafeSpot
    {
        public readonly record struct Threat(float X, float Y, int AggroRange);

        private static readonly int[] Rings = { 500, 1000, 1500 };

        /// <summary>Pure: a spot is clear of every threat's aggro (plus margin).</summary>
        public static bool IsClear(Vector3 spot, IReadOnlyList<Threat> threats, int margin)
        {
            foreach (Threat t in threats)
            {
                float reach = t.AggroRange + margin;
                float dx = spot.X - t.X, dy = spot.Y - t.Y;
                if (dx * dx + dy * dy <= reach * reach)
                    return false;
            }
            return true;
        }

        /// <summary>Pure: the candidate spots, nearest first.</summary>
        public static IEnumerable<Vector3> Candidates(Vector3 around)
        {
            yield return around;
            foreach (int r in Rings)
                for (int i = 0; i < 8; i++)
                {
                    double a = i * Math.PI / 4;
                    yield return new Vector3(around.X + (float)(Math.Cos(a) * r), around.Y + (float)(Math.Sin(a) * r), around.Z);
                }
        }

        /// <summary>The live threats near a place, seen from a group of <paramref name="groupLevel"/>.</summary>
        public static List<Threat> ThreatsNear(Region region, Vector3 around, int groupLevel, int radius)
        {
            var result = new List<Threat>();
            if (region == null)
                return result;
            var npcs = new List<GameNPC>();
            region.GetInRadius(new Point3D((int)around.X, (int)around.Y, (int)around.Z), eGameObjectType.NPC,
                (ushort)Math.Clamp(radius, 1, ushort.MaxValue), npcs);
            foreach (GameNPC npc in npcs)
            {
                if (npc is not { IsAlive: true } || npc is GameBot || npc.InCombat || npc.Realm != eRealm.None ||
                    npc.Brain is not StandardMobBrain mob || mob.AggroLevel <= 0 || mob.AggroRange <= 0 ||
                    Math.Abs(npc.Z - around.Z) > 600)
                    continue;
                if (ConLevels.GetConColor(ConLevels.GetConLevel(Math.Max(0, groupLevel), npc.Level)) == ConColor.GREY)
                    continue;
                result.Add(new Threat(npc.X, npc.Y, mob.AggroRange));
            }
            return result;
        }

        /// <summary>
        /// The nearest reachable safe spot around <paramref name="around"/>, starting from <paramref name="start"/>.
        /// False when the place is already safe (<paramref name="alreadySafe"/>) or no candidate works.
        /// </summary>
        public static bool TryFind(Region region, Vector3 start, Vector3 around, int groupLevel, out Vector3 spot,
            out bool alreadySafe, out int threatCount)
        {
            spot = around;
            alreadySafe = false;
            threatCount = 0;
            if (region == null)
                return false;
            SmartWorldConfig.Settings settings = SmartWorldConfig.Current;
            List<Threat> threats = ThreatsNear(region, around, groupLevel, settings.CampSearchRadius);
            threatCount = threats.Count;
            if (IsClear(around, threats, settings.AggroMargin))
            {
                alreadySafe = true;
                return false;
            }
            IPathfindingMgr nav = PathfindingProvider.Instance;
            foreach (Vector3 candidate in Candidates(around))
            {
                if (!IsClear(candidate, threats, settings.AggroMargin))
                    continue;
                Zone zone = region.GetZone((int)candidate.X, (int)candidate.Y);
                if (zone == null || !AutonomousRendezvousNavigation.TryChoosePoint(nav, zone, candidate, out Vector3 point) ||
                    !IsClear(point, threats, settings.AggroMargin))
                    continue;
                Zone startZone = region.GetZone((int)start.X, (int)start.Y);
                if (startZone == zone && !AutonomousRendezvousNavigation.CanReachFrom(nav, zone, start, point))
                    continue;
                spot = point;
                return true;
            }
            return false;
        }
    }
}
