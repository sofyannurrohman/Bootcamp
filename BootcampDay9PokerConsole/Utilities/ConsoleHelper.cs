using PokerConsoleApp.Core;
namespace PokerConsoleApp.Utilities;

public static class ConsoleHelper
{
    public static void WriteColored(string text, ConsoleColor color)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = originalColor;
    }

    public static void WriteLineColored(string text, ConsoleColor color)
    {
        WriteColored(text, color);
        Console.WriteLine();
    }

    public static void DisplayCard(Card card)
    {
        var color = card.Suit.ToString() switch
        {
            "Hearts" or "Diamonds" => ConsoleColor.Red,
            _ => ConsoleColor.White
        };

        WriteColored(card.ToString(), color);
    }

    public static void DisplayHand(Hand hand)
    {
        foreach (var card in hand.Cards)
        {
            DisplayCard(card);
            Console.Write(" ");
        }
    }
}