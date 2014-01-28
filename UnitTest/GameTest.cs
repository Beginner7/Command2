using System;
using ChessServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Protocol;
using Protocol.GameObjects;
using Protocol.Transport;
using ChessServer.GameLogic;

namespace UnitTest
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GameIDNotZero()
        {
            var game1 = new Game(null);
            Assert.AreNotEqual(0, game1.ID);
        }

        [TestMethod]
        public void UniqIDs()
        {
            //a - arange
            //a - act

            var game1 = new Game(null);
            var game2 = new Game(null);

            //a - assert
            Assert.AreNotEqual(game1.ID, game2.ID);
        }
        [TestMethod]
        public void InitUserAfterCreateGame()
        {

            //a - arange
            //a - act
            var user = new User();
            var game = new Game(user);
            //a - assert
            Assert.IsTrue(game.PlayerBlack != null || game.PlayerWhite != null);
        }

    }

}
