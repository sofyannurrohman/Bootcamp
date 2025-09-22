// Core/CardProvider.cs
using BootcampDay8.PokerGame.Enums;
using BootcampDay8.PokerGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootcampDay8.PokerGame.Core
{
    public class CardProvider : ICardProvider
    {
        private List<Card> _deck = new List<Card>();
        private Random _random = new Random();

        public CardProvider()
        {
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            _deck.Clear();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _deck.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            _deck = _deck.OrderBy(c => _random.Next()).ToList();
        }

        public Card DrawCard()
        {
            if (_deck.Count == 0)
                throw new InvalidOperationException("Deck is empty");

            var card = _deck[0];
            _deck.RemoveAt(0);
            return card;
        }

        public int CardsRemaining() => _deck.Count;
    }
}