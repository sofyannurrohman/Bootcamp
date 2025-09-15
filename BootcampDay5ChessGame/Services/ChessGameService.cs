using ChessGame.Models;
using System;
using System.Collections.Generic;

namespace ChessGame.Services
{
    public class ChessGameService
    {
        public Board Board { get; private set; }
        public PieceColor CurrentTurn { get; private set; }
        public (int row, int col)? PendingPromotion { get; private set; }

        private readonly MoveValidatorService _moveValidator;
        private readonly CastlingService _castlingService;
        private readonly EnPassantService _enPassantService;
        private readonly GameStateService _gameStateService;

        public ChessGameService()
        {
            Board = new Board();
            CurrentTurn = PieceColor.White;

            _enPassantService = new EnPassantService();
            _moveValidator = new MoveValidatorService(Board, _enPassantService);
            _castlingService = new CastlingService(_moveValidator); // _moveValidator implements IBoardService
            _gameStateService = new GameStateService(_moveValidator);

            // Now inject the castling service into move validator
            _moveValidator.SetCastlingService(_castlingService);
        }

        public bool MovePiece((int row, int col) from, (int row, int col) to)
        {
            var piece = Board.Squares[from.row, from.col];
            if (piece == null || piece.Color != CurrentTurn)
                return false;

            if (!_moveValidator.IsValidMove(from, to, CurrentTurn))
                return false;

            return ExecuteMove(from, to, piece);
        }

        private bool ExecuteMove((int row, int col) from, (int row, int col) to, ChessPiece piece)
        {
            var captured = Board.Squares[to.row, to.col];

            // Handle special moves
            HandleEnPassantCapture(from, to, piece);
            HandleCastlingMove(from, to, piece);

            // Execute the move
            Board.Squares[to.row, to.col] = piece;
            Board.Squares[from.row, from.col] = null;

            // Update game state
            _enPassantService.UpdateEnPassantTarget(from, to, piece);
            _castlingService.UpdateCastlingRights(from, piece);
            CheckPromotion(to, piece);

            // Validate move doesn't leave king in check
            if (_moveValidator.IsInCheck(CurrentTurn))
            {
                UndoMove(from, to, piece, captured);
                return false;
            }

            CurrentTurn = CurrentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
            return true;
        }

        private void HandleEnPassantCapture((int row, int col) from, (int row, int col) to, ChessPiece piece)
        {
            if (piece.Type == PieceType.Pawn && _enPassantService.EnPassantTarget.HasValue && to == _enPassantService.EnPassantTarget.Value)
            {
                int direction = piece.Color == PieceColor.White ? 1 : -1;
                Board.Squares[to.row - direction, to.col] = null;
            }
        }

        private void HandleCastlingMove((int row, int col) from, (int row, int col) to, ChessPiece piece)
        {
            if (piece.Type == PieceType.King && Math.Abs(to.col - from.col) == 2)
            {
                if (to.col == 6) // Kingside
                {
                    var rook = Board.Squares[to.row, 7];
                    Board.Squares[to.row, 5] = rook;
                    Board.Squares[to.row, 7] = null;
                }
                else if (to.col == 2) // Queenside
                {
                    var rook = Board.Squares[to.row, 0];
                    Board.Squares[to.row, 3] = rook;
                    Board.Squares[to.row, 0] = null;
                }
            }
        }

        private void CheckPromotion((int row, int col) to, ChessPiece piece)
        {
            if (piece.Type == PieceType.Pawn && (to.row == 0 || to.row == 7))
            {
                PendingPromotion = to;
            }
        }

        private void UndoMove((int row, int col) from, (int row, int col) to, ChessPiece piece, ChessPiece captured)
        {
            Board.Squares[from.row, from.col] = piece;
            Board.Squares[to.row, to.col] = captured;
        }

        public void PromotePawn((int row, int col) position, string newPiece)
        {
            var piece = Board.Squares[position.row, position.col];
            if (piece == null || piece.Type != PieceType.Pawn)
                return;

            PieceType promoteTo = newPiece switch
            {
                "Queen" => PieceType.Queen,
                "Rook" => PieceType.Rook,
                "Bishop" => PieceType.Bishop,
                "Knight" => PieceType.Knight,
                _ => PieceType.Queen
            };

            Board.Squares[position.row, position.col] = new ChessPiece(promoteTo, piece.Color);
            PendingPromotion = null;
        }

        public List<(int row, int col)> GetPossibleMoves(int row, int col)
        {
            return _moveValidator.GetPossibleMoves(row, col, CurrentTurn);
        }

        public bool IsCheck() => _moveValidator.IsInCheck(CurrentTurn);
        public bool IsCheckmate(PieceColor color) => _gameStateService.IsCheckmate(color);
        public bool IsStalemate(PieceColor color) => _gameStateService.IsStalemate(color);
    }
}