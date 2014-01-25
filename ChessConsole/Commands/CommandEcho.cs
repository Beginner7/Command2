using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandEcho
    {
        public static int ArgsNeed = -1;

        public static void Echo(string echoString)
        {
            var request = new EchoRequest();
            request.EchoString = echoString;
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
    }
}
