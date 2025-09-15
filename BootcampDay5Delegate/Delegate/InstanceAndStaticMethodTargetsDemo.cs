namespace DelegateDemo;

public class DelegateClassISMTD
{
    delegate int Transformer(int x);
    static int Square(int x) => x * x;
    static int Cube(int x) => x * x * x;
    private class Calculator
    {
        private int multiplier;

        public Calculator(int multiplier)
        {
            this.multiplier = multiplier;
        }

        public int Multiplier => multiplier;

        // Instance method that matches our Transformer delegate
        public int MultiplyBy(int input)
        {
            return input * multiplier;
        }
    }
    public static void InstanceAndStaticMethodTargetsDemo()
    {
        Console.WriteLine("3. INSTANCE AND STATIC METHOD TARGETS");
        Console.WriteLine("=====================================");

        // Static method target - no object instance needed
        Console.WriteLine("Static method delegation:");
        Transformer staticDelegate = Square;
        Console.WriteLine($"Static Square of 6: {staticDelegate(6)}");

        // Instance method target - delegate holds both method AND object reference
        Console.WriteLine("\nInstance method delegation:");
        Calculator calc = new Calculator(5);  // Object with multiplier = 5
        Transformer instanceDelegate = calc.MultiplyBy;  // Points to instance method

        Console.WriteLine($"Multiply 8 by {calc.Multiplier}: {instanceDelegate(8)}");

        // The delegate keeps the object alive - demonstrate this with Target property
        Console.WriteLine($"Delegate Target is null (static): {staticDelegate.Target == null}");
        Console.WriteLine($"Delegate Target is Calculator instance: {instanceDelegate.Target is Calculator}");

        // Multiple instances, multiple delegates
        Calculator calc2 = new Calculator(3);
        Transformer instanceDelegate2 = calc2.MultiplyBy;

        Console.WriteLine($"Different instance - multiply 8 by {calc2.Multiplier}: {instanceDelegate2(8)}");

        Console.WriteLine();
    }
}