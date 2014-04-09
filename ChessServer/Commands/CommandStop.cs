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
            user user;
            workResponse.Status = Server.PlayersQue.TryRemove(workRequest.UserName, out user) ? Statuses.Ok : Statuses.NoUser;

            return workResponse;
        }
    }
}