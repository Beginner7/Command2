using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;

namespace ChessServer.GameLogic
{
    class AttackMap
    {
        private List<Figure>[,] Attackers = new List<Figure>[Board.BoardSize, Board.BoardSize];
        
        public AttackMap(List<Move> moves)
        {
            Board board = new Board();
            board.InitialPosition();
            board.ApplyMoves(moves);
            for (int i = 0; i <  Board.BoardSize ; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    Attackers[i,j]=new List<Figure>();
                }
            }
            for (char i = 'a'; i <= 'h' ; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    Figure f = board[i.ToString() + j];
                    if (f.GetType() == typeof(FigureNone))
                    { 
                        continue; 
                    }
                    if (f.GetType() == typeof(FigureRook))
                    {
                        for (int k = j + 1; k < Board.BoardSize; k++)
                        {
                            Figure f1 = board[i.ToString()+k];
                            if (f1.GetType() == typeof(FigureNone))
                            {
                                Attackers[i - 'a', k].Add(f);
                                continue;
                            }
                            else if (f1.side != f.side)
                            {
                                Attackers[i - 'a', k].Add(f);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
