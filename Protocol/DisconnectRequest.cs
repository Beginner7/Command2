using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class DisconnectRequest: Request
    {
        public DisconnectRequest()
        {
            Command = "disconnect";
        }
        public string User;
        public int GameID;
    }
}
