using System;
using System.Linq;
using DOL.GS.Commands;
using DOL.GS.PacketHandler;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// /smartai: the GM's SmartAI tools (plan Phase 4; personality and debug subcommands come in Phases 10 and 11).
    /// - reload [force]: re-reads the class files without a restart. With errors the current files stay in use, unless
    ///   forced (then the valid files load and the broken classes use the legacy AI).
    /// - status: on/off, the session fallback, the loaded classes.
    /// - spec: the SmartAI spec of the targeted bot, or why it has none.
    /// </summary>
    [Cmd(
        "&smartai",
        ePrivLevel.Player,
        "SmartAI bot intelligence: companion personalities and the SmartAI tools.",
        "/smartai personality [auto|aggressive|defensive|tactical|opportunistic|supportive|conservative] - your target companion's personality",
        "/smartai status - SmartAI on/off and the loaded class files",
        "/smartai reload [force] - re-read runtime/server/smartai/classes",
        "/smartai spec - the SmartAI specialization of your target bot",
        "/smartai group - the SmartAI tactical view of your target bot's group (or yours)",
        "/smartai debug [n] - your target bot's last n SmartAI decisions (up to 32)",
        "/smartai debuggroup [on|mine|off|status] - record every bot group's completed fights to runtime/logs/smart_ai_groups (mine: only your or your target's group)",
        "Every /smartai command and its answer go to runtime/logs/smartai-session-<server start>.log")]
    public sealed class SmartAiCommandHandler : AbstractCommandHandler, ICommandHandler
    {
        private const int MaxIssueLines = 12;

        public void OnCommand(GameClient client, string[] args)
        {
            if (client?.Player == null)
                return;
            SmartAiSessionLog.Command(client.Player.Name, string.Join(' ', args).Replace("&", "/") +
                                      (client.Player.TargetObject != null ? $"   [target: {client.Player.TargetObject.Name}]" : string.Empty));
            bool gm = client.Account?.PrivLevel >= (uint)ePrivLevel.GM;
            // The tools only read SmartAI's state; players may use them while smart_ai_player_tools is on (owner 2026-11-04).
            bool tools = gm || SmartAiProperties.SMART_AI_PLAYER_TOOLS;
            string sub = args.Length > 1 ? args[1].ToLowerInvariant() : tools ? "status" : "personality";
            if (sub == "personality")
            {
                Personality(client, args.Length > 2 ? args[2] : null);
                return;
            }
            if (!tools)
            {
                Say(client, "Usage: /smartai personality [" + SmartPersonalityCommands.Names.Replace(", ", "|") + "] with a companion targeted.");
                return;
            }
            switch (sub)
            {
                case "reload":
                    Reload(client, args.Length > 2 && args[2].Equals("force", StringComparison.OrdinalIgnoreCase));
                    break;
                case "status":
                    Status(client);
                    break;
                case "spec":
                    Spec(client);
                    break;
                case "group":
                    GroupView(client);
                    break;
                case "debug":
                    Debug(client, args.Length > 2 && int.TryParse(args[2], out int n) ? n : 10);
                    break;
                case "debuggroup":
                    DebugGroup(client, args.Length > 2 ? args[2].ToLowerInvariant() : "on");
                    break;
                default:
                    DisplaySyntax(client);
                    break;
            }
            if (sub is "status")
                Say(client, $"This session's SmartAI log: {SmartAiSessionLog.FilePath}");
        }

        private static void Reload(GameClient client, bool force)
        {
            SmartAiSpecRegistry.LoadReport report;
            try
            {
                report = SmartAiSpecRegistry.Reload(SmartAiSpecRegistry.DefaultDirectory, SmartAiServerGameData.Instance, force);
            }
            catch (Exception e)
            {
                Say(client, $"SmartAI reload failed: {e.Message}. The current class files stay in use.");
                return;
            }
            SmartAiSpecRegistry.LogReport(report);
            Say(client, $"SmartAI reload: {report.Summary}.");
            foreach (SmartAiIssue issue in report.Issues.OrderByDescending(i => i.Severity).Take(MaxIssueLines))
                Say(client, issue.ToString());
            if (report.Issues.Count > MaxIssueLines)
                Say(client, $"... and {report.Issues.Count - MaxIssueLines} more in the server log.");
            if (!report.Activated)
                Say(client, "Fix the errors and reload, or use /smartai reload force to load the valid files.");
        }

        private static void Status(GameClient client)
        {
            SmartAiSpecRegistry registry = SmartAiSpecRegistry.Active;
            Say(client, $"SmartAI: {(SmartAiProperties.USE_SMART_AI ? "on" : "off (Use SmartAI in the launcher)")}" +
                        (SmartAiFallback.IsTripped ? $"; turned off for this session: {SmartAiFallback.TripReason}" : string.Empty));
            Say(client, $"Combat thinks: {SmartAiDiagnostics.Thinks}, average {SmartAiDiagnostics.AverageMicroseconds:0.0} us, " +
                        $"slowest {SmartAiDiagnostics.SlowestMicroseconds:0.0} us. Decision logging: {(SmartAiProperties.SMART_AI_DEBUG_LOGGING ? "on" : "off")}.");
            if (registry.Classes.Count == 0)
            {
                Say(client, $"No class files loaded ({SmartAiSpecRegistry.DefaultDirectory}). Every bot uses the legacy bot AI.");
                return;
            }
            Say(client, $"{registry.Classes.Count} class(es), {registry.Specs.Count} spec(s), loaded {registry.LoadedUtc.ToLocalTime():g}:");
            foreach (SmartClassDefinition c in registry.Classes.Values.OrderBy(c => c.ClassName))
                Say(client, $"  {c.ClassName}: {string.Join(", ", c.Specs.Select(s => s.Name))}");
        }

        private static void Spec(GameClient client)
        {
            if (client.Player.TargetObject is not GameBot bot)
            {
                Say(client, "Target a bot (a hired companion or a gamebot).");
                return;
            }
            SmartAiSpecRegistry registry = SmartAiSpecRegistry.Active;
            string className = bot.CharacterClass?.Name ?? "?";
            if (bot.CharacterClass == null || !registry.Classes.TryGetValue(bot.CharacterClass.ID, out SmartClassDefinition c))
            {
                Say(client, $"{bot.Name} ({className}): no SmartAI class file, so the legacy bot AI.");
                return;
            }
            string lines = string.Join(", ", c.Specs
                .Where(s => !string.IsNullOrEmpty(s.Match?.PrimaryLineKey))
                .Select(s => s.Match.PrimaryLineKey).Distinct()
                .Select(k => $"{k} {bot.GetBaseSpecLevel(k)}"));
            SmartSpecDefinition spec = registry.Resolve(bot, out string how);
            bool hired = HiredCompanions165.IsHired(bot);
            string kind = hired ? "hired companion" : bot.IsAutonomousWorldBot ? "gamebot" : "bot";
            string specType = hired ? "not used for companions" : bot.BotSpec?.SpecType.ToString() ?? "none";
            Say(client, spec == null
                ? $"{bot.Name} ({className} {bot.Level}, {kind}): no spec, so the legacy bot AI. Why: {how}."
                : $"{bot.Name} ({className} {bot.Level}, {kind}): {spec.Name}, role {spec.Role}. Matched by {how}.");
            Say(client, $"Trained: {lines}. Rolled spec type: {specType}.");
            if (spec == null && hired)
                Say(client, "Train its main line (companion window, Train) and it fights by that spec.");
            if (spec != null)
            {
                SmartSpecProfile profile = SmartSpecProfile.For(spec);
                Say(client, $"Melee: {(profile.MeleeBanned ? $"banned ({profile.MeleeBanReason})" : "allowed")}; mana floor {profile.ManaFloorPercent}%; " +
                            $"personality {SmartPersonalityRegistry.Resolve(bot, spec)} (saved: {SmartPersonalityRegistry.Saved(bot.DatabaseID)}).");
                foreach (SmartFamilyRule rule in profile.Rules)
                    Say(client, $"  {rule.Family} {rule.Priority}: {rule.Source}" +
                                (rule.Condition.Groups.Length > 0 ? $" [if {rule.Condition.Text}]" : string.Empty));
                foreach (SmartHealBand band in profile.HealBands)
                    Say(client, $"  Heal at {band.Min}-{band.Max}%: {string.Join(" then ", band.Heals)}");
                if (profile.GroupHeal is { } groupHeal)
                    Say(client, $"  Group heal when {groupHeal.Members}+ members are under {groupHeal.Below}%: {string.Join(" then ", groupHeal.Heals)}");
                if (profile.SitBetweenHealsAbovePercent > 0)
                    Say(client, $"  Sits between heals in PvE fights while everyone is above {profile.SitBetweenHealsAbovePercent}%" +
                                (bot.IsCombatSeated ? " (sitting now)." : "."));
                foreach (SmartAbilityRule rule in profile.Abilities)
                    Say(client, $"  Realm ability {rule.Priority}: {rule.Key.Replace("AtlasOF_", "")}" +
                                (bot.GetAbility(rule.Key) == null ? " (not trained)" : string.Empty) +
                                (rule.Condition.Groups.Length > 0 ? $" [if {rule.Condition.Text}]" : string.Empty));
                Say(client, $"Positional styles and stealth openers: {(SmartStyleSelector.Drives(bot) ? "allowed" : "off")}.");
            }
            if (SmartAiBrainDriver.TryGetLastDecision(bot, out SmartDecision last))
                Say(client, $"Last SmartAI decision: {last.Action}" + (last.Score > 0 ? $" (score {last.Score})" : string.Empty) +
                            (last.Note.Length > 0 ? $", {last.Note}" : string.Empty) +
                            $", {(GameLoop.GameLoopTime - last.GameLoopTime) / 1000} s ago.");
            if (SmartAiBrainDriver.TryGetBreaker(bot, out SmartAiCircuitBreaker breaker) && breaker.Trips > 0)
                Say(client, $"SmartAI failed {breaker.Trips} time(s) for this bot.");
        }

        private static void Debug(GameClient client, int n)
        {
            if (client.Player.TargetObject is not GameBot bot)
            {
                Say(client, "Target a bot.");
                return;
            }
            if (!SmartAiDiagnostics.TryGetRing(bot, out SmartAiDiagnostics.DecisionRing ring) || ring.Count == 0)
            {
                Say(client, $"{bot.Name} has made no SmartAI decision yet (no class file, or no fight since the server started).");
                return;
            }
            long now = GameLoop.GameLoopTime;
            SmartDecision[] latest = ring.Latest(n);
            Say(client, $"{bot.Name}: last {latest.Length} of {ring.Count} SmartAI decisions, newest first:");
            foreach (SmartDecision d in latest)
                Say(client, $"  {(now - d.GameLoopTime) / 1000.0,5:0.0} s ago: {d.Action}" + (d.Score > 0 ? $" ({d.Score})" : string.Empty) +
                            (d.Note.Length > 0 ? $" - {d.Note}" : string.Empty));
        }

        private static void DebugGroup(GameClient client, string mode)
        {
            switch (mode)
            {
                case "on":
                case "all":
                    SmartGroupCombatLog.RecordAll();
                    Say(client, "Recording every group with a bot in it. Each group gets a file in " +
                                $"{SmartGroupCombatLog.Directory}; a fight is written once the whole group is out of combat.");
                    break;
                case "mine":
                {
                    Group group = (client.Player.TargetObject as GameLiving)?.Group ?? client.Player.Group;
                    if (group == null)
                    {
                        Say(client, "Join a group, or target a bot in one.");
                        return;
                    }
                    SmartGroupCombatLog.RecordGroup(group);
                    Say(client, $"Recording {group.LivingLeader?.Name ?? "this"}'s group" +
                                (SmartGroupCombatLog.CurrentScope == SmartGroupCombatLog.Scope.AllGroups ? " (every group is already recorded)" : string.Empty) +
                                $" to {SmartGroupCombatLog.FileOf(group)}. A fight is written once the whole group is out of combat.");
                    break;
                }
                case "off":
                    SmartGroupCombatLog.Stop();
                    Say(client, $"Group recording off. Fights written this session: {SmartGroupCombatLog.FightsWritten}; " +
                                $"unfinished fights dropped: {SmartGroupCombatLog.FightsDiscarded}.");
                    break;
                case "status":
                    Say(client, $"Group recording: {SmartGroupCombatLog.CurrentScope switch { SmartGroupCombatLog.Scope.AllGroups => "every group with a bot", SmartGroupCombatLog.Scope.ChosenGroups => "chosen groups", _ => "off" }}; " +
                                $"{SmartGroupCombatLog.GroupsTracked} group(s) followed; fights written {SmartGroupCombatLog.FightsWritten}, " +
                                $"unfinished fights dropped {SmartGroupCombatLog.FightsDiscarded}. Folder: {SmartGroupCombatLog.Directory}");
                    break;
                default:
                    Say(client, "Usage: /smartai debuggroup [on|mine|off|status]");
                    break;
            }
        }

        private static void Personality(GameClient client, string name)
        {
            if (client.Player.TargetObject is not GameBot bot || !SmartPersonalityCommands.MaySet(client.Player, bot))
            {
                Say(client, "Target one of your hired companions.");
                return;
            }
            SmartSpecDefinition spec = SmartAiSpecRegistry.Active.Resolve(bot);
            if (name == null)
            {
                SmartPersonality saved = SmartPersonalityRegistry.Saved(bot.DatabaseID);
                SmartPersonality fights = SmartPersonalityRegistry.Resolve(bot, spec);
                Say(client, $"{bot.Name}'s personality: {saved}" + (saved == SmartPersonality.Auto && fights != SmartPersonality.Auto ? $" ({fights}, its specialization's default)" : string.Empty) +
                            $". Choose one of: {SmartPersonalityCommands.Names}.");
                if (spec == null)
                    Say(client, "It has no SmartAI class file yet, so a personality has no effect until it gets one.");
                return;
            }
            if (!SmartPersonalityProfile.TryParse(name, out SmartPersonality personality))
            {
                Say(client, $"Unknown personality '{name}'. Choose one of: {SmartPersonalityCommands.Names}.");
                return;
            }
            if (!SmartPersonalityRegistry.Set(bot.DatabaseID, personality))
            {
                Say(client, "The personality could not be saved.");
                return;
            }
            Say(client, $"{bot.Name}: {SmartPersonalityCommands.Spoken(personality)}" +
                        (spec == null ? " (No SmartAI class file for its class yet.)" : string.Empty));
        }

        private static void GroupView(GameClient client)
        {
            Group group = (client.Player.TargetObject as GameLiving)?.Group ?? client.Player.Group;
            if (group == null)
            {
                Say(client, "Target a bot in a group, or join one.");
                return;
            }
            if (!SmartGroupTacticalState.TryGet(group, out SmartGroupTacticalState state) || state.Current.Version == 0)
            {
                Say(client, "No SmartAI view for that group yet (it is built while its SmartAI bots fight).");
                return;
            }
            long now = GameLoop.GameLoopTime;
            SmartGroupTacticalSnapshot s = state.Current;
            Say(client, $"SmartAI group view #{s.Version}, {(now - s.Tick) / 1000.0:0.0} s old{(s.IsFresh(now) ? "" : " (stale)")}: " +
                        $"{s.Engagement}, {s.MemberCount} members, lowest health {s.GroupMinHealthPercent}%.");
            Say(client, $"Main assist: {s.MainAssist?.Name ?? "none"} ({s.MainAssistReason}). Primary target: {s.PrimaryTarget?.Name ?? "none"}" +
                        (s.PrimaryTarget != null && s.PrimaryTargetLockUntil > now ? $", held {(s.PrimaryTargetLockUntil - now) / 1000.0:0.0} s more" : string.Empty) + ".");
            Say(client, $"Mode: {s.Mode}. Strategy: {s.Strategy}" + (s.StrategyReason.Length > 0 ? $" ({s.StrategyReason})" : string.Empty) +
                        (s.PeelTarget != null ? $", peel {s.PeelTarget.Name}" : string.Empty) +
                        $". Observations last tick: {s.ObservationsRead}. Callouts: {(SmartAiProperties.SMART_AI_GROUP_CALLOUTS ? "on" : "off")}.");
            foreach (SmartHealPriority p in s.HealingPriorities)
                Say(client, $"  Hurt: {p.Member.Name} {p.HealthPercent}%{(p.Attackers > 0 ? $", {p.Attackers} attacker(s)" : string.Empty)}");
            Say(client, $"Live claims: {state.ClaimCount(now)}. Groups published last tick: {SmartAiCoordinator.LastPublishedGroups}.");
        }

        private static void Say(GameClient client, string text)
        {
            client.Out.SendMessage(text, eChatType.CT_System, eChatLoc.CL_SystemWindow);
            SmartAiSessionLog.Output(text);
        }
    }
}
