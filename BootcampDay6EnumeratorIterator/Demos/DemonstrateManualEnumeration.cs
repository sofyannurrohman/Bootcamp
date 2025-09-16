namespace Demos;

public class DemoClassDME
{
    public static void DemonstrateManualEnumeration()
        {
            Console.WriteLine("--- 2. Manual Enumeration (what foreach does behind the scenes) ---");
            
            string word = "beer";
            Console.WriteLine($"Manually iterating through '{word}' using GetEnumerator():");
            
            // This is what the compiler generates for foreach statements
            using (var enumerator = word.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    Console.WriteLine($"  Character: {element}");
                }
            } // Dispose is called automatically due to 'using'
            Console.WriteLine();
        }
}