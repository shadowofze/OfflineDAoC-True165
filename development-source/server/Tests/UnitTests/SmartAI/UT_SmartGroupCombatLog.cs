using DOL.GS.SmartAI;
using NUnit.Framework;
using static DOL.GS.SmartAI.SmartGroupCombatLog;

/// <summary>Owner 2026-11-05: /smartai debuggroup writes only complete fights - once the whole group is out of combat.</summary>
[TestFixture]
public class UT_SmartGroupCombatLog
{
    private const long Start = 100_000;

    [Test]
    public void AFightIsCompleteOnlyWhenNobodyFightsAndNoHeldEnemyIsNear()
    {
        Assert.That(EndOf(true, false, Start + 10_000, Start, Start + 9_000), Is.EqualTo(FightEnd.Ongoing), "A member still fighting");
        Assert.That(EndOf(false, true, Start + 10_000, Start, Start + 9_000), Is.EqualTo(FightEnd.Ongoing), "A mezzed add still near");
        Assert.That(EndOf(false, false, Start + 10_000, Start, Start + 9_000), Is.EqualTo(FightEnd.Complete));
    }

    [Test]
    public void AStuckFightIsClosedWithANote()
    {
        Assert.That(EndOf(true, false, Start + StaleCloseMs + 1, Start, Start), Is.EqualTo(FightEnd.Stale));
        Assert.That(EndOf(false, true, Start + StaleCloseMs, Start, Start), Is.EqualTo(FightEnd.Ongoing), "Not before the limit");
        Assert.That(EndOf(true, false, Start + MaxFightMs + 1, Start, Start + MaxFightMs), Is.EqualTo(FightEnd.TooLong));
    }

    [Test]
    public void AMemberFightsForAFewSecondsAfterItsLastBlow()
    {
        Assert.That(RecentBlow(Start + CombatWindowMs - 1, Start), Is.True);
        Assert.That(RecentBlow(Start + CombatWindowMs, Start), Is.False, "Out of combat: the fight can end");
        Assert.That(RecentBlow(Start, 0), Is.False, "Never fought");
    }

    [Test]
    public void ShortNamesAndSafeFileNames()
    {
        Assert.That(Short(SmartActionFamily.Triage), Is.EqualTo("Heal"));
        Assert.That(Short(SmartActionFamily.OffenseFull), Is.EqualTo("Melee"));
        Assert.That(SafeName("Sir Bob/../x"), Is.EqualTo("Sir_Bob____x"));
        Assert.That(SafeName(null), Is.EqualTo("group"));
    }

    [Test]
    public void PositionUsesThe165StyleArcs()
    {
        // StyleProcessor: back 150-210 (exclusive), side 45-150 and 210-315, front otherwise
        Assert.That(PositionOf(180), Is.EqualTo(Position.Back));
        Assert.That(PositionOf(151), Is.EqualTo(Position.Back));
        Assert.That(PositionOf(150), Is.EqualTo(Position.Side));
        Assert.That(PositionOf(90), Is.EqualTo(Position.Side));
        Assert.That(PositionOf(270), Is.EqualTo(Position.Side));
        Assert.That(PositionOf(45), Is.EqualTo(Position.Side));
        Assert.That(PositionOf(44), Is.EqualTo(Position.Front));
        Assert.That(PositionOf(0), Is.EqualTo(Position.Front));
        Assert.That(PositionOf(330), Is.EqualTo(Position.Front));
    }

    [Test]
    public void ADefenseOpensItsReaction()
    {
        Assert.That(ReactionTo(DOL.GS.eAttackResult.Parried), Is.EqualTo(DOL.GS.Styles.Style.eAttackResultRequirement.Parry));
        Assert.That(ReactionTo(DOL.GS.eAttackResult.Blocked), Is.EqualTo(DOL.GS.Styles.Style.eAttackResultRequirement.Block));
        Assert.That(ReactionTo(DOL.GS.eAttackResult.Evaded), Is.EqualTo(DOL.GS.Styles.Style.eAttackResultRequirement.Evade));
        Assert.That(ReactionTo(DOL.GS.eAttackResult.HitUnstyled), Is.EqualTo(DOL.GS.Styles.Style.eAttackResultRequirement.Any));
    }

    [Test]
    public void OffByDefault_AndStopClears()
    {
        Assert.That(IsOn, Is.False);
        RecordAll();
        Assert.That(CurrentScope, Is.EqualTo(Scope.AllGroups));
        Stop();
        Assert.That(IsOn, Is.False);
        Assert.That(GroupsTracked, Is.EqualTo(0));
    }
}
