namespace Demos;

public class DemoClassDCEX
{
    public static void DemonstrateCollectionExpressions()
        {
            Console.WriteLine("--- 5. Collection Expressions (C# 12+) ---");
            
            // Collection expressions use square brackets and are target-typed
            List<int> list = [1, 2, 3, 4, 5];
            int[] array = [10, 20, 30];
            
            Console.WriteLine("List created with collection expression:");
            foreach (int num in list)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
            
            Console.WriteLine("Array created with collection expression:");
            foreach (int num in array)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine("\n");
        }
}