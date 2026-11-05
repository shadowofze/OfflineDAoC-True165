using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DOL.GS.SmartAI
{
    /// <summary>What a SmartAI combat think can choose; each runs a legacy bot action (BotBrain.SmartAi.cs).</summary>
    public enum SmartActionFamily : byte
    {
        /// <summary>Heals, cures, resurrections (legacy triage).</summary>
        Triage,
        /// <summary>Defensive and buff spells.</summary>
        Support,
        /// <summary>Offensive spells only, no melee.</summary>
        OffenseSpells,
        /// <summary>The whole legacy offense, melee included.</summary>
        OffenseFull,
    }

    public enum SmartClauseKind : byte
    {
        GroupMinHealthAbove, GroupMinHealthBelow, SelfManaAbove, SelfManaBelow, TargetHealthAbove, TargetHealthBelow,
        OutOfCombat, InCombat, SilencedOrOutOfMana,
        /// <summary>Another group member is healing ("another healer is healing").</summary>
        OtherHealerActive,
        /// <summary>Nobody else in the group is healing ("no other healer is healing", "the only healer").</summary>
        NoOtherHealer,
    }

    public readonly record struct SmartClause(SmartClauseKind Kind, int Value);

    /// <summary>
    /// A priority line's "(...)" condition, read into clauses the engine can test. Clauses split on "," and " and " must
    /// all hold; a clause with " or " holds when any recognized side does. A clause the reader doesn't recognize
    /// ("on enemy casters", "primary healer is busy") is left out and counted; a condition with no recognized clause
    /// always holds. The reader is deliberately small: what it doesn't know never blocks an action.
    /// </summary>
    public sealed class SmartCondition
    {
        public static readonly SmartCondition Always = new([], 0, string.Empty);

        private static readonly Regex Number = new(@"(?<n>\d{1,3})\s*%", RegexOptions.Compiled);
        private static readonly Regex Splitter = new(@",|;|\band\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>All groups must hold; each group holds when any of its alternatives does. An array: the combat think
        /// walks it with no allocation.</summary>
        public SmartClause[][] Groups { get; }
        public int UnrecognizedClauses { get; }
        public string Text { get; }

        private SmartCondition(SmartClause[][] groups, int unrecognized, string text)
        {
            Groups = groups;
            UnrecognizedClauses = unrecognized;
            Text = text;
        }

        public static SmartCondition Parse(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Always;
            var groups = new List<SmartClause[]>();
            int unrecognized = 0;
            foreach (string part in Splitter.Split(text.ToLowerInvariant()))
            {
                if (part.Trim().Length == 0)
                    continue;
                SmartClause[] alternatives = part.Split(" or ")
                    .Select(ReadClause).Where(c => c.HasValue).Select(c => c.Value).ToArray();
                if (alternatives.Length == 0)
                    unrecognized++;
                else
                    groups.Add(alternatives);
            }
            return new SmartCondition(groups.ToArray(), unrecognized, text);
        }

        public bool Holds(in SmartBotPerception p)
        {
            SmartClause[][] groups = Groups;
            for (int g = 0; g < groups.Length; g++)
            {
                SmartClause[] group = groups[g];
                bool any = false;
                for (int i = 0; i < group.Length; i++)
                {
                    if (Test(group[i], p))
                    {
                        any = true;
                        break;
                    }
                }
                if (!any)
                    return false;
            }
            return true;
        }

        private static bool Test(SmartClause c, in SmartBotPerception p) => c.Kind switch
        {
            SmartClauseKind.GroupMinHealthAbove => p.GroupMinHealthPercent > c.Value,
            SmartClauseKind.GroupMinHealthBelow => p.GroupMinHealthPercent < c.Value,
            SmartClauseKind.SelfManaAbove => p.SelfManaPercent > c.Value,
            SmartClauseKind.SelfManaBelow => p.SelfManaPercent < c.Value,
            SmartClauseKind.TargetHealthAbove => p.HasTarget && p.TargetHealthPercent > c.Value,
            SmartClauseKind.TargetHealthBelow => p.HasTarget && p.TargetHealthPercent < c.Value,
            SmartClauseKind.OutOfCombat => !p.InCombat,
            SmartClauseKind.InCombat => p.InCombat,
            SmartClauseKind.SilencedOrOutOfMana => p.Silenced || p.SelfManaPercent < SmartBotPerception.OutOfManaPercent,
            SmartClauseKind.OtherHealerActive => p.OtherHealers > 0,
            SmartClauseKind.NoOtherHealer => p.OtherHealers == 0,
            _ => true,
        };

        private static SmartClause? ReadClause(string clause)
        {
            string c = clause.Trim();
            if (c.Contains("no other healer") || c.Contains("only healer") || c.Contains("sole healer") || c.Contains("no healer"))
                return new SmartClause(SmartClauseKind.NoOtherHealer, 0);
            if (c.Contains("another healer") || c.Contains("other healer"))
                return new SmartClause(SmartClauseKind.OtherHealerActive, 0);
            if (c.Contains("out of combat"))
                return new SmartClause(SmartClauseKind.OutOfCombat, 0);
            if (c.Contains("silenced") || c.Contains("out of mana") || c.Contains("out of power") || c.Contains("oom"))
                return new SmartClause(SmartClauseKind.SilencedOrOutOfMana, 0);
            if (c.Contains("in combat") && !c.Contains("out of"))
                return new SmartClause(SmartClauseKind.InCombat, 0);

            Match m = Number.Match(c);
            if (!m.Success)
                return null;
            int n = int.Parse(m.Groups["n"].Value);
            string before = c[..m.Index];
            bool? above = before.Contains('>') || before.Contains("above") || before.Contains("over") ? true
                : before.Contains('<') || before.Contains("below") || before.Contains("under") ? false
                : null;
            if (above == null)
                return null;

            // The subject is in the few words before the comparison ("assisting MA target when group HP > 80%" is about
            // the group), or right after the number ("mana drops below 25%" / "< 35% mana").
            string[] words = before.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string subject = string.Join(' ', words.Skip(Math.Max(0, words.Length - 4))) + " " + c[(m.Index + m.Length)..];
            subject = subject.Length > 40 ? subject[..40] : subject;

            // Mana is self mana ("group mana" too: the bot's own pool is the one it spends).
            if (subject.Contains("mana") || subject.Contains("power"))
                return new SmartClause(above.Value ? SmartClauseKind.SelfManaAbove : SmartClauseKind.SelfManaBelow, n);
            if (subject.Contains("group") || subject.Contains("ally") || subject.Contains("allies") || subject.Contains("member"))
                return new SmartClause(above.Value ? SmartClauseKind.GroupMinHealthAbove : SmartClauseKind.GroupMinHealthBelow, n);
            if (subject.Contains("target"))
                return new SmartClause(above.Value ? SmartClauseKind.TargetHealthAbove : SmartClauseKind.TargetHealthBelow, n);
            return null;
        }
    }

    /// <summary>One priority line that maps to an action family.</summary>
    public sealed record SmartFamilyRule(SmartActionFamily Family, int Priority, SmartCondition Condition, string Source);

    /// <summary>One section 11 line naming a realm ability SmartAI can fire (SmartRealmAbilities.SafeActives).</summary>
    public sealed record SmartAbilityRule(string Key, int Priority, SmartCondition Condition, string Source);

    /// <summary>Section 12 heal thresholds: a target health band (Min &lt;= health &lt; Max) and the heals for it, in
    /// order of preference ("HealInstant + HealBig": the instant first, the big heal when it isn't ready).</summary>
    public sealed record SmartHealBand(int Min, int Max, string[] Heals, string Source);

    /// <summary>Section 12 "3+ members &lt; 70%: HealSpread / HealGroup": a group heal when enough members are hurt.</summary>
    public sealed record SmartGroupHealRule(int Members, int Below, string[] Heals, string Source);

    /// <summary>
    /// Reads section 12's heal thresholds (owner 2026-11-04): bullets that name heal kinds in backticks (`HealEfficient`,
    /// `HealBig`, `HealInstant`, `HealGroup` / `HealSpread`, `HealInstantGroup`, `HealOverTime`) with a health range
    /// ("70%–85%", "< 35%", "when ally HP 50%–75%") or a group count ("3+ members < 70%"). Clauses split on ";".
    /// Lines without a heal kind (mana floors, resurrection) are not thresholds.
    /// </summary>
    public static class SmartHealThresholds
    {
        private static readonly Regex HealToken = new(@"`(?<k>Heal[A-Za-z]+)`", RegexOptions.Compiled);
        private static readonly Regex Range = new(@"(?<a>\d{1,3})\s*%?\s*[–—-]\s*(?<b>\d{1,3})\s*%", RegexOptions.Compiled);
        private static readonly Regex Below = new(@"(<|below|under)\s*(?<x>\d{1,3})\s*%", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex Group = new(@"(?<n>\d+)\s*\+\s*(members?|allies|group members?)\s*(<|below|under)\s*(?<x>\d{1,3})\s*%",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static readonly string[] Known = ["HealEfficient", "HealBig", "HealInstant", "HealGroup", "HealInstantGroup", "HealOverTime"];

        public static (List<SmartHealBand> Bands, SmartGroupHealRule Group) Read(SmartSpecSection resources)
        {
            var bands = new List<SmartHealBand>();
            SmartGroupHealRule group = null;
            if (resources == null)
                return (bands, null);
            foreach (string bullet in resources.Bullets)
            {
                foreach (string clause in bullet.Split(';'))
                {
                    string[] heals = HealToken.Matches(clause).Select(m => Canonical(m.Groups["k"].Value))
                        .Where(k => k != null).Distinct().ToArray();
                    if (heals.Length == 0)
                        continue;
                    Match g = Group.Match(clause);
                    if (g.Success)
                    {
                        group ??= new SmartGroupHealRule(int.Parse(g.Groups["n"].Value), int.Parse(g.Groups["x"].Value), heals, clause.Trim());
                        continue;
                    }
                    Match r = Range.Match(clause);
                    if (r.Success)
                    {
                        int a = int.Parse(r.Groups["a"].Value), b = int.Parse(r.Groups["b"].Value);
                        bands.Add(new SmartHealBand(Math.Min(a, b), Math.Max(a, b), heals, clause.Trim()));
                        continue;
                    }
                    Match below = Below.Match(clause);
                    if (below.Success)
                        bands.Add(new SmartHealBand(0, int.Parse(below.Groups["x"].Value), heals, clause.Trim()));
                }
            }
            return (bands, group);
        }

        /// <summary>The heal kind for a template name ("HealSpread" is the group heal); null for one the bots don't know.</summary>
        public static string Canonical(string name) => name switch
        {
            "HealSpread" => "HealGroup",
            _ => Array.IndexOf(Known, name) >= 0 ? name : null,
        };

        /// <summary>
        /// The heal kind to cast (pure, for tests): the group heal when its rule is met, else the first ready heal of the
        /// band holding the target's health; null to keep the legacy choice (no band fits, or none of its heals is ready).
        /// </summary>
        public static string Choose(IReadOnlyList<SmartHealBand> bands, SmartGroupHealRule group, int targetHealthPercent,
            Func<int, int> membersBelow, Func<string, bool> ready)
        {
            if (group != null && membersBelow(group.Below) >= group.Members)
                foreach (string heal in group.Heals)
                    if (ready(heal))
                        return heal;
            foreach (SmartHealBand band in bands)
            {
                if (targetHealthPercent < band.Min || targetHealthPercent >= band.Max)
                    continue;
                foreach (string heal in band.Heals)
                    if (ready(heal))
                        return heal;
            }
            return null;
        }
    }

    /// <summary>
    /// A spec compiled for the engine (cached per spec, rebuilt with the registry on /smartai reload).
    /// - Priority lines of sections 7 (offensive), 8 (defensive) and 9 (target) map to families by their wording;
    ///   section 11 (abilities, realm abilities) and potions wait for Phase 6.
    /// - Melee is banned by a priority 0 melee line or a hard "MUST NOT ... melee combat" rule.
    /// </summary>
    public sealed class SmartSpecProfile
    {
        private static readonly ConditionalWeakTable<SmartSpecDefinition, SmartSpecProfile> Cache = new();

        public SmartSpecDefinition Spec { get; }
        public IReadOnlyList<SmartFamilyRule> Rules => RuleArray;
        /// <summary>The rules as an array, for the allocation-free combat think.</summary>
        internal SmartFamilyRule[] RuleArray { get; }
        public bool MeleeBanned { get; }
        public string MeleeBanReason { get; }
        /// <summary>Section 12's mana floor; offensive spells below it score a quarter.</summary>
        public int ManaFloorPercent { get; }
        /// <summary>The spec says when to heal. If not, offense keeps the legacy habit of healing on the way.</summary>
        public bool HasTriageRules { get; }
        /// <summary>Section 11's realm abilities SmartAI can fire, highest priority first.</summary>
        public IReadOnlyList<SmartAbilityRule> Abilities { get; }
        /// <summary>Section 12's heal thresholds (SmartHealThresholds); empty: the legacy heal choice.</summary>
        public IReadOnlyList<SmartHealBand> HealBands { get; }
        public SmartGroupHealRule GroupHeal { get; }
        /// <summary>The spec puts Guard / Protect on the healer (SmartAbilityPolicy).</summary>
        public bool GuardsHealer { get; }
        public bool ProtectsHealer { get; }

        private static readonly Regex AbilityKey = new(@"AtlasOF_\w+", RegexOptions.Compiled);

        private SmartSpecProfile(SmartSpecDefinition spec, List<SmartFamilyRule> rules, bool meleeBanned, string reason)
        {
            Spec = spec;
            RuleArray = rules.ToArray();
            (List<SmartHealBand> bands, SmartGroupHealRule group) = SmartHealThresholds.Read(spec.Section(SmartSpecSectionKeys.Resources));
            HealBands = bands;
            GroupHeal = group;
            foreach (IReadOnlyList<SmartPriorityEntry> list in new[] { spec.OffensivePriorities, spec.DefensivePriorities, spec.TargetPriorities, spec.AbilityPriorities })
            {
                foreach (SmartPriorityEntry e in list)
                {
                    if (e.Priority <= 0)
                        continue;
                    GuardsHealer |= SmartAbilityPolicy.LineOnHealer("Guard", e.Name, e.Condition);
                    ProtectsHealer |= SmartAbilityPolicy.LineOnHealer("Protect", e.Name, e.Condition);
                }
            }
            Abilities = spec.AbilityPriorities
                .Select(e => (Entry: e, Key: AbilityKey.Match(e.Name)))
                .Where(x => x.Key.Success && x.Entry.Priority > 0 && SmartRealmAbilities.IsSafe(x.Key.Value))
                .Select(x => new SmartAbilityRule(x.Key.Value, x.Entry.Priority, SmartCondition.Parse(x.Entry.Condition), $"11.{x.Entry.Rank} {x.Entry.Name}"))
                .OrderByDescending(r => r.Priority)
                .ToList();
            MeleeBanned = meleeBanned;
            MeleeBanReason = reason;
            ManaFloorPercent = spec.ManaFloorPercent ?? 0;
            HasTriageRules = rules.Any(r => r.Family == SmartActionFamily.Triage && r.Priority > 0);
        }

        /// <summary>Whether this think's offense may run the legacy triage on the way.</summary>
        public bool OffenseMayHeal(ReadOnlySpan<SmartActionCandidate> candidates) =>
            !HasTriageRules || SmartCandidateGenerator.TriageAllowed(candidates);

        public static SmartSpecProfile For(SmartSpecDefinition spec) => spec == null ? null : Cache.GetValue(spec, Compile);

        public static SmartSpecProfile Compile(SmartSpecDefinition spec)
        {
            var rules = new List<SmartFamilyRule>();
            void Add(IEnumerable<SmartPriorityEntry> entries, string section)
            {
                foreach (SmartPriorityEntry e in entries)
                {
                    SmartActionFamily? family = Classify(e.Name, section);
                    if (family.HasValue)
                        rules.Add(new SmartFamilyRule(family.Value, e.Priority, SmartCondition.Parse(e.Condition), $"{section}.{e.Rank} {e.Name}"));
                }
            }
            Add(spec.OffensivePriorities, SmartSpecSectionKeys.Offensive);
            Add(spec.DefensivePriorities, SmartSpecSectionKeys.Defensive);
            Add(spec.TargetPriorities, SmartSpecSectionKeys.Target);

            string reason = null;
            SmartFamilyRule zeroMelee = rules.FirstOrDefault(r => r.Family == SmartActionFamily.OffenseFull && r.Priority <= 0);
            if (zeroMelee != null)
                reason = $"priority 0: {zeroMelee.Source}";
            else if (spec.ProhibitionsAreHard)
            {
                string rule = spec.Prohibitions.FirstOrDefault(BansMelee);
                if (rule != null)
                    reason = rule;
            }
            return new SmartSpecProfile(spec, rules, reason != null, reason ?? string.Empty);
        }

        /// <summary>"MUST NOT engage in melee combat ..." bans melee; "MUST NOT break mezzed targets with melee swings"
        /// or "MUST NOT stand in melee range" don't.</summary>
        public static bool BansMelee(string rule)
        {
            string r = rule.ToLowerInvariant();
            return !r.Contains("mez") && (r.Contains("engage in melee") || r.Contains("melee combat") || r.Contains("melee attack"));
        }

        /// <summary>A priority line's family from its wording, or null when the engine has no action for it yet.</summary>
        public static SmartActionFamily? Classify(string name, string section)
        {
            string n = name.ToLowerInvariant();
            if (n.Contains("potion") || n.Contains("cooldown") || n.Contains("shield block") || n.Contains("stance"))
                return null;
            if (section == SmartSpecSectionKeys.Target)
            {
                // Target priorities name whom to act on; the friendly ones mean triage. Enemy targeting is the legacy
                // choice until the group blackboard (Phase 7).
                return n.Contains("lowest") || (n.Contains("group member") || n.Contains("ally")) && (n.Contains("hp") || n.Contains("health") ||
                       n.Contains("disease") || n.Contains("mez"))
                    ? SmartActionFamily.Triage
                    : null;
            }
            if (n.Contains("melee") || n.Contains("style") || n.Contains("weapon"))
                return SmartActionFamily.OffenseFull;
            if (n.Contains("heal") || n.Contains("cure") || n.Contains("triage") || n.Contains("resurrect") || n.Contains("rez"))
                return SmartActionFamily.Triage;
            if (n.Contains("buff") || n.Contains("bubble") || n.Contains("absorb") || n.Contains("regen") || n.Contains("armor factor"))
                return SmartActionFamily.Support;
            if (n.Contains("damage") || n.Contains("nuke") || n.Contains("smite") || n.Contains("dd") || n.Contains("dot") ||
                n.Contains("bolt") || n.Contains("blast") || n.Contains("stun") || n.Contains("mez") || n.Contains("root") ||
                n.Contains("snare") || n.Contains("debuff") || n.Contains("lifetap") || n.Contains("aoe") || n.Contains("spell"))
                return SmartActionFamily.OffenseSpells;
            return null;
        }
    }
}
