using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Newtonsoft.Json;
using Protocol.Transport.Messages;

namespace ChessServer.Commands
{
    public class CommandPulse : CommandBase
    {
        public override string Name { get { return "pulse"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<PulseRequest>(request);
            var workResponse = new PulseResponse();
            Server.LostBeats.AddOrUpdate(workRequest.From, i => 0, (i, cv) => 0);
            workResponse.Status = Statuses.Ok;
            Server.Messages.AddOrUpdate(workRequest.From, s => new List<Message>(), (s, cmessages) =>
            {
                workResponse.Messages = cmessages;
                return new List<Message>();
            });
            return workResponse;
        }
    }
}
