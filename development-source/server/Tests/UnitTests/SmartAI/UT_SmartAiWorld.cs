using System;
using System.Collections.Generic;
using System.IO;
using DOL.GS;
using DOL.GS.SmartAI;
using NUnit.Framework;
using Monster = DOL.GS.SmartAI.SmartWorldKnowledge.Monster;
using CampSite = DOL.GS.SmartAI.SmartWorldKnowledge.CampSite;
using Threat = DOL.GS.SmartAI.SmartWorldKnowledge.Threat;

/// <summary>Owner 2026-11-05: zone level config, an area search around camps, learning that survives restarts, and
/// groups that pick camps from all of it.</summary>
[TestFixture]
public class UT_SmartAiWorld
{
    private string _dir;

    [SetUp]
    public void SetUp()
    {
        _dir = Path.Combine(Path.GetTempPath(), "smartai-world-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_dir);
        SmartWorldConfig.UseFileForTests(Path.Combine(_dir, SmartWorldConfig.FileName));
        SmartWorldLearning.UseFileForTests(Path.Combine(_dir, SmartWorldLearning.FileName));
    }

    [TearDown]
    public void TearDown()
    {
        SmartWorldConfig.UseFileForTests(null);
        SmartWorldLearning.UseFileForTests(null);
        try { Directory.Delete(_dir, true); } catch (IOException) { }
    }

    private static Monster M(string name, int x, int y, int level, int aggro = 0, string zone = "Lough Derg", ushort region = 200) =>
        new(name, region, zone, false, x, y, 0, level, aggro, aggro > 0);

    [Test]
    public void ZoneLevelsArePercentilesOfTheLiveMonsters()
    {
        var monsters = new List<Monster>();
        for (int level = 1; level <= 10; level++)
            monsters.Add(M("rat", 0, 0, level));
        var zone = SmartWorldKnowledge.BuildZoneLevels(monsters)["lough derg"];
        Assert.That(zone.Min, Is.EqualTo(1));
        Assert.That(zone.Max, Is.EqualTo(10));
        Assert.That(zone.Low, Is.EqualTo(2));
        Assert.That(zone.Typical, Is.EqualTo(6));
        Assert.That(zone.High, Is.EqualTo(9));
        Assert.That(zone.Monsters, Is.EqualTo(10));
    }

    [Test]
    public void TheAreaSearchFindsOtherAggressiveMonstersOnly()
    {
        Monster[] monsters =
        {
            M("water beetle", 100, 0, 6, 400),        // the camp's own kind
            M("lunantishee", 600, 0, 9, 500),         // aggressive, in range
            M("blackthorn", 900, 0, 8),               // not aggressive
            M("luricaduane", 2400, 0, 12, 600),       // aggressive, in range
            M("far away", 9000, 0, 12, 600),          // outside the search
            M("other region", 600, 0, 12, 600, region: 201),
        };
        CampSite[] camps = { new("c1", "water beetle", 200, 0, 0, 0) };
        Dictionary<string, Threat[]> threats = SmartWorldKnowledge.BuildThreats(monsters, camps, 2500);
        Assert.That(threats["c1"], Has.Length.EqualTo(2));
        Assert.That(threats["c1"][0], Is.EqualTo(new Threat(9, 500, 600)));
        Assert.That(threats["c1"][1].Distance, Is.EqualTo(2400));
    }

    [Test]
    public void SafetyDropsForHardMonstersWhoseAggroReachesTheCamp()
    {
        // Group level 5: 6 orange, 7 red, 8 purple.
        Assert.That(SmartCampChooser.Safety(new Threat[0], 5, 350, out _), Is.EqualTo(1));
        Assert.That(SmartCampChooser.Safety(new[] { new Threat(8, 500, 600) }, 5, 350, out int n), Is.EqualTo(0.15).Within(1e-9));
        Assert.That(n, Is.EqualTo(1));
        Assert.That(SmartCampChooser.Safety(new[] { new Threat(6, 500, 600) }, 5, 350, out _), Is.EqualTo(0.6).Within(1e-9));
        // Out of aggro reach: a red or harder one only costs a little.
        Assert.That(SmartCampChooser.Safety(new[] { new Threat(8, 300, 2000) }, 5, 350, out _), Is.EqualTo(0.9).Within(1e-9));
        Assert.That(SmartCampChooser.Safety(new[] { new Threat(6, 300, 2000) }, 5, 350, out _), Is.EqualTo(1));
        // Grey monsters are ignored.
        Assert.That(SmartCampChooser.Safety(new[] { new Threat(1, 500, 100) }, 20, 350, out _), Is.EqualTo(1));
    }

    [Test]
    public void ZonesFitByTheOwnersRangeOrTheTypicalLevel()
    {
        var gen = new SmartWorldConfig.ZoneGenerated { Typical = 12 };
        Assert.That(SmartCampChooser.ZoneFits(null, gen, 5, 7, 2), Is.False, "typical 12 is far above a band topping at 7");
        Assert.That(SmartCampChooser.ZoneFits(null, gen, 8, 10, 2), Is.True);
        Assert.That(SmartCampChooser.ZoneFits(null, null, 5, 7, 2), Is.True, "no data: allowed");
        Assert.That(SmartCampChooser.ZoneFits(new() { Skip = true }, gen, 8, 10, 2), Is.False);
        Assert.That(SmartCampChooser.ZoneFits(new() { Min = 1, Max = 8 }, gen, 5, 7, 2), Is.True, "the owner's range wins");
        Assert.That(SmartCampChooser.ZoneFits(new() { Min = 15, Max = 20 }, gen, 5, 7, 2), Is.False);
    }

    [Test]
    public void WeightGrowsWithSpawnsAndLevelAndShrinksWithDanger()
    {
        Assert.That(SmartCampChooser.Weight(4, 5, 5, 1, 1), Is.EqualTo(4));
        Assert.That(SmartCampChooser.Weight(40, 5, 5, 1, 1), Is.EqualTo(12), "spawns capped at 12");
        Assert.That(SmartCampChooser.Weight(4, 7, 5, 1, 1), Is.EqualTo(6), "+25% per level");
        Assert.That(SmartCampChooser.Weight(4, 5, 5, 0.5, 0.5), Is.EqualTo(1));
    }

    [Test]
    public void LearnedFactorIsNeutralWithoutHistoryAndBounded()
    {
        Assert.That(SmartWorldLearning.CampFactor(0, 0, 0), Is.EqualTo(1).Within(1e-9));
        Assert.That(SmartWorldLearning.CampFactor(100, 2, 0), Is.EqualTo(1.5).Within(1e-9), "a safe camp: up to 1.5x");
        Assert.That(SmartWorldLearning.CampFactor(5, 20, 0), Is.EqualTo(0.2).Within(1e-9), "a deadly camp: down to 0.2x");
        Assert.That(SmartWorldLearning.CampFactor(0, 0, 4), Is.EqualTo(0.5).Within(1e-9), "deaths on the road");
        Assert.That(SmartWorldLearning.CampFactor(0, 0, 100), Is.EqualTo(0.4).Within(1e-9));
        Assert.That(SmartWorldLearning.AreaFactor(0), Is.EqualTo(1));
        Assert.That(SmartWorldLearning.AreaFactor(100), Is.EqualTo(0.5));
    }

    [Test]
    public void OldResultsFadeByTheHalfLife()
    {
        DateTime t0 = new(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.That(SmartWorldLearning.Fade(8, t0, t0.AddDays(7), 7), Is.EqualTo(4).Within(1e-9));
        Assert.That(SmartWorldLearning.Fade(8, t0, t0.AddDays(14), 7), Is.EqualTo(2).Within(1e-9));
        Assert.That(SmartWorldLearning.Fade(8, t0, t0, 7), Is.EqualTo(8));
    }

    [Test]
    public void LearningIsRecordedSavedAndLoadedAgain()
    {
        DateTime now = DateTime.UtcNow;
        for (int i = 0; i < 4; i++)
            SmartWorldLearning.RecordDeath("deadly", true, 200, 0, 0, now);
        SmartWorldLearning.RecordKill("deadly", now);
        SmartWorldLearning.RecordDeath(null, false, 200, 5000, 5000, now);
        double before = SmartWorldLearning.FactorFor("deadly", 200, 0, 0, out string why, now);
        Assert.That(before, Is.LessThan(0.5));
        Assert.That(why, Does.Contain("4 deaths"));
        Assert.That(SmartWorldLearning.FactorFor("x", 200, 5000, 5000, out _, now), Is.LessThan(1), "deaths nearby count");
        SmartWorldLearning.Save();
        Assert.That(File.Exists(SmartWorldLearning.FilePath));

        SmartWorldLearning.UseFileForTests(SmartWorldLearning.FilePath);
        Assert.That(SmartWorldLearning.FactorFor("deadly", 200, 0, 0, out _, now), Is.EqualTo(1).Within(1e-9), "cleared");
        SmartWorldLearning.Load();
        Assert.That(SmartWorldLearning.FactorFor("deadly", 200, 0, 0, out _, now), Is.EqualTo(before).Within(1e-6));
    }

    [Test]
    public void TheConfigKeepsTheOwnersSettingsAndOverrides()
    {
        string path = SmartWorldConfig.FilePath;
        File.WriteAllText(path, "{ \"settings\": { \"campSearchRadius\": 3000 }, \"overrides\": { \"Lough Derg\": { \"skip\": true } } }");
        SmartWorldConfig.UseFileForTests(path);
        SmartWorldConfig.WriteGenerated(new Dictionary<string, SmartWorldConfig.ZoneGenerated>
        {
            ["Lough Derg"] = new() { Typical = 7, Monsters = 3 },
        });
        string text = File.ReadAllText(path);
        Assert.That(text, Does.Contain("3000"));
        Assert.That(text, Does.Contain("\"skip\": true"));
        Assert.That(text, Does.Contain("\"typical\": 7"));
        SmartWorldConfig.UseFileForTests(path);
        Assert.That(SmartWorldConfig.Current.CampSearchRadius, Is.EqualTo(3000));
        Assert.That(SmartWorldConfig.OverrideFor("lough derg")?.Skip, Is.True);
    }

    [Test]
    public void ABrokenConfigIsLeftAloneAndTheDefaultsApply()
    {
        string path = SmartWorldConfig.FilePath;
        File.WriteAllText(path, "{ not json");
        SmartWorldConfig.UseFileForTests(path);
        SmartWorldConfig.WriteGenerated(new Dictionary<string, SmartWorldConfig.ZoneGenerated>());
        Assert.That(File.ReadAllText(path), Is.EqualTo("{ not json"));
        Assert.That(SmartWorldConfig.Current.CampSearchRadius, Is.EqualTo(2500));
    }

    [Test]
    public void ASafeSpotIsOutsideEveryAggroRangePlusTheMargin()
    {
        var threats = new[] { new AutonomousSmartSafeSpot.Threat(0, 0, 500) };
        Assert.That(AutonomousSmartSafeSpot.IsClear(new System.Numerics.Vector3(800, 0, 0), threats, 350), Is.False, "850 reach");
        Assert.That(AutonomousSmartSafeSpot.IsClear(new System.Numerics.Vector3(900, 0, 0), threats, 350), Is.True);
        Assert.That(AutonomousSmartSafeSpot.IsClear(new System.Numerics.Vector3(0, 0, 0), new AutonomousSmartSafeSpot.Threat[0], 350), Is.True);
    }

    [Test]
    public void SafeSpotCandidatesStartHereThenRingOutward()
    {
        var around = new System.Numerics.Vector3(1000, 1000, 50);
        var list = new List<System.Numerics.Vector3>(AutonomousSmartSafeSpot.Candidates(around));
        Assert.That(list, Has.Count.EqualTo(25));
        Assert.That(list[0], Is.EqualTo(around));
        Assert.That(System.Numerics.Vector3.Distance(list[1], around), Is.EqualTo(500).Within(0.5));
        Assert.That(System.Numerics.Vector3.Distance(list[24], around), Is.EqualTo(1500).Within(0.5));
    }

    [Test]
    public void TheChooserPrefersTheSafeCamp()
    {
        // Two camps alike but one sits inside a purple monster's aggro.
        SmartWorldKnowledge.Rebuild(
            new[] { M("lunantishee", 600, 0, 8, 500) },
            new[] { new CampSite("danger", "beetle", 200, 0, 0, 0), new CampSite("safe", "beetle", 200, 20000, 0, 0) },
            2500, writeFile: false);
        var band = new SmartPullBand(ConColor.BLUE, ConColor.YELLOW, 70, "test");
        var candidates = new[]
        {
            new SmartCampChooser.Candidate("danger", "Lough Derg", 200, 0, 0, 5, 6),
            new SmartCampChooser.Candidate("safe", "Lough Derg", 200, 20000, 0, 5, 6),
        };
        int safe = 0;
        var random = new Random(7);
        for (int i = 0; i < 200; i++)
            if (SmartCampChooser.Choose(candidates, 5, band, random).Camp.Id == "safe")
                safe++;
        Assert.That(safe, Is.GreaterThan(160), "about 87% of draws (1 / (1 + 0.15))");
        SmartWorldKnowledge.Rebuild(new Monster[0], new CampSite[0], 2500, writeFile: false);
    }
}
