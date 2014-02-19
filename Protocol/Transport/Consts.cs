namespace Protocol.Transport
{
    public static class Consts
    {
        #if (DEBUG)
        public  const string DOMAIN = "http://localhost:2964/";
        #else
        public const string domain = "http://command2.apphb.com/";
        #endif
    }
    
    public enum Side
    {
        NONE, 
        BLACK,
        WHITE,
        SPECTATOR
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
