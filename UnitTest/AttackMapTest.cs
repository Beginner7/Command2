using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protocol;
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
        public AttackMapTest()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
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
        /// Одна ладья в центре поля и чужие фигуры вокруг
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
            //a - assert
            Assert.IsTrue(map["e3"].Contains(rook));
            Assert.IsTrue(map["e5"].Contains(rook));
            Assert.IsTrue(map["d4"].Contains(rook));
            Assert.IsTrue(map["f4"].Contains(rook));
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
    }
}
