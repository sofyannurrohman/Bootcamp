  namespace BootcampDay5.TryStatment;

public class TryClassTPP
{
    public static void TryParsePatternDemo()
    {
        Console.WriteLine("9. TRY-PARSE PATTERN DEMONSTRATION");
        Console.WriteLine("===================================");
        Console.WriteLine("The TryXXX pattern provides an alternative to exceptions for expected failures.");
        Console.WriteLine("It returns a boolean for success/failure and uses 'out' parameters for results.");
        Console.WriteLine("This is more efficient than try-catch for scenarios where failure is common.\n");

        Console.WriteLine("Comparing exception-based vs TryParse approaches:");

        string[] testInputs = { "123", "abc", "999999999999", "45.67", "" };

        foreach (string input in testInputs)
        {
            Console.WriteLine($"\nTesting input: '{input}'");

            // Exception-based approach - expensive when failures are common
            Console.WriteLine("  Exception-based approach:");
            try
            {
                int result = int.Parse(input);
                Console.WriteLine($"    ✓ Success: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    ✗ Failed: {ex.GetType().Name}");
            }

            // TryParse approach - efficient, no exceptions thrown
            Console.WriteLine("  TryParse approach:");
            if (int.TryParse(input, out int tryResult))
            {
                Console.WriteLine($"    ✓ Success: {tryResult}");
            }
            else
            {
                Console.WriteLine("    ✗ Failed: Invalid format or overflow");
            }
        }

        // Demonstrate custom TryXXX method
        Console.WriteLine("\nCustom TryDivide method demonstration:");
        TestCustomTryMethod();

        Console.WriteLine();
    }

    static void TestCustomTryMethod()
    {
        int[][] testCases = {
                new int[] { 10, 2 },
                new int[] { 15, 3 },
                new int[] { 7, 0 },   // Division by zero case
                new int[] { -20, 4 }
            };

        foreach (var testCase in testCases)
        {
            int numerator = testCase[0];
            int denominator = testCase[1];

            Console.WriteLine($"  Testing {numerator} ÷ {denominator}:");

            // Using our custom TryDivide method
            if (TryDivide(numerator, denominator, out int result))
            {
                Console.WriteLine($"    ✓ Success: {result}");
            }
            else
            {
                Console.WriteLine("    ✗ Failed: Division by zero not allowed");
            }
        }
    }

    // Custom TryXXX method implementation
    // Returns true for success, false for failure
    // Result is provided via 'out' parameter
    static bool TryDivide(int numerator, int denominator, out int result)
    {
        if (denominator == 0)
        {
            result = 0; // Set a default value
            return false; // Indicate failure
        }

        result = numerator / denominator;
        return true; // Indicate success
    }
}