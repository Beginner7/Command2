using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandLogin : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("login", "Вход а аккаунт", "<user name>"); } }
        public override int ArgsNeed { get { return 1; } }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsNotLoggedIn())
                {
                    var request = new AddUserRequest();
                    request.UserName = args.ToArray()[0];
                    var response = ServerProvider.MakeRequest(request);
                    if (response.Status == Statuses.OK)
                    {
                        CurrentUser.Name = args.ToArray()[0];
                        CurrentUser.StartPulse();
                        Console.WriteLine("You logged in as: " + CurrentUser.Name);
                    }
                    else
                    {
                        if (response.Status == Statuses.DuplicateUser)
                        {
                            Console.WriteLine("This user already logged in.");
                        }
                        else
                        {
                            Console.WriteLine("Bad status");
                        }
                    }
                }
            }
            return true;
        }
    }
}
