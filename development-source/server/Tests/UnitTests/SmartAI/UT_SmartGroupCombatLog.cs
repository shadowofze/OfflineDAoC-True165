using DOL.GS.SmartAI;
using NUnit.Framework;
using static DOL.GS.SmartAI.SmartGroupCombatLog;

/// <summary>Owner 2026-11-05: /smartai debuggroup writes only complete fights - once the whole group is out of combat.</summary>
[TestFixture]
public class UT_SmartGroupCombatLog
{
    private const long Start = 100_000;

    [Test]
    public void FinishedFightsWaitInMemoryAndEachFileGetsOneAppend()
    {
        // Owner 2026-11-05: fewer writes to the drive. Nothing reaches the disk until the batch is flushed.
        string dir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "ut-smart-group-log-" + System.Guid.NewGuid().ToString("N"));
        string a = System.IO.Path.Combine(dir, "a.log"), b = System.IO.Path.Combine(dir, "b.log");
        try
        {
            FlushPending(wait: true);
            BufferFight(a, "header A\n", "fight 1\n");
            BufferFight(a, "header A again\n", "fight 2\n");
            BufferFight(b, null, "fight 3\n");
            Assert.That(System.IO.File.Exists(a) || System.IO.File.Exists(b), Is.False, "still in memory");
            Assert.That(PendingFiles, Is.EqualTo(2));
            FlushPending(wait: true);
            Assert.Multiple(() =>
            {
                Assert.That(PendingFiles, Is.EqualTo(0));
                Assert.That(System.IO.File.ReadAllText(a), Is.EqualTo("header A\nfight 1\nfight 2\n"), "the first header only, fights in order");
                Assert.That(System.IO.File.ReadAllText(b), Is.EqualTo("fight 3\n"));
            });
            BufferFight(a, "header A\n", "fight 4\n");
            FlushPending(wait: true);
            Assert.That(System.IO.File.ReadAllText(a), Is.EqualTo("header A\nfight 1\nfight 2\nfight 4\n"), "no second header in an existing file");
        }
        finally
        {
            if (System.IO.Directory.Exists(dir))
                System.IO.Directory.Delete(dir, true);
        }
    }

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
