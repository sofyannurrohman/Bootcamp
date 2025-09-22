using LudoConsoleMVC.Interfaces;
using LudoConsoleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoConsoleMVC.Players
{
    public class SmartAIPlayer : IPlayer
    {
        public string Name { get; }
        public PieceColor Color { get; }
        private readonly Random _random = new();

        public SmartAIPlayer(string name, PieceColor color)
        {
            Name = name;
            Color = color;
        }

        public int RollDice()
        {
            int dice = _random.Next(1, 7);
            Console.WriteLine($"{Name} rolled a {dice}");
            return dice;
        }

        public Piece SelectPieceToMove(List<Piece> movablePieces, int diceRoll, IBoard board)
        {
            // 1. Capture opponent if possible
            var captureMoves = movablePieces
                .Where(p => board.WillCapture(p, diceRoll))
                .ToList();

            if (captureMoves.Any())
            {
                var piece = captureMoves.OrderByDescending(p => p.Position).First();
                Console.WriteLine($"{Name} chooses piece at {piece.Position} to capture opponent");
                return piece;
            }

            // 2. Move pieces already on the board closer to home
            var onBoardPieces = movablePieces
                .Where(p => !p.IsAtHome)
                .OrderByDescending(p => p.Position)
                .ToList();

            if (onBoardPieces.Any())
            {
                var piece = onBoardPieces.First();
                Console.WriteLine($"{Name} moves piece at {piece.Position} closer to home");
                return piece;
            }

            // 3. If all pieces are at home, prioritize moving piece out
            var homePieces = movablePieces.Where(p => p.IsAtHome).ToList();
            if (homePieces.Any())
            {
                var piece = homePieces.First();
                Console.WriteLine($"{Name} moves piece out of home");
                return piece;
            }

            // 4. Fallback: move the piece furthest along
            var fallback = movablePieces.OrderByDescending(p => p.Position).First();
            Console.WriteLine($"{Name} moves piece at {fallback.Position} by default");
            return fallback;
        }
    }
}
