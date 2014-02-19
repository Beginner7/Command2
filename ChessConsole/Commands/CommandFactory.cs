using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ChessConsole.Commands
{
    public class CommandFactory
    {
        private readonly IList<CommandBase> _commands = new List<CommandBase>();
        private CommandFactory()
        {
            _commands = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(CommandBase).IsAssignableFrom(type))
            .Where(type => !type.IsAbstract)
            .Select(type => (CommandBase) Activator.CreateInstance(type))
            .ToList();
        }
        private static CommandFactory _instance;
        public static CommandFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    var instance = new CommandFactory();
                    Interlocked.CompareExchange(ref _instance, instance, null);
                }
                return _instance;
            }
        }
        public IEnumerable<CommandBase> AllCommands
        {
            get
            {
                return _commands;
            }
        }
    }
}
