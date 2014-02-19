using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class FigurePawn : Figure
    {
        public const char SYMBOL = 'P';
        public FigurePawn(Side side) : base(side)
        {
            Symbol = SYMBOL;
        }
    }
}
