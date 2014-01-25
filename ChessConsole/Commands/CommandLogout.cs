using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandLogout
    {
        public static int ArgsNeed = 0;

        public static void Logout()
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
