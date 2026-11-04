using System.Collections.Generic;
using System.Linq;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 8: observations, the authority order of the arbitration, and the callout throttle.</summary>
[TestFixture]
public class UT_SmartAiGroupCoordination
{
    private static readonly object Healer = "healer", Tank = "tank", Dps = "dps", Orc = "orc", Shaman = "shaman", Add = "add";

    private static SmartBotObservation Obs(SmartObservationKind kind, object poster, object target, int urgency, int weight = 100, int id = 1) =>
        new(kind, poster, target, urgency, weight, id, 0);

    private static SmartArbitrationContext Ctx(object current = null, long lockUntil = 0, long now = 10_000,
        bool playerLed = false, bool humanTarget = false) => new(playerLed, humanTarget, current, lockUntil, now);

    [Test]
    public void Nothing_IsStandard()
    {
        SmartArbitration a = SmartGroupCoordinator.Arbitrate([], Ctx());
        Assert.That(a.Strategy, Is.EqualTo(SmartGroupStrategy.StandardAssistTrain));
        Assert.That(a.TargetSwitch, Is.Null);
    }

    [Test]
    public void Survival_BeatsTargetCalls()
    {
        SmartArbitration a = SmartGroupCoordinator.Arbitrate([
            Obs(SmartObservationKind.TargetVulnerable, Dps, Shaman, 95, 130, 3),
            Obs(SmartObservationKind.HealerUnderAttack, Healer, Orc, 80, 100, 2),
        ], Ctx(current: Add));
        Assert.That(a.Strategy, Is.EqualTo(SmartGroupStrategy.ProtectBackline));
        Assert.That(a.PeelTarget, Is.SameAs(Orc));
        Assert.That(a.TargetSwitch, Is.Null, "The main assist and DPS stay on their target while peelers peel");
    }

    [Test]
    public void HealerUnderAttack_BelowEightyIsNotAnEmergency()
    {
        SmartArbitration a = SmartGroupCoordinator.Arbitrate([Obs(SmartObservationKind.HealerUnderAttack, Healer, Orc, 70)], Ctx());
        Assert.That(a.Strategy, Is.EqualTo(SmartGroupStrategy.StandardAssistTrain));
    }

    [Test]
    public void HeavyDamage_MeansEmergencyTriage()
    {
        SmartArbitration a = SmartGroupCoordinator.Arbitrate([Obs(SmartObservationKind.HeavyGroupDamageWarning, Healer, null, 85)], Ctx());
        Assert.That(a.Strategy, Is.EqualTo(SmartGroupStrategy.EmergencyTriage));
    }

    [Test]
    public void Retreat_NeverAbandonsAPlayer()
    {
        var retreat = new List<SmartBotObservation> { Obs(SmartObservationKind.RecommendRetreat, Tank, null, 90) };
        Assert.That(SmartGroupCoordinator.Arbitrate(retreat, Ctx(playerLed: true)).Strategy, Is.EqualTo(SmartGroupStrategy.EmergencyTriage));
        Assert.That(SmartGroupCoordinator.Arbitrate(retreat, Ctx(playerLed: false)).Strategy, Is.EqualTo(SmartGroupStrategy.RetreatAdvised));
    }

    [Test]
    public void HumanMainAssist_IsNeverOverruledOnTargets()
    {
        SmartArbitration a = SmartGroupCoordinator.Arbitrate([Obs(SmartObservationKind.TargetVulnerable, Dps, Shaman, 95, 130)],
            Ctx(current: Add, humanTarget: true));
        Assert.That(a.TargetSwitch, Is.Null);
    }

    [Test]
    public void TargetSwitch_NeedsTheHoldOverAndAClearMargin()
    {
        var vulnerable = new List<SmartBotObservation> { Obs(SmartObservationKind.TargetVulnerable, Dps, Shaman, 90) };
        Assert.That(SmartGroupCoordinator.Arbitrate(vulnerable, Ctx(current: Add, lockUntil: 12_000, now: 10_000)).TargetSwitch, Is.Null,
            "Inside the 3.5 s hold");
        Assert.That(SmartGroupCoordinator.Arbitrate(vulnerable, Ctx(current: Add, lockUntil: 9_000, now: 10_000)).TargetSwitch, Is.SameAs(Shaman));

        var weak = new List<SmartBotObservation> { Obs(SmartObservationKind.TargetVulnerable, Dps, Shaman, 65) };
        Assert.That(SmartGroupCoordinator.Arbitrate(weak, Ctx(current: Add, lockUntil: 0)).TargetSwitch, Is.Null,
            $"Must beat {SmartGroupCoordinator.CurrentTargetScore} by {SmartGroupCoordinator.SwitchMargin}");
        Assert.That(SmartGroupCoordinator.Arbitrate(weak, Ctx(current: null)).TargetSwitch, Is.SameAs(Shaman), "No target yet: any call goes");
    }

    [Test]
    public void TargetSwitch_TheMainAssistWeighsMore()
    {
        SmartArbitration a = SmartGroupCoordinator.Arbitrate([
            Obs(SmartObservationKind.TargetVulnerable, Dps, Orc, 80, 100, 1),
            Obs(SmartObservationKind.RecommendTargetSwitch, Tank, Shaman, 70, 130, 2),
        ], Ctx(current: Add));
        Assert.That(a.TargetSwitch, Is.SameAs(Shaman), "70 x 130% = 91 beats 80");
    }

    [Test]
    public void Arbitration_IsDeterministic()
    {
        var obs = new List<SmartBotObservation>
        {
            Obs(SmartObservationKind.TargetVulnerable, Dps, Orc, 80, 100, 7),
            Obs(SmartObservationKind.TargetVulnerable, Tank, Shaman, 80, 100, 3),
        };
        object first = SmartGroupCoordinator.Arbitrate(obs, Ctx(current: Add)).TargetSwitch;
        obs.Reverse();
        Assert.That(SmartGroupCoordinator.Arbitrate(obs, Ctx(current: Add)).TargetSwitch, Is.SameAs(first));
        Assert.That(first, Is.SameAs(Shaman), "Equal scores: the lower poster id");
    }

    // Observations buffer --------------------------------------------------------------------------------------------

    [Test]
    public void Post_TwoPerBotPerTickAndThirtyTwoInAll()
    {
        var state = new SmartGroupTacticalState();
        Assert.That(state.Post(new SmartBotObservation(SmartObservationKind.HealerLowMana, Healer, null, 70, 100, 1, 5)), Is.True);
        Assert.That(state.Post(new SmartBotObservation(SmartObservationKind.HeavyGroupDamageWarning, Healer, null, 85, 100, 1, 5)), Is.True);
        Assert.That(state.Post(new SmartBotObservation(SmartObservationKind.HealerUnderAttack, Healer, Orc, 80, 100, 1, 5)), Is.False);
        Assert.That(state.Post(new SmartBotObservation(SmartObservationKind.HealerUnderAttack, Healer, Orc, 80, 100, 1, 6)), Is.True,
            "Next tick, room again");
        Assert.That(state.DrainObservations().Count, Is.EqualTo(3));
        Assert.That(state.DrainObservations(), Is.Empty);

        for (int i = 0; i < 40; i++)
            state.Post(new SmartBotObservation(SmartObservationKind.TargetVulnerable, Dps, Orc, 80, 100, i, 1));
        List<SmartBotObservation> kept = state.DrainObservations();
        Assert.That(kept.Count, Is.EqualTo(SmartGroupTacticalState.ObservationCapacity));
        Assert.That(kept.First().PosterId, Is.EqualTo(8), "The oldest go first");
    }

    // Callouts -------------------------------------------------------------------------------------------------------

    [Test]
    public void Callouts_OnePerEightSecondsAndNoRepeatsWithinThirty()
    {
        var policy = new SmartGroupCalloutPolicy();
        var heavy = Obs(SmartObservationKind.HeavyGroupDamageWarning, Healer, null, 85, id: 1);
        var lowMana = Obs(SmartObservationKind.HealerLowMana, Healer, null, 70, id: 1);
        Assert.That(policy.TryAllow(heavy, 0), Is.True);
        Assert.That(policy.TryAllow(lowMana, 5_000), Is.False, "8 s between callouts");
        Assert.That(policy.TryAllow(lowMana, 8_000), Is.True);
        Assert.That(policy.TryAllow(heavy, 16_000), Is.False, "Same bot, same call within 30 s");
        Assert.That(policy.TryAllow(heavy, 30_000), Is.True);
    }

    [Test]
    public void Callouts_OnlyUrgentAndOnlyKindsWithAText()
    {
        var policy = new SmartGroupCalloutPolicy();
        Assert.That(policy.TryAllow(Obs(SmartObservationKind.HealerLowMana, Healer, null, 60), 0), Is.False, "Below 70");
        Assert.That(policy.TryAllow(Obs(SmartObservationKind.CrowdControlOpportunity, Healer, null, 90), 0), Is.False, "No line for it");
        Assert.That(SmartGroupCalloutPolicy.Text(Obs(SmartObservationKind.HealerUnderAttack, Healer, Orc, 80), "an orc"),
            Is.EqualTo("an orc is on me, peel!"));
        Assert.That(SmartGroupCalloutPolicy.Text(Obs(SmartObservationKind.TargetVulnerable, Dps, Shaman, 80), "a shaman"),
            Is.EqualTo("Switching to a shaman!"));
    }

    // Who peels -------------------------------------------------------------------------------------------------------

    [TestCase("PrimaryHealer", true, false)]
    [TestCase("HybridSupport", false, true)]
    [TestCase("DefensiveTank", false, true)]
    [TestCase("CrowdControl", false, true)]
    [TestCase("CasterDps", false, false)]
    [TestCase("MeleeDPS", false, false)]
    public void Roles(string role, bool healer, bool peeler)
    {
        var profile = SmartSpecProfile.Compile(new SmartSpecDefinition { Name = "x", Role = role });
        Assert.That(SmartObservationProducer.IsHealerSpec(profile), Is.EqualTo(healer));
        Assert.That(SmartObservationProducer.IsPeelerSpec(profile), Is.EqualTo(peeler));
    }

    [Test]
    public void EmergencyTriage_RaisesHealing()
    {
        var profile = SmartSpecProfile.Compile(new SmartSpecDefinition { Name = "x", Role = "HybridSupport" });
        var calm = new SmartBotPerception { GroupMinHealthPercent = 90 };
        var emergency = calm with { GroupStrategy = SmartGroupStrategy.EmergencyTriage };
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.Triage, 50, profile, emergency) -
                    SmartUtilityScorer.Score(SmartActionFamily.Triage, 50, profile, calm), Is.EqualTo(SmartUtilityScorer.GroupTriageBonus));
    }
}
