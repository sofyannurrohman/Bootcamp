using OthelloConsoleMVC.Interfaces;
using OthelloConsoleMVC.Models;

public class OthelloGameController
{
    private readonly IBoard _board;
    private readonly IPlayer _blackPlayer;
    private readonly IPlayer _whitePlayer;
    private IPlayer _currentPlayer;

    public OthelloGameController(IBoard board, IPlayer blackPlayer, IPlayer whitePlayer)
    {
        _board = board;
        _blackPlayer = blackPlayer;
        _whitePlayer = whitePlayer;
        _currentPlayer = _blackPlayer;
    }

    public void Start()
    {
        while (!IsGameOver())
        {
            PlayTurn();
            Console.WriteLine(_board.ToString());
            SwitchTurn();
        }

        Console.WriteLine("Game over!");
        // Count pieces and declare winner
    }

    private void PlayTurn()
    {
        var validMoves = _board.GetValidMoves(_currentPlayer.Color);
        if (!validMoves.Any())
        {
            Console.WriteLine($"{_currentPlayer.Name} has no valid moves. Skipping turn.");
            return;
        }

        var move = _currentPlayer.SelectMove(_board, validMoves);
        _board.PlacePiece(move.row, move.col, _currentPlayer.Color);
    }

    private void SwitchTurn()
    {
        _currentPlayer = _currentPlayer == _blackPlayer ? _whitePlayer : _blackPlayer;
    }

    private bool IsGameOver()
    {
        return !_board.GetValidMoves(PieceColor.Black).Any() &&
               !_board.GetValidMoves(PieceColor.White).Any();
    }
}
