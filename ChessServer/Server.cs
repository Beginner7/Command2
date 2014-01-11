﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Protocol;
using Protocol.GameObjects;
using Protocol.Transport;
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
                        var adduserrequest = JsonConvert.DeserializeObject<AddUserRequest>(request);
                        var adduserresponse = new AddUserResponse();
                        if (Users.TryAdd(adduserrequest.UserName, new User { Name = adduserrequest.UserName }))
                        {
                            adduserresponse.Status = Statuses.OK;
                        }
                        else
                        {
                            adduserresponse.Status = Statuses.DuplicateUser;
                        }
                        resp = adduserresponse;
                    }
                    break;

                case "deleteuser":
                    {
                        var deleteuserrequest = JsonConvert.DeserializeObject<DeleteUserRequest>(request);
                        var deleteuserresponse = new DeleteUserResponse();
                        User removed;
                        if (Users.TryRemove(deleteuserrequest.UserName, out removed))
                        {
                            deleteuserresponse.Status = Statuses.OK;
                        }
                        else
                        {
                            deleteuserresponse.Status = Statuses.DuplicateUser;
                        }
                        resp = deleteuserresponse;
                    }
                    break;

                case "pulse":
                    {
                        var pulserequest = JsonConvert.DeserializeObject<PulseRequest>(request);
                        var pulseresponse = new PulseResponse();
                        User geted;
                        if (Users.TryGetValue(pulserequest.From, out geted))
                        {
                            pulseresponse.Status = Statuses.OK;
                            geted.lostbeats = 0;
                            if (geted.Messages.Capacity != 0)
                            {
                                foreach (var element in geted.Messages)
                                {
                                    pulseresponse.Messages.Add(element);
                                }
                                geted.Messages.Clear();
                            }
                        }
                        else
                        {
                            pulseresponse.Status = Statuses.NoUser;
                        }
                        resp = pulseresponse;
                    }
                    break;

                case "userlist":
                    {
                        var userlistrequest = JsonConvert.DeserializeObject<UserListRequest>(request);
                        var userlistresponse = new UserListResponse();
                        userlistresponse.Users = Users.Keys.ToArray();
                        userlistresponse.Status = Statuses.OK;
                        resp = userlistresponse;
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
                                    geted.Messages.Add(chatRequest.From + " says: " + chatRequest.SayString);
                                }
                            }
                            else
                            {
                                if (Games[chatRequest.GameID].PlayerBlack.Name == chatRequest.From)
                                {
                                    User geted;
                                    if (Users.TryGetValue(Games[chatRequest.GameID].PlayerWhite.Name, out geted))
                                    {
                                        geted.Messages.Add(chatRequest.From + " says: " + chatRequest.SayString);
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
                        var movelistrequest = JsonConvert.DeserializeObject<MoveListRequest>(request);
                        var movelistresponse = new MoveListResponse();
                        movelistresponse.Moves = Games[movelistrequest.Game].Moves;
                        movelistresponse.Status = Statuses.OK;
                        resp = movelistresponse;
                    }
                    break;

                case "creategame":
                    {
                        var createGameRequaest = JsonConvert.DeserializeObject<CreateGameRequest>(request);
                        var game = new Game(createGameRequaest.NewPlayer);
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
                                Games[connectToGameRequest.GameID].PlayerBlack = connectToGameRequest.NewPlayer;
                                connectToGameResponse.Status = Statuses.OK;
                            }
                            else
                            {
                                if (Games[connectToGameRequest.GameID].PlayerWhite == null)
                                {
                                    Games[connectToGameRequest.GameID].PlayerWhite = connectToGameRequest.NewPlayer;
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
                        var echorequest = JsonConvert.DeserializeObject<EchoRequest>(request);
                        var echoresponse = new EchoResponse();
                        echoresponse.EchoString = echorequest.EchoString;
                        echoresponse.Status = Statuses.OK;
                        resp = echoresponse;
                    }
                    break;

                case "gamestat":
                    {
                        var gamestatrequest = JsonConvert.DeserializeObject<GameStatRequest>(request);
                        var gamestatresponse = new GameStatResponse();
                        gamestatresponse.ID = gamestatrequest.gameID;
                        if (Games[gamestatrequest.gameID].PlayerBlack != null)
                        {
                            gamestatresponse.PlayerBlack = Games[gamestatrequest.gameID].PlayerBlack.Name;
                        }
                        if (Games[gamestatrequest.gameID].PlayerWhite != null)
                        {
                            gamestatresponse.PlayerWhite = Games[gamestatrequest.gameID].PlayerWhite.Name;
                        }
                        gamestatresponse.Turn = Games[gamestatrequest.gameID].Turn;
                        gamestatresponse.Status = Statuses.OK;
                        resp = gamestatresponse;
                    }
                    break;

                case "move":
                    {
                        var moveRequest = JsonConvert.DeserializeObject<MoveRequest>(request);
                        var moveResponse = new MoveResponse();
                        if (Games.ContainsKey(moveRequest.GameID))
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
                                    geted.Messages.Add("Opponent move: " + moveRequest.From + '-' + moveRequest.To);
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
                                        geted.Messages.Add("Opponent move: " + moveRequest.From + '-' + moveRequest.To);
                                    }
                                }
                            }
                            moveResponse.Status = Statuses.OK;
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
