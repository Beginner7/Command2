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
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var userListRequest = JsonConvert.DeserializeObject<UserListRequest>(request);
            var userListResponse = new UserListResponse();
            userListResponse.Users = users.Keys.ToArray();
            userListResponse.Status = Statuses.OK;
            return userListResponse;
        }
    }
}
