 namespace BootcampDay5.TryStatment;

public class TryClassUST
{
    public static void UsingStatementDemo()
    {
        Console.WriteLine("5. USING STATEMENT DEMONSTRATION");
        Console.WriteLine("=================================");

        Console.WriteLine("Comparing manual resource management vs using statement:");

        // Manual way (what we did before using statements)
        Console.WriteLine("\nManual resource management:");
        ReadFileManually();

        // Using statement way (cleaner and safer)
        Console.WriteLine("\nUsing statement approach:");
        ReadFileWithUsing();

        Console.WriteLine();
    }

    static void ReadFileManually()
    {
        StreamWriter? writer = null;
        try
        {
            writer = new StreamWriter("manual_test.txt");
            writer.WriteLine("This file was created manually");
            Console.WriteLine("  File written successfully (manual way)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error writing file: {ex.Message}");
        }
        finally
        {
            // We must remember to dispose manually
            if (writer != null)
            {
                writer.Dispose();
                Console.WriteLine("  Resource disposed manually in finally block");
            }
        }
    }

    static void ReadFileWithUsing()
    {
        try
        {
            // Using statement automatically calls Dispose() when exiting the block
            using (StreamWriter writer = new StreamWriter("using_test.txt"))
            {
                writer.WriteLine("This file was created with using statement");
                Console.WriteLine("  File written successfully (using statement)");
                // No need for manual cleanup - Dispose() is called automatically
            }
            Console.WriteLine("  Resource automatically disposed by using statement");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error writing file: {ex.Message}");
        }
    }
}