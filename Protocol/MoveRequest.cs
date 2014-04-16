using Protocol.GameObjects;
using Protocol.Transport;

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
       public string Player;
       public int GameId;
       public string InWhom;
       public MoveResult Result;
       public string MovedFigure;
   }
}
