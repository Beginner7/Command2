using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandSurrender : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("surrender", "Сдаться"); } }
        public override int ArgsNeed { get { return 0; } }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsInGame())
                {
                    var request = new SurrenderRequest {From = CurrentUser.Name, GameID = CurrentUser.CurrentGame.Value};
                    var response = ServerProvider.MakeRequest<ChatResponse>(request);
                    if (response.Status != Statuses.OK)
                    {
                        Console.WriteLine("Bad status.");
                    }
                    else
                    {
                        Console.WriteLine("You surrendered.");
                        CurrentUser.CurrentGame = null;
                    }
                }
            }
            return true;
        }
    }
}
