using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.Commands
{
    public class CommandHelpLabel
    {
        public string Name;
        public string HelpString;
        public string Args;

        public CommandHelpLabel(string name, string helpString)
        {
            Name = name;
            HelpString = helpString;
            Args = null;
        }

        public CommandHelpLabel(string name, string helpString, string args)
        {
            Name = name;
            HelpString = helpString;
            Args = args;
        }
    }
}
