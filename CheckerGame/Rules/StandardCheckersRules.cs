using CheckersConsoleMVC.Interfaces;
using CheckersConsoleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersConsoleMVC.Rules
{
    public class StandardCheckersRules : IGameRules
    {
        public bool IsValidMove(IBoard board, IPlayer player, Move move)
        {
            var piece = board.GetPiece(move.FromRow, move.FromCol);
            if (piece == null || piece.Color != player.Color) 
                return false;

            var validMoves = GetValidMoves(board, player);
            return validMoves.Any(m =>
                m.FromRow == move.FromRow &&
                m.FromCol == move.FromCol &&
                m.ToRow == move.ToRow &&
                m.ToCol == move.ToCol);
        }

        public List<Move> GetValidMoves(IBoard board, IPlayer player)
        {
            var allMoves = new List<Move>();

            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    var piece = board.GetPiece(row, col);
                    if (piece == null || piece.Color != player.Color) 
                        continue;

                    int direction = piece.Color == PieceColor.Red ? -1 : 1;
                    int[,] directions = piece.IsKing
                        ? new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } }
                        : new int[,] { { direction, 1 }, { direction, -1 } };

                    // Check captures first
                    var captures = GetCaptureMoves(board, row, col, directions, piece);
                    allMoves.AddRange(captures);

                    // If captures exist, we ignore normal moves (forced capture rule)
                    if (captures.Any()) 
                        continue;

                    // Otherwise, add simple moves
                    for (int i = 0; i < directions.GetLength(0); i++)
                    {
                        int dr = directions[i, 0];
                        int dc = directions[i, 1];
                        int newRow = row + dr;
                        int newCol = col + dc;

                        if (IsInside(board, newRow, newCol) && board.GetPiece(newRow, newCol) == null)
                        {
                            allMoves.Add(new Move(row, col, newRow, newCol));
                        }
                    }
                }
            }

            // If there are captures anywhere, only return those
            if (allMoves.Any(m => m.IsCapture))
                return allMoves.Where(m => m.IsCapture).ToList();

            return allMoves;
        }

        private List<Move> GetCaptureMoves(IBoard board, int row, int col, int[,] directions, Piece piece)
        {
            var captures = new List<Move>();

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dr = directions[i, 0];
                int dc = directions[i, 1];
                int enemyRow = row + dr;
                int enemyCol = col + dc;
                int landingRow = row + 2 * dr;
                int landingCol = col + 2 * dc;

                if (!IsInside(board, enemyRow, enemyCol) || !IsInside(board, landingRow, landingCol))
                    continue;

                var enemy = board.GetPiece(enemyRow, enemyCol);
                if (enemy != null && enemy.Color != piece.Color &&
                    board.GetPiece(landingRow, landingCol) == null)
                {
                    captures.Add(new Move(row, col, landingRow, landingCol, true, enemyRow, enemyCol));
                }
            }

            return captures;
        }

        public bool HasAvailableCapture(IBoard board, int row, int col)
        {
            var piece = board.GetPiece(row, col);
            if (piece == null) 
                return false;

            int direction = piece.Color == PieceColor.Red ? -1 : 1;
            int[,] directions = piece.IsKing
                ? new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } }
                : new int[,] { { direction, 1 }, { direction, -1 } };

            return GetCaptureMoves(board, row, col, directions, piece).Any();
        }

        public bool HasWon(IBoard board, IPlayer player)
        {
            var opponentColor = player.Color == PieceColor.Red ? PieceColor.Black : PieceColor.Red;
            var opponent = new DummyPlayer(opponentColor);

            var opponentMoves = GetValidMoves(board, opponent);
            return opponentMoves.Count == 0;
        }

        private bool IsInside(IBoard board, int row, int col) =>
            row >= 0 && row < board.Size && col >= 0 && col < board.Size;
    }

    // Dummy player for win checking
    public class DummyPlayer : IPlayer
    {
        public string Name => "Dummy";
        public bool IsHuman => false;
        public PieceColor Color { get; }
        public List<Piece> Pieces { get; } = new List<Piece>();

        public DummyPlayer(PieceColor color) => Color = color;

        public (int row, int col) SelectPiece(IBoard board) => (-1, -1);

        public Move SelectMove(List<Move> moves) => moves.FirstOrDefault() ?? new Move(-1, -1, -1, -1);
    }
}
