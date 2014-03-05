using System;
using System.Globalization;
using Newtonsoft.Json;
using Protocol;
using Protocol.Transport;
using Protocol.Transport.Messages;
using System.Collections.Concurrent;
using System.Timers;
using ChessServer.Commands;

namespace ChessServer
{
    public class Server
    {
        public static ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        public static ConcurrentDictionary<int, Game> Games = new ConcurrentDictionary<int, Game>();
        private static int _userNumber = 1;

        static Server()
        {
            var timer = new Timer();
            timer.Elapsed += PulseChecker;
            timer.Start();
            timer.Interval = 5000;
        }

        private static void PulseChecker(object source, ElapsedEventArgs e)
        {
            foreach (var element in Users)
            {
                if (element.Value.Lostbeats > 10)
                {
                    foreach (var elementGame in Games)
                    {
                        if ((elementGame.Value.PlayerWhite.Name == element.Key))
                        {
                            User geted;
                            if (Users.TryGetValue(elementGame.Value.PlayerBlack.Name, out geted))
                            {
                                geted.Messages.Add(MessageSender.OpponentLostConnection());
                                elementGame.Value.Act = Act.AbandonedByWhite;
                            }
                        }
                        if ((elementGame.Value.PlayerWhite.Name == element.Key))
                        {
                            User geted;
                            if (Users.TryGetValue(elementGame.Value.PlayerWhite.Name, out geted))
                            {
                                geted.Messages.Add(MessageSender.OpponentLostConnection());
                                elementGame.Value.Act = Act.AbandonedByBlack;
                            }
                        }
                    }
                    User removed;
                    Users.TryRemove(element.Value.Name, out removed);
                }
                else
                {
                    element.Value.Lostbeats++;
                }
            }
        }

        public string ProcessRequest(string request)
        {
            var req = JsonConvert.DeserializeObject<Request>(request);
            foreach (var element in CommandFactory.Instance.AllCommands)
            {
                if (req.Command == element.Name)
                {
                    return JsonConvert.SerializeObject(element.DoWork(request));
                }
            }
            return JsonConvert.SerializeObject(new Response { RequestCommand = req.Command, Status = Statuses.Unknown });
        }

        public static User CreateRandomNewUser()
        {
            User user;
            do
            {              
                user = new User {Name = Consts.GUEST_PREFIX + _userNumber++};
            } while (!Users.TryAdd(user.Name,user));
            return user;
        }
    }
}
