using Exiled.API.Features;
using Exiled.CreditTags;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nickname
{
    public class Main : Plugin<Config>
    {
        private static readonly Main Singleton = new Main();
        public static readonly Main Instance = Singleton;
        public override string Author => "Akai";
        public override string Name => "Nicknames";

        public override Version Version => new Version("1.0.1");

        public static Dictionary<string, string> Nicknames = new Dictionary<string, string>();

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnMatchStart;
            Exiled.Events.Handlers.Server.RoundStarted += OnMatchStart;
            Exiled.Events.Handlers.Player.Left += PlayerLeave;
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnMatchStart;
            Exiled.Events.Handlers.Server.RoundStarted -= OnMatchStart;
            Nicknames = null;
        }
        public void OnMatchStart()
        {
            foreach (var kvp in Nicknames)
            {
                if(Player.TryGet(kvp.Key, out var player))
                {
                    player.CustomName = kvp.Value;
                }
            }
        }
        public void PlayerLeave(LeftEventArgs ev)
        {
            Nicknames.Remove(ev.Player.UserId);
        }
    }
}
