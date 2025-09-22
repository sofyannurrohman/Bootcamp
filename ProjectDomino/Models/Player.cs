using System.Collections.Generic;
using System.Linq;
using DominoConsoleMVC.Interfaces;


namespace DominoConsoleMVC.Models
{
    public class Player : IPlayer
    {
        public string Name { get; }
        public bool IsHuman { get; }
        public List<DominoTile> Hand { get; } = new();

        public Player(string name, bool isHuman)
        {
            Name = name;
            IsHuman = isHuman;
        }
        public int Score => Hand.Sum(t => t.Left + t.Right);
        public void AddTile(DominoTile tile) => Hand.Add(tile);
        public void RemoveTile(DominoTile tile) => Hand.Remove(tile);
        public bool HasNoTiles() => Hand.Count == 0;
    }
}