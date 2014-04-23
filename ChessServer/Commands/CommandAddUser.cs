using System;
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
            var user = new user {name = workRequest.UserName};
            Server._chess.users.Add(user);
            workResponse.Status = Statuses.Ok;
            try
            {
                Server._chess.SaveChanges();
            }
            catch (Exception)
            {
                workResponse.Status = Statuses.DuplicateUser;
            }
            return workResponse;
        }
    }
}
