namespace Protocol.Transport.Messages
{
    public enum MessageType
    {
        OpponentLostConnection = 1,
        OpponentAbandonedGame = 2,
        OpponentJoinedGame = 3,
        ChatMessage = 4,
        OpponentMove = 5,
        YouWin = 6,
        YouLoose = 7,
        Pat = 8,
        OpponentSurrendered = 9,
        GameDraw = 10,
        OpponentRequestPeace = 11,
        OpponentAcceptedPeace = 12,
        OpponentDeclinedPeace = 13
    }
}
