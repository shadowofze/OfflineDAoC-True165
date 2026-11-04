using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 11: the decision engine's cost (architecture section 8: under 0.05 ms a think, no heap
/// allocation per think), anti-oscillation stickiness, and the per-bot decision ring.</summary>
[TestFixture, NonParallelizable]
public class UT_SmartAiPerformance
{
    private SmartSpecProfile[] _profiles;

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
        _profiles = SmartAiMarkdownParser.Parse(File.ReadAllText(path), "albion/cleric.md").Class.Specs
            .Select(SmartSpecProfile.Compile).ToArray();
    }

    private static SmartBotPerception See(int i) => new()
    {
        SelfManaPercent = i * 7 % 100,
        GroupMinHealthPercent = i * 13 % 100,
        MembersBelow65 = i % 3,
        MembersBelow40 = i % 5 == 0 ? 1 : 0,
        InCombat = true,
        HasTarget = i % 4 != 0,
        TargetHealthPercent = i * 11 % 100,
        TargetControlled = i % 9 == 0,
        Mode = (SmartGameMode)(i % 3),
        Personality = SmartPersonalityProfile.For((SmartPersonality)(i % 7)),
        StickyFamily = i % 2 == 0 ? (SmartActionFamily)(i % 4) : null,
    };

    private static int Plan(SmartSpecProfile profile, in SmartBotPerception p)
    {
        Span<SmartActionCandidate> buffer = stackalloc SmartActionCandidate[SmartCandidateGenerator.FamilyCount];
        int n = SmartCandidateGenerator.Generate(profile, p, buffer);
        return n == 0 ? -1 : (int)buffer[0].Family + (profile.OffenseMayHeal(buffer[..n]) ? 10 : 0);
    }

    [Test]
    public void Planning_IsWellUnderBudget()
    {
        const int Thinks = 200_000;
        var seen = new SmartBotPerception[64];
        for (int i = 0; i < seen.Length; i++)
            seen[i] = See(i);
        for (int i = 0; i < 2_000; i++)
            Plan(_profiles[i % _profiles.Length], seen[i % seen.Length]); // warm up

        long sink = 0;
        long start = Stopwatch.GetTimestamp();
        for (int i = 0; i < Thinks; i++)
            sink += Plan(_profiles[i % _profiles.Length], seen[i % seen.Length]);
        double perThinkMs = (Stopwatch.GetTimestamp() - start) * 1_000.0 / Stopwatch.Frequency / Thinks;
        TestContext.Out.WriteLine($"Planning: {perThinkMs * 1000:0.000} us per think (sink {sink})");
        Assert.That(perThinkMs, Is.LessThan(0.05), "Architecture section 8: under 0.05 ms a think");
    }

    [Test]
    public void Planning_AllocatesNothing()
    {
        var seen = new SmartBotPerception[64];
        for (int i = 0; i < seen.Length; i++)
            seen[i] = See(i);
        for (int i = 0; i < 1_000; i++)
            Plan(_profiles[i % _profiles.Length], seen[i % seen.Length]);

        long before = GC.GetAllocatedBytesForCurrentThread();
        long sink = 0;
        for (int i = 0; i < 50_000; i++)
            sink += Plan(_profiles[i % _profiles.Length], seen[i % seen.Length]);
        long allocated = GC.GetAllocatedBytesForCurrentThread() - before;
        TestContext.Out.WriteLine($"Allocated {allocated} bytes over 50,000 plans (sink {sink})");
        Assert.That(allocated, Is.Zero, "Perception, gates, candidates and scoring stay off the heap");
    }

    [Test]
    public void Stickiness_KeepsANearTie()
    {
        SmartSpecProfile enhancement = _profiles.Single(p => p.Spec.Name == "Enhancement Cleric");
        // Enhancement at full health: triage 90 and the bubble 90 tie; triage goes first by family order.
        var p = new SmartBotPerception { SelfManaPercent = 80, GroupMinHealthPercent = 100, InCombat = true, HasTarget = true, TargetHealthPercent = 50 };
        Span<SmartActionCandidate> buffer = stackalloc SmartActionCandidate[SmartCandidateGenerator.FamilyCount];
        SmartCandidateGenerator.Generate(enhancement, p, buffer);
        Assert.That(buffer[0].Family, Is.EqualTo(SmartActionFamily.Triage));
        // Having just buffed, it keeps buffing rather than flip-flopping.
        SmartCandidateGenerator.Generate(enhancement, p with { StickyFamily = SmartActionFamily.Support }, buffer);
        Assert.That(buffer[0].Family, Is.EqualTo(SmartActionFamily.Support));
    }

    [Test]
    public void Stickiness_NeverInAnEmergency()
    {
        SmartSpecProfile smite = _profiles.Single(p => p.Spec.Name == "Smite Cleric");
        var p = new SmartBotPerception
        {
            SelfManaPercent = 80, GroupMinHealthPercent = 20, MembersBelow65 = 1, MembersBelow40 = 1, InCombat = true,
            HasTarget = true, TargetHealthPercent = 50, StickyFamily = SmartActionFamily.OffenseSpells,
        };
        Span<SmartActionCandidate> buffer = stackalloc SmartActionCandidate[SmartCandidateGenerator.FamilyCount];
        SmartCandidateGenerator.Generate(smite, p, buffer);
        Assert.That(buffer[0].Family, Is.EqualTo(SmartActionFamily.Triage), "An ally under 25%: the heal, not the last nuke");
    }

    [Test]
    public void DecisionRing_KeepsTheLast32NewestFirst()
    {
        var ring = new SmartAiDiagnostics.DecisionRing();
        Assert.That(ring.Last, Is.Null);
        for (int i = 1; i <= 40; i++)
            ring.Add(new SmartDecision("s", $"a{i}", i, "", i));
        Assert.That(ring.Count, Is.EqualTo(SmartAiDiagnostics.RingCapacity));
        Assert.That(ring.Last.Action, Is.EqualTo("a40"));
        Assert.That(ring.Latest(3).Select(d => d.Action), Is.EqualTo(new[] { "a40", "a39", "a38" }));
        Assert.That(ring.Latest(100).Length, Is.EqualTo(32));
        Assert.That(ring.Latest(100).Last().Action, Is.EqualTo("a9"), "The oldest kept");
    }

    [Test]
    public void ThinkTimings()
    {
        SmartAiDiagnostics.ResetTimings();
        SmartAiDiagnostics.RecordThink(Stopwatch.Frequency / 10_000); // 100 us
        SmartAiDiagnostics.RecordThink(Stopwatch.Frequency / 100_000); // 10 us
        Assert.That(SmartAiDiagnostics.Thinks, Is.EqualTo(2));
        Assert.That(SmartAiDiagnostics.AverageMicroseconds, Is.EqualTo(55).Within(1));
        Assert.That(SmartAiDiagnostics.SlowestMicroseconds, Is.EqualTo(100).Within(1));
        SmartAiDiagnostics.ResetTimings();
    }
}
