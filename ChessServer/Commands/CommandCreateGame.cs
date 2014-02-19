using System.Collections.Concurrent;
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
            var game = new Game(workRequest.NewPlayer) {Act = Act.WaitingOpponent};

            if (games.TryAdd(game.Id, game))
            {
                workResponse.ID = game.Id;
                workResponse.Status = Statuses.OK;
            }
            else
                workResponse.Status = Statuses.ErrorCreateGame;
            return workResponse;
        }
    }
}
