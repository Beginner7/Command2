using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace Protocol
{
    public class GameStatResponse : Response
    {
        public int ID;
        public string PlayerWhite;
        public string PlayerBlack;
        public Side Turn;
        public Act Act;
    }
}
