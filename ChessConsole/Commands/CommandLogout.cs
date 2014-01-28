using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public class CommandLogout : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("logout", "Выход из аккаунта"); } }
        public override int ArgsNeed { get { return 0; } }

        public void Logout()
        {
            if (Utils.IsNotInGame() && Utils.IsLoggedIn())
            {
                var request = new DeleteUserRequest();
                request.UserName = CurrentUser.Name;
                var response = ServerProvider.MakeRequest(request);
                if (response.Status == Statuses.OK)
                {
                    Console.WriteLine("You logged out.");
                }
                else
                {
                    Console.WriteLine("Bad status");
                }
            }
        }
    }
}
