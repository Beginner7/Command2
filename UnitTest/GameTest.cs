using ChessServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protocol;

namespace UnitTest
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GameIdNotZero()
        {
            var game1 = new GameObject(null);
            Assert.AreNotEqual(0, game1.Id);
        }

        [TestMethod]
        public void UniqIDs()
        {
            //a - arange
            //a - act

            var game1 = new GameObject(null);
            var game2 = new GameObject(null);

            //a - assert
            Assert.AreNotEqual(game1.Id, game2.Id);
        }
        [TestMethod]
        public void InitUserAfterCreateGame()
        {

            //a - arange
            //a - act
            var user = new user();
            var game = new GameObject(user);
            //a - assert
            Assert.IsTrue(game.PlayerBlack != null || game.PlayerWhite != null);
        }

    }

}
