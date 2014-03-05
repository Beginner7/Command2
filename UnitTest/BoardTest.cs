using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protocol.GameObjects;
using System.Collections.Generic;
using ChessServer.GameLogic;
using Protocol.Transport;

namespace UnitTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void SimpleMoveTest()
        {
            //a - arange
            var board = new Board();
            board.InitialPosition();
            //a - act
            board.DoMove("e2", "e4");
            //a - assert
            Assert.AreEqual(typeof(FigureNone), board["e2"].GetType());
            Assert.AreEqual(typeof(FigurePawn), board["e4"].GetType());
        }

        /// <summary>
        /// Попытка выйти за пределы доски
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongMoveException))]
        public void OutputAbroadTest()
        {
            //a - arange
            var board = new Board();
            board.InitialPosition();
            //a - act
            board.DoMove("e8", "e9");
            //a - assert
        }

        /// <summary>
        /// Проверка нотации хода
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongMoveException))]
        public void NotationTest()
        {
            //a - arange
            var board = new Board();
            board.InitialPosition();
            //a - act
            board.DoMove("e10", "e12");
            //a - assert
        }

        /// <summary>
        /// Проверка нотации хода
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongMoveException))]
        public void NotationTest2()
        {
            //a - arange
            var board = new Board();
            board.InitialPosition();
            //a - act
            board.DoMove("z0", "de");
            //a - assert
        }

        [TestMethod]
        public void WhitePassedPawnTest()
        {
            //a - arange
            var board = new Board();
            var pawnWhite = new FigurePawn(Side.WHITE);
            var pawnBlack = new FigurePawn(Side.BLACK);
            board["f5"] = pawnWhite;
            board["g5"] = pawnBlack;

            var moves = new List<Move>{
                new Move { From = "g7", To = "g5" }};
            //a - act
            var map = new AttackMap(moves, board);
            board.DoMove("f5", "g6");
            //a - assert
            //Assert.AreEqual(pawnWhite.GetType(), board["g6"].GetType());
            Assert.AreEqual(typeof(FigureNone), board["g5"].GetType());
        }

        /// <summary>
        /// одна белая пешка на 7ой горизонтали
        /// </summary>
        [TestMethod]
        public void SimpleIsPromotionWhiteTest()
        {
            //a - arange
            var board = new Board();
            board["d7"] = new FigurePawn(Side.WHITE);

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            board.DoMove("d7", "d8", FigureQueen.SYMBOL.ToString(CultureInfo.InvariantCulture));

            //a - assert
            Assert.AreEqual(typeof(FigureQueen), board["d8"].GetType());
        }

        
    }
}
