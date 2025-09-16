namespace Demos;
public class DemoClassNCO {
    
public static void NullCoalescingOperatorDemo()
    {
        Console.WriteLine("11. NULL-COALESCING OPERATOR (??) DEMONSTRATION");
        Console.WriteLine("===============================================");

        int? nullableValue = null;
        int? anotherValue = 42;

        // Basic null-coalescing
        int result1 = nullableValue ?? 100;
        int result2 = anotherValue ?? 100;

        Console.WriteLine($"nullableValue = {nullableValue}");
        Console.WriteLine($"anotherValue = {anotherValue}");
        Console.WriteLine($"nullableValue ?? 100 = {result1}");
        Console.WriteLine($"anotherValue ?? 100 = {result2}");

        // Chaining null-coalescing operators
        int? first = null;
        int? second = null;
        int? third = 999;
        int? fourth = 888;

        int chainedResult = first ?? second ?? third ?? fourth ?? 0;
        Console.WriteLine($"\nChaining example:");
        Console.WriteLine($"first = {first}, second = {second}, third = {third}, fourth = {fourth}");
        Console.WriteLine($"first ?? second ?? third ?? fourth ?? 0 = {chainedResult}");

        // Null-coalescing with different types
        string? nullString = null;
        string defaultString = nullString ?? "Default Value";
        Console.WriteLine($"\nWith strings:");
        Console.WriteLine($"nullString ?? \"Default Value\" = \"{defaultString}\"");

        // Practical examples
        Console.WriteLine("\nPractical examples:");

        // Configuration values
        int? configTimeout = GetConfigTimeout(); // Might return null
        int actualTimeout = configTimeout ?? 30; // Default to 30 seconds
        Console.WriteLine($"Configuration timeout: {configTimeout}");
        Console.WriteLine($"Actual timeout used: {actualTimeout} seconds");

        // User input validation
        int? userAge = GetUserAge(); // Might be null if invalid
        string ageDisplay = $"Age: {userAge?.ToString() ?? "Not specified"}";
        Console.WriteLine($"User age display: {ageDisplay}");

        // Null-coalescing assignment (C# 8.0+)
        int? score = null;
        score ??= 0; // Assign 0 if score is null
        Console.WriteLine($"Score after ??= operator: {score}");

        Console.WriteLine();
    }

        // Helper methods for practical examples
        static int? GetConfigTimeout()
        {
            // Simulate reading from config file that might not have this value
            return null;
        }

        static int? GetUserAge()
        {
            // Simulate user input that might be invalid
            return null;
        }

}