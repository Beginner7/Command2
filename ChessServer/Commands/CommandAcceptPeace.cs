﻿using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandAcceptPeace : CommandBase
    {
        public override string Name { get { return "acceptpeace"; } }

        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<PeaceRequest>(request);
            var workResponse = new SurrenderResponse();
            if (Server.Games[workRequest.GameID].Act == Act.WaitingOpponent)
            {
                workResponse.Status = Statuses.NoUser;
            }
            else
            {
                if (workRequest.From == Server.Games[workRequest.GameID].PlayerWhite.Name)
                {
                    User geted;
                    if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentAcceptedPeace());
                    }
                    workResponse.Status = Statuses.Ok;
                }
                if (workRequest.From == Server.Games[workRequest.GameID].PlayerBlack.Name)
                {
                    User geted;
                    if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerWhite.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentAcceptedPeace());
                    }
                    workResponse.Status = Statuses.Ok;
                }
                Server.Games[workRequest.GameID].Act = Act.Peace;
            }
            return workResponse;
        }
    }
}
