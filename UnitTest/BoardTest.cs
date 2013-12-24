using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protocol;
using Protocol.GameObjects;

namespace UnitTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void SimpleMoveTest()
        {
            //a - arange
            Board board = new Board();
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
            Board board = new Board();
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
            Board board = new Board();
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
            Board board = new Board();
            board.InitialPosition();
            //a - act
            board.DoMove("z0", "de");
            //a - assert
        }
    }
}
