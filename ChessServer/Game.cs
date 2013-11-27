using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;

namespace ChessServer
{
    public class Game
    {
        public readonly int GameID;
        public User PlayerOne;
        public User PlayerTwo;
        public DateTime TimeCreateGame; //время создания игры
        public DateTime TimeStartGame; //время начала игры
        public static int GameIDSeq = 0;
        public Game(User user)
        {
            GameID = ++GameIDSeq;
            PlayerOne = user;
        }
    }
}
