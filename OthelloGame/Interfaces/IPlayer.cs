using OthelloConsoleMVC.Models;

namespace OthelloConsoleMVC.Interfaces;
public interface IPlayer
    {
        string Name { get; }
        PieceColor Color { get; }
        (int row, int col) SelectMove(IBoard board, List<(int row, int col)> validMoves);
    }