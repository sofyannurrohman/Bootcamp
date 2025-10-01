using System.Collections.Generic;
using BlockDomino.Interfaces;

namespace BlockDomino.Models
{
    public class Deck : IDeck
    {
        public List<IDominoTile> DominoTiles { get; private set; }

        public Deck()
        {
            DominoTiles = new List<IDominoTile>();
        }
    }
}
