using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol
{
    public class FigurePawn : Figure
    {
        public FigurePawn(Side side) : base(side)
        {
            symbol = 'p';
        }
    }
}
