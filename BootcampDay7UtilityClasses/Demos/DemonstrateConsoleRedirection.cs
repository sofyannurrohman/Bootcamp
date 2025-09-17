namespace BootcampDay7.Demo;
public class DemoClassDCR{
    
public static void DemonstrateConsoleRedirection()
    {
        // Save the current console output
        TextWriter originalOut = Console.Out;

        try
        {
            // Create a temporary file for demonstration
            string tempFile = Path.GetTempFileName();

            using (TextWriter fileWriter = File.CreateText(tempFile))
            {
                // Redirect console output to file
                Console.SetOut(fileWriter);
                Console.WriteLine("This message goes to the file, not the console");
                Console.WriteLine("Timestamp: " + DateTime.Now);
                Console.WriteLine("This is useful for logging application output");
            }

            // Restore original console output
            Console.SetOut(originalOut);

            // Read and display what was written to the file
            string fileContent = File.ReadAllText(tempFile);
            Console.WriteLine("Content written to file:");
            Console.WriteLine(fileContent);

            // Clean up
            File.Delete(tempFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during redirection demo: {ex.Message}");
        }
    }
}