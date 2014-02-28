using System.Collections.Concurrent;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandSurrender : CommandBase
    {
        public override string Name { get { return "surrender"; } }

        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<SurrenderRequest>(request);
            var workResponse = new SurrenderResponse();
            if (Server.Games[workRequest.GameID].Act == Act.WaitingOpponent)
            {
                workResponse.Status = Statuses.NoUser;
            }
            else
            {
                if (workRequest.From == Server.Games[workRequest.GameID].PlayerWhite.Name)
                {
                    Server.Games[workRequest.GameID].Act = Act.BlackWon;
                    User geted;
                    if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentSurrendered());
                    }
                    workResponse.Status = Statuses.Ok;
                }
                if (workRequest.From == Server.Games[workRequest.GameID].PlayerBlack.Name)
                {
                    Server.Games[workRequest.GameID].Act = Act.WhiteWon;
                    User geted;
                    if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerWhite.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentSurrendered());
                    }
                    workResponse.Status = Statuses.Ok;
                }
            }
            return workResponse;
        }
    }
}
