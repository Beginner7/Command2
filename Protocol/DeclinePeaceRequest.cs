using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class DeclinePeaceRequest : Request
    {
        public DeclinePeaceRequest()
        {
            Command = "declinepeace";
        }

        public string From;
        public int GameID;
    }
}
