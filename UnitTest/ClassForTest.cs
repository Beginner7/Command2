using System.Collections.Generic;
using System.Globalization;
using Protocol.GameObjects;
using ChessServer.GameLogic;

namespace UnitTest
{
    public class ClassForTest
    {
        public Board Board = new Board();
        public List<string> ValidCells = new List<string>();
        public Figure Figure;
        AttackMap _map;// = new AttackMap(new List<Move>(), board);

        public ClassForTest(Figure figure, string cell)
        {
            Board = new Board();
            ValidCells = new List<string>();
            Figure = figure;
            Board[cell] = Figure;
            _map = new AttackMap(new List<Move>(), Board);
         }

        public void MapUpdate()
        {
            _map = new AttackMap(new List<Move>(), Board);
        }

        public bool Check()
        {
            bool f = true;
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString(CultureInfo.InvariantCulture) + j;
                    if (ValidCells.Contains(cell))
                    {
                        f =  _map[cell].Contains(Figure);
                    }
                    else if (!_map[cell].Contains(Figure) && !_map[cell].Contains(Figure))
                    {
                        f = true;
                    }

                }
            }
            return f;
        }
    }
}
