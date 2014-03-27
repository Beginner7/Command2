using System;
using System.Linq;
using Protocol;
using Protocol.Transport;
using Protocol.GameObjects;
using ChessConsole.Commands;

namespace ChessConsole
{
    public static class CommandPromt
    {
        private static bool isStuffDone;
        public static bool IsContinue = true;

        public static void CommandProcess()
        {
            while (IsContinue)
            {
                isStuffDone = false;
                Console.Write("> ");
                var commandInput = Console.ReadLine();
                
                if (CurrentUser.NeedPeaseAnswer)
                {
                    if (!string.IsNullOrWhiteSpace(commandInput))
                    {
                        switch (commandInput.ToLower())
                        {
                            case "yes":
                                if (Utils.IsInGame())
                                {
                                    var request = new AcceptPeaceRequest
                                    {
                                        From = CurrentUser.Name,
                                        GameID = CurrentUser.CurrentGame.Value
                                    };
                                    var response = ServerProvider.MakeRequest<AcceptPeaceResponse>(request);
                                    if (response.Status == Statuses.Ok)
                                    {
                                        Console.WriteLine("You accept peace.");
                                        CurrentUser.NeedPeaseAnswer = false;
                                        CurrentUser.NeedPawnPromotion = false;
                                        CurrentUser.CurrentGame = null;
                                    }
                                }
                                break;
                            case "no":
                                if (Utils.IsInGame())
                                {
                                    var request = new DeclinePeaceRequest
                                    {
                                        From = CurrentUser.Name,
                                        GameID = CurrentUser.CurrentGame.Value
                                    };
                                    var response = ServerProvider.MakeRequest<DeclinePeaceResponse>(request);
                                    if (response.Status == Statuses.Ok)
                                    {
                                        Console.WriteLine("You decline peace.");
                                        CurrentUser.NeedPeaseAnswer = false;
                                    }
                                }
                                break;
                            default:
                                Console.WriteLine("Wrong answer!");
                                Console.WriteLine("You argee? (yes/no):");
                                break;
                        }
                    }
                    isStuffDone = true;
                }
                if (!isStuffDone && CurrentUser.NeedPawnPromotion && !CurrentUser.NeedPeaseAnswer)
                {
                    if (!string.IsNullOrWhiteSpace(commandInput))
                    {
                        if (commandInput.Length == 1 && "rnbqc".IndexOf(commandInput.ToLower()) >= 0)
                        {
                            if (commandInput == "c")
                            {
                                Console.WriteLine("Okay then.");
                                CurrentUser.NeedPawnPromotion = false;
                            }
                            else
                            {
                                if (Utils.IsInGame())
                                {
                                    var request = new MoveRequest
                                    {
                                        From = CurrentUser.LastMove.From,
                                        To = CurrentUser.LastMove.To,
                                        InWhom = commandInput[0].ToString(),
                                        Player = CurrentUser.Name,
                                        GameId = CurrentUser.CurrentGame.Value
                                    };
                                    var response = ServerProvider.MakeRequest(request);
                                    switch (response.Status)
                                    {
                                        case Statuses.Ok:
                                            Console.WriteLine("Move done.");
                                            CurrentUser.LastMove = new Move
                                            {
                                                From = request.From,
                                                InWhom = null,
                                                Player = CurrentUser.Name,
                                                To = request.From
                                            };
                                            CurrentUser.NeedPawnPromotion = false;
                                            break;
                                        case Statuses.WrongMove:
                                            Console.WriteLine("Wrong move.");
                                            break;
                                        default:
                                            Console.WriteLine("Wrong status.");
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong choise!");
                            Console.WriteLine("Your choise? (r - rook, n - knight, b - bishop, q - queen, c - cancel):");
                        }
                    }
                    isStuffDone = true;
                }
                if (!(isStuffDone || CurrentUser.NeedPawnPromotion || CurrentUser.NeedPeaseAnswer))
                {
                    while (commandInput.IndexOf("  ") >= 0)
                    {
                        commandInput = commandInput.Replace("  ", " ");
                    }
                    if (commandInput[0] == ' ')
                    {
                        commandInput = commandInput.Substring(1);
                    }
                    if (commandInput[commandInput.Length - 1] == ' ')
                    {
                        commandInput = commandInput.Substring(0, commandInput.Length - 1);
                    }
                    if (!String.IsNullOrWhiteSpace(commandInput))
                    {
                        var commandWords = commandInput.Split(' ');
                        var isKnownCommand = false;
                        foreach (var element in CommandFactory.Instance.AllCommands)
                        {
                            if (!String.IsNullOrWhiteSpace(commandWords[0]))
                            {
                                if (commandWords[0].ToLower() == element.Help.Name)
                                {
                                    element.DoWork(commandWords.Skip(1));
                                    isKnownCommand = true;
                                }
                            }
                        }
                        if (!isKnownCommand)
                        {
                            Console.WriteLine("Unknown command: '" + commandWords[0] + '\'');
                        }
                    }
                }
            }
        }
    }
}