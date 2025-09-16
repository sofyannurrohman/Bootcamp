using Entities;

namespace BootcampDay5.TryStatment;

public class TryClassRWS
{
    public static void RealWorldScenarioDemo()
    {
        Console.WriteLine("11. REAL WORLD SCENARIO - FILE PROCESSING SYSTEM");
        Console.WriteLine("=================================================");
        Console.WriteLine("This demonstrates comprehensive exception handling in a realistic scenario.");
        Console.WriteLine("It shows proper resource management, logging, and graceful error handling.\n");

        var processor = new FileProcessor();

        // Test different scenarios to show various exception handling patterns
        Console.WriteLine("Processing various file scenarios:\n");

        processor.ProcessFile("valid_data.txt");
        processor.ProcessFile(""); // Empty path
        processor.ProcessFile("nonexistent.txt"); // File not found
        processor.ProcessFile("corrupted.txt"); // Simulate corruption

        // Demonstrate the TryProcess alternative approach
        Console.WriteLine("\nDemonstrating TryProcess alternative:");
        if (processor.TryProcessFile("test.txt", out string? errorMessage))
        {
            Console.WriteLine("  ✓ File processed successfully using TryProcess");
        }
        else
        {
            Console.WriteLine($"  ✗ TryProcess failed: {errorMessage}");
        }

        Console.WriteLine();
    }
}