namespace LudoConsoleMVC.Models;

public enum PieceColor { Red, Blue, Green, Yellow }

public class Piece
{
    public PieceColor Color { get; set; }
    public int Position { get; set; } // 0..N positions on track
    public bool IsAtHome => Position == -1; // -1 = still in base/home
    public bool IsFinished => Position == LudoBoard.HomePosition;


}