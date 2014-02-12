using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class MoveVariantsRequest : Request
    {
        public MoveVariantsRequest()
        {
            Command = "moveVariants";
        }

        public string Cell;
    }
}
