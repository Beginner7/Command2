using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.GameObjects;
using Protocol.Transport;

namespace ChessServer.GameLogic
{
    public class AttackMap
    {
        public Board board {get; private set;}
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
                        char x = (char)(i + 1);
                        int y;

                        if (x <= 'h')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);

                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);

                            KingKnightStep(board, f, x, j);
                        }

                        x = (char)(i - 1);
                        if (x >= 'a')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 1;
                            if (y > 0)
                                KingKnightStep(board, f, x, y);

                            KingKnightStep(board, f, x, j);
                        }

                        y = j + 1;
                        if (y <= Board.BoardSize)
                            KingKnightStep(board, f, i, y);

                        y = j - 1;
                        if (y >= 1)
                            KingKnightStep(board, f, i, y);

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
                        char x = (char)(i + 2);
                        int y;
                        if (x <= 'h')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);
                        }
                        x = (char)(i + 1);
                        if (x <= 'h')
                        {
                            y = j + 2;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 2;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);
                        }
                        x = (char)(i - 2);
                        if (x >= 'a')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);
                        }
                        x = (char)(i - 1);
                        if (x >= 'a')
                        {
                            y = j + 2;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 2;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);
                        }
                        continue;
                    }
                }
            }
        }

        private void KingKnightStep(Board board, Figure f, char x, int y)
        {
            Figure f1 = board[x.ToString() + y];
            if (f1.GetType() == typeof(FigureNone) || f1.side != f.side)
                Attackers[x - 'a', y - 1].Add(f1);
        }

        private void SouthWest(Board board, char i, int j, Figure f)
        {
            int l = j - 1;
            char k = (char)(i - 1);
            for (; k >= 'a' && l >= 1; )
            {
                Figure f1 = board[k.ToString() + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k--;
                    l--;
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }            

        }

        private void NorthWest(Board board, char i, int j, Figure f)
        {
            int l = j + 1;
            char k = (char)(i + 1);
            for (; k >= 'a' && l <= Board.BoardSize; )
            {
                Figure f1 = board[k.ToString() + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k--;
                    l++;
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }            

        }

        private void SouthEast(Board board, char i, int j, Figure f)
        {
            int l = j - 1;
            char k = (char)(i + 1);
            for (; k <= 'h' && l >= 1; )
            {
                Figure f1 = board[k.ToString() + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k++;
                    l--;
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }            
        }

        private void NorthEast(Board board, char i, int j, Figure f)
        {
            int l = j + 1;
            char k = (char)(i + 1);
            for (; k <= 'h' && l <= Board.BoardSize; )
            {
                Figure f1 = board[k.ToString() + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k++;
                    l++;
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
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
