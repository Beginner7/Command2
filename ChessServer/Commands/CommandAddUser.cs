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
    public class CommandAddUser : CommandBase
    {
        public override string Name { get { return "adduser"; } }
        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<AddUserRequest>(request);
            var workResponse = new AddUserResponse();
            if (users.TryAdd(workRequest.UserName, new User { Name = workRequest.UserName }))
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
