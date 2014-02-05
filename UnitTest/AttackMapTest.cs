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
        ///длинная рокировка (черные): на поле король и ладья, которая уже ходила
        /// </summary>
        [TestMethod]
        public void LongCastlingBlackTest3()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);

            board["e8"] = king;
            board["a8"] = rook;
            //a - act
            List<Move> moves = new List<Move>{
                new Move { From = "a8", To = "a4" },
                new Move{From = "a4", To = "a8"}};
            AttackMap map = new AttackMap(moves, board);
            //a - assert
            Assert.IsFalse(map["c8"].Contains(king));
        }

        /// <summary>
        ///длинная рокировка (черные): на поле король, ладья и конь, который уже ходил
        /// </summary>
        [TestMethod]
        public void LongCastlingBlackTest4()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);
            var knight = new FigureKnight(Side.WHITE);

            board["e8"] = king;
            board["a8"] = rook;
            board["d6"] = knight;
            //a - act
            List<Move> moves = new List<Move>{
                new Move { From = "c8", To = "d6" }};
            AttackMap map = new AttackMap(moves, board);
            
            //a - assert
            Assert.IsFalse(map["c8"].Contains(king));
        }

        /// <summary>
        ///длинная рокировка (черные): на поле только король и ладья
        /// </summary>
        [TestMethod]
        public void LongCastlingBlackTest()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);

            board["e8"] = king;
            board["a8"] = rook;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map["c8"].Contains(king));
        }

        /// <summary>
        ///короткая рокировка (черные): на поле только король и ладья
        /// </summary>
        [TestMethod]
        public void ShortCastlingBlackTest()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);

            board["e8"] = king;
            board["h8"] = rook;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map["g8"].Contains(king));
        }

        /// <summary>
        ///длинная рокировка (черные): на поле король, ладья и "мешающая" пешка
        /// </summary>
        [TestMethod]
        public void LongCastlingBlackTest2()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);
            var pawn = new FigurePawn(Side.BLACK);

            board["e8"] = king;
            board["a8"] = rook;
            board["b8"] = pawn;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsFalse(map["c8"].Contains(king));
        }

        /// <summary>
        ///короткая рокировка (черные): на поле король, ладья и "мешающая" пешка
        /// </summary>
        [TestMethod]
        public void ShortCastlingBlackTest2()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);
            var pawn = new FigurePawn(Side.BLACK);

            board["e8"] = king;
            board["h8"] = rook;
            board["f8"] = pawn;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsFalse(map["g8"].Contains(king));
        }

        /// <summary>
        ///длинная рокировка (белые): на поле только король и ладья
        /// </summary>
        [TestMethod]
        public void LongCastlingWhiteTest()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.WHITE);
            var rook = new FigureRook(Side.WHITE);

            board["e1"] = king;
            board["a1"] = rook;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map["c1"].Contains(king));
        }

        /// <summary>
        ///короткая рокировка (белые): на поле только король и ладья
        /// </summary>
        [TestMethod]
        public void ShortCastlingWhiteTest()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.WHITE);
            var rook = new FigureRook(Side.WHITE);

            board["e1"] = king;
            board["h1"] = rook;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map["g1"].Contains(king));
        }

        /// <summary>
        ///длинная рокировка (белые): на поле король, ладья и "мешающая" пешка
        /// </summary>
        [TestMethod]
        public void LongCastlingWhiteTest2()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.WHITE);
            var rook = new FigureRook(Side.WHITE);
            var pawn = new FigurePawn(Side.WHITE);

            board["e1"] = king;
            board["a1"] = rook;
            board["b1"] = pawn;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsFalse(map["c1"].Contains(king));
        }

        /// <summary>
        ///длинная рокировка (белые): на поле король, ладья и "мешающая" пешка
        /// </summary>
        [TestMethod]
        public void ShortCastlingWhiteTest2()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.WHITE);
            var rook = new FigureRook(Side.WHITE);
            var pawn = new FigurePawn(Side.WHITE);

            board["e1"] = king;
            board["h1"] = rook;
            board["f1"] = pawn;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsFalse(map["g1"].Contains(king));
        }

        /// <summary>
        /// один король на поле
        /// </summary>
        [TestMethod]
        public void NewWhiteKingTest()
        {
            ClassForTest tmpClass = new ClassForTest(new FigureKing(Side.WHITE), "e4");
            //a - arange
            tmpClass.board["f5"] = new FigurePawn(Side.WHITE);
            tmpClass.board["f4"] = new FigurePawn(Side.BLACK);
            tmpClass.board["f3"] = new FigureBishop(Side.BLACK);
            tmpClass.board["e3"] = new FigureRook(Side.BLACK);
            tmpClass.board["d3"] = new FigureQueen(Side.BLACK);

            tmpClass.validCells = new List<string>
            {
                "d1", "d2",
                "f1", "f2",
                "e2"
            };
            //a - act
            tmpClass.MapUpdate();
            //a - assert
            Assert.IsTrue(tmpClass.Check());
        }

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
        /// Одна ладья в центре поля и чужие фигуры вокруг
        /// </summary>
        [TestMethod]
        public void WhiteRookTest()
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
        /// Одна ладья в центре поля и чужие фигуры вокруг
        /// </summary>
        [TestMethod]
        public void BlackRookTest()
        {
            //a - arange
            Board board = new Board();
            var rook = new FigureRook(Side.BLACK);
            var knight = new FigureKnight(Side.WHITE);
            var bishop = new FigureBishop(Side.WHITE);
            var pawn = new FigurePawn(Side.WHITE);
            var queen = new FigureQueen(Side.WHITE);
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
        public void BlackRookVsBlackTest()
        {
            //a - arange
            Board board = new Board();
            var rook = new FigureRook(Side.BLACK);
            var knight = new FigureKnight(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            var bishop = new FigureBishop(Side.BLACK);
            var rook2 = new FigureRook(Side.BLACK);
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
        /// Одна ладья в центре поля и свои вокруг
        /// </summary>
        [TestMethod]
        public void WhiteRookVsWhiteTest()
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

        /// <summary>
        ///  Черная пешка
        /// </summary>
        [TestMethod]
        public void BlackPawnSimpleTest()
        {
            //a - arange
            Board board = new Board();
            var pawn = new FigurePawn(Side.BLACK);
            board["e7"] = pawn;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);

            //a - assert
            List<string> validCells = new List<string>
            {
                "e6","e5"
            };
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(pawn));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(pawn));
                    }

                }
            }
        }

        /// <summary>
        /// Слон 
        /// </summary>
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
        ///  Белый слон окружен черными фигурами
        /// </summary>
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
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(bishop));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(bishop));
                    }

                }
            }
        }



        /// <summary>
        ///  Черный слон окружен белыми фигурами
        /// </summary>
        [TestMethod]
        public void BlackBishopTest()
        {
            //a - arange
            Board board = new Board();
            var bishop = new FigureBishop(Side.BLACK);
            var knight = new FigureKnight(Side.WHITE);
            var rook = new FigureRook(Side.WHITE);
            var queen = new FigureQueen(Side.WHITE);
            var pawn = new FigurePawn(Side.WHITE);
            board["e5"] = bishop;
            board["c3"] = knight;
            board["c7"] = rook;
            board["g3"] = pawn;
            board["g7"] = queen;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);

            //a - assert
            List<string> validCells = new List<string>
            {
                "f6","g7",
                "d4","c3",
                "d6","c7",
                "f4","g3"
            };
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(bishop));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(bishop));
                    }

                }
            }
        }

        /// <summary>
        ///  Белый слон окружен белыми фигурами
        /// </summary>
        [TestMethod]
        public void WhiteBishopVsWhiteTest()
        {
            //a - arange
            Board board = new Board();
            var bishop = new FigureBishop(Side.WHITE);
            var knight = new FigureKnight(Side.WHITE);
            var rook = new FigureRook(Side.WHITE);
            var queen = new FigureQueen(Side.WHITE);
            var pawn = new FigurePawn(Side.WHITE);
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
                "f5",
                "d3",
                "d5",
                "f3"
            };
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(bishop));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(bishop));
                    }

                }
            }
        }

        /// <summary>
        ///  Черный слон окружен черными фигурами
        /// </summary>
        [TestMethod]
        public void BlackBishopVsBlackTest()
        {
            //a - arange
            Board board = new Board();
            var bishop = new FigureBishop(Side.BLACK);
            var knight = new FigureKnight(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            var pawn = new FigurePawn(Side.BLACK);
            board["e5"] = bishop;
            board["c3"] = knight;
            board["c7"] = rook;
            board["g3"] = pawn;
            board["g7"] = queen;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);

            //a - assert
            List<string> validCells = new List<string>
            {
                "f6",
                "d4",
                "d6",
                "f4"
            };
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(bishop));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(bishop));
                    }

                }
            }
        }

        [TestMethod]
        public void WhiteKnightTest()
        {
            //a - arange
            Board board = new Board();
            var knight = new FigureKnight(Side.WHITE);
            var rook = new FigureRook(Side.BLACK);
            var bishop = new FigureBishop(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            var queen1 = new FigureQueen(Side.WHITE);
            var bishop1 = new FigureBishop(Side.BLACK);
            board["e4"] = knight;
            board["f6"] = queen1;
            board["g5"] = bishop1;
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
        /// один король на поле
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
            Assert.IsFalse(map["e5"].Contains(king));
            Assert.IsFalse(map["f5"].Contains(king));
            Assert.IsTrue(map["f4"].Contains(king));
            Assert.IsFalse(map["f3"].Contains(king));
            Assert.IsFalse(map["e3"].Contains(king));
            Assert.IsFalse(map["d3"].Contains(king));
            Assert.IsFalse(map["d4"].Contains(king));
            Assert.IsFalse(map["d5"].Contains(king));
            Assert.IsFalse(map["g4"].Contains(king));
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


            List<string> validCells = new List<string>
            {
                "e3","f2",
                "g1","c5",
                "b6","a7",
                "c3","b2",
                "a1","e5",
                "f6","g7",
                "h8",
                "d3","d2","d1",
                "d5","d6","d7","d8",
                "e4","f4","g4","h4",
                "c4","b4","a4"
            };
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(queen));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(queen));
                    }
                }
            }
        }

        /// <summary>
        /// Белая Королева окружена  Черными фигурами
        /// </summary>
        [TestMethod]
        public void WhiteQueenVsBlackTest()
        {
            //a - arange
            Board board = new Board();
            var bishop = new FigureBishop(Side.BLACK);
            var knight = new FigureKnight(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);
            var queen2 = new FigureQueen(Side.BLACK);
            var pawn = new FigurePawn(Side.BLACK);
            var queen = new FigureQueen(Side.WHITE);
            board["d1"] = rook;
            board["d8"] = rook;
            board["h4"] = knight;
            board["a4"] = queen2;


            board["d4"] = queen;
            board["a1"] = knight;
            board["a7"] = rook;
            board["g1"] = pawn;
            board["h8"] = queen2;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert


            List<string> validCells = new List<string>
            {
                "e3","f2",
                "g1","c5",
                "b6","a7",
                "c3","b2",
                "a1","e5",
                "f6","g7",
                "h8",
                "d3","d2","d1",
                "d5","d6","d7","d8",
                "e4","f4","g4","h4",
                "c4","b4","a4"
            };
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(queen));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(queen));
                    }
                }
            }
        }


        /// <summary>
        /// Черная Королева окружена  Белыми фигурами
        /// </summary>
        [TestMethod]
        public void BlackQueenVsWhiteTest()
        {
            //a - arange
            Board board = new Board();
            var bishop = new FigureBishop(Side.WHITE);
            var knight = new FigureKnight(Side.WHITE);
            var rook = new FigureRook(Side.WHITE);
            var queen2 = new FigureQueen(Side.WHITE);
            var pawn = new FigurePawn(Side.WHITE);
            var queen = new FigureQueen(Side.BLACK);
            board["d1"] = rook;
            board["d8"] = rook;
            board["h4"] = knight;
            board["a4"] = queen2;

            //Диагональ
            board["d4"] = queen;
            board["a1"] = knight;
            board["a7"] = rook;
            board["g1"] = pawn;
            board["h8"] = queen2;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert


            List<string> validCells = new List<string>
            {
                "e3","f2",
                "g1","c5",
                "b6","a7",
                "c3","b2",
                "a1","e5",
                "f6","g7",
                "h8",
                "d3","d2","d1",
                "d5","d6","d7","d8",
                "e4","f4","g4","h4",
                "c4","b4","a4"
            };
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(queen));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(queen));
                    }
                }
            }
        
        }

        /// <summary>
        /// Черная Королева окружена Cвоими
        /// </summary>
        [TestMethod]
        public void BlackQueenTest()
        {
            //a - arange
            Board board = new Board();
            var bishop = new FigureBishop(Side.BLACK);
            var knight = new FigureKnight(Side.BLACK);
            var rook = new FigureRook(Side.BLACK);
            var queen2 = new FigureQueen(Side.BLACK);
            var pawn = new FigurePawn(Side.BLACK);
            var queen = new FigureQueen(Side.BLACK);
            board["d2"] = rook;
            board["d7"] = rook;
            board["g4"] = knight;
            board["b4"] = queen2;


            board["d4"] = queen;
            board["b2"] = knight;
            board["b6"] = rook;
            board["f2"] = pawn;
            board["g7"] = queen2;
            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert


            List<string> validCells = new List<string>
            {
                "e3",
                "c5",
                "c3",
                "e5",
                "f6",
                
                "d3",
                "d5","d6",
                "e4","f4",
                "c4"
            };
            for (int j = 1; j <= Board.BoardSize; j++)
            {
                for (char i = 'a'; i <= 'h'; i++)
                {
                    string cell = i.ToString() + j;
                    if (validCells.Contains(cell))
                    {
                        Assert.IsTrue(map[cell].Contains(queen));
                    }
                    else
                    {
                        Assert.IsFalse(map[cell].Contains(queen));
                    }
                }
            }
        }

        /// <summary>
        /// проходная пешка
        /// </summary>
        [TestMethod]
        public void WhitePassedPawnTest()
        {
            //a - arange
            Board board = new Board();
            var pawnWhite = new FigurePawn(Side.WHITE);
            var pawnBlack = new FigurePawn(Side.BLACK);
            board["c4"] = pawnWhite;
            board["b4"] = pawnBlack;

            List<Move> moves = new List<Move>{
                new Move { From = "c2", To = "c4" }};
            //a - act
            AttackMap map = new AttackMap(moves, board);
            //a - assert
            Assert.IsTrue(map["c3"].Contains(pawnBlack));
        }

        /// <summary>
        /// проходная пешка
        /// </summary>
        [TestMethod]
        public void BlackPassedPawnTest()
        {
            //a - arange
            Board board = new Board();
            var pawnWhite = new FigurePawn(Side.WHITE);
            var pawnBlack = new FigurePawn(Side.BLACK);
            board["f5"] = pawnWhite;
            board["g5"] = pawnBlack;

            List<Move> moves = new List<Move>{
                new Move { From = "g7", To = "g5" }};
            //a - act
            AttackMap map = new AttackMap(moves, board);
            //a - assert
            Assert.IsTrue(map["g6"].Contains(pawnWhite));
        }

        /// <summary>
        /// проходная пешка
        /// </summary>
        [TestMethod]
        public void BlackPassedPawnTest2()
        {
            //a - arange
            Board board = new Board();
            var pawnWhite = new FigurePawn(Side.WHITE);
            var pawnBlack = new FigurePawn(Side.BLACK);
            board["f5"] = pawnWhite;
            board["g5"] = pawnBlack;

            List<Move> moves = new List<Move>{
                new Move { From = "g7", To = "g6" },
                new Move { From = "g6", To = "g5" }};
            //a - act
            AttackMap map = new AttackMap(moves, board);
            //a - assert
            Assert.IsFalse(map["g6"].Contains(pawnWhite));
        }

        /// <summary>
        /// проходная пешка
        /// </summary>
        [TestMethod]
        public void WhitePassedPawnTest2()
        {
            //a - arange
            Board board = new Board();
            var pawnWhite = new FigurePawn(Side.WHITE);
            var pawnBlack = new FigurePawn(Side.BLACK);
            var rookBlack = new FigureRook(Side.BLACK);
            //board["c4"] = pawnWhite;
            //board["b4"] = pawnBlack;
            //board["d4"] = rookBlack;
            
            List<Move> moves = new List<Move>{
                new Move { From = "c2", To = "c4" },
                new Move { From = "f6", To = "d4" },
                new Move { From = "b5", To = "b4" }};
            //a - act
            AttackMap map = new AttackMap(moves, new Board());
            //a - assert
            Assert.IsTrue(!map["c3"].Contains(pawnBlack));
        }

        /// <summary>
        /// Проверим, что в случае шаха возможны только ходы, которые убирают этот шах
        /// </summary>
        [TestMethod]
        public void AfterIsCheckTest()
        {
            //a - arange
            Board board = new Board();
            var pawn = new FigurePawn(Side.WHITE);
            var king = new FigureKing(Side.WHITE);
            board["h3"] = king;
            board["g2"] = pawn;
            board["f3"] = new FigureQueen(Side.BLACK);

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map.IsCheck);
            Assert.IsTrue(map["f3"].Contains(pawn));
            Assert.IsTrue(map["g3"].Contains(pawn));
            Assert.IsFalse(map["g4"].Contains(pawn));
            Assert.IsFalse(map["g3"].Contains(king));
            Assert.IsFalse(map["g4"].Contains(king));
            Assert.IsTrue(map["h2"].Contains(king));
            Assert.IsTrue(map["h4"].Contains(king));
        }

        /// <summary>
        /// Проверка работоспособности IsCheck
        /// </summary>
        [TestMethod]
        public void IsCheckTest()
        {
            //a - arange
            Board board = new Board();
            var rook = new FigureRook(Side.BLACK);
            var king = new FigureKing(Side.WHITE);
            board["e3"] = king;
            board["h3"] = rook;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            Assert.IsTrue(map.IsCheck);
        }

        /// <summary>
        /// Проверим, что нельзя сделать ходы, которые приводят к шаху
        /// </summary>
        [TestMethod]
        public void MovesToCheckTest()
        {
            //a - arange
            Board board = new Board();
            var pawn = new FigurePawn(Side.WHITE);
            var king = new FigureKing(Side.WHITE);
            board["h1"] = king;
            board["g2"] = pawn;
            board["a8"] = new FigureQueen(Side.BLACK);

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsFalse(map["g3"].Contains(pawn));
            Assert.IsFalse(map["g4"].Contains(pawn));
            Assert.IsTrue(map["g1"].Contains(king));
            Assert.IsTrue(map["h2"].Contains(king));
        }

        /// <summary>
        /// Проверка пата для белого короля
        /// </summary>
        [TestMethod]
        public void IsStalemateWhiteTest()
        {
            //a - arange
            Board board = new Board();
            var pawn = new FigurePawn(Side.WHITE);
            var pawn2 = new FigurePawn(Side.BLACK);
            var king = new FigureKing(Side.WHITE);
            board["h8"] = king;
            board["f7"] = new FigureQueen(Side.BLACK);
            board["b4"] = pawn;
            board["b5"] = pawn2;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map.IsStalemateWhite);
        }

        /// <summary>
        /// Проверка пата для черного короля
        /// </summary>
        [TestMethod]
        public void IsStalemateBlackTest()
        {
            //a - arange
            Board board = new Board();
            var pawn = new FigurePawn(Side.BLACK);
            var pawn2 = new FigurePawn(Side.WHITE);
            var king = new FigureKing(Side.BLACK);
            board["h8"] = king;
            board["f7"] = new FigureQueen(Side.WHITE);
            board["b4"] = pawn2;
            board["b5"] = pawn;

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map.IsStalemateBlack);
        }

        /// <summary>
        /// Тест на мат черного короля 4 белыми фигурами
        /// </summary>
        [TestMethod]
        public void IsMateBlackTest()
        {
            //a - arange
            Board board = new Board();
            var king = new FigureKing(Side.BLACK);
            board["e8"] = king;
            board["g8"] = new FigureQueen(Side.WHITE);
            board["c8"] = new FigureRook(Side.WHITE);
            board["b7"] = new FigureRook(Side.WHITE);
            board["e7"] = new FigureBishop(Side.WHITE);

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map.IsMateBlack);
        }

        /// <summary>
        /// Тест на мат белого короля окруженного своими фигурами и находящимся под ударом лошади
        /// </summary>
        [TestMethod]
        public void IsMateWhiteTest()
        {
            //a - arange
            Board board = new Board();
            board["a1"] = new FigureKing(Side.WHITE);
            board["a2"] = new FigurePawn(Side.WHITE);
            board["b1"] = new FigureRook(Side.WHITE);
            board["b2"] = new FigureBishop(Side.WHITE);
            board["c2"] = new FigureKnight(Side.BLACK);

            //a - act
            AttackMap map = new AttackMap(new List<Move>(), board);
            //a - assert
            Assert.IsTrue(map.IsMateWhite);
        }

    
    }

}
