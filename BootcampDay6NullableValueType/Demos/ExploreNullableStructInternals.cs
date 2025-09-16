namespace Demos;

public class DemoClassENSI
{

    public static void ExploreNullableStructInternals()
    {
        Console.WriteLine("3. EXPLORING THE NULLABLE<T> STRUCT INTERNALS");
        Console.WriteLine("=============================================");

        // Let's peek under the hood - int? is really Nullable<int>
        Console.WriteLine("Under the hood, int? is actually System.Nullable<int>");

        // These two declarations are equivalent
        Nullable<int> explicitNullable = new Nullable<int>(100);
        int? shorthandNullable = 100;

        Console.WriteLine($"Explicit Nullable<int>: {explicitNullable}");
        Console.WriteLine($"Shorthand int?: {shorthandNullable}");
        Console.WriteLine($"Are they the same type? {explicitNullable.GetType() == shorthandNullable.GetType()}");

        // The struct has two key properties: HasValue and Value
        int? testValue = 50;

        Console.WriteLine($"\nExamining the internal structure:");
        Console.WriteLine($"testValue = {testValue}");
        Console.WriteLine($"testValue.HasValue = {testValue.HasValue}");

        if (testValue.HasValue)
        {
            Console.WriteLine($"testValue.Value = {testValue.Value}");
        }

        // Let's see what happens when we set it to null
        testValue = null;
        Console.WriteLine($"\nAfter setting to null:");
        Console.WriteLine($"testValue = {testValue}");
        Console.WriteLine($"testValue.HasValue = {testValue.HasValue}");

        // Here's the dangerous part - accessing Value when HasValue is false
        Console.WriteLine("\nDemonstrating the danger of accessing Value when null:");
        try
        {
#pragma warning disable CS8629 // Nullable value type may be null
            int dangerousAccess = testValue.Value; // This will throw InvalidOperationException!
#pragma warning restore CS8629
            Console.WriteLine($"This won't print: {dangerousAccess}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Exception caught: {ex.Message}");
        }

        // Safe alternatives for getting values
        Console.WriteLine("\nSafe ways to extract values:");
        Console.WriteLine($"GetValueOrDefault(): {testValue.GetValueOrDefault()}");
        Console.WriteLine($"GetValueOrDefault(999): {testValue.GetValueOrDefault(999)}");

        // Default value behavior
        int? defaultNullable = default;
        Console.WriteLine($"\nDefault value of int?: {defaultNullable}");
        Console.WriteLine($"Default HasValue: {defaultNullable.HasValue}");

        Console.WriteLine();
    }
}