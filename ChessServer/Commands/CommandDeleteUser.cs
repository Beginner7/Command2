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
    public class CommandDeleteUser : CommandBase
    {
        public override string Name { get { return "deleteuser"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<DeleteUserRequest>(request);
            var workResponse = new DeleteUserResponse();
            User removed;
            if (users.TryRemove(workRequest.UserName, out removed))
            {
                workResponse.Status = Statuses.OK;
            }
            else
            {
                workResponse.Status = Statuses.DuplicateUser;
            }
            return workResponse;
        }
    }
}
