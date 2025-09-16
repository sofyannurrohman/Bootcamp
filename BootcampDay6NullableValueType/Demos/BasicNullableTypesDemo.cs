namespace BootcampDay6.Demos;
public class DemoClassBNT{
    
public static void BasicNullableTypesDemo()
    {
        Console.WriteLine("2. BASIC NULLABLE TYPES IN ACTION");
        Console.WriteLine("==================================");

        // Here's the magic - adding ? to any value type makes it nullable
        Console.WriteLine("The ? syntax is your gateway to nullable value types:");

        // Regular value types have default values, never null
        int regularInt = default;  // 0
        bool regularBool = default;  // false
        DateTime regularDate = default;  // 1/1/0001 12:00:00 AM

        Console.WriteLine($"Regular int default: {regularInt}");
        Console.WriteLine($"Regular bool default: {regularBool}");
        Console.WriteLine($"Regular DateTime default: {regularDate}");

        Console.WriteLine("\nNow watch what happens when we add the magic '?' symbol:");

        // Nullable value types CAN represent the absence of a value
        int? nullableInt = null;
        bool? nullableBool = null;
        DateTime? nullableDate = null;

        Console.WriteLine($"Nullable int: {nullableInt?.ToString() ?? "null"}");
        Console.WriteLine($"Nullable bool: {nullableBool?.ToString() ?? "null"}");
        Console.WriteLine($"Nullable DateTime: {nullableDate?.ToString() ?? "null"}");

        // Let's verify they're actually null
        Console.WriteLine($"\nVerifying null status:");
        Console.WriteLine($"nullableInt == null: {nullableInt == null}");
        Console.WriteLine($"nullableBool == null: {nullableBool == null}");
        Console.WriteLine($"nullableDate == null: {nullableDate == null}");

        // Now let's assign some actual values and see the difference
        nullableInt = 42;
        nullableBool = true;
        nullableDate = new DateTime(2024, 12, 25);

        Console.WriteLine($"\nAfter assigning real values:");
        Console.WriteLine($"Nullable int: {nullableInt}");
        Console.WriteLine($"Nullable bool: {nullableBool}");
        Console.WriteLine($"Nullable DateTime: {nullableDate}");

        // They're no longer null
        Console.WriteLine($"\nNull status after assignment:");
        Console.WriteLine($"nullableInt == null: {nullableInt == null}");
        Console.WriteLine($"nullableBool == null: {nullableBool == null}");
        Console.WriteLine($"nullableDate == null: {nullableDate == null}");

        Console.WriteLine();
    }
}