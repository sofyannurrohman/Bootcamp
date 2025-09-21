using System.Collections.Generic;
using DominoConsoleMVC.Interfaces;


namespace DominoConsoleMVC.Models
{
    public class Boneyard : IBoneyard
{
    private readonly Stack<DominoTile> _tiles = new();
    public int Count => _tiles.Count;
    public bool IsEmpty => _tiles.Count == 0;
    public void Push(DominoTile tile) => _tiles.Push(tile);
    public DominoTile? Draw() => _tiles.Count > 0 ? _tiles.Pop() : null;
}

}