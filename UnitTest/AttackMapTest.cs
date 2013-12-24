using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protocol.GameObjects;
using Protocol.Transport;
using ChessServer.GameLogic;

namespace UnitTest
{
    /// <summary>
    /// Проверка логики работы карты атак для всех фигур
    /// </summary>
    [TestClass]
    public class AttackMapTest
    {
        /// <summary>
        /// Тест карты атак для начальной позиции для доски. Не должно быть исключений при постройке этой карты атак.
        /// </summary>
        [TestMethod]
        public void InitialPositionTest()
        {
            //a - act
            AttackMap map = new AttackMap(new List<Move>());
        }
        /// <summary>
        /// Одна ладья в центре поля
        /// </summary>
        [TestMethod]
        public void SimpleRookTest()
        {
            //a - arange
            Board board = new Board();
            var rook = new FigureRook(Side.WHITE);
            board["e4"] = rook;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                if (j != 4)
                    Assert.IsTrue(map["e" + j].Contains(rook));

            }
            for (char i = 'a'; i <= 'h'; i++)
            {
                if (i != 'e')
                    Assert.IsTrue(map[i.ToString() + 4].Contains(rook));
            }
        }
        /// <summary>
        /// ДОДЕЛАТЬ!!!
        /// TODO: Одна ладья в центре поля и чужие фигуры вокруг
        /// </summary>
        [TestMethod]
        public void BlackRookTest()
        {
            //a - arange
            Board board = new Board();
            var rook = new FigureRook(Side.WHITE);
            var knight = new FigureKnight(Side.BLACK);
            var bishop = new FigureBishop(Side.BLACK);
            var pawn = new FigurePawn(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            board["e4"] = rook;
            board["e3"] = knight;
            board["e5"] = bishop;
            board["d4"] = pawn;
            board["f4"] = queen;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            for (char i = 'a'; i <= 'h'; i++)
            {
                for (int j = 1; j <= Board.BoardSize; j++)
                {
                    if (j == 4)
                    {
                        if (i == 'f' || i == 'd')
                            Assert.IsTrue(map[i.ToString() + j].Contains(rook));
                        if (i == 'e')
                            Assert.IsTrue(!map[i.ToString() + j].Contains(rook));
                    }
                    else if (j == 3 || j == 5)
                    {
                        if (i == 'e')
                            Assert.IsTrue(map[i.ToString() + j].Contains(rook));
                    }
                    else
                        Assert.IsTrue(!map[i.ToString() + j].Contains(rook));
                }
            }
        }

        /// <summary>
        /// Одна ладья в центре поля и свои вокруг
        /// </summary>
        [TestMethod]
        public void WhiteRookTest()
        {
            //a - arange
            Board board = new Board();
            var rook = new FigureRook(Side.WHITE);
            var knight = new FigureKnight(Side.WHITE);
            var queen = new FigureQueen(Side.WHITE);
            var bishop = new FigureBishop(Side.WHITE);
            var rook2 = new FigureRook(Side.WHITE);
            board["e4"] = rook;
            board["e2"] = rook2;
            board["e6"] = knight;
            board["f4"] = queen;
            board["b4"] = bishop;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert

            for (int j = 1; j <= Board.BoardSize; j++)
            {
                if (j != 4 && j < 6 && j > 2)
                    Assert.IsTrue(map["e" + j].Contains(rook));
                if (j >= 6 && j < 2)
                    Assert.IsTrue(!map["e" + j].Contains(rook));
            }
            for (char i = 'a'; i <= 'h'; i++)
            {
                if (i != 'e' && i < 'f' && i > 'b')
                    Assert.IsTrue(map[i.ToString() + 4].Contains(rook));
            }
        }

        /// <summary>
        /// одна белая пешка
        /// </summary>
        [TestMethod]
        public void OneWhitePawnTest()
        {
            //a - arange
            Board board = new Board();
            var pawn = new FigurePawn(Side.WHITE);
            board["e2"] = pawn;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map["e3"].Contains(pawn));
            Assert.IsTrue(map["e4"].Contains(pawn));
        }
        
        /// <summary>
        /// белая пешка
        /// </summary>
        [TestMethod]
        public void WhitePawnTest()
        {
            //a - arange
            Board board = new Board();
            var pawn = new FigurePawn(Side.WHITE);
            var rook = new FigureRook(Side.BLACK);
            var knight = new FigureKnight(Side.BLACK);
            var bishop = new FigureBishop(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            board["e4"] = pawn; 
            board["e5"] = knight;
            board["d5"] = bishop;
            board["f5"] = rook;
            board["e6"] = queen;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            for (char i = 'a'; i <= 'h'; i++)
            {
                for (int j = 1; j <= Board.BoardSize; j++)
                {
                    if (j == 5)
                    {
                        if (i == 'e' || i == 'd' || i == 'f')
                            Assert.IsTrue(map[i.ToString() + j].Contains(pawn));
                    }
                    else
                        Assert.IsTrue(!map[i.ToString() + j].Contains(pawn));
                }
            }
        }

        /// <summary>
        /// черная пешка
        /// </summary>
        [TestMethod]
        public void BlackPawnTest()
        {
            //a - arange
            Board board = new Board();
            var pawn = new FigurePawn(Side.BLACK);
            var rook = new FigureRook(Side.WHITE);
            var knight = new FigureKnight(Side.WHITE);
            var bishop = new FigureBishop(Side.WHITE);
            var queen = new FigureQueen(Side.WHITE);
            var queen1 = new FigureQueen(Side.WHITE);
            board["e6"] = pawn;
            board["e5"] = knight;
            board["d5"] = bishop;
            board["f5"] = rook;
            board["e4"] = queen;
            board["e7"] = queen1;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            for (char i = 'a'; i <= 'h'; i++)
            {
                for (int j = 1; j <= Board.BoardSize; j++)
                {
                    if (j == 5)
                    {
                        if (i == 'e' || i == 'd' || i == 'f')
                            Assert.IsTrue(map[i.ToString() + j].Contains(pawn));
                    }
                    else
                        Assert.IsTrue(!map[i.ToString() + j].Contains(pawn));
                }
            }
        }
        
        [TestMethod]
        public void SimpleBishopTest()
        {
            //a - arange
            Board board = new Board();
            var bishop = new FigureBishop(Side.WHITE);
            board["e4"] = bishop;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);

            //a - assert
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    if (Math.Abs('e' - i) == Math.Abs(4 - j) && j != 4)
                    {
                        Assert.IsTrue(map[i.ToString() + j].Contains(bishop));
                    }
                    else
                    {
                        Assert.IsFalse(map[i.ToString() + j].Contains(bishop));
                    }
                }
            }             
            
        }

        /// <summary>
        /// один конь и чужие вокруг 
        /// </summary>
        [TestMethod]
        public void WhiteKnightTest()
        {
            //a - arange
            Board board = new Board();
            var knight = new FigureKnight(Side.WHITE); 
            var pawn = new FigurePawn(Side.WHITE);
            var rook = new FigureRook(Side.BLACK);
            var pawn1 = new FigurePawn(Side.BLACK);
            var bishop = new FigureBishop(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            board["e4"] = knight;
            board["f6"] = pawn;
            board["g5"] = pawn1;
            board["g3"] = bishop;
            board["f3"] = rook;
            board["d2"] = queen;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(!map["f6"].Contains(knight));
            Assert.IsTrue(map["g5"].Contains(knight));
            Assert.IsTrue(map["g3"].Contains(knight));
            Assert.IsTrue(map["f2"].Contains(knight));
            Assert.IsTrue(map["d2"].Contains(knight));
            Assert.IsTrue(map["c3"].Contains(knight));
            Assert.IsTrue(map["c5"].Contains(knight));
            Assert.IsTrue(map["d6"].Contains(knight));
            Assert.IsTrue(!map["h6"].Contains(knight));
        }

        /// <summary>
        /// один конь на поле 
        /// </summary>
        [TestMethod]
        public void SimpleKnightTest()
        {
            //a - arange
            [TestMethod]
         public void WhiteBishopTest()
        {
            //a - arange
            Board board = new Board();
            var bishop = new FigureBishop(Side.WHITE);
            var knight = new FigureKnight(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            var pawn = new FigurePawn(Side.BLACK);
            board["e4"] = bishop;
            board["c6"] = knight;
            board["c2"] = rook;
            board["g6"] = pawn;
            board["g2"] = queen;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);

            //a - assert
            List<string> validCells = new List<string>
            {
                "f5","g6",
                "d3","c2",
                "d5","c6",
                "f3","g2"
            };
                for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell)) {
                        Assert.IsTrue(map[cell].Contains(bishop));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(bishop));
                    }
                   
                }
            }
        }
            Board board = new Board();
            var knight = new FigureKnight(Side.WHITE);
            board["e4"] = knight;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map["f6"].Contains(knight));
            Assert.IsTrue(map["g5"].Contains(knight));
            Assert.IsTrue(map["g3"].Contains(knight));
            Assert.IsTrue(map["f2"].Contains(knight));
            Assert.IsTrue(map["d2"].Contains(knight));
            Assert.IsTrue(map["c3"].Contains(knight));
            Assert.IsTrue(map["c5"].Contains(knight));
            Assert.IsTrue(map["d6"].Contains(knight));
        }

        /// <summary>
        /// один король на поле 
        /// </summary>
        [TestMethod]
        public void SimpleKingTest()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.WHITE);
            board["e4"] = king;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map["e5"].Contains(king));
            Assert.IsTrue(map["f5"].Contains(king));
            Assert.IsTrue(map["f4"].Contains(king));
            Assert.IsTrue(map["f3"].Contains(king));
            Assert.IsTrue(map["e3"].Contains(king));
            Assert.IsTrue(map["d3"].Contains(king));
            Assert.IsTrue(map["d4"].Contains(king));
            Assert.IsTrue(map["d5"].Contains(king));
            Assert.IsTrue(!map["h6"].Contains(king));
        }

        /// <summary>
        /// один конь на поле
        /// </summary>
        [TestMethod]
        public void WhiteKingTest()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.WHITE);
            var pawn = new FigurePawn(Side.WHITE);
            var rook = new FigureRook(Side.BLACK);
            var pawn1 = new FigurePawn(Side.BLACK);
            var bishop = new FigureBishop(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            board["e4"] = king;
            board["f5"] = pawn;
            board["f4"] = pawn1;
            board["f3"] = bishop;
            board["e3"] = rook;
            board["d3"] = queen;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map["e5"].Contains(king));
            Assert.IsTrue(!map["f5"].Contains(king));
            Assert.IsTrue(map["f4"].Contains(king));
            Assert.IsTrue(map["f3"].Contains(king));
            Assert.IsTrue(map["e3"].Contains(king));
            Assert.IsTrue(map["d3"].Contains(king));
            Assert.IsTrue(map["d4"].Contains(king));
            Assert.IsTrue(map["d5"].Contains(king));
            Assert.IsTrue(!map["h6"].Contains(king));
        }
        
        /// <summary>
        /// Одна ладья в центре поля
        /// </summary>
        [TestMethod]
        public void SimpleQueenTest()
        {
            //a - arange
            Board board = new Board();
            var queen = new FigureQueen(Side.WHITE);
            board["d4"] = queen;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                if (j != 4)
                    Assert.IsTrue(map["d" + j].Contains(queen));

            }
            for (char i = 'a'; i <= 'h'; i++)
            {
                if (i != 'd')
                    Assert.IsTrue(map[i.ToString() + 4].Contains(queen));
            }

            int k = 1;
            for (char i = 'a'; i <= 'h'; i++, k--)
            {
                if (i != 'd')
                    Assert.IsTrue(map[i.ToString() + k].Contains(queen));
            }

            k = Board.BoardSize;
            for (char i = 'a'; i <= 'h'; i++, k--)
            {
                if (i != 'd')
                    Assert.IsTrue(map[i.ToString() + k].Contains(queen));
            }
        }
    
    }
    
}
