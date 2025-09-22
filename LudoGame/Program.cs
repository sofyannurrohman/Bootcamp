using LudoConsoleMVC.Controllers;
using LudoConsoleMVC.Interfaces;
using LudoConsoleMVC.Models;
using LudoConsoleMVC.Players;
using LudoConsoleMVC.Rules;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        IBoard board = new LudoBoard();
        IGameRules rules = new LudoRules();
        List<IPlayer> players = new()
        {
            new HumanPlayer("Alice", PieceColor.Red),
            new SmartAIPlayer("Bob AI", PieceColor.Blue),
            new SmartAIPlayer("Carol AI", PieceColor.Green),
            new HumanPlayer("Dave", PieceColor.Yellow)
        };

        var controller = new LudoGameController(board, players, rules);
        controller.Start();
    }
}
