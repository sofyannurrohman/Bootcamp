using PokerConsoleApp.Core;

namespace PokerConsoleApp.Delegates;

public delegate void GameEventDelegate(string message);
public delegate string PlayerDecisionDelegate(Player player, int currentBet, int minRaise);