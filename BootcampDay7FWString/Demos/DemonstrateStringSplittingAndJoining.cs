namespace BootcampDay7.Demo;

public class DemoClassSSJ
{
    public static void DemonstrateStringSplittingAndJoining()
    {
        Console.WriteLine("5. STRING SPLITTING AND JOINING DEMONSTRATION");
        Console.WriteLine("=============================================");

        // Splitting strings - fundamental for data processing
        string sentence = "The quick brown fox jumps";
        string[] words = sentence.Split();

        Console.WriteLine($"Original sentence: '{sentence}'");
        Console.Write("Words: ");
        foreach (string word in words)
        {
            Console.Write($"'{word}' ");
        }
        Console.WriteLine();

        // Splitting with custom delimiters
        string csvData = "apple,banana,cherry,date";
        string[] fruits = csvData.Split(',');
        Console.WriteLine($"CSV data: '{csvData}'");
        Console.WriteLine($"Number of fruits: {fruits.Length}");

        // Joining strings back together
        string rejoined = string.Join(" ", words);
        string csvRejoined = string.Join(" | ", fruits);

        Console.WriteLine($"Rejoined with spaces: '{rejoined}'");
        Console.WriteLine($"Fruits joined with pipes: '{csvRejoined}'");

        Console.WriteLine();
    }
}