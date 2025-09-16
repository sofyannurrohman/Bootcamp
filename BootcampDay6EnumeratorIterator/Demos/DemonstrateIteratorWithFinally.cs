namespace Demos;

public class DemoClassDIF
{
    public static void DemonstrateIteratorWithFinally()
        {
            Console.WriteLine("--- 8. Iterator with try-finally (Resource Management) ---");
            
            Console.WriteLine("Processing numbers with cleanup:");
            foreach (string result in ProcessNumbersWithCleanup([1, 2, 3]))
            {
                Console.WriteLine($"  {result}");
            }
            Console.WriteLine();
        }

        // Iterator that demonstrates proper resource management
        // The finally block executes when enumeration completes or is disposed
        static IEnumerable<string> ProcessNumbersWithCleanup(IEnumerable<int> numbers)
        {
            Console.WriteLine("  Setup: Initializing resources...");
            
            try
            {
                foreach (int number in numbers)
                {
                    // yield return can appear in try block with finally
                    yield return $"Processed: {number * 2}";
                }
            }
            finally
            {
                // This executes when enumeration ends or enumerator is disposed
                Console.WriteLine("  Cleanup: Resources released");
            }
        }


}