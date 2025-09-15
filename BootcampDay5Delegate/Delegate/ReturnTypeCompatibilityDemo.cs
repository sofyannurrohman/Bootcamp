namespace DelegateDemo;
public class DelegateClassRTCD
{

    public static void ReturnTypeCompatibilityDemo()
    {
        Console.WriteLine("10. RETURN TYPE COMPATIBILITY - COVARIANCE");
        Console.WriteLine("==========================================");

        // Method that returns a more specific type
        string GetSpecificString() => "Hello from specific string method";

        // Delegate that expects a more general return type
        Func<object> objectGetter;

        // This works! String is more specific than object (covariance)
        // The string return value gets implicitly upcast to object
        objectGetter = GetSpecificString;

        Console.WriteLine("Calling Func<object> delegate that points to method returning string:");
        object result = objectGetter();
        Console.WriteLine($"  Returned: {result} (actual type: {result.GetType().Name})");

        // Real-world example: factory patterns
        Console.WriteLine("\nReal-world example - factory pattern:");

        // Factory that returns specific types
        string CreateString() => "Factory created string";
        object CreateInt() => 42;  // Return object instead of int

        // General factory delegate
        Func<object> factory;

        // Can point to specific factories due to covariance
        factory = CreateString;
        Console.WriteLine($"  String factory result: {factory()}");

        factory = CreateInt;
        Console.WriteLine($"  Int factory result: {factory()}");

        Console.WriteLine();
    }
}