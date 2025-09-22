using System.Collections.Generic;
using DominoConsoleMVC.Interfaces;
using DominoConsoleMVC.Models;

namespace DominoConsoleMVC.Rules
{
    public class StandardDominoRules : IGameRules
    {
        // Accepts a play if either side matches, considering flipping if needed.
        public bool IsPlayable(IBoard board, DominoTile tile, out bool playLeft)
        {
            playLeft = false;

            if (board.IsEmpty)
            {
                // First tile can go anywhere, default: right side
                playLeft = false;
                return true;
            }

            if (tile.Left == board.LeftValue)
            {
                playLeft = true;
                return true;
            }
            if (tile.Right == board.LeftValue)
            {
                playLeft = true; // needs flip when placed on left
                return true;
            }

            if (tile.Left == board.RightValue)
            {
                playLeft = false;
                return true;
            }
            if (tile.Right == board.RightValue)
            {
                playLeft = false; // needs flip when placed on right
                return true;
            }

            return false;
        }

        public List<(DominoTile tile, bool playLeft)> GetPlayableMoves(IBoard board, IPlayer player)
        {
            var moves = new List<(DominoTile, bool)>();

            foreach (var tile in player.Hand)
            {
                if (IsPlayable(board, tile, out var playLeft))
                {
                    moves.Add((tile, playLeft));
                }
            }

            return moves;
        }
    }
}
