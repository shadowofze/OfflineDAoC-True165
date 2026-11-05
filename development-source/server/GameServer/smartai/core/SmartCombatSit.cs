using DOL.AI.Brain;

namespace DOL.GS.SmartAI
{
    /// <summary>
    /// Sitting between heals (owner 2026-11-05). A spec whose section 12 has "- **Sit Between Heals**: N%" sits down in
    /// a PvE fight (PvE Standard and PvE Pet Pulling) to regain power faster, as a player healer does, while:
    /// - every living group member in reach, and the bot, is above N% health;
    /// - its power is not full;
    /// - nothing in its spec acted this think (no heal, cure, buff or spell to cast);
    /// - no enemy is on it or hit it in the last few seconds, and it is not casting, attacking or moving;
    /// - the group's leader is standing still within reach (the bot isn't about to follow).
    /// It stands to cast, attack or move, when an enemy hits it, and when the rule no longer holds; after standing it
    /// waits a moment before sitting again. Never against players (PvP, RvR). Regeneration is the player's seated
    /// interval (GameBot regeneration in True 1.65 Mode); no numbers change.
    /// </summary>
    public static class SmartCombatSit
    {
        /// <summary>No sitting this soon after it was hit.</summary>
        public const int HitQuietMs = 4000;
        /// <summary>No sitting this soon after it stood from a sit between heals.</summary>
        public const int ResitDelayMs = 2500;
        /// <summary>The leader must be this close (the companion rest's follow distance).</summary>
        public const int LeaderReach = SmartRestPolicy.FollowBeyondDistance;

        /// <summary>The rule, on its own for tests.</summary>
        public static bool ShouldSit(int sitAbovePercent, SmartGameMode mode, bool inGroup, int ownManaPercent,
            int lowestMemberHealthPercent, bool busy, bool enemyOnMe, long sinceHitMs, bool leaderSettled, long sinceStoodMs) =>
            sitAbovePercent > 0 && mode == SmartGameMode.PvE && inGroup && ownManaPercent < 100 &&
            lowestMemberHealthPercent > sitAbovePercent && !busy && !enemyOnMe && sinceHitMs >= HitQuietMs &&
            leaderSettled && sinceStoodMs >= ResitDelayMs;

        /// <summary>
        /// Called when no candidate acted this think. Seats the bot and returns true when the rule holds; otherwise stands
        /// it up if it was sitting between heals and returns false (the think goes on as before).
        /// </summary>
        public static bool TrySit(BotBrain brain, GameBot bot, SmartSpecProfile profile, in SmartBotPerception p, out string note)
        {
            note = null;
            if (profile.SitBetweenHealsAbovePercent <= 0 || !Classic165Mode.IsOn)
                return false;
            long now = GameLoop.GameLoopTime;
            int lowest = LowestGroupHealth(brain, bot);
            bool busy = bot.IsCasting || bot.castingComponent?.HasPendingSkillRequests == true || bot.IsAttacking ||
                        bot.IsMoving || bot.IsCrowdControlled || !bot.IsAlive;
            long lastHit = bot.LastAttackedByEnemyTick;
            if (ShouldSit(profile.SitBetweenHealsAbovePercent, p.Mode, bot.Group != null, bot.ManaPercent, lowest, busy,
                    brain.SmartAiEnemiesOnMe() > 0, lastHit > 0 ? now - lastHit : long.MaxValue, LeaderSettled(bot),
                    now - bot.CombatSeatStoodAt) && bot.SitBetweenHeals())
            {
                note = $"sitting between heals: group above {profile.SitBetweenHealsAbovePercent}% (lowest {lowest}%), power {bot.ManaPercent}%";
                return true;
            }
            bot.StandFromCombatSeat();
            return false;
        }

        /// <summary>The lowest health of the bot and its living group members in support reach (100 with nobody).</summary>
        private static int LowestGroupHealth(BotBrain brain, GameBot bot)
        {
            int lowest = bot.HealthPercent;
            if (bot.Group == null)
                return lowest;
            foreach (GameLiving m in bot.Group.GetMembersInTheGroup())
                if (m is { IsAlive: true } && m != bot && brain.SmartAiInSupportReach(m) && m.HealthPercent < lowest)
                    lowest = m.HealthPercent;
            return lowest;
        }

        /// <summary>The group's leader (if it isn't the bot) is alive, here, close and not moving.</summary>
        private static bool LeaderSettled(GameBot bot)
        {
            GameLiving leader = bot.Group?.LivingLeader;
            if (leader == null || leader == bot)
                return true;
            return leader.IsAlive && !leader.IsMoving && leader.CurrentRegionID == bot.CurrentRegionID &&
                   bot.IsWithinRadius(leader, LeaderReach);
        }
    }
}
