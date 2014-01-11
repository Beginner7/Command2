using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.GameObjects;

namespace Protocol
{
    public class MoveListResponse : Response
    {
        public List<Move> Moves;
    }
}
