using Protocol;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandAddUser : CommandBase
    {
        public override string Name { get { return "adduser"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<AddUserRequest>(request);
            var workResponse = new AddUserResponse();
            workResponse.Status = Server.Users.TryAdd(workRequest.UserName, new User { Name = workRequest.UserName }) ? Statuses.Ok : Statuses.DuplicateUser;
            return workResponse;
        }
    }
}
