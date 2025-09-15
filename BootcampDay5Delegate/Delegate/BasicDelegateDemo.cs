namespace DelegateDemo;
// First, let's define a delegate type - this is like creating a blueprint
// Any method that takes an int and returns an int can be assigned to this delegate
public class DelegateClassBDM
{
delegate int Transformer(int x);
public static void BasicDelegateDemo()
{
    Console.WriteLine("1. BASIC DELEGATE USAGE - THE FOUNDATION");
    Console.WriteLine("========================================");

    // Step 1: Create a delegate instance pointing to a method
    Transformer t = Square;  // This is shorthand for: new Transformer(Square)

    // Step 2: Invoke the delegate just like calling a method
    int result = t(3);  // This calls Square(3) through the delegate

    Console.WriteLine($"Square of 3 through delegate: {result}");

    // The beauty is indirection - we can change what method gets called
    t = Cube;  // Now t points to a different method
    result = t(3);  // Same syntax, different behavior

    Console.WriteLine($"Cube of 3 through same delegate: {result}");

    // You can also use the explicit Invoke method
    result = t.Invoke(4);
    Console.WriteLine($"Cube of 4 using Invoke: {result}");

    Console.WriteLine();
}

// Static methods that match our delegate signature
static int Square(int x) => x * x;
static int Cube(int x) => x * x * x;

}