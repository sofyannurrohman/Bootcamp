using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ChessGame.Models;
using ChessGame.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChessGame
{
    public partial class MainWindow : Window
    {
        private ChessGameService game;
        private (int row, int col)? selectedSquare = null;
        private List<(int row, int col)> possibleMoves = new();
        private Button[,] boardButtons = new Button[8, 8];

        public MainWindow()
        {
            InitializeComponent();
            game = new ChessGameService();
            InitializeBoard();
            DrawBoard();
        }

        private void InitializeBoard()
        {
            ChessBoard.Children.Clear();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button square = new Button
                    {
                        Tag = (row, col),
                        Background = (row + col) % 2 == 0 ? Brushes.Beige : Brushes.Brown,
                        FontSize = 32,
                        Margin = new Thickness(1)
                    };

                    square.Click += Square_Click;
                    boardButtons[row, col] = square;
                    ChessBoard.Children.Add(square);
                }
            }
        }

        private void DrawBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var button = boardButtons[row, col];
                    var piece = game.Board.Squares[row, col];

                    // Update piece display
                    button.Content = piece != null ? GetPieceSymbol(piece) : "";

                    // Reset to base colors first
                    button.Background = (row + col) % 2 == 0 ? Brushes.Beige : Brushes.Brown;

                    // Reset border
                    button.BorderBrush = Brushes.Transparent;
                    button.BorderThickness = new Thickness(1);
                }
            }

            // Apply highlights AFTER resetting everything
            UpdateHighlights();
            UpdateGameStatus();
        }

        private void UpdateHighlights()
        {
            Debug.WriteLine($"Updating highlights - Selected: {selectedSquare}, Possible moves: {possibleMoves.Count}");

            // Highlight possible moves with green background
            foreach (var move in possibleMoves)
            {
                Debug.WriteLine($"Highlighting move: ({move.row}, {move.col})");
                boardButtons[move.row, move.col].Background = Brushes.LightGreen;
            }

            // Highlight selected piece with red border
            if (selectedSquare != null)
            {
                var (row, col) = selectedSquare.Value;
                Debug.WriteLine($"Highlighting selected: ({row}, {col})");
                boardButtons[row, col].BorderBrush = Brushes.Red;
                boardButtons[row, col].BorderThickness = new Thickness(3);
            }
        }

        private string GetPieceSymbol(ChessPiece piece)
        {
            return piece.Type switch
            {
                PieceType.Pawn => piece.Color == PieceColor.White ? "♙" : "♟",
                PieceType.Rook => piece.Color == PieceColor.White ? "♖" : "♜",
                PieceType.Knight => piece.Color == PieceColor.White ? "♘" : "♞",
                PieceType.Bishop => piece.Color == PieceColor.White ? "♗" : "♝",
                PieceType.Queen => piece.Color == PieceColor.White ? "♕" : "♛",
                PieceType.King => piece.Color == PieceColor.White ? "♔" : "♚",
                _ => "?"
            };
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var (row, col) = ((int, int))btn.Tag;

            Debug.WriteLine($"Clicked: ({row}, {col})");
            Debug.WriteLine($"Selected square: {selectedSquare}");
            Debug.WriteLine($"Current turn: {game.CurrentTurn}");

            if (selectedSquare == null)
            {
                // select piece
                var piece = game.Board.Squares[row, col];
                Debug.WriteLine($"Piece at clicked square: {piece?.Type} {piece?.Color}");

                if (piece != null && piece.Color == game.CurrentTurn)
                {
                    selectedSquare = (row, col);
                    possibleMoves = game.GetPossibleMoves(row, col);
                    Debug.WriteLine($"Possible moves count: {possibleMoves.Count}");

                    // Debug: Print all possible moves
                    foreach (var move in possibleMoves)
                    {
                        Debug.WriteLine($"  -> ({move.row}, {move.col})");
                    }

                    DrawBoard();
                }
            }
            else
            {
                // attempt move
                var from = selectedSquare.Value;
                var to = (row, col);

                Debug.WriteLine($"Attempting move from {from} to {to}");

                if (game.MovePiece(from, to))
                {
                    Debug.WriteLine("Move successful!");

                    // handle promotion if needed
                    if (game.PendingPromotion != null)
                    {
                        Debug.WriteLine("Promotion needed!");
                        HandlePromotion(game.PendingPromotion.Value);
                    }

                    possibleMoves.Clear();
                    selectedSquare = null;
                    DrawBoard();
                }
                else
                {
                    Debug.WriteLine("Move failed!");
                    // invalid move -> deselect
                    selectedSquare = null;
                    possibleMoves.Clear();
                    DrawBoard();
                }
            }
        }

        private void HandlePromotion((int row, int col) position)
        {
            var promotionWindow = new PromotionWindow();
            promotionWindow.Owner = this;

            if (promotionWindow.ShowDialog() == true)
            {
                game.PromotePawn(position, promotionWindow.SelectedPiece);
                DrawBoard();
            }
        }

        private void UpdateGameStatus()
        {
            TurnStatus.Text = $"{game.CurrentTurn} to move";

            // Use the new service methods
            if (game.IsCheckmate(game.CurrentTurn))
                GameStatus.Text = $"Checkmate! {game.CurrentTurn} loses.";
            else if (game.IsStalemate(game.CurrentTurn))
                GameStatus.Text = "Stalemate! It's a draw.";
            else if (game.IsCheck()) // Changed from IsInCheck to IsCheck
                GameStatus.Text = $"{game.CurrentTurn} is in check!";
            else
                GameStatus.Text = "";
        }
    }
}