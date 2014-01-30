using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.GameObjects;
using ChessConsole.Commands;

namespace ChessConsole
{
    public static class CommandPromt
    {
        private static bool isContinue = true;
        public static void CommandProcess()
        {
            while (isContinue)
            {
                Console.Write('>');
                string commandInput = Console.ReadLine();
                var commandWords = commandInput.Split(' ');
                bool IsStuffDone = false;
                foreach (var element in CommandFactory.Instance.AllCommands)
                {
                    if (!String.IsNullOrWhiteSpace(commandWords[0]))
                    {
                        if (commandWords[0].ToLower() == element.Help.Name)
                        {
                            isContinue = element.DoWork(commandWords.Skip(1));
                            IsStuffDone = true;
                        }
                    }
                }
                if (!IsStuffDone)
                {
                    Console.WriteLine("Unknown command: '" + commandWords[0] + '\'');
                }
            }
        }
    }
}
