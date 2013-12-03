using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    public class CreateGameProvider
    {
        public int? Create(string userName)
        {
            var command = new CreateGameRequest();
            command.playerOne = new User { Name = userName };
            var response = ServerProvider.MakeRequest<CreateGameResponse>(command);
            return response.Status == Statuses.OK ? (int?)response.ID : null;
        }
    }
}
