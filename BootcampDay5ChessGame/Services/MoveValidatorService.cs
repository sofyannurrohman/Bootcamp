using ChessGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ChessGame.Interfaces;
namespace ChessGame.Services
{
    public class MoveValidatorService : IBoardService
    {
        private readonly Board _board;
        private CastlingService _castlingService;
        private readonly EnPassantService _enPassantService;

        // Implement the interface property
        public Board Board => _board;

        public MoveValidatorService(Board board, EnPassantService enPassantService)
        {
            _board = board;
            _enPassantService = enPassantService;
        }

        public void SetCastlingService(CastlingService castlingService)
        {
            _castlingService = castlingService;
        }

        // Implement interface methods (make sure they're public)
        public bool IsSquareAttacked((int row, int col) square, PieceColor attackerColor)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    var p = _board.Squares[r, c];
                    if (p != null && p.Color == attackerColor)
                    {
                        var moves = GetRawMoves(r, c, p);
                        if (moves.Contains(square)) return true;
                    }
                }
            }
            return false;
        }

        public bool IsInCheck(PieceColor color)
        {
            var kingPos = FindKingPosition(color);
            if (kingPos.row == -1) return false;

            var opponent = color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            return IsSquareAttacked(kingPos, opponent);
        }

        // This must be public to implement the interface
        public List<(int row, int col)> GetRawMoves(int row, int col, ChessPiece piece)
        {
            return piece.Type switch
            {
                PieceType.Pawn => GetPawnMoves(row, col, piece.Color),
                PieceType.Rook => GetRookMoves(row, col, piece.Color),
                PieceType.Knight => GetKnightMoves(row, col, piece.Color),
                PieceType.Bishop => GetBishopMoves(row, col, piece.Color),
                PieceType.Queen => GetQueenMoves(row, col, piece.Color),
                PieceType.King => GetKingMoves(row, col, piece.Color),
                _ => new List<(int row, int col)>()
            };
        }

        // Your other methods (these can remain private)
        public bool IsValidMove((int row, int col) from, (int row, int col) to, PieceColor currentTurn)
        {
            var piece = _board.Squares[from.row, from.col];
            if (piece == null || piece.Color != currentTurn)
                return false;

            var possibleMoves = GetPossibleMoves(from.row, from.col, currentTurn);
            return possibleMoves.Contains(to);
        }

        public List<(int row, int col)> GetPossibleMoves(int row, int col, PieceColor currentTurn)
        {
            var piece = _board.Squares[row, col];
            var moves = new List<(int, int)>();

            if (piece == null || piece.Color != currentTurn)
                return moves;

            // Get basic moves for the piece type
            moves.AddRange(GetBasicMoves(row, col, piece));

            // Add special moves
            moves.AddRange(_castlingService.GetCastlingMoves(row, col, piece.Color, currentTurn));
            moves.AddRange(_enPassantService.GetEnPassantMoves(row, col, piece.Color));

            // Filter out moves that leave the king in check
            return moves.Where(move => IsSafeMove(row, col, move, piece.Color)).ToList();
        }

        private List<(int, int)> GetBasicMoves(int row, int col, ChessPiece piece)
        {
            return piece.Type switch
            {
                PieceType.Pawn => GetPawnMoves(row, col, piece.Color),
                PieceType.Rook => GetRookMoves(row, col, piece.Color),
                PieceType.Knight => GetKnightMoves(row, col, piece.Color),
                PieceType.Bishop => GetBishopMoves(row, col, piece.Color),
                PieceType.Queen => GetQueenMoves(row, col, piece.Color),
                PieceType.King => GetKingMoves(row, col, piece.Color),
                _ => new List<(int, int)>()
            };
        }

        private bool IsSafeMove(int fromRow, int fromCol, (int row, int col) to, PieceColor color)
        {
            var piece = _board.Squares[fromRow, fromCol];
            var captured = _board.Squares[to.row, to.col];

            // Simulate move
            _board.Squares[to.row, to.col] = piece;
            _board.Squares[fromRow, fromCol] = null;

            bool safe = !IsInCheck(color);

            // Undo move
            _board.Squares[fromRow, fromCol] = piece;
            _board.Squares[to.row, to.col] = captured;

            return safe;
        }

        private (int row, int col) FindKingPosition(PieceColor color)
        {
            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    if (_board.Squares[r, c]?.Type == PieceType.King && _board.Squares[r, c].Color == color)
                        return (r, c);
            return (-1, -1);
        }

        // Individual piece move generators
        private List<(int, int)> GetPawnMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            int direction = (color == PieceColor.White) ? -1 : 1;
            int nextRow = row + direction;

            // Forward 1
            if (IsInsideBoard(nextRow, col) && _board.Squares[nextRow, col] == null)
            {
                moves.Add((nextRow, col));

                // Forward 2 if starting rank
                int startRow = (color == PieceColor.White) ? 6 : 1;
                int doubleRow = row + 2 * direction;
                if (row == startRow && IsInsideBoard(doubleRow, col) && _board.Squares[doubleRow, col] == null)
                {
                    moves.Add((doubleRow, col));
                }
            }

            // Captures
            foreach (var dc in new[] { -1, 1 })
            {
                int c = col + dc;
                if (IsInsideBoard(nextRow, c))
                {
                    var target = _board.Squares[nextRow, c];
                    if (target != null && target.Color != color)
                    {
                        moves.Add((nextRow, c));
                    }
                }
            }

            return moves;
        }

        private List<(int, int)> GetRookMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            for (int r = row - 1; r >= 0; r--) if (!AddMoveOrStop(moves, r, col, color)) break;
            for (int r = row + 1; r < 8; r++) if (!AddMoveOrStop(moves, r, col, color)) break;
            for (int c = col - 1; c >= 0; c--) if (!AddMoveOrStop(moves, row, c, color)) break;
            for (int c = col + 1; c < 8; c++) if (!AddMoveOrStop(moves, row, c, color)) break;
            return moves;
        }

        private List<(int, int)> GetKnightMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            int[,] offsets = {
                { -2, -1 }, { -2, 1 },
                { -1, -2 }, { -1, 2 },
                { 1, -2 },  { 1, 2 },
                { 2, -1 },  { 2, 1 }
            };

            for (int i = 0; i < offsets.GetLength(0); i++)
            {
                int r = row + offsets[i, 0];
                int c = col + offsets[i, 1];
                if (IsInsideBoard(r, c))
                {
                    var target = _board.Squares[r, c];
                    if (target == null || target.Color != color) moves.Add((r, c));
                }
            }

            return moves;
        }

        private List<(int, int)> GetBishopMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            for (int r = row - 1, c = col - 1; r >= 0 && c >= 0; r--, c--) if (!AddMoveOrStop(moves, r, c, color)) break;
            for (int r = row - 1, c = col + 1; r >= 0 && c < 8; r--, c++) if (!AddMoveOrStop(moves, r, c, color)) break;
            for (int r = row + 1, c = col - 1; r < 8 && c >= 0; r++, c--) if (!AddMoveOrStop(moves, r, c, color)) break;
            for (int r = row + 1, c = col + 1; r < 8 && c < 8; r++, c++) if (!AddMoveOrStop(moves, r, c, color)) break;
            return moves;
        }

        private List<(int, int)> GetQueenMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            moves.AddRange(GetRookMoves(row, col, color));
            moves.AddRange(GetBishopMoves(row, col, color));
            return moves;
        }

        private List<(int, int)> GetKingMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            int[,] offsets = {
                { -1, -1 }, { -1, 0 }, { -1, 1 },
                { 0, -1 },            { 0, 1 },
                { 1, -1 },  { 1, 0 }, { 1, 1 }
            };

            for (int i = 0; i < offsets.GetLength(0); i++)
            {
                int r = row + offsets[i, 0];
                int c = col + offsets[i, 1];
                if (IsInsideBoard(r, c))
                {
                    var target = _board.Squares[r, c];
                    if (target == null || target.Color != color)
                        moves.Add((r, c));
                }
            }

            return moves;
        }

        private bool AddMoveOrStop(List<(int, int)> moves, int row, int col, PieceColor color)
        {
            if (!IsInsideBoard(row, col)) return false;

            var target = _board.Squares[row, col];
            if (target == null)
            {
                moves.Add((row, col));
                return true;
            }
            else if (target.Color != color)
            {
                moves.Add((row, col));
            }
            return false;
        }

        private bool IsInsideBoard(int row, int col) => row >= 0 && row < 8 && col >= 0 && col < 8;
    }
}