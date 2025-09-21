using DominoConsoleMVC.Interfaces;
using DominoConsoleMVC.Models; // <- where Board and Boneyard live

namespace DominoConsoleMVC
{
    class Program
    {
        static void Main(string[] args)
        {
            var view = new Views.ConsoleView();
            var rules = new Rules.StandardDominoRules();
            IBoard board = new Board();       // instantiate class that implements IBoard
            IBoneyard boneyard = new Boneyard(); // instantiate class that implements IBoneyard

            var controller = new Controllers.GameController(view, rules, board, boneyard);

            controller.StartGame();
        }
    }
}
