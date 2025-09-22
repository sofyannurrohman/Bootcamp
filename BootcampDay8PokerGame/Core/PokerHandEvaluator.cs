// Core/PokerHandEvaluator.cs
using BootcampDay8.PokerGame.Enums;
using BootcampDay8.PokerGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootcampDay8.PokerGame.Core
{
    public class PokerHandEvaluator : IHandEvaluator
    {
        public HandRank EvaluateHand(IReadOnlyList<Card> cards)
        {
            if (cards == null || cards.Count < 5)
                throw new ArgumentException("At least 5 cards are required to evaluate a hand");

            bool isFlush = IsFlush(cards);
            bool isStraight = IsStraight(cards);
            var rankCounts = GetRankCounts(cards);

            return DetermineHandRank(rankCounts, isFlush, isStraight);
        }

        private bool IsFlush(IReadOnlyList<Card> cards)
        {
            return cards.GroupBy(c => c.Suit).Count() == 1;
        }

        private bool IsStraight(IReadOnlyList<Card> cards)
        {
            var sortedRanks = cards.Select(c => c.Rank).Distinct().OrderBy(r => r).ToList();
            
            // Check for regular straight
            if (sortedRanks.Count >= 5)
            {
                for (int i = 0; i <= sortedRanks.Count - 5; i++)
                {
                    if ((int)sortedRanks[i + 4] - (int)sortedRanks[i] == 4)
                        return true;
                }
            }
            
            // Check for Ace-low straight (A-2-3-4-5)
            if (sortedRanks.Contains(Rank.Ace) && sortedRanks.Contains(Rank.Two) && 
                sortedRanks.Contains(Rank.Three) && sortedRanks.Contains(Rank.Four) && 
                sortedRanks.Contains(Rank.Five))
            {
                return true;
            }
            
            return false;
        }

        private Dictionary<Rank, int> GetRankCounts(IReadOnlyList<Card> cards)
        {
            var rankCounts = new Dictionary<Rank, int>();
            foreach (var card in cards)
            {
                if (rankCounts.ContainsKey(card.Rank))
                    rankCounts[card.Rank]++;
                else
                    rankCounts[card.Rank] = 1;
            }
            return rankCounts;
        }

        private HandRank DetermineHandRank(Dictionary<Rank, int> rankCounts, bool isFlush, bool isStraight)
        {
            // Check for Royal Flush
            if (isFlush && isStraight && rankCounts.ContainsKey(Rank.Ace) && 
                rankCounts.ContainsKey(Rank.King))
                return HandRank.RoyalFlush;

            // Check for Straight Flush
            if (isFlush && isStraight)
                return HandRank.StraightFlush;

            // Check for Four of a Kind
            if (rankCounts.Values.Any(count => count == 4))
                return HandRank.FourOfAKind;

            // Check for Full House
            if (rankCounts.Values.Any(count => count == 3) && rankCounts.Values.Any(count => count == 2))
                return HandRank.FullHouse;

            // Check for Flush
            if (isFlush)
                return HandRank.Flush;

            // Check for Straight
            if (isStraight)
                return HandRank.Straight;

            // Check for Three of a Kind
            if (rankCounts.Values.Any(count => count == 3))
                return HandRank.ThreeOfAKind;

            // Check for Two Pair
            if (rankCounts.Values.Count(count => count == 2) == 2)
                return HandRank.TwoPair;

            // Check for One Pair
            if (rankCounts.Values.Any(count => count == 2))
                return HandRank.OnePair;

            // High Card
            return HandRank.HighCard;
        }
    }
}