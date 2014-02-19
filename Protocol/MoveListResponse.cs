using System.Collections.Generic;
using Protocol.GameObjects;

namespace Protocol
{
    public class MoveListResponse : Response
    {
        public List<Move> Moves;
    }
}
