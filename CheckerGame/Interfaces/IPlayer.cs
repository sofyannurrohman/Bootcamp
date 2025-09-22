using System.Collections.Generic;
using CheckersConsoleMVC.Models;

namespace CheckersConsoleMVC.Interfaces
{
    public interface IPlayer
    {
        string Name { get; }
        bool IsHuman { get; }
        PieceColor Color { get; }        // Player’s side (Red or Black)
        List<Piece> Pieces { get; }      // Player’s owned pieces (sync with Board)

        // 🔑 Used by GameController for moves
        (int row, int col) SelectPiece(IBoard board);   // Choose which piece to move
        Move SelectMove(List<Move> moves);              // Choose which move to perform
    }
}
