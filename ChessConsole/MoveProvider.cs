using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol.Transport;
using Protocol;

namespace ChessConsole
{
    public class MoveProvider
    {
        public void Move(string from, string to, string player, int gameID)
        {
            var command = new MoveRequest();
            command.From = from;
            command.To = to;
            command.Player = new User { Name = player };
            command.GameID = gameID;
            var response = ServerProvider.MakeRequest(command);
            switch (response.Status)
            {
                case Statuses.OK:
                    Console.WriteLine("Move done.");
                    break;
                case Statuses.OpponentTurn:
                    Console.WriteLine("Now is opponent turn.");
                    break;
                case Statuses.WrongMove:
                    Console.WriteLine("Wrong move.");
                    break;
                case Statuses.WrongMoveNotation:
                    Console.WriteLine("Wrong move notation.");
                    break;
                default:
                    Console.WriteLine("Wrong status.");
                    break;
            }
        }
    }
}
