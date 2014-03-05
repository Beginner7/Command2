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
                        Utils.Print("Opponent Surrendered");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.YouLoose:
                        Utils.Print("Check and Mate! You loose.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.YouWin:
                        Utils.Print("Check and Mate! You win!");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.Pat:
                        Utils.Print("Pat! Game is over.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.GameDraw:
                        Utils.Print("Game draw.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.OpponentAcceptedPeace:
                        Utils.Print("Opponent accepted peace. Game is over.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.OpponentDeclinedPeace:
                        Utils.Print("Opponent declined peace.");
                        break;

                    case MessageType.OpponentRequestPeace:
                        Utils.Print("Opponent request peace.");
                        Utils.Print("You argee? (yes/no):");
                        CurrentUser.NeedPeaseAnswer = true;
                        break;

                    case MessageType.OpponentJoinedGame:
                        Utils.Print("Opponent joined the game.");
                        break;

                    case MessageType.CheckToOpponent:
                        Utils.Print("You make Check.");
                        break;

                    case MessageType.CheckToYou:
                        Utils.Print("You have Check!");
                        break;

                    case MessageType.OpponentAbandonedGame:
                        Utils.Print("Opponent abandoned the game.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.OpponentLostConnection:
                        Utils.Print("Opponent lost connection.");
                        CurrentUser.CurrentGame = null;
                        break;

                    case MessageType.ChatMessage:
                        Utils.Print(element.Text);
                        break;

                    case MessageType.OpponentMove:
                        Utils.Print("Opponent make move: " + element.Text);
                        break;

                    default:
                        Utils.Print("Unexpected message type.");
                        break;
                }
            }
        }
    }
}
