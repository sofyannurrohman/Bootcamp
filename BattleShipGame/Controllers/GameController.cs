using System;
using Battleship.Interfaces;
using Battleship.Models;
using System.Collections.Generic;

namespace Battleship.Controllers
{
    public class GameController
    {
        private readonly IPlayer player1;
        private readonly IPlayer player2;
        private IPlayer currentPlayer;
        private IPlayer opponent;

        public GameController(IPlayer player1, IPlayer player2)
        {
            this.player1 = player1;
            this.player2 = player2;
            currentPlayer = player1;
            opponent = player2;
        }

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== Welcome to Battleship ===\n");

            // Automatically place ships for both players
            player1.Board.PlaceShipsRandomly();
            player2.Board.PlaceShipsRandomly();

            while (true)
            {
                Console.WriteLine($"\n{currentPlayer.Name}'s turn\n");

                DisplayOpponentBoardWithSuggestions();

                var attack = GetAttackCoordinates();

                var result = opponent.Board.Attack(attack.Row, attack.Col);

                ProcessAttackResultForAI(attack, result);

                DisplayAttackResult(result);

                Console.WriteLine("\nYour Board:");
                currentPlayer.Board.PrintFogOfWar(showOwnShips: true);

                if (opponent.Board.AllShipsSunk())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{currentPlayer.Name} wins!");
                    Console.ResetColor();
                    break;
                }

                SwapPlayers();
            }
        }

        private void DisplayOpponentBoardWithSuggestions()
        {
            if (currentPlayer is AIPlayer)
            {
                Console.WriteLine("Opponent Board:");
                opponent.Board.PrintFogOfWar();
            }
            else
            {
                var suggestions = opponent.Board.GetPriorityTargets();
                Console.WriteLine("Opponent Board (priority '*' = suggested next move):");
                opponent.Board.PrintFogOfWar(suggestions: suggestions);
            }
        }

        private (int Row, int Col) GetAttackCoordinates()
        {
            if (currentPlayer is AIPlayer ai)
            {
                var attack = ai.SelectAttack();
                Console.WriteLine($"{currentPlayer.Name} attacks ({attack.Row}, {attack.Col})");
                return attack;
            }
            else
            {
                return GetPlayerAttackInput();
            }
        }

        private void ProcessAttackResultForAI((int Row, int Col) attack, CellStatus result)
        {
            if (currentPlayer is AIPlayer ai)
            {
                ai.ProcessAttackResult(attack.Row, attack.Col, result);
            }
        }

        private void DisplayAttackResult(CellStatus result)
        {
            switch (result)
            {
                case CellStatus.Hit:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hit!");
                    break;
                case CellStatus.Miss:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Miss!");
                    break;
                case CellStatus.AlreadyAttacked:
                    Console.WriteLine("Already attacked that cell! Turn skipped.");
                    break;
            }
            Console.ResetColor();
        }

        private (int Row, int Col) GetPlayerAttackInput()
        {
            while (true)
            {
                Console.Write("Enter row and column to attack (e.g., 3 5): ");
                var input = Console.ReadLine()?.Split();

                if (input?.Length == 2 &&
                    int.TryParse(input[0], out int row) &&
                    int.TryParse(input[1], out int col) &&
                    row >= 0 && row < Board.Size &&
                    col >= 0 && col < Board.Size)
                {
                    return (row, col);
                }

                Console.WriteLine("Invalid input. Try again.");
            }
        }

        private void SwapPlayers()
        {
            (currentPlayer, opponent) = (opponent, currentPlayer);
        }
    }
}
