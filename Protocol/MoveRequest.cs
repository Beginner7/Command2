using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
   public class MoveRequest: Request
   {
       public MoveRequest()
       {
           Command = "move";
       }
       public string From;
       public string To;
       public User Player;
       public int GameID;
    }
}
