namespace Protocol.Transport
{
    public static class Consts
    {
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
        Cancled,
        InProgress,
        AbandonedByWhite,
        AbandonedByBlack,
        WhiteWon,
        BlackWon,
        Pat,
        Draw,
        Peace
    }
}
