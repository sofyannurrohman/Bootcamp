namespace ChessGame.Models
{
    public class Board
    {
        public ChessPiece[,] Squares { get; set; } = new ChessPiece[8, 8];

        public Board()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Initialize with null (empty squares)
            Squares = new ChessPiece[8, 8];

            // Set up black pieces (row 0)
            Squares[0, 0] = new ChessPiece(PieceType.Rook, PieceColor.Black);
            Squares[0, 1] = new ChessPiece(PieceType.Knight, PieceColor.Black);
            Squares[0, 2] = new ChessPiece(PieceType.Bishop, PieceColor.Black);
            Squares[0, 3] = new ChessPiece(PieceType.Queen, PieceColor.Black);
            Squares[0, 4] = new ChessPiece(PieceType.King, PieceColor.Black);
            Squares[0, 5] = new ChessPiece(PieceType.Bishop, PieceColor.Black);
            Squares[0, 6] = new ChessPiece(PieceType.Knight, PieceColor.Black);
            Squares[0, 7] = new ChessPiece(PieceType.Rook, PieceColor.Black);
            
            // Black pawns (row 1)
            for (int col = 0; col < 8; col++)
            {
                Squares[1, col] = new ChessPiece(PieceType.Pawn, PieceColor.Black);
            }

            // Set up white pieces (row 7)
            Squares[7, 0] = new ChessPiece(PieceType.Rook, PieceColor.White);
            Squares[7, 1] = new ChessPiece(PieceType.Knight, PieceColor.White);
            Squares[7, 2] = new ChessPiece(PieceType.Bishop, PieceColor.White);
            Squares[7, 3] = new ChessPiece(PieceType.Queen, PieceColor.White);
            Squares[7, 4] = new ChessPiece(PieceType.King, PieceColor.White);
            Squares[7, 5] = new ChessPiece(PieceType.Bishop, PieceColor.White);
            Squares[7, 6] = new ChessPiece(PieceType.Knight, PieceColor.White);
            Squares[7, 7] = new ChessPiece(PieceType.Rook, PieceColor.White);
            
            // White pawns (row 6)
            for (int col = 0; col < 8; col++)
            {
                Squares[6, col] = new ChessPiece(PieceType.Pawn, PieceColor.White);
            }
        }
    }
}