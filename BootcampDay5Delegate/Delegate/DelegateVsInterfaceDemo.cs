namespace DelegateDemo;

public class DelegateClassDVI
{
    class MathOperations
    {
        public int Square(int x) => x * x;
        public int Cube(int x) => x * x * x;
        public int Double(int x) => x * 2;
    }
    interface ITransformer
    {
        int Transform(int x);
    }

    class SquareTransformer : ITransformer
    {
        public int Transform(int x) => x * x;
    }

    class CubeTransformer : ITransformer
    {
        public int Transform(int x) => x * x * x;
    }
    static void TransformWithInterface(int[] values, ITransformer transformer)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = transformer.Transform(values[i]);
        Console.WriteLine($"  Result: [{string.Join(", ", values)}]");
    }
    static void TransformWithDelegate(int[] values, Func<int, int> transformer)
    {
        for (int i = 0; i < values.Length; i++)
            values[i] = transformer(values[i]);
        Console.WriteLine($"  Result: [{string.Join(", ", values)}]");
    }
    public static void DelegateVsInterfaceDemo()
    {
        Console.WriteLine("7. DELEGATES VS INTERFACES - WHEN TO USE WHAT");
        Console.WriteLine("=============================================");

        // Interface approach
        Console.WriteLine("Interface approach:");
        ITransformer squareTransformer = new SquareTransformer();
        ITransformer cubeTransformer = new CubeTransformer();

        TransformWithInterface(new int[] { 2, 3, 4 }, squareTransformer);
        TransformWithInterface(new int[] { 2, 3, 4 }, cubeTransformer);

        // Delegate approach (more concise for single-method scenarios)
        Console.WriteLine("\nDelegate approach:");
        Func<int, int> squareDelegate = x => x * x;
        Func<int, int> cubeDelegate = x => x * x * x;

        TransformWithDelegate(new int[] { 2, 3, 4 }, squareDelegate);
        TransformWithDelegate(new int[] { 2, 3, 4 }, cubeDelegate);

        // Multiple implementations from single class (advantage of delegates)
        Console.WriteLine("\nMultiple implementations from single class:");
        MathOperations math = new MathOperations();

        // One class, multiple compatible methods
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Square);
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Cube);
        TransformWithDelegate(new int[] { 2, 3, 4 }, math.Double);

        Console.WriteLine();
    }
}
