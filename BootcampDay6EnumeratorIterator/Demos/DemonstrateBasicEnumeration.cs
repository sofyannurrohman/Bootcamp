namespace Demos;

public class DemoClassDBE
{
    public static void DemonstrateBasicEnumeration()
    {
        Console.WriteLine("--- 1. Basic Enumeration with foreach ---");

        // The foreach statement is the high-level way to iterate
        // It works with any type that implements IEnumerable<T> or has GetEnumerator() method
        string word = "beer";
        Console.WriteLine($"Iterating through the string '{word}':");

        foreach (char c in word)
        {
            Console.WriteLine($"  Character: {c}");
        }
        Console.WriteLine();
    }
}