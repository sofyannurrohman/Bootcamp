namespace DelegateDemo;

public class DelegateClassFADD
{
    public static void FuncAndActionDelegatesDemo()
        {
            Console.WriteLine("6. FUNC AND ACTION DELEGATES - BUILT-IN CONVENIENCE");
            Console.WriteLine("===================================================");

            // Func delegates return values
            // Func<TResult> - no parameters, returns TResult
            // Func<T, TResult> - one parameter of type T, returns TResult
            // ... up to Func<T1, T2, ..., T16, TResult>
            
            Func<int, int> squareFunc = x => x * x;
            Func<int, int, int> addFunc = (a, b) => a + b;
            Func<string> getTimeFunc = () => DateTime.Now.ToString("HH:mm:ss");
            
            Console.WriteLine($"Func square of 7: {squareFunc(7)}");
            Console.WriteLine($"Func add 5 + 8: {addFunc(5, 8)}");
            Console.WriteLine($"Func current time: {getTimeFunc()}");
            
            // Action delegates return void
            // Action - no parameters
            // Action<T> - one parameter of type T
            // ... up to Action<T1, T2, ..., T16>
            
            Action simpleAction = () => Console.WriteLine("  Simple action executed");
            Action<string> messageAction = msg => Console.WriteLine($"  Message: {msg}");
            Action<int, string> complexAction = (num, text) => 
                Console.WriteLine($"  Number: {num}, Text: {text}");
            
            Console.WriteLine("Action demonstrations:");
            simpleAction();
            messageAction("Hello from Action!");
            complexAction(42, "The Answer");
            
            // The beauty: our Transform method can now use Func instead of custom delegate
            Console.WriteLine("\nUsing Func with Transform method:");
            int[] values = { 1, 2, 3, 4, 5 };
            TransformWithFunc(values, x => x * 2);  // Double each value
            Console.WriteLine($"Doubled values: [{string.Join(", ", values)}]");
            
            Console.WriteLine();
        }

        // Transform method using built-in Func delegate
        public static void TransformWithFunc<T>(T[] values, Func<T, T> transformer)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = transformer(values[i]);
        }
}