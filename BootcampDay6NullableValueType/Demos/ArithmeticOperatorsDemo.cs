namespace Demos;
public class DemoClassAO {
    
public static void ArithmeticOperatorsDemo()
    {
        Console.WriteLine("8. ARITHMETIC OPERATORS DEMONSTRATION");
        Console.WriteLine("=====================================");

        int? a = 15;
        int? b = 5;
        int? nullValue = null;

        Console.WriteLine($"a = {a}, b = {b}, nullValue = {nullValue}");

        // Normal arithmetic operations
        Console.WriteLine($"a + b = {a + b}");    // 20
        Console.WriteLine($"a - b = {a - b}");    // 10
        Console.WriteLine($"a * b = {a * b}");    // 75
        Console.WriteLine($"a / b = {a / b}");    // 3

        // Arithmetic with null propagates null
        Console.WriteLine($"a + nullValue = {a + nullValue}");      // null
        Console.WriteLine($"nullValue - b = {nullValue - b}");      // null
        Console.WriteLine($"nullValue * a = {nullValue * a}");      // null
        Console.WriteLine($"nullValue / b = {nullValue / b}");      // null

        // Chained operations
        int? result = a + b * nullValue;
        Console.WriteLine($"a + b * nullValue = {result}");         // null

        Console.WriteLine("\nKey insight: Any arithmetic operation with null results in null");
        Console.WriteLine("This is similar to SQL's NULL behavior");

        Console.WriteLine();
    }
}