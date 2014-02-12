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
using ChessServer.Commands;

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
                                geted.Messages.Add(MessageSender.OpponentLostConnection());
                                elementGame.Value.act = Act.AbandonedByWhite;
                            }
                        }
                        if ((elementGame.Value.PlayerWhite.Name == element.Key))
                        {
                            User geted;
                            if (Users.TryGetValue(elementGame.Value.PlayerWhite.Name, out geted))
                            {
                                geted.Messages.Add(MessageSender.OpponentLostConnection());
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

        public string ProcessRequest(string request)
        {
            var req = JsonConvert.DeserializeObject<Request>(request);
            foreach (var element in CommandFactory.Instance.AllCommands)
            {
                if (req.Command == element.Name)
                {
                    return JsonConvert.SerializeObject(element.DoWork(request, ref Users, ref Games));
                }
            }
            return JsonConvert.SerializeObject(new Response() { RequestCommand = req.Command, Status = Statuses.Unknown });
        }
    }
}
