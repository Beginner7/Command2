using System.Collections.Generic;
using Protocol.GameObjects;
using Protocol.Transport;

namespace Protocol
{
    public class MoveListResponse : Response
    {
        public List<Move> Moves;
    }
}
