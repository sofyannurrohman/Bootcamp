using BlockDomino.Enums;
using BlockDomino.Interfaces;

namespace BlockDomino.Models
{
    public class DominoTile : IDominoTile
    {
        public byte PipLeft { get; private set; }
        public byte PipRight { get; private set; }
        public bool IsDouble => PipLeft == PipRight;
        public Orientation Orientation { get; set; }

        public DominoTile(byte pipLeft, byte pipRight)
        {
            PipLeft = pipLeft;
            PipRight = pipRight;
            Orientation = Orientation.HORIZONTAL;
        }

        public DominoTile Flip()
        {
            return new DominoTile(PipRight, PipLeft)
            {
                Orientation = this.Orientation
            };
        }

        public override string ToString()
        {
            return $"[{PipLeft}|{PipRight}]";
        }
    }
}
