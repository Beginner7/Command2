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
        public static ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        public static ConcurrentDictionary<int, GameObject> Games = new ConcurrentDictionary<int, GameObject>();
        public static ConcurrentDictionary<string, User> PlayersQue = new ConcurrentDictionary<string, User>();
        public static ConcurrentDictionary<string,List<Message>> Messages = new ConcurrentDictionary<string, List<Message>>();
        public static ConcurrentDictionary<string, int> LostBeats = new ConcurrentDictionary<string, int>();
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
                Messages.GetOrAdd(players[i * 2].Name, k => new List<Message>()).Add(MessageSender.GameIsReady(game.Id));
                Messages.GetOrAdd(players[i * 2 + 1].Name, k => new List<Message>()).Add(MessageSender.GameIsReady(game.Id));
            }
        }

        private static void PulseChecker(object source, ElapsedEventArgs e)
        {
            const int MAX_LOSTBEATS = 10;
            foreach (var u in LostBeats)
            {
                LostBeats[u.Key]++;
            }
            foreach (var elementGame in Games)
            {
                if (elementGame.Value.PlayerWhite != null
                    && LostBeats.GetOrAdd(elementGame.Value.PlayerWhite.Name, name => 0) >= MAX_LOSTBEATS)
                {
                    if (elementGame.Value.PlayerBlack != null)
                        Messages.GetOrAdd(elementGame.Value.PlayerBlack.Name, i => new List<Message>())
                            .Add(MessageSender.OpponentLostConnection());
                    elementGame.Value.Act = Act.AbandonedByWhite;
                }
                if (elementGame.Value.PlayerBlack != null
                    && LostBeats.GetOrAdd(elementGame.Value.PlayerBlack.Name, name => 0) >= MAX_LOSTBEATS)
                {
                    if (elementGame.Value.PlayerWhite != null)
                        Messages.GetOrAdd(elementGame.Value.PlayerWhite.Name, i => new List<Message>())
                            .Add(MessageSender.OpponentLostConnection());
                    elementGame.Value.Act = Act.AbandonedByBlack;
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
                user = new User { Name = Consts.GUEST_PREFIX + _userNumber++ };

            } while (!Users.TryAdd(user.Name, user));
            LostBeats.TryAdd(user.Name, 0);
            return user;
        }
    }
}
