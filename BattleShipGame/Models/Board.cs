using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Models
{
    public partial class Board
    {
        public const int Size = 10;
        private Cell[,] grid = new Cell[Size, Size];
        private static readonly Random rng = new();
        private (int Row, int Col)? lastAttack = null;

        public List<Ship> Ships { get; set; } = new();

        public Board()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    grid[r, c] = new Cell { Row = r, Col = c };
        }

        public bool PlaceShip(Ship ship, int startRow, int startCol, Orientation orientation)
        {
            var positions = new List<(int, int)>();
            for (int i = 0; i < ship.Size; i++)
            {
                int r = orientation == Orientation.Vertical ? startRow + i : startRow;
                int c = orientation == Orientation.Horizontal ? startCol + i : startCol;

                if (r >= Size || c >= Size || grid[r, c].Status != CellStatus.Empty)
                    return false;

                positions.Add((r, c));
            }

            ship.Positions = positions;
            ship.Orientation = orientation;

            foreach (var pos in positions)
                grid[pos.Item1, pos.Item2].Status = CellStatus.Ship;

            Ships.Add(ship);
            return true;
        }

        public void PlaceShipsRandomly()
        {
            var types = new[] { ShipType.Carrier, ShipType.Battleship, ShipType.Cruiser, ShipType.Submarine, ShipType.Destroyer };
            foreach (var type in types)
            {
                bool placed = false;
                while (!placed)
                {
                    int row = rng.Next(Size);
                    int col = rng.Next(Size);
                    Orientation orientation = rng.Next(2) == 0 ? Orientation.Horizontal : Orientation.Vertical;
                    var ship = new Ship { Type = type };
                    placed = PlaceShip(ship, row, col, orientation);
                }
            }
        }

        public CellStatus Attack(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                throw new ArgumentOutOfRangeException(nameof(row), "Attack coordinates out of board range");

            lastAttack = (row, col);
            var cell = grid[row, col];

            if (cell.Status == CellStatus.Hit || cell.Status == CellStatus.Miss)
                return cell.Status;

            if (cell.Status == CellStatus.Ship)
            {
                cell.Status = CellStatus.Hit;
                var ship = Ships.First(s => s.Positions.Contains((row, col)));
                ship.Hits.Add((row, col));
                return CellStatus.Hit;
            }

            cell.Status = CellStatus.Miss;
            return CellStatus.Miss;
        }

        public void PrintFogOfWar(bool showOwnShips = false, List<(int Row, int Col)> suggestions = null)
        {
            string horizontalLine = "  +" + string.Concat(Enumerable.Repeat("---+", Size));

            // Column headers
            Console.Write("    ");
            for (int c = 0; c < Size; c++)
                Console.Write($"{c,2}  ");
            Console.WriteLine();
            Console.WriteLine(horizontalLine);

            for (int r = 0; r < Size; r++)
            {
                Console.Write($"{r,2} |");
                for (int c = 0; c < Size; c++)
                {
                    var cell = grid[r, c];
                    char symbol = '.';
                    ConsoleColor fgColor = ConsoleColor.Gray;
                    ConsoleColor bgColor = ConsoleColor.Black;

                    if (suggestions != null && suggestions.Contains((r, c)))
                    {
                        symbol = '*'; // suggested move
                        fgColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        switch (cell.Status)
                        {
                            case CellStatus.Hit:
                                symbol = 'X';
                                fgColor = ConsoleColor.Red;
                                break;
                            case CellStatus.Miss:
                                symbol = 'O';
                                fgColor = ConsoleColor.Blue;
                                break;
                            case CellStatus.Ship:
                                symbol = showOwnShips ? 'S' : '.';
                                fgColor = showOwnShips ? ConsoleColor.Green : ConsoleColor.Gray;
                                break;
                            case CellStatus.Empty:
                                symbol = '.';
                                fgColor = ConsoleColor.Gray;
                                break;
                        }
                    }

                    Console.ForegroundColor = fgColor;
                    Console.BackgroundColor = bgColor;
                    Console.Write($" {symbol} |");
                    Console.ResetColor();
                }
                Console.WriteLine();
                Console.WriteLine(horizontalLine);
            }
        }


        public bool AllShipsSunk() => Ships.All(s => s.IsSunk);

        public List<(int Row, int Col)> GetPriorityTargets()
        {
            var targets = new List<(int, int)>();

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    if (grid[r, c].Status != CellStatus.Empty) continue;

                    // Check neighboring cells for hits
                    bool nearHit = false;
                    if (r > 0 && grid[r - 1, c].Status == CellStatus.Hit) nearHit = true;
                    if (r < Size - 1 && grid[r + 1, c].Status == CellStatus.Hit) nearHit = true;
                    if (c > 0 && grid[r, c - 1].Status == CellStatus.Hit) nearHit = true;
                    if (c < Size - 1 && grid[r, c + 1].Status == CellStatus.Hit) nearHit = true;

                    if (nearHit) targets.Add((r, c));
                }
            }

            return targets;
        }
    }
}
