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
                if (Server.Games[workRequest.GameID].PlayerWhite.Name == workRequest.From)
                {
                    var blackName = Server.Games[workRequest.GameID].PlayerBlack.Name;
                    var geted = Server.Users.Values.FirstOrDefault(user => user.Name == blackName);
                    if (geted != null)
                    {
                        Server.Messages.GetOrAdd(geted.Name, i => new List<Message>()).Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
                    }
                }
                else
                {
                    if (Server.Games[workRequest.GameID].PlayerBlack.Name == workRequest.From)
                    {
                        var whiteName = Server.Games[workRequest.GameID].PlayerWhite.Name;
                        var geted = Server.Users.Values.FirstOrDefault(user => user.Name == whiteName);
                    
                        if (geted != null)
                        {
                            Server.Messages.GetOrAdd(geted.Name, i => new List<Message>()).Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
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