﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol
{
    public class FigureNone : Figure
    {
        public FigureNone(Side side) : base(side)
        {
            symbol = 'X';
        }
    }
}