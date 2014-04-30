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
            var user = new User {Name = workRequest.UserName};
            workResponse.Status = Statuses.Ok;
            if (!Server.Users.TryAdd(user.Name, user))
            {
                workResponse.Status = Statuses.DuplicateUser;
            }
            return workResponse;
        }
    }
}
