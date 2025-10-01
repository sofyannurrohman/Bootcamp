using System.Collections.Generic;
using System.Linq;
using BlockDomino.Interfaces;

namespace BlockDomino.Models
{
    public class Player : IPlayer
    {
        public string Name { get; private set; }
        public byte Score { get; set; }
        public List<IDominoTile> Hand { get; private set; }

        public Player(string name)
        {
            Name = name;
            Hand = new List<IDominoTile>();
            Score = 0;
        }

        public bool PlayDominoTile(IDominoTile dominoTile)
        {
            var toRemove = Hand.FirstOrDefault(d =>
                d.PipLeft == dominoTile.PipLeft && d.PipRight == dominoTile.PipRight
            );

            if (toRemove != null)
            {
                Hand.Remove(toRemove);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Name} (Score: {Score})";
        }
    }
}
