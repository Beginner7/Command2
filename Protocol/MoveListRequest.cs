using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class MoveListRequest: Request
    {
        public MoveListRequest()
        {
            Command = "movelist";
        }

        public int Game;
    }
}
