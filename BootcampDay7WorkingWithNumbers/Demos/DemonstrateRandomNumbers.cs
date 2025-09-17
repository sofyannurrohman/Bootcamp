using System.Security.Cryptography;

namespace BootcampDay7.Demo;

public class DemoClassRN
{
    public static void DemonstrateRandomNumbers()
    {
        Console.WriteLine("9. RANDOM NUMBERS - PSEUDORANDOM AND CRYPTOGRAPHIC");
        Console.WriteLine("==================================================");

        // Random numbers are crucial for simulations, games, and security
        // Key principle: Use ONE static Random instance per application to avoid duplicate sequences
        // For crypto: Use RandomNumberGenerator, not Random!

        Console.WriteLine("Understanding Random class fundamentals:");
        Console.WriteLine("- Pseudorandom: mathematically generated, not truly random");
        Console.WriteLine("- Deterministic: same seed = same sequence (useful for testing)");
        Console.WriteLine("- Thread safety: Random is NOT thread-safe");
        Console.WriteLine("- Best practice: Use one static instance per application");

        Console.WriteLine("\nBasic Random class usage:");

        Random random = new Random(42); // Fixed seed for reproducible results

        Console.WriteLine("  Random integers:");
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"    Next(): {random.Next(),12} (0 to int.MaxValue)");
            Console.WriteLine($"    Next(100): {random.Next(100),10} (0 to 99)");
            Console.WriteLine($"    Next(10, 50): {random.Next(10, 50),8} (10 to 49)");
            Console.WriteLine();
        }

        Console.WriteLine("  Random floating-point numbers:");
        for (int i = 0; i < 5; i++)
        {
            double nextDouble = random.NextDouble();
            Console.WriteLine($"    NextDouble(): {nextDouble:F6} (0.0 to 0.999999...)");
            Console.WriteLine($"    Range [5.0, 10.0): {nextDouble * 5 + 5:F2}");
        }

        // Random bytes - useful for cryptographic applications
        Console.WriteLine("\n  Random bytes:");
        byte[] randomBytes = new byte[10];
        random.NextBytes(randomBytes);
        Console.WriteLine($"    Byte array: [{string.Join(", ", randomBytes)}]");

        // NEW .NET 8 methods - these are powerful additions!
        Console.WriteLine("\n  NEW .NET 8 Random methods:");

        try
        {
            // GetItems - select random items from a collection
            string[] colors = { "Red", "Green", "Blue", "Yellow", "Purple", "Orange", "Pink", "Brown" };

            // This method is new in .NET 8 - may not be available in all versions
            Console.WriteLine("    Random color selection:");
            for (int i = 0; i < 3; i++)
            {
                // For compatibility, we'll use the traditional approach
                string randomColor = colors[random.Next(colors.Length)];
                Console.WriteLine($"      Selected: {randomColor}");
            }

            // Shuffle - randomize array order (manual implementation for compatibility)
            Console.WriteLine("\n    Array shuffling (Fisher-Yates algorithm):");
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine($"      Original: [{string.Join(", ", numbers)}]");

            // Fisher-Yates shuffle implementation
            for (int i = numbers.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
            }
            Console.WriteLine($"      Shuffled: [{string.Join(", ", numbers)}]");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"    .NET 8 methods not available: {ex.Message}");
        }

        // Demonstrating the critical importance of seed
        Console.WriteLine("\nSeed importance - WHY this matters:");

        Console.WriteLine("  Same seed = identical sequence (useful for testing):");
        Random seeded1 = new Random(123);
        Random seeded2 = new Random(123);

        for (int i = 0; i < 3; i++)
        {
            int val1 = seeded1.Next(100);
            int val2 = seeded2.Next(100);
            Console.WriteLine($"    Seeded1: {val1,3}, Seeded2: {val2,3} (identical: {val1 == val2})");
        }

        Console.WriteLine("\n  Different seeds = different sequences:");
        Random different1 = new Random(456);
        Random different2 = new Random(789);

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"    Seed 456: {different1.Next(100),3}, Seed 789: {different2.Next(100),3}");
        }

        // The dangerous pattern - multiple Random instances created quickly
        Console.WriteLine("\n  DANGER: Creating multiple Random instances rapidly:");
        Console.WriteLine("  (This can produce identical sequences due to system clock granularity)");

        // This demonstrates the problem
        Random quick1 = new Random();
        Random quick2 = new Random();
        Random quick3 = new Random();

        Console.WriteLine("    Three Random instances created rapidly:");
        Console.WriteLine($"      Random1: {quick1.Next(1000)}");
        Console.WriteLine($"      Random2: {quick2.Next(1000)}");
        Console.WriteLine($"      Random3: {quick3.Next(1000)}");
        Console.WriteLine("    (They might be similar or identical!)");

        // Practical examples - real-world applications
        Console.WriteLine("\nPractical Random applications:");

        // Dice simulation with statistical analysis
        Console.WriteLine("  Dice roll simulation (6-sided die, 1000 rolls):");
        Random diceRandom = new Random();
        int[] diceCounts = new int[6];

        for (int i = 0; i < 1000; i++)
        {
            int roll = diceRandom.Next(1, 7);
            diceCounts[roll - 1]++;
        }

        Console.WriteLine("    Results (should be roughly equal for fair die):");
        for (int i = 0; i < 6; i++)
        {
            double percentage = diceCounts[i] / 10.0;
            string bar = new string('█', (int)(percentage / 2)); // Visual bar
            Console.WriteLine($"      {i + 1}: {diceCounts[i],3} times ({percentage:F1}%) {bar}");
        }

        // Monte Carlo method example - estimating π
        Console.WriteLine("\n  Monte Carlo estimation of π:");
        int totalPoints = 100000;
        int pointsInCircle = 0;
        Random mcRandom = new Random(42); // Fixed seed for reproducible result

        for (int i = 0; i < totalPoints; i++)
        {
            double x = mcRandom.NextDouble() * 2 - 1; // -1 to 1
            double y = mcRandom.NextDouble() * 2 - 1; // -1 to 1

            if (x * x + y * y <= 1) // Inside unit circle
            {
                pointsInCircle++;
            }
        }

        double estimatedPi = 4.0 * pointsInCircle / totalPoints;
        double error = Math.Abs(estimatedPi - Math.PI);

        Console.WriteLine($"    Points inside circle: {pointsInCircle:N0} out of {totalPoints:N0}");
        Console.WriteLine($"    Estimated π: {estimatedPi:F6}");
        Console.WriteLine($"    Actual π: {Math.PI:F6}");
        Console.WriteLine($"    Error: {error:F6} ({error / Math.PI * 100:F2}%)");

        // Cryptographically secure random numbers - for security applications
        Console.WriteLine("\nCryptographically secure random numbers:");
        Console.WriteLine("  Use RandomNumberGenerator for security-critical applications!");
        Console.WriteLine("  Regular Random is predictable - NEVER use for passwords, keys, etc.");

        using (RandomNumberGenerator cryptoRandom = RandomNumberGenerator.Create())
        {
            // Generate secure random bytes
            byte[] secureBytes = new byte[16];
            cryptoRandom.GetBytes(secureBytes);

            Console.WriteLine($"  Secure random bytes: [{string.Join(", ", secureBytes.Take(8))}...]");

            // Generate secure random integer
            byte[] intBytes = new byte[4];
            cryptoRandom.GetBytes(intBytes);
            int secureInt = Math.Abs(BitConverter.ToInt32(intBytes, 0));
            Console.WriteLine($"  Secure random int: {secureInt:N0}");

            // Generate secure random password
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            byte[] passwordBytes = new byte[12];
            cryptoRandom.GetBytes(passwordBytes);

            string password = new string(passwordBytes.Select(b => chars[b % chars.Length]).ToArray());
            Console.WriteLine($"  Secure random password: {password}");
        }

        // Performance considerations
        Console.WriteLine("\nPerformance considerations:");
        Console.WriteLine("  ✓ Random.Next(): Very fast for most applications");
        Console.WriteLine("  ✓ RandomNumberGenerator: Slower but cryptographically secure");
        Console.WriteLine("  ✓ Reuse Random instances - don't create new ones frequently");
        Console.WriteLine("  ✗ Never use Random for security (passwords, tokens, etc.)");
        Console.WriteLine("  ✗ Don't create multiple Random instances in tight loops");

        Console.WriteLine();
    }

}