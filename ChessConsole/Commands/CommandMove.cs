﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole.Commands
{
    public static class CommandMove
    {
        public static int ArgsNeed = 2;

        public static void Move(string from, string to)
        {
            if (Utils.IsInGame())
            {
                var command = new MoveRequest();
                command.From = from;
                command.To = to;
                command.Player = new User { Name = CurrentUser.Name };
                command.GameID = CurrentUser.CurrentGame.Value;
                var response = ServerProvider.MakeRequest(command);
                switch (response.Status)
                {
                    case Statuses.OK:
                        Console.WriteLine("Move done.");
                        break;
                    case Statuses.NoUser:
                        Console.WriteLine("No opponent yet.");
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
}