using System;
using System.Linq;
using System.Reflection;
using DOL.Database.Attributes;
using DOL.GS.ServerProperties;
using DOL.GS.SmartAI;
using NUnit.Framework;

/// <summary>SmartAI plan phases 1 and 2: the use_smart_ai switch (shared with the launcher), its server property,
/// the profile side table and the session fallback.</summary>
[TestFixture, NonParallelizable]
public class UT_LauncherSmartAiConfig
{
    [TearDown] public void Cleanup()
    {
        SmartAiFallback.Reset();
        SmartAiProperties.USE_SMART_AI = false;
    }

    [TestCase("True")]
    [TestCase("true")]
    [TestCase("TRUE")]
    [TestCase("  True  ")]
    public void Switch_ExplicitTrue_IsOn(string raw) => Assert.That(SmartAiSwitch.IsOn(raw), Is.True);

    // A clean database (no row), an empty value or a corrupt one must all mean the legacy AI.
    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    [TestCase("False")]
    [TestCase("false")]
    [TestCase("GARBAGE")]
    [TestCase("1")]
    [TestCase("yes")]
    [TestCase("Truee")]
    public void Switch_AnythingElse_IsOff(string raw) => Assert.That(SmartAiSwitch.IsOn(raw), Is.False);

    [Test]
    public void Switch_StoredForm_RoundTrips()
    {
        Assert.That(SmartAiSwitch.ToStored(true), Is.EqualTo("True"));
        Assert.That(SmartAiSwitch.ToStored(false), Is.EqualTo("False"));
        Assert.That(SmartAiSwitch.IsOn(SmartAiSwitch.ToStored(true)), Is.True);
        Assert.That(SmartAiSwitch.IsOn(SmartAiSwitch.ToStored(false)), Is.False);
        // Matches how the server stores a bool default (bool.ToString()).
        Assert.That(SmartAiSwitch.ToStored(false), Is.EqualTo(false.ToString()));
    }

    [Test]
    public void UseSmartAi_IsRegisteredOptInServerProperty()
    {
        FieldInfo field = typeof(SmartAiProperties).GetField(nameof(SmartAiProperties.USE_SMART_AI));
        Assert.That(field, Is.Not.Null);
        Assert.That(field.IsStatic && !field.IsInitOnly, Is.True, "InitProperties only loads static, writable fields");
        var attribute = field.GetCustomAttribute<ServerPropertyAttribute>();
        Assert.That(attribute, Is.Not.Null);
        Assert.That(attribute.Key, Is.EqualTo("use_smart_ai"));
        Assert.That(attribute.Category, Is.EqualTo("autonomous"));
        Assert.That(attribute.DefaultValue, Is.EqualTo(false));
        Assert.That(attribute.DefaultValue.ToString(), Is.EqualTo("False"), "The default row the server writes on a clean database");
    }

    [Test]
    public void SmartAiProperties_AreUniqueAndTyped()
    {
        var properties = typeof(SmartAiProperties).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => (Field: f, Attribute: f.GetCustomAttribute<ServerPropertyAttribute>()))
            .Where(p => p.Attribute != null)
            .ToList();
        Assert.That(properties.Select(p => p.Attribute.Key),
            Is.EquivalentTo(new[] { "use_smart_ai", "smart_ai_group_callouts", "smart_ai_debug_logging", "smart_ai_player_tools" }));
        foreach (var (field, attribute) in properties)
            Assert.That(attribute.DefaultValue.GetType(), Is.EqualTo(field.FieldType), field.Name);
    }

    [Test]
    public void SmartAiKeys_DoNotCollideWithOtherServerProperties()
    {
        string[] smartKeys = ["use_smart_ai", "smart_ai_group_callouts", "smart_ai_debug_logging", "smart_ai_player_tools"];
        int matches = typeof(SmartAiProperties).Assembly.GetTypes()
            .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            .Select(f => f.GetCustomAttribute<ServerPropertyAttribute>())
            .Count(a => a != null && smartKeys.Contains(a.Key, StringComparer.OrdinalIgnoreCase));
        Assert.That(matches, Is.EqualTo(smartKeys.Length));
    }

    [Test]
    public void ProfileRecord_IsSideTableKeyedByBotId()
    {
        var table = typeof(OfflineBotSmartAiProfileRecord).GetCustomAttribute<DataTable>();
        Assert.That(table, Is.Not.Null);
        Assert.That(table.TableName, Is.EqualTo("offline_bot_smartai_profiles"));
        Assert.That(typeof(OfflineBotSmartAiProfileRecord).GetProperty(nameof(OfflineBotSmartAiProfileRecord.BotId))
            .GetCustomAttribute<PrimaryKey>(), Is.Not.Null);
        foreach (string column in new[] { "Personality", "PreferredRole", "UpdatedUtc" })
        {
            var element = typeof(OfflineBotSmartAiProfileRecord).GetProperty(column).GetCustomAttribute<DataElement>();
            Assert.That(element, Is.Not.Null, column);
            Assert.That(element.AllowDbNull, Is.False, column);
        }
        var record = new OfflineBotSmartAiProfileRecord();
        Assert.That(record.Personality, Is.EqualTo("Auto"));
        Assert.That(record.PreferredRole, Is.EqualTo("Auto"));
        Assert.That(record.UpdatedUtc, Is.Not.Null);
    }

    [Test]
    public void Fallback_IsActiveOnlyWhenSwitchedOn()
    {
        SmartAiFallback.Reset();
        SmartAiProperties.USE_SMART_AI = false;
        Assert.That(SmartAiFallback.IsActive, Is.False);
        SmartAiProperties.USE_SMART_AI = true;
        Assert.That(SmartAiFallback.IsActive, Is.True);
    }

    [Test]
    public void Fallback_ScatteredErrorsDoNotTrip()
    {
        SmartAiFallback.Reset();
        SmartAiProperties.USE_SMART_AI = true;
        long now = 1_000_000;
        // One error every 3 s: never FailureLimit inside one minute.
        for (int i = 0; i < SmartAiFallback.FailureLimit * 4; i++, now += 3_000)
            Assert.That(SmartAiFallback.ReportFailure("test", null, now), Is.False);
        Assert.That(SmartAiFallback.IsTripped, Is.False);
        Assert.That(SmartAiFallback.IsActive, Is.True);
    }

    [Test]
    public void Fallback_BurstTripsForTheSessionButKeepsTheSetting()
    {
        SmartAiFallback.Reset();
        SmartAiProperties.USE_SMART_AI = true;
        long now = 5_000_000;
        for (int i = 0; i < SmartAiFallback.FailureLimit - 1; i++, now += 100)
            Assert.That(SmartAiFallback.ReportFailure("test", null, now), Is.False);
        Assert.That(SmartAiFallback.ReportFailure("last", new InvalidOperationException("boom"), now), Is.True);
        Assert.That(SmartAiFallback.IsTripped, Is.True);
        Assert.That(SmartAiFallback.IsActive, Is.False, "Every bot goes back to the legacy AI");
        Assert.That(SmartAiProperties.USE_SMART_AI, Is.True, "The player's saved choice is not rewritten");
        Assert.That(SmartAiFallback.TripReason, Does.Contain("last"));
        Assert.That(SmartAiFallback.ReportFailure("after", null, now + 1), Is.False, "Trips once");

        SmartAiFallback.Reset();
        Assert.That(SmartAiFallback.IsTripped, Is.False);
        Assert.That(SmartAiFallback.IsActive, Is.True);
    }
}
