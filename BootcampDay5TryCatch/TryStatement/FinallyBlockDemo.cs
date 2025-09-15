namespace BootcampDay5.TryStatment;

public class TryClassFB
{

    public static void FinallyBlockDemo()
    {
        Console.WriteLine("4. FINALLY BLOCK DEMONSTRATION");
        Console.WriteLine("==============================");

        Console.WriteLine("Testing finally block execution:");

        // Test scenario 1: No exception
        Console.WriteLine("Scenario 1: Normal execution");
        TestFinallyBlock(false);

        // Test scenario 2: With exception
        Console.WriteLine("\nScenario 2: With exception");
        TestFinallyBlock(true);

        Console.WriteLine();
    }

    static void TestFinallyBlock(bool throwException)
    {
        string? resource = null;

        try
        {
            Console.WriteLine("  Acquiring resource...");
            resource = "Important Resource";

            if (throwException)
            {
                throw new InvalidOperationException("Simulated error");
            }

            Console.WriteLine("  Using resource successfully");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  Caught exception: {ex.Message}");
        }
        finally
        {
            // This ALWAYS runs, regardless of exceptions
            if (resource != null)
            {
                Console.WriteLine("  Finally block: Cleaning up resource");
                resource = null; // Simulate cleanup
            }
            else
            {
                Console.WriteLine("  Finally block: No resource to clean up");
            }
        }

        Console.WriteLine("  Method completed");
    }
}