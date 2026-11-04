using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 3: the global gate, the per-bot circuit breaker and the seam's legacy fallbacks.</summary>
[TestFixture, NonParallelizable]
public class UT_SmartAiFallbackSeam
{
    [TearDown] public void Cleanup()
    {
        SmartAiFallback.Reset();
        SmartAiProperties.USE_SMART_AI = false;
    }

    [Test]
    public void Config_FollowsSwitchAndSessionFallback()
    {
        SmartAiFallback.Reset();
        SmartAiProperties.USE_SMART_AI = false;
        Assert.That(SmartAiConfig.IsEnabled, Is.False, "Off by default: legacy AI");

        SmartAiProperties.USE_SMART_AI = true;
        Assert.That(SmartAiConfig.IsEnabled, Is.True);

        long now = 1_000;
        for (int i = 0; i < SmartAiFallback.FailureLimit; i++)
            SmartAiFallback.ReportFailure("test", null, now + i);
        Assert.That(SmartAiConfig.IsEnabled, Is.False, "A tripped session fallback turns SmartAI off everywhere");
    }

    [Test]
    public void Driver_Off_ReturnsFalseWithoutTouchingTheBrain()
    {
        SmartAiProperties.USE_SMART_AI = false;
        // A null brain would throw if the driver looked at it: off must return before that.
        Assert.That(SmartAiBrainDriver.TryThinkCombat(null), Is.False);
    }

    [Test]
    public void Driver_On_NoBotBody_FallsBackToLegacy()
    {
        SmartAiFallback.Reset();
        SmartAiProperties.USE_SMART_AI = true;
        Assert.That(SmartAiBrainDriver.TryThinkCombat(null), Is.False);
        Assert.That(SmartAiFallback.IsTripped, Is.False, "Not an error");
    }

    [Test]
    public void Coordinator_Off_DoesNothing()
    {
        SmartAiProperties.USE_SMART_AI = false;
        long before = SmartAiCoordinator.LastPreparedTick;
        SmartAiCoordinator.PrepareTick();
        Assert.That(SmartAiCoordinator.LastPreparedTick, Is.EqualTo(before));
    }

    [Test]
    public void Breaker_StartsClosed()
    {
        var breaker = new SmartAiCircuitBreaker();
        Assert.That(breaker.IsOpen(0), Is.False);
        Assert.That(breaker.IsOpen(long.MaxValue / 2), Is.False);
        Assert.That(breaker.Trips, Is.Zero);
    }

    [Test]
    public void Breaker_OpensForTenSecondsThenCloses()
    {
        var breaker = new SmartAiCircuitBreaker();
        long t = 50_000;
        breaker.Trip(t);
        Assert.That(breaker.Trips, Is.EqualTo(1));
        Assert.That(breaker.IsOpen(t), Is.True);
        Assert.That(breaker.IsOpen(t + SmartAiCircuitBreaker.OpenMs - 1), Is.True);
        Assert.That(breaker.IsOpen(t + SmartAiCircuitBreaker.OpenMs), Is.False, "SmartAI tries the bot again");
    }

    [Test]
    public void Breaker_RetripExtendsFromTheNewFailure()
    {
        var breaker = new SmartAiCircuitBreaker();
        breaker.Trip(0);
        breaker.Trip(SmartAiCircuitBreaker.OpenMs);
        Assert.That(breaker.Trips, Is.EqualTo(2));
        Assert.That(breaker.IsOpen(SmartAiCircuitBreaker.OpenMs * 2 - 1), Is.True);
        Assert.That(breaker.IsOpen(SmartAiCircuitBreaker.OpenMs * 2), Is.False);
    }
}
