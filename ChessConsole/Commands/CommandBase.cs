using System.Collections.Generic;

namespace ChessConsole.Commands
{
    public abstract class CommandBase
    {
        public abstract CommandHelpLabel Help { get; }
        public abstract int ArgsNeed { get; }
        public abstract void DoWork(IEnumerable<string> args);
    }
}
