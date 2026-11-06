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
    public void FightRecordingStartsWithTheServerOnlyWhenTheFileSaysSo()
    {
        SmartGroupCombatLog.Stop();
        try
        {
            SmartGroupCombatLog.OnServerStarted(null, null, EventArgs.Empty);
            Assert.That(SmartGroupCombatLog.IsOn, Is.False, "off by default");

            File.WriteAllText(SmartWorldConfig.FilePath, "{ \"settings\": { \"recordFightsFromStart\": true } }");
            SmartWorldConfig.UseFileForTests(SmartWorldConfig.FilePath);
            SmartGroupCombatLog.OnServerStarted(null, null, EventArgs.Empty);
            Assert.That(SmartGroupCombatLog.CurrentScope, Is.EqualTo(SmartGroupCombatLog.Scope.AllGroups));
        }
        finally
        {
            SmartGroupCombatLog.Stop();
        }
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
    public void CampsWithTwoOtherPartiesNearbyAreLeftOut()
    {
        var a = new SmartCampChooser.Candidate("a", "Mag Mell", 200, 0, 0, 5, 6);
        var b = new SmartCampChooser.Candidate("b", "Mag Mell", 200, 9000, 0, 5, 6);
        var c = new SmartCampChooser.Candidate("c", "Mag Mell", 200, 20000, 0, 5, 6);
        var others = new Dictionary<string, int> { ["a"] = 3, ["b"] = 1, ["c"] = 0 };
        var quiet = SmartCampChooser.Uncrowded(new[] { a, b, c }, x => others[x.Id], 2, out int dropped);
        Assert.That(quiet, Is.EquivalentTo(new[] { b, c }));
        Assert.That(dropped, Is.EqualTo(1));
        // All crowded: the least crowded stay.
        others = new Dictionary<string, int> { ["a"] = 4, ["b"] = 2, ["c"] = 3 };
        Assert.That(SmartCampChooser.Uncrowded(new[] { a, b, c }, x => others[x.Id], 2, out _), Is.EquivalentTo(new[] { b }));
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
    [Test]
    public void AStalledCampLosesMostOfItsWeightAndTheStallsArePersisted()
    {
        // Owner 2026-11-05: Hibernian parties froze at Lough Derg water beetle camps.
        Assert.Multiple(() =>
        {
            Assert.That(SmartWorldLearning.CampFactor(0, 0, 0, 0), Is.EqualTo(1.0).Within(1e-9));
            Assert.That(SmartWorldLearning.CampFactor(0, 0, 0, 1), Is.EqualTo(0.5).Within(1e-9));
            Assert.That(SmartWorldLearning.CampFactor(0, 0, 0, 10), Is.EqualTo(0.25).Within(1e-9), "floor");
        });
        DateTime now = new(2026, 11, 5, 12, 0, 0, DateTimeKind.Utc);
        SmartWorldLearning.RecordStall("capnbry:200:6:4:water beetle", now);
        double factor = SmartWorldLearning.FactorFor("capnbry:200:6:4:water beetle", 200, 0, 0, out string why, now);
        Assert.That(factor, Is.EqualTo(0.5).Within(1e-6));
        Assert.That(why, Does.Contain("1 stalls"));
        SmartWorldLearning.Save();
        SmartWorldLearning.Load();
        Assert.That(SmartWorldLearning.FactorFor("capnbry:200:6:4:water beetle", 200, 0, 0, out _, now), Is.EqualTo(0.5).Within(1e-6));
    }

    [Test]
    public void ACampStallNeedsTheWholeWaitAndCanBeTurnedOff()
    {
        Assert.Multiple(() =>
        {
            Assert.That(AutonomousWorldBotController.IsCampStall(1_000, 120_999, 120), Is.False);
            Assert.That(AutonomousWorldBotController.IsCampStall(1_000, 121_000, 120), Is.True);
            Assert.That(AutonomousWorldBotController.IsCampStall(0, 500_000, 120), Is.False, "not ready yet");
            Assert.That(AutonomousWorldBotController.IsCampStall(1_000, 500_000, 0), Is.False, "0 turns it off");
        });
    }

    [Test]
    public void AReleasedUnreachablePullStaysReleased()
    {
        // The release used to last one call: the 30 s retry timer held the party again (58 tries in 30 minutes).
        object beetle = new(), other = new();
        Assert.Multiple(() =>
        {
            Assert.That(AutonomousDefensivePull.IsReleased(beetle, 180_000, beetle, 1_000), Is.True);
            Assert.That(AutonomousDefensivePull.IsReleased(beetle, 180_000, beetle, 180_000), Is.False, "expired");
            Assert.That(AutonomousDefensivePull.IsReleased(beetle, 180_000, other, 1_000), Is.False, "another target");
            Assert.That(AutonomousDefensivePull.IsReleased(null, 180_000, beetle, 1_000), Is.False);
        });
    }
    // ---- route planning (owner 2026-11-05: "Setup Routes")

    private static readonly SmartRoutePlanner.Bounds Zone = new(0, 0, 65536, 65536);

    [Test]
    public void ASafeStraightWayIsNotPlanned()
    {
        Assert.That(SmartRoutePlanner.Find(1000, 30000, 30000, 30000, Zone, (_, _) => 0), Is.Null);
    }

    [Test]
    public void AWallOfDangerAcrossTheWayIsWalkedAround()
    {
        // Danger in column x = 14, rows 25..32 (cells): the straight way along y = 30000 (row 29) crosses it.
        double Danger(int cx, int cy) => cx == 14 && cy >= 25 && cy <= 32 ? 3 : 0;
        SmartRoutePlanner.Plan plan = SmartRoutePlanner.Find(1000, 30000, 30000, 30000, Zone, Danger);
        Assert.That(plan, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(plan.StraightDanger, Is.EqualTo(3).Within(1e-9));
            Assert.That(plan.PlannedDanger, Is.EqualTo(0).Within(1e-9), "the way around crosses no danger");
            Assert.That(plan.Waypoints, Is.Not.Empty);
            Assert.That(plan.Waypoints.Count, Is.LessThanOrEqualTo(4), "pulled taut to a few waypoints");
            Assert.That(plan.PlannedLength, Is.LessThan(plan.StraightLength * SmartRoutePlanner.MaxLengthRatio));
        });
    }

    [Test]
    public void AHugeDetourIsNotTaken()
    {
        // Danger across the whole zone height except the top row: the only way around is far longer.
        double Danger(int cx, int cy) => cx == 14 && cy >= 1 ? 3 : 0;
        Assert.That(SmartRoutePlanner.Find(1000, 30000, 30000, 30000, Zone, Danger), Is.Null);
    }

    [Test]
    public void ADeclinedPlanSaysWhy()
    {
        // Run 6: bots died 12-39 times on one square with almost no planned route, and nothing said why.
        Assert.Multiple(() =>
        {
            Assert.That(SmartRoutePlanner.Find(1000, 30000, 30000, 30000, Zone, (_, _) => 0, out string safe, out double none), Is.Null);
            Assert.That(safe, Is.EqualTo("safe"));
            Assert.That(none, Is.EqualTo(0));
            Assert.That(SmartRoutePlanner.Find(1000, 30000, 30000, 30000, Zone, (cx, cy) => cx == 14 && cy >= 1 ? 3 : 0,
                out string through, out double danger), Is.Null);
            Assert.That(through, Is.EqualTo("straight"), "a mild wall: going through it is cheaper than the long way round");
            Assert.That(danger, Is.EqualTo(3).Within(1e-9));
            Assert.That(SmartRoutePlanner.Find(1000, 30000, 30000, 30000, Zone, (cx, cy) => cx == 14 && cy >= 1 ? 100 : 0,
                out string longer, out _), Is.Null);
            Assert.That(longer, Is.EqualTo("longer"), "a deadly wall: the only way around is more than 2.2 times as long");
            Assert.That(SmartRoutePlanner.Find(1000, 30000, 30000, 30000, Zone, (_, _) => 1, out string everywhere, out _), Is.Null);
            Assert.That(everywhere, Is.EqualTo("straight"), "danger everywhere: the straight way is already the best");
            Assert.That(SmartRoutePlanner.Find(1000, 30000, 90000, 30000, Zone, (_, _) => 1, out string outside, out _), Is.Null);
            Assert.That(outside, Is.EqualTo("outside"), "the destination is outside the zone grid");
            Assert.That(SmartRoutePlanner.Find(1000, 30000, 30000, 30000, Zone, (cx, cy) => cx == 14 && cy >= 25 && cy <= 32 ? 3 : 0,
                out string planned, out _), Is.Not.Null);
            Assert.That(planned, Is.Null, "a plan has no decline reason");
        });
    }

    // ---- solo death loops (run 6)

    [Test]
    public void ThreeDeathsInOneAreaWithinHalfAnHourMakeALoopArea()
    {
        var deaths = new List<SmartDeathLoop.Death>();
        long t = 10_000_000;
        Assert.Multiple(() =>
        {
            Assert.That(SmartDeathLoop.RecordDeath(deaths, new(200, 345_750, 522_100, t)), Is.Null);
            Assert.That(SmartDeathLoop.RecordDeath(deaths, new(200, 330_000, 522_100, t + 60_000)), Is.Null, "elsewhere");
            Assert.That(SmartDeathLoop.RecordDeath(deaths, new(200, 345_700, 522_400, t + 180_000)), Is.Null, "two here so far");
            SmartDeathLoop.Area? area = SmartDeathLoop.RecordDeath(deaths, new(200, 346_000, 521_800, t + 360_000));
            Assert.That(area, Is.Not.Null);
            Assert.That(area.Value.Region, Is.EqualTo(200));
            Assert.That(area.Value.X, Is.EqualTo((345_750 + 345_700 + 346_000) / 3), "the centre of the three deaths");
            Assert.That(area.Value.Until, Is.EqualTo(t + 360_000 + SmartDeathLoop.AvoidMilliseconds));
        });
    }

    [Test]
    public void OldDeathsAndOtherRegionsDoNotMakeALoop()
    {
        var deaths = new List<SmartDeathLoop.Death>();
        long t = 10_000_000;
        SmartDeathLoop.RecordDeath(deaths, new(200, 345_750, 522_100, t));
        SmartDeathLoop.RecordDeath(deaths, new(201, 345_750, 522_100, t + 1_000));
        SmartDeathLoop.RecordDeath(deaths, new(200, 345_750, 522_100, t + SmartDeathLoop.LoopWindowMilliseconds + 5_000));
        Assert.That(SmartDeathLoop.RecordDeath(deaths, new(200, 345_750, 522_100, t + SmartDeathLoop.LoopWindowMilliseconds + 10_000)),
            Is.Null, "the first death has faded out of the window and one was in another region");
    }

    [Test]
    public void AWayIsBlockedOnlyWhenItPassesALoopArea()
    {
        long now = 5_000_000;
        var areas = new List<SmartDeathLoop.Area>();
        SmartDeathLoop.Remember(areas, new(200, 345_750, 522_100, now + 60_000), now);
        Assert.Multiple(() =>
        {
            // Saoaora: bind 345,050 / 528,730, walking north through the pack.
            Assert.That(SmartDeathLoop.Crossed(areas, 200, 345_050, 528_730, 345_189, 491_669, now), Is.Not.Null);
            Assert.That(SmartDeathLoop.Crossed(areas, 200, 345_050, 528_730, 352_000, 554_000, now), Is.Null, "south, away from it");
            Assert.That(SmartDeathLoop.Crossed(areas, 201, 345_050, 528_730, 345_189, 491_669, now), Is.Null, "another region");
            Assert.That(SmartDeathLoop.Crossed(areas, 200, 345_050, 528_730, 345_189, 491_669, now + 60_000), Is.Null, "expired");
            Assert.That(SmartDeathLoop.SegmentDistance(0, 0, 1000, 0, 500, 300), Is.EqualTo(300).Within(1e-9));
            Assert.That(SmartDeathLoop.SegmentDistance(0, 0, 1000, 0, 1300, 400), Is.EqualTo(500).Within(1e-9), "past the end");
        });
        SmartDeathLoop.Remember(areas, new(200, 345_800, 522_000, now + 120_000), now);
        Assert.That(areas, Has.Count.EqualTo(1), "a newer area on the same spot replaces the older one");
    }

    [Test]
    public void HardSpawnsAndLearnedAreasAreIndexed()
    {
        SmartWorldKnowledge.Rebuild(new[]
        {
            new Monster("lunantishee", 200, "Lough Derg", false, 341_100, 520_020, 5300, 9, 500, true),
            new Monster("blackthorn", 200, "Lough Derg", false, 341_200, 520_000, 5300, 8, 400, true),
            new Monster("water beetle", 200, "Lough Derg", false, 341_300, 520_050, 5300, 9, 0, false),
        }, new CampSite[0], 2500, writeFile: false);
        int cx = SmartRoutePlanner.CellOf(341_100), cy = SmartRoutePlanner.CellOf(520_020);
        Assert.Multiple(() =>
        {
            Assert.That(SmartWorldKnowledge.HardAggressiveIn(200, cx, cy, AutonomousWorldBotController.SmartRouteHardLevel(4)), Is.EqualTo(2),
                "both aggressive ones are hard for a level 4 (6+); the passive beetle is not counted");
            Assert.That(SmartWorldKnowledge.HardAggressiveIn(200, cx, cy, AutonomousWorldBotController.SmartRouteHardLevel(8)), Is.EqualTo(0), "10+ for a level 8");
        });
        SmartWorldKnowledge.Rebuild(new Monster[0], new CampSite[0], 2500, writeFile: false);

        DateTime now = new(2026, 11, 5, 12, 0, 0, DateTimeKind.Utc);
        SmartWorldLearning.RecordDeath(null, false, 1, 556_000, 575_000, now, victimLevel: 7);
        SmartWorldLearning.RecordDeath(null, false, 1, 556_100, 575_100, now, victimLevel: 7);
        Assert.Multiple(() =>
        {
            Assert.That(SmartWorldLearning.AreaDanger(1, 556_050, 575_050, 7, now), Is.EqualTo(2).Within(1e-9));
            Assert.That(SmartWorldLearning.AreaDanger(1, 556_050, 575_050, 30, now), Is.EqualTo(0.4).Within(1e-9),
                "a level 30 counts level 7 deaths at a fifth");
            Assert.That(SmartWorldLearning.DeathsFor(2, 0, 30), Is.EqualTo(2), "no levels kept: counted fully");
        });
    }

    [Test]
    public void ACastInTheInterruptWindowIsRefusedBeforeItIsQueued()
    {
        // The player rule from SpellHandler: instant, uninterruptible, QuickCast or Mastery of Concentration may cast.
        Assert.Multiple(() =>
        {
            Assert.That(GameBot.InterruptBlocksCast(false, false, 1200, false, false), Is.True);
            Assert.That(GameBot.InterruptBlocksCast(true, false, 1200, false, false), Is.False, "instant");
            Assert.That(GameBot.InterruptBlocksCast(false, true, 1200, false, false), Is.False, "uninterruptible");
            Assert.That(GameBot.InterruptBlocksCast(false, false, 1200, true, false), Is.False, "QuickCast");
            Assert.That(GameBot.InterruptBlocksCast(false, false, 1200, false, true), Is.False, "Mastery of Concentration");
            Assert.That(GameBot.InterruptBlocksCast(false, false, 0, false, false), Is.False, "not interrupted");
        });
    }
    [Test]
    public void ASoloBotStartsAgainFromItsBindInsteadOfWalkingBackToItsKillers()
    {
        // Run 5 (2026-11-05): a level 5 Mentalist walked back to its last death spot 31 times and died there each time.
        Assert.Multiple(() =>
        {
            Assert.That(GameBot.SoloSmartRestartsFromBind(true, false, false, true), Is.True);
            Assert.That(GameBot.SoloSmartRestartsFromBind(true, true, false, true), Is.False, "a group member returns to its party");
            Assert.That(GameBot.SoloSmartRestartsFromBind(true, false, true, true), Is.False, "a player's companion");
            Assert.That(GameBot.SoloSmartRestartsFromBind(true, false, false, false), Is.False, "SmartAI off: unchanged");
            Assert.That(GameBot.SoloSmartRestartsFromBind(false, false, false, true), Is.False, "not a world bot");
        });
    }
    [Test]
    public void ASpecThatDiesTooOftenAloneFightsALowerConAlone()
    {
        // Run 5 (2026-11-05): Regrowth Bards died in 21% of their solo fights, Nurture Druids in 9%.
        Assert.Multiple(() =>
        {
            Assert.That(SmartWorldLearning.SoloConSteps(10, 5), Is.EqualTo(0), "too little evidence yet");
            Assert.That(SmartWorldLearning.SoloConSteps(90, 8), Is.EqualTo(0), "9 deaths per 100 kills: as before");
            Assert.That(SmartWorldLearning.SoloConSteps(80, 20), Is.EqualTo(1), "25 per 100: one con lower");
            Assert.That(SmartWorldLearning.SoloConSteps(30, 15), Is.EqualTo(2), "50 per 100: two lower");
        });
        DateTime now = new(2026, 11, 5, 12, 0, 0, DateTimeKind.Utc);
        for (int i = 0; i < 16; i++)
            SmartWorldLearning.RecordSolo("Bard / Regrowth Bard", died: false, now);
        for (int i = 0; i < 6; i++)
            SmartWorldLearning.RecordSolo("Bard / Regrowth Bard", died: true, now);
        Assert.That(SmartWorldLearning.SoloConStepsFor("Bard / Regrowth Bard", out double kills, out double deaths, now), Is.EqualTo(1));
        Assert.That((kills, deaths), Is.EqualTo((16.0, 6.0)));
        SmartWorldLearning.Save();
        SmartWorldLearning.Load();
        Assert.That(SmartWorldLearning.SoloConStepsFor("Bard / Regrowth Bard", out _, out _, now), Is.EqualTo(1), "kept on disk");
    }

    [Test]
    public void AWeakSoloBotRestsBeforeItTravels()
    {
        Assert.Multiple(() =>
        {
            Assert.That(AutonomousWorldBotController.SoloTooWeakToTravel(33, 33, 33, true, false), Is.True, "just released");
            Assert.That(AutonomousWorldBotController.SoloTooWeakToTravel(70, 30, 100, true, false), Is.True, "a caster low on power");
            Assert.That(AutonomousWorldBotController.SoloTooWeakToTravel(70, 30, 100, false, false), Is.False, "power doesn't matter to a fighter");
            Assert.That(AutonomousWorldBotController.SoloTooWeakToTravel(70, 70, 100, true, false), Is.False, "fit enough to set off");
            Assert.That(AutonomousWorldBotController.SoloTooWeakToTravel(85, 85, 100, true, true), Is.True, "once sitting, rests to 90/80/80");
            Assert.That(AutonomousWorldBotController.SoloTooWeakToTravel(92, 85, 90, true, true), Is.False);
        });
    }
}