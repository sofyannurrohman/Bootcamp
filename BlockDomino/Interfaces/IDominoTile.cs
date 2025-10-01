using BlockDomino.Enums;

namespace BlockDomino.Interfaces
{
    public interface IDominoTile
    {
        byte PipLeft { get; }
        byte PipRight { get; }
        bool IsDouble { get; }
        Orientation Orientation { get; set; }
    }
}
