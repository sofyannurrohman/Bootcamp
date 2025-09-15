namespace DelegateDemo;

public class DelegateClassDC
{
    delegate void D1();
    delegate void D2();
    public static void DelegateCompatibilityDemo()
    {
        Console.WriteLine("8. DELEGATE COMPATIBILITY - TYPE SAFETY RULES");
        Console.WriteLine("=============================================");

        void TestMethod() => Console.WriteLine("  Test method executed");

        // Same signature, different delegate types - not compatible
        D1 d1 = TestMethod;

        // This would cause compile error:
        // D2 d2 = d1;  // Cannot convert D1 to D2

        // But explicit construction works
        D2 d2 = new D2(d1);

        Console.WriteLine("Both delegates call the same method:");
        d1();
        d2();

        // Delegate equality based on method targets and order
        D1 d1Copy = TestMethod;
        Console.WriteLine($"d1 == d1Copy (same method): {d1 == d1Copy}");  // True

        // Multicast delegates - equality depends on all methods in same order
        D1 d1Multi = TestMethod;
        d1Multi += TestMethod;  // Now has two references to TestMethod

        Console.WriteLine($"d1 == d1Multi (different invocation lists): {d1 == d1Multi}");  // False

        Console.WriteLine();
    }
}