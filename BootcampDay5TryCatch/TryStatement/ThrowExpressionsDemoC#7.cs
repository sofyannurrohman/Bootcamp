namespace BootcampDay5.TryStatment;

public class TryClassTE
{
    public static void ThrowExpressionsDemo()
    {
        Console.WriteLine("7B. THROW EXPRESSIONS DEMONSTRATION (C# 7+)");
        Console.WriteLine("============================================");
        Console.WriteLine("C# 7+ allows 'throw' to be used as an expression, not just a statement.");
        Console.WriteLine("This enables throwing exceptions in expression contexts like ternary operators.\n");

        Console.WriteLine("Testing throw expressions in different contexts:");

        // Test expression-bodied method that throws
        try
        {
            string result = GetNotImplementedFeature();
            Console.WriteLine(result);
        }
        catch (NotImplementedException ex)
        {
            Console.WriteLine($"  ✓ Caught from expression-bodied method: {ex.Message}");
        }

        // Test throw in ternary conditional
        try
        {
            string result = ProperCase(null);
            Console.WriteLine(result);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  ✓ Caught from ternary expression: {ex.Message}");
        }

        // Test with valid input
        try
        {
            string result = ProperCase("hello world");
            Console.WriteLine($"  ✓ ProperCase result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Unexpected error: {ex.Message}");
        }

        Console.WriteLine();
    }

    // Expression-bodied member that throws (C# 7+ throw expression)
    static string GetNotImplementedFeature() =>
        throw new NotImplementedException("This feature is planned for version 2.0");

    // Method using throw expression in ternary conditional
    static string ProperCase(string? value) =>
        value == null ? throw new ArgumentException("Value cannot be null") :
        value == "" ? "" :
        char.ToUpper(value[0]) + value.Substring(1).ToLower();
}