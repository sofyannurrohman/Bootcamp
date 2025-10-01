using System;
using System.Collections.Generic;
using System.Linq;
using BlockDomino.Controllers;
using BlockDomino.Interfaces;
using BlockDomino.Models;

namespace BlockDomino.Views
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Block Domino Console Game ===");

            List<IPlayer> players = new List<IPlayer>
            {
                new Player("Player 1"),
                new BotPlayer("Computer")
            };

            IDeck deck = new Deck();
            IBoard board = new Board();

            Game game = new Game(players, deck, board)
            {
                ScoreLimit = 30
            };

            game.OnGameOver += finalWinner =>
            {
                Console.WriteLine($"\n=== MATCH OVER! Final Winner: {finalWinner?.Name} ===");
            };

            game.StartMatch();

            bool matchRunning = true;

            while (matchRunning)
            {
                bool roundRunning = true;

                Console.WriteLine("\n--- New Round Started ---");
                game.DisplayBoard();

                while (roundRunning)
                {
                    var currentPlayer = game.GetCurrentPlayer();
                    Console.WriteLine($"\n{currentPlayer.Name}'s Turn:");

                    if (currentPlayer is BotPlayer bot)
                    {
                        HandleBotTurn(game, bot);
                    }
                    else
                    {
                        HandleHumanTurn(game, currentPlayer);
                    }

                    if (game.IsRoundOver())
                    {
                        var roundWinner = game.ResolveRound();

                        Console.WriteLine("\n=== Round Finished ===");
                        foreach (var p in game.Players)
                        {
                            int pipSum = game.GetPipSum(p);
                            Console.WriteLine($"{p.Name} -> Score: {p.Score}, Pips in hand: {pipSum}");
                        }

                        if (roundWinner != null)
                            Console.WriteLine($"\nRound Winner: {roundWinner.Name}");
                        else
                            Console.WriteLine("\nRound tied (no points awarded).");

                        if (game.IsMatchOver())
                        {
                            var matchWinner = game.GetMatchWinner();
                            Console.WriteLine($"\n🎉 Match Winner: {matchWinner?.Name} 🎉");
                            matchRunning = false;
                        }
                        else
                        {
                            Console.WriteLine("\nPress Enter to start next round...");
                            Console.ReadLine();
                            game.NextRound();
                        }

                        roundRunning = false;
                    }
                    else
                    {
                        game.NextTurn();
                    }
                }
            }

            Console.WriteLine("\nThanks for playing!");
        }

        private static void HandleBotTurn(Game game, BotPlayer bot)
        {
            var chosenTile = bot
                .Hand
                .FirstOrDefault(tile => game.CanPlay(tile));

            if (chosenTile != null)
            {
                if (game.PlayDominoTile(bot, chosenTile))
                {
                    Console.WriteLine($"{bot.Name} played {chosenTile}");
                    game.DisplayBoard();
                }
            }
            else
            {
                Console.WriteLine($"{bot.Name} skips (no playable tiles)");
            }
        }

        private static void HandleHumanTurn(Game game, IPlayer currentPlayer)
        {
            var playableTiles = currentPlayer.Hand
                .Where(tile => game.CanPlay(tile))
                .ToList();

            if (!playableTiles.Any())
            {
                Console.WriteLine($"{currentPlayer.Name} has no playable tiles — skipping turn.");
                return;
            }

            Console.WriteLine("Playable tiles:");
            for (int i = 0; i < playableTiles.Count; i++)
            {
                Console.WriteLine($"[{i}] {playableTiles[i]}");
            }

            Console.WriteLine("Enter tile index to play (0-based), or X to skip:");
            string input = Console.ReadLine() ?? string.Empty;

            if (input.ToUpper() == "X")
            {
                Console.WriteLine("Skipping turn...");
                return;
            }

            if (int.TryParse(input, out int selection) &&
                selection >= 0 && selection < playableTiles.Count)
            {
                var chosenTile = playableTiles[selection];

                if (game.PlayDominoTile(currentPlayer, chosenTile))
                {
                    Console.WriteLine("Tile played!");
                    game.DisplayBoard();
                }
                else
                {
                    Console.WriteLine("Unexpected placement error!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input, skipping turn.");
            }
        }
    }
}
