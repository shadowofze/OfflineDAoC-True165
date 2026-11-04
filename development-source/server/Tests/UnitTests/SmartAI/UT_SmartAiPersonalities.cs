using System;
using System.Linq;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 10: the six personalities, their bounds, the world bots' fixed variation, and that
/// Aggressive and Defensive choose differently in the same fight while never passing a hard rule.</summary>
[TestFixture]
public class UT_SmartAiPersonalities
{
    // A small spec: nuke 80, bubble 85, heal 70 (only when an ally is under 65%).
    private const string Spec = "---\nschema_version: 1\nclass_name: \"Cleric\"\nclass_id: 6\n---\n" +
        "## Specialization 1: Test\n- **role**: HybridSupport\n- **default_personality**: Tactical\n" +
        "### 7. Offensive Priorities\n1. Direct Damage (Smite): Priority 80\n" +
        "### 8. Defensive Priorities\n1. Self-Cast Absorb Bubble: Priority 85\n2. Spot-Heal: Priority 70 (When ally HP < 65%)\n";

    private static SmartSpecProfile Profile() => SmartSpecProfile.Compile(SmartAiMarkdownParser.Parse(Spec, "t.md").Class.Specs.Single());

    private static SmartBotPerception See(SmartPersonality personality, int groupMin = 100, int target = 60) => new()
    {
        SelfManaPercent = 80,
        GroupMinHealthPercent = groupMin,
        MembersBelow65 = groupMin < 65 ? 1 : 0,
        MembersBelow40 = groupMin < 40 ? 1 : 0,
        InCombat = true,
        HasTarget = true,
        TargetHealthPercent = target,
        Personality = SmartPersonalityProfile.For(personality),
    };

    private static SmartActionFamily[] Order(SmartBotPerception p)
    {
        Span<SmartActionCandidate> buffer = stackalloc SmartActionCandidate[SmartCandidateGenerator.FamilyCount];
        int n = SmartCandidateGenerator.Generate(Profile(), p, buffer);
        return buffer[..n].ToArray().Select(c => c.Family).ToArray();
    }

    [Test]
    public void AggressiveAndDefensive_ChooseDifferently()
    {
        Assert.That(Order(See(SmartPersonality.Auto)).First(), Is.EqualTo(SmartActionFamily.Support), "Neutral: bubble 85 over nuke 80");
        Assert.That(Order(See(SmartPersonality.Aggressive)).First(), Is.EqualTo(SmartActionFamily.OffenseSpells), "80 x 120% = 96 over 85 x 85% = 72");
        Assert.That(Order(See(SmartPersonality.Defensive)).First(), Is.EqualTo(SmartActionFamily.Support));
        Assert.That(Order(See(SmartPersonality.Supportive)).First(), Is.EqualTo(SmartActionFamily.Support));
    }

    [Test]
    public void Opportunistic_JumpsOnAWeakTarget()
    {
        var healthy = See(SmartPersonality.Opportunistic, target: 80);
        var weak = See(SmartPersonality.Opportunistic, target: 20);
        Assert.That(Order(healthy).First(), Is.EqualTo(SmartActionFamily.OffenseSpells), "80 x 115% = 92 over 85 x 95% = 80");
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.OffenseSpells, 80, Profile(), weak),
            Is.EqualTo(80 * SmartPersonalityProfile.MaxMultiplier / 100), "115% x 125% is held to 125%");
    }

    [Test]
    public void Emergency_IgnoresPersonality()
    {
        // An ally under 40%: the spec's own order, whatever the personality.
        SmartActionFamily[] neutral = Order(See(SmartPersonality.Auto, groupMin: 30));
        foreach (SmartPersonality p in Enum.GetValues<SmartPersonality>())
            Assert.That(Order(See(p, groupMin: 30)), Is.EqualTo(neutral), p.ToString());
        Assert.That(neutral.First(), Is.EqualTo(SmartActionFamily.Triage), "Heal 70 + 40 emergency first");
    }

    [Test]
    public void Personality_NeverOpensAGate()
    {
        var mezzed = See(SmartPersonality.Aggressive, target: 10) with { TargetControlled = true };
        Assert.That(Order(mezzed), Has.No.Member(SmartActionFamily.OffenseSpells), "Nobody breaks a mez");
    }

    [Test]
    public void Multipliers_StayWithinBounds()
    {
        var p = See(SmartPersonality.Auto, target: 10) with { TargetIsGroupTarget = true };
        foreach (SmartPersonality personality in Enum.GetValues<SmartPersonality>())
        {
            SmartPersonalityProfile profile = SmartPersonalityProfile.For(personality);
            Assert.That(profile.Personality, Is.EqualTo(personality));
            foreach (SmartActionFamily family in Enum.GetValues<SmartActionFamily>())
                Assert.That(profile.Multiplier(family, p), Is.InRange(SmartPersonalityProfile.MinMultiplier, SmartPersonalityProfile.MaxMultiplier),
                    $"{personality} {family}");
        }
    }

    [Test]
    public void ManaReserve_ReplacesTheSpecFloor()
    {
        var p = See(SmartPersonality.Auto) with { SelfManaPercent = 25 };
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.OffenseSpells, 80, Profile(), p), Is.EqualTo(80), "The spec has no floor");
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.OffenseSpells, 80, Profile(), p with { ManaFloorOverride = 35 }), Is.EqualTo(20));
    }

    [TestCase("aggressive", SmartPersonality.Aggressive)]
    [TestCase(" Defensive ", SmartPersonality.Defensive)]
    [TestCase("AUTO", SmartPersonality.Auto)]
    public void Parse(string text, SmartPersonality expected)
    {
        Assert.That(SmartPersonalityProfile.TryParse(text, out SmartPersonality p), Is.True);
        Assert.That(p, Is.EqualTo(expected));
    }

    [TestCase("brave")]
    [TestCase("3")]
    [TestCase("")]
    [TestCase(null)]
    public void Parse_RejectsOthers(string text) => Assert.That(SmartPersonalityProfile.TryParse(text, out _), Is.False);

    [TestCase("Cedric personality aggressive", true, SmartPersonality.Aggressive)]
    [TestCase("cedric, PERSONALITY supportive please", true, SmartPersonality.Supportive)]
    [TestCase("Cedric what is your personality", false, SmartPersonality.Auto)]
    [TestCase("Cedric personality brave", false, SmartPersonality.Auto)]
    public void Voice(string line, bool parsed, SmartPersonality expected)
    {
        Assert.That(SmartPersonalityCommands.TryParseVoice(line, out SmartPersonality p), Is.EqualTo(parsed));
        if (parsed)
            Assert.That(p, Is.EqualTo(expected));
    }

    [Test]
    public void WorldBots_FixedVariationAboutSixtyFortyNoAggressiveHealers()
    {
        int kept = 0;
        for (long id = 1; id <= 10_000; id++)
        {
            SmartPersonality p = SmartPersonalityRegistry.Variation(id, SmartPersonality.Tactical, false);
            Assert.That(SmartPersonalityRegistry.Variation(id, SmartPersonality.Tactical, false), Is.EqualTo(p), "Same bot, same personality");
            if (p == SmartPersonality.Tactical)
                kept++;
            SmartPersonality healer = SmartPersonalityRegistry.Variation(id, SmartPersonality.Supportive, true);
            Assert.That(healer, Is.Not.EqualTo(SmartPersonality.Aggressive).And.Not.EqualTo(SmartPersonality.Opportunistic));
        }
        // 60% keep the default, plus the variations that land on it again (1 in 6 of the rest).
        Assert.That(kept, Is.InRange(6_300, 7_000));
    }
}
