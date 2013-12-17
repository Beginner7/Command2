using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;

namespace ChessServer.GameLogic
{
    public class AttackMap
    {
        private List<Figure>[,] Attackers = new List<Figure>[Board.BoardSize, Board.BoardSize];
        public List<Figure> this[string cell]
        {
            set
            {
                Attackers[Board.GetCoords(cell).Item1, Board.GetCoords(cell).Item2] = value;
            }
            get
            {
                return Attackers[Board.GetCoords(cell).Item1, Board.GetCoords(cell).Item2];
            }
        }
        public AttackMap(List<Move> moves, Board forceBoard= null)
        {
            Board board;
            if (forceBoard == null)
            {
                board = new Board();
                board.InitialPosition();
                board.ApplyMoves(moves);
            }
            else
                board=forceBoard;
            for (int i = 0; i <  Board.BoardSize ; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    Attackers[i,j]=new List<Figure>();
                }
            }
            for (char i = 'a'; i <= 'h' ; i++)
            {
                for (int j = 1; j <= Board.BoardSize; j++)
                {
                    Figure f = board[i.ToString() + j];
                    if (f.GetType() == typeof(FigureNone))
                    {
                        continue;
                    }
                    if (f.GetType() == typeof(FigurePawn))
                    {
                        
                        if (j + 1 < Board.BoardSize && j-1>=0)
                        {
                            int k;
                            if (f.side == Side.WHITE)
                            {
                                k = j + 1;
                                Figure f1 = board[i.ToString() + k];
                                if (f1.GetType() == typeof(FigureNone))
                                {
                                    Attackers[i - 'a', k].Add(f1);
                                    if (j == 1) // первый или нет
                                    {
                                        Figure f2 = board[i.ToString() + (k + 1)];
                                        if (f2.GetType() == typeof(FigureNone)) Attackers[i - 'a', k + 1].Add(f2);
                                    }
                                }
                                if (i + 1 <= 'h')
                                {
                                    int l = i + 1;
                                    Figure f2 = board[l.ToString() + k];

                                    if (f2.side != f.side) Attackers[l - 'a', k].Add(f2);
                                }
                                if (i - 1 >= 'a')
                                {
                                    int l = i - 1;
                                    Figure f2 = board[l.ToString() + k];

                                    if (f2.side != f.side) Attackers[l - 'a', k].Add(f2);
                                }
                            }
                            if (f.side == Side.BLACK)
                            {
                                k = j - 1;
                                Figure f1 = board[i.ToString() + k];
                                if (f1.GetType() == typeof(FigureNone))
                                {
                                    Attackers[i - 'a', k].Add(f1);
                                    if (j == Board.BoardSize - 2) // первый или нет
                                    {
                                        Figure f2 = board[i.ToString() + (k + 1)];
                                        if (f2.GetType() == typeof(FigureNone)) Attackers[i - 'a', k - 1].Add(f2);
                                    }
                                }
                                if (i + 1 <= 'h')
                                {
                                    int l = i + 1;
                                    Figure f2 = board[l.ToString() + k];

                                    if (f2.side != f.side) Attackers[l - 'a', k].Add(f2);
                                }
                                if (i - 1 >= 'a')
                                {
                                    int l = i - 1;
                                    Figure f2 = board[l.ToString() + k];

                                    if (f2.side != f.side) Attackers[l - 'a', k].Add(f2);
                                }
                            }
                            
                        }
                    }
                    if (f.GetType() == typeof(FigureRook))
                    {
                        North(board, i, j, f);
                        South(board, i, j, f);
                        East(board, i, j, f);
                        West(board, i, j, f);
                    }

                    if (f.GetType() == typeof(FigureQueen))
                    {
                        North(board, i, j, f);
                        South(board, i, j, f);
                        East(board, i, j, f);
                        West(board, i, j, f);
                        NorthEast(board, i, j, f);
                        SouthEast(board, i, j, f);
                        NorthWest(board, i, j, f);
                        SouthWest(board, i, j, f);
                    }

                    if (f.GetType() == typeof(FigureBishop))
                    {
                        NorthEast(board, i, j, f);
                        SouthEast(board, i, j, f);
                        NorthWest(board, i, j, f);
                        SouthWest(board, i, j, f);
                    }

                    if (f.GetType()==typeof(FigureKnight))
                    {
                        Figure[] f1 = new Figure[] { board[(i + 2).ToString() + (j + 1)],
                                                    board[(i + 2).ToString() + (j - 1)],
                                                    board[(i + 1).ToString() + (j + 2)],
                                                    board[(i + 1).ToString() + (j - 2)],
                                                    board[(i - 2).ToString() + (j + 1)],
                                                    board[(i - 1).ToString() + (j + 2)],
                                                    board[(i - 2).ToString() + (j - 1)],
                                                    board[(i - 1).ToString() + (j - 2)]};

                        if (i + 2 <= 'h')
                        {
                            if (j + 1 < Board.BoardSize)
                                Attackers[(i + 2) - 'a', j + 1].Add(f1[0]);
                            if (j - 1 > 0)
                                Attackers[(i + 2) - 'a', j - 1].Add(f1[1]);
                        }
                        if (i + 1 <= 'h')
                        {
                            if (j + 2 < Board.BoardSize)
                                Attackers[(i + 1) - 'a', j + 1].Add(f1[2]);
                            if (j - 2 > 0)
                                Attackers[(i + 1) - 'a', j - 1].Add(f1[3]);
                        }
                        if (i - 2 >= 'a')
                        {
                            if (j + 1 < Board.BoardSize)
                                Attackers[(i - 2) - 'a', j + 1].Add(f1[4]);
                            if (j - 1 > 0)
                                Attackers[(i - 2) - 'a', j - 1].Add(f1[5]);
                        }
                        if (i - 1 >= 'a')
                        {
                            if (j + 2 < Board.BoardSize)
                                Attackers[(i - 1) - 'a', j + 1].Add(f1[6]);
                            if (j - 2 > 0)
                                Attackers[(i - 1) -'a', j - 1].Add(f1[7]);
                        }
                    }
                }
            }
        }

        private void SouthWest(Board board, char i, int j, Figure f)
        {
            for (char k = i; k > 'a'; k--)
            {
                int l = j;
                Figure f1 = board[k.ToString() + l];
                if (l < Board.BoardSize)
                {
                    if (f1.GetType() == typeof(FigureNone))
                    {
                        Attackers[k - 'a', l].Add(f);
                        l--;
                        continue;
                    }
                    else if (f1.side != f.side)
                    {
                        Attackers[k - 'a', l].Add(f);
                        break;
                    }
                    else
                    {
                        l--;
                        break;
                    }
                }
                else
                    break;
            }
        }

        private void NorthWest(Board board, char i, int j, Figure f)
        {
            for (char k = i; k > 'a'; k--)
            {
                int l = j;
                Figure f1 = board[k.ToString() + l];
                if (l < Board.BoardSize)
                {
                    if (f1.GetType() == typeof(FigureNone))
                    {
                        Attackers[k - 'a', l].Add(f);
                        l++;
                        continue;
                    }
                    else if (f1.side != f.side)
                    {
                        Attackers[k - 'a', l].Add(f);
                        break;
                    }
                    else
                    {
                        l++;
                        break;
                    }
                }
                else
                    break;
            }
        }

        private void SouthEast(Board board, char i, int j, Figure f)
        {
            for (char k = i; k < 'h'; k++)
            {
                int l = j - 1;
                Figure f1 = board[k.ToString() + l];
                if (l < Board.BoardSize)
                {
                    if (f1.GetType() == typeof(FigureNone))
                    {
                        Attackers[k - 'a', l].Add(f);
                        l--;
                        continue;
                    }
                    else if (f1.side != f.side)
                    {
                        Attackers[k - 'a', l].Add(f);
                        break;
                    }
                    else
                    {
                        l--;
                        break;
                    }
                }
                else
                    break;
            }
        }

        private void NorthEast(Board board, char i, int j, Figure f)
        {
            for (char k = i; k < 'h'; k++)
            {
                int l = j + 1;
                Figure f1 = board[k.ToString() + l];
                if (l < Board.BoardSize)
                {
                    if (f1.GetType() == typeof(FigureNone))
                    {
                        Attackers[k - 'a', l].Add(f);
                        l++;
                        continue;
                    }
                    else if (f1.side != f.side)
                    {
                        Attackers[k - 'a', l].Add(f);
                        break;
                    }
                    else
                    {
                        l++;
                        break;
                    }
                }
                else
                    break;
            }
        }

        private void West(Board board, char i, int j, Figure f)
        {
            for (char k = i; k > 'a'; k--)
            {
                Figure f1 = board[k.ToString() + j];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', j].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', j].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        private void East(Board board, char i, int j, Figure f)
        {
            for (char k = i; k < 'h'; k++)
            {
                Figure f1 = board[k.ToString() + j];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', j].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', j].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        private void South(Board board, char i, int j, Figure f)
        {
            for (int k = j - 1; k > 0; k--)
            {
                Figure f1 = board[i.ToString() + k];
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

        private void North(Board board, char i, int j, Figure f)
        {
            for (int k = j + 1; k < Board.BoardSize; k++)
            {
                Figure f1 = board[i.ToString() + k];
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
