using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.game_objects
{
    class Figure
    {
        public Figure(Side side)
        {
            this.side = side;
        }
        private Side side = Side.BLACK;
        private char symbol = 'X';
    }
}
