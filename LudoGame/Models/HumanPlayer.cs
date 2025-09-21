using LudoConsoleMVC.Interfaces;
using LudoConsoleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoConsoleMVC.Players
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; }
        public PieceColor Color { get; }

        private readonly Random _random = new();

        public HumanPlayer(string name, PieceColor color)
        {
            Name = name;
            Color = color;
        }

        public int RollDice()
        {
            return _random.Next(1, 7);
        }

        public Piece SelectPieceToMove(List<Piece> movablePieces)
        {
            if (movablePieces.Count == 1)
            {
                Console.WriteLine($"Only one piece can move, auto-selected.");
                return movablePieces[0];
            }

            Console.WriteLine("Select a piece to move:");
            for (int i = 0; i < movablePieces.Count; i++)
            {
                var p = movablePieces[i];
                Console.WriteLine($"{i}: Position {p.Position}");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice >= movablePieces.Count)
                Console.WriteLine("Invalid choice, try again.");

            return movablePieces[choice];
        }
        public Piece SelectPieceToMove(List<Piece> movablePieces, int diceRoll, IBoard board)
        {
            Console.WriteLine("Your movable pieces:");
            for (int i = 0; i < movablePieces.Count; i++)
                Console.WriteLine($"{i}: Position {movablePieces[i].Position}");

            int choice = int.Parse(Console.ReadLine()!);
            return movablePieces[choice];
        }
    }
}
