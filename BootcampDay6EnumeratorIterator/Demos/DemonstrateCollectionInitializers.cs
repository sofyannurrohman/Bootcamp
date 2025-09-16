namespace Demos;

public class DemoClassDCI
{
    public static void DemonstrateCollectionInitializers()
        {
            Console.WriteLine("--- 4. Collection Initializers ---");
            
            // Collection initializer syntax - syntactic sugar for calling Add() method
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine("List created with collection initializer:");
            foreach (int num in numbers)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
            
            // Dictionary with collection initializer
            var colors = new Dictionary<string, string>
            {
                { "red", "#FF0000" },
                { "green", "#00FF00" },
                { "blue", "#0000FF" }
            };
            
            // Dictionary with indexer syntax
            var moreColors = new Dictionary<string, string>
            {
                ["yellow"] = "#FFFF00",
                ["purple"] = "#800080",
                ["orange"] = "#FFA500"
            };
            
            Console.WriteLine("Dictionary colors:");
            foreach (var kvp in colors)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine();
        }
}