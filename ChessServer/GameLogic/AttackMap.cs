using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Protocol.GameObjects;
using Protocol.Transport;

namespace ChessServer.GameLogic
{
    public class AttackMap
    {
        private readonly string _whiteKing;
        private readonly string _blackKing;
        public Board SourceBoard {get; private set;}
        public List<Figure>[,] Attackers = new List<Figure>[Board.BoardSize, Board.BoardSize];
        private readonly Dictionary<Figure, string> _figuresPosition = new Dictionary<Figure, string>();

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
                SourceBoard = new Board();
                SourceBoard.InitialPosition();
                SourceBoard.ApplyMoves(moves);
            }
            else
            {
                SourceBoard = forceBoard;
            }

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
                    Figure f = SourceBoard[i.ToString(CultureInfo.InvariantCulture) + j];
                    _figuresPosition[f] = i.ToString(CultureInfo.InvariantCulture) + j;
                    if (f.GetType() == typeof(FigureNone))
                    {
                        continue;
                    }

                    if (f.GetType() == typeof(FigurePawn))
                    {
                        if (j + 1 <= Board.BoardSize && j - 1 >= 1 )
                        {
                            int k;
                            if (f.Side == Side.WHITE)
                            {
                                k = j + 1;
                                Figure f1 = SourceBoard[i.ToString(CultureInfo.InvariantCulture) + k];
                                if (f1.GetType() != f.GetType())
                                    Attackers[i - 'a', k - 1].Add(f);
                                if (f1.GetType() == typeof(FigureNone))
                                {
                                    if (j == 2) // первый или нет
                                    {
                                        Figure f2 = SourceBoard[i.ToString(CultureInfo.InvariantCulture) + (k + 1)];
                                        if (f2.GetType() == typeof(FigureNone)) 
                                            Attackers[i - 'a', k].Add(f);
                                    }
                                }
                                if (i + 1 <= 'h')
                                {
                                    var l = (char)(i + 1);
                                    Figure f2 = SourceBoard[l.ToString(CultureInfo.InvariantCulture) + k];
                                    if (f2.GetType() != typeof(FigureNone))
                                    {
                                        if (f2.Side != f.Side)
                                            Attackers[l - 'a', k - 1].Add(f);
                                    }
                                }
                                if (i - 1 >= 'a')
                                {
                                    var l = (char)(i - 1);
                                    Figure f2 = SourceBoard[l.ToString(CultureInfo.InvariantCulture) + k];
                                    if (f2.GetType() != typeof(FigureNone))
                                    {
                                        if (f2.Side != f.Side)
                                            Attackers[l - 'a', k - 1].Add(f);
                                    }
                                }
                            }
                            if (f.Side == Side.BLACK)
                            {
                                k = j - 1;
                                Figure f1 = SourceBoard[i.ToString(CultureInfo.InvariantCulture) + k];
                                if (f1.GetType() != f.GetType())
                                    Attackers[i - 'a', k - 1].Add(f);
                                if (f1.GetType() == typeof(FigureNone))
                                {
                                    if (j == Board.BoardSize - 1) // первый или нет
                                    {
                                        Figure f2 = SourceBoard[i.ToString(CultureInfo.InvariantCulture) + (k - 1)];
                                        if (f2.GetType() == typeof(FigureNone))
                                            Attackers[i - 'a', k - 2].Add(f);
                                    }
                                }
                                if (i + 1 <= 'h')
                                {
                                    var l = (char)(i + 1);
                                    Figure f2 = SourceBoard[l.ToString(CultureInfo.InvariantCulture) + k];

                                    if (f2.Side != f.Side && f2.GetType() != typeof(FigureNone))
                                        Attackers[l - 'a', k - 1].Add(f);
                                }
                                if (i - 1 >= 'a')
                                {
                                    var l = (char)(i - 1);
                                    Figure f2 = SourceBoard[l.ToString(CultureInfo.InvariantCulture) + k];

                                    if (f2.Side != f.Side && f2.GetType() != typeof(FigureNone))
                                        Attackers[l - 'a', k - 1].Add(f);
                                }
                            }
                        }
                        PassedPawn(moves, SourceBoard, f);
                        continue;
                    }

                    if (f.GetType() == typeof(FigureRook))
                    {
                        North(SourceBoard, i, j, f);
                        South(SourceBoard, i, j, f);
                        East(SourceBoard, i, j, f);
                        West(SourceBoard, i, j, f);
                        continue;
                    }

                    if (f.GetType() == typeof(FigureQueen))
                    {
                        North(SourceBoard, i, j, f);
                        South(SourceBoard, i, j, f);
                        East(SourceBoard, i, j, f);
                        West(SourceBoard, i, j, f);
                        NorthEast(SourceBoard, i, j, f);
                        SouthEast(SourceBoard, i, j, f);
                        NorthWest(SourceBoard, i, j, f);
                        SouthWest(SourceBoard, i, j, f);
                        continue;
                    }

                    if (f.GetType() == typeof(FigureBishop))
                    {
                        NorthEast(SourceBoard, i, j, f);
                        SouthEast(SourceBoard, i, j, f);
                        NorthWest(SourceBoard, i, j, f);
                        SouthWest(SourceBoard, i, j, f);
                        continue;
                    }

                    if (f.GetType() == typeof(FigureKnight))
                    {
                        var x = (char)(i + 2);
                        int y;
                        if (x <= 'h')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(SourceBoard, f, x, y);
                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(SourceBoard, f, x, y);
                        }
                        x = (char)(i + 1);
                        if (x <= 'h')
                        {
                            y = j + 2;
                            if (y <= Board.BoardSize)
                                KingKnightStep(SourceBoard, f, x, y);
                            y = j - 2;
                            if (y >= 1)
                                KingKnightStep(SourceBoard, f, x, y);
                        }
                        x = (char)(i - 2);
                        if (x >= 'a')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(SourceBoard, f, x, y);
                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(SourceBoard, f, x, y);
                        }
                        x = (char)(i - 1);
                        if (x >= 'a')
                        {
                            y = j + 2;
                            if (y <= Board.BoardSize)
                                KingKnightStep(SourceBoard, f, x, y);
                            y = j - 2;
                            if (y >= 1)
                                KingKnightStep(SourceBoard, f, x, y);
                        }
                        continue;
                    }

                    if (f.GetType() == typeof(FigureKing))
                    {
                        var x = (char)(i + 1);
                        int y;

                        if (x <= 'h')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(SourceBoard, f, x, y);

                            y = j - 1;
                            if (y >= 1)
                                KingKnightStep(SourceBoard, f, x, y);

                            KingKnightStep(SourceBoard, f, x, j);
                        }

                        x = (char)(i - 1);
                        if (x >= 'a')
                        {
                            y = j + 1;
                            if (y <= Board.BoardSize)
                                KingKnightStep(SourceBoard, f, x, y);
                            y = j - 1;
                            if (y > 0)
                                KingKnightStep(SourceBoard, f, x, y);

                            KingKnightStep(SourceBoard, f, x, j);
                        }

                        y = j + 1;
                        if (y <= Board.BoardSize)
                            KingKnightStep(SourceBoard, f, i, y);

                        y = j - 1;
                        if (y >= 1)
                            KingKnightStep(SourceBoard, f, i, y);

                        //if (SourceBoard["a1"].GetType() == typeof(FigureRook) && SourceBoard["a1"].side == Side.WHITE)
                        char kingX = SourceBoard.ReturnPosition(f).Item1;
                        int kingY = SourceBoard.ReturnPosition(f).Item2;

                        Side side = f.Side == Side.WHITE ? Side.BLACK : Side.WHITE;

                        if (kingX == 'e' && (kingY == 1 || kingY == 8))
                        {
                            if (IsColorFigureAttack(this[kingX + kingY.ToString(CultureInfo.InvariantCulture)], side))
                                if (SourceBoard["a" + kingY].GetType() == typeof(FigureRook))
                                    Castling(moves, SourceBoard, f, SourceBoard["a" + kingY]);
                                else if (SourceBoard["h" + kingY].GetType() == typeof(FigureRook))
                                    Castling(moves, SourceBoard, f, SourceBoard["h" + kingY]);
                        }
                        if (f.Side == Side.WHITE)
                        {
                            _whiteKing = (i.ToString(CultureInfo.InvariantCulture) + j);
                        }

                        if (f.Side == Side.BLACK)
                        {
                            _blackKing = (i.ToString(CultureInfo.InvariantCulture) + j);
                        }
                    }
                }
            }
            if (!isRecursive)
            {
                foreach (var move in AllPossibleMoves)
                {
                    Board newBoard = SourceBoard.Clone();
                    var additionalMoves = new List<Move> { move };
                    newBoard.ApplyMoves(additionalMoves);
                    var newAttackMap = new AttackMap(moves.Concat(additionalMoves).ToList(), newBoard, true);
                    var figure = SourceBoard[move.From];
                    if ((figure.Side == Side.WHITE && newAttackMap.IsCheckWhite)
                      || (figure.Side == Side.BLACK && newAttackMap.IsCheckBlack))
                    {
                        Attackers[Board.GetCoords(move.To).Item1, Board.GetCoords(move.To).Item2].Remove(figure);
                    }
                }
            }
        }

        private void KingKnightStep(Board board, Figure f, char x, int y)
        {
            Figure f1 = board[x.ToString(CultureInfo.InvariantCulture) + y];
            if (f1.GetType() == typeof(FigureNone))
                Attackers[x - 'a', y - 1].Add(f);
            else if (f1.Side != f.Side)
                Attackers[x - 'a', y - 1].Add(f);
        }

        private void SouthWest(Board board, char i, int j, Figure f)
        {
            int l = j - 1;
            var k = (char)(i - 1);
            for (; k >= 'a' && l >= 1; )
            {
                Figure f1 = board[k.ToString(CultureInfo.InvariantCulture) + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k--;
                    l--;
                }
                else
                {
                    if (f1.Side != f.Side)
                    {
                        Attackers[k - 'a', l - 1].Add(f);
                    }
                    break;
                }
            }            

        }

        private void NorthWest(Board board, char i, int j, Figure f)
        {
            int l = j + 1;
            var k = (char)(i - 1);
            for (; k >= 'a' && l <= Board.BoardSize; )
            {
                Figure f1 = board[k.ToString(CultureInfo.InvariantCulture) + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k--;
                    l++;
                }
                else if (f1.Side != f.Side)
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
            var k = (char)(i + 1);
            for (; k <= 'h' && l >= 1; )
            {
                Figure f1 = board[k.ToString(CultureInfo.InvariantCulture) + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k++;
                    l--;
                }
                else if (f1.Side != f.Side)
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
            var k = (char)(i + 1);
            for (; k <= 'h' && l <= Board.BoardSize; )
            {
                Figure f1 = board[k.ToString(CultureInfo.InvariantCulture) + l];

                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', l - 1].Add(f);
                    k++;
                    l++;
                }
                else if (f1.Side != f.Side)
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
            for (var k = (char)(i - 1); k >= 'a'; k--)
            {
                Figure f1 = board[k.ToString(CultureInfo.InvariantCulture) + j];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', j - 1].Add(f);
                }
                else if (f1.Side != f.Side)
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
            for (var k = (char)(i + 1); k <= 'h'; k++)
            {
                Figure f1 = board[k.ToString(CultureInfo.InvariantCulture) + j];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[k - 'a', j - 1].Add(f);
                }
                else if (f1.Side != f.Side)
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
                Figure f1 = board[i.ToString(CultureInfo.InvariantCulture) + k];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[i - 'a', k - 1].Add(f);
                }
                else if (f1.Side != f.Side)
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
                Figure f1 = board[i.ToString(CultureInfo.InvariantCulture) + k];
                if (f1.GetType() == typeof(FigureNone))
                {
                    Attackers[i - 'a', k - 1].Add(f);
                }
                else if (f1.Side != f.Side)
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

        public bool IsPat
        {
            get
            {
                if (WhitePossibleMoves.Count() == 0 && !IsCheckWhite || BlackPossibleMoves.Count() == 0 && !IsCheckBlack)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsDraw
        {
            get
            {
                int rangerFiguresWhite = 0;
                int rangerFiguresBlack = 0;
                foreach (var element in SourceBoard.Cells)
                {
                    if (element.GetType() == typeof(FigureBishop) || element.GetType() == typeof(FigureKnight) ||
                        element.GetType() == typeof(FigureQueen) || element.GetType() == typeof(FigureRook))
                    {
                        if (element.Side == Side.WHITE)
                        {
                            rangerFiguresWhite++;
                        }
                        else
                        {
                            rangerFiguresBlack++;
                        }
                    }
                }
                if (rangerFiguresBlack < 2 && rangerFiguresWhite < 2)
                {
                    return true;
                }
                return false;
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

        private void Castling(List<Move> moves, Board board, Figure king, Figure rook)
        {
            int rows = 0;
            if (king.Side == Side.WHITE)
                rows = 1;
            else if (king.Side == Side.BLACK)
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
                foreach (Move move in moves)
                {
                    if (!move.From.Contains(rookX + rookY.ToString(CultureInfo.InvariantCulture)))
                    {
                        foreach (Move move2 in moves)
                            if (!move2.From.Contains(kingX + kingY.ToString(CultureInfo.InvariantCulture)))
                            {
                                CastlingTest(board, king, rook, rows, rookX);
                            }
                            else
                                return;
                    }
                    else
                        return;
                }
            else
            {
                CastlingTest(board, king, rook, rows, rookX);
            }
        }

        private void CastlingTest(Board board, Figure king, Figure rook, int rows, char rookX)
        {
            if (rookX == 'a')
            {
                if (board["b" + rows].GetType() == typeof(FigureNone) &&
                    board["c" + rows].GetType() == typeof(FigureNone) &&
                    board["d" + rows].GetType() == typeof(FigureNone) &&
                    IsColorFigureAttack(this["c" + rows], king.Side) &&
                    // Attackers['c' - 'a', rows - 1]
                    IsColorFigureAttack(this["d" + rows], king.Side) )
                //   Attackers['d' - 'a', rows - 1].Contains(new Figure(side)))
                {
                    Attackers['c' - 'a', rows - 1].Add(king);
                    Attackers['d' - 'a', rows - 1].Add(rook);
                }
            }
            else if (rookX == 'h')
            {
                if (board["f" + rows].GetType() == typeof(FigureNone) &&
                    board["g" + rows].GetType() == typeof(FigureNone) &&
                    IsColorFigureAttack(this["f" + rows], king.Side) &&
                    IsColorFigureAttack(this["g" + rows], king.Side))
                {
                    Attackers['g' - 'a', rows - 1].Add(king);
                    Attackers['f' - 'a', rows - 1].Add(rook);
                }
            }
        }

        private bool IsColorFigureAttack(IEnumerable<Figure> figure, Side side)
        {
            return figure.All(t => t.Side == side);
        }

        private void PassedPawn(IReadOnlyList<Move> moves, Board board, Figure pawn)
        {
            int rowFrom;
            int rowTo;
            int rows;
            var pawnX = board.ReturnPosition(pawn).Item1;
            var pawnY = board.ReturnPosition(pawn).Item2;
            switch (pawn.Side)
            {
                case Side.BLACK:
                    rows = 4;
                    rowFrom = rows - 2;
                    rowTo = rows - 1;
                    break;
                case Side.WHITE:
                    rows = 5;
                    rowFrom = rows + 2;
                    rowTo = rows + 1;
                    break;
                default:
                    return;
            }

            var cell = new List<string>();
            if (pawnX != 'a')
                cell.Add(((char) (pawnX - 1)).ToString(CultureInfo.InvariantCulture) + rows);
            if (pawnX != 'h')
                cell.Add(((char) (pawnX + 1)).ToString(CultureInfo.InvariantCulture) + rows);

            foreach (var c in cell)
            {
                if (board[c].GetType() == typeof (FigurePawn) && board[c].Side != pawn.Side &&
                    moves[moves.Count - 1].To == c && moves[moves.Count - 1].From ==
                    c[0].ToString(CultureInfo.InvariantCulture) + rowFrom && pawnY == rows)
                    Attackers[c[0] - 'a', rowTo - 1].Add(pawn);
            }
        }

        public IEnumerable<Move> AllPossibleMoves
        {
            get
            {
                var moves = new List<Move>();
                for (char i = 'a'; i <= 'h'; i++)
                {
                    for (int j = 1; j <= Board.BoardSize; j++)
                    {
                        var currentCell = i.ToString(CultureInfo.InvariantCulture) + j;
                        foreach (var figure in this[currentCell])
                        {
                            var move = new Move {From = _figuresPosition[figure], To = currentCell};
                            moves.Add(move);
                        }
                    }
                }
                return moves;
            }
        }

        public IEnumerable<Move> WhitePossibleMoves
        {
            get
            {
                return AllPossibleMoves.Where(move => SourceBoard[move.From].Side == Side.WHITE);
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
                return AllPossibleMoves.Where(move => SourceBoard[move.From].Side == Side.BLACK);
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

        public bool IsMateWhite
        {
            get 
            {
                return WhitePossibleMoves.Count() == 0 && IsCheckWhite;
            }
        }

        public bool IsMateBlack
        {
            get
            {
                return BlackPossibleMoves.Count() == 0 && IsCheckBlack;
            }
        }

        public List<string> MoveVariants(string cell)
        {
            var moveVariants = new List<string>();
            for (int i = 0; i < Board.BoardSize; i++)
            {
                for (int j = 0; j < Board.BoardSize; j++)
                {
                    if (Attackers[i, j].Contains(SourceBoard[cell]))
                        moveVariants.Add((char) ('a' + i) + (j + 1).ToString(CultureInfo.InvariantCulture));
                }
            }
            return moveVariants;
        }
    }
}
