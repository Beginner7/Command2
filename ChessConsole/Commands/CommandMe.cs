using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.Commands
{
    public static class CommandMe
    {
        public static int ArgsNeed = 0;

        public static void Show()
        {
            if (Utils.IsLoggedIn())
            {
                Console.WriteLine("You are logged as: " + CurrentUser.Name);
            }
            if (Utils.IsInGame())
            {
                Console.WriteLine("You are in game. ID: " + CurrentUser.CurrentGame);
            }
        }
    }
}
