namespace BootcampDay7.Demo;

public class DemoClassSC
{
    public static void DemonstrateStringComparison()
    {
        Console.WriteLine("7. STRING COMPARISON DEMONSTRATION");
        Console.WriteLine("==================================");

        string str1 = "Hello";
        string str2 = "hello";
        string str3 = "Hello";

        // Default equality comparison - ordinal, case-sensitive
        Console.WriteLine("=== EQUALITY COMPARISON ===");
        Console.WriteLine($"'{str1}' == '{str3}': {str1 == str3}");
        Console.WriteLine($"'{str1}' == '{str2}': {str1 == str2}");
        Console.WriteLine($"'{str1}'.Equals('{str2}'): {str1.Equals(str2)}");

        // StringComparison enum - gives you full control over comparison behavior
        Console.WriteLine("\n=== STRING COMPARISON OPTIONS ===");
        Console.WriteLine($"Ordinal (default): {string.Equals(str1, str2, StringComparison.Ordinal)}");
        Console.WriteLine($"OrdinalIgnoreCase: {string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase)}");
        Console.WriteLine($"CurrentCulture: {string.Equals(str1, str2, StringComparison.CurrentCulture)}");
        Console.WriteLine($"CurrentCultureIgnoreCase: {string.Equals(str1, str2, StringComparison.CurrentCultureIgnoreCase)}");
        Console.WriteLine($"InvariantCulture: {string.Equals(str1, str2, StringComparison.InvariantCulture)}");
        Console.WriteLine($"InvariantCultureIgnoreCase: {string.Equals(str1, str2, StringComparison.InvariantCultureIgnoreCase)}");

        // Order comparison - for sorting and alphabetical ordering
        Console.WriteLine("\n=== ORDER COMPARISON ===");
        string[] words = { "apple", "Banana", "cherry", "Date" };
        Console.WriteLine("Original order: " + string.Join(", ", words));

        // Default culture-sensitive comparison
        Array.Sort(words, string.Compare);
        Console.WriteLine("Culture sort: " + string.Join(", ", words));

        // Reset array
        words = new[] { "apple", "Banana", "cherry", "Date" };

        // Ordinal comparison - treats characters as their numeric Unicode values
        Array.Sort(words, StringComparer.Ordinal);
        Console.WriteLine("Ordinal sort: " + string.Join(", ", words));

        // Case-insensitive ordinal comparison
        Array.Sort(words, StringComparer.OrdinalIgnoreCase);
        Console.WriteLine("Ordinal ignore case: " + string.Join(", ", words));

        // CompareTo examples - returns negative, zero, or positive
        Console.WriteLine("\n=== COMPARETO EXAMPLES ===");
        Console.WriteLine($"'Boston'.CompareTo('Austin'): {string.Compare("Boston", "Austin")}");
        Console.WriteLine($"'Boston'.CompareTo('Boston'): {string.Compare("Boston", "Boston")}");
        Console.WriteLine($"'Boston'.CompareTo('Chicago'): {string.Compare("Boston", "Chicago")}");

        // Ordinal vs Culture demonstration
        Console.WriteLine("\n=== ORDINAL VS CULTURE COMPARISON ===");
        string a = "Atom";
        string b = "atom";
        Console.WriteLine($"Ordinal: '{a}' vs '{b}' = {string.Compare(a, b, StringComparison.Ordinal)}");
        Console.WriteLine($"Culture: '{a}' vs '{b}' = {string.Compare(a, b, StringComparison.CurrentCulture)}");
        Console.WriteLine("Note: Ordinal treats 'A' (65) and 'a' (97) by Unicode values");
        Console.WriteLine("Culture comparison considers language rules for proper alphabetical ordering");

        Console.WriteLine();
    }
}