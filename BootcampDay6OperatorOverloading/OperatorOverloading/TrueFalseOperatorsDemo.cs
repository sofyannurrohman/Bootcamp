using BootcampDay6.Entities;
namespace BootcampDay6.OperatorOverloading;
public class OperatorOverloadingClassTFO{
    
public static void TrueFalseOperatorsDemo()
    {
        Console.WriteLine("11. TRUE/FALSE OPERATORS DEMONSTRATION");
        Console.WriteLine("======================================");

        Console.WriteLine("SqlBoolean demonstrates three-valued logic (True, False, Null)");
        Console.WriteLine("This is useful for database operations where NULL values are common.");

        SqlBoolean trueValue = SqlBoolean.True;
        SqlBoolean falseValue = SqlBoolean.False;
        SqlBoolean nullValue = SqlBoolean.Null;

        Console.WriteLine($"\nBasic values:");
        Console.WriteLine($"True: {trueValue}");
        Console.WriteLine($"False: {falseValue}");
        Console.WriteLine($"Null: {nullValue}");

        // Test in conditional statements
        Console.WriteLine($"\nConditional statement tests:");

        Console.WriteLine("Testing 'if (trueValue)':");
        if (trueValue)
            Console.WriteLine("  Entered true branch");
        else
            Console.WriteLine("  Entered false branch");

        Console.WriteLine("Testing 'if (falseValue)':");
        if (falseValue)
            Console.WriteLine("  Entered true branch");
        else
            Console.WriteLine("  Entered false branch");

        Console.WriteLine("Testing 'if (nullValue)':");
        if (nullValue)
            Console.WriteLine("  Entered true branch");
        else
            Console.WriteLine("  Entered false branch");

        // Test logical operations with bitwise operators
        Console.WriteLine($"\nLogical operations:");
        Console.WriteLine($"trueValue & falseValue: {trueValue & falseValue}");
        Console.WriteLine($"trueValue & nullValue: {trueValue & nullValue}");
        Console.WriteLine($"falseValue & nullValue: {falseValue & nullValue}");

        Console.WriteLine($"trueValue | falseValue: {trueValue | falseValue}");
        Console.WriteLine($"trueValue | nullValue: {trueValue | nullValue}");
        Console.WriteLine($"falseValue | nullValue: {falseValue | nullValue}");

        // Test negation
        Console.WriteLine($"\nNegation operations:");
        Console.WriteLine($"!trueValue: {!trueValue}");
        Console.WriteLine($"!falseValue: {!falseValue}");
        Console.WriteLine($"!nullValue: {!nullValue}");

        Console.WriteLine();
    }
}