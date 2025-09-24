using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number (n): ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Please enter a valid positive integer.");
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

        for (int x = 1; x <= n; x++)
        {
            var result = GetOutputAndCount(x, rules);
         
            if (result.matchedRules > 1)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (result.matchedRules == 1)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ResetColor();

            Console.Write(result.output);
            Console.ResetColor();

            if (x != n) Console.Write(", ");
        }

        Console.WriteLine();
    }

    static (string output, int matchedRules) GetOutputAndCount(int x, Dictionary<int, string> rules)
    {
        var sb = new StringBuilder();
        int count = 0;

        foreach (var rule in rules)
        {
            if (x % rule.Key == 0)
            {
                sb.Append(rule.Value);
                count++;
            }
        }

        return (sb.Length > 0 ? sb.ToString() : x.ToString(), count);
    }
}
