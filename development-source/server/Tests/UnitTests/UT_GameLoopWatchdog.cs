using DOL.GS;
using NUnit.Framework;

/// <summary>Run 7 (2026-11-05): two game-loop workers hung and the warning named only the threads.</summary>
[TestFixture]
public class UT_GameLoopWatchdog
{
    private sealed class Plain { }

    private sealed class Throwing
    {
        public DOL.GS.GameObject Owner => throw new System.InvalidOperationException("racing");
    }

    [Test]
    public void AnItemIsDescribedWithoutEverThrowing()
    {
        Assert.That(GameLoopWorkDescriber.Describe(null), Is.EqualTo("nothing"));
        Assert.That(GameLoopWorkDescriber.Describe(new Plain()), Is.EqualTo("Plain"));
        Assert.That(GameLoopWorkDescriber.Describe(new Throwing()), Does.StartWith("Throwing (could not describe"));
    }

    [Test]
    public void AStuckWorkerIsReportedAgainEveryThirtySecondsUpToTenTimes()
    {
        const long since = 1_000_000;
        Assert.That(GameLoopThreadPoolWatchdog.RepeatStuckWarning(since, since, 0, since + 29_999), Is.False);
        Assert.That(GameLoopThreadPoolWatchdog.RepeatStuckWarning(since, since, 0, since + 30_000), Is.True);
        Assert.That(GameLoopThreadPoolWatchdog.RepeatStuckWarning(since, since, GameLoopThreadPoolWatchdog.StuckRepeats, since + 60_000), Is.False);
    }
}
