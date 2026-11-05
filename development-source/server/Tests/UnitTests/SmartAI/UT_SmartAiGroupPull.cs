using System.Collections.Generic;
using DOL.GS;
using DOL.GS.SmartAI;
using NUnit.Framework;
using GearItem = DOL.GS.SmartAI.SmartGroupPullPolicy.GearItem;

/// <summary>Owner 2026-11-05: a gear score for the group (the tank counts most) chooses the level it pulls, and low
/// levels are harder on higher monsters. Also the player's 3 s self-interrupt for bots.</summary>
[TestFixture]
public class UT_SmartAiGroupPull
{
    private static GearItem? Item(int level, int quality = 100, int condition = 100) => new GearItem(level, quality, condition, 100);

    private static List<GearItem?> Armour(int level, int quality = 100) =>
        new() { Item(level, quality), Item(level, quality), Item(level, quality), Item(level, quality), Item(level, quality), Item(level, quality) };

    [Test]
    public void FullKitAtOwnLevelScoresAHundred() =>
        Assert.That(SmartGroupPullPolicy.MemberScore(Armour(20), Item(20), 20), Is.EqualTo(100));

    [Test]
    public void LowLevelQualityAndWornGearScoreLess()
    {
        Assert.That(SmartGroupPullPolicy.MemberScore(Armour(10), Item(10), 20), Is.EqualTo(50));
        Assert.That(SmartGroupPullPolicy.MemberScore(Armour(20, 85), Item(20, 85), 20), Is.EqualTo(85));
        Assert.That(SmartGroupPullPolicy.ItemScore(Item(20, 100, 50), 20), Is.EqualTo(50).Within(0.01));
    }

    [Test]
    public void EmptySlotsScoreZero()
    {
        Assert.That(SmartGroupPullPolicy.MemberScore(new List<GearItem?>(), null, 20), Is.EqualTo(0));
        // Weapon only (weight 2 of 8).
        Assert.That(SmartGroupPullPolicy.MemberScore(new List<GearItem?>(), Item(20), 20), Is.EqualTo(25));
    }

    [Test]
    public void AboveLevelGearIsCappedAndTheWholeScoreAtAHundred()
    {
        Assert.That(SmartGroupPullPolicy.ItemScore(Item(40), 20), Is.EqualTo(125).Within(0.01));
        Assert.That(SmartGroupPullPolicy.MemberScore(Armour(40), Item(40), 20), Is.EqualTo(100));
    }

    [Test]
    public void TheTankCountsTwice() =>
        Assert.That(SmartGroupPullPolicy.GroupScore(new List<(int, bool)> { (40, true), (100, false) }), Is.EqualTo(60));

    [Test]
    public void AWellGearedFullGroupAtFiftyPullsPurple()
    {
        SmartPullBand band = SmartGroupPullPolicy.Band(8, 50, 95, true, true, 0);
        Assert.That(band.Highest, Is.EqualTo(ConColor.PURPLE));
        Assert.That(band.Lowest, Is.EqualTo(ConColor.RED));
    }

    [Test]
    public void LowerLevelsPullLowerColours()
    {
        Assert.That(SmartGroupPullPolicy.Band(6, 30, 95, true, true, 0).Highest, Is.EqualTo(ConColor.RED));
        Assert.That(SmartGroupPullPolicy.Band(6, 15, 95, true, true, 0).Highest, Is.EqualTo(ConColor.ORANGE));
        Assert.That(SmartGroupPullPolicy.Band(6, 5, 95, true, true, 0).Highest, Is.EqualTo(ConColor.ORANGE));
    }

    [Test]
    public void GearIsJudgedAgainstWhatIsNormalForTheLevel()
    {
        Assert.That(SmartGroupPullPolicy.ExpectedGearScore(6), Is.EqualTo(45));
        Assert.That(SmartGroupPullPolicy.ExpectedGearScore(50), Is.EqualTo(80));
        // Groups from the 2026-11-05 run, which all got green-blue before the retune.
        Assert.That(SmartGroupPullPolicy.Band(5, 6, 63, true, true, 0).Highest, Is.EqualTo(ConColor.YELLOW));
        Assert.That(SmartGroupPullPolicy.Band(6, 7, 74, true, true, 0).Highest, Is.EqualTo(ConColor.ORANGE));
        // Weak gear: the full-group floor keeps yellow.
        SmartPullBand weak = SmartGroupPullPolicy.Band(6, 4, 22, true, true, 0);
        Assert.That(weak.Highest, Is.EqualTo(ConColor.YELLOW));
        Assert.That(weak.Lowest, Is.EqualTo(ConColor.BLUE));
        Assert.That(weak.Why, Does.Contain("yellow floor"));
        // No floor without a healer, or after a wipe.
        Assert.That(SmartGroupPullPolicy.Band(6, 4, 22, true, false, 0).Highest, Is.EqualTo(ConColor.BLUE));
        Assert.That(SmartGroupPullPolicy.Band(6, 4, 22, true, true, 1).Highest, Is.EqualTo(ConColor.BLUE));
    }

    [Test]
    public void WeakGearMissingRolesAndWipesLowerTheBandButNeverBelowBlue()
    {
        Assert.That(SmartGroupPullPolicy.Band(8, 50, 70, true, true, 0).Highest, Is.EqualTo(ConColor.RED));
        Assert.That(SmartGroupPullPolicy.Band(8, 50, 60, true, true, 0).Highest, Is.EqualTo(ConColor.ORANGE));
        Assert.That(SmartGroupPullPolicy.Band(8, 50, 95, false, true, 0).Highest, Is.EqualTo(ConColor.RED));
        Assert.That(SmartGroupPullPolicy.Band(8, 50, 95, true, true, 5).Highest, Is.EqualTo(ConColor.ORANGE));
        SmartPullBand floor = SmartGroupPullPolicy.Band(3, 5, 10, false, false, 3);
        Assert.That(floor.Highest, Is.EqualTo(ConColor.BLUE));
        Assert.That(floor.Lowest, Is.EqualTo(ConColor.GREEN));
    }

    [Test]
    public void TheBandPicksTheHighestLevelThatFits()
    {
        // Level 5: 5 yellow, 6 orange, 7 red, 8 purple.
        SmartPullBand band = SmartGroupPullPolicy.Band(5, 25, 95, true, true, 0) with { };
        SmartPullBand yellowTop = new(ConColor.BLUE, ConColor.YELLOW, 70, "test");
        Assert.That(yellowTop.Select(new[] { 4, 5, 6, 7, 8 }, 5), Is.EqualTo(5));
        Assert.That(yellowTop.Allows(5, 8), Is.False);
        Assert.That(yellowTop.HighestLevel(5), Is.EqualTo(5));
        Assert.That(band.Highest, Is.EqualTo(ConColor.ORANGE));
    }

    [Test]
    public void BotsSelfInterruptLikeAPlayer() =>
        Assert.That(GameBot.PlayerSelfInterruptOnMeleeMs, Is.EqualTo(3000));
}
