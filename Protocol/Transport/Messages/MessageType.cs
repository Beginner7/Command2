namespace Protocol.Transport.Messages
{
    public enum MessageType
    {
        ChatMessage,
        OpponentLostConnection,
        OpponentAbandonedGame,
        OpponentJoinedGame,
        OpponentMove,
        OpponentSurrendered,
        OpponentRequestPeace,
        OpponentAcceptedPeace,
        OpponentDeclinedPeace,
        YouWin,
        YouLoose,
        Pat,
        GameDraw,
        CheckToOpponent,
        CheckToYou,
        GameIsReady
    }
}
