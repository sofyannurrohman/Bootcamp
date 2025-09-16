namespace Demos;

public class DemoClassRO
{
    public static void RelationalOperatorsDemo()
    {
        Console.WriteLine("7. RELATIONAL OPERATORS DEMONSTRATION");
        Console.WriteLine("=====================================");

        int? a = 10;
        int? b = 20;
        int? nullValue = null;

        Console.WriteLine($"a = {a}, b = {b}, nullValue = {nullValue}");

        // Normal comparisons
        Console.WriteLine($"a < b: {a < b}");    // True
        Console.WriteLine($"a > b: {a > b}");    // False
        Console.WriteLine($"a <= 10: {a <= 10}");  // True (comparing with literal)
        Console.WriteLine($"b >= a: {b >= a}");  // True

        // Comparisons involving null always return false
        Console.WriteLine($"a < nullValue: {a < nullValue}");     // False
        Console.WriteLine($"nullValue < a: {nullValue < a}");     // False
        Console.WriteLine($"nullValue > b: {nullValue > b}");     // False
        Console.WriteLine($"nullValue <= 50: {nullValue <= 50}");  // False (comparing null with literal)

        Console.WriteLine("\nKey insight: Any relational comparison with null returns false");
        Console.WriteLine("This is different from equality, where null == null is true");

        Console.WriteLine();
    }
}