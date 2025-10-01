using System.Collections.Generic;
using System.Linq;
using BlockDomino.Interfaces;

namespace BlockDomino.Models
{
    public class BotPlayer : IPlayer
    {
        public string Name { get; private set; }
        public byte Score { get; set; }
        public List<IDominoTile> Hand { get; private set; }

        public BotPlayer(string name = "Computer")
        {
            Name = name;
            Hand = new List<IDominoTile>();
            Score = 0;
        }

        public bool PlayDominoTile(IDominoTile dominoTile)
        {
            var match = Hand.FirstOrDefault(d =>
                d.PipLeft == dominoTile.PipLeft &&
                d.PipRight == dominoTile.PipRight
            );

            if (match != null)
            {
                Hand.Remove(match);
                return true;
            }
            return false;
        }

        public IDominoTile ChooseMove(byte[] openEnds)
        {
            if (openEnds.Length == 0)
                return Hand.FirstOrDefault();

            return Hand.FirstOrDefault(d =>
                openEnds.Contains(d.PipLeft) ||
                openEnds.Contains(d.PipRight));
        }

        public override string ToString() => $"{Name} (Score: {Score})";
    }
}
