// Core/Hand.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootcampDay8.PokerGame.Core
{
    public class Hand
    {
        private List<Card> _cards = new List<Card>();

        public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

        public void AddCard(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));
            
            _cards.Add(card);
        }

        public bool RemoveCard(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));
            
            return _cards.Remove(card);
        }

        public Card RemoveCardAt(int index)
        {
            if (index < 0 || index >= _cards.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var card = _cards[index];
            _cards.RemoveAt(index);
            return card;
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public int Count => _cards.Count;

        // Method to check if hand contains a specific card
        public bool Contains(Card card)
        {
            return _cards.Contains(card);
        }

        // Method to get card at specific index
        public Card GetCardAt(int index)
        {
            if (index < 0 || index >= _cards.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            
            return _cards[index];
        }

        // Method to find index of a card
        public int IndexOf(Card card)
        {
            return _cards.IndexOf(card);
        }

        // Method to remove multiple cards
        public void RemoveCards(IEnumerable<Card> cardsToRemove)
        {
            foreach (var card in cardsToRemove.ToList()) // ToList to avoid collection modified exception
            {
                _cards.Remove(card);
            }
        }

        // Method to get cards by indices
        public List<Card> GetCardsByIndices(IEnumerable<int> indices)
        {
            var result = new List<Card>();
            foreach (var index in indices)
            {
                if (index >= 0 && index < _cards.Count)
                {
                    result.Add(_cards[index]);
                }
            }
            return result;
        }

        // Method to remove cards by indices
        public List<Card> RemoveCardsByIndices(IEnumerable<int> indices)
        {
            var sortedIndices = indices.OrderByDescending(i => i).ToList();
            var removedCards = new List<Card>();

            foreach (var index in sortedIndices)
            {
                if (index >= 0 && index < _cards.Count)
                {
                    var card = _cards[index];
                    _cards.RemoveAt(index);
                    removedCards.Add(card);
                }
            }

            return removedCards;
        }

        public override string ToString()
        {
            return string.Join(", ", _cards.Select(c => c.ToString()));
        }

        // Method to sort hand (useful for display and evaluation)
        public void Sort()
        {
            _cards.Sort((c1, c2) => 
            {
                int rankComparison = c1.Rank.CompareTo(c2.Rank);
                return rankComparison != 0 ? rankComparison : c1.Suit.CompareTo(c2.Suit);
            });
        }

        // Method to get a copy of all cards (for hand evaluation)
        public List<Card> GetAllCards()
        {
            return new List<Card>(_cards);
        }
    }
}