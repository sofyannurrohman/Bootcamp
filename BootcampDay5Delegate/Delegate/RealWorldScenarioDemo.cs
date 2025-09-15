namespace DelegateDemo;

public class DelegateClassRWSD
{

    public static void RealWorldScenarioDemo()
    {
        Console.WriteLine("11. REAL WORLD SCENARIO - FILE PROCESSING SYSTEM");
        Console.WriteLine("================================================");

        // Simulate a file processing system with pluggable processing logic
        FileProcessor processor = new FileProcessor();

        // Subscribe to progress events (multicast delegate in action)
        processor.Progress += (percent) => Console.WriteLine($"  Console: {percent}% processed");
        processor.Progress += (percent) =>
        {
            if (percent % 25 == 0)  // Every 25%
                Console.WriteLine($"  Milestone: Reached {percent}% completion!");
        };

        // Different processing strategies using delegates
        Console.WriteLine("Processing with different strategies:");

        // Strategy 1: Simple text processing
        Console.WriteLine("\n1. Text processing strategy:");
        processor.ProcessFiles(new[] { "doc1.txt", "doc2.txt" }, ProcessTextFile);

        // Strategy 2: Image processing
        Console.WriteLine("\n2. Image processing strategy:");
        processor.ProcessFiles(new[] { "img1.jpg", "img2.png" }, ProcessImageFile);

        // Strategy 3: Lambda expression for custom processing
        Console.WriteLine("\n3. Custom processing with lambda:");
        processor.ProcessFiles(new[] { "data1.xml", "data2.json" },
            fileName =>
            {
                Console.WriteLine($"    Custom processing: {fileName}");
                Thread.Sleep(200);  // Simulate work
                return $"Processed_{fileName}";
            });

        Console.WriteLine();
    }

    // Processing strategy methods
    static string ProcessTextFile(string fileName)
    {
        Console.WriteLine($"    Text processing: {fileName}");
        Thread.Sleep(300);  // Simulate processing time
        return $"TEXT_{fileName}";
    }

    static string ProcessImageFile(string fileName)
    {
        Console.WriteLine($"    Image processing: {fileName}");
        Thread.Sleep(500);  // Simulate longer processing
        return $"IMG_{fileName}";
    }
    public class FileProcessor
    {
        // Event using multicast delegate
        public event Action<int>? Progress;

        // Method that uses strategy pattern with delegates
        public void ProcessFiles(string[] fileNames, Func<string, string> processingStrategy)
        {
            for (int i = 0; i < fileNames.Length; i++)
            {
                // Calculate progress
                int percent = (i * 100) / fileNames.Length;
                Progress?.Invoke(percent);  // Notify all subscribers

                // Apply the plugged-in processing strategy
                string result = processingStrategy(fileNames[i]);
                Console.WriteLine($"    Result: {result}");
            }

            // Final progress report
            Progress?.Invoke(100);
        }
    }

}