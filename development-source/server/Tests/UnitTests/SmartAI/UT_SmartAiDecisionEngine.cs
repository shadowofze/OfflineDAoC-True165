using System;
using System.IO;
using System.Linq;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 5: conditions, the spec profile, the rule gates and the candidate order, on the shipped
/// Cleric file. The executor runs legacy bot actions and is checked in game.</summary>
[TestFixture]
public class UT_SmartAiDecisionEngine
{
    private SmartClassDefinition _cleric;

    [OneTimeSetUp] public void LoadCleric()
    {
        string path = null;
        for (DirectoryInfo d = new(TestContext.CurrentContext.TestDirectory); d != null && path == null; d = d.Parent)
        {
            string candidate = Path.Combine(d.FullName, "runtime", "server", "smartai", "classes", "albion", "cleric.md");
            if (File.Exists(candidate) && Directory.Exists(Path.Combine(d.FullName, "development-source")))
                path = candidate;
        }
        if (path == null)
            Assert.Ignore("runtime/server/smartai/classes/albion/cleric.md is not beside this build");
        _cleric = SmartAiMarkdownParser.Parse(File.ReadAllText(path), "albion/cleric.md").Class;
    }

    private SmartSpecProfile Profile(string spec) => SmartSpecProfile.Compile(_cleric.Specs.Single(s => s.Name == spec));

    private static SmartBotPerception See(int groupMin = 100, int mana = 80, int target = 80, bool controlled = false,
        bool silenced = false, bool hasTarget = true, bool inCombat = true, int otherHealers = 1) => new()
    {
        OtherHealers = otherHealers,
        SelfHealthPercent = 100,
        SelfManaPercent = mana,
        GroupMinHealthPercent = groupMin,
        MembersBelow65 = groupMin < 65 ? 1 : 0,
        MembersBelow40 = groupMin < 40 ? 1 : 0,
        InCombat = inCombat,
        Silenced = silenced,
        HasTarget = hasTarget,
        TargetHealthPercent = target,
        TargetControlled = controlled,
    };

    private static SmartActionFamily[] Order(SmartSpecProfile profile, SmartBotPerception p)
    {
        Span<SmartActionCandidate> buffer = stackalloc SmartActionCandidate[SmartCandidateGenerator.FamilyCount];
        int n = SmartCandidateGenerator.Generate(profile, p, buffer);
        return buffer[..n].ToArray().Select(c => c.Family).ToArray();
    }

    // Conditions ----------------------------------------------------------------------------------------------------

    [Test]
    public void Condition_ReadsTheClericSmiteGate()
    {
        var c = SmartCondition.Parse("Only if all group members > 90% HP, group mana > 60%, and target has < 15% HP");
        Assert.That(c.Groups.SelectMany(g => g), Is.EqualTo(new[]
        {
            new SmartClause(SmartClauseKind.GroupMinHealthAbove, 90),
            new SmartClause(SmartClauseKind.SelfManaAbove, 60),
            new SmartClause(SmartClauseKind.TargetHealthBelow, 15),
        }));
        Assert.That(c.Holds(See(groupMin: 95, mana: 70, target: 10)), Is.True);
        Assert.That(c.Holds(See(groupMin: 85, mana: 70, target: 10)), Is.False);
        Assert.That(c.Holds(See(groupMin: 95, mana: 50, target: 10)), Is.False);
        Assert.That(c.Holds(See(groupMin: 95, mana: 70, target: 20)), Is.False);
        Assert.That(c.Holds(See(groupMin: 95, mana: 70, hasTarget: false)), Is.False);
    }

    [Test]
    public void Condition_OrTakesTheRecognizedSide()
    {
        var c = SmartCondition.Parse("When ally HP < 65% or primary healer is busy");
        Assert.That(c.Holds(See(groupMin: 60)), Is.True);
        Assert.That(c.Holds(See(groupMin: 70)), Is.False);
    }

    [TestCase("Assisting MA target when group HP > 80%", SmartClauseKind.GroupMinHealthAbove, 80)]
    [TestCase("target has < 15% HP", SmartClauseKind.TargetHealthBelow, 15)]
    [TestCase("Use when mana drops below 25%", SmartClauseKind.SelfManaBelow, 25)]
    [TestCase("when ally is < 25% HP", SmartClauseKind.GroupMinHealthBelow, 25)]
    [TestCase("Execute window: target below 20% health", SmartClauseKind.TargetHealthBelow, 20)]
    public void Condition_SubjectIsNextToTheComparison(string text, SmartClauseKind kind, int value) =>
        Assert.That(SmartCondition.Parse(text).Groups.Single().Single(), Is.EqualTo(new SmartClause(kind, value)));

    [TestCase("On enemy casters, healers, or called assist target")]
    [TestCase("Focusing MA target")]
    [TestCase("")]
    public void Condition_UnrecognizedNeverBlocks(string text) =>
        Assert.That(SmartCondition.Parse(text).Holds(See()), Is.True);

    [Test]
    public void Condition_CombatAndPowerStates()
    {
        Assert.That(SmartCondition.Parse("Out of Combat").Holds(See(inCombat: true)), Is.False);
        Assert.That(SmartCondition.Parse("Out of Combat").Holds(See(inCombat: false)), Is.True);
        var fallback = SmartCondition.Parse("Fallback when silenced or out of mana");
        Assert.That(fallback.Holds(See(mana: 50)), Is.False);
        Assert.That(fallback.Holds(See(mana: 5)), Is.True);
        Assert.That(fallback.Holds(See(silenced: true)), Is.True);
    }

    // Classification and bans ----------------------------------------------------------------------------------------

    [TestCase("Direct Damage (Smite)", "7", SmartActionFamily.OffenseSpells)]
    [TestCase("Ranged Holy Stun", "7", SmartActionFamily.OffenseSpells)]
    [TestCase("Melee Attack (Crushing + Shield)", "7", SmartActionFamily.OffenseFull)]
    [TestCase("Self-Cast Absorb Bubble", "8", SmartActionFamily.Support)]
    [TestCase("Secondary Spot-Heal / Cure", "8", SmartActionFamily.Triage)]
    [TestCase("Lowest Effective HP% Group Member (weighted by incoming attackers)", "9", SmartActionFamily.Triage)]
    // Someone else in melee is not the bot's own melee (2026-11-05 group logs: list casters swung through these).
    [TestCase("Single Root on Enemy Melee Attacker", "8", SmartActionFamily.OffenseSpells)]
    [TestCase("Cold Snare on Melee Pursuer", "8", SmartActionFamily.OffenseSpells)]
    [TestCase("AoE Root on Advancing Enemy Melee (RvR)", "7", SmartActionFamily.OffenseSpells)]
    [TestCase("Single Root on Attacking Melee", "8", SmartActionFamily.OffenseSpells)]
    [TestCase("Damage Add Buff on Melee Allies", "7", SmartActionFamily.Support)]
    [TestCase("Shield Slam on Melee Pursuer", "8", SmartActionFamily.OffenseFull)]
    [TestCase("Melee Staff Attack", "7", SmartActionFamily.OffenseFull)]
    [TestCase("One-Handed Weapon Style Chain on Focus Target (at 85% HP)", "7", SmartActionFamily.OffenseFull)]
    public void Classify_MapsWording(string name, string section, SmartActionFamily family) =>
        Assert.That(SmartSpecProfile.Classify(name, section), Is.EqualTo(family));

    [TestCase("Self-Heal Potion / Mana Potion", "8")]
    [TestCase("Defensive Cooldown Activation (`Purge` / `MoC` / `BoF`)", "8")]
    [TestCase("Shield Block", "8")]
    [TestCase("Main Assist Target", "9")]
    public void Classify_LeavesWhatHasNoActionYet(string name, string section) =>
        Assert.That(SmartSpecProfile.Classify(name, section), Is.Null);

    [TestCase("MUST NOT engage in melee combat under any circumstances.", true)]
    [TestCase("MUST NOT break mezzed targets with melee swings or area-of-effect spells.", false)]
    [TestCase("MUST NOT stand in melee range when able to cast from maximum Smite distance.", false)]
    public void BansMelee_ReadsOnlyRealBans(string rule, bool bans) =>
        Assert.That(SmartSpecProfile.BansMelee(rule), Is.EqualTo(bans));

    // The Cleric specs ----------------------------------------------------------------------------------------------

    [Test]
    public void Rejuvenation_HealsFirstAndNeverMelees()
    {
        SmartSpecProfile rejuv = Profile("Rejuvenation Cleric");
        Assert.That(rejuv.MeleeBanned, Is.True);
        Assert.That(rejuv.ManaFloorPercent, Is.EqualTo(30));

        // An ally at 30%: triage first, then the bubble; no smite while anyone is hurt.
        Assert.That(Order(rejuv, See(groupMin: 30, target: 10)), Is.EqualTo(new[] { SmartActionFamily.Triage, SmartActionFamily.Support }));
        // All healthy, the target nearly dead: smite last.
        Assert.That(Order(rejuv, See(groupMin: 100, mana: 80, target: 10)),
            Is.EqualTo(new[] { SmartActionFamily.Triage, SmartActionFamily.Support, SmartActionFamily.OffenseSpells }));
        // Never offense at all, so no melee candidate ever.
        Assert.That(Order(rejuv, See(groupMin: 100, mana: 5, target: 10)), Has.No.Member(SmartActionFamily.OffenseFull));
    }

    [Test]
    public void AnySpec_NeverBreaksAMez()
    {
        foreach (SmartSpecDefinition spec in _cleric.Specs)
        {
            SmartActionFamily[] order = Order(SmartSpecProfile.Compile(spec), See(groupMin: 100, mana: 90, target: 10, controlled: true));
            Assert.That(order, Has.No.Member(SmartActionFamily.OffenseSpells).And.No.Member(SmartActionFamily.OffenseFull), spec.Name);
        }
    }

    [Test]
    public void Silenced_CastsNothing()
    {
        SmartSpecProfile rejuv = Profile("Rejuvenation Cleric");
        Assert.That(Order(rejuv, See(groupMin: 20, silenced: true)), Is.Empty, "The executor then holds: melee is banned");
    }

    [Test]
    public void Smite_NukesAndLeavesHealingToTheHealer()
    {
        SmartSpecProfile smite = Profile("Smite Cleric");
        Assert.That(smite.MeleeBanned, Is.False);

        SmartActionFamily[] order = Order(smite, See(groupMin: 50, mana: 80, target: 50));
        Assert.That(order, Is.EqualTo(new[] { SmartActionFamily.OffenseSpells, SmartActionFamily.Support }));
        Span<SmartActionCandidate> buffer = stackalloc SmartActionCandidate[SmartCandidateGenerator.FamilyCount];
        int n = SmartCandidateGenerator.Generate(smite, See(groupMin: 50, mana: 80, target: 50), buffer);
        Assert.That(smite.OffenseMayHeal(buffer[..n]), Is.False, "Its heal line only allows an ally under 25%");

        // An ally under 25%: the emergency heal goes first.
        Assert.That(Order(smite, See(groupMin: 20, mana: 80, target: 50)).First(), Is.EqualTo(SmartActionFamily.Triage));
    }

    [Test]
    public void Smite_OutOfManaFallsBackToMelee()
    {
        SmartSpecProfile smite = Profile("Smite Cleric");
        Assert.That(Order(smite, See(groupMin: 100, mana: 5, target: 50)),
            Is.EqualTo(new[] { SmartActionFamily.Support, SmartActionFamily.OffenseFull, SmartActionFamily.OffenseSpells }),
            "Below the 15% mana floor nukes score a quarter; the out-of-mana melee line opens");
    }

    [Test]
    public void Enhancement_BuffsHealsThenFights()
    {
        SmartSpecProfile enh = Profile("Enhancement Cleric");
        Assert.That(enh.MeleeBanned, Is.False);
        Assert.That(Order(enh, See(groupMin: 100, mana: 80, target: 50)),
            Is.EqualTo(new[] { SmartActionFamily.Triage, SmartActionFamily.Support, SmartActionFamily.OffenseSpells, SmartActionFamily.OffenseFull }));
        Assert.That(Order(enh, See(groupMin: 100, mana: 40, target: 50)), Has.No.Member(SmartActionFamily.OffenseSpells),
            "Its smite waits for mana above 50%");
    }

    /// <summary>Owner 2026-11-04: an Enhancement Cleric heals, and doesn't smite, when no other healer is healing.</summary>
    [Test]
    public void Enhancement_SoleHealer_HealsAndNeverSmites()
    {
        SmartSpecProfile enh = Profile("Enhancement Cleric");
        foreach (int groupMin in new[] { 100, 80, 50, 20 })
        {
            SmartActionFamily[] order = Order(enh, See(groupMin: groupMin, mana: 90, target: 50, otherHealers: 0));
            Assert.That(order.First(), Is.EqualTo(SmartActionFamily.Triage), $"group at {groupMin}%");
            Assert.That(order, Has.No.Member(SmartActionFamily.OffenseSpells), $"group at {groupMin}%");
        }
        Span<SmartActionCandidate> buffer = stackalloc SmartActionCandidate[SmartCandidateGenerator.FamilyCount];
        int n = SmartCandidateGenerator.Generate(enh, See(groupMin: 100, mana: 90, target: 50, otherHealers: 0), buffer);
        Assert.That(buffer[0].BasePriority, Is.EqualTo(100), "Its sole-healer line, above the bubble");
        Assert.That(enh.OffenseMayHeal(buffer[..n]), Is.True);

        // With a healer beside it, it smites again when the group is healthy and it has the power.
        Assert.That(Order(enh, See(groupMin: 100, mana: 90, target: 50, otherHealers: 1)), Has.Member(SmartActionFamily.OffenseSpells));
    }

    [Test]
    public void Condition_ReadsTheHealerClauses()
    {
        Assert.That(SmartCondition.Parse("When no other healer is healing").Groups.Single().Single(),
            Is.EqualTo(new SmartClause(SmartClauseKind.NoOtherHealer, 0)));
        var smite = SmartCondition.Parse("Assisting MA target when group HP > 80% and mana > 50% and another healer is healing");
        Assert.That(smite.Groups.SelectMany(g => g), Has.Member(new SmartClause(SmartClauseKind.OtherHealerActive, 0)));
        Assert.That(smite.Holds(See(groupMin: 90, mana: 60, otherHealers: 1)), Is.True);
        Assert.That(smite.Holds(See(groupMin: 90, mana: 60, otherHealers: 0)), Is.False);
        Assert.That(SmartCondition.Parse("Only if primary healer is dead and ally is < 25% HP").Groups.SelectMany(g => g),
            Has.No.Member(new SmartClause(SmartClauseKind.OtherHealerActive, 0)), "\"primary healer is dead\" is not a healer clause");
    }

    [TestCase(DOL.GS.eCharacterClass.Cleric, true)]
    [TestCase(DOL.GS.eCharacterClass.Friar, true)]
    [TestCase(DOL.GS.eCharacterClass.Healer, true)]
    [TestCase(DOL.GS.eCharacterClass.Shaman, true)]
    [TestCase(DOL.GS.eCharacterClass.Druid, true)]
    [TestCase(DOL.GS.eCharacterClass.Bard, true)]
    [TestCase(DOL.GS.eCharacterClass.Necromancer, false)]
    [TestCase(DOL.GS.eCharacterClass.Paladin, false)]
    public void HealingClasses(DOL.GS.eCharacterClass c, bool heals) =>
        Assert.That(SmartBotPerception.IsHealingClass((int)c), Is.EqualTo(heals));

    // Heal thresholds (owner 2026-11-04) ------------------------------------------------------------------------------

    [Test]
    public void HealThresholds_ReadFromTheClericFile()
    {
        SmartSpecProfile rejuv = Profile("Rejuvenation Cleric");
        Assert.That(rejuv.HealBands.Select(b => (b.Min, b.Max, string.Join("+", b.Heals))), Is.EqualTo(new[]
        {
            (70, 85, "HealEfficient"),
            (35, 70, "HealBig"),
            (0, 35, "HealInstant+HealBig"),
        }));
        Assert.That(rejuv.GroupHeal.Members, Is.EqualTo(3));
        Assert.That(rejuv.GroupHeal.Below, Is.EqualTo(70));
        Assert.That(rejuv.GroupHeal.Heals, Is.EqualTo(new[] { "HealGroup" }), "HealSpread / HealGroup are the same group heal");

        SmartSpecProfile enh = Profile("Enhancement Cleric");
        Assert.That(enh.HealBands.Select(b => (b.Min, b.Max, string.Join("+", b.Heals))), Is.EqualTo(new[]
        {
            (50, 75, "HealEfficient"),
            (0, 50, "HealBig"),
        }), "Two clauses on one line, split on ';'");
        Assert.That(enh.GroupHeal, Is.Null);
    }

    [Test]
    public void HealThresholds_SmiteHasNoneSoTheLegacyChoiceStands() =>
        Assert.That(Profile("Smite Cleric").HealBands, Is.Empty);

    private static string HealFor(SmartSpecProfile p, int health, int hurtUnder70 = 0, params string[] notReady) =>
        SmartHealThresholds.Choose(p.HealBands, p.GroupHeal, health, below => below == 70 ? hurtUnder70 : 0,
            heal => !notReady.Contains(heal));

    [Test]
    public void HealThresholds_PickTheBandsHeal()
    {
        SmartSpecProfile rejuv = Profile("Rejuvenation Cleric");
        Assert.That(HealFor(rejuv, 80), Is.EqualTo("HealEfficient"));
        Assert.That(HealFor(rejuv, 70), Is.EqualTo("HealEfficient"), "70 opens the upper band");
        Assert.That(HealFor(rejuv, 50), Is.EqualTo("HealBig"));
        Assert.That(HealFor(rejuv, 20), Is.EqualTo("HealInstant"));
        Assert.That(HealFor(rejuv, 20, 0, "HealInstant"), Is.EqualTo("HealBig"), "Instant on its timer: the big heal");
        Assert.That(HealFor(rejuv, 90), Is.Null, "Above every band: the legacy choice");
        Assert.That(HealFor(rejuv, 50, 0, "HealBig"), Is.Null, "The band's heal not ready: the legacy choice");
    }

    [Test]
    public void HealThresholds_GroupHealWhenThreeAreHurt()
    {
        SmartSpecProfile rejuv = Profile("Rejuvenation Cleric");
        Assert.That(HealFor(rejuv, 50, hurtUnder70: 3), Is.EqualTo("HealGroup"));
        Assert.That(HealFor(rejuv, 50, hurtUnder70: 2), Is.EqualTo("HealBig"));
        Assert.That(HealFor(rejuv, 50, 3, "HealGroup"), Is.EqualTo("HealBig"), "No group heal ready: the band");
    }

    [TestCase("HealSpread", "HealGroup")]
    [TestCase("HealEfficient", "HealEfficient")]
    [TestCase("HealMiracle", null)]
    public void HealThresholds_KnownKinds(string name, string kind) =>
        Assert.That(SmartHealThresholds.Canonical(name), Is.EqualTo(kind));

    [Test]
    public void Scorer_EmergenciesRaiseTriage()
    {
        SmartSpecProfile rejuv = Profile("Rejuvenation Cleric");
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.Triage, 100, rejuv, See(groupMin: 30)), Is.EqualTo(140));
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.Triage, 100, rejuv, See(groupMin: 60)), Is.EqualTo(120));
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.Triage, 100, rejuv, See(groupMin: 90)), Is.EqualTo(100));
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.OffenseSpells, 80, rejuv, See(mana: 20)), Is.EqualTo(20));
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.Triage, 80, rejuv, See(mana: 5)), Is.EqualTo(80), "Heals ignore the mana floor");
    }
}
