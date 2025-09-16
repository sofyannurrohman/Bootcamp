using BootcampDay6.Entities;
namespace BootcampDay6.OperatorOverloading;

public class OperatorOverloadingClassCO
{

    public static void CheckedOperatorsDemo()
    {
        Console.WriteLine("3.5. CHECKED OPERATORS (C# 11+)");
        Console.WriteLine("=================================");

        Console.WriteLine("Starting with SafeNumber demonstration:");

        SafeNumber safeNum1 = new SafeNumber(int.MaxValue - 1);
        SafeNumber safeNum2 = new SafeNumber(5);

        Console.WriteLine($"SafeNumber1: {safeNum1.Value}");
        Console.WriteLine($"SafeNumber2: {safeNum2.Value}");

        // Unchecked addition (default behavior)
        try
        {
            SafeNumber uncheckedResult = safeNum1 + safeNum2;
            Console.WriteLine($"Unchecked addition result: {uncheckedResult.Value}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Unchecked addition failed: {ex.Message}");
        }

        // Checked addition (explicit overflow checking)
        try
        {
            SafeNumber checkedResult = checked(safeNum1 + safeNum2);
            Console.WriteLine($"Checked addition result: {checkedResult.Value}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Checked addition detected overflow: {ex.Message}");
        }

        Console.WriteLine("\nNote: The checked operator provides overflow detection for safer arithmetic operations.");
        Console.WriteLine();
    }
}