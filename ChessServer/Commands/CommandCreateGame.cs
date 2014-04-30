using System.Linq;
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
            User user;
            if (workRequest.NewPlayer == null)
            {
                user = Server.CreateRandomNewUser();
                workRequest.NewPlayer = new User { Name = user.Name };
            }
            else
            {
                user = Server.Users.Values.FirstOrDefault(u => u.Name == workRequest.NewPlayer.Name);
            }
            if (user == null)
            {
                workResponse.Status = Statuses.NoUser;
                return workResponse;
            }
            var game = new GameObject(user) {Act = Act.WaitingOpponent};

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