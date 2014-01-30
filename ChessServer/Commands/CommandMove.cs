using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Side UserSide(string username, int gameid, ConcurrentDictionary<int, Game> games)
        {
            if ((username == games[gameid].PlayerWhite.Name))
            {
                return Side.WHITE;
            }

            if ((username == games[gameid].PlayerBlack.Name))
            {
                return Side.BLACK;
            }

            if ((username != games[gameid].PlayerWhite.Name) && (username != games[gameid].PlayerBlack.Name))
            {
                return Side.SPECTATOR;
            }

            return Side.NONE;
        }

        public override Response DoWork(string request, ref ConcurrentDictionary<string, User> users, ref ConcurrentDictionary<int, Game> games)
        {
            var workRequest = JsonConvert.DeserializeObject<MoveRequest>(request);
            var workResponse = new MoveResponse();
            if (games.ContainsKey(workRequest.GameID))
            {
                if (!(games[workRequest.GameID].act == Act.WaitingOpponent))
                {
                    if (UserSide(workRequest.Player.Name, workRequest.GameID, games) != games[workRequest.GameID].Turn)
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
                    if (!attackMap[workRequest.To].Contains(attackMap.board[workRequest.From]) || attackMap.board[workRequest.From].side != UserSide(workRequest.Player.Name, workRequest.GameID, games))
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
                            geted.Messages.Add(MessageSander.OpponentMove(workRequest.From, workRequest.To));
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
                                geted.Messages.Add(MessageSander.OpponentMove(workRequest.From, workRequest.To));
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
