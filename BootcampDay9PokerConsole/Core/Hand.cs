namespace PokerConsoleApp.Core;

public class Hand
{
    private readonly List<Card> _cards;

    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

    public Hand()
    {
        _cards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        if (_cards.Count >= 2)
            throw new InvalidOperationException("Hand can only hold 2 cards");

        _cards.Add(card);
    }

    public void Clear()
    {
        _cards.Clear();
    }

    public override string ToString()
    {
        return string.Join(" ", _cards.Select(c => c.ToString()));
    }
}