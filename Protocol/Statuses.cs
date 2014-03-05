namespace Protocol
{
    public enum Statuses
    {
        Unknown,
        Ok,
        DuplicateUser,
        NoUser,
        NeedPawnPromotion,
        ErrorCreateGame,
        GameNotFound,
        OpponentTurn,
        GameCancled,
        GameIsRunning,
        WrongMove,
        WrongMoveNotation,
        NotAuthorized
    }
}
