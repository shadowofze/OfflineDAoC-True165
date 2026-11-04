using System;
using System.Collections.Generic;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// SmartAI's single-threaded pre-pass, run by NpcService.Tick before the parallel brain thinks (plan Phases 3 and 7,
    /// architecture sections 2.1 and 4.2). For each group whose SmartAI bots are fighting it builds and publishes the
    /// group's tactical snapshot and prunes expired claims, so every bot's Think reads one consistent view. Suggestion
    /// arbitration (Phase 8) joins here. NpcService calls it only while SmartAiConfig.IsEnabled.
    /// </summary>
    public static class SmartAiCoordinator
    {
        /// <summary>A snapshot older than this is stale (its group stopped publishing).</summary>
        public const long SnapshotFreshMs = 1_000;

        /// <summary>The game loop time of the last completed pre-pass (0 = none yet).</summary>
        public static long LastPreparedTick { get; private set; }

        /// <summary>Groups published by the last pre-pass (diagnostics).</summary>
        public static int LastPublishedGroups { get; private set; }

        public static void PrepareTick()
        {
            if (!SmartAiConfig.IsEnabled)
                return;

            long now = GameLoop.GameLoopTime;
            int published = 0;
            try
            {
                foreach (KeyValuePair<Group, SmartGroupTacticalState> pair in SmartGroupTacticalState.All)
                {
                    SmartGroupTacticalState state = pair.Value;
                    if (!state.IsActive(now))
                        continue;
                    try
                    {
                        Group group = pair.Key;
                        SmartGroupTacticalSnapshot previous = state.Current;
                        List<SmartBotObservation> observations = state.DrainObservations();
                        SmartArbitration arbitration = observations.Count == 0
                            ? SmartArbitration.None
                            : SmartGroupCoordinator.Arbitrate(observations, SmartGroupSnapshotBuilder.Context(group, previous, now));
                        SmartGroupTacticalSnapshot snapshot = SmartGroupSnapshotBuilder.Build(group, previous, now, arbitration, observations.Count);
                        state.Publish(snapshot);
                        state.PruneClaims(now);
                        Callout(group, state, arbitration, snapshot, now);
                        published++;
                    }
                    catch (Exception e)
                    {
                        // One group's bad state never stops the others: report it and let that group run legacy.
                        state.Publish(SmartGroupTacticalSnapshot.Empty);
                        SmartAiFallback.ReportFailure("group snapshot", e);
                    }
                }
                LastPublishedGroups = published;
                LastPreparedTick = now;
            }
            catch (Exception e)
            {
                // Never let SmartAI stop the NPC service: report it and let the brains run legacy this tick.
                SmartAiFallback.ReportFailure(nameof(PrepareTick), e);
            }
        }

        /// <summary>The leading observation as party chat, for a group with a player (SmartGroupCalloutPolicy).</summary>
        private static void Callout(Group group, SmartGroupTacticalState state, SmartArbitration arbitration,
            SmartGroupTacticalSnapshot snapshot, long now)
        {
            if (!SmartAiProperties.SMART_AI_GROUP_CALLOUTS || arbitration.Lead is not { } lead || lead.Poster is not GameLiving poster ||
                group.GetPlayersInTheGroup().Count == 0)
                return;
            // A target call is said only when the group actually switched.
            if (lead.Kind is SmartObservationKind.TargetVulnerable or SmartObservationKind.RecommendTargetSwitch &&
                (arbitration.TargetSwitch == null || snapshot.PrimaryTarget != arbitration.TargetSwitch))
                return;
            string text = SmartGroupCalloutPolicy.Text(lead, (lead.Target as GameLiving)?.Name);
            if (text == null || !state.Callouts.TryAllow(lead, now))
                return;
            group.SendMessageToGroupMembers(poster, text, PacketHandler.eChatType.CT_Group, PacketHandler.eChatLoc.CL_ChatWindow);
        }
    }
}
