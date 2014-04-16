using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandUserlist : CommandBase
    {
        public override string Name { get { return "userlist"; } }
        public override Response DoWork(string request)
        {
            var userListRequest = JsonConvert.DeserializeObject<UserListRequest>(request);
            var userListResponse = new UserListResponse();
            var user = Server._chess.users.Select(u =>  u.name).ToArray();
            userListResponse.Users = user;
            userListResponse.Status = Statuses.Ok;
            return userListResponse;
        }
    }
}
