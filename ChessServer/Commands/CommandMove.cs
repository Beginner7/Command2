using System.Collections.Generic;
using System.Linq;
using ChessServer.GameLogic;
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

        private static string SideUser(Side side, GameObject game)
        {
            return side == Side.WHITE ? game.PlayerWhite.Name : (side == Side.BLACK ? game.PlayerBlack.Name : null);
        }

        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<MoveRequest>(request);
            var workResponse = new MoveResponse();

            var whitePlayerName = Server.Games[workRequest.GameId].PlayerWhite.Name;
            var whitePlayer = Server.Users.Values.FirstOrDefault(user => user.Name == whitePlayerName);
            var blackPlayerName = Server.Games[workRequest.GameId].PlayerBlack.Name;
            var blackPlayer = Server.Users.Values.FirstOrDefault(user => user.Name == blackPlayerName);
            GameObject currentGame;
            Server.Games.TryGetValue(workRequest.GameId, out currentGame);

            if (currentGame == null)
            {
                workResponse.Status = Statuses.GameNotFound;
                return workResponse;
            }

            if (whitePlayer == null || blackPlayer == null)
            {
                workResponse.Status = Statuses.NotAuthorized;
                return workResponse;
            }

            if (currentGame.Act == Act.WaitingOpponent)
            {
                workResponse.Status = Statuses.NoUser;
                return workResponse;
            }

            if (!Board.CheckNotation(workRequest.From) || !Board.CheckNotation(workRequest.To))
            {
                workResponse.Status = Statuses.WrongMoveNotation;
                return workResponse;
            }

            var userSide = currentGame.Turn;
            if (SideUser(userSide, currentGame) != workRequest.Player)
            {
                workResponse.Status = Statuses.OpponentTurn;
                return workResponse;
            }

            var moves = currentGame.Moves;
            var attackMap = new AttackMap(moves);

            if (attackMap.SourceBoard[workRequest.From].Side != userSide || !attackMap[workRequest.To].Contains(attackMap.SourceBoard[workRequest.From]))
            {
                workResponse.Status = Statuses.WrongMove;
                return workResponse;
            }

            var fakeMoves = moves.ToList();
            fakeMoves.Add(new Move { From = workRequest.From, To = workRequest.To, Player = workRequest.Player, InWhom = workRequest.InWhom });
            var fakeAttackMap = new AttackMap(fakeMoves);

            if (fakeAttackMap.SourceBoard.IsNeedPawnPromotion)
            {
                workResponse.Status = Statuses.NeedPawnPromotion;
                return workResponse;
            }

            if (fakeAttackMap.IsCheckBlack)
            {
                if (fakeAttackMap.IsMateBlack)
                {
                    currentGame.Act = Act.WhiteWon;
                    Server.Messages.GetOrAdd(blackPlayer.Name, i => new List<Message>()).Add(MessageSender.YouLoose());
                    Server.Messages.GetOrAdd(whitePlayer.Name, i => new List<Message>()).Add(MessageSender.YouWin());
                }
                else
                {
                    currentGame.Act = Act.BlackCheck;
                    Server.Messages.GetOrAdd(blackPlayer.Name, i => new List<Message>()).Add(MessageSender.CheckToYou());
                    Server.Messages.GetOrAdd(whitePlayer.Name, i => new List<Message>()).Add(MessageSender.CheckToOpponent());
                }
            } else if (currentGame.Act == Act.BlackCheck)
            {
                currentGame.Act = Act.InProgress;
            }

            if (fakeAttackMap.IsCheckWhite)
            {
                if (fakeAttackMap.IsMateWhite)
                {
                    currentGame.Act = Act.BlackWon;
                    Server.Messages.GetOrAdd(blackPlayer.Name, i => new List<Message>()).Add(MessageSender.YouWin());
                    Server.Messages.GetOrAdd(whitePlayer.Name, i => new List<Message>()).Add(MessageSender.YouLoose());
                }
                else
                {
                    currentGame.Act = Act.WhiteCheck;
                    Server.Messages.GetOrAdd(blackPlayer.Name, i => new List<Message>()).Add(MessageSender.CheckToOpponent());
                    Server.Messages.GetOrAdd(whitePlayer.Name, i => new List<Message>()).Add(MessageSender.CheckToYou());
                }
            }
            else if (currentGame.Act == Act.WhiteCheck)
            {
                currentGame.Act = Act.InProgress;
            }

            if (fakeAttackMap.IsPat)
            {
                currentGame.Act = Act.Pat;
                Server.Messages.GetOrAdd(blackPlayer.Name, i => new List<Message>()).Add(MessageSender.Pat());
                Server.Messages.GetOrAdd(whitePlayer.Name, i => new List<Message>()).Add(MessageSender.Pat());
            }

            if (!(attackMap.IsCheck || attackMap.IsPat) && fakeAttackMap.IsDraw)
            {
                currentGame.Act = Act.Draw;
                Server.Messages.GetOrAdd(blackPlayer.Name, i => new List<Message>()).Add(MessageSender.GameDraw());
                Server.Messages.GetOrAdd(whitePlayer.Name, i => new List<Message>()).Add(MessageSender.GameDraw());
            }

            if (attackMap.SourceBoard[workRequest.To].GetType() != typeof(FigureNone))
            {
                if (attackMap.SourceBoard[workRequest.To].Side == Side.WHITE)
                {
                    currentGame.EatedWhites += attackMap.SourceBoard[workRequest.To].Symbol;
                }
                else
                {
                    currentGame.EatedBlacks += attackMap.SourceBoard[workRequest.To].Symbol;
                }
                if (fakeAttackMap.IsCheck )
                {
                    workRequest.Result = MoveResult.Check | MoveResult.Taking;
                }
                else
                {
                    if (fakeAttackMap.IsMateBlack || attackMap.IsMateWhite)
                    {
                        workRequest.Result = MoveResult.Mate | MoveResult.Taking;
                    }
                    else
                    {
                        if (fakeAttackMap.IsPat)
                        {
                            workRequest.Result = MoveResult.Pat | MoveResult.Taking;
                        }
                        else
                        {
                            workRequest.Result = MoveResult.Taking;
                        }
                    }
                }
            }
            else
            {
                if (fakeAttackMap.IsCheck )
                {
                    workRequest.Result = MoveResult.Check | MoveResult.SilentMove;
                }
                else
                {
                    if (fakeAttackMap.IsMateBlack || attackMap.IsMateWhite)
                    {
                        workRequest.Result = MoveResult.Mate | MoveResult.SilentMove;
                    }
                    else
                    {
                        if (fakeAttackMap.IsPat)
                        {
                            workRequest.Result = MoveResult.Pat | MoveResult.SilentMove;
                        }
                        else
                        {
                            workRequest.Result = MoveResult.SilentMove;
                        }
                    }
                }
                if (workRequest.To == "c1" || workRequest.To == "c8")
                {
                    workRequest.Result = workRequest.Result | MoveResult.LongCastling;
                }
                else
                {
                    if (workRequest.To == "g1" || workRequest.To == "g8")
                    {
                        workRequest.Result = workRequest.Result | MoveResult.ShortCastling;
                    }
                }
            }


            if (attackMap.SourceBoard[workRequest.From].GetType() == typeof (FigureBishop))
            {
                workRequest.MovedFigure = "B";
            }
            else
            {
                if (attackMap.SourceBoard[workRequest.From].GetType() == typeof (FigureKing))
                {
                    workRequest.MovedFigure = "K";
                }
                else
                {
                    if (attackMap.SourceBoard[workRequest.From].GetType() == typeof (FigureKnight))
                    {
                        workRequest.MovedFigure = "N";
                    }
                    else
                    {
                        if (attackMap.SourceBoard[workRequest.From].GetType() == typeof(FigurePawn))
                        {
                            workRequest.MovedFigure = "";
                        }
                        else
                        {
                            if (attackMap.SourceBoard[workRequest.From].GetType() == typeof(FigureQueen))
                            {
                                workRequest.MovedFigure = "Q";
                            }
                            else
                            {
                                if (attackMap.SourceBoard[workRequest.From].GetType() == typeof(FigureRook))
                                {
                                    workRequest.MovedFigure = "R";
                                }
                                else
                                {
                                    workRequest.MovedFigure = "";
                                }
                            }
                        }
                    }
                }
            }

            moves.Add(new Move { From = workRequest.From, To = workRequest.To, Player = workRequest.Player, InWhom = workRequest.InWhom, Result = workRequest.Result, MovedFigure = workRequest.MovedFigure });

            if (Server.Games[workRequest.GameId].Turn == Side.WHITE)
            {
                Server.Games[workRequest.GameId].Turn = Side.BLACK;
                Server.Messages.GetOrAdd(blackPlayer.Name, i => new List<Message>()).Add(MessageSender.OpponentMove(workRequest.From, workRequest.To));
            }
            else
            {
                Server.Games[workRequest.GameId].Turn = Side.WHITE;
                Server.Messages.GetOrAdd(whitePlayer.Name, i => new List<Message>()).Add(MessageSender.OpponentMove(workRequest.From, workRequest.To));
            }

            workResponse.Status = Statuses.Ok;
            return workResponse;
        }
    }
}
