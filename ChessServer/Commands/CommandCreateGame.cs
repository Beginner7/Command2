using System;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
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
            user user;
            if (workRequest.NewPlayer == null)
            {
                user = Server.CreateRandomNewUser();
                workRequest.NewPlayer = new User {Name = user.name};
            }
            else
            {
                user = Server._chess.users.Where(u => u.name == workRequest.NewPlayer.Name).FirstOrDefault();
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