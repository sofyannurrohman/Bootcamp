using System.Collections.Generic;

namespace BlockDomino.Interfaces
{
    public interface IDeck
    {
        List<IDominoTile> DominoTiles { get; }
    }
}
