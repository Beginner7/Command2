using System.Collections.Generic;
using System.Linq;

namespace ChessConsole.Commands
{
    public class CommandExit : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("exit", "Выйти"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                if (Utils.IsNotInGame())
                {
                    var commandLogout = new CommandLogout();
                    commandLogout.DoWork(args);
                    CommandPromt.IsContinue = false;
                }
            }
        }
    }
}
