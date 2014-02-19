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
    public class CommandPulse : CommandBase
    {
        public override string Name { get { return "pulse"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<PulseRequest>(request);
            var workResponse = new PulseResponse();
            User geted;
            if (users.TryGetValue(workRequest.From, out geted))
            {
                workResponse.Status = Statuses.OK;
                geted.Lostbeats = 0;
                if (geted.Messages.Capacity != 0)
                {
                    foreach (var element in geted.Messages)
                    {
                        workResponse.Messages.Add(element);
                    }
                    geted.Messages.Clear();
                }
            }
            else
            {
                workResponse.Status = Statuses.NoUser;
            }
            return workResponse;
        }
    }
}
