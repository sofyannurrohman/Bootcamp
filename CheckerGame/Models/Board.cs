using CheckersConsoleMVC.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CheckersConsoleMVC.Models
{
    public class Board : IBoard
    {
        private readonly Piece?[,] _grid;
        public int Size { get; }

        public Board(int size = 8)
        {
            Size = size;
            _grid = new Piece[size, size];
            InitializeBoard();
        }

        public Piece? GetPiece(int row, int col) => _grid[row, col];

        public void PlacePiece(int row, int col, Piece piece)
        {
            _grid[row, col] = piece;
        }

        public void RemovePiece(int row, int col) => _grid[row, col] = null;

        public void MovePiece(Move move)
        {
            var piece = _grid[move.FromRow, move.FromCol];
            if (piece == null) return;

            // Clear origin
            _grid[move.FromRow, move.FromCol] = null;

            // Place at new position
            _grid[move.ToRow, move.ToCol] = piece;

            // Capture logic (if jump)
            if (System.Math.Abs(move.FromRow - move.ToRow) == 2)
            {
                int midRow = (move.FromRow + move.ToRow) / 2;
                int midCol = (move.FromCol + move.ToCol) / 2;
                _grid[midRow, midCol] = null;

                // Check for additional captures (chain jump)
                if (HasAvailableCapture(move.ToRow, move.ToCol))
                {
                    // In a real game controller, you'd ask the player to continue jumping
                    // For now, we just "allow" further moves from this position
                }
            }

            // Promotion to King
            if (piece.Color == PieceColor.Red && move.ToRow == 0)
                piece.IsKing = true;
            if (piece.Color == PieceColor.Black && move.ToRow == Size - 1)
                piece.IsKing = true;
        }

        public bool HasAvailableCapture(int row, int col)
        {
            var piece = _grid[row, col];
            if (piece == null) return false;

            int direction = piece.Color == PieceColor.Red ? -1 : 1;
            int[,] directions = piece.IsKing
                ? new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } }
                : new int[,] { { direction, 1 }, { direction, -1 } };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dr = directions[i, 0];
                int dc = directions[i, 1];
                int enemyRow = row + dr;
                int enemyCol = col + dc;
                int landingRow = row + 2 * dr;
                int landingCol = col + 2 * dc;

                if (IsInside(enemyRow, enemyCol) && IsInside(landingRow, landingCol))
                {
                    var enemy = _grid[enemyRow, enemyCol];
                    if (enemy != null && enemy.Color != piece.Color &&
                        _grid[landingRow, landingCol] == null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsInside(int row, int col) =>
            row >= 0 && row < Size && col >= 0 && col < Size;

        public List<Move> GetValidMoves(int row, int col)
        {
            var moves = new List<Move>();
            var piece = _grid[row, col];
            if (piece == null) return moves;

            int direction = piece.Color == PieceColor.Red ? -1 : 1;
            int[,] directions = piece.IsKing
                ? new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } }
                : new int[,] { { direction, 1 }, { direction, -1 } };

            // First: check captures
            foreach (var move in GetCaptureMoves(row, col, directions, piece))
                moves.Add(move);

            if (moves.Count > 0) return moves; // forced capture rule

            // Otherwise, normal moves
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dr = directions[i, 0];
                int dc = directions[i, 1];
                int newRow = row + dr;
                int newCol = col + dc;

                if (IsInside(newRow, newCol) && _grid[newRow, newCol] == null)
                    moves.Add(new Move(row, col, newRow, newCol));
            }

            return moves;
        }

        private IEnumerable<Move> GetCaptureMoves(int row, int col, int[,] directions, Piece piece)
        {
            var result = new List<Move>();

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dr = directions[i, 0];
                int dc = directions[i, 1];
                int enemyRow = row + dr;
                int enemyCol = col + dc;
                int landingRow = row + 2 * dr;
                int landingCol = col + 2 * dc;

                if (IsInside(enemyRow, enemyCol) && IsInside(landingRow, landingCol))
                {
                    var enemy = _grid[enemyRow, enemyCol];
                    if (enemy != null && enemy.Color != piece.Color &&
                        _grid[landingRow, landingCol] == null)
                    {
                        result.Add(new Move(row, col, landingRow, landingCol));
                    }
                }
            }

            return result;
        }
        public override string ToString()
{
    var sb = new System.Text.StringBuilder();

    // ANSI color codes
    const string reset = "\u001b[0m";
    const string red = "\u001b[31m";      // Red pieces
    const string black = "\u001b[34m";    // Black pieces
    const string darkSquare = "\u001b[47m";  // White background for dark squares
    const string lightSquare = "\u001b[40m"; // Black background for light squares

    // Column headers
    sb.Append("   ");
    for (int col = 0; col < Size; col++)
        sb.Append($"{col,3}");
    sb.AppendLine();

    for (int row = 0; row < Size; row++)
    {
        sb.Append($"{row,2} "); // row number

        for (int col = 0; col < Size; col++)
        {
            var piece = GetPiece(row, col);
            bool isDark = (row + col) % 2 == 1;
            string bg = isDark ? darkSquare : lightSquare;

            if (piece != null)
            {
                string color = piece.Color == PieceColor.Red ? red : black;
                string symbol = piece.IsKing ? "K" : "O"; // O = regular piece
                sb.Append($"{bg}{color} {symbol} {reset}");
            }
            else
            {
                sb.Append($"{bg}   {reset}");
            }
        }

        sb.AppendLine();
    }

    return sb.ToString();
}


        public void InitializeBoard()
        {
            // Standard Checkers setup: 3 rows each
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        _grid[row, col] = new Piece(PieceColor.Black);
                    }
                }
            }

            for (int row = Size - 3; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        _grid[row, col] = new Piece(PieceColor.Red);
                    }
                }
            }
        }
        public IEnumerable<Piece> GetAllPieces()
        {
            for (int row = 0; row < Size; row++)
                for (int col = 0; col < Size; col++)
                    if (_grid[row, col] != null)
                        yield return _grid[row, col];
        }
    }
}
