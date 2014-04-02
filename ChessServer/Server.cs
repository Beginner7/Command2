using System.Collections.Generic;
using System.Linq;
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
        private static chessEntities _chess = new chessEntities();
        public static ConcurrentDictionary<int, GameObject> Games = new ConcurrentDictionary<int, GameObject>();
        public static ConcurrentDictionary<string, User> PlayersQue = new ConcurrentDictionary<string, User>();
        public static ConcurrentDictionary<int,List<Message>> Messages = new ConcurrentDictionary<int, List<Message>>();
        private static int _userNumber = 1;

        static Server()
        {
            var timer = new Timer();
            timer.Elapsed += PulseChecker;
            timer.Start();
            timer.Interval = 5000;

            var matchMakingTimer = new Timer();
            matchMakingTimer.Elapsed += MatchMaking;
            matchMakingTimer.Start();
            matchMakingTimer.Interval = 10000;
        }

        private static void MatchMaking(object source, ElapsedEventArgs e)
        {
            if (PlayersQue.Count < 2) return;
            var players = PlayersQue.Values.ToArray();
            for (var i = 0; i < PlayersQue.Count/2; i++)
            {
                var game = new GameObject(players[i*2], players[i*2 + 1]) {Act = Act.InProgress};
                if (!Games.TryAdd(game.Id, game)) continue;
                User dummy;
                PlayersQue.TryRemove(players[i*2].Name, out dummy);
                PlayersQue.TryRemove(players[i*2 + 1].Name, out dummy);
                players[i*2].Messages.Add(MessageSender.GameIsReady(game.Id));
                players[i*2 + 1].Messages.Add(MessageSender.GameIsReady(game.Id));
            }
        }

        private static void PulseChecker(object source, ElapsedEventArgs e)
        {
            foreach (var element in _chess.users.Where(user => user.lostbeats == 0))
            {
                if (!element.name.StartsWith(Consts.GUEST_PREFIX))
                {
                    foreach (var elementGame in Games)
                    {
                        if ((elementGame.Value.PlayerWhite.id == element.id))
                        {

                            Messages.GetOrAdd(elementGame.Value.PlayerBlack.id, i => new List<Message>())
                                .Add(MessageSender.OpponentLostConnection());
                            elementGame.Value.Act = Act.AbandonedByWhite;

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
