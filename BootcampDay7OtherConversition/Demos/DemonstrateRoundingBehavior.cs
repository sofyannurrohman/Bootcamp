namespace BootcampDay7.Demo;

public class DemoClassRB
{

    public static void DemonstrateRoundingBehavior()
    {
        Console.WriteLine("2. ROUNDING CONVERSIONS - BANKER'S ROUNDING EXPLAINED");
        Console.WriteLine("======================================================");

        // This is where Convert really shines - it uses banker's rounding
        // Standard casting truncates, Convert rounds intelligently
        // Banker's rounding reduces systematic bias in large datasets

        Console.WriteLine("Understanding the difference between casting and Convert:");
        Console.WriteLine("Casting truncates (always rounds toward zero)");
        Console.WriteLine("Convert uses banker's rounding (rounds to nearest even for .5 values)");
        Console.WriteLine();

        double[] criticalValues = { 2.1, 2.5, 2.9, 3.5, 4.5, 5.5, 6.5, 7.5 };

        Console.WriteLine("Value    Cast    Convert   Explanation");
        Console.WriteLine("-----    ----    -------   -----------");

        foreach (double value in criticalValues)
        {
            int castResult = (int)value;           // Truncates
            int convertResult = Convert.ToInt32(value); // Banker's rounding

            string explanation = value % 1 == 0.5 ?
                $"Midpoint -> rounds to nearest even ({convertResult})" :
                "Standard rounding";

            Console.WriteLine($"{value,-7}  {castResult,-6}  {convertResult,-7}   {explanation}");
        }

        // Real-world impact: Financial calculations
        Console.WriteLine("\nWhy banker's rounding matters in financial systems:");
        Console.WriteLine("It prevents systematic bias that could accumulate over many transactions");

        double[] transactions = { 10.125, 15.625, 23.375, 8.875, 12.125, 19.375 };
        double castTotal = 0;
        double convertTotal = 0;

        Console.WriteLine("\nTransaction processing comparison:");
        Console.WriteLine("Amount    Cast(¢)  Convert(¢)  Difference");
        Console.WriteLine("------    -------  ----------  ----------");

        foreach (double transaction in transactions)
        {
            // Convert to cents for processing
            int castCents = (int)(transaction * 100);
            int convertCents = Convert.ToInt32(transaction * 100);

            castTotal += castCents;
            convertTotal += convertCents;

            Console.WriteLine($"${transaction,-8:F3}  {castCents,-7}  {convertCents,-10}  {convertCents - castCents}");
        }

        Console.WriteLine($"\nTotal difference: {convertTotal - castTotal} cents");
        Console.WriteLine("Over thousands of transactions, this adds up!");

        // Demonstrating Math.Round for custom rounding control
        Console.WriteLine("\nCustom rounding with Math.Round:");
        double testValue = 3.5;

        Console.WriteLine($"Value: {testValue}");
        Console.WriteLine($"  Convert.ToInt32(): {Convert.ToInt32(testValue)} (banker's rounding)");
        Console.WriteLine($"  Math.Round(ToEven): {Math.Round(testValue, MidpointRounding.ToEven)}");
        Console.WriteLine($"  Math.Round(AwayFromZero): {Math.Round(testValue, MidpointRounding.AwayFromZero)}");
        Console.WriteLine($"  Math.Round(ToZero): {Math.Round(testValue, MidpointRounding.ToZero)}");
        Console.WriteLine($"  Math.Round(ToPositiveInfinity): {Math.Round(testValue, MidpointRounding.ToPositiveInfinity)}");

        Console.WriteLine();
    }
}