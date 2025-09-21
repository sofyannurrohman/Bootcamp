using CheckersConsoleMVC.Interfaces;
using CheckersConsoleMVC.Models;
using System;
using System.Collections.Generic;

namespace CheckersConsoleMVC.Views
{
    public class ConsoleView : IView
    {
        public void ShowBoard(IBoard board)
        {
            for (int r = 0; r < board.Size; r++)
            {
                for (int c = 0; c < board.Size; c++)
                {
                    var piece = board.GetPiece(r, c);
                    if (piece == null) Console.Write(". ");
                    else Console.Write(piece.Color == PieceColor.Red ? "R " : "B ");
                }
                Console.WriteLine();
            }
        }

        public void ShowMessage(string message) => Console.WriteLine(message);

        public Move AskPlayerMove(IPlayer player, List<Move> validMoves)
        {
            Console.WriteLine($"{player.Name}, choose your move:");
            for (int i = 0; i < validMoves.Count; i++)
            {
                var m = validMoves[i];
                Console.WriteLine($"{i + 1}. ({m.FromRow},{m.FromCol}) -> ({m.ToRow},{m.ToCol})");
            }
            int choice = int.Parse(Console.ReadLine() ?? "1") - 1;
            return validMoves[choice];
        }

        public void ShowWinner(IPlayer player) => Console.WriteLine($"Winner: {player.Name}");
    }
}
