namespace Demos;

public class DemoClassDI
{
    public static void DemonstrateIterators()
        {
            Console.WriteLine("--- 6. Iterator Methods with yield ---");
            
            Console.WriteLine("Fibonacci sequence (first 8 numbers):");
            foreach (int fib in GenerateFibonacci(8))
            {
                Console.Write($"{fib} ");
            }
            Console.WriteLine();
            
            Console.WriteLine("\nSquares of numbers 1-5:");
            foreach (int square in GenerateSquares(5))
            {
                Console.Write($"{square} ");
            }
            Console.WriteLine("\n");
        }

        // Iterator method that generates Fibonacci sequence
        // Notice: this method doesn't execute immediately when called
        // It returns an IEnumerable that produces values on-demand
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

        // Another iterator example
        static IEnumerable<int> GenerateSquares(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                yield return i * i; // Yields the square of i
            }
        }

}