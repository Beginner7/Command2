﻿using System;
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
                    if (f.GetType() == typeof(FigureKing))
                    {
                        //char x = (char)(i + 1);
                        //if (x <= 'h')
                        //{
                        //    int y = j + 1;
                        //    if (y <= Board.BoardSize)
                        //    {
                        //        Figure f1 = board[x.ToString() + y];
                        //        if (f1.side != f.side)
                        //            Attackers[x - 'a', y].Add(f1);
                        //    }
                        //    y = j - 1;
                        //    if (y >= 1)
                        //    {
                        //        Figure f1 = board[x.ToString() + y];
                        //        if (f1.side != f.side)
                        //            Attackers[x - 'a', y].Add(f1);
                        //        //Attackers[x - 'a', y].Add(board[((char)(i + 1)).ToString() + (j - 1)]);
                        //    }
                        //}
                        //if ((char)(i + 1) <= 'h')
                        //{
                        //    if (j + 2 < Board.BoardSize)
                        //        Attackers[(char)(i + 1) - 'a', j + 2].Add(board[((char)(i + 1)).ToString() + (j + 2)]);
                        //    if (j - 2 > 0)
                        //        Attackers[(char)(i + 1) - 'a', j - 2].Add(board[((char)(i + 1)).ToString() + (j - 2)]);
                        //}
                        //if ((char)(i - 2) >= 'a')
                        //{
                        //    if (j + 1 < Board.BoardSize)
                        //        Attackers[(char)(i - 2) - 'a', j + 1].Add(board[((char)(i - 2)).ToString() + (j + 1)]);
                        //    if (j - 1 > 0)
                        //        Attackers[(char)(i - 2) - 'a', j - 1].Add(board[((char)(i - 2)).ToString() + (j - 1)]);
                        //}
                        //if ((char)(i - 1) >= 'a')
                        //{
                        //    if (j + 2 < Board.BoardSize)
                        //        Attackers[(char)(i - 1) - 'a', j + 2].Add(board[((char)(i - 1)).ToString() + (j - 2)]);
                        //    if (j - 2 > 0)
                        //        Attackers[(char)(i - 1) - 'a', j - 2].Add(board[((char)(i - 1)).ToString() + (j - 2)]);
                        //}
                        continue;
                    }

                    if (f.GetType() == typeof(FigurePawn))
                    {
                        if (j + 1 <= Board.BoardSize && j - 1 >= 1 )
                        {
                            int k;
                            if (f.side == Side.WHITE)
                            {
                                k = j + 1;
                                Figure f1 = board[i.ToString() + k];
                                if (f1.GetType() != f.GetType())
                                    Attackers[i - 'a', k - 1].Add(f);
                                if (f1.GetType() == typeof(FigureNone))
                                {
                                    if (j == 2) // первый или нет
                                    {
                                        Figure f2 = board[i.ToString() + (k + 1)];
                                        if (f2.GetType() == typeof(FigureNone)) 
                                            Attackers[i - 'a', k].Add(f);
                                    }
                                }
                                if (i + 1 <= 'h')
                                {
                                    char l = (char)(i + 1);
                                    Figure f2 = board[l.ToString() + k];
                                    if (f2.GetType() != typeof(FigureNone))
                                    {
                                        if (f2.side != f.side)
                                            Attackers[l - 'a', k - 1].Add(f);
                                    }
                                }
                                if (i - 1 >= 'a')
                                {
                                    char l = (char)(i - 1);
                                    Figure f2 = board[l.ToString() + k];
                                    if (f2.GetType() != typeof(FigureNone))
                                    {
                                        if (f2.side != f.side)
                                            Attackers[l - 'a', k - 1].Add(f);
                                    }
                                }
                            }
                            if (f.side == Side.BLACK)
                            {
                                k = j - 1;
                                Figure f1 = board[i.ToString() + k];
                                if (f1.GetType() != f.GetType())
                                    Attackers[i - 'a', k - 1].Add(f);
                                if (f1.GetType() == typeof(FigureNone))
                                {
                                    if (j == Board.BoardSize - 1) // первый или нет
                                    {
                                        Figure f2 = board[i.ToString() + (k - 1)];
                                        if (f2.GetType() == typeof(FigureNone)) 
                                            Attackers[i - 'a', k].Add(f);
                                    }
                                }
                                if (i + 1 <= 'h')
                                {
                                    char l = (char)(i + 1);
                                    Figure f2 = board[l.ToString() + k];

                                    if (f2.side != f.side) 
                                        Attackers[l - 'a', k - 1].Add(f);
                                }
                                if (i - 1 >= 'a')
                                {
                                    char l = (char)(i - 1);
                                    Figure f2 = board[l.ToString() + k];

                                    if (f2.side != f.side) 
                                        Attackers[l - 'a', k - 1].Add(f);
                                }
                            }
                            
                        }
                        continue;
                    }
                    if (f.GetType() == typeof(FigureRook))
                    {
                        North(board, i, j, f);
                        South(board, i, j, f);
                        East(board, i, j, f);
                        West(board, i, j, f);
                        continue;
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
                        continue;
                    }

                    if (f.GetType() == typeof(FigureBishop))
                    {
                        NorthEast(board, i, j, f);
                        SouthEast(board, i, j, f);
                        NorthWest(board, i, j, f);
                        SouthWest(board, i, j, f);
                        continue;
                    }

                    if (f.GetType()==typeof(FigureKnight))
                    {
                    //    Figure[] f1 = new Figure[] { board[(i + 2).ToString() + (j + 1)],
                    //                                board[(i + 2).ToString() + (j - 1)],
                    //                                board[(i + 1).ToString() + (j + 2)],
                    //                                board[(i + 1).ToString() + (j - 2)],
                    //                                board[(i - 2).ToString() + (j + 1)],
                    //                                board[(i - 1).ToString() + (j + 2)],
                    //                                board[(i - 2).ToString() + (j - 1)],
                    //                                board[(i - 1).ToString() + (j - 2)]};

                        if ((char)(i + 2) <= 'h')
                        {
                            if (j + 1 <= Board.BoardSize)
                                Attackers[(char)(i + 2) - 'a', j + 1].Add(board[((char)(i + 2)).ToString() + (j + 1)]);
                            if (j - 1 >= 1)
                                Attackers[(char)(i + 2) - 'a', j - 1].Add(board[((char)(i + 2)).ToString() + (j - 1)]);
                        }
                        if ((char)(i + 1) <= 'h')
                        {
                            if (j + 2 < Board.BoardSize)
                                Attackers[(char)(i + 1) - 'a', j + 2].Add(board[((char)(i + 1)).ToString() + (j + 2)]);
                            if (j - 2 > 0)
                                Attackers[(char)(i + 1) - 'a', j - 2].Add(board[((char)(i + 1)).ToString() + (j - 2)]);
                        }
                        if ((char)(i - 2) >= 'a')
                        {
                            if (j + 1 < Board.BoardSize)
                                Attackers[(char)(i - 2) - 'a', j + 1].Add(board[((char)(i - 2)).ToString() + (j + 1)]);
                            if (j - 1 > 0)
                                Attackers[(char)(i - 2) - 'a', j - 1].Add(board[((char)(i - 2)).ToString() + (j - 1)]);
                        }
                        if ((char)(i - 1) >= 'a')
                        {
                            if (j + 2 < Board.BoardSize)
                                Attackers[(char)(i - 1) - 'a', j + 2].Add(board[((char)(i - 1)).ToString() + (j - 2)]);
                            if (j - 2 > 0)
                                Attackers[(char)(i - 1) - 'a', j - 2].Add(board[((char)(i - 1)).ToString() + (j - 2)]);
                        }
                        continue;
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
            for (char k = (char)(i-1); k >= 'a'; k--)
            {
                Figure f1 = board[k.ToString() + j];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', j-1].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', j-1].Add(f);
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
            for (char k = (char)(i+1); k <= 'h'; k++)
            {
                Figure f1 = board[k.ToString() + j];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', j-1].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', j-1].Add(f);
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
            for (int k = j - 1; k >= 1; k--)
            {
                Figure f1 = board[i.ToString() + k];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[i - 'a', k-1].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[i - 'a', k-1].Add(f);
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
            for (int k = j + 1; k <= Board.BoardSize; k++)
            {
                Figure f1 = board[i.ToString() + k];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[i - 'a', k-1].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[i - 'a', k-1].Add(f);
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
