namespace BootcampDay5.TryStatment;
public class TryClassMCB
{

    public static void MultipleCatchBlocksDemo()
    {
        Console.WriteLine("2. MULTIPLE CATCH BLOCKS DEMONSTRATION");
        Console.WriteLine("======================================");

        // Simulate command line arguments for testing
        string[] testArgs = { "300" }; // Try: "300", "abc", "", or null

        Console.WriteLine($"Testing with argument: '{testArgs[0]}'");

        try
        {
            byte b = byte.Parse(testArgs[0]);
            Console.WriteLine($"Successfully parsed: {b}");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Error: Please provide at least one argument");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: That's not a valid number!");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: The number is too large to fit in a byte (max: 255)!");
        }
        catch (Exception ex) // General catch-all (should be last)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        // Test different scenarios
        Console.WriteLine("\nTesting different error scenarios:");
        TestParsingScenarios();
        Console.WriteLine();
    }

    static void TestParsingScenarios()
    {
        string[] testCases = { "100", "abc", "500", "" };

        foreach (string testCase in testCases)
        {
            Console.WriteLine($"  Testing '{testCase}':");
            try
            {
                byte result = byte.Parse(testCase);
                Console.WriteLine($"    Success: {result}");
            }
            catch (FormatException)
            {
                Console.WriteLine("    Error: Invalid format");
            }
            catch (OverflowException)
            {
                Console.WriteLine("    Error: Number too large for byte");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("    Error: Empty string");
            }
        }
    }
}