namespace BootcampDay7.Demo;

public class DemoClassDMC
{

    public static void DemonstrateMathClass()
    {
        Console.WriteLine("5. MATH CLASS - MATHEMATICAL OPERATIONS");
        Console.WriteLine("=======================================");

        // The Math class is your toolbox for mathematical calculations
        Console.WriteLine("Basic Math operations:");

        double[] values = { -10.7, -5.5, 0, 5.5, 10.7 };

        Console.WriteLine("Value     Abs       Floor     Ceiling   Round     Sign");
        Console.WriteLine("-----     ---       -----     -------   -----     ----");

        foreach (double value in values)
        {
            Console.WriteLine($"{value,5:F1}     {Math.Abs(value),3:F1}       {Math.Floor(value),5}     {Math.Ceiling(value),7}   {Math.Round(value),5}     {Math.Sign(value),4}");
        }

        // Min/Max operations
        Console.WriteLine("\nMin/Max operations:");
        int[] numbers = { 15, 8, 23, 4, 42, 16 };

        Console.WriteLine($"  Numbers: [{string.Join(", ", numbers)}]");
        Console.WriteLine($"  Min: {numbers.Min()}");
        Console.WriteLine($"  Max: {numbers.Max()}");
        Console.WriteLine($"  Math.Min(15, 8): {Math.Min(15, 8)}");
        Console.WriteLine($"  Math.Max(15, 8): {Math.Max(15, 8)}");

        // Power and root operations
        Console.WriteLine("\nPower and root operations:");

        double baseNumber = 2;
        double[] exponents = { 2, 3, 0.5, -1 };

        foreach (double exp in exponents)
        {
            double result = Math.Pow(baseNumber, exp);
            string description = exp switch
            {
                2 => "(squared)",
                3 => "(cubed)",
                0.5 => "(square root)",
                -1 => "(reciprocal)",
                _ => ""
            };
            Console.WriteLine($"  {baseNumber}^{exp} = {result:F3} {description}");
        }

        Console.WriteLine($"  Square root of 16: {Math.Sqrt(16)}");
        Console.WriteLine($"  Cube root of 27: {Math.Pow(27, 1.0 / 3):F3}");

        // Logarithmic operations
        Console.WriteLine("\nLogarithmic operations:");

        double[] logValues = { 1, 10, 100, Math.E };

        foreach (double val in logValues)
        {
            Console.WriteLine($"  ln({val:F3}) = {Math.Log(val):F3}");
            if (val > 0)
            {
                Console.WriteLine($"  log10({val:F3}) = {Math.Log10(val):F3}");
            }
        }

        // Trigonometric operations
        Console.WriteLine("\nTrigonometric operations (angles in radians):");

        double[] angles = { 0, Math.PI / 6, Math.PI / 4, Math.PI / 3, Math.PI / 2 };
        string[] angleNames = { "0°", "30°", "45°", "60°", "90°" };

        for (int i = 0; i < angles.Length; i++)
        {
            double angle = angles[i];
            Console.WriteLine($"  {angleNames[i],3}: sin={Math.Sin(angle):F3}, cos={Math.Cos(angle):F3}, tan={Math.Tan(angle):F3}");
        }

        // Practical example: Distance calculation
        Console.WriteLine("\nPractical example - Distance between two points:");

        (double x1, double y1) = (3, 4);
        (double x2, double y2) = (6, 8);

        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        Console.WriteLine($"  Point 1: ({x1}, {y1})");
        Console.WriteLine($"  Point 2: ({x2}, {y2})");
        Console.WriteLine($"  Distance: {distance:F2}");

        Console.WriteLine();
    }
}