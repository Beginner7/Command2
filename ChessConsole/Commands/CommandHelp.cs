using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessConsole.Commands
{
    public class CommandHelp : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("help", "Помощь по коммандам (Эта самая)"); } }
        public override int ArgsNeed { get { return 0; } }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
            {
                int longestStringLength = 0;
                foreach (var element in CommandFactory.Instance.AllCommands)
                {
                    if (element.Help.Name.Length + ((element.Help.Args != null) ? element.Help.Args.Length : 0) > longestStringLength)
                    {
                        longestStringLength = element.Help.Name.Length + ((element.Help.Args != null) ? element.Help.Args.Length : 0);
                    }
                }

                foreach (var element in CommandFactory.Instance.AllCommands)
                {
                    Console.Write(element.Help.Name);
                    if (element.Help.Args != null)
                    {
                        Console.Write(' ' + element.Help.Args);
                    }
                    for (int i = 0; i < 1 + longestStringLength - (element.Help.Name.Length + ((element.Help.Args != null) ? element.Help.Args.Length + 1 : 0)); i++)
                    {
                        Console.Write(' ');
                    }
                    Console.Write(" - ");
                    Console.WriteLine(element.Help.HelpString);
                }
            }
            return true;
        }
    }
}
