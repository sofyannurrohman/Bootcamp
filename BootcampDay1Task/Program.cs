Console.Write("Enter a number (n): ");
string? input = Console.ReadLine();
if (!int.TryParse(input, out int n))
        {
            Console.WriteLine("Invalid number! Please enter a valid integer.");
            return;
        }
    // Rules dictionary: divisor -> word
    var rules = new Dictionary<int, string>
        {
            { 3, "foo" },
            { 5, "bar" },
            { 7, "jazz" }
    }; // Scalable just define the divisor rather than adding else if

    // Generalized FooBarJazz implementation
    for (int x = 1; x <= n; x++)
        {
            string output = "";

            foreach (var rule in rules)
            {
                if (x % rule.Key == 0)
                    output += rule.Value;
            }

            if (string.IsNullOrEmpty(output))
                output = x.ToString();

            Console.Write(output);

            if (x < n)
                Console.Write(", ");
        }

Console.WriteLine();
