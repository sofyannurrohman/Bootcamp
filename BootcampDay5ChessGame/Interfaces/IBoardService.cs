using ChessGame.Models;
using System.Collections.Generic;

namespace ChessGame.Interfaces
{
    public interface IBoardService
    {
        Board Board { get; }
        bool IsSquareAttacked((int row, int col) square, PieceColor attackerColor);
        bool IsInCheck(PieceColor color);
        List<(int row, int col)> GetRawMoves(int row, int col, ChessPiece piece);
    }
}