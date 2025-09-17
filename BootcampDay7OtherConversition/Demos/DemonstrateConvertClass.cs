namespace BootcampDay7.Demo;

public class DemoClassCC
{
    public static void DemonstrateConvertClass()
    {
        Console.WriteLine("1. CONVERT CLASS - THE SWISS ARMY KNIFE OF CONVERSIONS");
        Console.WriteLine("=======================================================");

        // Convert class handles conversions between .NET's "base types"
        // bool, char, string, DateTime, DateTimeOffset, and all numeric types
        // It's more robust than simple casting and handles edge cases gracefully

        Console.WriteLine("Basic Convert class capabilities:");

        // Converting between common types - more forgiving than Parse methods
        string numberString = "42";
        string doubleString = "3.14159";
        string boolString = "true";

        int convertedInt = Convert.ToInt32(numberString);
        double convertedDouble = Convert.ToDouble(doubleString);
        bool convertedBool = Convert.ToBoolean(boolString);

        Console.WriteLine($"  String \"{numberString}\" -> int {convertedInt}");
        Console.WriteLine($"  String \"{doubleString}\" -> double {convertedDouble}");
        Console.WriteLine($"  String \"{boolString}\" -> bool {convertedBool}");

        // Converting between numeric types with automatic handling
        decimal money = 123.456m;
        float precision = 789.012f;
        long reasonableNumber = 9876543; // Changed from too large number

        Console.WriteLine("\nNumeric type conversions:");
        Console.WriteLine($"  decimal {money} -> int {Convert.ToInt32(money)}");
        Console.WriteLine($"  float {precision} -> int {Convert.ToInt32(precision)}");
        Console.WriteLine($"  long {reasonableNumber} -> int {Convert.ToInt32(reasonableNumber)}");

        // The Convert class gracefully handles null values - a huge advantage
        Console.WriteLine("\nNull handling (Convert's superpower):");
        string? nullString = null;
        object? nullObject = null;

        // Convert returns default values instead of throwing exceptions
        int defaultInt = Convert.ToInt32(nullString); // Returns 0
        bool defaultBool = Convert.ToBoolean(nullObject); // Returns false
        double defaultDouble = Convert.ToDouble(nullString); // Returns 0.0

        Console.WriteLine($"  null string -> int: {defaultInt}");
        Console.WriteLine($"  null object -> bool: {defaultBool}");
        Console.WriteLine($"  null string -> double: {defaultDouble}");

        // Demonstrating Convert's advantage over direct parsing
        Console.WriteLine("\nWhy Convert is often better than Parse:");

        try
        {
            // This would throw an exception
            // int.Parse(null); 
            Console.WriteLine("  int.Parse(null) would throw NullReferenceException");

            // This gracefully returns 0
            int safeResult = Convert.ToInt32(null);
            Console.WriteLine($"  Convert.ToInt32(null) safely returns: {safeResult}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error: {ex.Message}");
        }

        Console.WriteLine();
    }
}