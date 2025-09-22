using LudoConsoleMVC.Models;
namespace LudoConsoleMVC.Interfaces;

public interface IBoard
{
    void MovePiece(Piece piece, int steps);
    List<Piece> GetPieces(PieceColor color);
    Piece? GetPieceAtPosition(int position);
    void PrintBoard();
    int Size { get; }
    List<Piece> GetAllPieces();

    // Check if moving a piece by diceRoll will land on opponent
    bool WillCapture(Piece piece, int diceRoll);
}