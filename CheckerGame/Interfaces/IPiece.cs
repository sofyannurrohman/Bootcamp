using CheckersConsoleMVC.Models;

namespace CheckersConsoleMVC.Interfaces
{
    public interface IPiece
    {
        PieceColor Color { get; }
        bool IsKing { get; set; }
    }
}
