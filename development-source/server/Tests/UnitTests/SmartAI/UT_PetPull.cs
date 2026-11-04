using DOL.GS;
using NUnit.Framework;

/// <summary>/petpull (owner 2026-11-04, class templates section 6b): companions leave the monsters on the player's pet to
/// it until 85%, then only casters and healers join; break-offs are fought; the pet is healed once a monster on it has
/// lost ~5%.</summary>
[TestFixture]
public class UT_PetPull
{
    [TestCase(100, true, false)]
    [TestCase(86, true, false)]
    [TestCase(85, true, true)]
    [TestCase(40, true, true)]
    [TestCase(85, false, false)]
    [TestCase(20, false, false)]
    public void MonsterOnThePet(int monsterHealth, bool casterOrHealer, bool allowed) =>
        Assert.That(CompanionEngagementMode.PetPullAllows(true, monsterHealth, casterOrHealer), Is.EqualTo(allowed));

    [TestCase(100, false)]
    [TestCase(100, true)]
    public void BreakOffsAreFoughtByEveryone(int monsterHealth, bool casterOrHealer) =>
        Assert.That(CompanionEngagementMode.PetPullAllows(false, monsterHealth, casterOrHealer), Is.True);

    [Test]
    public void PetHeal_WaitsForFivePercent()
    {
        Assert.That(CompanionEngagementMode.HoldPetHeal(true, 100), Is.True);
        Assert.That(CompanionEngagementMode.HoldPetHeal(true, 96), Is.True);
        Assert.That(CompanionEngagementMode.HoldPetHeal(true, 95), Is.False, "A monster has lost 5%: heal");
        Assert.That(CompanionEngagementMode.HoldPetHeal(false, 100), Is.False, "Nothing on the pet: heal it as normal");
    }

    [Test]
    public void ModeIsOffWithoutAPlayer()
    {
        Assert.That(CompanionEngagementMode.IsPetPulling(null), Is.False);
        Assert.That(CompanionEngagementMode.PetPullLeader(null), Is.Null);
        Assert.That(CompanionEngagementMode.PetPullStaysPut(null, null), Is.False);
        Assert.That(CompanionEngagementMode.PetPullSmallHeal(null, null), Is.False);
    }

    // AoE only on the pack ---------------------------------------------------------------------------------------------

    [TestCase(true, 0, false, false)]      // a single-target nuke on the pack
    [TestCase(true, 0, true, false)]       // a single-target mez or stun on the pack
    [TestCase(true, 350, false, true)]     // an area nuke on the pack
    [TestCase(true, 350, true, false)]     // an area mez on the pack (it would stop the pack hitting the pet)
    [TestCase(false, 0, false, true)]      // a single-target nuke on a monster that broke off
    [TestCase(false, 0, true, true)]       // a mez on a monster that broke off
    public void SpellOnThePack(bool onPet, int radius, bool crowdControl, bool allowed) =>
        Assert.That(CompanionEngagementMode.PetPullSpellAllowed(onPet, radius, crowdControl), Is.EqualTo(allowed));

    // Smallest heal --------------------------------------------------------------------------------------------------

    [TestCase(100, true)]
    [TestCase(60, true)]
    [TestCase(50, true)]
    [TestCase(49, false)]
    [TestCase(10, false)]
    public void SmallHealUnlessThePetIsGoingDown(int petHealth, bool small) =>
        Assert.That(CompanionEngagementMode.UseSmallHeal(petHealth), Is.EqualTo(small));
}
