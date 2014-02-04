using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class FigureKnight : Figure
    {
        public const char SYMBOL = 'N';
        public FigureKnight(Side side) : base(side)
        {
            symbol = SYMBOL;
        }
    }
}