namespace Demos;
public class DemoClassCWMV {
    
public static void CompareWithMagicValues()
    {
        Console.WriteLine("13. NULLABLE TYPES VS MAGIC VALUES COMPARISON");
        Console.WriteLine("==============================================");

        Console.WriteLine("Before nullable types, developers used 'magic values' to represent null:");
        Console.WriteLine("This approach had serious limitations...\n");

        // Example of the old "magic value" approach
        Console.WriteLine("OLD APPROACH - Magic Values:");
        Console.WriteLine("=============================");

        // String.IndexOf returns -1 if character not found (magic value approach)
        string text = "Hello World";
        int indexOfZ = text.IndexOf('z');  // Returns -1 (magic value for "not found")
        int indexOfH = text.IndexOf('H');  // Returns 0 (actual index)

        Console.WriteLine($"Looking for 'z' in '{text}': {indexOfZ}");
        Console.WriteLine($"Looking for 'H' in '{text}': {indexOfH}");

        // Problems with magic values:
        Console.WriteLine("\nProblems with magic values:");
        Console.WriteLine("1. Inconsistency - each type uses different 'null' representations");
        Console.WriteLine("2. Collision risk - magic value might be valid data");
        Console.WriteLine("3. Silent errors - forgetting to check leads to bugs");
        Console.WriteLine("4. Not type-safe - 'null' state not captured in type system");

        // Demonstrating inconsistency
        int notFoundIndex = -1;        // String operations use -1
        DateTime invalidDate = DateTime.MinValue;  // Date operations might use MinValue
        double invalidNumber = double.NaN;         // Math operations might use NaN

        Console.WriteLine($"\nInconsistent magic values:");
        Console.WriteLine($"String 'not found': {notFoundIndex}");
        Console.WriteLine($"Invalid date: {invalidDate}");
        Console.WriteLine($"Invalid number: {invalidNumber}");

        Console.WriteLine("\nNEW APPROACH - Nullable Types:");
        Console.WriteLine("===============================");

        // Modern nullable approach provides consistency
        int? findIndex = FindCharacterIndex(text, 'z');  // Returns null if not found
        int? findValidIndex = FindCharacterIndex(text, 'H');  // Returns actual index

        Console.WriteLine($"Looking for 'z': {findIndex?.ToString() ?? "Not found"}");
        Console.WriteLine($"Looking for 'H': {findValidIndex?.ToString() ?? "Not found"}");

        Console.WriteLine("\nBenefits of nullable types:");
        Console.WriteLine("1. Consistent pattern across all value types");
        Console.WriteLine("2. No collision with valid data");
        Console.WriteLine("3. Compile-time checking for null handling");
        Console.WriteLine("4. Type-safe - null state is part of the type system");
        Console.WriteLine("5. Clear intent - T? explicitly means 'might be null'");

        // Demonstrating type safety
        if (findIndex.HasValue)
        {
            Console.WriteLine($"Character found at index: {findIndex.Value}");
        }
        else
        {
            Console.WriteLine("Character not found - no ambiguity!");
        }

        Console.WriteLine();
    }

        // Helper method demonstrating the nullable approach
        static int? FindCharacterIndex(string text, char character)
        {
            int index = text.IndexOf(character);
            return index >= 0 ? index : null;  // Return null instead of -1
        }
}