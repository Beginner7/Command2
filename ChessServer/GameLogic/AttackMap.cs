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
                    if (f.GetType() == typeof(FigurePawn))
                    {
                        if (j + 1 < Board.BoardSize)
                        {
                            int k = j + 1;
                            Figure f1 = board[i.ToString() + k];
                            if (f1.GetType() == typeof(FigureNone))
                            {
                                Attackers[i - 'a', k].Add(f1);
                            }
                            if (i + 1 <= 'h')
                            {
                                int l = i + 1;
                                Figure f2 = board[l.ToString() + k];

                                if (f2.side != f.side)
                                {
                                    Attackers[l - 'a', k].Add(f2);
                                }
                            }
                            if (i - 1 >= 'a')
                            {
                                int l = i - 1;
                                Figure f2 = board[l.ToString() + k];

                                if (f2.side != f.side)
                                {
                                    Attackers[l - 'a', k].Add(f2);
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
                        Figure f1 = board[(i + 2).ToString() + (j + 1)];
                        if (f1.side != f.side)
                            Attackers[i - 'a', j + 1].Add(f1);
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
