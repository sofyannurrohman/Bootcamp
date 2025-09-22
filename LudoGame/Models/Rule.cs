using LudoConsoleMVC.Interfaces;
using LudoConsoleMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace LudoConsoleMVC.Rules
{
    public class LudoRules : IGameRules
    {
        public List<Piece> GetMovablePieces(IBoard board, IPlayer player, int diceRoll)
        {
            return board.GetPieces(player.Color)
                        .Where(p => !p.IsFinished && (p.Position != -1 || diceRoll == 6))
                        .ToList();
        }

        public bool IsGameOver(IBoard board, List<IPlayer> players)
        {
            return players.All(p => board.GetPieces(p.Color).All(pc => pc.IsFinished));
        }
    }
}
