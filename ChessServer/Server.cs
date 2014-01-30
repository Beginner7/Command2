using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Protocol;
using Protocol.GameObjects;
using Protocol.Transport;
using Protocol.Transport.Messages;
using System.Collections.Concurrent;
using System.Timers;

namespace ChessServer
{
    public class Server
    {
        public static ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        public static ConcurrentDictionary<int, Game> Games = new ConcurrentDictionary<int, Game>();

        static Server()
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(PulseChecker);
            timer.Start();
            timer.Interval = 5000;
        }

        private static void PulseChecker(object source, ElapsedEventArgs e)
        {
            foreach (var element in Users)
            {
                if (element.Value.lostbeats > 10)
                {
                    foreach (var elementGame in Games)
                    {
                        if ((elementGame.Value.PlayerWhite.Name == element.Key))
                        {
                            User geted;
                            if (Users.TryGetValue(elementGame.Value.PlayerBlack.Name, out geted))
                            {
                                geted.Messages.Add(MessageSander.OpponentLostConnection());
                                elementGame.Value.act = Act.AbandonedByWhite;
                            }
                        }
                        if ((elementGame.Value.PlayerWhite.Name == element.Key))
                        {
                            User geted;
                            if (Users.TryGetValue(elementGame.Value.PlayerWhite.Name, out geted))
                            {
                                geted.Messages.Add(MessageSander.OpponentLostConnection());
                                elementGame.Value.act = Act.AbandonedByBlack;
                            }
                        }
                    }
                    User removed;
                    Users.TryRemove(element.Value.Name, out removed);
                }
                else
                {
                    element.Value.lostbeats++;
                }
            }
        }

        private Side UserSide(string username, int gameid)
        {
            if ((username == Games[gameid].PlayerWhite.Name))
            {
                return Side.WHITE;
            }

            if ((username == Games[gameid].PlayerBlack.Name))
            {
                return Side.BLACK;
            }

            if ((username != Games[gameid].PlayerWhite.Name) && (username != Games[gameid].PlayerBlack.Name))
            {
                return Side.SPECTATOR;
            }

            return Side.NONE;
        }

        public string ProcessRequest(string request)
        {
            var req = JsonConvert.DeserializeObject<Request>(request);
            var resp = new Response();
            switch (req.Command)
            {
                case "adduser":
                    {
                        var addUserRequest = JsonConvert.DeserializeObject<AddUserRequest>(request);
                        var addUserResponse = new AddUserResponse();
                        if (Users.TryAdd(addUserRequest.UserName, new User { Name = addUserRequest.UserName }))
                        {
                            addUserResponse.Status = Statuses.OK;
                        }
                        else
                        {
                            addUserResponse.Status = Statuses.DuplicateUser;
                        }
                        resp = addUserResponse;
                    }
                    break;

                case "deleteuser":
                    {
                        var deleteUserRequest = JsonConvert.DeserializeObject<DeleteUserRequest>(request);
                        var deleteUserResponse = new DeleteUserResponse();
                        User removed;
                        if (Users.TryRemove(deleteUserRequest.UserName, out removed))
                        {
                            deleteUserResponse.Status = Statuses.OK;
                        }
                        else
                        {
                            deleteUserResponse.Status = Statuses.DuplicateUser;
                        }
                        resp = deleteUserResponse;
                    }
                    break;

                case "disconnect":
                    {
                        var disconnectRequest = JsonConvert.DeserializeObject<DisconnectRequest>(request);
                        var disconnectResponse = new DisconnectResponse();
                        if (Games[disconnectRequest.GameID].PlayerWhite == null ||
                            Games[disconnectRequest.GameID].PlayerBlack == null)
                        {
                            Games[disconnectRequest.GameID].act = Act.Cancled;
                            disconnectResponse.Status = Statuses.OK;
                        }
                        else
                        {
                            if (disconnectRequest.User == Games[disconnectRequest.GameID].PlayerWhite.Name)
                            {
                                Games[disconnectRequest.GameID].act = Act.AbandonedByWhite;
                                User geted;
                                if (Users.TryGetValue(Games[disconnectRequest.GameID].PlayerBlack.Name, out geted))
                                {
                                    geted.Messages.Add(MessageSander.OpponentAbandonedGame());
                                }
                                disconnectResponse.Status = Statuses.OK;
                            }
                            if (disconnectRequest.User == Games[disconnectRequest.GameID].PlayerBlack.Name)
                            {
                                Games[disconnectRequest.GameID].act = Act.AbandonedByBlack;
                                User geted;
                                if (Users.TryGetValue(Games[disconnectRequest.GameID].PlayerWhite.Name, out geted))
                                {
                                    geted.Messages.Add(MessageSander.OpponentAbandonedGame());
                                }
                                disconnectResponse.Status = Statuses.OK;
                            }
                        }
                        resp = disconnectResponse;
                    }
                    break;

                case "pulse":
                    {
                        var pulseRequest = JsonConvert.DeserializeObject<PulseRequest>(request);
                        var pulseResponse = new PulseResponse();
                        User geted;
                        if (Users.TryGetValue(pulseRequest.From, out geted))
                        {
                            pulseResponse.Status = Statuses.OK;
                            geted.lostbeats = 0;
                            if (geted.Messages.Capacity != 0)
                            {
                                foreach (var element in geted.Messages)
                                {
                                    pulseResponse.Messages.Add(element);
                                }
                                geted.Messages.Clear();
                            }
                        }
                        else
                        {
                            pulseResponse.Status = Statuses.NoUser;
                        }
                        resp = pulseResponse;
                    }
                    break;

                case "userlist":
                    {
                        var userListRequest = JsonConvert.DeserializeObject<UserListRequest>(request);
                        var userListResponse = new UserListResponse();
                        userListResponse.Users = Users.Keys.ToArray();
                        userListResponse.Status = Statuses.OK;
                        resp = userListResponse;
                    }
                    break;

                case "chat":
                    {
                        var chatRequest = JsonConvert.DeserializeObject<ChatRequest>(request);
                        var chatResponse = new ChatResponse();
                        if (Games.ContainsKey(chatRequest.GameID))
                        {
                            if (Games[chatRequest.GameID].PlayerWhite.Name == chatRequest.From)
                            {
                                User geted;
                                if (Users.TryGetValue(Games[chatRequest.GameID].PlayerBlack.Name, out geted))
                                {
                                    geted.Messages.Add(MessageSander.ChatMessage(chatRequest.From, chatRequest.SayString));
                                }
                            }
                            else
                            {
                                if (Games[chatRequest.GameID].PlayerBlack.Name == chatRequest.From)
                                {
                                    User geted;
                                    if (Users.TryGetValue(Games[chatRequest.GameID].PlayerWhite.Name, out geted))
                                    {
                                        geted.Messages.Add(MessageSander.ChatMessage(chatRequest.From, chatRequest.SayString));
                                    }
                                }
                            }
                            chatResponse.Status = Statuses.OK;
                        }
                        else
                        {
                            chatResponse.Status = Statuses.GameNotFound;
                        }
                        resp = chatResponse;
                    }
                    break;

                case "movelist":
                    {
                        var moveListRequest = JsonConvert.DeserializeObject<MoveListRequest>(request);
                        var moveListResponse = new MoveListResponse();
                        moveListResponse.Moves = Games[moveListRequest.Game].Moves;
                        moveListResponse.Status = Statuses.OK;
                        resp = moveListResponse;
                    }
                    break;

                case "creategame":
                    {
                        var createGameRequest = JsonConvert.DeserializeObject<CreateGameRequest>(request);
                        var game = new Game(createGameRequest.NewPlayer);
                        game.act = Act.WaitingOpponent;
                        var createGameResponse = new CreateGameResponse();
                        if (Games.TryAdd(game.ID, game))
                        {
                            createGameResponse.ID = game.ID;
                            createGameResponse.Status = Statuses.OK;
                        }
                        else
                            createGameResponse.Status = Statuses.ErrorCreateGame;
                        resp = createGameResponse;
                    }
                    break;

                case "gamelist":
                    {
                        var gameListRequest = JsonConvert.DeserializeObject<GameListRequest>(request);
                        var gameListResponse = new GameListResponse();
                        gameListResponse.Games = Games.Keys.ToArray();
                        gameListResponse.Status = Statuses.OK;
                        resp = gameListResponse;
                    }
                    break;

                case "connecttogame":
                    {
                        var connectToGameRequest = JsonConvert.DeserializeObject<ConnectToGameRequest>(request);
                        var connectToGameResponse = new ConnectToGameResponse();
                        if (Games.Keys.ToArray().Contains(connectToGameRequest.GameID))
                        {
                            if (Games[connectToGameRequest.GameID].PlayerBlack == null)
                            {
                                Games[connectToGameRequest.GameID].act = Act.InProgress;
                                Games[connectToGameRequest.GameID].PlayerBlack = connectToGameRequest.NewPlayer;
                                Games[connectToGameRequest.GameID].PlayerWhite.Messages.Add(MessageSander.OpponentJoinedGame());
                                User geted;
                                if (Users.TryGetValue(Games[connectToGameRequest.GameID].PlayerWhite.Name, out geted))
                                {
                                    geted.Messages.Add(MessageSander.OpponentJoinedGame());
                                }
                                connectToGameResponse.Status = Statuses.OK;
                            }
                            else
                            {
                                if (Games[connectToGameRequest.GameID].PlayerWhite == null)
                                {
                                    Games[connectToGameRequest.GameID].act = Act.InProgress;
                                    Games[connectToGameRequest.GameID].PlayerWhite = connectToGameRequest.NewPlayer;
                                    User geted;
                                    if (Users.TryGetValue(Games[connectToGameRequest.GameID].PlayerBlack.Name, out geted))
                                    {
                                        geted.Messages.Add(MessageSander.OpponentJoinedGame());
                                    }
                                    connectToGameResponse.Status = Statuses.OK;
                                }
                                else
                                {
                                    connectToGameResponse.Status = Statuses.GameIsRunning;
                                }
                            }
                        }
                        else
                        {
                            connectToGameResponse.Status = Statuses.GameNotFound;
                        }
                        resp = connectToGameResponse;
                    }
                    break;

                case "echo":
                    {
                        var echoRequest = JsonConvert.DeserializeObject<EchoRequest>(request);
                        var echoResponse = new EchoResponse();
                        echoResponse.EchoString = echoRequest.EchoString;
                        echoResponse.Status = Statuses.OK;
                        resp = echoResponse;
                    }
                    break;

                case "gamestat":
                    {
                        var gameStatRequest = JsonConvert.DeserializeObject<GameStatRequest>(request);
                        var gameStatResponse = new GameStatResponse();
                        gameStatResponse.ID = gameStatRequest.gameID;
                        gameStatResponse.Act = Games[gameStatRequest.gameID].act;
                        if (Games[gameStatRequest.gameID].PlayerBlack != null)
                        {
                            gameStatResponse.PlayerBlack = Games[gameStatRequest.gameID].PlayerBlack.Name;
                        }
                        if (Games[gameStatRequest.gameID].PlayerWhite != null)
                        {
                            gameStatResponse.PlayerWhite = Games[gameStatRequest.gameID].PlayerWhite.Name;
                        }
                        gameStatResponse.Turn = Games[gameStatRequest.gameID].Turn;
                        gameStatResponse.Status = Statuses.OK;
                        resp = gameStatResponse;
                    }
                    break;

                case "move":
                    {
                        var moveRequest = JsonConvert.DeserializeObject<MoveRequest>(request);
                        var moveResponse = new MoveResponse();
                        if (Games.ContainsKey(moveRequest.GameID))
                        {
                            if (!(Games[moveRequest.GameID].act == Act.WaitingOpponent))
                            {
                                if (UserSide(moveRequest.Player.Name, moveRequest.GameID) != Games[moveRequest.GameID].Turn)
                                {
                                    moveResponse.Status = Statuses.OpponentTurn;
                                    resp = moveResponse;
                                    break;
                                }
                                var moves = Games[moveRequest.GameID].Moves;
                                var attackMap = new GameLogic.AttackMap(moves);
                                if (!Board.CheckNotation(moveRequest.From) || !Board.CheckNotation(moveRequest.To))
                                {
                                    moveResponse.Status = Statuses.WrongMoveNotation;
                                    resp = moveResponse;
                                    break;
                                }
                                if (!attackMap[moveRequest.To].Contains(attackMap.board[moveRequest.From]) || attackMap.board[moveRequest.From].side != UserSide(moveRequest.Player.Name, moveRequest.GameID))
                                {
                                    moveResponse.Status = Statuses.WrongMove;
                                    resp = moveResponse;
                                    break;
                                }

                                moves.Add(new Move { From = moveRequest.From, To = moveRequest.To, Player = moveRequest.Player });

                                if (Games[moveRequest.GameID].Turn == Side.WHITE)
                                {
                                    Games[moveRequest.GameID].Turn = Side.BLACK;
                                    User geted;
                                    if (Users.TryGetValue(Games[moveRequest.GameID].PlayerBlack.Name, out geted))
                                    {
                                        geted.Messages.Add(MessageSander.OpponentMove(moveRequest.From, moveRequest.To));
                                    }
                                }
                                else
                                {
                                    if (Games[moveRequest.GameID].Turn == Side.BLACK)
                                    {
                                        Games[moveRequest.GameID].Turn = Side.WHITE;
                                        User geted;
                                        if (Users.TryGetValue(Games[moveRequest.GameID].PlayerWhite.Name, out geted))
                                        {
                                            geted.Messages.Add(MessageSander.OpponentMove(moveRequest.From, moveRequest.To));
                                        }
                                    }
                                }
                                moveResponse.Status = Statuses.OK;
                            }
                            else
                            {
                                moveResponse.Status = Statuses.NoUser;
                            }
                        }
                        else
                        {
                            moveResponse.Status = Statuses.GameNotFound;
                        }
                        resp = moveResponse;
                    }
                    break;
            }

            resp.RequestCommand = req.Command;

            return JsonConvert.SerializeObject(resp);
        }
    }
}
