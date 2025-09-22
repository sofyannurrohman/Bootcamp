using PokerConsoleApp.Core;
using PokerConsoleApp.Game;
using PokerConsoleApp.Delegates;

class Program
{
    static void Main()
    {
        Console.Title = "Poker Console Game";
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        var players = new List<Player>
        {
            new HumanPlayer("Player", 1000),
            new AIPlayer("AI 1", 1000),
            new AIPlayer("AI 2", 1000),
            new AIPlayer("AI 3", 1000)
        };

        var game = new PokerGame(players);
        
        // Setup event handlers
        game.OnGameEvent += message => Console.WriteLine(message);
        game.OnPlayerDecision += (player, currentBet, minRaise) =>
        {
            if (player is HumanPlayer)
                return player.MakeDecision(currentBet, minRaise);
            
            // AI decision with delay for realism
            Thread.Sleep(1000);
            return player.MakeDecision(currentBet, minRaise);
        };

        try
        {
            game.StartGame();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Game error: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}