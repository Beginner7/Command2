using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class CreateGameRequest: Request
    {
        public CreateGameRequest()
        {
            Command = "creategame";
        }
        public User playerOne;
    }
}
