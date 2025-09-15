using ChessGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessGame.Services
{
    public class ChessGameService
    {
        public Board Board { get; private set; }
        public PieceColor CurrentTurn { get; private set; }

        // track castling rights
        private bool whiteKingMoved = false;
        private bool blackKingMoved = false;
        private bool whiteRookLeftMoved = false;
        private bool whiteRookRightMoved = false;
        private bool blackRookLeftMoved = false;
        private bool blackRookRightMoved = false;

        // track en passant target square (the square that can be captured en-passant)
        // null when no en-passant is available
        private (int row, int col)? enPassantTarget = null;

        public ChessGameService()
        {
            Board = new Board();
            CurrentTurn = PieceColor.White;
        }

        public bool MovePiece((int row, int col) from, (int row, int col) to)
        {
            var piece = Board.Squares[from.row, from.col];
            if (piece == null || piece.Color != CurrentTurn)
                return false;

            var possibleMoves = GetPossibleMoves(from.row, from.col);
            if (!possibleMoves.Contains(to))
                return false;

            // backup
            var captured = Board.Squares[to.row, to.col];

            // handle en passant capture (remove the pawn that moved two squares previously)
            if (piece.Type == PieceType.Pawn && enPassantTarget.HasValue && to == enPassantTarget.Value)
            {
                int direction = (piece.Color == PieceColor.White) ? 1 : -1; // opponent pawn is behind target
                Board.Squares[to.row - direction, to.col] = null;
            }

            // move piece
            Board.Squares[to.row, to.col] = piece;
            Board.Squares[from.row, from.col] = null;

            // handle pawn double-move (set en-passant target)
            if (piece.Type == PieceType.Pawn && Math.Abs(to.row - from.row) == 2)
            {
                int midRow = (from.row + to.row) / 2;
                enPassantTarget = (midRow, from.col);
            }
            else
            {
                enPassantTarget = null;
            }

            // handle pawn promotion (auto-queen for now — you can prompt user later)
            if (piece.Type == PieceType.Pawn && (to.row == 0 || to.row == 7))
            {
                PendingPromotion = to;

            }

            // handle castling (move rook accordingly)
            if (piece.Type == PieceType.King && Math.Abs(to.col - from.col) == 2)
            {
                if (to.col == 6) // kingside
                {
                    var rook = Board.Squares[to.row, 7];
                    Board.Squares[to.row, 5] = rook;
                    Board.Squares[to.row, 7] = null;
                }
                else if (to.col == 2) // queenside
                {
                    var rook = Board.Squares[to.row, 0];
                    Board.Squares[to.row, 3] = rook;
                    Board.Squares[to.row, 0] = null;
                }
            }

            // update castling rights flags
            if (piece.Type == PieceType.King)
            {
                if (piece.Color == PieceColor.White) whiteKingMoved = true;
                else blackKingMoved = true;
            }
            if (piece.Type == PieceType.Rook)
            {
                // these checks assume standard starting positions for rooks:
                // adjust if your Board initialization uses different rows
                if (from.row == 7 && from.col == 0) whiteRookLeftMoved = true;
                if (from.row == 7 && from.col == 7) whiteRookRightMoved = true;
                if (from.row == 0 && from.col == 0) blackRookLeftMoved = true;
                if (from.row == 0 && from.col == 7) blackRookRightMoved = true;
            }

            // prevent moving into check: if the move left the player's king in check, undo
            if (IsInCheck(CurrentTurn))
            {
                // undo move
                Board.Squares[from.row, from.col] = piece;
                Board.Squares[to.row, to.col] = captured;

                // if en-passant capture was performed, we should restore the captured pawn as well.
                // For simplicity, we won't attempt to reconstruct complicated past states here.
                // A more robust approach: simulate on a copied board.

                return false;
            }

            // swap turn
            CurrentTurn = (CurrentTurn == PieceColor.White) ? PieceColor.Black : PieceColor.White;
            return true;
        }
        public void PromotePawn((int row, int col) position, string newPiece)
        {
            var piece = Board.Squares[position.row, position.col];
            if (piece == null || piece.Type != PieceType.Pawn)
                return;

            PieceType promoteTo = newPiece switch
            {
                "Queen" => PieceType.Queen,
                "Rook" => PieceType.Rook,
                "Bishop" => PieceType.Bishop,
                "Knight" => PieceType.Knight,
                _ => PieceType.Queen
            };

            Board.Squares[position.row, position.col] = new ChessPiece(promoteTo, piece.Color);
            PendingPromotion = null;
        }

        public List<(int row, int col)> GetPossibleMoves(int row, int col)
        {
            var piece = Board.Squares[row, col];
            var moves = new List<(int, int)>();
            if (piece == null || piece.Color != CurrentTurn)
                return moves;

            switch (piece.Type)
            {
                case PieceType.Pawn:
                    moves.AddRange(GetPawnMoves(row, col, piece.Color));
                    break;
                case PieceType.Rook:
                    moves.AddRange(GetRookMoves(row, col, piece.Color));
                    break;
                case PieceType.Knight:
                    moves.AddRange(GetKnightMoves(row, col, piece.Color));
                    break;
                case PieceType.Bishop:
                    moves.AddRange(GetBishopMoves(row, col, piece.Color));
                    break;
                case PieceType.Queen:
                    moves.AddRange(GetQueenMoves(row, col, piece.Color));
                    break;
                case PieceType.King:
                    moves.AddRange(GetKingMoves(row, col, piece.Color));
                    break;
            }

            // Castling moves (only if king not currently in check)
            if (piece.Type == PieceType.King && !IsInCheck(piece.Color))
            {
                if (piece.Color == PieceColor.White && !whiteKingMoved)
                {
                    // kingside
                    if (!whiteRookRightMoved &&
                        Board.Squares[7, 5] == null && Board.Squares[7, 6] == null &&
                        !SquareAttacked((7, 5), PieceColor.Black) && !SquareAttacked((7, 6), PieceColor.Black))
                        moves.Add((7, 6));
                    // queenside
                    if (!whiteRookLeftMoved &&
                        Board.Squares[7, 1] == null && Board.Squares[7, 2] == null && Board.Squares[7, 3] == null &&
                        !SquareAttacked((7, 2), PieceColor.Black) && !SquareAttacked((7, 3), PieceColor.Black))
                        moves.Add((7, 2));
                }
                else if (piece.Color == PieceColor.Black && !blackKingMoved)
                {
                    // kingside
                    if (!blackRookRightMoved &&
                        Board.Squares[0, 5] == null && Board.Squares[0, 6] == null &&
                        !SquareAttacked((0, 5), PieceColor.White) && !SquareAttacked((0, 6), PieceColor.White))
                        moves.Add((0, 6));
                    // queenside
                    if (!blackRookLeftMoved &&
                        Board.Squares[0, 1] == null && Board.Squares[0, 2] == null && Board.Squares[0, 3] == null &&
                        !SquareAttacked((0, 2), PieceColor.White) && !SquareAttacked((0, 3), PieceColor.White))
                        moves.Add((0, 2));
                }
            }

            // En-passant: allow capture to enPassantTarget if it exists and is a valid capture square
            if (piece.Type == PieceType.Pawn && enPassantTarget.HasValue)
            {
                var ep = enPassantTarget.Value;
                // pawn must be adjacent in column and on correct row to capture en-passant
                int direction = (piece.Color == PieceColor.White) ? -1 : 1; // white moves up (-1)
                // white pawn must be on row 3 and black just moved from 1->3 (enPassantTarget row 2), adjust depending on your initial row scheme
                // We'll simply allow en-passant when the ep square is one diagonal away
                if (Math.Abs(ep.col - col) == 1 && ep.row == row + direction)
                {
                    moves.Add(ep);
                }
            }

            // Filter out moves that leave the king in check
            return moves.Where(move => IsSafeMove(row, col, move)).ToList();
        }
        public bool IsCheck()
        {
            return IsInCheck(CurrentTurn);
        }
        private bool IsSafeMove(int fromRow, int fromCol, (int row, int col) to)
        {
            var piece = Board.Squares[fromRow, fromCol];
            var captured = Board.Squares[to.row, to.col];

            // simulate on the real board; for production a cloned board is safer
            Board.Squares[to.row, to.col] = piece;
            Board.Squares[fromRow, fromCol] = null;

            bool safe = !IsInCheck(piece.Color);

            // undo
            Board.Squares[fromRow, fromCol] = piece;
            Board.Squares[to.row, to.col] = captured;

            return safe;
        }

        // Detect check
        public bool IsInCheck(PieceColor color)
        {
            // find king
            (int row, int col) kingPos = (-1, -1);
            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    if (Board.Squares[r, c]?.Type == PieceType.King && Board.Squares[r, c].Color == color)
                        kingPos = (r, c);

            if (kingPos.row == -1) return false; // no king (edge case)

            var opponent = (color == PieceColor.White) ? PieceColor.Black : PieceColor.White;
            return SquareAttacked(kingPos, opponent);
        }

        private bool SquareAttacked((int row, int col) square, PieceColor attackerColor)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    var p = Board.Squares[r, c];
                    if (p != null && p.Color == attackerColor)
                    {
                        var moves = GetRawMoves(r, c, attackerColor);
                        if (moves.Contains(square)) return true;
                    }
                }
            }
            return false;
        }

        // Detect checkmate
        public bool IsCheckmate(PieceColor color)
        {
            if (!IsInCheck(color)) return false;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    var piece = Board.Squares[r, c];
                    if (piece != null && piece.Color == color)
                    {
                        var legalMoves = GetPossibleMoves(r, c);
                        if (legalMoves.Count > 0)
                            return false;
                    }
                }
            }
            return true;
        }

        public bool IsStalemate(PieceColor color)
        {
            if (IsInCheck(color)) return false;

            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                {
                    var p = Board.Squares[r, c];
                    if (p != null && p.Color == color && GetPossibleMoves(r, c).Count > 0)
                        return false;
                }
            return true;
        }

        // helper to generate raw moves without king-safety filtering (used to check attacked squares)
        private List<(int, int)> GetRawMoves(int row, int col, PieceColor color)
        {
            var piece = Board.Squares[row, col];
            var moves = new List<(int, int)>();
            if (piece == null) return moves;

            switch (piece.Type)
            {
                case PieceType.Pawn: moves.AddRange(GetPawnMoves(row, col, color)); break;
                case PieceType.Rook: moves.AddRange(GetRookMoves(row, col, color)); break;
                case PieceType.Knight: moves.AddRange(GetKnightMoves(row, col, color)); break;
                case PieceType.Bishop: moves.AddRange(GetBishopMoves(row, col, color)); break;
                case PieceType.Queen: moves.AddRange(GetQueenMoves(row, col, color)); break;
                case PieceType.King: moves.AddRange(GetKingMoves(row, col, color)); break;
            }
            return moves;
        }

        // Pawn moves (forward, double at start, diagonal captures)
        private List<(int, int)> GetPawnMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            int direction = (color == PieceColor.White) ? -1 : 1;
            int nextRow = row + direction;

            // Forward 1
            if (IsInsideBoard(nextRow, col) && Board.Squares[nextRow, col] == null)
            {
                moves.Add((nextRow, col));

                // Forward 2 if starting rank
                int startRow = (color == PieceColor.White) ? 6 : 1;
                int doubleRow = row + 2 * direction;
                if (row == startRow && IsInsideBoard(doubleRow, col) && Board.Squares[doubleRow, col] == null)
                {
                    moves.Add((doubleRow, col));
                }
            }

            // Captures (including normal capture; en-passant handled in GetPossibleMoves)
            foreach (var dc in new[] { -1, 1 })
            {
                int c = col + dc;
                if (IsInsideBoard(nextRow, c))
                {
                    var target = Board.Squares[nextRow, c];
                    if (target != null && target.Color != color)
                    {
                        moves.Add((nextRow, c));
                    }
                }
            }

            return moves;
        }

        // Rook moves
        private List<(int, int)> GetRookMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            for (int r = row - 1; r >= 0; r--) if (!AddMoveOrStop(moves, r, col, color)) break;
            for (int r = row + 1; r < 8; r++) if (!AddMoveOrStop(moves, r, col, color)) break;
            for (int c = col - 1; c >= 0; c--) if (!AddMoveOrStop(moves, row, c, color)) break;
            for (int c = col + 1; c < 8; c++) if (!AddMoveOrStop(moves, row, c, color)) break;
            return moves;
        }

        // Knight moves
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
                    var target = Board.Squares[r, c];
                    if (target == null || target.Color != color) moves.Add((r, c));
                }
            }

            return moves;
        }

        // Bishop moves
        private List<(int, int)> GetBishopMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            for (int r = row - 1, c = col - 1; r >= 0 && c >= 0; r--, c--) if (!AddMoveOrStop(moves, r, c, color)) break;
            for (int r = row - 1, c = col + 1; r >= 0 && c < 8; r--, c++) if (!AddMoveOrStop(moves, r, c, color)) break;
            for (int r = row + 1, c = col - 1; r < 8 && c >= 0; r++, c--) if (!AddMoveOrStop(moves, r, c, color)) break;
            for (int r = row + 1, c = col + 1; r < 8 && c < 8; r++, c++) if (!AddMoveOrStop(moves, r, c, color)) break;
            return moves;
        }

        // Queen = Rook + Bishop
        private List<(int, int)> GetQueenMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            moves.AddRange(GetRookMoves(row, col, color));
            moves.AddRange(GetBishopMoves(row, col, color));
            return moves;
        }

        // King one-square moves (castling handled in GetPossibleMoves)
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
                    var target = Board.Squares[r, c];
                    if (target == null || target.Color != color)
                        moves.Add((r, c));
                }
            }

            return moves;
        }

        // Helper used by sliding pieces
        private bool AddMoveOrStop(List<(int, int)> moves, int row, int col, PieceColor color)
        {
            if (!IsInsideBoard(row, col)) return false;

            var target = Board.Squares[row, col];
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
        public (int row, int col)? PendingPromotion { get; private set; } = null;
        private bool IsInsideBoard(int row, int col) => row >= 0 && row < 8 && col >= 0 && col < 8;
    }
}
