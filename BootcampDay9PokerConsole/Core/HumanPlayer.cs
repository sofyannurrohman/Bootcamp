namespace PokerConsoleApp.Core;

public class HumanPlayer : Player
{
    public HumanPlayer(string name, int startingChips) : base(name, startingChips)
    {
    }

    public override string MakeDecision(int currentBet, int minRaise)
    {
        Console.WriteLine($"\n=== {Name}'s Turn ===");
        Console.WriteLine($"Your hand: {Hand}");
        Console.WriteLine($"Your chips: {Chips}");
        Console.WriteLine($"Current bet: {currentBet}, Your current bet: {CurrentBet}");
        
        // Tampilkan minimum raise hanya jika relevant
        if (currentBet > 0)
        {
            Console.WriteLine($"Minimum raise: {minRaise}");
        }
        else
        {
            Console.WriteLine($"Minimum bet: {minRaise}");
        }

        while (true)
        {
            // Tampilkan options yang sesuai dengan situasi
            if (currentBet == 0)
            {
                Console.WriteLine("\nOptions: check, bet [amount], allin");
            }
            else if (currentBet > CurrentBet)
            {
                Console.WriteLine("\nOptions: fold, call, raise [amount], allin");
            }
            else
            {
                Console.WriteLine("\nOptions: check, raise [amount], allin");
            }

            Console.Write("Your decision: ");
            var input = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(input))
                continue;

            if (input == "fold")
            {
                if (currentBet == 0)
                {
                    Console.WriteLine("Cannot fold when no bet is placed");
                    continue;
                }
                return "fold";
            }

            if (input == "check")
            {
                if (currentBet > CurrentBet)
                {
                    Console.WriteLine("Cannot check - you need to call or fold");
                    continue;
                }
                return "check";
            }

            if (input == "call")
            {
                if (currentBet == 0)
                {
                    Console.WriteLine("No bet to call - use 'check' or 'bet'");
                    continue;
                }

                var callAmount = currentBet - CurrentBet;
                if (callAmount == 0)
                {
                    Console.WriteLine("No need to call - current bet is matched");
                    continue;
                }
                if (callAmount > Chips)
                {
                    Console.WriteLine("Not enough chips to call");
                    continue;
                }
                return $"call {callAmount}";
            }

            if (input.StartsWith("bet "))
            {
                if (currentBet > 0)
                {
                    Console.WriteLine("Cannot bet - there's already a bet. Use 'raise' instead.");
                    continue;
                }

                if (int.TryParse(input[4..], out int betAmount))
                {
                    if (betAmount < minRaise)
                    {
                        Console.WriteLine($"Bet amount must be at least {minRaise}");
                        continue;
                    }
                    if (betAmount > Chips)
                    {
                        Console.WriteLine("Not enough chips to bet");
                        continue;
                    }
                    return $"bet {betAmount}";
                }
            }

            if (input.StartsWith("raise "))
            {
                if (currentBet == 0)
                {
                    Console.WriteLine("Cannot raise - no current bet. Use 'bet' instead.");
                    continue;
                }

                if (int.TryParse(input[6..], out int raiseAmount))
                {
                    if (raiseAmount < minRaise)
                    {
                        Console.WriteLine($"Raise amount must be at least {minRaise}");
                        continue;
                    }
                    var totalToCall = currentBet - CurrentBet;
                    var totalAmount = totalToCall + raiseAmount;
                    if (totalAmount > Chips)
                    {
                        Console.WriteLine("Not enough chips to raise");
                        continue;
                    }
                    return $"raise {raiseAmount}";
                }
            }

            if (input == "allin")
            {
                if (currentBet == 0)
                {
                    return $"bet {Chips}";
                }
                else
                {
                    var totalToCall = currentBet - CurrentBet;
                    var raiseAmount = Chips - totalToCall;
                    return $"raise {raiseAmount}";
                }
            }

            Console.WriteLine("Invalid input. Please try again.");
        }
    }
}