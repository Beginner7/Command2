using Protocol;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandDeleteUser : CommandBase
    {
        public override string Name { get { return "deleteuser"; } }
        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<DeleteUserRequest>(request);
            var workResponse = new DeleteUserResponse();
            User removed;
            if (Server.Users.TryRemove(workRequest.UserName, out removed))
            {
                workResponse.Status = Statuses.Ok;
            }
            else
            {
                workResponse.Status = Statuses.DuplicateUser;
            }
            return workResponse;
        }
    }
}
