// Core/Card.cs
using BootcampDay8.PokerGame.Enums;
using System;
using System.IO;

namespace BootcampDay8.PokerGame.Core
{
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }
        public bool IsFaceUp { get; set; }
        public string ImagePath { get; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            IsFaceUp = false;
            ImagePath = GetImagePath(suit, rank);
        }

        private string GetImagePath(Suit suit, Rank rank)
        {
            string suitName = suit switch
            {
                Suit.Hearts => "Hearts",
                Suit.Diamonds => "Tiles", // Your assets use "Tiles" for Diamonds
                Suit.Clubs => "Clovers",  // Your assets use "Clovers" for Clubs
                Suit.Spades => "Pikes",   // Your assets use "Pikes" for Spades
                _ => "Unknown"
            };

            string rankName = rank switch
            {
                Rank.Ace => "A",
                Rank.King => "King",
                Rank.Queen => "Queen",
                Rank.Jack => "Jack",
                Rank.Ten => "10",
                Rank.Nine => "9",
                Rank.Eight => "8",
                Rank.Seven => "7",
                Rank.Six => "6",
                Rank.Five => "5",
                Rank.Four => "4",
                Rank.Three => "3",
                Rank.Two => "2",
                _ => "Unknown"
            };

            // Use pack URI for WPF resources
            return $"Assets/Cards/{suitName}_{rankName}_white.png";
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Card other)
            {
                return Suit == other.Suit && Rank == other.Rank;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Suit, Rank);
        }
    }
}