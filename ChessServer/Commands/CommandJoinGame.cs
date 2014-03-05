using System.Collections.Concurrent;
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

            if (Server.Games[workRequest.GameID].Act == Act.Cancled)
            {
                workResponse.Status = Statuses.GameCancled;
                return workResponse;
            }

            if (Server.Games[workRequest.GameID].PlayerBlack == null)
            {
                Server.Games[workRequest.GameID].Act = Act.InProgress;
                Server.Games[workRequest.GameID].PlayerBlack = workRequest.NewPlayer;
                Server.Games[workRequest.GameID].PlayerWhite.Messages.Add(MessageSender.OpponentJoinedGame());
                User geted;
                if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerWhite.Name, out geted))
                {
                    geted.Messages.Add(MessageSender.OpponentJoinedGame());
                }
                workResponse.Status = Statuses.Ok;
            }
            else
            {
                if (Server.Games[workRequest.GameID].PlayerWhite == null)
                {
                    Server.Games[workRequest.GameID].Act = Act.InProgress;
                    Server.Games[workRequest.GameID].PlayerWhite = workRequest.NewPlayer;
                    User geted;
                    if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentJoinedGame());
                    }
                    workResponse.Status = Statuses.Ok;
                }
                else
                {
                    workResponse.Status = Statuses.GameIsRunning;
                }
            }
            return workResponse;
        }
    }
}
