using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class MoveVariantsRequest : Request
    {
        public const string command = "moveVariants";

        public MoveVariantsRequest()
        {
            Command = command;
        }

        public string Cell;
    }
}
