 
             namespace BootcampDay5.TryStatment;

public class TryClassTEX
{
    public static void ThrowingExceptionsDemo()
    {
        Console.WriteLine("7. THROWING EXCEPTIONS DEMONSTRATION");
        Console.WriteLine("====================================");
        Console.WriteLine("Your code can throw exceptions explicitly using the 'throw' keyword.");
        Console.WriteLine("This is useful for validating inputs and enforcing business rules.\n");

        Console.WriteLine("Testing custom exception throwing:");

        // Test with valid input
        try
        {
            DisplayName("John Doe");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  ✗ Caught: {ex.Message}");
        }

        // Test with null input
        try
        {
            DisplayName(null!); // null-forgiving operator for demo
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  ✓ Caught ArgumentNullException: {ex.ParamName} - {ex.Message}");
        }

        // Test with empty input
        try
        {
            DisplayName("");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  ✓ Caught ArgumentException: {ex.ParamName} - {ex.Message}");
        }

        Console.WriteLine();
    }

    static void DisplayName(string? name)
    {
        // Input validation with specific exceptions
        // ArgumentNullException for null values
        if (name == null)
            throw new ArgumentNullException(nameof(name), "Name cannot be null");

        // ArgumentException for invalid but non-null values
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty or whitespace", nameof(name));

        Console.WriteLine($"  ✓ Hello, {name}!");
    }
}