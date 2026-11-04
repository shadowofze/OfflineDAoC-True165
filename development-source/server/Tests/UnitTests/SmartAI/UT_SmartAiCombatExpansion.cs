using System;
using System.IO;
using System.Linq;
using DOL.GS;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 6: realm ability choice and the style eligibility switch. Swings, positions and the
/// abilities' effects run through the native handlers and are checked in game.</summary>
[TestFixture]
public class UT_SmartAiCombatExpansion
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

    private static SmartBotPerception See(int hp = 100, int mana = 80, int endurance = 100, int groupMin = 100,
        bool incapacitated = false, bool inCombat = true) => new()
    {
        SelfHealthPercent = hp,
        SelfManaPercent = mana,
        SelfEndurancePercent = endurance,
        GroupMinHealthPercent = Math.Min(hp, groupMin),
        InCombat = inCombat,
        Incapacitated = incapacitated,
        HasTarget = true,
        TargetHealthPercent = 50,
    };

    private static readonly Func<string, bool> AllReady = _ => true;

    [Test]
    public void Rejuvenation_KeepsOnlyTheAbilitiesABotCanUse()
    {
        // Its section 11: Purge, QuickCast (no key), MoC and Bunker of Faith (player-only handlers), MCL.
        Assert.That(Profile("Rejuvenation Cleric").Abilities.Select(a => (a.Key, a.Priority)),
            Is.EqualTo(new[] { ("AtlasOF_Purge", 100), ("AtlasOF_MCL", 80) }));
    }

    [Test]
    public void Purge_OnlyWhenMezzedOrStunnedAndTheConditionHolds()
    {
        SmartSpecProfile rejuv = Profile("Rejuvenation Cleric");
        // "Use immediately if stunned/mezzed while any ally is < 50% HP".
        Assert.That(SmartRealmAbilities.Choose(rejuv, See(incapacitated: true, groupMin: 40), AllReady)?.Key, Is.EqualTo("AtlasOF_Purge"));
        Assert.That(SmartRealmAbilities.Choose(rejuv, See(incapacitated: true, groupMin: 90), AllReady), Is.Null,
            "Nobody below 50%: the Purge is saved");
        Assert.That(SmartRealmAbilities.Choose(rejuv, See(incapacitated: false, groupMin: 10), AllReady), Is.Null,
            "Never a Purge with nothing to purge");
        Assert.That(SmartRealmAbilities.Choose(rejuv, See(incapacitated: true, groupMin: 40), key => key != "AtlasOF_Purge"), Is.Null,
            "Not while it is on its reuse timer");
    }

    [Test]
    public void Mcl_OnlyOutOfCombatAndLow()
    {
        SmartSpecProfile rejuv = Profile("Rejuvenation Cleric");
        // "Use when mana drops below 25%"; the handler itself refuses in combat.
        Assert.That(SmartRealmAbilities.Choose(rejuv, See(mana: 20, inCombat: false), AllReady)?.Key, Is.EqualTo("AtlasOF_MCL"));
        Assert.That(SmartRealmAbilities.Choose(rejuv, See(mana: 20, inCombat: true), AllReady), Is.Null);
        Assert.That(SmartRealmAbilities.Choose(rejuv, See(mana: 50, inCombat: false), AllReady), Is.Null);
    }

    [TestCase("AtlasOF_Purge", true)]
    [TestCase("AtlasOF_PurgeReduced", true)]
    [TestCase("AtlasOF_IgnorePain", true)]
    [TestCase("AtlasOF_SecondWind", true)]
    [TestCase("AtlasOF_FirstAid", true)]
    [TestCase("AtlasOF_MCL", true)]
    [TestCase("AtlasOF_MasteryOfConcentration", false)]
    [TestCase("AtlasOF_BunkerOfFaith", false)]
    public void OnlyBotSafeActives(string key, bool safe) => Assert.That(SmartRealmAbilities.IsSafe(key), Is.EqualTo(safe));

    [Test]
    public void HardPreconditions()
    {
        Assert.That(SmartRealmAbilities.Wanted("AtlasOF_IgnorePain", See(hp: 30)), Is.True);
        Assert.That(SmartRealmAbilities.Wanted("AtlasOF_IgnorePain", See(hp: 80)), Is.False);
        Assert.That(SmartRealmAbilities.Wanted("AtlasOF_IgnorePain", See(hp: 30, incapacitated: true)), Is.False);
        Assert.That(SmartRealmAbilities.Wanted("AtlasOF_SecondWind", See(endurance: 10)), Is.True);
        Assert.That(SmartRealmAbilities.Wanted("AtlasOF_SecondWind", See(endurance: 60)), Is.False);
        Assert.That(SmartRealmAbilities.Wanted("AtlasOF_FirstAid", See(hp: 50, inCombat: false)), Is.True);
        Assert.That(SmartRealmAbilities.Wanted("AtlasOF_FirstAid", See(hp: 50, inCombat: true)), Is.False);
    }

    [Test]
    public void StyleEligibility_OnlySmartAiOpensPositionalAndStealth()
    {
        Assert.That(BotMeleeStylePolicy.IsEligible(null, true), Is.False);
        Assert.That(SmartStyleSelector.Drives(null), Is.False);
    }
}
