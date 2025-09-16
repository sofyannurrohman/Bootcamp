using Entities;

namespace Demos;

public class DemoClassDCE
{
    public static void DemonstrateCustomEnumerable()
        {
            Console.WriteLine("--- 3. Custom Enumerable and Enumerator ---");
            
            var countdown = new CountdownSequence(5);
            Console.WriteLine("Custom countdown sequence from 5 to 1:");
            
            foreach (int number in countdown)
            {
                Console.WriteLine($"  Count: {number}");
            }
            Console.WriteLine();
        }
}