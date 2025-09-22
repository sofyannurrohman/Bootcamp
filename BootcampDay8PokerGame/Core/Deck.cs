// Core/Deck.cs
using BootcampDay8.PokerGame.Enums;
using BootcampDay8.PokerGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootcampDay8.PokerGame.Core
{
    public class Deck : ICardProvider
    {
        private Stack<Card> _cards;

        public Deck()
        {
            _cards = new Stack<Card>();
            InitializeDeck();
        }

        public void Shuffle()
        {
            var random = new Random();
            var cardsList = _cards.ToList();
            _cards.Clear();

            while (cardsList.Count > 0)
            {
                int index = random.Next(cardsList.Count);
                var card = cardsList[index];
                cardsList.RemoveAt(index);
                _cards.Push(card);
            }
        }

        public Card DrawCard()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("Deck is empty");

            return _cards.Pop();
        }

        public int CardsRemaining()
        {
            return _cards.Count;
        }

        public void Reset()
        {
            _cards.Clear();
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Push(new Card(suit, rank));
                }
            }
            Shuffle();
        }
    }
}