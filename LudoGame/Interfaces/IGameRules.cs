using LudoConsoleMVC.Interfaces;
using LudoConsoleMVC.Models;

public interface IGameRules
    {
        List<Piece> GetMovablePieces(IBoard board, IPlayer player, int diceRoll);
        bool IsGameOver(IBoard board, List<IPlayer> players);
    }