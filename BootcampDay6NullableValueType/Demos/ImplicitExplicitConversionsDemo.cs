namespace Demos;

public class DemoClassIEC
{
    public static void ImplicitExplicitConversionsDemo()
    {
        Console.WriteLine("4. UNDERSTANDING NULLABLE CONVERSIONS");
        Console.WriteLine("=====================================");

        Console.WriteLine("One of the beautiful things about nullable types is how they handle conversions.");
        Console.WriteLine("Let's see the rules in action...\n");

        // RULE 1: T to T? is always implicit (safe)
        Console.WriteLine("RULE 1: Regular value type → Nullable type (IMPLICIT - always safe)");
        int regularInt = 25;
        int? nullableFromRegular = regularInt;  // No cast needed!

        Console.WriteLine($"Regular int: {regularInt}");
        Console.WriteLine($"Implicitly converted to nullable: {nullableFromRegular}");
        Console.WriteLine("Why is this safe? Because a regular value always has a value!\n");

        // RULE 2: T? to T requires explicit conversion (dangerous)
        Console.WriteLine("RULE 2: Nullable type → Regular value type (EXPLICIT - potentially dangerous)");
        int? nullableInt = 75;
        int backToRegular = (int)nullableInt;  // Explicit cast required

        Console.WriteLine($"Nullable int: {nullableInt}");
        Console.WriteLine($"Explicitly converted back to regular: {backToRegular}");
        Console.WriteLine("Why explicit? Because the nullable might be null!\n");

        // The dangerous scenario - null to regular type
        Console.WriteLine("The DANGEROUS scenario - converting null to regular type:");
        int? nullValue = null;
        try
        {
#pragma warning disable CS8629 // Nullable value type may be null
            int willThrow = (int)nullValue;  // This explodes!
#pragma warning restore CS8629
            Console.WriteLine($"This won't print: {willThrow}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"💥 Boom! {ex.Message}");
        }

        // SAFE ALTERNATIVES
        Console.WriteLine("\nSAFE alternatives for nullable → regular conversions:");

        // Option 1: Check HasValue first
        Console.WriteLine("Option 1: Check HasValue first");
        if (nullValue.HasValue)
        {
            int safeConversion = (int)nullValue;
            Console.WriteLine($"Safe conversion: {safeConversion}");
        }
        else
        {
            Console.WriteLine("Cannot convert - value is null, avoiding the explosion!");
        }

        // Option 2: Use GetValueOrDefault
        Console.WriteLine("\nOption 2: Use GetValueOrDefault");
        int defaultValue = nullValue.GetValueOrDefault(-1);
        Console.WriteLine($"Using GetValueOrDefault(-1): {defaultValue}");

        // Option 3: Use null-coalescing operator (we'll cover this more later)
        Console.WriteLine("\nOption 3: Use null-coalescing operator (??)");
        int coalescedValue = nullValue ?? -999;
        Console.WriteLine($"Using ?? operator: {coalescedValue}");

        Console.WriteLine();
    }
}