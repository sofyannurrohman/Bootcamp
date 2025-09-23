using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number (n): ");
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Invalid number! Please enter a valid integer.");
            return;
        }

        var rules = new Dictionary<int, string>
        {
            {3, "foo"},
            {4, "baz"},
            {5, "bar"},
            {7, "jazz"},
            {9, "huzz"}
        };

        int columns = 6;
        int count = 0;

        for (int x = 1; x <= n; x++)
        {
            string output = GetOutput(x, rules);
            int matchedRules = rules.Count(r => x % r.Key == 0);
            if (matchedRules > 1)
                Console.ForegroundColor = ConsoleColor.Yellow; // multiple rules
            else if (matchedRules == 1)
                Console.ForegroundColor = ConsoleColor.Green;  // single rule
            else
                Console.ResetColor();                           // no rule
            Console.Write(output.PadRight(12));

            count++;
            if (count % columns == 0)
                Console.WriteLine();
            Console.ResetColor();
        }

        Console.WriteLine();
    }

    static string GetOutput(int x, Dictionary<int, string> rules)
    {
        var sb = new StringBuilder();
        foreach (var rule in rules)
        {
            if (x % rule.Key == 0)
                sb.Append(rule.Value);
        }
        return sb.Length > 0 ? sb.ToString() : x.ToString();
    }
}