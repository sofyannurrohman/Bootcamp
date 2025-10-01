using System.Collections.Generic;

namespace BlockDomino.Interfaces
{
    public interface IBoard
    {
        List<IDominoTile> DominoTiles { get; }
    }
}
