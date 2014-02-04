using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.GameObjects;
using Protocol.Transport;

namespace ChessServer.GameLogic
{
    public class AttackMap
    {
        private string _whiteKing;
        private string _blackKing;
        public Board board {get; private set;}
        public List<Figure>[,] Attackers = new List<Figure>[Board.BoardSize, Board.BoardSize];
        private List<Move> moves;
        private Dictionary<Figure, string> _figuresPosition = new Dictionary<Figure, string>();

        public List<Figure> this[string cell]
        {
            set
            {
                Attackers[Board.GetCoords(cell).Item1, Board.GetCoords(cell).Item2] = value;
            }
            get
            {
                return Attackers[Board.GetCoords(cell).Item1, Board.GetCoords(cell).Item2];
            }
        }

        public AttackMap(List<Move> moves, Board forceBoard = null, bool isRecursive = false)
        {
            
            if (forceBoard == null)
            {
                board = new Board();
                board.InitialPosition();
                board.ApplyMoves(moves);
            }
            else
            {
                board = forceBoard;
            }
            this.moves = moves;

            for (int i = 0; i < Board.BoardSize; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    Attackers[i, j] = new List<Figure>();
                }
            }
            for (char i = 'a'; i <= 'h' ; i++)
            {
                for (int j = 1; j <= Board.BoardSize; j++)
                {
                    Figure f = board[i.ToString() + j];
                    _figuresPosition[f] = i.ToString() + j;
                    if (f.GetType() == typeof(FigureNone))
                    {
                        continue;
                    }
                       

                    if (f.GetType() == typeof(FigurePawn))
                    {
                        if (j + 1 <= Board.BoardSize && j - 1 >= 1 )
                        {
                            int k;
                            if (f.side == Side.WHITE)
                            {
                                k = j + 1;
                                Figure f1 = board[i.ToString() + k];
                                if (f1.GetType() != f.GetType())
                                    Attackers[i - 'a', k - 1].Add(f);
                                if (f1.GetType() == typeof(FigureNone))
                                {
                                    if (j == 2) // первый или нет
                                    {
                                        Figure f2 = board[i.ToString() + (k + 1)];
                                        if (f2.GetType() == typeof(FigureNone)) 
                                            Attackers[i - 'a', k].Add(f);
                                    }
                                }
                                if (i + 1 <= 'h')
                                {
                                    char l = (char)(i + 1);
                                    Figure f2 = board[l.ToString() + k];
                                    if (f2.GetType() != typeof(FigureNone))
                                    {
                                        if (f2.side != f.side)
                                            Attackers[l - 'a', k - 1].Add(f);
                                    }
                                }
                                if (i - 1 >= 'a')
                                {
                                    char l = (char)(i - 1);
                                    Figure f2 = board[l.ToString() + k];
                                    if (f2.GetType() != typeof(FigureNone))
                                    {
                                        if (f2.side != f.side)
                                            Attackers[l - 'a', k - 1].Add(f);
                                    }
                                }
                            }
                            if (f.side == Side.BLACK)
                            {
                                k = j - 1;
                                Figure f1 = board[i.ToString() + k];
                                if (f1.GetType() != f.GetType())
                                    Attackers[i - 'a', k - 1].Add(f);
                                if (f1.GetType() == typeof(FigureNone))
                                {
                                    if (j == Board.BoardSize - 1) // первый или нет
                                    {
                                        Figure f2 = board[i.ToString() + (k - 1)];
                                        if (f2.GetType() == typeof(FigureNone))
                                            Attackers[i - 'a', k - 2].Add(f);
                                    }
                                }
                                if (i + 1 <= 'h')
                                {
                                    char l = (char)(i + 1);
                                    Figure f2 = board[l.ToString() + k];

                                    if (f2.side != f.side && f2.GetType() != typeof(FigureNone))
                                        Attackers[l - 'a', k - 1].Add(f);
                                }
                                if (i - 1 >= 'a')
                                {
                                    char l = (char)(i - 1);
                                    Figure f2 = board[l.ToString() + k];

                                    if (f2.side != f.side && f2.GetType() != typeof(FigureNone))
                                        Attackers[l - 'a', k - 1].Add(f);
                                }
                            }
                            
                        }
                        PassedPawn(moves, board, f);
                        continue;
                    }
                    if (f.GetType() == typeof(FigureRook))
                    {
                        North(board, i, j, f);
                        South(board, i, j, f);
                        East(board, i, j, f);
                        West(board, i, j, f);
                        continue;
                    }

                    if (f.GetType() == typeof(FigureQueen))
                    {
                        North(board, i, j, f);
                        South(board, i, j, f);
                        East(board, i, j, f);
                        West(board, i, j, f);
                        NorthEast(board, i, j, f);
                        SouthEast(board, i, j, f);
                        NorthWest(board, i, j, f);
                        SouthWest(board, i, j, f);
                        continue;
                    }

                    if (f.GetType() == typeof(FigureBishop))
                    {
                        NorthEast(board, i, j, f);
                        SouthEast(board, i, j, f);
                        NorthWest(board, i, j, f);
                        SouthWest(board, i, j, f);
                        continue;
                    }

                    if (f.GetType() == typeof(FigureKnight))
                    {
                        char x = (char)(i + 2);
                        int y;
                        if (x <= 'h')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);
                        }
                        x = (char)(i + 1);
                        if (x <= 'h')
                        {
                            y = j + 2;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 2;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);
                        }
                        x = (char)(i - 2);
                        if (x >= 'a')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);
                        }
                        x = (char)(i - 1);
                        if (x >= 'a')
                        {
                            y = j + 2;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 2;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);
                        }
                        continue;
                    }
                    if (f.GetType() == typeof(FigureKing))
                    {
                        char x = (char)(i + 1);
                        int y;

                        if (x <= 'h')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);

                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(board, f, x, y);

                            KingKnightStep(board, f, x, j);
                        }

                        x = (char)(i - 1);
                        if (x >= 'a')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(board, f, x, y);
                            y = j - 1;
                            if (y > 0)
                                KingKnightStep(board, f, x, y);

                            KingKnightStep(board, f, x, j);
                        }

                        y = j + 1;
                        if (y <= Board.BoardSize)
                            KingKnightStep(board, f, i, y);

                        y = j - 1;
                        if (y >= 1)
                            KingKnightStep(board, f, i, y);

                        //if (board["a1"].GetType() == typeof(FigureRook) && board["a1"].side == Side.WHITE)
                        char kingX = board.ReturnPosition(f).Item1;
                        int kingY = board.ReturnPosition(f).Item2;

                        Side side;
                        if (f.side == Side.WHITE)
                            side = Side.BLACK;
                        else
                            side = Side.WHITE;

                        if (kingX == 'e' && (kingY == 1 || kingY == 8))
                        {
                            if (IsColorFigureAttack(this[kingX.ToString() + kingY.ToString()], side))
                                if (board["a" + kingY.ToString()].GetType() == typeof(FigureRook))
                                    Castling(this.moves, board, f, board["a" + kingY.ToString()], side);
                                else if (board["h" + kingY.ToString()].GetType() == typeof(FigureRook))
                                    Castling(this.moves, board, f, board["h" + kingY.ToString()], side);
                        }
                        if (f.side == Side.WHITE)
                        {
                            _whiteKing = (i.ToString() + j);
                        }

                        if (f.side == Side.BLACK)
                        {
                            _blackKing = (i.ToString() + j);
                        }

                        continue;
                    }
                }
            }
            if (!isRecursive)
            {

                foreach (var move in AllPossibleMoves)
                {
                    Board newBoard = board.Clone();
                    var additionalMoves = new List<Move> { move };
                    newBoard.ApplyMoves(additionalMoves);
                    var newAttackMap = new AttackMap(moves.Concat(additionalMoves).ToList(), newBoard, true);
                    var figure = board[move.From];
                    if ((figure.side == Side.WHITE && newAttackMap.IsCheckWhite)
                      || (figure.side == Side.BLACK && newAttackMap.IsCheckBlack))
                    {
                        Attackers[Board.GetCoords(move.To).Item1, Board.GetCoords(move.To).Item2].Remove(figure);
                    }
                }
            }
            if (!isRecursive)
            {
                foreach (var move in AllPossibleMoves)
                {
                    Board newBoard = board.Clone();
                    var additionalMoves = new List<Move> { move };
                    newBoard.ApplyMoves(additionalMoves);
                    var newAttackMap = new AttackMap(moves.Concat(additionalMoves).ToList(), newBoard, true);
                    var figure = board[move.From];


                }

            }
        
        }

        private void KingKnightStep(Board board, Figure f, char x, int y)
        {
            Figure f1 = board[x.ToString() + y];
            if (f1.GetType() == typeof(FigureNone))
                Attackers[x - 'a', y - 1].Add(f);
            else if (f1.side != f.side)
                Attackers[x - 'a', y - 1].Add(f);
        }

        private void SouthWest(Board board, char i, int j, Figure f)
        {
            int l = j - 1;
            char k = (char)(i - 1);
            for (; k >= 'a' && l >= 1; )
            {
                Figure f1 = board[k.ToString() + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k--;
                    l--;
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }            

        }

        private void NorthWest(Board board, char i, int j, Figure f)
        {
            int l = j + 1;
            char k = (char)(i - 1);
            for (; k >= 'a' && l <= Board.BoardSize; )
            {
                Figure f1 = board[k.ToString() + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k--;
                    l++;
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }            

        }

        private void SouthEast(Board board, char i, int j, Figure f)
        {
            int l = j - 1;
            char k = (char)(i + 1);
            for (; k <= 'h' && l >= 1; )
            {
                Figure f1 = board[k.ToString() + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k++;
                    l--;
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }            
        }

        private void NorthEast(Board board, char i, int j, Figure f)
        {
            int l = j + 1;
            char k = (char)(i + 1);
            for (; k <= 'h' && l <= Board.BoardSize; )
            {
                Figure f1 = board[k.ToString() + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k++;
                    l++;
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }            

        }

        private void West(Board board, char i, int j, Figure f)
        {
            for (char k = (char)(i - 1); k >= 'a'; k--)
            {
                Figure f1 = board[k.ToString() + j];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', j - 1].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', j - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        private void East(Board board, char i, int j, Figure f)
        {
            for (char k = (char)(i + 1); k <= 'h'; k++)
            {
                Figure f1 = board[k.ToString() + j];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', j - 1].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[k - 'a', j - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        private void South(Board board, char i, int j, Figure f)
        {
            for (int k = j - 1; k >= 1; k--)
            {
                Figure f1 = board[i.ToString() + k];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[i - 'a', k - 1].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[i - 'a', k - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        private void North(Board board, char i, int j, Figure f)
        {
            for (int k = j + 1; k <= Board.BoardSize; k++)
            {
                Figure f1 = board[i.ToString() + k];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[i - 'a', k - 1].Add(f);
                    continue;
                }
                else if (f1.side != f.side)
                {
                    Attackers[i - 'a', k - 1].Add(f);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        public bool IsCheckWhite
        {
            get
            {
                if (_whiteKing != null && this[_whiteKing].Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        
        public bool IsCheckBlack
        {
            get
            {
                if (_blackKing != null && this[_blackKing].Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsCheck
        {
            get
            {
                if (IsCheckWhite)
                {
                    return true;
                }
                if (IsCheckBlack)
                {
                    return true;
                }
                return false;
            }
        }

        private void Castling(List<Move> moves, Board board, Figure king, Figure rook, Side side)
        {
            int rows = 0;
            if (king.side == Side.WHITE)
                rows = 1;
            else if (king.side == Side.BLACK)
                rows = 8;

            char rookX = board.ReturnPosition(rook).Item1;
            int rookY = board.ReturnPosition(rook).Item2;
            char kingX = board.ReturnPosition(king).Item1;
            int kingY = board.ReturnPosition(king).Item2;

            //List<char> cell = new List<char>();
            //if (bishopX == 'a')
            //    cell = new List<char> { 'b', 'c', 'd' };
            //else if (bishopX == 'h')
            //    cell = new List<char> { 'f', 'g' };

            
            if (moves.Count != 0)
                for (int i = 0; i < moves.Count; i++)
                {
                    if (!moves[i].From.Contains(rookX.ToString() + rookY.ToString()))
                    {
                        for (int j = 0; j < moves.Count; j++)
                            if (!moves[j].From.Contains(kingX.ToString() + kingY.ToString()))
                            {
                                CastlingTest(board, king, rook, side, rows, rookX);
                            }
                            else
                                return;
                    }
                    else
                        return;
                }
            else
            {
                CastlingTest(board, king, rook, side, rows, rookX);
            }

        }

        private void CastlingTest(Board board, Figure king, Figure rook, Side side, int rows, char rookX)
        {
            if (rookX == 'a')
            {
                var figure = this["c" + rows.ToString()];
                figure = this["d" + rows.ToString()];

                if (board["b" + rows.ToString()].GetType() == typeof(FigureNone) &&
                    board["c" + rows.ToString()].GetType() == typeof(FigureNone) &&
                    board["d" + rows.ToString()].GetType() == typeof(FigureNone) &&
                    IsColorFigureAttack(this["c" + rows.ToString()], king.side) &&
                    // Attackers['c' - 'a', rows - 1]
                    IsColorFigureAttack(this["d" + rows.ToString()], king.side) )
                //   Attackers['d' - 'a', rows - 1].Contains(new Figure(side)))
                {
                    Attackers['c' - 'a', rows - 1].Add(king);
                    Attackers['d' - 'a', rows - 1].Add(rook);
                }
            }
            else if (rookX == 'h')
            {
                if (board["f" + rows.ToString()].GetType() == typeof(FigureNone) &&
                    board["g" + rows.ToString()].GetType() == typeof(FigureNone) &&
                    IsColorFigureAttack(this["f" + rows.ToString()], king.side) &&
                    IsColorFigureAttack(this["g" + rows.ToString()], king.side))
                {
                    Attackers['g' - 'a', rows - 1].Add(king);
                    Attackers['f' - 'a', rows - 1].Add(rook);
                }
            }
        }

        private bool IsColorFigureAttack(List<Figure> figure, Side side)
        {
            for (int i = 0; i < figure.Count; i++)
            {
                if (figure[i].side != side)
                    return false;
            }
            return true;
        }

        private void PassedPawn(List<Move> moves, Board board, Figure pawn)
        {
            int rows = 0;
            if (pawn.side == Side.BLACK)
                rows = 4;
            else if (pawn.side == Side.WHITE)
                rows = 5;

            char pawnX = board.ReturnPosition(pawn).Item1;
            int pawnY = board.ReturnPosition(pawn).Item2;

            List<string> cell = new List<string>();
            if (pawnX != 'a')
                cell.Add(((char)(pawnX - 1)).ToString() + rows);
            if (pawnX != 'h')
                cell.Add(((char)(pawnX + 1)).ToString() + rows);

            for (int i = 0; i < cell.Count; i++)
            {
                if (board[cell[i]].GetType() == typeof(FigurePawn) &&
                    board[cell[i]].side != pawn.side)
                {
                    if (moves[moves.Count - 1].To == cell[i])
                    {
                        if ((rows == 4 &&
                          moves[moves.Count - 1].From == cell[i][0].ToString() + (int.Parse(cell[i][1].ToString()) - 2)))
                            Attackers[cell[i][0] - 'a', (int.Parse(cell[i][1].ToString()) - 1) - 1].Add(pawn);
                        if (rows == 5 &&
                                moves[moves.Count - 1].From == cell[i][0].ToString() + (int.Parse(cell[i][1].ToString()) + 2))
                            Attackers[cell[i][0] - 'a', (int.Parse(cell[i][1].ToString()) + 1) - 1].Add(pawn);
                    }

                }
            }
        }

        public IEnumerable<Move> AllPossibleMoves
        {
            get
            {
                var _moves = new List<Move>();
                for (char i = 'a'; i <= 'h'; i++)
                {
                    for (int j = 1; j <= Board.BoardSize; j++)
                    {
                        var currentCell = i.ToString() + j;
                        foreach (var figure in this[currentCell])
                        {
                            var move = new Move();
                            move.From = _figuresPosition[figure];
                            move.To = currentCell;
                            _moves.Add(move);
                        }
                    }
                }
                return _moves;
            }
        }

        public IEnumerable<Move> WhitePossibleMoves
        {
            get
            {
                return AllPossibleMoves.Where(move => board[move.From].side == Side.WHITE);
            }
        }

        public bool IsStalemateWhite
        {
            get
            {
                return WhitePossibleMoves.Count() == 0 && !IsCheckWhite;
            }
        }

        public IEnumerable<Move> BlackPossibleMoves
        {
            get
            {
                return AllPossibleMoves.Where(move => board[move.From].side == Side.BLACK);
            }
        }

        public bool IsStalemateBlack
        {
            get
            {
                return BlackPossibleMoves.Count() == 0 && !IsCheckBlack;
            }
        }

        public bool IsContains(Figure figure)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Attackers[i, j].Contains(figure))
                        return true;
                }
            }
            return false;
        }

    }
}
