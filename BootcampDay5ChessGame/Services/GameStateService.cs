using ChessGame.Models;

namespace ChessGame.Services
{
    public class GameStateService
    {
        private readonly MoveValidatorService _moveValidator;
        private readonly BoardService _boardService;

        public GameStateService(MoveValidatorService moveValidator, BoardService boardService)
        {
            _moveValidator = moveValidator;
            _boardService = boardService;
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
            for (int fromRow = 0; fromRow < 8; fromRow++)
            {
                for (int fromCol = 0; fromCol < 8; fromCol++)
                {
                    var piece = _moveValidator.Board.Squares[fromRow, fromCol];
                    if (piece != null && piece.Color == color)
                    {
                        // Get all possible moves for this piece
                        var moves = _moveValidator.GetPossibleMoves(fromRow, fromCol, color);
                        
                        // Test each move to see if it would resolve check (if in check)
                        foreach (var move in moves)
                        {
                            // Make the move on a temporary board
                            var tempBoard = _boardService.CloneBoard();
                            _boardService.MakeMove(tempBoard, fromRow, fromCol, move.Row, move.Col);
                            
                            // Check if king is still in check after this move
                            if (!IsKingInCheckAfterMove(tempBoard, color))
                                return false; // Found at least one legal move
                        }
                    }
                }
            }
            return true; // No legal moves found
        }

        private bool IsKingInCheckAfterMove(Board board, PieceColor color)
        {
            // Create a temporary move validator with the new board state
            var tempMoveValidator = new MoveValidatorService(board);
            
            // Check if the king is in check in this new board state
            return tempMoveValidator.IsInCheck(color);
        }
    }
}