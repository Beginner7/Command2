using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.GameObjects;
using Protocol.Transport;
using ChessServer.GameLogic;

namespace UnitTest
{
    public class ClassForTest
    {
        public Board board = new Board();
        public List<string> validCells = new List<string>();
        public Figure figure;
        AttackMap map;// = new AttackMap(new List<Move>(), board);

        public ClassForTest(Figure figure, string cell)
        {
            this.board = new Board();
            this.validCells = new List<string>();
            this.figure = figure;
            board[cell] = this.figure;
            map = new AttackMap(new List<Move>(), board);
         }

        public void MapUpdate()
        {
            map = new AttackMap(new List<Move>(), board);
        }

        public bool Check()
        {
            bool f = true;
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        f =  map[cell].Contains(figure)? true : false;
                    }
                    else if (!map[cell].Contains(figure) && !map[cell].Contains(figure))
                    {
                        f = true;
                    }

                }
            }
            return f;
        }
    }
}
