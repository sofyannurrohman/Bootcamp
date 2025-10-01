using System.Collections.Generic;
using System.Linq;
using BlockDomino.Interfaces;

namespace BlockDomino.Models
{
    public class Board : IBoard
    {
        public List<IDominoTile> DominoTiles { get; private set; }

        public Board()
        {
            DominoTiles = new List<IDominoTile>();
        }

        public void PlaceLeft(IDominoTile tile)
        {
            DominoTiles.Insert(0, tile);
        }

        public void PlaceRight(IDominoTile tile)
        {
            DominoTiles.Add(tile);
        }

        public byte[] GetOpenEnds()
        {
            if (DominoTiles.Count == 0)
                return new byte[0];

            var left = DominoTiles.First();
            var right = DominoTiles.Last();
            return new byte[] { left.PipLeft, right.PipRight };
        }

        public override string ToString()
        {
            if (DominoTiles.Count == 0)
                return "<empty board>";

            return string.Join(" ", DominoTiles.Select(d => d.ToString()));
        }
    }
}
