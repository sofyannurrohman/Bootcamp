using PokerConsoleApp.Core;
using PokerConsoleApp.Core.Enums;

namespace PokerConsoleApp.Game;

public static class HandEvaluator
{
    public static string EvaluateHand(List<Card> holeCards, List<Card> communityCards)
    {
        if (holeCards.Count != 2 || communityCards.Count < 3)
            return "Not enough cards";
            
        var allCards = holeCards.Concat(communityCards).ToList();
        return EvaluateBestHand(allCards);
    }
    
    private static string EvaluateBestHand(List<Card> allCards)
    {
        if (allCards.Count < 5) return "Not enough cards";
        
        // Generate all possible 5-card combinations
        var bestHand = "High Card";
        var bestStrength = 0;
        
        var combinations = GetCombinations(allCards, 5);
        foreach (var combo in combinations)
        {
            var handType = EvaluateHand(combo);
            var strength = GetHandStrength(handType);
            
            if (strength > bestStrength)
            {
                bestStrength = strength;
                bestHand = handType;
            }
        }
        
        return bestHand;
    }
    public static string EvaluateHand(List<Card> cards)
    {
        if (cards.Count < 5) return "Not enough cards";

        if (IsRoyalFlush(cards)) return "Royal Flush";
        if (IsStraightFlush(cards)) return "Straight Flush";
        if (IsFourOfAKind(cards)) return "Four of a Kind";
        if (IsFullHouse(cards)) return "Full House";
        if (IsFlush(cards)) return "Flush";
        if (IsStraight(cards)) return "Straight";
        if (IsThreeOfAKind(cards)) return "Three of a Kind";
        if (IsTwoPair(cards)) return "Two Pair";
        if (IsOnePair(cards)) return "One Pair";
        return "High Card";
    }

    private static IEnumerable<List<Card>> GetCombinations(List<Card> cards, int k)
    {
        // Simple combination generator for 7 choose 5
        var result = new List<List<Card>>();
        
        if (k == 0)
        {
            result.Add(new List<Card>());
            return result;
        }
        
        for (int i = 0; i <= cards.Count - k; i++)
        {
            var rest = cards.Skip(i + 1).ToList();
            foreach (var combo in GetCombinations(rest, k - 1))
            {
                combo.Insert(0, cards[i]);
                result.Add(combo);
            }
        }
        
        return result;
    }
    public static int GetHandStrength(string handType) => handType switch
    {
        "Royal Flush" => 10,
        "Straight Flush" => 9,
        "Four of a Kind" => 8,
        "Full House" => 7,
        "Flush" => 6,
        "Straight" => 5,
        "Three of a Kind" => 4,
        "Two Pair" => 3,
        "One Pair" => 2,
        _ => 1
    };

    private static bool IsFlush(List<Card> cards) =>
        cards.GroupBy(c => c.Suit).Any(g => g.Count() >= 5);

    private static bool IsStraight(List<Card> cards)
    {
        var ranks = cards.Select(c => (int)c.Rank).Distinct().OrderBy(r => r).ToList();
        if (ranks.Count < 5) return false;

        for (int i = 0; i <= ranks.Count - 5; i++)
        {
            if (ranks[i + 4] - ranks[i] == 4)
                return true;
        }

        // Check for wheel straight (A-2-3-4-5)
        return ranks.Contains(14) && ranks.Contains(2) && ranks.Contains(3) && 
               ranks.Contains(4) && ranks.Contains(5);
    }

    private static bool IsStraightFlush(List<Card> cards) => 
        IsFlush(cards) && IsStraight(cards);

    private static bool IsRoyalFlush(List<Card> cards) =>
        IsStraightFlush(cards) && cards.Any(c => c.Rank == Rank.Ace) && 
        cards.Any(c => c.Rank == Rank.King);

    private static bool IsFourOfAKind(List<Card> cards) =>
        cards.GroupBy(c => c.Rank).Any(g => g.Count() >= 4);

    private static bool IsFullHouse(List<Card> cards)
    {
        var groups = cards.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ToList();
        return groups.Count >= 2 && groups[0].Count() >= 3 && groups[1].Count() >= 2;
    }

    private static bool IsThreeOfAKind(List<Card> cards) =>
        cards.GroupBy(c => c.Rank).Any(g => g.Count() >= 3);

    private static bool IsTwoPair(List<Card> cards) =>
        cards.GroupBy(c => c.Rank).Count(g => g.Count() >= 2) >= 2;

    private static bool IsOnePair(List<Card> cards) =>
        cards.GroupBy(c => c.Rank).Any(g => g.Count() >= 2);
}