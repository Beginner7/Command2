using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandCreateGame : CommandBase
    {
        public override string Name { get { return "creategame"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<CreateGameRequest>(request);
            var workResponse = new CreateGameResponse();
            var game = new Game(workRequest.NewPlayer);
            game.act = Act.WaitingOpponent;
            
            if (games.TryAdd(game.ID, game))
            {
                workResponse.ID = game.ID;
                workResponse.Status = Statuses.OK;
            }
            else
                workResponse.Status = Statuses.ErrorCreateGame;
            return workResponse;
        }
    }
}
