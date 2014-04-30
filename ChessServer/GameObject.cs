using System;
using System.Collections.Generic;
using Protocol.GameObjects;
using Protocol.Transport;
using Protocol;
using System.Threading;

namespace ChessServer
{
    public class GameObject
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
        public List<MoveResult> MoveActions = new List<MoveResult>();

        public GameObject(User user)
        {
            Interlocked.Increment(ref GameIdSeq);
            Id = GameIdSeq;
            var rnd = new Random();
            if (rnd.Next(100) < 50)
            {
                PlayerWhite = user;
            }
            else
            {
                PlayerBlack = user;
            }
        }

        public GameObject(User user1, User user2)
        {
            Interlocked.Increment(ref GameIdSeq);
            Id = GameIdSeq;
            var rnd = new Random();
            if (rnd.Next(100) < 50)
            {
                PlayerWhite = user1;
                PlayerBlack = user2;
            }
            else
            {
                PlayerWhite = user2;
                PlayerBlack = user1;
            }
        }
    }
}
