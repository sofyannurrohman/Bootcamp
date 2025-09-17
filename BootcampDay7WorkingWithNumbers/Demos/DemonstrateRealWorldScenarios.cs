using System.Numerics;
namespace BootcampDay7.Demo;

public class DemoClassRWS {

public static void DemonstrateRealWorldScenarios()
{
    Console.WriteLine("11. REAL-WORLD SCENARIOS");
    Console.WriteLine("=========================");

    // Scenario 1: Financial calculations with precision
    Console.WriteLine("Scenario 1: Financial calculations");

    decimal principal = 10000m;
    decimal annualRate = 0.05m; // 5%
    int years = 10;

    // Compound interest calculation
    decimal compoundAmount = principal * (decimal)Math.Pow((double)(1 + annualRate), years);
    decimal simpleInterest = principal * (1 + annualRate * years);

    Console.WriteLine($"  Principal: {principal:C}");
    Console.WriteLine($"  Annual rate: {annualRate:P}");
    Console.WriteLine($"  Years: {years}");
    Console.WriteLine($"  Compound interest: {compoundAmount:C}");
    Console.WriteLine($"  Simple interest: {simpleInterest:C}");
    Console.WriteLine($"  Difference: {compoundAmount - simpleInterest:C}");

    // Loan payment calculation
    decimal loanAmount = 250000m;
    decimal monthlyRate = 0.04m / 12; // 4% annual / 12 months
    int months = 30 * 12; // 30 years

    decimal monthlyPayment = loanAmount * (monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), months))
                           / ((decimal)Math.Pow((double)(1 + monthlyRate), months) - 1);

    Console.WriteLine($"\n  Loan calculation:");
    Console.WriteLine($"    Loan amount: {loanAmount:C}");
    Console.WriteLine($"    Monthly payment: {monthlyPayment:C}");
    Console.WriteLine($"    Total paid: {monthlyPayment * months:C}");
    Console.WriteLine($"    Total interest: {(monthlyPayment * months) - loanAmount:C}");

    // Scenario 2: Scientific calculations
    Console.WriteLine("\n\nScenario 2: Scientific calculations");

    // Physics: Projectile motion
    double initialVelocity = 50; // m/s
    double angle = 45; // degrees
    double gravity = 9.81; // m/s²

    double angleRadians = angle * Math.PI / 180;
    double maxHeight = Math.Pow(initialVelocity * Math.Sin(angleRadians), 2) / (2 * gravity);
    double range = Math.Pow(initialVelocity, 2) * Math.Sin(2 * angleRadians) / gravity;
    double flightTime = 2 * initialVelocity * Math.Sin(angleRadians) / gravity;

    Console.WriteLine($"  Projectile motion:");
    Console.WriteLine($"    Initial velocity: {initialVelocity} m/s");
    Console.WriteLine($"    Launch angle: {angle}°");
    Console.WriteLine($"    Maximum height: {maxHeight:F2} m");
    Console.WriteLine($"    Range: {range:F2} m");
    Console.WriteLine($"    Flight time: {flightTime:F2} s");

    // Chemistry: Ideal gas law calculations
    double pressure = 101325; // Pa (1 atm)
    double volume = 0.0224; // m³ (22.4 L)
    double gasConstant = 8.314; // J/(mol·K)
    double temperature = pressure * volume / gasConstant; // Kelvin

    Console.WriteLine($"\n  Ideal gas law (PV = nRT, assuming 1 mole):");
    Console.WriteLine($"    Pressure: {pressure:N0} Pa");
    Console.WriteLine($"    Volume: {volume:F4} m³");
    Console.WriteLine($"    Temperature: {temperature:F2} K ({temperature - 273.15:F2}°C)");

    // Scenario 3: Data analysis and statistics
    Console.WriteLine("\n\nScenario 3: Statistical analysis");

    double[] dataset = { 23.5, 18.2, 31.7, 22.1, 28.9, 19.8, 26.4, 33.2, 21.6, 29.3 };

    double mean = dataset.Average();
    double variance = dataset.Select(x => Math.Pow(x - mean, 2)).Average();
    double standardDeviation = Math.Sqrt(variance);
    double median = dataset.OrderBy(x => x).Skip(dataset.Length / 2).First();

    Console.WriteLine($"  Dataset: [{string.Join(", ", dataset.Select(x => x.ToString("F1")))}]");
    Console.WriteLine($"  Mean: {mean:F2}");
    Console.WriteLine($"  Median: {median:F1}");
    Console.WriteLine($"  Standard deviation: {standardDeviation:F2}");
    Console.WriteLine($"  Min: {dataset.Min():F1}");
    Console.WriteLine($"  Max: {dataset.Max():F1}");

    // Normal distribution probability (approximation)
    double value = 25.0;
    double zScore = (value - mean) / standardDeviation;
    double probability = 0.5 * (1 + Erf(zScore / Math.Sqrt(2))); // Cumulative probability

    Console.WriteLine($"\n  Normal distribution analysis for value {value}:");
    Console.WriteLine($"    Z-score: {zScore:F2}");
    Console.WriteLine($"    Cumulative probability: {probability:F3} ({probability * 100:F1}%)");

    // Scenario 4: Cryptographic and security applications
    Console.WriteLine("\n\nScenario 4: Cryptographic operations");

    // Generate a simple hash (for demonstration - not cryptographically secure)
    string data = "Hello, World!";
    byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
    uint simpleHash = 0;

    foreach (byte b in dataBytes)
    {
        simpleHash = simpleHash * 31 + b;
    }

    Console.WriteLine($"  Data: \"{data}\"");
    Console.WriteLine($"  Simple hash: 0x{simpleHash:X8}");

    // RSA key size calculation
    int[] rsaKeySizes = { 1024, 2048, 3072, 4096 };

    Console.WriteLine("\n  RSA key strength estimates:");
    foreach (int keySize in rsaKeySizes)
    {
        // Very rough estimate of security level in bits
        double securityBits = keySize / 3.0;
        BigInteger keySpace = BigInteger.Pow(2, keySize);

        Console.WriteLine($"    {keySize}-bit RSA: ~{securityBits:F0} bits security");
        Console.WriteLine($"      Key space: 2^{keySize} ≈ 10^{Math.Log10((double)keySpace):F0}");
    }

    Console.WriteLine();
}

 // Helper methods
        static string ConvertToPermissionString(int octalValue)
        {
            // Convert octal permission to rwx format
            string[] permissions = { "---", "--x", "-w-", "-wx", "r--", "r-x", "rw-", "rwx" };
            
            int owner = (octalValue >> 6) & 7;
            int group = (octalValue >> 3) & 7;
            int other = octalValue & 7;
            
            return permissions[owner] + permissions[group] + permissions[other];
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

        // Error function approximation for normal distribution
        static double Erf(double x)
        {
            // Abramowitz and Stegun approximation
            double a1 =  0.254829592;
            double a2 = -0.284496736;
            double a3 =  1.421413741;
            double a4 = -1.453152027;
            double a5 =  1.061405429;
            double p  =  0.3275911;

            int sign = x < 0 ? -1 : 1;
            x = Math.Abs(x);

            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }
}
    