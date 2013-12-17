﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class ConnectToGameRequest: Request
    {
        public ConnectToGameRequest()
        {
            Command = "connecttogame";
        }
        public int GameID;
        public User NewPlayer;
    }
}
