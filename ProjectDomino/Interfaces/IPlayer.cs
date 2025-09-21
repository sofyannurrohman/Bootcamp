using DominoConsoleMVC.Models;

namespace DominoConsoleMVC.Interfaces;

public interface IPlayer
{
    string Name { get; }
    bool IsHuman { get; }
    List<DominoTile> Hand { get; }
    void AddTile(DominoTile tile);
    void RemoveTile(DominoTile tile);
    bool HasNoTiles();
    int Score { get; }
}
