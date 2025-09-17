namespace BootcampDay7.Demo;

public class DemoClassSM
{
    public static void DemonstrateStringManipulation()
    {
        Console.WriteLine("4. STRING MANIPULATION DEMONSTRATION");
        Console.WriteLine("====================================");

        // Remember: strings are immutable - each operation creates a new string
        string original = "Hello World";

        // Substring extraction
        string left5 = original.Substring(0, 5);
        string right5 = original.Substring(6);
        Console.WriteLine($"Original: '{original}'");
        Console.WriteLine($"Left 5 characters: '{left5}'");
        Console.WriteLine($"From index 6 to end: '{right5}'");

        // Insert and remove operations
        string inserted = original.Insert(5, ",");
        string removed = inserted.Remove(5, 1);
        Console.WriteLine($"After inserting comma: '{inserted}'");
        Console.WriteLine($"After removing comma: '{removed}'");

        // Padding - useful for formatting output
        string number = "123";
        Console.WriteLine($"Right-padded: '{number.PadRight(10, '*')}'");
        Console.WriteLine($"Left-padded: '{number.PadLeft(10, '0')}'");

        // Trimming whitespace - essential for user input processing
        string messy = "   Hello World   \t\r\n";
        Console.WriteLine($"Original length: {messy.Length}");
        Console.WriteLine($"Trimmed length: {messy.Trim().Length}");
        Console.WriteLine($"Trimmed result: '{messy.Trim()}'");

        // String replacement
        string sentence = "I like cats and cats like me";
        string replaced = sentence.Replace("cats", "dogs");
        Console.WriteLine($"Original: '{sentence}'");
        Console.WriteLine($"Replaced: '{replaced}'");

        Console.WriteLine();
    }
}