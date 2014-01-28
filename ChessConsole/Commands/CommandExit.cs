using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.Commands
{
    public class CommandExit : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("exit", "Выйти"); } }
        public override int ArgsNeed { get { return 0; } }

        public bool Exit()
        {
            if (Utils.IsNotInGame())
            {
                var commandLogout = new CommandLogout();
                commandLogout.Logout();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
