using Protocol;

namespace ChessServer.Commands
{
    public abstract class CommandBase
    {
        public abstract string Name { get; }
        public abstract Response DoWork(string request);
    }
}
