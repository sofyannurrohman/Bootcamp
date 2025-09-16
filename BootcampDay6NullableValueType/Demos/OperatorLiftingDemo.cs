namespace Demos;

public class DemoClassOL
{
    public static void OperatorLiftingDemo()
    {
        Console.WriteLine("6. OPERATOR LIFTING - THE MAGIC BEHIND THE SCENES");
        Console.WriteLine("==================================================");

        Console.WriteLine("Here's something cool: you can use regular operators (+, -, <, >, ==) with nullable types!");
        Console.WriteLine("This works because of 'operator lifting' - the compiler does magic for us.\n");

        int? a = 10;
        int? b = 20;
        int? c = null;

        Console.WriteLine($"Our test values: a = {a}, b = {b}, c = {c}");

        Console.WriteLine("\nArithmetic operations with valid values work as expected:");
        Console.WriteLine($"a + b = {a + b}");     // Lifted addition
        Console.WriteLine($"b - a = {b - a}");     // Lifted subtraction  
        Console.WriteLine($"a * 2 = {a * 2}");     // Mixed nullable/regular

        Console.WriteLine("\nBut here's where it gets interesting - operations with null:");
        Console.WriteLine($"a + c = {a + c}");     // 10 + null = null
        Console.WriteLine($"c * b = {c * b}");     // null * 20 = null
        Console.WriteLine($"c / 5 = {c / 5}");     // null / 5 = null

        Console.WriteLine("\nThe rule: If ANY operand is null, arithmetic result is null");
        Console.WriteLine("(This is similar to SQL's NULL behavior)\n");

        Console.WriteLine("Comparison operations have their own rules:");
        Console.WriteLine($"a < b = {a < b}");     // 10 < 20 = true
        Console.WriteLine($"a > b = {a > b}");     // 10 > 20 = false

        Console.WriteLine("\nBut comparisons with null are always false:");
        Console.WriteLine($"a < c = {a < c}");     // 10 < null = false
        Console.WriteLine($"c > b = {c > b}");     // null > 20 = false
        Console.WriteLine($"c < 5 = {c < 5}");     // null < 5 = false

        Console.WriteLine("\nKey insight: null is incomparable - neither greater, less, nor equal to anything");
        Console.WriteLine("(except for equality, where null == null is true)");

        Console.WriteLine();
    }
}