using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.Commands
{
    public static class CommandExit
    {
        public static int ArgsNeed = 0;

        public static bool Exit()
        {
            if (Utils.IsNotInGame())
            {
                CommandLogout.Logout();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
