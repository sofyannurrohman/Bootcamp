using DominoConsoleMVC.Models;

namespace DominoConsoleMVC.Interfaces;
public interface IBoneyard
{
    int Count { get; }
    bool IsEmpty { get; }
    DominoTile? Draw();
    void Push(DominoTile tile);
}