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

        private string SideUser(Side side, Game game)
        {
            if (side == Side.WHITE)
            {
                return game.PlayerWhite.Name;
            }

            if (side == Side.BLACK)
            {
                return game.PlayerBlack.Name;
            }

            return null;
        }

        public override Response DoWork(string request)
        {
            var workRequest = JsonConvert.DeserializeObject<MoveRequest>(request);
            var workResponse = new MoveResponse();

            User whitePlayer;
            Server.Users.TryGetValue(Server.Games[workRequest.GameId].PlayerWhite.Name, out whitePlayer);
            User blackPlayer;
            Server.Users.TryGetValue(Server.Games[workRequest.GameId].PlayerBlack.Name, out blackPlayer);
            Game currentGame;
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
                    blackPlayer.Messages.Add(MessageSender.YouLoose());
                    whitePlayer.Messages.Add(MessageSender.YouWin());
                }
                else
                {
                    blackPlayer.Messages.Add(MessageSender.CheckToYou());
                    whitePlayer.Messages.Add(MessageSender.CheckToOpponent());
                }
            }

            if (fakeAttackMap.IsCheckWhite)
            {
                if (fakeAttackMap.IsMateWhite)
                {
                    currentGame.Act = Act.BlackWon;
                    whitePlayer.Messages.Add(MessageSender.YouLoose());
                    blackPlayer.Messages.Add(MessageSender.YouWin());
                }
                else
                {
                    whitePlayer.Messages.Add(MessageSender.CheckToYou());
                    blackPlayer.Messages.Add(MessageSender.CheckToOpponent());
                }
            }

            if (fakeAttackMap.IsPat)
            {
                currentGame.Act = Act.Pat;
                whitePlayer.Messages.Add(MessageSender.Pat());
                blackPlayer.Messages.Add(MessageSender.Pat());
            }

            if (attackMap.IsCheckWhite)
            {
                currentGame.Act = Act.WhiteCheck;
            }

            if (attackMap.IsCheckBlack)
            {
                currentGame.Act = Act.BlackCheck;
            }


            if (!(attackMap.IsCheck || attackMap.IsPat) && fakeAttackMap.IsDraw)
            {
                currentGame.Act = Act.Draw;
                whitePlayer.Messages.Add(MessageSender.GameDraw());
                blackPlayer.Messages.Add(MessageSender.GameDraw());
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
            }
            moves.Add(new Move { From = workRequest.From, To = workRequest.To, Player = workRequest.Player, InWhom = workRequest.InWhom });

            if (Server.Games[workRequest.GameId].Turn == Side.WHITE)
            {
                Server.Games[workRequest.GameId].Turn = Side.BLACK;
                blackPlayer.Messages.Add(MessageSender.OpponentMove(workRequest.From, workRequest.To));
            }
            else
            {
                Server.Games[workRequest.GameId].Turn = Side.WHITE;
                whitePlayer.Messages.Add(MessageSender.OpponentMove(workRequest.From, workRequest.To));
            }

            workResponse.Status = Statuses.Ok;
            return workResponse;
        }
    }
}
