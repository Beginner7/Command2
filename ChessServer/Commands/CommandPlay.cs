using System.Linq;
using Protocol;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandPlay : CommandBase
    {
        public override string Name { get { return "play"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<PlayRequest>(request);
            var workResponse = new PlayResponse();
            var u = Server.Users.Values.FirstOrDefault(user => user.Name == workRequest.UserName);
            if (u == null)
            {
                workResponse.Status = Statuses.NoUser;
                return workResponse;
            }
            if (Server.PlayersQue.ContainsKey(workRequest.UserName))
            {
                workResponse.Status = Statuses.DuplicateUser;
                return workResponse;
            }
            if (Server.PlayersQue.TryAdd(workRequest.UserName, u))
            {
                workResponse.Status = Statuses.Ok;
                return workResponse;
            }
            workResponse.Status = Statuses.Unknown;
            return workResponse;
        }
    }
}