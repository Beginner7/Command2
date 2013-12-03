using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.game_objects
{
    class Board
    {
        public Board()
        {
            foreach (Cell element in Cells)
            {
                //element = new Cell();
            }
        }
        private static int BoardSize = 8;
        private Cell[,] Cells = new Cell[BoardSize, BoardSize];

        public void ShowBoard()
        {
        }
    }
}
