using System.Collections.Generic;

namespace BlockDomino.Interfaces
{
    public interface IPlayer
    {
        string Name { get; }
        byte Score { get; set; }
        List<IDominoTile> Hand { get; }
        bool PlayDominoTile(IDominoTile dominoTile);
    }
}
