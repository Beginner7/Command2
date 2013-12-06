using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol
{
    class FigureRook : Figure
    {
        public FigureRook(Side side) : base(side)
        {
            symbol = 'R';
        }
    }
}