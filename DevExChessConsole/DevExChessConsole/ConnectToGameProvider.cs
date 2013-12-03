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
        public bool Connect(string game, string userName, out string message)
        {
            var command = new ConnectToGameRequest();
            Regex rxNums = new Regex(@"^\d+$");
            if (rxNums.IsMatch(game))
                command.GameID = int.Parse(game);
            else
            {
                message = "GameID must be a number";
                return false;
            }
            command.PlayerTwo = new User { Name = userName };
            var response = ServerProvider.MakeRequest(command);
            message = "success";
            return response.Status == Statuses.OK;
        }
    }
}
