using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandChat : CommandBase
    {
        public override string Name { get { return "chat"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<ChatRequest>(request);
            var workResponse = new ChatResponse();
            if (games.ContainsKey(workRequest.GameID))
            {
                if (games[workRequest.GameID].PlayerWhite.Name == workRequest.From)
                {
                    User geted;
                    if (users.TryGetValue(games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
                    }
                }
                else
                {
                    if (games[workRequest.GameID].PlayerBlack.Name == workRequest.From)
                    {
                        User geted;
                        if (users.TryGetValue(games[workRequest.GameID].PlayerWhite.Name, out geted))
                        {
                            geted.Messages.Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
                        }
                    }
                }
                workResponse.Status = Statuses.OK;
            }
            else
            {
                workResponse.Status = Statuses.GameNotFound;
            }
            return workResponse;
        }
    }
}
