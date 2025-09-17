using System.Diagnostics;
namespace BootcampDay7.Demo;
public class DemoClassDRWS{
    
/// <summary>
/// Shows real-world scenarios where utility classes are essential
/// These patterns appear frequently in production applications
/// </summary>
public static void DemonstrateRealWorldScenarios()
    {
        Console.WriteLine("Common real-world scenarios:");

        // Scenario 1: Application diagnostics
        Console.WriteLine("\nScenario 1: Application Diagnostics");
        GenerateDiagnosticReport();

        // Scenario 2: Configuration management
        Console.WriteLine("\nScenario 2: Environment-based Configuration");
        DemonstrateEnvironmentBasedConfig();

        // Scenario 3: External tool integration
        Console.WriteLine("\nScenario 3: External Tool Integration");
        DemonstrateExternalToolIntegration();
    }
         /// <summary>
        /// Generates a comprehensive diagnostic report
        /// Essential for troubleshooting production issues
        /// </summary>
        static void GenerateDiagnosticReport()
        {
            Console.WriteLine("Generating diagnostic report...");
            
            var report = new
            {
                Timestamp = DateTime.Now,
                Application = new
                {
                    BaseDirectory = AppContext.BaseDirectory,
                    Framework = AppContext.TargetFrameworkName,
                    Arguments = Environment.GetCommandLineArgs()
                },
                System = new
                {
                    MachineName = Environment.MachineName,
                    OSVersion = Environment.OSVersion.ToString(),
                    ProcessorCount = Environment.ProcessorCount,
                    Is64Bit = Environment.Is64BitOperatingSystem,
                    UserName = Environment.UserName,
                    WorkingSet = Environment.WorkingSet
                },
                Process = new
                {
                    Id = Process.GetCurrentProcess().Id,
                    Name = Process.GetCurrentProcess().ProcessName,
                    StartTime = Process.GetCurrentProcess().StartTime,
                    Memory = Process.GetCurrentProcess().WorkingSet64
                }
            };
            
            Console.WriteLine("Diagnostic report generated successfully:");
            Console.WriteLine($"  Generated at: {report.Timestamp}");
            Console.WriteLine($"  Running on: {report.System.MachineName} ({report.System.OSVersion})");
            Console.WriteLine($"  User: {report.System.UserName}");
            Console.WriteLine($"  Process: {report.Process.Name} (PID: {report.Process.Id})");
            Console.WriteLine($"  Memory usage: {report.Process.Memory:N0} bytes");
            Console.WriteLine($"  Framework: {report.Application.Framework ?? "Unknown"}");
        }

        /// <summary>
        /// Demonstrates environment-based configuration
        /// Critical for applications that run in different environments (dev, test, prod)
        /// </summary>
        static void DemonstrateEnvironmentBasedConfig()
        {
            // Check for environment-specific settings
            string environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "Development";
            string dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION") ?? "localhost";
            string logLevel = Environment.GetEnvironmentVariable("LOG_LEVEL") ?? "Information";
            
            Console.WriteLine($"Current environment: {environment}");
            Console.WriteLine($"Database connection: {dbConnection}");
            Console.WriteLine($"Log level: {logLevel}");
            
            // Feature switches based on environment
            bool isDevelopment = environment.Equals("Development", StringComparison.OrdinalIgnoreCase);
            bool isProduction = environment.Equals("Production", StringComparison.OrdinalIgnoreCase);
            
            if (isDevelopment)
            {
                AppContext.SetSwitch("MyApp.DetailedErrors", true);
                AppContext.SetSwitch("MyApp.PerformanceMetrics", true);
                Console.WriteLine("Development mode: Detailed errors and metrics enabled");
            }
            else if (isProduction)
            {
                AppContext.SetSwitch("MyApp.DetailedErrors", false);
                AppContext.SetSwitch("MyApp.PerformanceMetrics", false);
                Console.WriteLine("Production mode: Error details and metrics disabled for security");
            }
        }

        /// <summary>
        /// Demonstrates integration with external tools
        /// Common in build systems, deployment scripts, and automation
        /// </summary>
        static void DemonstrateExternalToolIntegration()
        {
            Console.WriteLine("External tool integration examples:");
            
            // Example 1: Version control integration
            Console.WriteLine("\n1. Version Control Integration:");
            CheckGitRepository();
            
            // Example 2: File system operations
            Console.WriteLine("\n2. File System Operations:");
            DemonstrateFileOperations();
            
            // Example 3: System information gathering
            Console.WriteLine("\n3. System Information Gathering:");
            GatherSystemInfo();
        }
        /// <summary>
        /// Checks if the current directory is a Git repository
        /// Common pattern in build and deployment scripts
        /// </summary>
        static void CheckGitRepository()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = "rev-parse --git-dir",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process? process = Process.Start(psi))
                {
                    if (process == null)
                    {
                        Console.WriteLine("Failed to start git process");
                        return;
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    string errors = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    
                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine("Git repository detected");
                        // Could get branch, commit info, etc.
                    }
                    else
                    {
                        Console.WriteLine("Not a Git repository or Git not available");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Git check failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates file operations using Environment paths
        /// Shows how to work with files in a cross-platform way
        /// </summary>
        static void DemonstrateFileOperations()
        {
            try
            {
                string tempDir = Path.GetTempPath();
                string demoFile = Path.Combine(tempDir, $"demo_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                
                // Write some data
                File.WriteAllText(demoFile, $"Demo file created at {DateTime.Now}\nApplication: {AppContext.BaseDirectory}");
                
                Console.WriteLine($"Created demo file: {demoFile}");
                Console.WriteLine($"File size: {new FileInfo(demoFile).Length} bytes");
                
                // Clean up
                File.Delete(demoFile);
                Console.WriteLine("Demo file cleaned up");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File operation failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Gathers comprehensive system information
        /// Useful for system monitoring and capacity planning
        /// </summary>
        static void GatherSystemInfo()
        {
            Console.WriteLine("System Information Summary:");
            Console.WriteLine($"  Platform: {Environment.OSVersion.Platform}");
            Console.WriteLine($"  Architecture: {(Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit")}");
            Console.WriteLine($"  Processors: {Environment.ProcessorCount}");
            Console.WriteLine($"  .NET Version: {Environment.Version}");
            Console.WriteLine($"  Current Directory: {Environment.CurrentDirectory}");
            Console.WriteLine($"  System Directory: {Environment.SystemDirectory}");
            Console.WriteLine($"  Uptime: {TimeSpan.FromMilliseconds(Environment.TickCount):dd\\.hh\\:mm\\:ss}");
            
            // Memory information
            long workingSet = Environment.WorkingSet;
            Console.WriteLine($"  Working Set: {workingSet:N0} bytes ({workingSet / 1024.0 / 1024.0:F1} MB)");
        }

}