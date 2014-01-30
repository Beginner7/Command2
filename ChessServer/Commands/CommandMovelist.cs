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
    public class CommandMovelist : CommandBase
    {
        public override string Name { get { return "movelist"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<MoveListRequest>(request);
            var workResponse = new MoveListResponse();
            workResponse.Moves = games[workRequest.Game].Moves;
            workResponse.Status = Statuses.OK;
            return workResponse;
        }
    }
}
