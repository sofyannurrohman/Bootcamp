namespace Demos;

public class DemoClassDSC
{
    static IEnumerable<int> GenerateFibonacci(int count)
        {
            if (count <= 0) yield break; // Early termination with yield break
            
            int previous = 0, current = 1;
            
            for (int i = 0; i < count; i++)
            {
                yield return current; // Yields current value and pauses execution
                
                // Calculate next Fibonacci number
                int next = previous + current;
                previous = current;
                current = next;
            }
        }
    public  static void DemonstrateSequenceComposition()
    {
        Console.WriteLine("--- 7. Composing Sequences ---");

        // Chain iterators together for powerful data processing
        var fibonacci = GenerateFibonacci(15);
        var evenFibs = FilterEvenNumbers(fibonacci);
        var limitedEvenFibs = TakeFirst(evenFibs, 4);

        Console.WriteLine("First 4 even Fibonacci numbers:");
        foreach (int num in limitedEvenFibs)
        {
            Console.Write($"{num} ");
        }
        Console.WriteLine("\n");
    }

        // Iterator that filters for even numbers only
        static IEnumerable<int> FilterEvenNumbers(IEnumerable<int> source)
        {
            foreach (int number in source)
            {
                if (number % 2 == 0)
                {
                    yield return number; // Only yield even numbers
                }
            }
        }

        // Iterator that takes only the first N elements
        static IEnumerable<T> TakeFirst<T>(IEnumerable<T> source, int count)
        {
            int taken = 0;
            foreach (T item in source)
            {
                if (taken >= count)
                    yield break; // Stop when we've taken enough
                
                yield return item;
                taken++;
            }
        }
}