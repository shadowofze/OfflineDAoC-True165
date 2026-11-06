using System;
using System.Reflection;

namespace DOL.GS
{
    /// <summary>
    /// Says what a game-loop work item is, for the watchdog's stuck-worker warning. Run 7 (2026-11-05): two workers
    /// hung for good in one tick and the warning named only the thread, so nothing said which bot or object froze the
    /// server. Never throws; reads another thread's objects without locks (a diagnostic, values may be a moment old).
    /// </summary>
    public static class GameLoopWorkDescriber
    {
        public static string Describe(object item)
        {
            try
            {
                if (item == null)
                    return "nothing";
                if (Owned(item) is GameObject owner && !ReferenceEquals(owner, item))
                    return $"{item.GetType().Name} of {Describe(owner)}";
                if (item is not GameObject obj)
                    return item.GetType().Name;
                string text = $"{obj.GetType().Name} \"{obj.Name}\" id={obj.ObjectID} region={obj.CurrentRegionID} at={obj.X},{obj.Y},{obj.Z}";
                if (obj is GameLiving living)
                    text += $" level={living.Level} inCombat={living.InCombat}";
                if (obj is GameBot bot)
                {
                    text += $" class=\"{bot.ClassName}\"";
                    if (bot.PersistentRecord is { } record)
                        text += $" activity=\"{record.Activity}\" goal=\"{record.CurrentGoal}\" target=\"{record.TargetName}\"";
                }
                if (obj is GameNPC npc && npc.Brain != null)
                    text += $" brain={npc.Brain.GetType().Name}";
                return text;
            }
            catch (Exception e)
            {
                return $"{item?.GetType().Name ?? "?"} (could not describe: {e.GetType().Name})";
            }
        }

        /// <summary>The game object a component belongs to (an Owner property or field), or null.</summary>
        private static object Owned(object item)
        {
            if (item is GameObject)
                return null;
            Type type = item.GetType();
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (string name in new[] { "Owner", "owner", "_owner" })
            {
                if (type.GetProperty(name, flags) is { CanRead: true } property && property.GetIndexParameters().Length == 0 &&
                    typeof(GameObject).IsAssignableFrom(property.PropertyType))
                    return property.GetValue(item);
                if (type.GetField(name, flags) is { } field && typeof(GameObject).IsAssignableFrom(field.FieldType))
                    return field.GetValue(item);
            }
            return null;
        }
    }
}
