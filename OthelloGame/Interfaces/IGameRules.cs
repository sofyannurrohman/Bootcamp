using OthelloConsoleMVC.Models;

namespace OthelloConsoleMVC.Interfaces;
public interface IGameRules
    {
        List<(int row, int col)> GetValidMoves(IBoard board, PieceColor playerColor);
        bool IsMoveValid(IBoard board, int row, int col, PieceColor color);
    }