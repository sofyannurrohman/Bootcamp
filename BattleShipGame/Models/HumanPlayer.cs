using Battleship.Interfaces;

using Battleship.Models;

public class HumanPlayer : IPlayer
{
    public string Name { get; set; }
    public Board Board { get; set; } = new();

    public (int Row, int Col) SelectAttack()
    {
        while (true)
        {
            Console.Write("Enter attack (row col): ");
            var input = Console.ReadLine()?.Split();
            if (input?.Length == 2 &&
                int.TryParse(input[0], out int row) &&
                int.TryParse(input[1], out int col) &&
                row >= 0 && row < Board.Size && col >= 0 && col < Board.Size)
                return (row, col);
            Console.WriteLine("Invalid input. Try again.");
        }
    }
}
