using ChessGame.Models;

namespace ChessGame.Services
{
    public class GameStateService
    {
        private readonly MoveValidatorService _moveValidator;

        public GameStateService(MoveValidatorService moveValidator)
        {
            _moveValidator = moveValidator;
        }

        public bool IsCheckmate(PieceColor color)
        {
            if (!_moveValidator.IsInCheck(color)) 
                return false;

            return HasNoLegalMoves(color);
        }

        public bool IsStalemate(PieceColor color)
        {
            if (_moveValidator.IsInCheck(color)) 
                return false;

            return HasNoLegalMoves(color);
        }

        private bool HasNoLegalMoves(PieceColor color)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    var piece = _moveValidator.Board.Squares[r, c];
                    if (piece != null && piece.Color == color)
                    {
                        var moves = _moveValidator.GetPossibleMoves(r, c, color);
                        if (moves.Count > 0)
                            return false;
                    }
                }
            }
            return true;
        }
    }
}