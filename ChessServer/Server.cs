using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
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
        internal static chessEntities _chess = new chessEntities();
        public static ConcurrentDictionary<int, GameObject> Games = new ConcurrentDictionary<int, GameObject>();
        public static ConcurrentDictionary<string, user> PlayersQue = new ConcurrentDictionary<string, user>();
        public static ConcurrentDictionary<string, List<Message>> Messages = new ConcurrentDictionary<string, List<Message>>();
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
                user dummy;
                PlayersQue.TryRemove(players[i*2].name, out dummy);
                PlayersQue.TryRemove(players[i*2 + 1].name, out dummy);
                Messages.GetOrAdd(players[i * 2].name, k => new List<Message>()).Add(MessageSender.GameIsReady(game.Id));
                Messages.GetOrAdd(players[i * 2 + 1].name, k => new List<Message>()).Add(MessageSender.GameIsReady(game.Id));
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
                    && LostBeats.GetOrAdd(elementGame.Value.PlayerWhite.name, name => 0) >= MAX_LOSTBEATS)
                {
                    if (elementGame.Value.PlayerBlack != null)
                        Messages.GetOrAdd(elementGame.Value.PlayerBlack.name, i => new List<Message>())
                            .Add(MessageSender.OpponentLostConnection());
                    elementGame.Value.Act = Act.AbandonedByWhite;
                }
                if (elementGame.Value.PlayerBlack != null
                    && LostBeats.GetOrAdd(elementGame.Value.PlayerBlack.name, name => 0) >= MAX_LOSTBEATS)
                {
                    if (elementGame.Value.PlayerWhite != null)
                        Messages.GetOrAdd(elementGame.Value.PlayerWhite.name, i => new List<Message>())
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

        public static user CreateRandomNewUser()
        {
            user user = null;
            bool success = true;
            do
            {
                if (user != null)
                {
                    _chess.users.Remove(user);
                }
                user = new user {name = Consts.GUEST_PREFIX + _userNumber++};
                _chess.users.Add(user);
                try
                {
                    _chess.SaveChanges();
                    success = true;
                }
                catch (EntityException)
                {
                    success = false;
                }
                catch (DbUpdateException)
                {
                    success = false;
                }
            } while (!success);
            LostBeats.TryAdd(user.name, 0);
            return user;
        }
    }
}
