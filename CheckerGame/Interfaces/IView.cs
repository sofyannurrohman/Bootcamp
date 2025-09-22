using CheckersConsoleMVC.Models;

namespace CheckersConsoleMVC.Interfaces;
public interface IView
    {
        void ShowBoard(IBoard board);
        void ShowMessage(string message);
        Move AskPlayerMove(IPlayer player, List<Move> validMoves);
        void ShowWinner(IPlayer player);
    }