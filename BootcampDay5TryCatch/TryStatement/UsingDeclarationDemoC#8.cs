namespace BootcampDay5.TryStatment;

public class TryClassUSD
{
    public static void UsingDeclarationDemo()
    {
        Console.WriteLine("5B. USING DECLARATIONS DEMONSTRATION (C# 8+)");
        Console.WriteLine("==============================================");
        Console.WriteLine("Using declarations provide a cleaner syntax for resource management.");
        Console.WriteLine("The resource is automatically disposed when execution leaves the scope.\n");

        Console.WriteLine("Demonstrating using declaration syntax:");
        DemonstrateUsingDeclaration();
        Console.WriteLine();
    }

    static void DemonstrateUsingDeclaration()
    {
        // Create a temporary file for demonstration
        string tempFile = "using_declaration_demo.txt";

        try
        {
            if (File.Exists(tempFile))
            {
                // Using declaration - resource disposed when leaving the 'if' block
                using var reader = File.OpenText(tempFile);
                Console.WriteLine("  ✓ File opened with using declaration");
                string? firstLine = reader.ReadLine();
                Console.WriteLine($"  ✓ Read line: {firstLine ?? "empty"}");
                // reader.Dispose() is automatically called here when leaving scope
            }
            else
            {
                // Create the file first for demonstration
                using var writer = new StreamWriter(tempFile);
                writer.WriteLine("Demo content for using declaration");
                Console.WriteLine("  ✓ File created with using declaration");
                // writer.Dispose() is automatically called here
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Error: {ex.Message}");
        }
        finally
        {
            // Clean up the demo file
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
                Console.WriteLine("  ✓ Demo file cleaned up");
            }
        }
    }
}