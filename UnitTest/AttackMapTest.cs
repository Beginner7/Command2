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
            Board board = new Board();
            var bishop = new FigureBishop(Side.WHITE);
            board["e4"] = bishop;
            AttackMap map = new AttackMap(new List<Move>(), board);
            for (int j = 1; j <= Board.BoardSize; j++)
            for (char i = 'a'; i <= 'h'; i++)
                if ('e' - i == Board.BoardSize - j && Board.BoardSize - j != 0)
                {
                    Assert.IsTrue(map[i.ToString() + j].Contains(bishop));
                }
              
            
        }
            [TestMethod]
         public void WhiteBishopTest()
        {
            Board board = new Board();
            var bishop = new FigureBishop(Side.WHITE);
            var knight = new FigureKnight(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            var pawn = new FigurePawn(Side.BLACK);
            board["e4"] = bishop;
            board["b7"] = knight;
            board["g2"] = bishop;
            board["g6"] = pawn;
            board["b1"] = queen;
            AttackMap map = new AttackMap(new List<Move>(), board);
            for (int j = 1; j <= Board.BoardSize; j++)
                for (char i = 'a'; i <= 'h'; i++)
                    if ('e' - i == Board.BoardSize - j && Board.BoardSize - j != 0 )
                    {
                        Assert.IsTrue(map[i.ToString() + j].Contains(bishop));
                    }
        }
    }
    
}
