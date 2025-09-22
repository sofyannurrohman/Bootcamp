// MainWindow.xaml.cs
using BootcampDay8.PokerGame.Core;
using BootcampDay8.PokerGame.Utilities;
using BootcampDay8.PokerGame.ViewModels;
using System.Windows;

namespace BootcampDay8.PokerGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Initialize game components
            var deck = new Deck();
            var handEvaluator = new PokerHandEvaluator();
            var game = new Core.PokerGame(deck, handEvaluator);
            var cardProvider = new CardProvider();
            var pokerGame = new Core.PokerGame(cardProvider, handEvaluator);

            // Add event logger
            var logger = new GameEventLogger();
            game.AddEventHandler(logger);

            // Add players
            game.AddPlayer(new Player("Player 1", 1000));
            game.AddPlayer(new Player("Player 2", 1000));
            game.AddPlayer(new Player("Player 3", 1000));
            game.AddPlayer(new Player("Player 4", 1000));
           
            // Set data context
            DataContext = new PokerGameViewModel(game);
        }
    }
}