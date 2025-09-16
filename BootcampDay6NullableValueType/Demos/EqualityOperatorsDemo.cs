namespace Demos;

public class DemoClassEO
{
    public static void EqualityOperatorsDemo()
    {
        Console.WriteLine("6. EQUALITY OPERATORS DEMONSTRATION");
        Console.WriteLine("===================================");

        int? x = 5;
        int? y = 5;
        int? z = 10;
        int? nullValue1 = null;
        int? nullValue2 = null;

        Console.WriteLine($"x = {x}, y = {y}, z = {z}");
        Console.WriteLine($"nullValue1 = {nullValue1}, nullValue2 = {nullValue2}");

        // Equality with same values
        Console.WriteLine($"x == y: {x == y}");  // True
        Console.WriteLine($"x != z: {x != z}");  // True

        // Equality with null
        Console.WriteLine($"x == null: {x == null}");  // False
        Console.WriteLine($"nullValue1 == null: {nullValue1 == null}");  // True

        // Two nulls are equal
        Console.WriteLine($"nullValue1 == nullValue2: {nullValue1 == nullValue2}");  // True

        // Mixed comparisons
        Console.WriteLine($"x == nullValue1: {x == nullValue1}");  // False

        // Comparing with regular value types
        int regularInt = 5;
        Console.WriteLine($"x == regularInt: {x == regularInt}");  // True (implicit conversion)

        Console.WriteLine();
    }
}