using System;
using System.Collections.Generic;
using System.Linq;
using DominoConsoleMVC.Interfaces;
using DominoConsoleMVC.Models;

namespace DominoConsoleMVC.Views
{
    public class ConsoleView : IView
    {
        public void ShowWelcome()
        {
            Console.Clear();
            Console.WriteLine("=== Domino Console (MVC) ===\n");
        }

        public void ShowBoard(IBoard board)
        {
            string boardState = board.IsEmpty ? "<empty>" : board.ToString();
            Console.WriteLine($"Board: {boardState}");
            Console.WriteLine($"Ends: Left={board.LeftValue} Right={board.RightValue}\n");
        }

        public void ShowPlayerHand(IPlayer player)
        {
            Console.WriteLine($"{player.Name}'s Hand (Score: {player.Score})");
            
            var hand = player.Hand.ToList();
            if (hand.Count == 0)
            {
                Console.WriteLine(" (No tiles left!)");
            }
            else
            {
                for (int i = 0; i < hand.Count; i++)
                {
                    Console.WriteLine($" {i + 1}. {hand[i]}");
                }
            }
            
            Console.WriteLine();
        }

        public void ShowMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine(message);
            }
        }

        public void ShowScores(IEnumerable<IPlayer> players)
        {
            Console.WriteLine("--- Scores ---");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name}: {player.Score}");
            }
            Console.WriteLine();
        }

        public string AskPlayerChoice(IPlayer player, List<(DominoTile tile, bool playLeft)> moves, bool canDraw)
        {
            if (player.IsHuman)
            {
                ShowPlayerHand(player);

                Console.WriteLine("Playable moves:");
                if (moves.Count > 0)
                {
                    for (int i = 0; i < moves.Count; i++)
                    {
                        var (tile, playLeft) = moves[i];
                        string side = playLeft ? "Left" : "Right";
                        Console.WriteLine($" {i + 1}. {tile} → {side}");
                    }
                }
                else
                {
                    Console.WriteLine(" (No playable moves)");
                }

                if (canDraw) Console.WriteLine(" D. Draw from boneyard");
                Console.WriteLine(" P. Pass");

                Console.Write("Choose move number (or D/P): ");
                return Console.ReadLine()?.Trim().ToUpper() ?? "P";
            }

            // Simple AI: choose first available move, otherwise pass
            return moves.Any() ? "1" : "P";
        }
    }
}
