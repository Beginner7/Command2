using System.Linq;
using Protocol;

namespace ChessServer.Commands
{
    public class CommandUserlist : CommandBase
    {
        public override string Name { get { return "userlist"; } }
        public override Response DoWork(string request)
        {
            var userListResponse = new UserListResponse();
            var user = Server.Users.Values.Select(u => u.Name).ToArray();
            userListResponse.Users = user;
            userListResponse.Status = Statuses.Ok;
            return userListResponse;
        }
    }
}
