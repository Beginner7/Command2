using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessConsole.Commands
{
    public abstract class CommandBase
    {
        public abstract CommandHelpLabel Help { get; }
        public abstract int ArgsNeed { get; }
        public abstract bool DoWork(IEnumerable<string> args);
    }
}
