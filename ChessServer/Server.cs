using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Protocol;
using System.Collections.Concurrent;

namespace ChessServer
{
    public class Server
    {
        public static ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        public static ConcurrentDictionary<int, Game> Games = new ConcurrentDictionary<int, Game>();
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

                case "creategame":
                        var createGameRequaest = JsonConvert.DeserializeObject<CreateGameRequest>(request);
                        var createGameResponse = new CreateGameResponse();
                        var game = new Game(createGameRequaest.playerOne);
                        createGameResponse.Status = Games.TryAdd(game.GameID, game) ? Statuses.OK : Statuses.ErrorCreateGame;
                    break;

                case "gamelist":
                        var gameListRequest = JsonConvert.DeserializeObject<GameListRequest>(request);
                        var gameListResponse = new GameListResponse();
                        gameListResponse.Games = Games.Keys.ToArray();
                        gameListResponse.Status = Statuses.OK;
                        resp = gameListResponse;
                    break;

                case "connecttogame":
                    var connectToGameRequest = JsonConvert.DeserializeObject<ConnectToGameRequest>(request);
                    var connectToGameResponse = new ConnectToGameResponse();
                    if (Games.Keys.ToArray().Contains(connectToGameRequest.GameID))
                    {
                        Games[connectToGameRequest.GameID].PlayerTwo = connectToGameRequest.PlayerTwo;
                        connectToGameResponse.Status = Statuses.OK;
                    }
                    else
                        connectToGameResponse.Status = Statuses.GameNotFound;
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
            }

            resp.RequestCommand = req.Command;

            return JsonConvert.SerializeObject(resp);
        }
    }
}
