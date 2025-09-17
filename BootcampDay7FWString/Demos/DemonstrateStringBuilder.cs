using System.Text;

namespace BootcampDay7.Demo;

public class DemoClassSBUILD
{
    public static void DemonstrateStringBuilder()
    {
        Console.WriteLine("8. STRINGBUILDER DEMONSTRATION");
        Console.WriteLine("==============================");

        // StringBuilder for efficient string building - mutable strings
        // Critical when you need to build strings in loops or with many operations
        Console.WriteLine("=== BASIC STRINGBUILDER OPERATIONS ===");

        StringBuilder sb = new StringBuilder();
        Console.WriteLine($"Initial capacity: {sb.Capacity}");
        Console.WriteLine($"Initial length: {sb.Length}");

        // Building strings efficiently
        for (int i = 0; i < 10; i++)
        {
            sb.Append($"Item {i}, ");
        }

        Console.WriteLine($"After appending 10 items:");
        Console.WriteLine($"Length: {sb.Length}, Capacity: {sb.Capacity}");
        Console.WriteLine($"Content: {sb.ToString()}");

        // StringBuilder with initial capacity - performance optimization
        Console.WriteLine("\n=== CAPACITY MANAGEMENT ===");
        StringBuilder sbWithCapacity = new StringBuilder(100); // Pre-allocate capacity
        Console.WriteLine($"StringBuilder with initial capacity 100: {sbWithCapacity.Capacity}");

        // Various StringBuilder methods
        Console.WriteLine("\n=== STRINGBUILDER METHODS ===");
        sb.Clear();
        sb.AppendLine("First line");
        sb.AppendLine("Second line");
        sb.Insert(0, "Header: ");
        sb.Replace("First", "Primary");
        sb.AppendFormat("Formatted number: {0:N2}", 12345.67);
        sb.AppendLine();

        Console.WriteLine("StringBuilder after various operations:");
        Console.WriteLine(sb.ToString());

        // Writable indexer - you can modify individual characters
        Console.WriteLine("\n=== MUTABLE CHARACTER ACCESS ===");
        StringBuilder demo = new StringBuilder("Hello World");
        Console.WriteLine($"Original: {demo}");
        demo[6] = 'w'; // Change 'W' to 'w'
        Console.WriteLine($"After changing index 6: {demo}");

        // Performance comparison demonstration
        Console.WriteLine("\n=== PERFORMANCE INSIGHTS ===");
        Console.WriteLine("StringBuilder vs String Concatenation:");
        Console.WriteLine("- String: Creates new object for each concatenation");
        Console.WriteLine("- StringBuilder: Modifies internal buffer, much more efficient");
        Console.WriteLine("- Use StringBuilder when building strings in loops");
        Console.WriteLine("- Use string concatenation for simple, one-time operations");

        // Method chaining with StringBuilder
        StringBuilder chained = new StringBuilder()
            .Append("Method ")
            .Append("chaining ")
            .Append("works ")
            .AppendLine("great!");
        Console.WriteLine($"Method chaining result: {chained}");

        Console.WriteLine();
    }
}