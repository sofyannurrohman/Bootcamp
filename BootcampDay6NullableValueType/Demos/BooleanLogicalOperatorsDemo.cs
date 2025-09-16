namespace Demos;
public class DemoClassBLO {
    
public static void BooleanLogicalOperatorsDemo()
    {
        Console.WriteLine("10. BOOLEAN LOGICAL OPERATORS DEMONSTRATION");
        Console.WriteLine("===========================================");

        bool? n = null;
        bool? f = false;
        bool? t = true;

        Console.WriteLine($"n = {n}, f = {f}, t = {t}");
        Console.WriteLine("\nTesting logical OR (|) operator:");

        // OR operations - null is treated as "unknown"
        Console.WriteLine($"n | n = {n | n}");        // null | null = null
        Console.WriteLine($"n | f = {n | f}");        // null | false = null
        Console.WriteLine($"n | t = {n | t}");        // null | true = true (because true OR anything is true)
        Console.WriteLine($"f | n = {f | n}");        // false | null = null
        Console.WriteLine($"t | n = {t | n}");        // true | null = true

        Console.WriteLine("\nTesting logical AND (&) operator:");

        // AND operations
        Console.WriteLine($"n & n = {n & n}");        // null & null = null
        Console.WriteLine($"n & f = {n & f}");        // null & false = false (because false AND anything is false)
        Console.WriteLine($"n & t = {n & t}");        // null & true = null
        Console.WriteLine($"f & n = {f & n}");        // false & null = false
        Console.WriteLine($"t & n = {t & n}");        // true & null = null

        Console.WriteLine("\nKey insights:");
        Console.WriteLine("- true OR anything = true (even null)");
        Console.WriteLine("- false AND anything = false (even null)");
        Console.WriteLine("- Other combinations with null remain null");

        // Practical example
        Console.WriteLine("\nPractical example - user permissions:");
        bool? hasAdminRights = null;  // Unknown
        bool? hasReadAccess = true;
        bool? hasWriteAccess = false;

        bool? canPerformAction = hasAdminRights | (hasReadAccess & hasWriteAccess);
        Console.WriteLine($"Can perform action: {canPerformAction}");  // false

        Console.WriteLine();
    }
}