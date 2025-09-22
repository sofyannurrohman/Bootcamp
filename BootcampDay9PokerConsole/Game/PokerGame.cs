using PokerConsoleApp.Core;
using PokerConsoleApp.Delegates;

namespace PokerConsoleApp.Game;

public class PokerGame
{
    public event GameEventDelegate OnGameEvent;
    public event PlayerDecisionDelegate OnPlayerDecision;

    private readonly List<Player> _players;
    private readonly Deck _deck;
    private int _pot;
    private int _currentBet;
    private int _minRaise;
    private Player _dealer;
    private int _currentPlayerIndex;
    private readonly List<Card> _communityCards;
    private readonly int _bigBlind;
    private int _smallBlindIndex;
    private int _bigBlindIndex;
    public PokerGame(List<Player> players)
    {
        _players = players;
        _deck = new Deck();
        _pot = 0;
        _currentBet = 0;
        _minRaise = 10;
        _dealer = players[0];
        _communityCards = new List<Card>();
        _bigBlind = 50; // atau 100, sesuai keinginan
        _minRaise = _bigBlind;
        _smallBlindIndex = 0;
        _bigBlindIndex = 1;
    }
    private void DealCommunityCards(int count)
    {
        OnGameEvent?.Invoke($"\n--- Dealing {count} Community Cards ---");
        for (int i = 0; i < count; i++)
        {
            var card = _deck.DealCard();
            _communityCards.Add(card);
            OnGameEvent?.Invoke($"Community card {i + 1}: {card}");
        }
    }
    public void StartGame()
    {
        OnGameEvent?.Invoke("=== POKER GAME STARTED ===");

        while (_players.Count(p => p.Chips > 0) > 1)
        {
            PlayRound();
        }

        var winner = _players.First(p => p.Chips > 0);
        OnGameEvent?.Invoke($"\n=== GAME OVER ===");
        OnGameEvent?.Invoke($"{winner.Name} wins the game!");
    }

    private void PlayRound()
    {
        ResetRound();
        PostBlinds();
        DealCards();
        BettingRound();

        if (_players.Count(p => !p.IsFolded) > 1)
        {
            DealCommunityCards(3); // Flop
            BettingRound();
        }

        if (_players.Count(p => !p.IsFolded) > 1)
        {
            DealCommunityCards(1); // Turn
            BettingRound();
        }

        if (_players.Count(p => !p.IsFolded) > 1)
        {
            DealCommunityCards(1); // River
            BettingRound();
        }

        if (_players.Count(p => !p.IsFolded) > 1)
        {
            Showdown();
        }

        DistributePot();
    }
    private void PostBlinds()
    {
        var smallBlindPlayer = _players[_smallBlindIndex];
        var bigBlindPlayer = _players[_bigBlindIndex];

        var smallBlindAmount = _bigBlind / 2;
        var bigBlindAmount = _bigBlind;

        if (smallBlindPlayer.PlaceBet(smallBlindAmount))
        {
            _pot += smallBlindAmount;
            OnGameEvent?.Invoke($"{smallBlindPlayer.Name} posts small blind: {smallBlindAmount}");
        }

        if (bigBlindPlayer.PlaceBet(bigBlindAmount))
        {
            _pot += bigBlindAmount;
            _currentBet = bigBlindAmount;
            OnGameEvent?.Invoke($"{bigBlindPlayer.Name} posts big blind: {bigBlindAmount}");
        }

        // Rotate blinds for next round
        _smallBlindIndex = (_smallBlindIndex + 1) % _players.Count;
        _bigBlindIndex = (_bigBlindIndex + 1) % _players.Count;
    }
    private void ResetRound()
    {
        _deck.Reset();
        _pot = 0;
        _currentBet = 0;
        _minRaise = _bigBlind; // Reset ke big blind setiap round baru
        _communityCards.Clear();

        foreach (var player in _players)
        {
            player.ResetForNewRound();
        }
    }

    private void DealCards()
    {
        OnGameEvent?.Invoke("\n--- Dealing Cards ---");

        foreach (var player in _players)
        {
            player.ReceiveCard(_deck.DealCard());
            player.ReceiveCard(_deck.DealCard());

            if (player is HumanPlayer)
            {
                OnGameEvent?.Invoke($"{player.Name}'s hand: {player.Hand}");
            }
        }
    }

    private void BettingRound()
    {
        OnGameEvent?.Invoke("\n--- Betting Round ---");
        if (_communityCards.Count > 0)
        {
            OnGameEvent?.Invoke($"Community cards: {string.Join(" ", _communityCards)}");
        }
        var activePlayers = _players.Where(p => !p.IsFolded).ToList();
        var currentIndex = 0;
        var betsSettled = false;

        while (!betsSettled && activePlayers.Count > 1)
        {
            var currentPlayer = activePlayers[currentIndex];

            if (currentPlayer.CurrentBet < _currentBet || _currentBet == 0)
            {
                ProcessPlayerTurn(currentPlayer);
            }

            // Check if all bets are settled
            betsSettled = activePlayers.All(p =>
                p.IsFolded || p.CurrentBet == _currentBet || p.Chips == 0);

            currentIndex = (currentIndex + 1) % activePlayers.Count;
        }
    }

    private void ProcessPlayerTurn(Player player)
    {
        var decision = OnPlayerDecision?.Invoke(player, _currentBet, _minRaise);

        if (decision == null) return;

        if (decision == "fold")
        {
            player.Fold();
            OnGameEvent?.Invoke($"{player.Name} folds");
        }
        else if (decision == "check")
        {
            OnGameEvent?.Invoke($"{player.Name} checks");
        }
        else if (decision.StartsWith("call"))
        {
            var amount = int.Parse(decision.Split(' ')[1]);
            if (player.PlaceBet(amount))
            {
                _pot += amount;
                OnGameEvent?.Invoke($"{player.Name} calls {amount}");
            }
        }
        else if (decision.StartsWith("bet"))
        {
            var amount = int.Parse(decision.Split(' ')[1]);
            if (player.PlaceBet(amount))
            {
                _pot += amount;
                _currentBet = amount;
                _minRaise = amount; // Minimum raise = amount yang di-bet
                OnGameEvent?.Invoke($"{player.Name} bets {amount}");
            }
        }
        else if (decision.StartsWith("raise"))
        {
            var raiseAmount = int.Parse(decision.Split(' ')[1]);
            var totalToCall = _currentBet - player.CurrentBet;
            var totalAmount = totalToCall + raiseAmount;

            if (player.PlaceBet(totalAmount))
            {
                _pot += totalAmount;
                _currentBet = player.CurrentBet;
                _minRaise = raiseAmount; // Minimum raise = amount raise
                OnGameEvent?.Invoke($"{player.Name} raises to {_currentBet}");
            }
        }
    }

    private void Showdown()
    {
        OnGameEvent?.Invoke("\n--- Showdown ---");
        OnGameEvent?.Invoke($"Community cards: {string.Join(" ", _communityCards)}");

        var activePlayers = _players.Where(p => !p.IsFolded).ToList();
        var handStrengths = new Dictionary<Player, (string handType, int strength)>();

        foreach (var player in activePlayers)
        {
            var handValue = HandEvaluator.EvaluateHand(player.Hand.Cards.ToList(), _communityCards);
            var strength = HandEvaluator.GetHandStrength(handValue);
            handStrengths[player] = (handValue, strength);

            OnGameEvent?.Invoke($"{player.Name}: {player.Hand} - {handValue}");
        }

        var winners = handStrengths.OrderByDescending(h => h.Value.strength)
                                 .ThenByDescending(h => GetHighCardValue(h.Key.Hand.Cards.ToList(), _communityCards))
                                 .GroupBy(h => h.Value.strength)
                                 .First()
                                 .Select(g => g.Key)
                                 .ToList();

        if (winners.Count == 1)
        {
            OnGameEvent?.Invoke($"\n{winners[0].Name} wins with {handStrengths[winners[0]].handType}!");
        }
        else
        {
            OnGameEvent?.Invoke($"\nSplit pot between: {string.Join(", ", winners.Select(w => w.Name))}");
        }
    }

    private int GetHighCardValue(List<Card> holeCards, List<Card> communityCards)
    {
        var allCards = holeCards.Concat(communityCards).ToList();
        return allCards.Max(c => (int)c.Rank);
    }

    private void DistributePot()
    {
        var winner = _players.First(p => !p.IsFolded);
        winner.AddChips(_pot);
        OnGameEvent?.Invoke($"{winner.Name} wins {_pot} chips!");
    }
}