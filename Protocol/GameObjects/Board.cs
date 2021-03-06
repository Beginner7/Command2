﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Protocol.Transport;

namespace Protocol.GameObjects
{
    public class Board
    {
        public static bool CheckNotation(string cell)
        {
            return (cell.Length == 2) && !OutputAbroad(GetCoords(cell));
        }
        public static int BoardSize = 8;
        public Figure[,] Cells = new Figure[BoardSize, BoardSize];
        public bool IsNeedPawnPromotion = false;

        public Board()
        {
            FillEmptyCells(0, BoardSize, 0, BoardSize);
        }

        public void InitialPosition()
        {
            Cells[0, 0] = new FigureRook(Side.WHITE);
            Cells[1, 0] = new FigureKnight(Side.WHITE);
            Cells[2, 0] = new FigureBishop(Side.WHITE);
            Cells[3, 0] = new FigureQueen(Side.WHITE);
            Cells[4, 0] = new FigureKing(Side.WHITE);
            Cells[5, 0] = new FigureBishop(Side.WHITE);
            Cells[6, 0] = new FigureKnight(Side.WHITE);
            Cells[7, 0] = new FigureRook(Side.WHITE);

            for (int i = 0; i < BoardSize; i++)
            {
                Cells[i, 1] = new FigurePawn(Side.WHITE);
            }

            //FillEmptyCells(0, BoardSize, 2, BoardSize - 2);

            for (int i = 0; i < BoardSize; i++)
            {
                Cells[i, 6] = new FigurePawn(Side.BLACK);
            }

            Cells[0, 7] = new FigureRook(Side.BLACK);
            Cells[1, 7] = new FigureKnight(Side.BLACK);
            Cells[2, 7] = new FigureBishop(Side.BLACK);
            Cells[3, 7] = new FigureQueen(Side.BLACK);
            Cells[4, 7] = new FigureKing(Side.BLACK);
            Cells[5, 7] = new FigureBishop(Side.BLACK);
            Cells[6, 7] = new FigureKnight(Side.BLACK);
            Cells[7, 7] = new FigureRook(Side.BLACK);
        }

        private void FillEmptyCells(int startX, int endX, int startY, int endY)
        {
            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    Cells[i, j] = new FigureNone(Side.NONE);
                }
            }
        }

        public void ShowBoardToConcole()
        {
            Console.Clear();
            Console.Write("\n    A  B  C  D  E  F  G  H");
            Console.Write("\n\n\n 8\n\n\n 7\n\n\n 6\n\n\n 5\n\n\n 4\n\n\n 3\n\n\n 2\n\n\n 1");
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    ConsoleColor cellcolor = (x + y) % 2 != 0 ? ConsoleColor.Black : ConsoleColor.Gray;
                    Console.ForegroundColor = cellcolor;
                    Console.SetCursorPosition((x + 1) * 3, (y + 1) * 3);
                    Console.Write("███");
                    Console.SetCursorPosition((x + 1) * 3, (y + 1) * 3 + 1);
                    Console.Write('█');
                    if (Cells[x, 8 - y - 1].Symbol != 'X')
                    {
                        if (Cells[x, 8 - y - 1].Side == Side.BLACK)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        if (Cells[x, 8 - y - 1].Side == Side.WHITE)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        Console.Write(Cells[x, 8 - y - 1].Symbol);
                    }
                    else
                    {
                        Console.Write('█');
                    }
                    Console.ForegroundColor = cellcolor;
                    Console.Write('█');
                    Console.SetCursorPosition((x + 1) * 3, (y + 1) * 3 + 2);
                    Console.Write("███");
                }
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public Dictionary<string, string> ShowBoardToWeb()
        {
            var figures = new Dictionary<string,string>();
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    if (Cells[x, y].Symbol != 'X')
                        figures.Add((char)('a' + x) + (y + 1).ToString(CultureInfo.InvariantCulture), Cells[x, y].Symbol.ToString(CultureInfo.InvariantCulture) + Cells[x, y].Side.ToString()[0]);
                }
            }
            return figures;
        }

        public Figure this[string cell]
        {
            set
            {
                Cells[GetCoords(cell).Item1, GetCoords(cell).Item2]=value;
            }
            get
            {
                return Cells[GetCoords(cell).Item1, GetCoords(cell).Item2];
            }
        }

        public void DoMove(string from, string to)
        {
            DoMove(from, to, null);
        }

        public void DoMove(string from, string to, string inWho)
        {
            if (CheckNotation(from) && CheckNotation(to))
            {
                if (Cells[GetCoords(to).Item1, GetCoords(to).Item2].GetType() == typeof(FigureNone) &&
                    Cells[GetCoords(from).Item1, GetCoords(from).Item2].GetType() == typeof(FigurePawn)
                    && (GetCoords(from).Item1 != GetCoords(to).Item1))
                {
                    if (Cells[GetCoords(from).Item1, GetCoords(from).Item2].Side == Side.BLACK)
                        Cells[GetCoords(to).Item1, GetCoords(to).Item2 + 1] = new FigureNone(Side.NONE);
                    if (Cells[GetCoords(from).Item1, GetCoords(from).Item2].Side == Side.WHITE)
                        Cells[GetCoords(to).Item1, GetCoords(to).Item2 - 1] = new FigureNone(Side.NONE);
                }
                
                Cells[GetCoords(to).Item1, GetCoords(to).Item2] = Cells[GetCoords(from).Item1, GetCoords(from).Item2];
                Cells[GetCoords(from).Item1, GetCoords(from).Item2] = new FigureNone(Side.NONE);

                if (Cells[GetCoords(to).Item1, GetCoords(to).Item2].GetType() == typeof(FigurePawn) && IsPromotion(to))
                {
                    if (inWho != null && "rnbqc".IndexOf(inWho.ToLower()) >= 0)
                    {
                        DoPromotion(to, inWho);
                        IsNeedPawnPromotion = false;
                    }
                    else
                    {
                        IsNeedPawnPromotion = true;
                    }
                }
                if (Cells[GetCoords(to).Item1, GetCoords(to).Item2].GetType() == typeof (FigureKing) &&
                    Math.Abs(GetCoords(to).Item1 - GetCoords(from).Item1) == 2)
                {
                    int cellRookToX;
                    int cellRookFromX;
                    if (GetCoords(to).Item1 < GetCoords(from).Item1)
                    {
                        cellRookToX = 'd' - 'a';
                        cellRookFromX = 'a' - 'a';
                        
                    }
                    else
                    {
                        cellRookToX = 'f' - 'a';
                        cellRookFromX = 'h' - 'a';
                    }
                    Cells[cellRookToX, GetCoords(to).Item2] = Cells[cellRookFromX, GetCoords(from).Item2];
                    Cells[cellRookFromX, GetCoords(from).Item2] = new FigureNone(Side.NONE);
                }
            }
            else
            {
                throw new WrongMoveException(string.Format("Incorrect move notation {0} {1}", from, to));
            }
        }

        private void DoPromotion(string to, string inWho)
        {
            switch (inWho.ToUpperInvariant()[0])
            {
                case FigureBishop.SYMBOL:
                    Cells[GetCoords(to).Item1, GetCoords(to).Item2] =
                        new FigureBishop(Cells[GetCoords(to).Item1, GetCoords(to).Item2].Side);
                    break;
                case FigureKnight.SYMBOL:
                    Cells[GetCoords(to).Item1, GetCoords(to).Item2] =
                        new FigureKnight(Cells[GetCoords(to).Item1, GetCoords(to).Item2].Side);
                    break;
                case FigureQueen.SYMBOL:
                    Cells[GetCoords(to).Item1, GetCoords(to).Item2] =
                        new FigureQueen(Cells[GetCoords(to).Item1, GetCoords(to).Item2].Side);
                    break;
                case FigureRook.SYMBOL:
                    Cells[GetCoords(to).Item1, GetCoords(to).Item2] =
                        new FigureRook(Cells[GetCoords(to).Item1, GetCoords(to).Item2].Side);
                    break;
                default:
                    throw new WrongMoveException(string.Format("Incorrect symbol {0}", inWho));
            }
        }

        private bool IsPromotion(string to)
        {
            if  (GetCoords(to).Item2 == 7 && Cells[GetCoords(to).Item1, GetCoords(to).Item2].Side == Side.WHITE ||
                GetCoords(to).Item2 == 0 && Cells[GetCoords(to).Item1, GetCoords(to).Item2].Side == Side.BLACK)
                return true;
            return false;
        }

        public static Tuple<int,int> GetCoords(string cell)
        {
            cell = cell.ToLowerInvariant();
            return new Tuple<int, int>(cell[0] - 'a', int.Parse(cell[1].ToString(CultureInfo.InvariantCulture)) - 1);
        }

        private static bool OutputAbroad(Tuple<int, int> cell)
        {
            if (cell.Item1 > BoardSize - 1 || cell.Item2 > BoardSize - 1 || cell.Item1 < 0 || cell.Item2 < 0)
                return true;
            return false;
        }

        public void ApplyMoves(List<Move> moveList)
        {
            foreach (Move element in moveList)
            {
                DoMove(element.From, element.To, element.InWhom);
            }
        }

        public Tuple<char,int> ReturnPosition(Figure figure)
        {
            for (char i = 'a'; i <= 'h'; i++)
            {
                for (int j = 1; j <= BoardSize; j++)
                {
                    if (this[i + j.ToString(CultureInfo.InvariantCulture)] == figure)
                        return  new Tuple<char, int>(i, j);
                }
            }
            throw new Exception("Figure not found");
        }

        public Board Clone()
        {
            var board = new Board {Cells = (Figure[,]) Cells.Clone()};
            return board;
        }
    }
}
