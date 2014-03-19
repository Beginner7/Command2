using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandStop : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("stop", "Остановить поиск игры"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsLoggedIn() && Utils.IsNotInGame())
                {
                    if (CurrentUser.Searching)
                    {
                        var request = new StopRequest {UserName = CurrentUser.Name};
                        var response = ServerProvider.MakeRequest<StopResponse>(request);
                        if (response.Status == Statuses.Ok)
                        {
                            Console.WriteLine("Searching stoped.");
                            CurrentUser.Searching = false;
                        }
                        else
                        {
                            Console.WriteLine("Bad status");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You dont searching.");
                    }
                }
            }
        }
    }
}
