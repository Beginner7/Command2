using System.Collections.Concurrent;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using Protocol.GameObjects;
using Newtonsoft.Json;

namespace ChessServer.Commands
{
    public class CommandMove : CommandBase
    {
        public override string Name { get { return "move"; } }

        private string SideUser(Side side, int gameid, ConcurrentDictionary<int, Game> games)
        {
            if (side == Side.WHITE)
            {
                return games[gameid].PlayerWhite.Name;
            }

            if (side == Side.BLACK)
            {
                return games[gameid].PlayerBlack.Name;
            }
            
            return null;
        }

        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<MoveRequest>(request);
            var workResponse = new MoveResponse();
            if (games.ContainsKey(workRequest.GameID))
            {
                if (games[workRequest.GameID].Act != Act.WaitingOpponent)
                {
                    var userSide = games[workRequest.GameID].Turn;
                    if (SideUser(userSide, workRequest.GameID, games) != workRequest.Player.Name)
                    {
                        workResponse.Status = Statuses.OpponentTurn;
                        return workResponse;
                    }
                    var moves = games[workRequest.GameID].Moves;
                    var attackMap = new GameLogic.AttackMap(moves);
                    if (!Board.CheckNotation(workRequest.From) || !Board.CheckNotation(workRequest.To))
                    {
                        workResponse.Status = Statuses.WrongMoveNotation;
                        return workResponse;
                    }
                    if (!attackMap[workRequest.To].Contains(attackMap.SourceBoard[workRequest.From]) || attackMap.SourceBoard[workRequest.From].Side != userSide)
                    {
                        workResponse.Status = Statuses.WrongMove;
                        return workResponse;
                    }

                    moves.Add(new Move { From = workRequest.From, To = workRequest.To, Player = workRequest.Player });

                    if (games[workRequest.GameID].Turn == Side.WHITE)
                    {
                        games[workRequest.GameID].Turn = Side.BLACK;
                        User geted;
                        if (users.TryGetValue(games[workRequest.GameID].PlayerBlack.Name, out geted))
                        {
                            geted.Messages.Add(MessageSender.OpponentMove(workRequest.From, workRequest.To));
                        }
                    }
                    else
                    {
                        if (games[workRequest.GameID].Turn == Side.BLACK)
                        {
                            games[workRequest.GameID].Turn = Side.WHITE;
                            User geted;
                            if (users.TryGetValue(games[workRequest.GameID].PlayerWhite.Name, out geted))
                            {
                                geted.Messages.Add(MessageSender.OpponentMove(workRequest.From, workRequest.To));
                            }
                        }
                    }
                    workResponse.Status = Statuses.OK;
                }
                else
                {
                    workResponse.Status = Statuses.NoUser;
                }
            }
            else
            {
                workResponse.Status = Statuses.GameNotFound;
            }
            return workResponse;
        }
    }
}
