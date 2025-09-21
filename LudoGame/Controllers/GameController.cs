using LudoConsoleMVC.Interfaces;
using LudoConsoleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoConsoleMVC.Controllers
{
    public class LudoGameController
    {
        private readonly IBoard _board;
        private readonly List<IPlayer> _players;
        private readonly IGameRules _rules;
        private int _currentPlayerIndex = 0;

        public LudoGameController(IBoard board, List<IPlayer> players, IGameRules rules)
        {
            _board = board;
            _players = players;
            _rules = rules;
        }

        public void Start()
        {
            while (!_rules.IsGameOver(_board, _players))
            {
                var player = _players[_currentPlayerIndex];
                _board.PrintBoard();

                Console.WriteLine($"{player.Name}'s turn ({player.Color})");
                int dice = player.RollDice();
                Console.WriteLine($"{player.Name} rolled a {dice}");

                var movablePieces = _rules.GetMovablePieces(_board, player, dice);
                if (!movablePieces.Any())
                {
                    Console.WriteLine("No pieces can move. Turn skipped.");
                    NextTurn();
                    continue;
                }
                var piece = player.SelectPieceToMove(movablePieces, dice, _board);
                _board.MovePiece(piece, dice);

                if (dice != 6)
                    NextTurn();
            }

            Console.WriteLine("Game Over!");
        }

        private void NextTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        }
    }
}
