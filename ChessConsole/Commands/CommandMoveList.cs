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
    public class CommandMoveList : CommandBase
    {
        public override CommandHelpLabel Help { get { return new CommandHelpLabel("ml", "Показать лог игры"); } }
        public override int ArgsNeed { get { return 0; } }
        public List<Move> GetList()
        {
            var request = new MoveListRequest();
            request.Game = CurrentUser.CurrentGame.Value;
            var response = ServerProvider.MakeRequest<MoveListResponse>(request);
            return response.Moves;
        }
        public override bool DoWork(IEnumerable<string> args)
        {
            if (Utils.CheckArgs(ArgsNeed, args.Count()))
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
            return true;
        }
    }
}
