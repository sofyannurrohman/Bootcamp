using System.Collections.Generic;
using System.Text;
using DominoConsoleMVC.Interfaces;


namespace DominoConsoleMVC.Models
{
    public class Board : IBoard
    {
        private readonly LinkedList<DominoTile> _tiles = new();

        public bool IsEmpty => !_tiles.Any();
        public int LeftValue => IsEmpty ? -1 : _tiles.First.Value.Left;
        public int RightValue => IsEmpty ? -1 : _tiles.Last.Value.Right;

        public void PlaceLeft(DominoTile tile) => _tiles.AddFirst(tile);
        public void PlaceRight(DominoTile tile) => _tiles.AddLast(tile);
        public IReadOnlyList<DominoTile> GetTiles() => _tiles.ToList();
        public override string ToString()
        {
            if (IsEmpty)
                return "<empty>";

            return string.Join(" ", _tiles.Select(t => t.ToString()));
        }
    }

}