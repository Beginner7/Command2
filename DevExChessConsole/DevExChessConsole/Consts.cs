﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole
{
    public static class Consts
    { 
           #if (DEBUG)
           public  const string domain = "http://localhost:2964/";
            #else
          public const string domain = "http://command2.apphb.com/";
            #endif
    }
}
