using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace ChessServer.Commands
{
    public class CommandMoveVariants: CommandBase
    {
        public override string Name { get { return "moveVariants"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var moveVariantsRequest = JsonConvert.DeserializeObject<MoveVariantsRequest>(request);
            var moveVariantsResponse = new MoveVariantsResponse();
           
            moveVariantsResponse.Status = Statuses.OK;
            return moveVariantsResponse;
        }
    }
}
