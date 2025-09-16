namespace Demos;
public class DemoClassMNNN {
    
public static void MixingNullableNonNullableDemo()
    {
        Console.WriteLine("9. MIXING NULLABLE AND NON-NULLABLE DEMONSTRATION");
        Console.WriteLine("=================================================");

        int? nullableInt = null;
        int regularInt = 10;
        double regularDouble = 3.14;

        Console.WriteLine($"nullableInt = {nullableInt}");
        Console.WriteLine($"regularInt = {regularInt}");
        Console.WriteLine($"regularDouble = {regularDouble}");

        // Mixing in arithmetic - regular types are implicitly converted to nullable
        int? result1 = nullableInt + regularInt;     // null + 10 = null
        int? result2 = regularInt + nullableInt;     // 10 + null = null

        Console.WriteLine($"nullableInt + regularInt = {result1}");
        Console.WriteLine($"regularInt + nullableInt = {result2}");

        // When both operands are non-null
        nullableInt = 5;
        int? result3 = nullableInt + regularInt;     // 5 + 10 = 15

        Console.WriteLine($"After setting nullableInt = 5:");
        Console.WriteLine($"nullableInt + regularInt = {result3}");

        // Mixed type operations
        double? mixedResult = nullableInt * regularDouble;  // 5 * 3.14 = 15.7
        Console.WriteLine($"nullableInt * regularDouble = {mixedResult}");

        // Assignment scenarios
        int? fromRegular = regularInt;       // Implicit conversion
                                             // int toRegular = nullableInt;      // This would require explicit cast

        if (nullableInt.HasValue)
        {
            int safeAssignment = (int)nullableInt;
            Console.WriteLine($"Safe assignment: {safeAssignment}");
        }

        Console.WriteLine();
    }
}