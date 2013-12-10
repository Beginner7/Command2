using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protocol;

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
            board.DoMove("e2","e4");
            //a - assert
            Assert.AreEqual(typeof(FigureNone),board["e2"].GetType());
            Assert.AreEqual(typeof(FigurePawn), board["e4"].GetType());
        }
    }
}
