using DOL.GS.SmartAI;
using NUnit.Framework;
using Position = DOL.GS.SmartAI.SmartFlank.Position;

/// <summary>Owner 2026-11-05: flanking for positional styles, without rubberbanding.</summary>
[TestFixture]
public class UT_SmartAiFlank
{
    [Test]
    public void TheArcsAreTheStyleArcsWithAMargin()
    {
        Assert.Multiple(() =>
        {
            // StyleProcessor: back 150-210, side 45-150 and 210-315.
            Assert.That(SmartFlank.InArc(Position.Back, 180, 0), Is.True);
            Assert.That(SmartFlank.InArc(Position.Back, 155, 0), Is.True);
            Assert.That(SmartFlank.InArc(Position.Back, 155, SmartFlank.ArcMargin), Is.False, "too near the edge to count");
            Assert.That(SmartFlank.InArc(Position.Back, 100, 0), Is.False);
            Assert.That(SmartFlank.InArc(Position.Side, 97.5f, SmartFlank.ArcMargin), Is.True);
            Assert.That(SmartFlank.InArc(Position.Side, 262.5f, SmartFlank.ArcMargin), Is.True);
            Assert.That(SmartFlank.InArc(Position.Side, 180, 0), Is.False, "behind is not the side");
            Assert.That(SmartFlank.InArc(Position.Side, 10, 0), Is.False);
            Assert.That(SmartFlank.InArc(Position.None, 0, 0), Is.True, "nothing wanted: nothing to do");
        });
    }

    [Test]
    public void TheSpotIsTheMiddleOfTheArcAndTheBackGoesRoundTheSide()
    {
        Assert.Multiple(() =>
        {
            Assert.That(SmartFlank.SlotAngle(Position.Side, 20), Is.EqualTo(97.5f));
            Assert.That(SmartFlank.SlotAngle(Position.Side, 340), Is.EqualTo(262.5f));
            Assert.That(SmartFlank.SlotAngle(Position.Back, 10), Is.EqualTo(97.5f), "from the front: the nearer side first");
            Assert.That(SmartFlank.SlotAngle(Position.Back, 350), Is.EqualTo(262.5f));
            Assert.That(SmartFlank.SlotAngle(Position.Back, 120), Is.EqualTo(180f), "from the side: straight to the back");
            Assert.That(SmartFlank.InArc(Position.Side, SmartFlank.SlotAngle(Position.Side, 0), SmartFlank.ArcMargin), Is.True);
            Assert.That(SmartFlank.InArc(Position.Back, SmartFlank.SlotAngle(Position.Back, 120), SmartFlank.ArcMargin), Is.True);
        });
    }

    [Test]
    public void FlankingOnlyWhereItCanWork()
    {
        Assert.Multiple(() =>
        {
            Assert.That(SmartFlank.Applies(true, true, true, false, false, true), Is.True);
            Assert.That(SmartFlank.Applies(false, true, true, false, false, true), Is.False, "SmartAI off or no spec");
            Assert.That(SmartFlank.Applies(true, false, true, false, false, true), Is.False, "alone, the monster faces you");
            Assert.That(SmartFlank.Applies(true, true, false, false, false, true), Is.False, "players turn freely");
            Assert.That(SmartFlank.Applies(true, true, true, true, false, true), Is.False, "a moving monster");
            Assert.That(SmartFlank.Applies(true, true, true, false, true, true), Is.False, "the monster is on this bot");
            Assert.That(SmartFlank.Applies(true, true, true, false, false, false), Is.False, "not in melee");
        });
    }
}
