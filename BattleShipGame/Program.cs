using Battleship.Models;
using Battleship.Controllers;
using Battleship.Interfaces;

class Program
{
    static void Main()
    {
        // Set console window size
        try
        {
            Console.SetWindowSize(60, 25); // Width x Height
            Console.SetBufferSize(60, 25); // Ensure buffer matches window
        }
        catch
        {
            // Some consoles may not support resizing, ignore errors
        }

        // Create players
        IPlayer player1 = new HumanPlayer { Name = "Player 1", Board = new Board() };
        IPlayer player2 = new AIPlayer { Name = "Computer", Board = new Board() };

        // Create game controller
        var gameController = new GameController(player1, player2);

        // Start the game
        gameController.StartGame();

        // Wait before closing
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
