namespace BootcampDay7.Demo;

public class DemoClassDEC
{
    /// <summary>
    /// Environment Class Demonstration
    /// Shows system information retrieval and environment variable access
    /// Essential for applications that need to adapt to different environments
    /// </summary>
    public static void DemonstrateEnvironmentClass()
    {
        Console.WriteLine("=== ENVIRONMENT CLASS DEMONSTRATION ===");

        // System information - crucial for diagnostics and support
        Console.WriteLine("1. System Information:");
        Console.WriteLine($"Machine Name: {Environment.MachineName}");
        Console.WriteLine($"Operating System: {Environment.OSVersion}");
        Console.WriteLine($"Current User: {Environment.UserName}");
        Console.WriteLine($"System Uptime: {Environment.TickCount} milliseconds");
        Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
        Console.WriteLine($"Working Set: {Environment.WorkingSet:N0} bytes");

        // Framework and runtime information
        Console.WriteLine("\n2. Runtime Information:");
        Console.WriteLine($".NET Version: {Environment.Version}");
        Console.WriteLine($"Is 64-bit OS: {Environment.Is64BitOperatingSystem}");
        Console.WriteLine($"Is 64-bit Process: {Environment.Is64BitProcess}");
        Console.WriteLine($"System Directory: {Environment.SystemDirectory}");

        // Special folders - these paths change between different OS versions
        Console.WriteLine("\n3. Special Folders:");
        Console.WriteLine($"User Profile: {Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}");
        Console.WriteLine($"Desktop: {Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}");
        Console.WriteLine($"My Documents: {Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}");
        Console.WriteLine($"Application Data: {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}");
        Console.WriteLine($"Program Files: {Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}");

        // Environment variables - essential for configuration
        Console.WriteLine("\n4. Environment Variables:");
        Console.WriteLine("Important system environment variables:");

        string[] importantVars = { "PATH", "TEMP", "USERNAME", "COMPUTERNAME", "OS" };
        foreach (string varName in importantVars)
        {
            string? value = Environment.GetEnvironmentVariable(varName);
            // Truncate PATH because it's usually very long
            if (varName == "PATH" && value != null && value.Length > 100)
            {
                value = value.Substring(0, 100) + "... (truncated)";
            }
            Console.WriteLine($"  {varName}: {value ?? "Not found"}");
        }

        // Command line arguments
        Console.WriteLine("\n5. Command Line Arguments:");
        string[] commandArgs = Environment.GetCommandLineArgs();
        Console.WriteLine($"Application started with {commandArgs.Length} arguments:");
        for (int i = 0; i < commandArgs.Length; i++)
        {
            Console.WriteLine($"  Arg[{i}]: {commandArgs[i]}");
        }

        // Environment manipulation
        Console.WriteLine("\n6. Environment Manipulation:");
        Environment.SetEnvironmentVariable("DEMO_VAR", "This is a demo value");
        string? demoValue = Environment.GetEnvironmentVariable("DEMO_VAR");
        Console.WriteLine($"Set and retrieved demo variable: {demoValue ?? "Failed to retrieve"}");

        Console.WriteLine();
    }
}