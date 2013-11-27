using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;
using System.Text.RegularExpressions;

namespace ChessConsole
{
    class ConnectToGameProvider
    {
        public bool Connect(string game)
        {
            var command = new ConnectToGameRequest();
            var response = ServerProvider.MakeRequest(command);
            Regex rxNums = new Regex(@"^\d+$");
            if (rxNums.IsMatch(game))
                command.GameID = int.Parse(game);
            else 
                return response.Status == Statuses.Unknown;
            return response.Status == Statuses.OK;
        }
    }
}
