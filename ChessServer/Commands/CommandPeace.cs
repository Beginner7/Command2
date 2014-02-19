using System.Collections.Concurrent;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandPeace : CommandBase
    {
        public override string Name { get { return "peace"; } }

        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<PeaceRequest>(request);
            var workResponse = new SurrenderResponse();
            if (games[workRequest.GameID].Act == Act.WaitingOpponent)
            {
                workResponse.Status = Statuses.NoUser;
            }
            else
            {
                if (workRequest.From == games[workRequest.GameID].PlayerWhite.Name)
                {
                    User geted;
                    if (users.TryGetValue(games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentRequestPeace());
                    }
                    workResponse.Status = Statuses.OK;
                }
                if (workRequest.From == games[workRequest.GameID].PlayerBlack.Name)
                {
                    User geted;
                    if (users.TryGetValue(games[workRequest.GameID].PlayerWhite.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentRequestPeace());
                    }
                    workResponse.Status = Statuses.OK;
                }
            }
            return workResponse;
        }
    }
}
