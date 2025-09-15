namespace DelegateDemo;
public class DelegateClassGDD
{

    public delegate TResult Transformer<TArg, TResult>(TArg arg);

    public static void GenericDelegatesDemo()
    {
        Console.WriteLine("5. GENERIC DELEGATE TYPES - ULTIMATE REUSABILITY");
        Console.WriteLine("================================================");

        // Same delegate type, different type arguments
        Transformer<int, int> intSquarer = x => x * x;
        Transformer<string, int> stringLength = s => s.Length;
        Transformer<double, string> doubleFormatter = d => $"Value: {d:F2}";

        Console.WriteLine($"Int squarer (5): {intSquarer(5)}");
        Console.WriteLine($"String length ('Hello'): {stringLength("Hello")}");
        Console.WriteLine($"Double formatter (3.14159): {doubleFormatter(3.14159)}");

        // Using generic Transform method
        Console.WriteLine("\nGeneric Transform method demo:");
        int[] numbers = { 1, 2, 3, 4 };
        Console.WriteLine($"Original numbers: [{string.Join(", ", numbers)}]");

        TransformGeneric(numbers, x => x * x);  // Square each number
        Console.WriteLine($"Squared numbers: [{string.Join(", ", numbers)}]");

        string[] words = { "cat", "dog", "elephant" };
        Console.WriteLine($"Original words: [{string.Join(", ", words)}]");

        TransformGeneric(words, s => s.ToUpper());  // Uppercase each word
        Console.WriteLine($"Uppercase words: [{string.Join(", ", words)}]");

        Console.WriteLine();
    }

    // Truly generic transform method
    public static void TransformGeneric<T>(T[] values, Transformer<T, T> transformer)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = transformer(values[i]);
    }

}