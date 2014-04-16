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
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<MoveListRequest>(request);
            var workResponse = new MoveListResponse
            {
                Moves = Server.Games[workRequest.Game].Moves,
                Status = Statuses.Ok
            };
            return workResponse;
        }
    }
}
