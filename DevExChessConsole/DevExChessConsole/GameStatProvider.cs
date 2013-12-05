using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessConsole
{
    class GameStatProvider
    {
        public GameStatProvider(int gameid)
        {
            var command = new GameStatRequest();
            command.gameID = gameid;
            var response = ServerProvider.MakeRequest<GameStatResponse>(command);
            Console.WriteLine("Game \"" + response.ID + "\" stats:");
            Console.WriteLine("White player: " + response.PlayerWhite);
            Console.WriteLine("Black player: " + response.PlayerBlack);
            Console.WriteLine("Now " + ((response.Turn == Side.BLACK) ? "black's" : "white's") + " tern");
        }
    }
}
