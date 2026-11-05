using System.IO;
using System.Linq;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>Owner 2026-11-04: class abilities and peels - Guard/Protect on the healer when the spec says so, Berserk
/// gated (never below 40% health, not on a nearly dead monster).</summary>
[TestFixture]
public class UT_SmartAiAbilities
{
    [TestCase("Guard", "Guard on Primary Cleric", "Never let drop", true)]
    [TestCase("Guard", "Guard / Protect", "Active on Cleric", true)]
    [TestCase("Protect", "Guard / Protect", "Active on Cleric", true)]
    [TestCase("Guard", "Guard Ability", "Targeted on main Cleric", true)]
    [TestCase("Guard", "Guard on the Healer", "", true)]
    [TestCase("Protect", "Protect Ability", "", false)]
    [TestCase("Guard", "Guard on the squishiest caster", "", false)]
    [TestCase("Guard", "Shield Slam peel", "On Cleric attacker", false)]
    public void LineOnHealer(string ability, string name, string condition, bool onHealer) =>
        Assert.That(SmartAbilityPolicy.LineOnHealer(ability, name, condition), Is.EqualTo(onHealer));

    [TestCase(100, 80, false, true)]
    [TestCase(40, 80, false, true)]
    [TestCase(39, 80, false, false)]   // never below 40% health (the Berserker template)
    [TestCase(90, 29, false, false)]   // not on a nearly dead monster
    [TestCase(90, 10, true, true)]     // a player: any time
    [TestCase(30, 90, true, false)]
    public void BerserkGate(int self, int target, bool player, bool allowed) =>
        Assert.That(SmartAbilityPolicy.BerserkAllowed(self, target, player), Is.EqualTo(allowed));

    [TestCase(80, false, true)]
    [TestCase(30, false, true)]
    [TestCase(29, false, false)]   // Stag / Triple Wield are not spent on a nearly dead monster
    [TestCase(5, true, true)]      // a player: any time
    public void CombatCooldownGate(int target, bool player, bool allowed) =>
        Assert.That(SmartAbilityPolicy.CombatCooldownAllowed(target, player), Is.EqualTo(allowed));

    [TestCase(true, true, false, 0, true, true)]     // a caster being hit
    [TestCase(true, true, false, 1, false, true)]    // a melee enemy on it
    [TestCase(true, true, false, 0, false, false)]   // nobody on it
    [TestCase(false, true, false, 2, true, false)]   // out of combat
    [TestCase(true, false, false, 2, true, false)]   // not a caster
    [TestCase(true, true, true, 2, true, false)]     // stunned or mezzed
    public void MasteryOfConcentration_WhenPressured(bool combat, bool caster, bool cc, int melee, bool interrupted, bool wanted) =>
        Assert.That(SmartRealmAbilities.Wanted(SmartRealmAbilityActions.MasteryOfConcentration, new SmartBotPerception
        {
            InCombat = combat, IsCaster = caster, Incapacitated = cc, MeleeAttackersOnSelf = melee, BeingInterrupted = interrupted,
        }), Is.EqualTo(wanted));

    [TestCase(true, 2, 0, true)]    // two hurt
    [TestCase(true, 0, 1, true)]    // one nearly dead
    [TestCase(true, 1, 0, false)]
    [TestCase(false, 3, 2, false)]  // out of combat
    public void BunkerOfFaith_OnGroupBurst(bool combat, int below65, int below40, bool wanted) =>
        Assert.That(SmartRealmAbilities.Wanted(SmartRealmAbilityActions.BunkerOfFaith, new SmartBotPerception
        {
            InCombat = combat, MembersBelow65 = below65, MembersBelow40 = below40,
        }), Is.EqualTo(wanted));

    [TestCase(7, true)]    // Wizard
    [TestCase(5, true)]    // Theurgist
    [TestCase(42, true)]   // Mentalist
    [TestCase(55, true)]   // Animist
    [TestCase(29, true)]   // Runemaster
    [TestCase(6, false)]   // Cleric
    [TestCase(26, false)]  // Healer
    [TestCase(47, false)]  // Druid
    [TestCase(12, false)]  // Necromancer
    [TestCase(39, false)]  // Bainshee (not 1.65)
    [TestCase(63, false)]  // Sluaghbinder (not 1.65)
    public void QuickCast_ListCastersOnly(int classId, bool listCaster) =>
        Assert.That(SmartQuickCast.IsListCaster(classId), Is.EqualTo(listCaster));

    [TestCase(true, 50, false, false, true)]
    [TestCase(false, 50, false, false, false)]  // nothing on it
    [TestCase(true, 19, false, false, false)]   // not enough power for a doubled cost
    [TestCase(true, 50, true, false, false)]    // in melee attack mode (player rule)
    [TestCase(true, 50, false, true, false)]    // the 30 s lockout
    public void QuickCast_WhenPressured(bool pressure, int mana, bool melee, bool locked, bool wanted) =>
        Assert.That(SmartQuickCast.Wanted(pressure, mana, melee, locked), Is.EqualTo(wanted));

    [Test]
    public void MocAndBof_AreSmartAiAbilities()
    {
        Assert.That(SmartRealmAbilities.IsSafe(SmartRealmAbilityActions.MasteryOfConcentration), Is.True);
        Assert.That(SmartRealmAbilities.IsSafe(SmartRealmAbilityActions.BunkerOfFaith), Is.True);
    }

    [Test]
    public void ShippedTanks_GuardTheirHealer()
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
        SmartClassDefinition armsman = SmartAiMarkdownParser.Parse(File.ReadAllText(Path.Combine(dir, "albion", "armsman.md")), "albion/armsman.md").Class;
        Assert.That(armsman.Specs.Select(SmartSpecProfile.Compile).Any(p => p.GuardsHealer && p.ProtectsHealer), Is.True,
            "The Armsman's \"Guard on Primary Cleric: Priority 100\"");
    }
}
