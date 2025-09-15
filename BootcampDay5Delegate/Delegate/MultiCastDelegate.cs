namespace DelegateDemo;
public class DelegateClassMDD
{
    delegate int Transformer(int x);
    delegate void ProgressReporter(int percentComplete);
    static int Square(int x) => x * x;
    static int Cube(int x) => x * x * x;

    public static void MulticastDelegatesDemo()
    {
        Console.WriteLine("4. MULTICAST DELEGATES - COMBINING MULTIPLE METHODS");
        Console.WriteLine("===================================================");

        // Start with a single method
        ProgressReporter? reporter = WriteProgressToConsole;

        // Add more methods using += operator
        // Remember: delegates are immutable, so += creates a new delegate
        reporter += WriteProgressToFile;
        reporter += SendProgressAlert;

        Console.WriteLine("Progress reporting with multicast delegate (3 methods):");
        reporter(50);  // This calls ALL three methods in the order they were added

        Console.WriteLine("\nRemoving console reporter using -= operator:");
        reporter -= WriteProgressToConsole;

        Console.WriteLine("Progress reporting after removal (2 methods):");
        if (reporter != null)
            reporter(75);

        // Demonstrate that return values are lost in multicast (except the last one)
        Console.WriteLine("\nMulticast with return values (only last one is kept):");
        Transformer multiTransformer = Square;
        multiTransformer += Cube;  // Now has two methods

        int lastResult = multiTransformer(3);  // Calls Square(3) then Cube(3)
        Console.WriteLine($"Only the last result is returned: {lastResult}");  // Will be 27 (cube), not 9 (square)

        Console.WriteLine();
    }

    static void WriteProgressToConsole(int percentComplete)
    {
        Console.WriteLine($"  Console Log: {percentComplete}% complete");
    }

    static void WriteProgressToFile(int percentComplete)
    {
        Console.WriteLine($"  File Log: Writing {percentComplete}% to progress.log");
    }

    static void SendProgressAlert(int percentComplete)
    {
        if (percentComplete >= 75)
            Console.WriteLine($"  Alert: High progress reached - {percentComplete}%!");
    }
}