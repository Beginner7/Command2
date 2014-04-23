using System;

namespace Protocol.Transport
{
    public static class Consts
    {
        public const string GUEST_PREFIX = "#GUEST";

#if (DEBUG)
        public  const string DOMAIN = "http://localhost:2964/";
#else
        public const string DOMAIN = "http://command2.apphb.com/";
#endif
    }
    
   
    public enum Side
    {
        NONE,
        BLACK,
        WHITE
    }

    public enum Act
    {
        WaitingOpponent,
        Canceld,
        InProgress,
        AbandonedByWhite,
        AbandonedByBlack,
        WhiteWon,
        BlackWon,
        Pat,
        Draw,
        Peace,
        WhiteCheck,
        BlackCheck
    }

    [Flags]
    public enum  MoveResult
    {
        SilentMove ,
        Taking ,
        Check ,
        Mate ,
        Pat,
        LongCastling,
        ShortCastling
    }
}

