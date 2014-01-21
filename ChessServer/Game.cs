using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.GameObjects;
using Protocol.Transport;
using Protocol;
using System.Threading;

namespace ChessServer
{
    public class Game
    {
        public Act act;
        public readonly int ID;
        public User PlayerWhite;
        public User PlayerBlack;
        public Side Turn = Side.WHITE;
        public DateTime TimeCreateGame = new DateTime(); //время создания игры
        public DateTime TimeStartGame; //время начала игры
        public List<Move> Moves = new List<Move>();

        public static int GameIDSeq = 0;

        private Random rnd = new Random();

        public Game(User user)
        {
            Interlocked.Increment(ref GameIDSeq);
            ID = GameIDSeq;

            if (rnd.Next(0, 2) == 0)
            {
                PlayerWhite = user;
            }
            else
            {
                PlayerBlack = user;
            }
        }
    }
}
