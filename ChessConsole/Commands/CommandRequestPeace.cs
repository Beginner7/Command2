using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandRequestPeace : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("peace", "Предложить оппоненту перемирие"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.IsInGame())
            {
                if (Utils.CheckArgs(ArgsNeed, args.Count()))
                {
                    var request = new PeaceRequest();
                    request.From = CurrentUser.Name;
                    request.GameID = CurrentUser.CurrentGame.Value;
                    var response = ServerProvider.MakeRequest(request);
                    Console.WriteLine(response.Status == Statuses.Ok
                        ? "Peace request sended to opponent."
                        : response.Status.ToString());
                }
            }
        }
    }
}
