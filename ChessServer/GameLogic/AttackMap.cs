using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;

namespace ChessServer.GameLogic
{
    class AttackMap
    {
        public AttackMap(List<Move> moves)
        {
            Board board = new Board();
            board.InitialPosition();
            
        }
    }
}
