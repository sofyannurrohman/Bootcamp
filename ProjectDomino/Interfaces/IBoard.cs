using DominoConsoleMVC.Models;

namespace DominoConsoleMVC.Interfaces;
 public interface IBoard
    {
        bool IsEmpty { get; }
         int LeftValue { get; }   // instead of LeftEnd
    int RightValue { get; } 
        void PlaceLeft(DominoTile tile);
        void PlaceRight(DominoTile tile);
        IReadOnlyList<DominoTile> GetTiles();
    }