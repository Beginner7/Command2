using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer
{
    class Game
    {
        public int GameID;
        public User PlayerOne;
        public User PlayerTwo;
        public DateTime TimeCreateGame; //время создания игры
        public DateTime TimeStartGame; //время начала игры
    }
}
