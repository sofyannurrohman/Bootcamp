using System.Numerics;

namespace BootcampDay7.Demo;

public class DemoClassDBI
{

    public static void DemonstrateBigInteger()
    {
        Console.WriteLine("6. BIGINTEGER - ARBITRARILY LARGE NUMBERS");
        Console.WriteLine("==========================================");

        // BigInteger handles numbers larger than long.MaxValue
        Console.WriteLine("Working with very large numbers:");

        // Creating BigInteger values
        BigInteger small = new BigInteger(123456789);
        BigInteger fromString = BigInteger.Parse("987654321012345678901234567890");
        BigInteger googol = BigInteger.Pow(10, 100); // 10^100

        Console.WriteLine($"  Small BigInteger: {small:N0}");
        Console.WriteLine($"  From string: {fromString}");
        Console.WriteLine($"  Googol (10^100): {googol}");

        // Arithmetic operations with BigInteger
        Console.WriteLine("\nBigInteger arithmetic:");

        BigInteger a = BigInteger.Parse("12345678901234567890");
        BigInteger b = BigInteger.Parse("98765432109876543210");

        Console.WriteLine($"  a = {a}");
        Console.WriteLine($"  b = {b}");
        Console.WriteLine($"  a + b = {a + b}");
        Console.WriteLine($"  a * b = {a * b}");
        Console.WriteLine($"  a^2 = {BigInteger.Pow(a, 2)}");

        // Factorials - impossible with regular integers for large values
        Console.WriteLine("\nFactorial calculations:");

        for (int n = 10; n <= 50; n += 10)
        {
            BigInteger factorial = CalculateFactorial(n);
            Console.WriteLine($"  {n}! = {factorial}");
            Console.WriteLine($"       ({factorial.ToString().Length} digits)");
        }

        // Fibonacci sequence with large numbers
        Console.WriteLine("\nLarge Fibonacci numbers:");

        BigInteger fib1 = 0, fib2 = 1;
        Console.WriteLine($"  F(0) = {fib1}");
        Console.WriteLine($"  F(1) = {fib2}");

        for (int i = 2; i <= 100; i += 10)
        {
            for (int j = (i == 2 ? 2 : i - 8); j <= i; j++)
            {
                BigInteger fibNext = fib1 + fib2;
                fib1 = fib2;
                fib2 = fibNext;
            }
            Console.WriteLine($"  F({i}) = {fib2}");
        }

        // Precision loss when converting to double
        Console.WriteLine("\nPrecision loss with double conversion:");

        BigInteger hugNumber = BigInteger.Pow(2, 100);
        double asDouble = (double)hugNumber;
        BigInteger backToBig = (BigInteger)asDouble;

        Console.WriteLine($"  Original: {hugNumber}");
        Console.WriteLine($"  As double: {asDouble:E3}");
        Console.WriteLine($"  Back to BigInteger: {backToBig}");
        Console.WriteLine($"  Precision lost: {hugNumber != backToBig}");

        Console.WriteLine();
    }
    static BigInteger CalculateFactorial(int n)
        {
            BigInteger result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
}