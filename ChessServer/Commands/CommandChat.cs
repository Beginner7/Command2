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
                    User geted;
                    if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerBlack.Name, out geted))
                    {
                        geted.Messages.Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
                    }
                }
                else
                {
                    if (Server.Games[workRequest.GameID].PlayerBlack.Name == workRequest.From)
                    {
                        User geted;
                        if (Server.Users.TryGetValue(Server.Games[workRequest.GameID].PlayerWhite.Name, out geted))
                        {
                            geted.Messages.Add(MessageSender.ChatMessage(workRequest.From, workRequest.SayString));
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