using BootcampDay8.PokerGame.Core;
using BootcampDay8.PokerGame.Enums;
namespace BootcampDay8.PokerGame.Interfaces
{
    public interface IGameEventHandler
    {
        void OnGameStateChanged(GameState newState);
        void OnCardDealt(Card card, Player player);
        void OnCardDiscarded(Card card, Player player);
        void OnBettingRoundStarted();
        void OnPlayerFolded(Player player);
        void OnPlayerChecked(Player player);
        void OnPlayerCalled(Player player, int amount);
        void OnPlayerRaised(Player player, int amount);
        void OnPlayerBet(Player player, int amount);
        void OnDiscardRoundStarted();
        void OnPlayerRevealedHand(Player player);
    }
}