using CheckersConsoleMVC.Interfaces;
using CheckersConsoleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersConsoleMVC.Controllers
{
    public class GameController
    {
        private readonly IBoard _board;
        private readonly IPlayer _redPlayer;
        private readonly IPlayer _blackPlayer;
        private readonly IGameRules _rules;
        private IPlayer _currentPlayer;

        public GameController(IBoard board, IPlayer redPlayer, IPlayer blackPlayer, IGameRules rules)
        {
            _board = board;
            _redPlayer = redPlayer;
            _blackPlayer = blackPlayer;
            _rules = rules;
            _currentPlayer = _redPlayer;
        }

        // === Public API ===
        public void Start()
        {
            Console.WriteLine("=== Welcome to Console Checkers ===\n");
            DisplayBoard();

            while (!IsGameOver())
            {
                PlayTurn();
                DisplayBoard(); // ✅ rely on board’s own ToString
            }

            Console.WriteLine("Game over!");
        }

        public void PlayTurn()
        {
            Console.WriteLine($"{_currentPlayer.Name}'s turn ({_currentPlayer.Color})");

            var allMoves = _rules.GetValidMoves(_board, _currentPlayer);
            if (!allMoves.Any())
            {
                Console.WriteLine("No valid moves. Turn skipped.");
                SwitchTurn();
                return;
            }

            // Must capture if available
            var moves = EnforceCaptures(allMoves);

            // Select move
            var move = SelectMove(moves);

            // Execute move sequence (handles chain captures)
            PerformMoveSequence(move);

            SwitchTurn();
        }

        public bool IsGameOver()
        {
            int redPieces = _board.GetAllPieces().Count(p => p.Color == PieceColor.Red);
            int blackPieces = _board.GetAllPieces().Count(p => p.Color == PieceColor.Black);

            if (redPieces == 0 || blackPieces == 0) return true;

            bool redHasMoves = _rules.GetValidMoves(_board, _redPlayer).Any();
            bool blackHasMoves = _rules.GetValidMoves(_board, _blackPlayer).Any();

            return !(redHasMoves && blackHasMoves);
        }

        // === Private Helpers ===
        private List<Move> EnforceCaptures(List<Move> allMoves)
        {
            var captureMoves = allMoves.Where(m => m.IsCapture).ToList();
            if (captureMoves.Any())
            {
                Console.WriteLine("Capture available! You must capture.");
                return captureMoves;
            }
            return allMoves;
        }

        private Move SelectMove(List<Move> moves)
        {
            if (moves.Count == 1)
            {
                var move = moves[0];
                Console.WriteLine($"Automatically selected: ({move.FromRow},{move.FromCol}) -> ({move.ToRow},{move.ToCol})");
                return move;
            }

            ShowMovablePieces(moves);

            List<Move> pieceMoves;
            (int row, int col) = (-1, -1);
            do
            {
                (row, col) = _currentPlayer.SelectPiece(_board);
                pieceMoves = moves.Where(m => m.FromRow == row && m.FromCol == col).ToList();

                if (!pieceMoves.Any())
                    Console.WriteLine("Invalid choice. Please select a piece with a valid move.");
            } while (!pieceMoves.Any());

            return _currentPlayer.SelectMove(pieceMoves);
        }

        private void PerformMoveSequence(Move move)
        {
            ExecuteMove(move);

            // Handle chain captures
            while (move.IsCapture)
            {
                var followUps = _rules.GetValidMoves(_board, _currentPlayer)
                                      .Where(m => m.FromRow == move.ToRow && m.FromCol == move.ToCol && m.IsCapture)
                                      .ToList();

                if (!followUps.Any()) break;

                Console.WriteLine($"{_currentPlayer.Name} must continue capturing!");

                move = followUps.Count == 1 ? AutoSelectMove(followUps) : _currentPlayer.SelectMove(followUps);

                ExecuteMove(move);
            }
        }

        private Move AutoSelectMove(List<Move> moves)
        {
            var move = moves[0];
            Console.WriteLine($"Automatically capturing: ({move.FromRow},{move.FromCol}) -> ({move.ToRow},{move.ToCol})");
            return move;
        }

        private void ExecuteMove(Move move)
        {
            _board.MovePiece(move);

            if (move.IsCapture && move.CapturedRow.HasValue && move.CapturedCol.HasValue)
                _board.RemovePiece(move.CapturedRow.Value, move.CapturedCol.Value);
        }

        private void SwitchTurn()
        {
            _currentPlayer = _currentPlayer == _redPlayer ? _blackPlayer : _redPlayer;
        }

        private void ShowMovablePieces(List<Move> moves)
        {
            var movablePieces = moves
                .Select(m => (m.FromRow, m.FromCol))
                .Distinct()
                .ToList();

            Console.WriteLine("You can move these pieces:");
            foreach (var (r, c) in movablePieces)
                Console.WriteLine($"({r},{c})");
        }

        private void DisplayBoard()
        {
            Console.WriteLine(_board.ToString()); // ✅ centralized board printing
        }
    }
}
