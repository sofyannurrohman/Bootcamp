using ChessGame.Models;
using System.Collections.Generic;
using ChessGame.Interfaces;
namespace ChessGame.Services
{
    public class CastlingService
    {
        private readonly IBoardService _boardService;
        
        public bool WhiteKingMoved { get; set; }
        public bool BlackKingMoved { get; set; }
        public bool WhiteRookLeftMoved { get; set; }
        public bool WhiteRookRightMoved { get; set; }
        public bool BlackRookLeftMoved { get; set; }
        public bool BlackRookRightMoved { get; set; }

        public CastlingService(IBoardService boardService)
        {
            _boardService = boardService;
        }

        public List<(int row, int col)> GetCastlingMoves(int row, int col, PieceColor color, PieceColor currentTurn)
        {
            var moves = new List<(int, int)>();
            
            if (_boardService.Board.Squares[row, col]?.Type != PieceType.King || 
                _boardService.IsInCheck(color) || 
                color != currentTurn)
                return moves;

            if (color == PieceColor.White && !WhiteKingMoved)
            {
                if (!WhiteRookRightMoved && CanCastleKingside(7, color))
                    moves.Add((7, 6));
                if (!WhiteRookLeftMoved && CanCastleQueenside(7, color))
                    moves.Add((7, 2));
            }
            else if (color == PieceColor.Black && !BlackKingMoved)
            {
                if (!BlackRookRightMoved && CanCastleKingside(0, color))
                    moves.Add((0, 6));
                if (!BlackRookLeftMoved && CanCastleQueenside(0, color))
                    moves.Add((0, 2));
            }

            return moves;
        }

        private bool CanCastleKingside(int row, PieceColor color)
        {
            var opponentColor = color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            return _boardService.Board.Squares[row, 5] == null &&
                   _boardService.Board.Squares[row, 6] == null &&
                   !_boardService.IsSquareAttacked((row, 5), opponentColor) &&
                   !_boardService.IsSquareAttacked((row, 6), opponentColor);
        }

        private bool CanCastleQueenside(int row, PieceColor color)
        {
            var opponentColor = color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            return _boardService.Board.Squares[row, 1] == null &&
                   _boardService.Board.Squares[row, 2] == null &&
                   _boardService.Board.Squares[row, 3] == null &&
                   !_boardService.IsSquareAttacked((row, 2), opponentColor) &&
                   !_boardService.IsSquareAttacked((row, 3), opponentColor);
        }

        public void UpdateCastlingRights((int row, int col) from, ChessPiece piece)
        {
            if (piece.Type == PieceType.King)
            {
                if (piece.Color == PieceColor.White) WhiteKingMoved = true;
                else BlackKingMoved = true;
            }
            else if (piece.Type == PieceType.Rook)
            {
                if (from.row == 7 && from.col == 0) WhiteRookLeftMoved = true;
                if (from.row == 7 && from.col == 7) WhiteRookRightMoved = true;
                if (from.row == 0 && from.col == 0) BlackRookLeftMoved = true;
                if (from.row == 0 && from.col == 7) BlackRookRightMoved = true;
            }
        }
    }
}