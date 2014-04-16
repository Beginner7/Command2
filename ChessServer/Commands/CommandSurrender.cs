using System.Collections.Concurrent;
using System.Collections.Generic;
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
                return workResponse;
            }
            if (workRequest.From == Server.Games[workRequest.GameID].PlayerWhite.name)
            {
                Server.Messages.GetOrAdd(Server.Games[workRequest.GameID].PlayerBlack.name, i => new List<Message>()).Add(MessageSender.OpponentSurrendered());
                workResponse.Status = Statuses.Ok;
            }
            if (workRequest.From == Server.Games[workRequest.GameID].PlayerBlack.name)
            {
                Server.Messages.GetOrAdd(Server.Games[workRequest.GameID].PlayerWhite.name, i => new List<Message>()).Add(MessageSender.OpponentSurrendered());
                workResponse.Status = Statuses.Ok;
            }
            return workResponse;
        }
    }
}
