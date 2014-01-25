using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;
using Protocol.GameObjects;

namespace ChessConsole.Commands
{
    public static class CommandMoveList
    {
        public static int ArgsNeed = 0;

        public static List<Move> GetList()
        {
            var request = new MoveListRequest();
            request.Game = CurrentUser.CurrentGame.Value;
            var response = ServerProvider.MakeRequest<MoveListResponse>(request);
            return response.Moves;
        }

        public static void ShowList()
        {
            if (Utils.IsInGame())
            {
                Console.WriteLine("Moves from game \"" + CurrentUser.CurrentGame + "\":");
                foreach (var element in GetList())
                {
                    Console.WriteLine(String.Format("{0}: {1}-{2}", element.Player.Name, element.From, element.To));
                }
            }
        }
    }
}
