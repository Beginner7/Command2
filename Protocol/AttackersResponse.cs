using System.Collections.Generic;
using Protocol.GameObjects;

namespace Protocol
{
    public class AttackersResponse : Response
    {
        public List<Figure> Attackers;
        public string BlackKing;
        public string WhiteKing;
    }
}
