using System;

namespace DOL.GS.SmartAI
{
    public readonly record struct SmartActionCandidate(SmartActionFamily Family, int Score, int BasePriority);

    /// <summary>
    /// Stage 3: the candidates of a think, in the order to try them (architecture section 3.1).
    /// A family is a candidate when the gates allow it and one of its spec lines has a priority above 0 and a condition
    /// that holds; its base priority is the highest such line. Families the spec never mentions are not candidates.
    /// Ordered by score, ties in family order (triage first). Writes into a caller buffer: no allocation per think.
    /// </summary>
    public static class SmartCandidateGenerator
    {
        public const int FamilyCount = 4;

        public static int Generate(SmartSpecProfile profile, in SmartBotPerception p, Span<SmartActionCandidate> buffer)
        {
            int count = 0;
            for (int f = 0; f < FamilyCount && count < buffer.Length; f++)
            {
                var family = (SmartActionFamily)f;
                if (!SmartRuleGateEvaluator.Allows(family, profile, p))
                    continue;
                int best = 0;
                SmartFamilyRule[] rules = profile.RuleArray;
                for (int r = 0; r < rules.Length; r++)
                {
                    SmartFamilyRule rule = rules[r];
                    if (rule.Family == family && rule.Priority > best && rule.Condition.Holds(p))
                        best = rule.Priority;
                }
                if (best <= 0)
                    continue;
                buffer[count++] = new SmartActionCandidate(family, SmartUtilityScorer.Score(family, best, profile, p), best);
            }

            // Insertion sort, highest score first; stable, so equal scores keep family order.
            for (int i = 1; i < count; i++)
            {
                SmartActionCandidate c = buffer[i];
                int j = i - 1;
                while (j >= 0 && buffer[j].Score < c.Score)
                {
                    buffer[j + 1] = buffer[j];
                    j--;
                }
                buffer[j + 1] = c;
            }
            return count;
        }

        /// <summary>True when the spec lets this think's triage run (decides whether offense may heal on the way).</summary>
        public static bool TriageAllowed(ReadOnlySpan<SmartActionCandidate> candidates)
        {
            foreach (SmartActionCandidate c in candidates)
                if (c.Family == SmartActionFamily.Triage)
                    return true;
            return false;
        }
    }
}
