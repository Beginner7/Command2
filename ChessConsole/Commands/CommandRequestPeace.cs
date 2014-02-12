using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandRequestPeace : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("peace", "Предложить оппоненту перемирие"); } }
        public override int ArgsNeed { get { return 0; } }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.IsInGame())
            {
                if (Utils.CheckArgs(ArgsNeed, args.Count()))
                {
                    var request = new PeaceRequest();
                    request.From = CurrentUser.Name;
                    request.GameID = CurrentUser.CurrentGame.Value;
                    var response = ServerProvider.MakeRequest(request);
                    if (response.Status == Statuses.OK)
                    {
                        Console.WriteLine("Peace request sanded to opponent.");
                    }
                    else
                    {
                        Console.WriteLine(response.Status.ToString());
                    }
                }
            }
            return true;
        }
    }
}
