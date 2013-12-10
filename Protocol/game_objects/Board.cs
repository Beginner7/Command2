using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class Board
    {
        public static int BoardSize = 8;
        public static Cell[,] Cells = new Cell[BoardSize, BoardSize];

        public Board()
        {
            Cells[0, 0] = new Cell(new FigureRook(Protocol.Transport.Side.WHITE));
            Cells[1, 0] = new Cell(new FigureKnight(Protocol.Transport.Side.WHITE));
            Cells[2, 0] = new Cell(new FigureBishop(Protocol.Transport.Side.WHITE));
            Cells[3, 0] = new Cell(new FigureQueen(Protocol.Transport.Side.WHITE));
            Cells[4, 0] = new Cell(new FigureKing(Protocol.Transport.Side.WHITE));
            Cells[5, 0] = new Cell(new FigureBishop(Protocol.Transport.Side.WHITE));
            Cells[6, 0] = new Cell(new FigureKnight(Protocol.Transport.Side.WHITE));
            Cells[7, 0] = new Cell(new FigureRook(Protocol.Transport.Side.WHITE));

            for (int i = 0; i < BoardSize; i++)
            {
                Cells[i, 1] = new Cell(new FigurePawn(Protocol.Transport.Side.WHITE));
            }

            for (int i = 0; i < BoardSize; i++)
            {
                for (int k = 2; k < BoardSize - 2; k++)
                {
                    Cells[i, k] = new Cell(new FigureNone(Protocol.Transport.Side.NONE));
                }
            }

            for (int i = 0; i < BoardSize; i++)
            {
                Cells[i, 6] = new Cell(new FigurePawn(Protocol.Transport.Side.BLACK));
            }

            Cells[0, 7] = new Cell(new FigureRook(Protocol.Transport.Side.BLACK));
            Cells[1, 7] = new Cell(new FigureKnight(Protocol.Transport.Side.BLACK));
            Cells[2, 7] = new Cell(new FigureBishop(Protocol.Transport.Side.BLACK));
            Cells[3, 7] = new Cell(new FigureQueen(Protocol.Transport.Side.BLACK));
            Cells[4, 7] = new Cell(new FigureKing(Protocol.Transport.Side.BLACK));
            Cells[5, 7] = new Cell(new FigureBishop(Protocol.Transport.Side.BLACK));
            Cells[6, 7] = new Cell(new FigureKnight(Protocol.Transport.Side.BLACK));
            Cells[7, 7] = new Cell(new FigureRook(Protocol.Transport.Side.BLACK));
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
                    if (Cells[x, 8 - y - 1].figure.symbol != 'X')
                    {
                        if (Cells[x, 8 - y - 1].figure.side == Transport.Side.BLACK)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        if (Cells[x, 8 - y - 1].figure.side == Transport.Side.WHITE)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        Console.Write(Cells[x, 8 - y - 1].figure.symbol);
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
    }
}
