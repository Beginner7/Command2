using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandPlay : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("play", "Найти игру"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsLoggedIn() && Utils.IsNotInGame())
                {
                    if (!CurrentUser.Searching)
                    {
                        var request = new PlayRequest {UserName = CurrentUser.Name};
                        var response = ServerProvider.MakeRequest<PlayResponse>(request);
                        if (response.Status == Statuses.Ok)
                        {
                            Console.WriteLine("Searching game...");
                            CurrentUser.Searching = true;
                        }
                        else
                        {
                            Console.WriteLine("Bad status");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You allready searching.");
                    }
                }
            }
        }
    }
}
