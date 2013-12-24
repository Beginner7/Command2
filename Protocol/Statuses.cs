﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    public enum Statuses
    {
        Unknown,
        OK,
        DuplicateUser,
        NoUser,
        ErrorCreateGame,
        GameNotFound,
        OpponentTurn,
        GameIsRunning,
        WrongMove
    }
}
