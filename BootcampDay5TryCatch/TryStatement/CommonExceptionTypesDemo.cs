namespace BootcampDay5.TryStatment;

public class TryClassCET
{

    public static void CommonExceptionTypesDemo()
    {
        Console.WriteLine("8B. COMMON EXCEPTION TYPES DEMONSTRATION");
        Console.WriteLine("========================================");
        Console.WriteLine("C# has many built-in exception types for different error scenarios.");
        Console.WriteLine("Understanding when to use each type is crucial for good error handling.\n");

        // ArgumentException family
        DemonstrateArgumentExceptions();

        // Operation state exceptions
        DemonstrateOperationExceptions();

        // System exceptions
        DemonstrateSystemExceptions();

        Console.WriteLine();
    }

    static void DemonstrateArgumentExceptions()
    {
        Console.WriteLine("ArgumentException Family:");
        Console.WriteLine("-------------------------");

        // ArgumentNullException
        try
        {
            ValidateUserInput(null, 25);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  ✓ ArgumentNullException: {ex.ParamName} - {ex.Message}");
        }

        // ArgumentException (general)
        try
        {
            ValidateUserInput("", 25);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  ✓ ArgumentException: {ex.ParamName} - {ex.Message}");
        }

        // ArgumentOutOfRangeException
        try
        {
            ValidateUserInput("John", -5);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"  ✓ ArgumentOutOfRangeException: {ex.ParamName} - {ex.Message}");
        }
    }

    static void ValidateUserInput(string? name, int age)
    {
        // These exceptions indicate programming errors in the calling code
        if (name == null)
            throw new ArgumentNullException(nameof(name), "Name parameter cannot be null");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty or whitespace", nameof(name));

        if (age < 0 || age > 150)
            throw new ArgumentOutOfRangeException(nameof(age), age, "Age must be between 0 and 150");

        Console.WriteLine($"  ✓ Valid input: {name}, age {age}");
    }

    static void DemonstrateOperationExceptions()
    {
        Console.WriteLine("\nOperation State Exceptions:");
        Console.WriteLine("---------------------------");

        var demoObject = new DisposableDemo();

        // InvalidOperationException
        try
        {
            demoObject.PerformOperation(false);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  ✓ InvalidOperationException: {ex.Message}");
        }

        // ObjectDisposedException
        demoObject.Dispose();
        try
        {
            demoObject.PerformOperation(true);
        }
        catch (ObjectDisposedException ex)
        {
            Console.WriteLine($"  ✓ ObjectDisposedException: {ex.Message}");
        }

        // NotSupportedException
        try
        {
            var list = new List<string> { "item1", "item2" };
            var readOnlyCollection = list.AsReadOnly();

            // Attempting to modify a read-only collection
            ((ICollection<string>)readOnlyCollection).Add("item3"); // This throws NotSupportedException
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"  ✓ NotSupportedException: {ex.Message}");
        }
    }

    static void DemonstrateSystemExceptions()
    {
        Console.WriteLine("\nSystem Exceptions:");
        Console.WriteLine("------------------");

        // NullReferenceException (usually indicates a programming bug)
        try
        {
            string? nullString = null;
            int length = nullString!.Length; // null-forgiving operator for demo purposes
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"  ✓ NullReferenceException: {ex.Message}");
            Console.WriteLine("    Note: This usually indicates a programming bug - always validate for null!");
        }

        // FormatException
        try
        {
            int number = int.Parse("not_a_number");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"  ✓ FormatException: {ex.Message}");
        }
    }

    // Helper class for demonstrating object state exceptions
    class DisposableDemo : IDisposable
    {
        private bool _isReady = false;
        private bool _disposed = false;

        public void PerformOperation(bool setReady)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DisposableDemo));

            if (setReady)
                _isReady = true;

            if (!_isReady)
                throw new InvalidOperationException("Object is not in a ready state for this operation");

            Console.WriteLine("  ✓ Operation completed successfully");
        }

        public void Dispose()
        {
            _disposed = true;
            Console.WriteLine("  ✓ Object disposed");
        }
    }
}