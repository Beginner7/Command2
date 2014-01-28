using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.Transport;
using Protocol.GameObjects;

namespace Protocol.GameObjects
{
    public class Board
    {
        public static bool CheckNotation(string cell)
        {
            return (cell.Length == 2) && !OutputAbroad(GetCoords(cell));
        }
        public static int BoardSize = 8;
        public static Figure[,] Cells = new Figure[BoardSize, BoardSize];

        public Board()
        {
            FillEmptyCells(0, BoardSize, 0, BoardSize);
        }

        public void InitialPosition()
        {
            Cells[0, 0] = new FigureRook(Protocol.Transport.Side.WHITE);
            Cells[1, 0] = new FigureKnight(Protocol.Transport.Side.WHITE);
            Cells[2, 0] = new FigureBishop(Protocol.Transport.Side.WHITE);
            Cells[3, 0] = new FigureQueen(Protocol.Transport.Side.WHITE);
            Cells[4, 0] = new FigureKing(Protocol.Transport.Side.WHITE);
            Cells[5, 0] = new FigureBishop(Protocol.Transport.Side.WHITE);
            Cells[6, 0] = new FigureKnight(Protocol.Transport.Side.WHITE);
            Cells[7, 0] = new FigureRook(Protocol.Transport.Side.WHITE);

            for (int i = 0; i < BoardSize; i++)
            {
                Cells[i, 1] = new FigurePawn(Protocol.Transport.Side.WHITE);
            }

            //FillEmptyCells(0, BoardSize, 2, BoardSize - 2);

            for (int i = 0; i < BoardSize; i++)
            {
                Cells[i, 6] = new FigurePawn(Protocol.Transport.Side.BLACK);
            }

            Cells[0, 7] = new FigureRook(Protocol.Transport.Side.BLACK);
            Cells[1, 7] = new FigureKnight(Protocol.Transport.Side.BLACK);
            Cells[2, 7] = new FigureBishop(Protocol.Transport.Side.BLACK);
            Cells[3, 7] = new FigureQueen(Protocol.Transport.Side.BLACK);
            Cells[4, 7] = new FigureKing(Protocol.Transport.Side.BLACK);
            Cells[5, 7] = new FigureBishop(Protocol.Transport.Side.BLACK);
            Cells[6, 7] = new FigureKnight(Protocol.Transport.Side.BLACK);
            Cells[7, 7] = new FigureRook(Protocol.Transport.Side.BLACK);
        }

        private static void FillEmptyCells(int startX, int endX, int startY, int endY)
        {
            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    Cells[i, j] = new FigureNone(Side.NONE);
                }
            }
        }

        public void ShowBoard()
        {
            Console.Clear();
            Console.Write("\n    A  B  C  D  E  F  G  H");
            Console.Write("\n\n\n 8\n\n\n 7\n\n\n 6\n\n\n 5\n\n\n 4\n\n\n 3\n\n\n 2\n\n\n 1");
            ConsoleColor cellcolor;
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    if ((x + y) % 2 != 0)
                    {
                        cellcolor = ConsoleColor.Black;
                    }
                    else
                    {
                        cellcolor = ConsoleColor.Gray;
                    }
                    Console.ForegroundColor = cellcolor;
                    Console.SetCursorPosition((x + 1) * 3, (y + 1) * 3);
                    Console.Write("███");
                    Console.SetCursorPosition((x + 1) * 3, (y + 1) * 3 + 1);
                    Console.Write('█');
                    if (Cells[x, 8 - y - 1].symbol != 'X')
                    {
                        if (Cells[x, 8 - y - 1].side == Transport.Side.BLACK)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        if (Cells[x, 8 - y - 1].side == Transport.Side.WHITE)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        Console.Write(Cells[x, 8 - y - 1].symbol);
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
            if (CheckNotation(from) && CheckNotation(to))
            {
                if (Cells[GetCoords(to).Item1, GetCoords(to).Item2].GetType() == typeof(FigureNone) &&
                    Cells[GetCoords(from).Item1, GetCoords(from).Item2].GetType() == typeof(FigurePawn))
                {
                    if (Cells[GetCoords(from).Item1, GetCoords(from).Item2].side == Side.BLACK)
                        Cells[GetCoords(to).Item1, GetCoords(to).Item2 + 1] = new FigureNone(Protocol.Transport.Side.NONE);
                    if (Cells[GetCoords(from).Item1, GetCoords(from).Item2].side == Side.WHITE)
                        Cells[GetCoords(to).Item1, GetCoords(to).Item2 - 1] = new FigureNone(Protocol.Transport.Side.NONE);
                }
                Cells[GetCoords(to).Item1, GetCoords(to).Item2] = Cells[GetCoords(from).Item1, GetCoords(from).Item2];
                Cells[GetCoords(from).Item1, GetCoords(from).Item2] = new FigureNone(Protocol.Transport.Side.NONE);
                
            }
            else
            {
                throw new WrongMoveException(string.Format("Incorrect move notation {0} {1}", from, to));
            }
        }

        public static Tuple<int,int> GetCoords(string cell)
        {
            cell = cell.ToLowerInvariant();
            return new Tuple<int, int>(cell[0] - 'a', int.Parse(cell[1].ToString()) - 1);
        }

        private static bool OutputAbroad(Tuple<int, int> cell)
        {
            if (cell.Item1 > BoardSize - 1 || cell.Item2 > BoardSize - 1 || cell.Item1 < 0 || cell.Item2 < 0)
                return true;
            else
                return false;
        }
        
        public void ApplyMoves(List<Move> moveList)
        {
            foreach (Move element in moveList)
            {
                DoMove(element.From, element.To);
            }
        }

        public Tuple<char,int> ReturnPosition(Figure figure)
        {
            for (char i = 'a'; i <= 'h'; i++)
            {
                for (int j = 1; j <= BoardSize; j++)
                {
                    if (this[i.ToString() + j.ToString()] == figure)
                        return new Tuple<char, int>(i, j);
                }
            }
            throw new Exception("Figure not found");
        }
    }
}
