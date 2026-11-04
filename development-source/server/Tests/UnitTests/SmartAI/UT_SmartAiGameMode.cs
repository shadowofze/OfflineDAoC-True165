using DOL.GS;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phase 9: PvE, PvP and RvR, the enemy class order, and how the group's target follows it.</summary>
[TestFixture]
public class UT_SmartAiGameMode
{
    [TestCase(false, 0, SmartGameMode.PvE)]
    [TestCase(true, 0, SmartGameMode.PvE)]
    [TestCase(false, 1, SmartGameMode.PvP)]
    [TestCase(false, 5, SmartGameMode.PvP)]
    [TestCase(true, 2, SmartGameMode.PvP)]
    [TestCase(true, 3, SmartGameMode.RvR)]
    public void Classify(bool frontier, int playerEnemies, SmartGameMode expected) =>
        Assert.That(SmartGameModeDetector.Classify(frontier, playerEnemies), Is.EqualTo(expected));

    [TestCase(eCharacterClass.Cleric, SmartEnemyPriority.Healer)]
    [TestCase(eCharacterClass.Healer, SmartEnemyPriority.Healer)]
    [TestCase(eCharacterClass.Druid, SmartEnemyPriority.Healer)]
    [TestCase(eCharacterClass.Sorcerer, SmartEnemyPriority.CrowdControl)]
    [TestCase(eCharacterClass.Mentalist, SmartEnemyPriority.CrowdControl)]
    [TestCase(eCharacterClass.Wizard, SmartEnemyPriority.Caster)]
    [TestCase(eCharacterClass.Runemaster, SmartEnemyPriority.Caster)]
    [TestCase(eCharacterClass.Berserker, SmartEnemyPriority.Melee)]
    [TestCase(eCharacterClass.Infiltrator, SmartEnemyPriority.Melee)]
    [TestCase(eCharacterClass.Armsman, SmartEnemyPriority.Tank)]
    [TestCase(eCharacterClass.Hero, SmartEnemyPriority.Tank)]
    public void EnemyOrder(eCharacterClass c, int priority) => Assert.That(SmartEnemyPriority.ForClass((int)c), Is.EqualTo(priority));

    [Test]
    public void EnemyOrder_HealersAndCrowdControlFirstTanksLast() =>
        Assert.That(SmartEnemyPriority.Healer, Is.EqualTo(SmartEnemyPriority.CrowdControl)
            .And.GreaterThan(SmartEnemyPriority.Caster));

    [Test]
    public void Monsters_HaveNoClassPriority() => Assert.That(SmartEnemyPriority.Of(null), Is.EqualTo(SmartEnemyPriority.Monster));

    private static readonly object Warrior = "warrior", Healer = "healer", Mob = "mob";

    [Test]
    public void Candidate_HumanMainAssistKeepsTheirTarget() =>
        Assert.That(SmartGroupSnapshotBuilder.ChooseCandidate(true, Warrior, SmartEnemyPriority.Tank, Healer, SmartEnemyPriority.Healer, Mob),
            Is.SameAs(Warrior));

    [Test]
    public void Candidate_BotMainAssistSwitchesToTheHealer() =>
        Assert.That(SmartGroupSnapshotBuilder.ChooseCandidate(false, Warrior, SmartEnemyPriority.Tank, Healer, SmartEnemyPriority.Healer, Mob),
            Is.SameAs(Healer));

    [Test]
    public void Candidate_EqualPriorityKeepsTheMainAssistTarget() =>
        Assert.That(SmartGroupSnapshotBuilder.ChooseCandidate(false, Warrior, SmartEnemyPriority.Melee, Healer, SmartEnemyPriority.Melee, Mob),
            Is.SameAs(Warrior));

    [Test]
    public void Candidate_PvEFallsBackToTheMainAssistOrTheMostFought()
    {
        Assert.That(SmartGroupSnapshotBuilder.ChooseCandidate<object>(false, Mob, 0, null, 0, null), Is.SameAs(Mob));
        Assert.That(SmartGroupSnapshotBuilder.ChooseCandidate<object>(false, null, 0, null, 0, Mob), Is.SameAs(Mob));
        Assert.That(SmartGroupSnapshotBuilder.ChooseCandidate<object>(true, null, 0, null, 0, Mob), Is.SameAs(Mob),
            "A human not fighting yet: the group's fight");
    }

    [Test]
    public void Scorer_PlayerFightsHealSooner()
    {
        var profile = SmartSpecProfile.Compile(new SmartSpecDefinition { Name = "x", Role = "PrimaryHealer" });
        var pve = new SmartBotPerception { GroupMinHealthPercent = 60, MembersBelow65 = 1, Mode = SmartGameMode.PvE };
        int pveScore = SmartUtilityScorer.Score(SmartActionFamily.Triage, 50, profile, pve);
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.Triage, 50, profile, pve with { Mode = SmartGameMode.RvR }) - pveScore,
            Is.EqualTo(SmartUtilityScorer.PlayerBurstTriageBonus));
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.Triage, 50, profile, pve with { Mode = SmartGameMode.PvP }) - pveScore,
            Is.EqualTo(SmartUtilityScorer.PlayerBurstTriageBonus));
        Assert.That(SmartUtilityScorer.Score(SmartActionFamily.Triage, 50, profile,
                pve with { Mode = SmartGameMode.RvR, MembersBelow65 = 0, GroupMinHealthPercent = 90 }),
            Is.EqualTo(50), "Nobody wounded: no change");
    }
}
