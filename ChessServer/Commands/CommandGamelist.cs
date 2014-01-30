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
    public class CommandGamelist : CommandBase
    {
        public override string Name { get { return "gamelist"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<GameListRequest>(request);
            var workResponse = new GameListResponse();
            workResponse.Games = games.Keys.ToArray();
            workResponse.Status = Statuses.OK;
            return workResponse;
        }
    }
}
