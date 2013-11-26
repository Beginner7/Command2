using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public class EchoProvider
    {
        public string Echo(string InputString)
        {
            var command = new EchoRequest();
            command.EchoString = InputString;
            var response = ServerProvider.MakeRequest<EchoResponse>(command);
            return response.EchoString;
        }
    }
}
