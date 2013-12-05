using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.game_objects
{
    class Figure
    {
        public Figure(Side side)
        {
            this.side = side;
        }
        public Side side = Side.BLACK;
        public char symbol = 'X';
    }
}
