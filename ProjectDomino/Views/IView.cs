using System.Collections.Generic;
using DominoConsoleMVC.Models;

namespace DominoConsoleMVC.Interfaces
{
    public interface IView
    {
        void ShowWelcome();
        void ShowBoard(IBoard board);
        void ShowPlayerHand(IPlayer player);
        void ShowMessage(string message);
        void ShowScores(IEnumerable<IPlayer> players);
        string AskPlayerChoice(IPlayer player, List<(DominoTile tile, bool playLeft)> moves, bool canDraw);
    }
}
