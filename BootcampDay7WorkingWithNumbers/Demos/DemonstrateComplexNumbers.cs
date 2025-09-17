using System.Numerics;

namespace BootcampDay7.Demo;

public class DemoClassCN
{

    public static void DemonstrateComplexNumbers()
    {
        Console.WriteLine("8. COMPLEX NUMBERS - REAL AND IMAGINARY PARTS");
        Console.WriteLine("==============================================");

        // Complex numbers are essential for advanced mathematics and engineering
        Console.WriteLine("Creating and working with complex numbers:");

        // Creating complex numbers
        Complex c1 = new Complex(3, 4);        // 3 + 4i
        Complex c2 = new Complex(1, -2);       // 1 - 2i
        Complex real = new Complex(5, 0);      // Real number
        Complex imaginary = new Complex(0, 3); // Pure imaginary number

        Console.WriteLine($"  c1 = {c1}");
        Console.WriteLine($"  c2 = {c2}");
        Console.WriteLine($"  real = {real}");
        Console.WriteLine($"  imaginary = {imaginary}");

        // Properties of complex numbers
        Console.WriteLine("\nComplex number properties:");

        Console.WriteLine($"  c1 real part: {c1.Real}");
        Console.WriteLine($"  c1 imaginary part: {c1.Imaginary}");
        Console.WriteLine($"  c1 magnitude: {c1.Magnitude:F3}");
        Console.WriteLine($"  c1 phase (radians): {c1.Phase:F3}");
        Console.WriteLine($"  c1 phase (degrees): {c1.Phase * 180 / Math.PI:F1}°");

        // Complex arithmetic
        Console.WriteLine("\nComplex arithmetic:");

        Complex sum = c1 + c2;
        Complex difference = c1 - c2;
        Complex product = c1 * c2;
        Complex quotient = c1 / c2;

        Console.WriteLine($"  {c1} + {c2} = {sum}");
        Console.WriteLine($"  {c1} - {c2} = {difference}");
        Console.WriteLine($"  {c1} * {c2} = {product}");
        Console.WriteLine($"  {c1} / {c2} = {quotient}");

        // Complex conjugate and other operations
        Console.WriteLine("\nAdvanced complex operations:");

        Complex conjugate = Complex.Conjugate(c1);
        Complex reciprocal = Complex.Reciprocal(c1);

        Console.WriteLine($"  Conjugate of {c1}: {conjugate}");
        Console.WriteLine($"  Reciprocal of {c1}: {reciprocal}");
        Console.WriteLine($"  |{c1}|² = {c1.Magnitude * c1.Magnitude:F3}");
        Console.WriteLine($"  {c1} * conjugate = {c1 * conjugate}");

        // Practical example: AC circuit analysis (simplified)
        Console.WriteLine("\nPractical example - AC circuit impedance:");

        // Impedance = Resistance + j*Reactance
        Complex impedance1 = new Complex(100, 50);  // 100Ω resistance, 50Ω inductive reactance
        Complex impedance2 = new Complex(75, -25);  // 75Ω resistance, 25Ω capacitive reactance

        Complex totalImpedance = impedance1 + impedance2;

        Console.WriteLine($"  Z1 = {impedance1} Ω");
        Console.WriteLine($"  Z2 = {impedance2} Ω");
        Console.WriteLine($"  Total impedance = {totalImpedance} Ω");
        Console.WriteLine($"  Magnitude = {totalImpedance.Magnitude:F1} Ω");
        Console.WriteLine($"  Phase = {totalImpedance.Phase * 180 / Math.PI:F1}°");

        Console.WriteLine();
    }
}