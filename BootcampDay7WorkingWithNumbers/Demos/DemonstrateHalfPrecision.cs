namespace BootcampDay7.Demo;

public class DemoClassHP
{

    public static void DemonstrateHalfPrecision()
    {
        Console.WriteLine("7. HALF PRECISION - 16-BIT FLOATING POINT (.NET 5+)");
        Console.WriteLine("====================================================");

        // Half is a 16-bit floating-point type introduced in .NET 5
        // Primarily used for GPU interoperability and memory-constrained scenarios
        // Remember: Half has NO arithmetic operators - you must cast to float/double first!

        Console.WriteLine("Understanding Half precision floating-point:");
        Console.WriteLine("- 16-bit floating-point type (vs 32-bit float, 64-bit double)");
        Console.WriteLine("- Range: approximately -65,500 to +65,500");
        Console.WriteLine("- Precision: about 3-4 decimal digits");
        Console.WriteLine("- Primary use: GPU computing, memory optimization");
        Console.WriteLine("- Important: NO built-in arithmetic operators!");

        // Creating Half values
        Console.WriteLine("\nCreating Half values:");

        Half h1 = (Half)123.456f;
        Half h2 = (Half)(-789.123);
        Half h3 = (Half)Math.PI;
        Half h4 = (Half)0.0001f;  // Very small number
        Half h5 = (Half)100000f;  // Large number (will lose precision)

        Console.WriteLine($"  From 123.456f: {h1} (notice precision loss)");
        Console.WriteLine($"  From -789.123: {h2}");
        Console.WriteLine($"  From π: {h3}");
        Console.WriteLine($"  From 0.0001f: {h4} (very small number)");
        Console.WriteLine($"  From 100000f: {h5} (large number behavior)");

        // Demonstrating precision limitations
        Console.WriteLine("\nPrecision comparison:");
        float[] testValues = { 1.234567f, 12.34567f, 123.4567f, 1234.567f };

        Console.WriteLine("Original     Half         Float        Double");
        Console.WriteLine("--------     ----         -----        ------");

        foreach (float original in testValues)
        {
            Half asHalf = (Half)original;
            float backToFloat = (float)asHalf;
            double asDouble = (double)original;

            Console.WriteLine($"{original,-12:F6} {asHalf,-12} {backToFloat,-12:F6} {asDouble,-12:F6}");
        }

        // Range limitations
        Console.WriteLine("\nRange limitations:");

        float[] extremeValues = { -70000f, -65504f, 65504f, 70000f };

        foreach (float extreme in extremeValues)
        {
            try
            {
                Half extremeHalf = (Half)extreme;
                Console.WriteLine($"  {extreme,8:F0} -> {extremeHalf} (valid: {!Half.IsInfinity(extremeHalf)})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  {extreme,8:F0} -> Error: {ex.Message}");
            }
        }

        // Special values
        Console.WriteLine("\nSpecial Half values:");

        Half positiveInfinity = Half.PositiveInfinity;
        Half negativeInfinity = Half.NegativeInfinity;
        Half notANumber = Half.NaN;
        Half zero = Half.Zero;
        Half epsilon = Half.Epsilon;

        Console.WriteLine($"  Positive Infinity: {positiveInfinity}");
        Console.WriteLine($"  Negative Infinity: {negativeInfinity}");
        Console.WriteLine($"  NaN: {notANumber}");
        Console.WriteLine($"  Zero: {zero}");
        Console.WriteLine($"  Epsilon (smallest): {epsilon}");

        // The critical point: NO arithmetic operators!
        Console.WriteLine("\nArithmetic operations - MUST convert first:");

        Half a = (Half)10.5f;
        Half b = (Half)2.25f;

        // This would NOT compile:
        // Half sum = a + b;  // ERROR!

        // Correct approach:
        float aFloat = (float)a;
        float bFloat = (float)b;
        float sumFloat = aFloat + bFloat;
        Half sum = (Half)sumFloat;

        Console.WriteLine($"  a = {a}");
        Console.WriteLine($"  b = {b}");
        Console.WriteLine($"  a + b = {aFloat} + {bFloat} = {sumFloat} -> {sum} (as Half)");

        // More complex operations
        float product = aFloat * bFloat;
        float quotient = aFloat / bFloat;

        Console.WriteLine($"  a * b = {product} -> {(Half)product}");
        Console.WriteLine($"  a / b = {quotient:F2} -> {(Half)quotient}");

        // Practical use case: Memory-efficient arrays
        Console.WriteLine("\nPractical example - Memory-efficient storage:");

        // Create arrays of different types
        int arraySize = 1000;
        float[] floatArray = new float[arraySize];
        double[] doubleArray = new double[arraySize];
        Half[] halfArray = new Half[arraySize];

        // Fill with sample data
        Random rand = new Random(42);
        for (int i = 0; i < arraySize; i++)
        {
            float value = (float)(rand.NextDouble() * 1000);
            floatArray[i] = value;
            doubleArray[i] = value;
            halfArray[i] = (Half)value;
        }

        // Memory usage comparison
        int floatMemory = arraySize * sizeof(float);
        int doubleMemory = arraySize * sizeof(double);
        int halfMemory = arraySize * 2; // Half is 2 bytes

        Console.WriteLine($"  Array of {arraySize} numbers:");
        Console.WriteLine($"    float[]:  {floatMemory:N0} bytes ({floatMemory / 1024.0:F1} KB)");
        Console.WriteLine($"    double[]: {doubleMemory:N0} bytes ({doubleMemory / 1024.0:F1} KB)");
        Console.WriteLine($"    Half[]:   {halfMemory:N0} bytes ({halfMemory / 1024.0:F1} KB)");
        Console.WriteLine($"    Memory saved vs float: {((float)(floatMemory - halfMemory) / floatMemory * 100):F1}%");
        Console.WriteLine($"    Memory saved vs double: {((float)(doubleMemory - halfMemory) / doubleMemory * 100):F1}%");

        // Accuracy comparison
        Console.WriteLine("\nAccuracy comparison on sample data:");

        float originalValue = 123.456789f;
        Half halfValue = (Half)originalValue;
        float recovered = (float)halfValue;

        Console.WriteLine($"  Original: {originalValue:F6}");
        Console.WriteLine($"  Via Half: {recovered:F6}");
        Console.WriteLine($"  Error: {Math.Abs(originalValue - recovered):E2}");

        // When to use Half
        Console.WriteLine("\nWhen to use Half:");
        Console.WriteLine("  ✓ GPU computing (graphics cards often use 16-bit floats)");
        Console.WriteLine("  ✓ Machine learning (neural networks with reduced precision)");
        Console.WriteLine("  ✓ Memory-constrained applications");
        Console.WriteLine("  ✓ Data transmission where bandwidth is limited");
        Console.WriteLine("  ✗ Precise mathematical calculations");
        Console.WriteLine("  ✗ Financial applications");
        Console.WriteLine("  ✗ When you need arithmetic operators directly");

        Console.WriteLine();
    }
}