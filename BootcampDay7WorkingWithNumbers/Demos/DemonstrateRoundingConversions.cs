namespace BootcampDay7.Demo;

public class DemoClassDRC
{

    public static void DemonstrateRoundingConversions()
    {
        Console.WriteLine("4. ROUNDING CONVERSIONS - REAL TO INTEGRAL");
        Console.WriteLine("==========================================");

        // Different rounding strategies can significantly impact calculations
        double[] testValues = { 2.3, 2.5, 2.7, 3.5, 4.5, -2.5, -3.5 };

        Console.WriteLine("Comparing different rounding methods:");
        Console.WriteLine("Value     Truncate  Convert   Round     Floor     Ceiling");
        Console.WriteLine("-----     --------  -------   -----     -----     -------");

        foreach (double value in testValues)
        {
            int truncated = (int)value;                                    // Simple cast truncates
            int converted = Convert.ToInt32(value);                        // Banker's rounding
            int rounded = (int)Math.Round(value, MidpointRounding.AwayFromZero); // Round away from zero
            int floored = (int)Math.Floor(value);                          // Always round down
            int ceiled = (int)Math.Ceiling(value);                         // Always round up

            Console.WriteLine($"{value,5:F1}     {truncated,8}  {converted,7}   {rounded,5}     {floored,5}     {ceiled,7}");
        }

        // Banker's rounding explanation
        Console.WriteLine("\nBanker's rounding (used by Convert.ToInt32):");
        Console.WriteLine("  - Rounds .5 to the nearest even number");
        Console.WriteLine("  - Reduces bias in large datasets");
        Console.WriteLine("  - 2.5 -> 2, 3.5 -> 4, 4.5 -> 4, 5.5 -> 6");

        // Practical example: Currency calculations
        Console.WriteLine("\nPractical example - Currency rounding:");

        double[] prices = { 19.995, 29.985, 15.125, 8.875 };

        Console.WriteLine("Original   Truncate   Banker's   Away from Zero");
        Console.WriteLine("--------   --------   --------   --------------");

        foreach (double price in prices)
        {
            double truncated = Math.Truncate(price * 100) / 100;
            double bankers = Math.Round(price, 2, MidpointRounding.ToEven);
            double awayFromZero = Math.Round(price, 2, MidpointRounding.AwayFromZero);

            Console.WriteLine($"${price:F3}     ${truncated:F2}      ${bankers:F2}       ${awayFromZero:F2}");
        }

        Console.WriteLine();
    }
}