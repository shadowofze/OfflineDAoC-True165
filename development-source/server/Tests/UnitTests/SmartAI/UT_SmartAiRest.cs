using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>Owner 2026-11-04: healers and casters in a player's group rest below 90% power while nobody fights, keep
/// resting while the player stays within 800 units, and follow as normal beyond.</summary>
[TestFixture]
public class UT_SmartAiRest
{
    private static bool Rest(bool combat = false, bool present = true, int distance = 200, bool leaderMoving = false,
        bool resting = false, int hp = 100, int mana = 80, int end = 100) =>
        SmartRestPolicy.ShouldRest(combat, present, distance, leaderMoving, resting, hp, mana, end);

    [Test]
    public void SitsBelowNinetyPowerWhenQuiet()
    {
        Assert.That(Rest(mana: 89), Is.True);
        Assert.That(Rest(mana: 90), Is.False, "90% and over: no need");
        Assert.That(Rest(mana: 50), Is.True, "Long before the legacy 45%");
    }

    [Test]
    public void LegacyHealthAndEnduranceStillCount() => Assert.That(Rest(mana: 100, hp: 60), Is.True);

    [Test]
    public void NeverInCombat()
    {
        Assert.That(Rest(combat: true, mana: 10), Is.False);
        Assert.That(Rest(combat: true, resting: true, mana: 10), Is.False, "A fight wakes it");
    }

    [Test]
    public void StartsOnlyWhenThePlayerStopsAndItIsInFormation()
    {
        Assert.That(Rest(leaderMoving: true), Is.False, "Not on the road");
        Assert.That(Rest(distance: 351), Is.False, "Still walking back to formation");
        Assert.That(Rest(distance: 350), Is.True);
    }

    [Test]
    public void KeepsRestingWhileThePlayerStaysWithinEightHundred()
    {
        Assert.That(Rest(resting: true, leaderMoving: true, distance: 600), Is.True, "The player moving about nearby");
        Assert.That(Rest(resting: true, distance: 800), Is.True);
        Assert.That(Rest(resting: true, mana: 99), Is.True, "Until full");
        Assert.That(Rest(resting: true, mana: 100), Is.False, "Full: up");
    }

    [Test]
    public void FollowsBeyondEightHundred()
    {
        Assert.That(Rest(resting: true, distance: 801), Is.False);
        Assert.That(Rest(resting: true, present: false), Is.False, "Player gone (another region, dead): follow the legacy way");
    }
}
