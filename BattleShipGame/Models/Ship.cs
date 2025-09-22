namespace Battleship.Models;

public enum ShipType { Carrier, Battleship, Cruiser, Submarine, Destroyer }
public enum Orientation { Horizontal, Vertical }

public class Ship
{
    public ShipType Type { get; set; }
    public Orientation Orientation { get; set; }
    public List<(int Row, int Col)> Positions { get; set; } = new();
    public HashSet<(int Row, int Col)> Hits { get; set; } = new();

    public int Size => Type switch
    {
        ShipType.Carrier => 5,
        ShipType.Battleship => 4,
        ShipType.Cruiser => 3,
        ShipType.Submarine => 3,
        ShipType.Destroyer => 2,
        _ => 0
    };

    public bool IsSunk => Hits.Count >= Positions.Count;
}
