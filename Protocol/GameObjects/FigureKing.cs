using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class FigureKing : Figure
    {
        public FigureKing(Side side) : base(side)
        {
            symbol = 'K';
        }
    }
}