using System;
using System.Collections.Generic;
using System.Linq;
using CheckersConsoleMVC.Interfaces;

namespace CheckersConsoleMVC.Models
{
    public class Player : IPlayer
    {
        public string Name { get; }
        public bool IsHuman { get; }
        public PieceColor Color { get; }
        public List<Piece> Pieces { get; }  // Pieces currently owned

        public Player(string name, bool isHuman, PieceColor color)
        {
            Name = name;
            IsHuman = isHuman;
            Color = color;
            Pieces = new List<Piece>();
        }

        public (int row, int col) SelectPiece(IBoard board)
        {
            if (IsHuman)
            {
                Console.WriteLine($"{Name}, select a piece to move (row col): ");
                var input = Console.ReadLine()?.Split(' ');
                if (input != null && input.Length == 2 &&
                    int.TryParse(input[0], out int row) &&
                    int.TryParse(input[1], out int col))
                {
                    return (row, col);
                }

                Console.WriteLine("Invalid input. Try again.");
                return SelectPiece(board); // Retry until valid
            }
            else
            {
                // Simple AI: just pick the first piece with a move
                for (int r = 0; r < board.Size; r++)
                {
                    for (int c = 0; c < board.Size; c++)
                    {
                        var piece = board.GetPiece(r, c);
                        if (piece != null && piece.Color == Color)
                            return (r, c);
                    }
                }
                return (-1, -1); // No valid piece
            }
        }

        public Move SelectMove(List<Move> moves)
        {
            if (moves == null || moves.Count == 0)
                throw new InvalidOperationException("No valid moves available.");

            // 🔑 Step 1: Check for forced captures (jump = move of 2 rows)
            var captures = moves.Where(m => Math.Abs(m.ToRow - m.FromRow) == 2).ToList();

            if (captures.Any())
            {
                // Auto-select if only one capture exists
                if (captures.Count == 1)
                {
                    var autoMove = captures[0];
                    Console.WriteLine($"{Name}, forced to capture → auto-selected: " +
                                      $"({autoMove.FromRow},{autoMove.FromCol}) -> ({autoMove.ToRow},{autoMove.ToCol})");
                    return autoMove;
                }

                // Otherwise: ask the human which capture to do
                if (IsHuman)
                {
                    Console.WriteLine($"{Name}, you must capture! Choose one of these:");
                    for (int i = 0; i < captures.Count; i++)
                    {
                        var m = captures[i];
                        Console.WriteLine($"{i}: ({m.FromRow},{m.FromCol}) -> ({m.ToRow},{m.ToCol})");
                    }

                    int choice;
                    while (!int.TryParse(Console.ReadLine(), out choice) ||
                           choice < 0 || choice >= captures.Count)
                    {
                        Console.WriteLine("Invalid choice. Try again.");
                    }
                    return captures[choice];
                }
                else
                {
                    // AI: random capture
                    var rnd = new Random();
                    return captures[rnd.Next(captures.Count)];
                }
            }

            // 🔑 Step 2: No captures available → normal moves
            if (moves.Count == 1)
            {
                var autoMove = moves[0];
                Console.WriteLine($"{Name}, only one move available → auto-selected: " +
                                  $"({autoMove.FromRow},{autoMove.FromCol}) -> ({autoMove.ToRow},{autoMove.ToCol})");
                return autoMove;
            }

            if (IsHuman)
            {
                Console.WriteLine($"{Name}, select a move:");
                for (int i = 0; i < moves.Count; i++)
                {
                    var m = moves[i];
                    Console.WriteLine($"{i}: ({m.FromRow},{m.FromCol}) -> ({m.ToRow},{m.ToCol})");
                }

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) ||
                       choice < 0 || choice >= moves.Count)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
                return moves[choice];
            }
            else
            {
                // AI: random move
                var rnd = new Random();
                return moves[rnd.Next(moves.Count)];
            }
        }
    }
}
