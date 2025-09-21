using System.Collections.Generic;
using DominoConsoleMVC.Models;

namespace DominoConsoleMVC.Interfaces
{
    public interface IGameRules
    {
        bool IsPlayable(IBoard board, DominoTile tile, out bool playLeft);
        List<(DominoTile tile, bool playLeft)> GetPlayableMoves(IBoard board, IPlayer player);
    }
}
