using System;
using System.Collections.Generic;
using System.Linq;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandLogin : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("login", "Вход а аккаунт", "<user name>"); } }
        public override int ArgsNeed { get { return 1; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsNotLoggedIn())
                {
                    var request = new AddUserRequest {UserName = args.ToArray()[0]};
                    var response = ServerProvider.MakeRequest(request);
                    if (response.Status == Statuses.Ok)
                    {
                        CurrentUser.Name = args.ToArray()[0];
                        CurrentUser.StartPulse();
                        Console.WriteLine("You logged in as: " + CurrentUser.Name);
                    }
                    else
                    {
                        Console.WriteLine(response.Status == Statuses.DuplicateUser
                            ? "This user already logged in."
                            : "Bad status");
                    }
                }
            }
        }
    }
}
