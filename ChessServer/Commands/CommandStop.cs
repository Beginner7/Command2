using Protocol;
using Protocol.Transport;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandStop : CommandBase
    {
        public override string Name { get { return "stop"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<PlayRequest>(request);
            var workResponse = new PlayResponse();
            User user;
            Server.Users.TryGetValue(workRequest.UserName, out user);
            if (!Server.PlayersQue.ContainsKey(workRequest.UserName))
            {
                workResponse.Status = Statuses.NoUser;
                return workResponse;
            }
            if (user != null)
            {
                if (Server.PlayersQue.TryRemove(workRequest.UserName, out user))
                {
                    workResponse.Status = Statuses.Ok;
                    return workResponse;
                }
            }
            else
            {
                workResponse.Status = Statuses.NoUser;
                return workResponse;
            }
            return workResponse;
        }
    }
}