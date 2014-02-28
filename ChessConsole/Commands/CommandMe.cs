using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessConsole.Commands
{
    public class CommandMe : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("me", "Состояние аккаунта"); } }
        public override int ArgsNeed { get { return 0; } }
        public override void DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
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
}
