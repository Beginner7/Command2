using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.GameObjects
{
    public class WrongMoveException : Exception
    {
        public WrongMoveException(string message) : base(message) { }
    }
}
