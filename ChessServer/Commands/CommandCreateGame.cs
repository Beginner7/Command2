using Protocol;
using Protocol.Transport;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandCreateGame : CommandBase
    {
        public override string Name { get { return "creategame"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<CreateGameRequest>(request);
            var workResponse = new CreateGameResponse();
            if (workRequest.NewPlayer == null)
            {
                workRequest.NewPlayer = Server.CreateRandomNewUser();
            }
            if (!Server.Users.ContainsKey(workRequest.NewPlayer.Name))
            {
                Server.Users.TryAdd(workRequest.NewPlayer.Name, workRequest.NewPlayer);
            }
            var game = new GameObject(workRequest.NewPlayer) {Act = Act.WaitingOpponent};

            if (Server.Games.TryAdd(game.Id, game))
            {
                workResponse.ID = game.Id;
                workResponse.Status = Statuses.Ok;
                workResponse.FirstPlayer = workRequest.NewPlayer;
            }
            else
                workResponse.Status = Statuses.ErrorCreateGame;
            return workResponse;
        }
    }
}