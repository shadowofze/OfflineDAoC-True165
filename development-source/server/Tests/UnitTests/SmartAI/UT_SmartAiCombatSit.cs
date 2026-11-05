using System.IO;
using System.Linq;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>Owner 2026-11-05: Mentalist, Druid, Healer, Shaman, Cleric, Friar, Warden and Bard sit between heals in PvE
/// fights (PvE Standard and PvE Pet Pulling) to keep power up while every party member is above 90% health.</summary>
[TestFixture]
public class UT_SmartAiCombatSit
{
    private static bool Sit(int above = 90, SmartGameMode mode = SmartGameMode.PvE, bool group = true, int mana = 60,
        int lowest = 95, bool busy = false, bool enemyOnMe = false, long sinceHit = long.MaxValue, bool leaderSettled = true,
        long sinceStood = long.MaxValue) =>
        SmartCombatSit.ShouldSit(above, mode, group, mana, lowest, busy, enemyOnMe, sinceHit, leaderSettled, sinceStood);

    [Test]
    public void SitsWhileEveryoneIsAboveTheThreshold()
    {
        Assert.That(Sit(lowest: 91), Is.True);
        Assert.That(Sit(lowest: 90), Is.False, "At 90% or below: stay up to heal");
        Assert.That(Sit(lowest: 40), Is.False);
    }

    [Test]
    public void OnlyWhenPowerIsNotFull() => Assert.That(Sit(mana: 100), Is.False);

    [Test]
    public void PvEOnly()
    {
        Assert.That(Sit(mode: SmartGameMode.PvP), Is.False);
        Assert.That(Sit(mode: SmartGameMode.RvR), Is.False);
    }

    [Test]
    public void NotWithAMonsterOnIt()
    {
        Assert.That(Sit(enemyOnMe: true), Is.False);
        Assert.That(Sit(sinceHit: SmartCombatSit.HitQuietMs - 1), Is.False, "Just hit");
        Assert.That(Sit(sinceHit: SmartCombatSit.HitQuietMs), Is.True);
    }

    [Test]
    public void NotWhileBusyOrFollowing()
    {
        Assert.That(Sit(busy: true), Is.False, "Casting, attacking, moving or crowd controlled");
        Assert.That(Sit(leaderSettled: false), Is.False, "The leader is moving or far");
        Assert.That(Sit(group: false), Is.False, "Party members only");
    }

    [Test]
    public void WaitsAMomentAfterStanding()
    {
        Assert.That(Sit(sinceStood: SmartCombatSit.ResitDelayMs - 1), Is.False);
        Assert.That(Sit(sinceStood: SmartCombatSit.ResitDelayMs), Is.True);
    }

    [Test]
    public void ASpecWithoutTheLineNeverSits() => Assert.That(Sit(above: 0), Is.False);

    [Test]
    public void SectionTwelveLineIsRead()
    {
        const string text = "---\nschema_version: 1\nclass_name: \"Cleric\"\nclass_id: 6\nrealm: \"Albion\"\n---\n\n" +
            "## Specialization 1: Rejuvenation Cleric\n- **role**: PrimaryHealer\n\n" +
            "### 12. Resource Management\n- **Conservative Mana Floor**: 30% (x)\n" +
            "- **Sit Between Heals**: 90% (PvE Standard and PvE Pet Pulling)\n";
        SmartSpecDefinition spec = SmartAiMarkdownParser.Parse(text, "albion/cleric.md").Class.Specs.Single();
        Assert.That(spec.ManaFloorPercent, Is.EqualTo(30), "The mana floor is still read");
        Assert.That(spec.SitBetweenHealsAbovePercent, Is.EqualTo(90));
        Assert.That(SmartSpecProfile.Compile(spec).SitBetweenHealsAbovePercent, Is.EqualTo(90));
        Assert.That(SmartSpecProfile.Compile(spec).HealBands, Is.Empty, "The sit line is not a heal threshold");
    }

    [Test]
    public void ShippedFiles_TheHealingClassesSitAndOthersDont()
    {
        string dir = null;
        for (DirectoryInfo d = new(TestContext.CurrentContext.TestDirectory); d != null && dir == null; d = d.Parent)
        {
            string candidate = Path.Combine(d.FullName, "runtime", "server", "smartai", "classes");
            if (Directory.Exists(candidate) && Directory.Exists(Path.Combine(d.FullName, "development-source")))
                dir = candidate;
        }
        if (dir == null)
            Assert.Ignore("runtime/server/smartai/classes is not beside this build");

        string[] sitting = ["mentalist", "druid", "healer", "shaman", "cleric", "friar", "warden", "bard"];
        foreach (string path in Directory.EnumerateFiles(dir, "*.md", SearchOption.AllDirectories))
        {
            string name = Path.GetFileNameWithoutExtension(path);
            SmartClassDefinition c = SmartAiMarkdownParser.Parse(File.ReadAllText(path), name).Class;
            foreach (SmartSpecDefinition spec in c.Specs)
            {
                int expected = sitting.Contains(name) && spec.Role != "MeleeDPS" ? 90 : 0;
                Assert.That(SmartSpecProfile.Compile(spec).SitBetweenHealsAbovePercent, Is.EqualTo(expected), $"{name}: {spec.Name}");
            }
        }
    }
}
