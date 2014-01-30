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
    public class CommandJoinGame : CommandBase
    {
        public override string Name { get { return "joingame"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<JoinGameRequest>(request);
            var workResponse = new JoinGameResponse();
            if (games[workRequest.GameID].act == Act.Cancled)
            {
                workResponse.Status = Statuses.GameCancled;
                return workResponse;
            }
            if (games.Keys.ToArray().Contains(workRequest.GameID))
            {
                if (games[workRequest.GameID].PlayerBlack == null)
                {
                    games[workRequest.GameID].act = Act.InProgress;
                    games[workRequest.GameID].PlayerBlack = workRequest.NewPlayer;
                    games[workRequest.GameID].PlayerWhite.Messages.Add(MessageSander.OpponentJoinedGame());
                    User geted;
                    if (users.TryGetValue(games[workRequest.GameID].PlayerWhite.Name, out geted))
                    {
                        geted.Messages.Add(MessageSander.OpponentJoinedGame());
                    }
                    workResponse.Status = Statuses.OK;
                }
                else
                {
                    if (games[workRequest.GameID].PlayerWhite == null)
                    {
                        games[workRequest.GameID].act = Act.InProgress;
                        games[workRequest.GameID].PlayerWhite = workRequest.NewPlayer;
                        User geted;
                        if (users.TryGetValue(games[workRequest.GameID].PlayerBlack.Name, out geted))
                        {
                            geted.Messages.Add(MessageSander.OpponentJoinedGame());
                        }
                        workResponse.Status = Statuses.OK;
                    }
                    else
                    {
                        workResponse.Status = Statuses.GameIsRunning;
                    }
                }
            }
            else
            {
                workResponse.Status = Statuses.GameNotFound;
            }
            return workResponse;
        }
    }
}
