using LudoConsoleMVC.Interfaces;
using LudoConsoleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LudoConsoleMVC.Models
{
    public class LudoBoard : IBoard
    {
        private readonly Dictionary<PieceColor, List<Piece>> _pieces = new();
        public int Size => 52;
        private const int TrackSize = 52;
        private const int HomeStretch = 6;
public static int HomePosition => 57;
        public LudoBoard()
        {
            foreach (PieceColor color in Enum.GetValues(typeof(PieceColor)))
            {
                _pieces[color] = new List<Piece>();
                for (int i = 0; i < 4; i++)
                    _pieces[color].Add(new Piece { Color = color });
            }
        }

        public void MovePiece(Piece piece, int steps)
        {
            if (piece.IsFinished) return;

            if (piece.Position == -1)
            {
                if (steps == 6)
                    piece.Position = GetStartPosition(piece.Color); // enter track
            }
            else
            {
                piece.Position += steps;

                int finishPosition = GetStartPosition(piece.Color) + TrackSize;
                if (piece.Position >= finishPosition)
                {
                    piece.Position = LudoBoard.HomePosition;
                }
                else
                {
                    // Capture logic: if any opponent on same position, send back home
                    foreach (var list in _pieces.Values)
                    {
                        foreach (var other in list)
                        {
                            if (other != piece && other.Position == piece.Position && other.Color != piece.Color)
                                other.Position = -1;
                        }
                    }
                }
            }
        }

        public List<Piece> GetPieces(PieceColor color) => _pieces[color];

        public Piece? GetPieceAtPosition(int position)
        {
            foreach (var list in _pieces.Values)
            {
                foreach (var piece in list)
                    if (piece.Position == position)
                        return piece;
            }
            return null;
        }

        public int GetStartPosition(PieceColor color)
        {
            return color switch
            {
                PieceColor.Red => 0,
                PieceColor.Blue => 13,
                PieceColor.Yellow => 26,
                PieceColor.Green => 39,
                _ => 0
            };
        }

        public void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("==== LUDO BOARD ====");
            for (int pos = 0; pos < TrackSize; pos++)
            {
                var piece = GetPieceAtPosition(pos);
                if (piece != null)
                {
                    SetColor(piece.Color);
                    Console.Write($"{piece.Color.ToString()[0]} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(". ");
                }

                if ((pos + 1) % 13 == 0) Console.WriteLine();
            }
            Console.WriteLine("\nHome Pieces:");
            foreach (var kvp in _pieces)
            {
                SetColor(kvp.Key);
                Console.Write($"{kvp.Key}: ");
                Console.ResetColor();
                foreach (var piece in kvp.Value)
                    Console.Write(piece.Position == -1 ? "H " : "- ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void SetColor(PieceColor color)
        {
            Console.ForegroundColor = color switch
            {
                PieceColor.Red => ConsoleColor.Red,
                PieceColor.Blue => ConsoleColor.Blue,
                PieceColor.Green => ConsoleColor.Green,
                PieceColor.Yellow => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
        }
        public bool WillCapture(Piece piece, int diceRoll)
        {
            int target = piece.Position + diceRoll;
            var opponents = GetAllPieces().Where(p => p.Color != piece.Color && !p.IsAtHome && !p.IsFinished);
            return opponents.Any(op => op.Position == target);
        }
        public List<Piece> GetAllPieces()
        {
            return _pieces.Values.SelectMany(list => list).ToList();
        }
    }
}
