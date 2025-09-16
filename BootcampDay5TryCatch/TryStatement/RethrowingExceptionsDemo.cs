namespace BootcampDay5.TryStatment;

public class TryClassRE
{
    public static void RethrowingExceptionsDemo()
    {
        Console.WriteLine("8. RETHROWING EXCEPTIONS DEMONSTRATION");
        Console.WriteLine("======================================");
        Console.WriteLine("You can catch an exception, do some processing (like logging), and rethrow it.");
        Console.WriteLine("Use 'throw;' to preserve the original stack trace, or 'throw new Exception()' to wrap.\n");

        Console.WriteLine("Testing exception rethrowing and wrapping:");

        try
        {
            ProcessDataWithLogging();
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine($"  ✓ Final catch - InvalidDataException: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"  ✓ Inner exception preserved: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
            }
        }

        Console.WriteLine("\nDemonstrating simple rethrow (preserving stack trace):");
        try
        {
            MethodThatRethrows();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  ✓ Caught rethrown exception: {ex.Message}");
            Console.WriteLine($"    Exception was logged and rethrown from intermediate method");
        }

        Console.WriteLine();
    }

    static void ProcessDataWithLogging()
    {
        try
        {
            ParseCriticalData("invalid_data");
        }
        catch (FormatException ex)
        {
            // Log the error for debugging (in real app, this would go to a log file)
            Console.WriteLine("  → Logging original error for debugging purposes...");
            Console.WriteLine($"    Original error: {ex.GetType().Name} - {ex.Message}");

            // Wrap the original exception in a domain-specific exception
            // The original exception becomes the InnerException
            throw new InvalidDataException("Failed to process critical business data", ex);
        }
    }

    static void ParseCriticalData(string data)
    {
        // Simulate parsing that can fail
        if (data == "invalid_data")
        {
            throw new FormatException("Data format is not recognized by the parser");
        }

        Console.WriteLine($"  ✓ Successfully parsed: {data}");
    }

    static void MethodThatRethrows()
    {
        try
        {
            // Simulate an operation that fails
            throw new InvalidOperationException("Original operation failed");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("  → Logging in intermediate method...");
            // Use 'throw;' to rethrow the same exception with preserved stack trace
            // Never use 'throw ex;' as it resets the stack trace
            throw;
            throw;
        }
    }

    // Custom exception for business logic
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
        public InvalidDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}