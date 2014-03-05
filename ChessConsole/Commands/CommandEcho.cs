using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandEcho : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("echo", "Эхо запрос на сервер"); } }
        public override int ArgsNeed { get { return -1; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                var request = new EchoRequest {EchoString = args.StrJoin(' ')};
                var response = ServerProvider.MakeRequest<EchoResponse>(request);
                Console.WriteLine(response.Status == Statuses.Ok ? response.EchoString : "Bad status.");
            }
        }
    }
}
