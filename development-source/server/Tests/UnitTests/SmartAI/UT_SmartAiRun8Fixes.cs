using DOL.Database;
using DOL.GS;
using DOL.GS.SmartAI;
using DOL.GS.Styles;
using NUnit.Framework;

/// <summary>Run 8 (2026-11-06): sit/stand loop, Rangers' cancelled draws, stealth openers, positional styles.</summary>
[TestFixture]
public class UT_SmartAiRun8Fixes
{
    private static Style S(string name, int opening = 0, int position = 0, bool stealth = false) =>
        new(new DbStyle { Name = name, ID = name.GetHashCode() & 0xFFFF, SpecKeyName = Specs.Sword, SpecLevelRequirement = 1,
            WeaponTypeRequirement = (int)eObjectType.Sword, OpeningRequirementType = opening, OpeningRequirementValue = position,
            StealthRequirement = stealth, GrowthRate = 5 }, null);

    [Test]
    public void ARestBeforeTravelIsLeftAloneWhileItApplies()
    {
        Assert.That(AutonomousWorldBotController.TravelRestHolds(true, true, false, false), Is.True);
        Assert.That(AutonomousWorldBotController.TravelRestHolds(false, true, false, false), Is.False, "not resting for travel");
        Assert.That(AutonomousWorldBotController.TravelRestHolds(true, true, true, false), Is.False, "joined a group");
        Assert.That(AutonomousWorldBotController.TravelRestHolds(true, true, false, true), Is.False, "in a fight");
        Assert.That(AutonomousWorldBotController.TravelRestHolds(true, false, false, false), Is.False, "SmartAI off");
    }

    [Test]
    public void ABowDrawIsKeptWhenASpellIsOnlyTried()
    {
        Assert.That(BotRangedCombat.KeepsDrawForSpells(true, eActiveWeaponSlot.Distance), Is.True);
        Assert.That(BotRangedCombat.KeepsDrawForSpells(true, eActiveWeaponSlot.Standard), Is.False, "a melee swing may stop for a cast");
        Assert.That(BotRangedCombat.KeepsDrawForSpells(false, eActiveWeaponSlot.Distance), Is.False);
    }

    [Test]
    public void AnOpenerOrAPositionalStyleInPlaceBeatsTheQueuedAndTheBestStyle()
    {
        Style opener = S("Backstab", stealth: true), back = S("Wasp's Sting", (int)Style.eOpening.Positional,
            (int)Style.eOpeningPosition.Back), queued = S("Taunting Blade"), best = S("Bumblebee's Sting");
        Assert.That(BotMeleeStylePolicy.Choose(true, queued, opener, back, best), Is.SameAs(opener));
        Assert.That(BotMeleeStylePolicy.Choose(true, queued, null, back, best), Is.SameAs(back));
        Assert.That(BotMeleeStylePolicy.Choose(true, queued, null, null, best), Is.SameAs(queued));
        Assert.That(BotMeleeStylePolicy.Choose(true, null, null, null, best), Is.SameAs(best));
        Assert.That(BotMeleeStylePolicy.Choose(false, queued, opener, back, best), Is.SameAs(queued), "the legacy brain is unchanged");
        Assert.That(BotMeleeStylePolicy.Choose(false, null, opener, back, best), Is.SameAs(best));
    }

    [Test]
    public void AnAssassinWalksInHiddenOnlyOnAMonsterNotOnItAndInAGroupOnlyOnOneAlreadyFighting()
    {
        bool A(bool grouped = false, bool targetInCombat = false, bool onMe = false, bool monster = true, bool opener = true,
            bool busy = false, bool hit = false) =>
            SmartStealthOpener.Applies(true, true, true, opener, monster, onMe, grouped, targetInCombat, busy, hit);
        Assert.That(A(), Is.True, "alone, a fresh monster");
        Assert.That(A(grouped: true), Is.False, "a group's assassin never pulls");
        Assert.That(A(grouped: true, targetInCombat: true), Is.True, "the party is fighting it");
        Assert.That(A(onMe: true), Is.False, "it is already on the bot");
        Assert.That(A(monster: false), Is.False, "players and bots: the RvR ambush");
        Assert.That(A(opener: false), Is.False, "no usable opener");
        Assert.That(A(busy: true), Is.False);
        Assert.That(A(hit: true), Is.False);
    }

    [Test]
    public void AnOpenerNeedingTheBackIsInPlaceOnlyBehindTheTarget()
    {
        Assert.That(SmartStealthOpener.InPlace(true, SmartFlank.Position.None, 0), Is.True);
        Assert.That(SmartStealthOpener.InPlace(false, SmartFlank.Position.None, 0), Is.False);
        Assert.That(SmartStealthOpener.InPlace(true, SmartFlank.Position.Back, 180), Is.True);
        Assert.That(SmartStealthOpener.InPlace(true, SmartFlank.Position.Back, 0), Is.False);
        Assert.That(SmartStealthOpener.PositionOf(S("Backstab", stealth: true)), Is.EqualTo(SmartFlank.Position.None));
        Assert.That(SmartStealthOpener.PositionOf(S("Assassinate", (int)Style.eOpening.Positional, (int)Style.eOpeningPosition.Back, true)),
            Is.EqualTo(SmartFlank.Position.Back));
    }
}
