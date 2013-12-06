using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol
{
    public class Figure
    {
        public Figure(Side side)
        {
            this.side = side;
        }
        public Side side;
        public char symbol;
    }
}
