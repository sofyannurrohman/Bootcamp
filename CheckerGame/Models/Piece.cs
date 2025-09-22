using CheckersConsoleMVC.Interfaces;

namespace CheckersConsoleMVC.Models
{
    public enum PieceColor { Red, Black }

    public class Piece : IPiece
    {
        public PieceColor Color { get; }
        public bool IsKing { get; set; }

        public Piece(PieceColor color)
        {
            Color = color;
            IsKing = false;
        }
    }
}
