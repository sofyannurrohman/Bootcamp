namespace BootcampDay7.Demo;

public class DemoClassSB
{
public static void DemonstrateStringBasics()
        {
            Console.WriteLine("2. STRING BASICS DEMONSTRATION");
            Console.WriteLine("==============================");

            // Different ways to create strings - understanding construction options
            string literal = "Hello World";
            string multiline = "First Line\r\nSecond Line";
            string verbatim = @"C:\Path\File.txt";          // Verbatim string literal - no escape sequences
            string repeated = new string('*', 10);          // Repeat character constructor
            char[] charArray = { 'H', 'e', 'l', 'l', 'o' };
            string fromArray = new string(charArray);       // From char array constructor
            string fromSubset = new string(charArray, 1, 3); // From char array subset (start, count)

            Console.WriteLine($"Literal string: {literal}");
            Console.WriteLine($"Multiline string:\n{multiline}");
            Console.WriteLine($"Verbatim string: {verbatim}");
            Console.WriteLine($"Repeated character: {repeated}");
            Console.WriteLine($"From char array: {fromArray}");
            Console.WriteLine($"From char subset: {fromSubset}");

            // Null and empty string handling - critical for robust applications
            string empty = "";
            string alsoEmpty = string.Empty;
            string? nullString = null; // Explicitly nullable for modern C# nullable reference types

            Console.WriteLine($"Empty string == \"\": {empty == ""}");
            Console.WriteLine($"Empty string == string.Empty: {empty == string.Empty}");
            Console.WriteLine($"Empty string length: {empty.Length}");

            // Safe null checking - prevents NullReferenceException
            Console.WriteLine($"Is null string null? {nullString == null}");
            Console.WriteLine($"Is null string empty? {string.IsNullOrEmpty(nullString)}");
            Console.WriteLine($"Is empty string null or empty? {string.IsNullOrEmpty(empty)}");

            // Accessing characters within strings
            string sample = "Programming";
            Console.WriteLine($"Character at index 0: {sample[0]}");
            Console.WriteLine($"Character at index 4: {sample[4]}");

            // Iterating through string characters
            Console.Write("Characters in '123': ");
            foreach (char c in "123")
            {
                Console.Write($"{c},");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
}