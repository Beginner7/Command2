using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class JoinGameRequest: Request
    {
        public JoinGameRequest()
        {
            Command = "joingame";
        }
        public int GameID;
        public User NewPlayer;
    }
}
