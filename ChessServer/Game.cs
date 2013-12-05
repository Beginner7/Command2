﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessServer
{
    public class Game
    {
        public readonly int ID;
        public User PlayerWhite;
        public User PlayerBlack;
        public Side Turn = Side.WHITE;
        public DateTime TimeCreateGame = new DateTime(); //время создания игры
        public DateTime TimeStartGame; //время начала игры
        public static int GameIDSeq = 0;
        public Game(User user)
        {
            ID = ++GameIDSeq;
            PlayerWhite = user;
        }
        public List<Move> Moves = new List<Move>();
    }
}
