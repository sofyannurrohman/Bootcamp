namespace BootcampDay7.Demo;

public class DemoClassSS
{
    public static void DemonstrateStringSearching()
    {
        Console.WriteLine("3. STRING SEARCHING DEMONSTRATION");
        Console.WriteLine("=================================");

        string text = "The quick brown fox jumps over the lazy dog";

        // Basic search methods
        Console.WriteLine($"Text: '{text}'");
        Console.WriteLine($"Starts with 'The': {text.StartsWith("The")}");
        Console.WriteLine($"Ends with 'dog': {text.EndsWith("dog")}");
        Console.WriteLine($"Contains 'brown': {text.Contains("brown")}");

        // Finding positions - useful for text parsing
        Console.WriteLine($"Index of 'fox': {text.IndexOf("fox")}");
        Console.WriteLine($"Index of 'cat' (not found): {text.IndexOf("cat")}");
        Console.WriteLine($"Last index of 'the': {text.LastIndexOf("the")}");

        // Finding any of multiple characters - great for parsing delimited data
        string sample = "apple,banana;orange:grape";
        char[] delimiters = { ',', ';', ':' };
        int firstDelimiter = sample.IndexOfAny(delimiters);
        Console.WriteLine($"Sample: '{sample}'");
        Console.WriteLine($"First delimiter position: {firstDelimiter}");

        Console.WriteLine();
    }
}