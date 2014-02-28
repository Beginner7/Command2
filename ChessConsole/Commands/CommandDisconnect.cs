using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandDisconnect : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("disconnect", "Покинуть игру"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.IsInGame())
            {
                if (Utils.CheckArgs(ArgsNeed, args.Count()))
                {
                    var request = new DisconnectRequest
                    {
                        User = CurrentUser.Name,
                        GameID = CurrentUser.CurrentGame.Value
                    };
                    var response = ServerProvider.MakeRequest(request);
                    if (response.Status == Statuses.Ok)
                    {
                        Console.WriteLine("You abandoned the game.");
                        CurrentUser.CurrentGame = null;
                    }
                    else
                    {
                        Console.WriteLine(response.Status.ToString());
                    }
                }
            }
        }
    }
}
