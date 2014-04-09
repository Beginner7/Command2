using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandJoinGame : CommandBase
    {
        public override string Name { get { return "joingame"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<JoinGameRequest>(request);
            var workResponse = new JoinGameResponse();
            if (!Server.Games.Keys.ToArray().Contains(workRequest.GameID))
            {
                workResponse.Status = Statuses.GameNotFound;
                return workResponse;
            }

            if (Server.Games[workRequest.GameID].Act == Act.Canceld)
            {
                workResponse.Status = Statuses.GameCanceld;
                return workResponse;
            }

            user user = Server._chess.users.Where(u => u.name == workRequest.NewPlayer.Name).FirstOrDefault();
            if (user == null)
            {
                workResponse.Status = Statuses.NoUser;
                return workResponse;
            }
            if (Server.Games[workRequest.GameID].PlayerBlack == null)
            {
                Server.Games[workRequest.GameID].Act = Act.InProgress;
                Server.Games[workRequest.GameID].PlayerBlack = user;
                Server.Messages.GetOrAdd(Server.Games[workRequest.GameID].PlayerWhite.name, i => new List<Message>()).Add(MessageSender.OpponentJoinedGame());
                workResponse.Status = Statuses.Ok;
            }
            else if (Server.Games[workRequest.GameID].PlayerWhite == null)
            {
                Server.Games[workRequest.GameID].Act = Act.InProgress;
                Server.Games[workRequest.GameID].PlayerWhite = user; 
                Server.Messages.GetOrAdd(Server.Games[workRequest.GameID].PlayerBlack.name, i => new List<Message>()).Add(MessageSender.OpponentJoinedGame());
                workResponse.Status = Statuses.Ok;
            }
            else
            {
                workResponse.Status = Statuses.GameIsRunning;
            }
            
            return workResponse;
        }
    }
}
