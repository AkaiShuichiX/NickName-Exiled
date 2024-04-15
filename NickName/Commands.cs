using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using Nickname;

namespace NickName
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Nick : ICommand
    {
        public string Command { get; } = "nickname";

        public string[] Aliases { get; } = { "nick", "nn" };

        public string Description { get; } = "Chame the nickname of the player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Main.Instance.Config.NickNameCommand)
            {
                response = "This command is currently disabled";
                return false;
            }
            
            Player player = Player.Get(sender);
            Log.Debug($"Player {player.Nickname} (ID: {player.UserId}) run the Change NickName Command");

            // Check if the player's group name is in the NickPermissions array
            if (!sender.CheckPermission("nickname.change"))
            {
                response = "Nie masz permisji żeby to zrobić - nickname.change";
                return false;
            }

            if (arguments.Count < 1 || arguments.At(0) == null)
            {
                response = "Podaj imię - .nn <imię>";
                return false;
            }

            if (Main.Nicknames.TryGetValue(player.UserId, out string value))
            {
                Main.Nicknames[player.UserId] = arguments.At(0);
            }
            else
            {
                Main.Nicknames.Add(player.UserId, arguments.At(0));
            }

            player.CustomName = arguments.At(0);
            response = "Ustaw nickname na " + arguments.At(0);

            return true;
        }
    }
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ClearNick : ICommand
    {
        public string Command { get; } = "clearnickname";

        public string[] Aliases { get; } = { "clearnick", "cn" };

        public string Description { get; } = "Usuwa nickname gracza";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Main.Instance.Config.ClearNicknameCommand)
            {
                response = "This command is currently disabled";
                return false;
            }

            
            Player player = Player.Get(sender);
            Log.Debug($"Player {player.Nickname} (ID: {player.UserId}) run the Clear Nick Command");

            if (!sender.CheckPermission("nickname.clear"))
            {
                response = "Nie masz permisji żeby to zrobić - nickname.clear";
                return false;
            }
            
            Main.Nicknames.Remove(player.UserId);

            player.CustomName = string.Empty;
            response = "Komenda wysłana pomyślnie";
            return true;
        }
    }
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Name : ICommand
    {
        public string Command { get; } = "name";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Pokazuje twoje imie na 5 sekund";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Main.Instance.Config.NameCommand)
            {
                response = "This command is currently disabled";
                return false;
            }
            
            Player player = Player.Get(sender);

            Log.Debug($"Player {player.Nickname} (ID: {player.UserId}) run the Name Command");

            // Show a hint with the current player's name for 5 seconds
            player.ShowHint(string.Format(Main.Instance.Config.NameHintMessage, player.DisplayNickname), Main.Instance.Config.NameHintDuration);

            response = "Komenda wysłana pomyślnie";
            return true;
        }
    }
}
