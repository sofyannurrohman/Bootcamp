namespace Battleship.Models;

public enum CellStatus { Empty, Ship, Hit, Miss, AlreadyAttacked }

public class Cell
{
    public int Row { get; set; }
    public int Col { get; set; }
    public CellStatus Status { get; set; } = CellStatus.Empty;
}
