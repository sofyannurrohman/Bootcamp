namespace BootcampDay7.Demo;

public class DemoClassLLC
{

    public static void DemonstrateLosslessConversions()
    {
        Console.WriteLine("3. LOSSLESS CONVERSIONS - IMPLICIT VS EXPLICIT");
        Console.WriteLine("==============================================");

        // Understanding when data loss can occur is crucial for reliable applications
        Console.WriteLine("Implicit conversions (no data loss):");

        // These conversions are safe - smaller to larger type
        byte byteValue = 100;
        short shortValue = byteValue;      // byte -> short (safe)
        int intValue = shortValue;         // short -> int (safe)
        long longValue = intValue;         // int -> long (safe)
        float floatValue = longValue;      // long -> float (safe for most values)
        double doubleValue = floatValue;   // float -> double (safe)

        Console.WriteLine($"  byte {byteValue} -> short {shortValue} -> int {intValue}");
        Console.WriteLine($"  -> long {longValue} -> float {floatValue} -> double {doubleValue}");

        // Explicit conversions (potential data loss)
        Console.WriteLine("\nExplicit conversions (potential data loss):");

        double largeDouble = 123.456789;
        float fromDouble = (float)largeDouble;      // May lose precision
        int fromFloat = (int)fromDouble;            // Loses decimal part
        short fromInt = (short)fromFloat;           // May lose data if too large
        byte fromShort = (byte)fromInt;             // May lose data if too large

        Console.WriteLine($"  double {largeDouble} -> float {fromDouble}");
        Console.WriteLine($"  -> int {fromInt} -> short {fromShort} -> byte {fromShort}");

        // Demonstrating precision loss
        Console.WriteLine("\nPrecision loss examples:");

        double preciseValue = 1.23456789012345;
        float lessePrecise = (float)preciseValue;

        Console.WriteLine($"  Original double: {preciseValue:F14}");
        Console.WriteLine($"  As float: {lessePrecise:F14}");
        Console.WriteLine($"  Precision lost: {Math.Abs(preciseValue - lessePrecise):E3}");

        // Overflow examples
        Console.WriteLine("\nOverflow examples:");

        try
        {
            int maxInt = int.MaxValue;
            Console.WriteLine($"  int.MaxValue: {maxInt:N0}");

            // This will overflow silently unless checked
            checked
            {
                int overflow = maxInt + 1;
                Console.WriteLine($"  MaxValue + 1: {overflow}");
            }
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"  Overflow detected: {ex.Message}");
        }

        Console.WriteLine();
    }
}