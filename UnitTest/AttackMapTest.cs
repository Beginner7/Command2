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
            AttackMap map= new AttackMap(new List<Move>(),board);
            //a - assert
            
            for (int j = 1; j <= Board.BoardSize; j++)
            { 
                if(j!=4)
                Assert.IsTrue(map["e"+j].Contains(rook));

            }
            for (char i = 'a'; i <= 'h'; i++)
            {
                if(i!='e')
                Assert.IsTrue(map[i.ToString() +4].Contains(rook));
            }
        }
    }
}
