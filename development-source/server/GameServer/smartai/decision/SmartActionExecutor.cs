using System;
using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>What one SmartAI think decided, for /smartai spec (Phase 11 keeps a ring of them).</summary>
    public sealed record SmartDecision(string Spec, string Action, int Score, string Note, long GameLoopTime,
        SmartActionFamily? Family = null);

    /// <summary>
    /// Stage 5: runs the candidates in order through the legacy bot actions until one acts (architecture section 3.1).
    /// - Returns true when SmartAI handled the think: an action ran, or the spec bans melee and nothing else applied
    ///   (the bot holds rather than letting the legacy brain swing).
    /// - Returns false to hand the think to the legacy brain, unchanged.
    /// </summary>
    public static class SmartActionExecutor
    {
        public static bool Execute(BotBrain brain, GameLiving target, SmartSpecProfile profile, in SmartBotPerception p,
            ReadOnlySpan<SmartActionCandidate> candidates, out SmartDecision decision)
        {
            bool mayHeal = profile.OffenseMayHeal(candidates);
            bool support = brain.SmartAiExclusiveSupport;
            foreach (SmartActionCandidate c in candidates)
            {
                bool acted = c.Family switch
                {
                    SmartActionFamily.Triage => brain.SmartAiTriage(),
                    SmartActionFamily.Support => brain.SmartAiSupport(),
                    SmartActionFamily.OffenseSpells => brain.SmartAiOffensiveSpells(target, mayHeal),
                    SmartActionFamily.OffenseFull => FullOffense(brain, mayHeal),
                    _ => false,
                };
                if (acted)
                {
                    // A support bot keeps its legacy upkeep: formation behind its player, no melee or pet fight.
                    // (The full offense of a support bot is the legacy support turn itself.)
                    if (support && c.Family != SmartActionFamily.OffenseFull)
                        brain.SmartAiSupportUpkeep(false);
                    decision = new SmartDecision(profile.Spec.Name, c.Family.ToString(), c.Score,
                        $"{p.Mode}, {p.Personality?.Personality ?? SmartPersonality.Auto}{(support ? ", support role" : string.Empty)}",
                        GameLoop.GameLoopTime, c.Family);
                    return true;
                }
            }

            // Nothing to cast: a spec with "Sit Between Heals" sits to regain power while the whole group is healthy
            // (SmartCombatSit; PvE only). Otherwise it stands up, if it was sitting, and the think goes on as before.
            if (brain.BotBody is { } body && SmartCombatSit.TrySit(brain, body, profile, p, out string sitNote))
            {
                brain.AlreadyCheckedHeals = true;
                decision = new SmartDecision(profile.Spec.Name, "Sit", 0, sitNote, GameLoop.GameLoopTime);
                return true;
            }

            if (support)
            {
                // Nothing in the spec applies now: the legacy support turn (buffs, add control, formation), with the
                // spec's heal conditions still respected.
                brain.SmartAiLegacySupport(mayHeal);
                decision = new SmartDecision(profile.Spec.Name, "Support turn", 0,
                    candidates.Length == 0 ? "no candidate; legacy support upkeep" : "no candidate acted; legacy support upkeep",
                    GameLoop.GameLoopTime);
                return true;
            }

            if (profile.MeleeBanned)
            {
                // Nothing to cast and no melee allowed: hold instead of the legacy swing. Keep the triage this think
                // already ran from running again in a later legacy path.
                brain.AlreadyCheckedHeals = true;
                if (brain.Body.attackComponent?.AttackState == true)
                    brain.Body.StopAttack();
                decision = new SmartDecision(profile.Spec.Name, "Hold", 0, $"melee banned ({profile.MeleeBanReason})", GameLoop.GameLoopTime);
                return true;
            }

            decision = new SmartDecision(profile.Spec.Name, "Legacy", 0, candidates.Length == 0 ? "no candidate" : "no candidate acted",
                GameLoop.GameLoopTime);
            return false;
        }

        private static bool FullOffense(BotBrain brain, bool mayHeal)
        {
            brain.SmartAiFullOffense(mayHeal);
            return true;
        }
    }
}
