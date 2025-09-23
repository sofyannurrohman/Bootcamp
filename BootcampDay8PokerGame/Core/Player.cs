// Core/Player.cs
using System.Linq;

namespace BootcampDay8.PokerGame.Core
{
    public class Player
    {
        private int _currentBet;   // backing field

        public string Name { get; }
        public int Chips { get; private set; }
        public Hand Hand { get; }
        public bool IsDealer { get; set; }
        public bool IsActive { get; set; }
        public bool HasFolded { get; set; }
        public int CurrentBet => _currentBet;

        public Player(string name, int initialChips)
        {
            Name = name;
            Chips = initialChips;
            Hand = new Hand();
            IsActive = true;
            HasFolded = false;
            _currentBet = 0;
        }

        // PlaceBet now also increments the player's current bet
        public int PlaceBet(int amount)
        {
            if (amount > Chips)
                amount = Chips;

            Chips -= amount;
            _currentBet += amount;
            return amount;
        }

        public void ReceiveWinnings(int amount)
        {
            Chips += amount;
        }

        public void Fold()
        {
            HasFolded = true;
            IsActive = false;
        }

        public void ResetForNewRound()
        {
            Hand.Clear();
            IsActive = true;
            HasFolded = false;
            _currentBet = 0;
        }

        // Reset just the current bet (useful between betting rounds)
        public void ResetCurrentBet()
        {
            _currentBet = 0;
        }

        public string GetHandDescription()
        {
            if (Hand.Count == 0)
                return "No cards";

            return string.Join(", ", Hand.Cards.Select(c => c.ToString()));
        }
    }
}
