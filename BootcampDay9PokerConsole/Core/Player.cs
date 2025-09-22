namespace PokerConsoleApp.Core;

public abstract class Player
{
    public string Name { get; }
    public Hand Hand { get; }
    public int Chips { get; protected set; }
    public bool IsFolded { get; protected set; }
    public int CurrentBet { get; protected set; }

    protected Player(string name, int startingChips)
    {
        Name = name;
        Hand = new Hand();
        Chips = startingChips;
        IsFolded = false;
        CurrentBet = 0;
    }

    public virtual void ReceiveCard(Card card)
    {
        Hand.AddCard(card);
    }

    public virtual void Fold()
    {
        IsFolded = true;
    }

    public virtual void ResetForNewRound()
    {
        Hand.Clear();
        IsFolded = false;
        CurrentBet = 0;
    }

    public bool PlaceBet(int amount)
    {
        if (amount > Chips)
            return false;

        Chips -= amount;
        CurrentBet += amount;
        return true;
    }

    public void AddChips(int amount)
    {
        Chips += amount;
    }

    public abstract string MakeDecision(int currentBet, int minRaise);
}