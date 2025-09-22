using Battleship.Models;
namespace Battleship.Interfaces;
public interface IPlayer
{
    string Name { get; }
    Board Board { get; }
    (int Row, int Col) SelectAttack();
}
