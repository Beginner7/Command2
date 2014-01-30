using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandDisconnect : CommandBase
    {
        public override string Name { get { return "disconnect"; } }

        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<DisconnectRequest>(request);
            var workResponse = new DisconnectResponse();
            if (games[workRequest.GameID].act == Act.WaitingOpponent)
            {
                games[workRequest.GameID].act = Act.Cancled;
                workResponse.Status = Statuses.OK;
            }
            else
            {
                if (workRequest.User == games[workRequest.GameID].PlayerWhite.Name)
                {
                    games[workRequest.GameID].act = Act.AbandonedByWhite;
                    User geted;
                    if (users.TryGetValue(games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSander.OpponentAbandonedGame());
                    }
                    workResponse.Status = Statuses.OK;
                }
                if (workRequest.User == games[workRequest.GameID].PlayerBlack.Name)
                {
                    games[workRequest.GameID].act = Act.AbandonedByBlack;
                    User geted;
                    if (users.TryGetValue(games[workRequest.GameID].PlayerWhite.Name, out geted))
                    {
                        geted.Messages.Add(MessageSander.OpponentAbandonedGame());
                    }
                    workResponse.Status = Statuses.OK;
                }
            }
            return workResponse;
        }
    }
}
