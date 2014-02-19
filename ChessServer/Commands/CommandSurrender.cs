using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandSurrender : CommandBase
    {
        public override string Name { get { return "surrender"; } }

        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<SurrenderRequest>(request);
            var workResponse = new SurrenderResponse();
            if (games[workRequest.GameID].Act == Act.WaitingOpponent)
            {
                workResponse.Status = Statuses.NoUser;
            }
            else
            {
                if (workRequest.From == games[workRequest.GameID].PlayerWhite.Name)
                {
                    games[workRequest.GameID].Act = Act.BlackWon;
                    User geted;
                    if (users.TryGetValue(games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentSurrendered());
                    }
                    workResponse.Status = Statuses.OK;
                }
                if (workRequest.From == games[workRequest.GameID].PlayerBlack.Name)
                {
                    games[workRequest.GameID].Act = Act.WhiteWon;
                    User geted;
                    if (users.TryGetValue(games[workRequest.GameID].PlayerWhite.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.OpponentSurrendered());
                    }
                    workResponse.Status = Statuses.OK;
                }
            }
            return workResponse;
        }
    }
}
