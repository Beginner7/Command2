using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport.Messages;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandChat : CommandBase
    {
        public override string Name { get { return "chat"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<ChatRequest>(request);
            var workResponse = new ChatResponse();
            if (Server.Games.ContainsKey(workRequest.GameID))
            {
                if (Server.Games[workRequest.GameID].PlayerWhite.name == workRequest.From)
                {
                    user geted = Server._chess.users.Where(user => user.name == Server.Games[workRequest.GameID].PlayerBlack.name).FirstOrDefault();
                    if (geted != null)
                    {
                        Server.Messages.GetOrAdd(geted.name, i=> new List<Message>()).Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
                    }
                }
                else
                {
                    if (Server.Games[workRequest.GameID].PlayerBlack.name == workRequest.From)
                    {
                         user geted = Server._chess.users.Where(user => user.name == Server.Games[workRequest.GameID].PlayerWhite.name).FirstOrDefault();
                    
                        if (geted != null)
                        {
                            Server.Messages.GetOrAdd(geted.name, i => new List<Message>()).Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
                        }
                    }
                }
                workResponse.Status = Statuses.Ok;
            }
            else
            {
                workResponse.Status = Statuses.GameNotFound;
            }
            return workResponse;
        }
    }
}