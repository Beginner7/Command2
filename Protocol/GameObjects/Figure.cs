﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class Figure
    {
        public Figure(Side side)
        {
            Side = side;
        }
        public Side Side;
        public char Symbol;
    }
}
