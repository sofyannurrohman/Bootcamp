// Utilities/GameEventLogger.cs
using BootcampDay8.PokerGame.Core;
using BootcampDay8.PokerGame.Enums;
using BootcampDay8.PokerGame.Interfaces;

namespace BootcampDay8.PokerGame.Utilities
{
    public class GameEventLogger : IGameEventHandler
{
    public void OnGameStateChanged(GameState newState)
    {
        System.Diagnostics.Debug.WriteLine($"Game state changed to: {newState}");
    }

    public void OnCardDealt(Card card, Player player)
    {
        System.Diagnostics.Debug.WriteLine($"Card dealt: {card} to {player.Name}");
    }

    public void OnCardDiscarded(Card card, Player player)
    {
        System.Diagnostics.Debug.WriteLine($"Card discarded: {card} from {player.Name}");
    }

    public void OnBettingRoundStarted()
    {
        System.Diagnostics.Debug.WriteLine("Betting round started");
    }

    public void OnPlayerFolded(Player player)
    {
        System.Diagnostics.Debug.WriteLine($"{player.Name} folded");
    }

    public void OnPlayerChecked(Player player)
    {
        System.Diagnostics.Debug.WriteLine($"{player.Name} checked");
    }

    public void OnPlayerCalled(Player player, int amount)
    {
        System.Diagnostics.Debug.WriteLine($"{player.Name} called with {amount}");
    }

    public void OnPlayerRaised(Player player, int amount)
    {
        System.Diagnostics.Debug.WriteLine($"{player.Name} raised to {amount}");
    }

    public void OnPlayerBet(Player player, int amount)
    {
        System.Diagnostics.Debug.WriteLine($"{player.Name} bet {amount}");
    }

    public void OnDiscardRoundStarted()
    {
        System.Diagnostics.Debug.WriteLine("Discard round started");
    }

    public void OnPlayerRevealedHand(Player player)
    {
        System.Diagnostics.Debug.WriteLine($"{player.Name} revealed their hand");
    }
}
}