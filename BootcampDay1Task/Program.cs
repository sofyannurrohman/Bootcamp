// See https://aka.ms/new-console-template for more information
//Capture User Input
Console.Write("Enter a number (n): ");
int n = int.Parse(Console.ReadLine());
// Rules dictionary: divisor -> word
    var rules = new Dictionary<int, string>
        {
            { 3, "foo" },
            { 5, "bar" },
            { 7, "jazz" }
    }; // Scalable just  define the divisor rather than adding else if

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

// Write Output
Console.WriteLine();
