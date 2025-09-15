using ChessGame.Models;
using System.Collections.Generic;

namespace ChessGame.Services
{
    public class EnPassantService
    {
        public (int row, int col)? EnPassantTarget { get; set; }

        public List<(int row, int col)> GetEnPassantMoves(int row, int col, PieceColor color)
        {
            var moves = new List<(int, int)>();
            
            if (!EnPassantTarget.HasValue)
                return moves;

            var ep = EnPassantTarget.Value;
            int direction = color == PieceColor.White ? -1 : 1;
            
            if (Math.Abs(ep.col - col) == 1 && ep.row == row + direction)
            {
                moves.Add(ep);
            }

            return moves;
        }

        public void UpdateEnPassantTarget((int row, int col) from, (int row, int col) to, ChessPiece piece)
        {
            if (piece.Type == PieceType.Pawn && Math.Abs(to.row - from.row) == 2)
            {
                int midRow = (from.row + to.row) / 2;
                EnPassantTarget = (midRow, from.col);
            }
            else
            {
                EnPassantTarget = null;
            }
        }
    }
}   