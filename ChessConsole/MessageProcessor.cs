using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    case MessageType.OpponentJoinedGame:
                        {
                            Console.WriteLine("Opponent joined the game.");
                        }
                        break;

                    case MessageType.OpponentAbandonedGame:
                        {
                            Console.WriteLine("Opponent abandoned the game.");
                            CurrentUser.CurrentGame = null;
                        }
                        break;

                    case MessageType.OpponentLostConnection:
                        {
                            Console.WriteLine("Opponent lost connection.");
                            CurrentUser.CurrentGame = null;
                        }
                        break;

                    case MessageType.ChatMessage:
                        {
                            Console.WriteLine(element.Text);
                        }
                        break;

                    case MessageType.OpponentMove:
                        {
                            Console.WriteLine("Opponent make move: " + element.Text);
                        }
                        break;

                    default:
                        {
                            Console.WriteLine("Unexpected message.");
                        }
                        break;
                }
            }
        }
    }
}
