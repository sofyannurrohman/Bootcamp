using PokerConsoleApp.Core.Enums;

namespace PokerConsoleApp.Core;

public class Deck
{
    private readonly List<Card> _cards;
    private readonly Random _random;

    public int CardsRemaining => _cards.Count;

    public Deck()
    {
        _cards = new List<Card>();
        _random = new Random();
        InitializeDeck();
    }

    private void InitializeDeck()
    {
        _cards.Clear();
        
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                _cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    public Card DealCard()
    {
        if (_cards.Count == 0)
            throw new InvalidOperationException("Deck is empty");

        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public void Reset()
    {
        InitializeDeck();
        Shuffle();
    }
}