using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandEcho : CommandBase
    {
        public override string Name { get { return "echo"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<EchoRequest>(request);
            var workResponse = new EchoResponse();
            workResponse.EchoString = workRequest.EchoString;
            workResponse.Status = Statuses.OK;
            return workResponse;
        }
    }
}
