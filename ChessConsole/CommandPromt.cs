using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;
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
                if (CurrentUser.NeedPeaseAnswer)
                {
                    bool IsStuffDone = false;
                    do
                    {
                        if (commandInput.ToLower() == "yes")
                        {
                            var request = new AcceptPeaceRequest();
                            request.From = CurrentUser.Name;
                            request.GameID = CurrentUser.CurrentGame.Value;
                            var response = ServerProvider.MakeRequest<AcceptPeaceResponse>(request);
                            if (response.Status == Statuses.OK)
                            {
                                Console.WriteLine("You accept peace. War is over.");
                                CurrentUser.CurrentGame = null;
                            }
                            CurrentUser.NeedPeaseAnswer = false;
                            IsStuffDone = true;
                        }
                        else
                        {
                            if (commandInput.ToLower() == "no")
                            {
                                var request = new DeclinePeaceRequest();
                                request.From = CurrentUser.Name;
                                request.GameID = CurrentUser.CurrentGame.Value;
                                var response = ServerProvider.MakeRequest<DeclinePeaceResponse>(request);
                                CurrentUser.NeedPeaseAnswer = false;
                                IsStuffDone = true;
                                if (response.Status == Statuses.OK)
                                {
                                    Console.WriteLine("You decline peace. NO MERCY!");
                                    CurrentUser.CurrentGame = null;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Wrong answer!");
                                Console.Write("You accept it? (Yes/No): ");
                            }
                        }
                    }
                    while (!IsStuffDone);
                }
                else
                {
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
}
