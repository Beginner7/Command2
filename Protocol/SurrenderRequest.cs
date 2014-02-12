using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class SurrenderRequest : Request
    {
        public SurrenderRequest()
        {
            Command = "surrender";
        }

        public string From;
        public int GameID;
    }
}
