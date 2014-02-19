﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class FigureNone : Figure
    {
        public const char SYMBOL = 'X';
        public FigureNone(Side side) : base(side)
        {
            Symbol = SYMBOL;
        }
    }
}