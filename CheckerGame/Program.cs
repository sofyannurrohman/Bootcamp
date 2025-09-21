using System;
using CheckersConsoleMVC.Controllers;
using CheckersConsoleMVC.Models;
using CheckersConsoleMVC.Rules;
namespace CheckersConsoleMVC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to Console Checkers ===\n");

            // 🎲 Setup Game
            var board = new Board(8);
            var redPlayer = new Player("Player 1", true, PieceColor.Red);
            var blackPlayer = new Player("Player 2", true, PieceColor.Black);
            var rules = new StandardCheckersRules();

            var game = new GameController(board, redPlayer, blackPlayer, rules);

            // ▶ Start Game
            game.Start();

            Console.WriteLine("\nThanks for playing!");
        }
    }
}
