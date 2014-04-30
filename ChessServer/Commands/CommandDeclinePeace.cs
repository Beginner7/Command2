using System.Collections.Generic;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandDeclinePeace : CommandBase
    {
        public override string Name { get { return "declinepeace"; } }

        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<PeaceRequest>(request);
            var workResponse = new SurrenderResponse();
            if (Server.Games[workRequest.GameID].Act == Act.WaitingOpponent)
            {
                workResponse.Status = Statuses.NoUser;
                return workResponse;
            }
            if (workRequest.From == Server.Games[workRequest.GameID].PlayerWhite.Name)
            {
                Server.Messages.GetOrAdd(Server.Games[workRequest.GameID].PlayerBlack.Name, i => new List<Message>()).Add(MessageSender.OpponentDeclinedPeace());
                workResponse.Status = Statuses.Ok;
            }
            if (workRequest.From == Server.Games[workRequest.GameID].PlayerBlack.Name)
            {
                Server.Messages.GetOrAdd(Server.Games[workRequest.GameID].PlayerWhite.Name, i => new List<Message>()).Add(MessageSender.OpponentDeclinedPeace());
                workResponse.Status = Statuses.Ok;
            }
            return workResponse;
        }
    }
}
