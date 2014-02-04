using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class FigureQueen : Figure
    {
        public const char SYMBOL = 'Q';
        public FigureQueen(Side side) : base(side)
        {
            symbol = SYMBOL;
        }
    }
}