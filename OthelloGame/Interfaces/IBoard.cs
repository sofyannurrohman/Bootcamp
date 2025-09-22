using OthelloConsoleMVC.Models;

namespace OthelloConsoleMVC.Interfaces;
public interface IBoard
    {
        int Size { get; }
        Piece? GetPiece(int row, int col);
        void PlacePiece(int row, int col, PieceColor color);
        List<(int row, int col)> GetValidMoves(PieceColor playerColor);
        void FlipPieces(int row, int col, PieceColor playerColor);
    }