namespace BootcampDay5.TryStatment;

public class TryClassANTIN
{

    public static void ArgumentNullThrowIfNullDemo()
    {
        Console.WriteLine("9B. ARGUMENTNULLEXCEPTION.THROWIFNULL (.NET 6+)");
        Console.WriteLine("================================================");
        Console.WriteLine("The ThrowIfNull method provides a concise way to validate null arguments.");
        Console.WriteLine("It's cleaner than manually writing if-checks and throw statements.\n");

        Console.WriteLine("Comparing old vs new null validation approaches:");

        // Test with valid input
        try
        {
            ProcessUserDataOldWay("Valid User");
            ProcessUserDataNewWay("Valid User");
            Console.WriteLine("  ✓ Both methods succeeded with valid input");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Unexpected error: {ex.Message}");
        }

        Console.WriteLine();

        // Test with null input
        try
        {
            ProcessUserDataOldWay(null);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  ✓ Old way caught: {ex.ParamName} - {ex.Message}");
        }

        try
        {
            ProcessUserDataNewWay(null);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  ✓ New way caught: {ex.ParamName} - {ex.Message}");
        }

        Console.WriteLine();
    }

    // Traditional approach (pre-.NET 6)
    static void ProcessUserDataOldWay(string? userData)
    {
        // Manual null check with explicit throw
        if (userData == null)
            throw new ArgumentNullException(nameof(userData), "User data cannot be null");

        Console.WriteLine($"  Processing (old way): {userData}");
    }

    // Modern approach (.NET 6+)
    static void ProcessUserDataNewWay(string? userData)
    {
        // Concise null check - throws ArgumentNullException if null
        ArgumentNullException.ThrowIfNull(userData);

        Console.WriteLine($"  Processing (new way): {userData}");
    }


}