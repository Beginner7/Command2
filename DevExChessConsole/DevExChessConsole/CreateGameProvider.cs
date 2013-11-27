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
        public bool Create()
        {
            var command = new CreateGameRequest();
            var response = ServerProvider.MakeRequest(command);
            return response.Status == Statuses.OK;
        }
    }
}
