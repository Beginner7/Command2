﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class FigureBishop : Figure
    {
        public const char SYMBOL = 'B';
        public FigureBishop(Side side) : base(side)
        {
            Symbol = SYMBOL;
        }
    }
}