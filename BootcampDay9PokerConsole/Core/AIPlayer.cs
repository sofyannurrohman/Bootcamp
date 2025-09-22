namespace PokerConsoleApp.Core;

public class AIPlayer : Player
{
    private readonly Random _random;

    public AIPlayer(string name, int startingChips) : base(name, startingChips)
    {
        _random = new Random();
    }

    public override string MakeDecision(int currentBet, int minRaise)
    {
        var handStrength = EvaluateHandStrength();
        var aggression = _random.NextDouble();

        if (currentBet == 0)
        {
            // No current bet - can check or bet
            if (handStrength > 0.4 && aggression > 0.4)
            {
                var betAmount = Math.Min((int)(Chips * (handStrength * 0.3)), Chips);
                betAmount = Math.Max(betAmount, minRaise); // Ensure minimum bet
                return $"bet {betAmount}";
            }
            return "check";
        }
        else
        {
            // There's a current bet
            var callAmount = currentBet - CurrentBet;

            // Fold if hand is weak and bet is high
            if (handStrength < 0.3 && callAmount > Chips * 0.2 && aggression < 0.6)
                return "fold";

            // Call if reasonable
            if (callAmount > 0 && callAmount < Chips * 0.3)
                return $"call {callAmount}";

            // Raise with good hand or aggression
            if ((handStrength > 0.5 || aggression > 0.5) && Chips > minRaise + callAmount)
            {
                var raiseAmount = Math.Min((int)(Chips * (handStrength * 0.2)), Chips - callAmount);
                raiseAmount = Math.Max(raiseAmount, minRaise); // Ensure minimum raise
                return $"raise {raiseAmount}";
            }

            // Default to call if can afford, otherwise fold
            return callAmount <= Chips ? $"call {callAmount}" : "fold";
        }
    }
    private double EvaluateHandStrength()
    {
        // Simple hand evaluation based on card ranks
        var cards = Hand.Cards;
        if (cards.Count < 2) return 0.0;

        var highCard = Math.Max((int)cards[0].Rank, (int)cards[1].Rank) / 14.0;
        var isPair = cards[0].Rank == cards[1].Rank;

        return isPair ? 0.8 + highCard * 0.2 : highCard;
    }
}