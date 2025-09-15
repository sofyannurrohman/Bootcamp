namespace DelegateDemo;

public class DelegateClassPMD
{
    delegate int Transformer(int x);
    // Static methods that match our delegate signature
    static int Square(int x) => x * x;
    static int Cube(int x) => x * x * x;

    public static void PluginMethodsDemo()
    {
        Console.WriteLine("2. WRITING PLUGIN METHODS WITH DELEGATES");
        Console.WriteLine("========================================");

        // This demonstrates the power of delegates for creating pluggable behavior
        int[] values = { 5, 6, 7, 8, 9 };

        Console.WriteLine($"Original values: [{string.Join(", ", values)}]");

        // Transform array using Square as the plugin
        Transform(values, Square);
        Console.WriteLine($"After Square transform: [{string.Join(", ", values)}]");

        // Reset values
        values = new int[] { 5, 6, 7, 8, 9 };

        // Same Transform method, different behavior by passing different delegate
        Transform(values, Cube);
        Console.WriteLine($"After Cube transform: [{string.Join(", ", values)}]");

        // You can even pass lambda expressions as plugins
        values = new int[] { 5, 6, 7, 8, 9 };
        Transform(values, x => x + 10);  // Add 10 to each element
        Console.WriteLine($"After +10 transform: [{string.Join(", ", values)}]");

        Console.WriteLine();
    }

    // This is a higher-order function - it takes a function as a parameter
    static void Transform(int[] values, Transformer t)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = t(values[i]);  // Apply the plugged-in transformation
    }

}