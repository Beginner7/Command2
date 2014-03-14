using System;
using System.Collections.Generic;
using Protocol.GameObjects;
using Protocol.Transport;
using Protocol;
using System.Threading;

namespace ChessServer
{
    public class Game
    {
        public Act Act;
        public readonly int Id;
        public User PlayerWhite;
        public User PlayerBlack;
        public Side Turn = Side.WHITE;
        public DateTime TimeCreateGame = new DateTime(); //время создания игры
        public DateTime TimeStartGame; //время начала игры
        public List<Move> Moves = new List<Move>();
        public static int GameIdSeq = 0;
        public string EatedWhites = "";
        public string EatedBlacks = "";

        public Game(User user)
        {
            Interlocked.Increment(ref GameIdSeq);
            Id = GameIdSeq;
            var _rnd = new Random();
            if (_rnd.Next(0, 2) == 0)
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
