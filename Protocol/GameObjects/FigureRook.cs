using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class FigureRook : Figure
    {
        public const char SYMBOL = 'R';
        public FigureRook(Side side) : base(side)
        {
            symbol = SYMBOL;
        }
    }
}