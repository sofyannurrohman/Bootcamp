namespace Demos;

public class DemoClassBU
{
    public static void BoxingUnboxingDemo()
    {
        Console.WriteLine("4. BOXING AND UNBOXING DEMONSTRATION");
        Console.WriteLine("====================================");

        // Boxing nullable types - the boxed object contains the underlying value, not the nullable wrapper
        int? nullableValue = 123;
        object boxedValue = nullableValue;  // Boxing

        Console.WriteLine($"Original nullable: {nullableValue}");
        Console.WriteLine($"Boxed object: {boxedValue}");
        Console.WriteLine($"Boxed type: {boxedValue.GetType().Name}");

        // Boxing a null nullable type results in null reference
        int? nullNullable = null;
        object? boxedNull = nullNullable;

        Console.WriteLine($"Null nullable: {nullNullable}");
        Console.WriteLine($"Boxed null: {boxedNull}");
        Console.WriteLine($"Boxed null == null: {boxedNull == null}");

        // Unboxing back to nullable
        int? unboxedValue = (int?)boxedValue;
        Console.WriteLine($"Unboxed back to nullable: {unboxedValue}");

        // Safe unboxing using 'as' operator
        object stringObject = "Not a number";
        int? safeUnbox = stringObject as int?;

        Console.WriteLine($"Safe unboxing from string: {safeUnbox}");
        Console.WriteLine($"Safe unboxing HasValue: {safeUnbox.HasValue}");

        // Direct unboxing to regular value type
        object anotherBoxed = 456;
        int directUnbox = (int)anotherBoxed;
        Console.WriteLine($"Direct unboxing: {directUnbox}");

        Console.WriteLine();
    }
}