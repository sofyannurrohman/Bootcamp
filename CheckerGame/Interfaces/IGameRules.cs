using System.Collections.Generic;
using CheckersConsoleMVC.Models;

namespace CheckersConsoleMVC.Interfaces
{
    public interface IGameRules
    {
        bool IsValidMove(IBoard board, IPlayer player, Move move);
        List<Move> GetValidMoves(IBoard board, IPlayer player);
        bool HasAvailableCapture(IBoard board, int row, int col); // 🔑 enforce multiple jumps
        bool HasWon(IBoard board, IPlayer player);
    }
}
