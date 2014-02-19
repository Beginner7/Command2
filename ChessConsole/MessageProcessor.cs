using System;
using System.Collections.Generic;
using Protocol.Transport.Messages;

namespace ChessConsole
{
    public static class MessageProcessor
    {
        public static void Process(List<Message> listOfMessages)
        {
            foreach (var element in listOfMessages)
            {
                switch (element.Type)
                {
                    case MessageType.OpponentSurrendered:
                        Console.WriteLine("Opponent Surrendered");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.YouLoose:
                        Console.WriteLine("Check and Mate! You loose.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.YouWin:
                        Console.WriteLine("Check and Mate! You win!");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.Pat:
                        Console.WriteLine("Pat! Game is over.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.GameDraw:
                        Console.WriteLine("Game draw.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.OpponentAcceptedPeace:
                        Console.WriteLine("Opponent accepted peace. Game is over.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.OpponentDeclinedPeace:
                        Console.WriteLine("Opponent decline peace.");
                        break;

                    case MessageType.OpponentRequestPeace:
                        Console.WriteLine("Opponent request peace.");
                        Console.Write("You accept it? (Yes/No): ");
                        CurrentUser.NeedPeaseAnswer = true;
                        break;

                    case MessageType.OpponentJoinedGame:
                        Console.WriteLine("Opponent joined the game.");
                        break;

                    case MessageType.OpponentAbandonedGame:
                        Console.WriteLine("Opponent abandoned the game.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.OpponentLostConnection:
                        Console.WriteLine("Opponent lost connection.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.ChatMessage:
                        Console.WriteLine(element.Text);
                        break;

                    case MessageType.OpponentMove:
                        Console.WriteLine("Opponent make move: " + element.Text);
                        break;

                    default:
                        Console.WriteLine("Unexpected message.");
                        break;
                }
            }
        }
    }
}
