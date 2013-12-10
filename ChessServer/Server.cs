﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Protocol;
using Protocol.Transport;
using System.Collections.Concurrent;

namespace ChessServer
{
    public class Server
    {
        public static ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        public static ConcurrentDictionary<int, Game> Games = new ConcurrentDictionary<int, Game>();
        
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
                        var removed = new User();
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

                case "userlist":
                    {
                        var userlistrequest = JsonConvert.DeserializeObject<UserListRequest>(request);
                        var userlistresponse = new UserListResponse();
                        userlistresponse.Users = Users.Keys.ToArray();
                        userlistresponse.Status = Statuses.OK;
                        resp = userlistresponse;
                    }
                    break;

                case "movelist":
                    {
                        var movelistrequest = JsonConvert.DeserializeObject<MoveListRequest>(request);
                        var movelistresponse = new MoveListResponse();
                        foreach (Move e in Games[movelistrequest.Game].Moves)
                        {
                            movelistresponse.Moves.Add(e.Player.Name + ": " + e.From + "-" + e.To);
                        }
                        movelistresponse.Status = Statuses.OK;
                        resp = movelistresponse;
                    }
                    break;

                case "creategame":
                    {
                        var createGameRequaest = JsonConvert.DeserializeObject<CreateGameRequest>(request);
                        var game = new Game(createGameRequaest.playerOne);
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
                            Games[connectToGameRequest.GameID].PlayerBlack = connectToGameRequest.PlayerTwo;
                            connectToGameResponse.Status = Statuses.OK;
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
                        gamestatresponse.PlayerBlack = Games[gamestatrequest.gameID].PlayerBlack.Name;
                        gamestatresponse.PlayerWhite = Games[gamestatrequest.gameID].PlayerWhite.Name;
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

                            Games[moveRequest.GameID].Moves.Add(new Move { From = moveRequest.From, To = moveRequest.To, Player = moveRequest.Player });
                            if (Games[moveRequest.GameID].Turn == Side.WHITE)
                            {
                                Games[moveRequest.GameID].Turn = Side.BLACK;
                            }
                            else
                            {
                                if (Games[moveRequest.GameID].Turn == Side.BLACK)
                                {
                                    Games[moveRequest.GameID].Turn = Side.WHITE;
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
