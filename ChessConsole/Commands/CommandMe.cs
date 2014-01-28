using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.Commands
{
    public class CommandMe : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("me", "Состояние аккаунта"); } }
        public override int ArgsNeed { get { return 0; } }

        public void Show()
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
