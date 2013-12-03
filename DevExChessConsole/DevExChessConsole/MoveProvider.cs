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
        public bool Move(string from, string to, string player, int gameID)
    {
            var command = new MoveRequest();


            command.From = from;
            command.To = to;
            command.Player = new User { Name = player };
            command.GameID = gameID;
            var response = ServerProvider.MakeRequest(command);
            return response.Status == Statuses.OK;
        
    }
    }
}
