namespace BootcampDay7.Demo;

public class DemoClassNC
{

    public static void DemonstrateNumericConversions()
    {
        Console.WriteLine("1. NUMERIC CONVERSIONS - SAFE AND UNSAFE");
        Console.WriteLine("=========================================");

        // Basic parsing operations - the foundation of handling user input
        Console.WriteLine("Basic string to number parsing:");

        string[] numberStrings = { "42", "3.14159", "-123", "0", "999999999" };

        foreach (string numberStr in numberStrings)
        {
            Console.WriteLine($"\nParsing \"{numberStr}\":");

            // Safe parsing with TryParse - always prefer this for user input
            if (int.TryParse(numberStr, out int intResult))
            {
                Console.WriteLine($"  ✓ As int: {intResult}");
            }
            else
            {
                Console.WriteLine($"  ✗ Cannot parse as int");
            }

            if (double.TryParse(numberStr, out double doubleResult))
            {
                Console.WriteLine($"  ✓ As double: {doubleResult}");
            }
            else
            {
                Console.WriteLine($"  ✗ Cannot parse as double");
            }

            if (decimal.TryParse(numberStr, out decimal decimalResult))
            {
                Console.WriteLine($"  ✓ As decimal: {decimalResult}");
            }
        }

        // Demonstrating the difference between Parse and TryParse
        Console.WriteLine("\nDifference between Parse() and TryParse():");

        string invalidNumber = "not_a_number";

        // TryParse approach - safe, no exceptions
        if (int.TryParse(invalidNumber, out int safeResult))
        {
            Console.WriteLine($"  TryParse succeeded: {safeResult}");
        }
        else
        {
            Console.WriteLine($"  TryParse failed gracefully for \"{invalidNumber}\"");
        }

        // Parse approach - can throw exceptions
        try
        {
            int unsafeResult = int.Parse(invalidNumber);
            Console.WriteLine($"  Parse succeeded: {unsafeResult}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"  Parse threw exception: {ex.Message}");
        }

        Console.WriteLine();
    }
}