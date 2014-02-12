using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class AcceptPeaceRequest : Request
    {
        public AcceptPeaceRequest()
        {
            Command = "acceptpeace";
        }

        public string From;
        public int GameID;
    }
}
