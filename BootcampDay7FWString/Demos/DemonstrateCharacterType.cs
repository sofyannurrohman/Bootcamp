namespace BootcampDay7.Demo;
public class DemoClassCT{
    
public static void DemonstrateCharacterType()
    {
        Console.WriteLine("1. CHARACTER TYPE DEMONSTRATION");
        Console.WriteLine("================================");

        // Basic character creation - char is System.Char, represents 16-bit Unicode
        char letter = 'A';
        char newLine = '\n';
        char tab = '\t';
        char unicodeChar = '\u0041'; // Unicode escape sequence for 'A'

        Console.WriteLine($"Basic character: {letter}");
        Console.WriteLine($"Unicode character \\u0041: {unicodeChar}");
        Console.WriteLine($"Special characters exist: newline={newLine}, tab={tab}");

        // Character manipulation methods
        Console.WriteLine($"Uppercase 'c': {char.ToUpper('c')}");
        Console.WriteLine($"Lowercase 'C': {char.ToLower('C')}");
        Console.WriteLine($"Is tab whitespace? {char.IsWhiteSpace(tab)}");

        // Culture-invariant methods - critical for international applications
        // Turkish example: 'i' -> 'İ' in Turkish culture vs 'I' in invariant
        Console.WriteLine($"Culture-invariant uppercase 'i': {char.ToUpperInvariant('i')}");
        Console.WriteLine($"Regular uppercase 'i': {char.ToUpper('i')}");

        // Character categorization - very useful for data validation
        Console.WriteLine($"Is 'A' a letter? {char.IsLetter('A')}");
        Console.WriteLine($"Is '5' a digit? {char.IsDigit('5')}");
        Console.WriteLine($"Is '!' punctuation? {char.IsPunctuation('!')}");
        Console.WriteLine($"Is ' ' whitespace? {char.IsWhiteSpace(' ')}");

        // Unicode categorization for advanced text processing
        char testChar = 'A';
        Console.WriteLine($"Unicode category of '{testChar}': {char.GetUnicodeCategory(testChar)}");

        Console.WriteLine();
    }
}