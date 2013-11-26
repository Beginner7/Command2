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
        public static ConcurrentDictionary<string, Game> Games = new ConcurrentDictionary<string, Game>();
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
                    {
                        var creategamerequaest = JsonConvert.DeserializeObject<CreateGameRequest>(request);
                        var creategameresponse = new CreateGameResponse();
                        Games.TryAdd(creategamerequaest.ID.ToString(), new Game { GameID = Games.Count + 1 });
                        //Games.TryAdd(creategamerequaest.ID.ToString(), new Game { PlayerOne = User });
                        Games.TryAdd(creategamerequaest.ID.ToString(), new Game { TimeCreateGame = DateTime.Now });
                    }
                    break;
                case "connecttogame":
                    {
                        var connecttogamerequest = JsonConvert.DeserializeObject<ConnectToGameRequest>(request);
                        var connecttogameresponse = new ConnectToGameResponse();
                        connecttogameresponse.Games = Games.Keys.ToArray();
                        connecttogameresponse.Status = Statuses.OK;
                        resp = connecttogameresponse;
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
            }

            resp.RequestCommand = req.Command;

            return JsonConvert.SerializeObject(resp);
        }
    }
}
