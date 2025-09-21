using CheckersConsoleMVC.Models;

namespace CheckersConsoleMVC.Interfaces;

public interface IBoard
{
    int Size { get; }
    Piece? GetPiece(int row, int col);
    void MovePiece(Move move);
    void RemovePiece(int row, int col);
    void InitializeBoard();
    IEnumerable<Piece> GetAllPieces();
}