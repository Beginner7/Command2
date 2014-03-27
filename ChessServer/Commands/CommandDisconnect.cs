using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandDisconnect : CommandBase
    {
        public override string Name { get { return "disconnect"; } }

        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<DisconnectRequest>(request);
            var workResponse = new DisconnectResponse();
            if (Server.Games[workRequest.GameID].Act == Act.WaitingOpponent)
            {
                Server.Games[workRequest.GameID].Act = Act.Canceld;
                workResponse.Status = Statuses.Ok;
            }
            else
            {
                if (workRequest.User == Server.Games[workRequest.GameID].PlayerWhite.Name)
                {
                    Server.Games[workRequest.GameID].Act = Act.AbandonedByWhite;
                    User geted;
                    if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentAbandonedGame());
                    }
                    workResponse.Status = Statuses.Ok;
                }
                if (workRequest.User == Server.Games[workRequest.GameID].PlayerBlack.Name)
                {
                    Server.Games[workRequest.GameID].Act = Act.AbandonedByBlack;
                    User geted;
                    if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerWhite.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentAbandonedGame());
                    }
                    workResponse.Status = Statuses.Ok;
                }
            }
            return workResponse;
        }
    }
}
