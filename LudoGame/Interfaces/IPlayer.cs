using LudoConsoleMVC.Interfaces;
using LudoConsoleMVC.Models;

public interface IPlayer
    {
        string Name { get; }
        PieceColor Color { get; }
        int RollDice();
        Piece SelectPieceToMove(List<Piece> movablePieces, int diceRoll, IBoard board);

    }
