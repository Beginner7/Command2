using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;
using System.Reflection;

namespace ChessConsole.Commands
{
    public class CommandEcho : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("echo", "Эхо запрос на сервер"); } }
        public override int ArgsNeed { get { return -1; } }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                var request = new EchoRequest();
                request.EchoString = args.StrJoin(' ');
                var response = ServerProvider.MakeRequest<EchoResponse>(request);
                if (response.Status == Statuses.OK)
                {
                    Console.WriteLine(response.EchoString);
                }
                else
                {
                    Console.WriteLine("Bad status.");
                }
            }
            return true;
        }
    }
}
