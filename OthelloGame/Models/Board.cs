using OthelloConsoleMVC.Interfaces;
using OthelloConsoleMVC.Models;

public class OthelloBoard : IBoard
{
    private Piece?[,] _grid;
    public int Size { get; } = 8;

    public OthelloBoard()
    {
        _grid = new Piece?[Size, Size];
        // Initial 4 pieces
        _grid[3, 3] = new Piece { Color = PieceColor.White };
        _grid[3, 4] = new Piece { Color = PieceColor.Black };
        _grid[4, 3] = new Piece { Color = PieceColor.Black };
        _grid[4, 4] = new Piece { Color = PieceColor.White };
    }

    public Piece? GetPiece(int row, int col) => _grid[row, col];

    public void PlacePiece(int row, int col, PieceColor color)
    {
        _grid[row, col] = new Piece { Color = color };
        FlipPieces(row, col, color);
    }

    public List<(int row, int col)> GetValidMoves(PieceColor playerColor)
    {
        // Implement logic to return all valid positions where a player can place a piece
        throw new NotImplementedException();
    }

    public void FlipPieces(int row, int col, PieceColor playerColor)
    {
        // Implement flipping logic in all 8 directions
        throw new NotImplementedException();
    }
}
