using System.Numerics;
using DOL.Database;
using DOL.GS;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>Owner 2026-11-05: a caster alone nukes, roots (or stuns) the monster coming at it, backs off and nukes again.</summary>
[TestFixture]
public class UT_SmartAiKite
{
    private static Spell Make(string type, double value, string target = "Enemy") =>
        new(new DbSpell { SpellID = 900001, Name = "test", Type = type, Target = target, Value = value, Range = 1500, CastTime = 2.5 }, 10);

    [Test]
    public void RootsAndStunsAreRecognised()
    {
        Assert.Multiple(() =>
        {
            Assert.That(SmartKite.IsRoot(Make("SpeedDecrease", 99)), Is.True);
            Assert.That(SmartKite.IsRoot(Make("UnbreakableSpeedDecrease", 99)), Is.True);
            Assert.That(SmartKite.IsRoot(Make("SpeedDecrease", 40)), Is.False, "a snare slows, it doesn't hold");
            Assert.That(SmartKite.IsRoot(Make("SpeedDecrease", 99, "Self")), Is.False);
            Assert.That(SmartKite.IsStun(Make("Stun", 0)), Is.True);
            Assert.That(SmartKite.IsStun(Make("DirectDamage", 0)), Is.False);
        });
    }

    [Test]
    public void TheControlSpellStartsEarlyEnoughToLandFirst()
    {
        Assert.Multiple(() =>
        {
            // 2.5 s cast + 0.6 s margin at 200 units/s, plus a 200 melee range: 820.
            Assert.That(SmartKite.ControlDistance(2_500, 200, 200), Is.EqualTo(820));
            Assert.That(SmartKite.ControlDistance(0, 200, 200), Is.EqualTo(320), "an instant one just before contact");
            Assert.That(SmartKite.ControlDistance(4_000, 600, 200), Is.EqualTo(1_400), "capped");
        });
    }

    [Test]
    public void TheCasterBacksOffToItsNukeRange()
    {
        Assert.Multiple(() =>
        {
            Assert.That(SmartKite.DesiredGap(1_500), Is.EqualTo(1_300));
            Assert.That(SmartKite.DesiredGap(1_200), Is.EqualTo(1_050));
            Assert.That(SmartKite.DesiredGap(500), Is.EqualTo(SmartKite.MinimumGap));
        });
    }

    [Test]
    public void TheEscapePointLeadsAwayFromTheMonster()
    {
        Vector2 caster = new(1000, 1000), monster = new(1000, 800);
        Vector2 straight = SmartKite.EscapePoint(caster, monster, 0, 500);
        Vector2 side = SmartKite.EscapePoint(caster, monster, 90, 500);
        Assert.Multiple(() =>
        {
            Assert.That(straight.X, Is.EqualTo(1000).Within(0.01));
            Assert.That(straight.Y, Is.EqualTo(1500).Within(0.01), "straight away");
            Assert.That(Vector2.Distance(side, caster), Is.EqualTo(500).Within(0.01));
            Assert.That(Vector2.Distance(side, monster), Is.GreaterThan(Vector2.Distance(caster, monster)), "a side step still opens the gap");
        });
    }

    [Test]
    public void KitingOnlyAloneInTheOpenAgainstMonsters()
    {
        Assert.Multiple(() =>
        {
            Assert.That(SmartKite.Applies(true, true, true, true, true), Is.True);
            Assert.That(SmartKite.Applies(true, true, true, false, true), Is.False, "never in a dungeon");
            Assert.That(SmartKite.Applies(true, false, true, true, true), Is.False, "a group holds the monster instead");
            Assert.That(SmartKite.Applies(true, true, false, true, true), Is.False, "casters only");
            Assert.That(SmartKite.Applies(true, true, true, true, false), Is.False, "not against players");
            Assert.That(SmartKite.Applies(false, true, true, true, true), Is.False, "SmartAI off");
        });
    }
    [Test]
    public void ATheurgistCallsEachIcePetAboutFourHundredUnitsFromTheLast()
    {
        var ice = new Spell(new DbSpell { SpellID = 900002, Name = "Call Minor Ice Elemental", Type = "SummonTheurgistPet", Target = "Enemy", Range = 1500 }, 5);
        var earth = new Spell(new DbSpell { SpellID = 900003, Name = "Call Minor Earth Elemental", Type = "SummonTheurgistPet", Target = "Enemy", Range = 1500 }, 5);
        var spots = new System.Collections.Generic.List<Vector2>();
        Assert.Multiple(() =>
        {
            Assert.That(SmartKite.IsIcePet(ice), Is.True);
            Assert.That(SmartKite.IsIcePet(earth), Is.False, "earth pets are meant to stand in melee");
            Assert.That(SmartKite.FreshPetSpot(new(0, 0), spots, SmartKite.PetSpreadDistance), Is.True, "the first pet: anywhere");
        });
        spots.Add(new(0, 0));
        Assert.Multiple(() =>
        {
            Assert.That(SmartKite.FreshPetSpot(new(200, 0), spots, SmartKite.PetSpreadDistance), Is.False, "too close to the first pet");
            Assert.That(SmartKite.FreshPetSpot(new(380, 0), spots, SmartKite.PetSpreadDistance), Is.True, "about 400 away");
        });
        spots.Add(new(400, 0));
        Assert.That(SmartKite.FreshPetSpot(new(200, 380), spots, SmartKite.PetSpreadDistance), Is.True, "away from both");
        Assert.That(SmartKite.FreshPetSpot(new(700, 0), spots, SmartKite.PetSpreadDistance), Is.False, "too close to the second");
    }
}