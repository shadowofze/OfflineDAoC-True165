using System.Linq;
using DOL.AI.Brain;

namespace DOL.GS.Commands
{
    [CmdAttribute("&defensive", ePrivLevel.Player, "Companions wait for enemies to approach you", "/defensive")]
    public class CompanionDefensiveCommand : AbstractCommandHandler, ICommandHandler
    {
        public void OnCommand(GameClient client, string[] args)
        {
            CompanionEngagementMode.Set(client.Player, true);
            if (client.Player.Group != null)
                foreach (GameBot bot in client.Player.Group.GetMembersInTheGroup().OfType<GameBot>())
                    if (CompanionEngagementMode.DefensiveLeader(bot) == client.Player && bot.Brain is BotBrain brain)
                        brain.EnforceCompanionEngagementRange();
            DisplayMessage(client, "Companions: DEFENSIVE. Pull enemies within 350 units of you; companions and their pets will engage nearby threats. Use /aggressive to restore normal assisting.");
        }
    }
    [CmdAttribute("&petpull", ePrivLevel.Player, "Companions leave the monsters your pet pulls to it", "/petpull [on|off]")]
    public class CompanionPetPullCommand : AbstractCommandHandler, ICommandHandler
    {
        public void OnCommand(GameClient client, string[] args)
        {
            GamePlayer player = client.Player;
            bool on = args.Length > 1
                ? !args[1].Equals("off", System.StringComparison.OrdinalIgnoreCase)
                : !CompanionEngagementMode.IsPetPulling(player);
            CompanionEngagementMode.SetPetPull(player, on);
            if (on && player.Group != null)
                foreach (GameBot bot in player.Group.GetMembersInTheGroup().OfType<GameBot>())
                    if (CompanionEngagementMode.RestrictingLeader(bot) == player && bot.Brain is BotBrain brain)
                        brain.EnforceCompanionEngagementRange();
            DisplayMessage(client, on
                ? $"Companions: PET PULLING. Monsters on your pet are left to it until they drop to {CompanionEngagementMode.PetPullPackHoldAbovePercent}%, " +
                  "then casters and healers join with area spells only; melee only fights monsters that break away. Your pet gets the " +
                  $"smallest heal once a monster on it is at {CompanionEngagementMode.PetPullHealPetAtOrBelowPercent}%. Companions stay put: " +
                  "bring the monsters to them. /petpull again to stop."
                : "Companions: pet pulling OFF. They assist your pet's fights normally again.");
        }
    }
    [CmdAttribute("&aggressive", ePrivLevel.Player, "Restore normal companion assisting (default)", "/aggressive")]
    public class CompanionAggressiveCommand : AbstractCommandHandler, ICommandHandler
    {
        public void OnCommand(GameClient client, string[] args)
        {
            CompanionEngagementMode.Set(client.Player, false);
            DisplayMessage(client, "Companions: AGGRESSIVE (default). They assist your attacks normally again.");
        }
    }
}
