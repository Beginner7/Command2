using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class PeaceRequest : Request
    {
        public PeaceRequest()
        {
            Command = "peace";
        }

        public string From;
        public int GameID;
    }
}
