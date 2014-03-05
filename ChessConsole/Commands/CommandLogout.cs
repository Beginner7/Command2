using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandLogout : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("logout", "Выход из аккаунта"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsNotInGame() && Utils.IsLoggedIn())
                {
                    var request = new DeleteUserRequest {UserName = CurrentUser.Name};
                    var response = ServerProvider.MakeRequest(request);
                    Console.WriteLine(response.Status == Statuses.Ok ? "You logged out." : "Bad status");
                }
            }
        }
    }
}
