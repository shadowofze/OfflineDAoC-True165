using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 7: the group blackboard. Snapshots are swapped whole (no torn reads), claims keep two
/// bots off the same target, the main assist election and the primary target hold.</summary>
[TestFixture]
public class UT_SmartAiGroupState
{
    private static SmartGroupTacticalSnapshot Snapshot(long version) => new()
    {
        Version = version,
        Tick = version * 10,
        GroupMinHealthPercent = (int)(version % 100),
        MemberCount = (int)(version % 8) + 1,
        PrimaryTargetLockUntil = version * 7,
        Checksum = version * 10 + version % 100 + (version % 8 + 1) + version * 7,
    };

    private static bool Consistent(SmartGroupTacticalSnapshot s) =>
        s.Version == 0 || s.Checksum == s.Tick + s.GroupMinHealthPercent + s.MemberCount + s.PrimaryTargetLockUntil;

    [Test]
    public void EightReaders_OneWriter_NoTornReads()
    {
        var state = new SmartGroupTacticalState();
        const int Publishes = 200_000;
        int torn = 0, backwards = 0;
        using var stop = new CancellationTokenSource();

        Task[] readers = Enumerable.Range(0, 8).Select(_ => Task.Run(() =>
        {
            long last = 0;
            while (!stop.IsCancellationRequested)
            {
                SmartGroupTacticalSnapshot s = state.Current;
                if (!Consistent(s))
                    Interlocked.Increment(ref torn);
                if (s.Version < last)
                    Interlocked.Increment(ref backwards);
                last = s.Version;
            }
        })).ToArray();

        for (long v = 1; v <= Publishes; v++)
            state.Publish(Snapshot(v));
        stop.Cancel();
        Task.WaitAll(readers);

        Assert.That(torn, Is.Zero, "A reader saw a half-written snapshot");
        Assert.That(backwards, Is.Zero, "A reader saw an older snapshot after a newer one");
        Assert.That(state.Current.Version, Is.EqualTo(Publishes));
    }

    [Test]
    public void Claims_OneOwnerAtATime()
    {
        var state = new SmartGroupTacticalState();
        object add = new(), cleric = new(), sorc = new();
        Assert.That(state.TryClaim(SmartClaimKind.CrowdControl, add, cleric, 0, 5_000), Is.True);
        Assert.That(state.TryClaim(SmartClaimKind.CrowdControl, add, sorc, 1_000, 6_000), Is.False, "No double mez");
        Assert.That(state.IsClaimedByOther(SmartClaimKind.CrowdControl, add, sorc, 1_000), Is.True);
        Assert.That(state.IsClaimedByOther(SmartClaimKind.CrowdControl, add, cleric, 1_000), Is.False);
        Assert.That(state.TryClaim(SmartClaimKind.Heal, add, sorc, 1_000, 6_000), Is.True, "Kinds are separate");
        Assert.That(state.TryClaim(SmartClaimKind.CrowdControl, add, cleric, 2_000, 7_000), Is.True, "The owner may renew");
        Assert.That(state.TryClaim(SmartClaimKind.CrowdControl, add, sorc, 7_000, 9_000), Is.True, "Expired: free again");

        state.Release(SmartClaimKind.CrowdControl, add, cleric);
        Assert.That(state.IsClaimedByOther(SmartClaimKind.CrowdControl, add, cleric, 7_500), Is.True, "Only the owner releases");
        state.Release(SmartClaimKind.CrowdControl, add, sorc);
        Assert.That(state.IsClaimedByOther(SmartClaimKind.CrowdControl, add, cleric, 7_500), Is.False);
    }

    [Test]
    public void Claims_PruneDropsExpired()
    {
        var state = new SmartGroupTacticalState();
        state.TryClaim(SmartClaimKind.Heal, new object(), new object(), 0, 1_000);
        state.TryClaim(SmartClaimKind.Heal, new object(), new object(), 0, 5_000);
        state.PruneClaims(2_000);
        Assert.That(state.ClaimCount(2_000), Is.EqualTo(1));
    }

    [Test]
    public void Claims_ConcurrentClaimersGetOneWinner()
    {
        var state = new SmartGroupTacticalState();
        object target = new();
        int winners = 0;
        Parallel.For(0, 64, i =>
        {
            if (state.TryClaim(SmartClaimKind.Interrupt, target, new object(), 0, 10_000))
                Interlocked.Increment(ref winners);
        });
        Assert.That(winners, Is.EqualTo(1));
    }

    [Test]
    public void Activity_StopsPublishingAfterFiveSeconds()
    {
        var state = new SmartGroupTacticalState();
        state.Touch(10_000);
        Assert.That(state.IsActive(14_000), Is.True);
        Assert.That(state.IsActive(15_001), Is.False);
    }

    // Main assist ---------------------------------------------------------------------------------------------------

    private static SmartMemberRole Human(bool leader = true, bool alive = true) => new(true, leader, alive, false, false);
    private static SmartMemberRole Bot(bool tank = false, bool support = false, bool alive = true) => new(false, false, alive, tank, support);

    [Test]
    public void MainAssist_HumanLeaderFirst()
    {
        Assert.That(SmartGroupSnapshotBuilder.ElectMainAssist([Bot(tank: true), Human()]), Is.EqualTo(1));
        Assert.That(SmartGroupSnapshotBuilder.ElectMainAssist([Bot(tank: true), Human(alive: false)]), Is.EqualTo(0), "A dead leader can't call targets");
        Assert.That(SmartGroupSnapshotBuilder.ElectMainAssist([Human(leader: false), Bot(tank: true)]), Is.EqualTo(1), "A human who isn't leader doesn't take over");
    }

    [Test]
    public void MainAssist_TankThenAttackerThenAnyone()
    {
        Assert.That(SmartGroupSnapshotBuilder.ElectMainAssist([Bot(support: true), Bot(), Bot(tank: true)]), Is.EqualTo(2));
        Assert.That(SmartGroupSnapshotBuilder.ElectMainAssist([Bot(support: true), Bot()]), Is.EqualTo(1));
        Assert.That(SmartGroupSnapshotBuilder.ElectMainAssist([Bot(tank: true, alive: false), Bot(support: true)]), Is.EqualTo(1));
        Assert.That(SmartGroupSnapshotBuilder.ElectMainAssist([Bot(alive: false)]), Is.EqualTo(-1));
    }

    // Primary target hold ------------------------------------------------------------------------------------------

    private static readonly object A = new(), B = new();

    [Test]
    public void Hold_KeepsTheTargetForThreeAndAHalfSeconds()
    {
        var (t, until) = SmartGroupSnapshotBuilder.HoldTarget<object>(null, 0, false, A, false, 1_000);
        Assert.That(t, Is.SameAs(A));
        Assert.That(until, Is.EqualTo(1_000 + SmartGroupSnapshotBuilder.TargetHoldMs));

        (t, until) = SmartGroupSnapshotBuilder.HoldTarget(A, until, true, B, false, 2_000);
        Assert.That(t, Is.SameAs(A), "No thrashing inside the hold");

        (t, until) = SmartGroupSnapshotBuilder.HoldTarget(A, until, true, B, false, 5_000);
        Assert.That(t, Is.SameAs(B), "The hold ran out");
        Assert.That(until, Is.EqualTo(5_000 + SmartGroupSnapshotBuilder.TargetHoldMs));
    }

    [Test]
    public void Hold_BreaksForAHumanOrAnInvalidTarget()
    {
        Assert.That(SmartGroupSnapshotBuilder.HoldTarget(A, 9_000, true, B, true, 2_000).Target, Is.SameAs(B), "The human main assist decides");
        Assert.That(SmartGroupSnapshotBuilder.HoldTarget(A, 9_000, false, B, false, 2_000).Target, Is.SameAs(B), "Dead, mezzed or out of reach");
        Assert.That(SmartGroupSnapshotBuilder.HoldTarget<object>(A, 9_000, false, null, false, 2_000).Target, Is.Null);
    }

    [Test]
    public void Hold_SameTargetKeepsItsLock()
    {
        Assert.That(SmartGroupSnapshotBuilder.HoldTarget(A, 9_000, true, A, false, 2_000), Is.EqualTo((A, 9_000L)));
        Assert.That(SmartGroupSnapshotBuilder.HoldTarget<object>(A, 9_000, true, null, false, 2_000), Is.EqualTo((A, 9_000L)),
            "Nobody fighting it this tick: still the target while it is valid");
    }

    [Test]
    public void Freshness()
    {
        Assert.That(SmartGroupTacticalSnapshot.Empty.IsFresh(0), Is.False);
        var s = new SmartGroupTacticalSnapshot { Version = 1, Tick = 10_000 };
        Assert.That(s.IsFresh(10_500), Is.True);
        Assert.That(s.IsFresh(11_001), Is.False);
    }
}
