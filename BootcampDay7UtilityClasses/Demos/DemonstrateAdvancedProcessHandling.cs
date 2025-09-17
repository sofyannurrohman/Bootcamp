using System.Diagnostics;

namespace BootcampDay7.Demo;
/// <summary>
/// Demonstrates the advanced process pattern with real examples
/// Shows how to properly handle complex command execution scenarios
/// </summary>
public class DemoClassDAPH{
public static void DemonstrateAdvancedProcessHandling()
    {
        Console.WriteLine("\n6. Advanced Process Handling with Proper Stream Management:");

        // Test with a command that produces both output and potentially errors
        Console.WriteLine("Running system information command with advanced handling:");

        var (output, errors) = RunAdvancedProcess("cmd.exe", "/c systeminfo");

        if (!string.IsNullOrEmpty(output))
        {
            // Show first few lines of output
            string[] lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int linesToShow = Math.Min(8, lines.Length);

            Console.WriteLine("System Information (first 8 lines):");
            for (int i = 0; i < linesToShow; i++)
            {
                Console.WriteLine($"  {lines[i].Trim()}");
            }

            if (lines.Length > 8)
            {
                Console.WriteLine($"  ... and {lines.Length - 8} more lines");
            }
        }

        if (!string.IsNullOrEmpty(errors))
        {
            Console.WriteLine($"Errors encountered: {errors}");
        }

        // Demonstrate timeout handling
        Console.WriteLine("\nDemonstrating process timeout handling:");
        DemonstrateProcessTimeout();
    }

        /// <summary>
        /// Shows how to handle process timeouts gracefully
        /// Critical for production applications that can't hang indefinitely
        /// </summary>
        static void DemonstrateProcessTimeout()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c timeout /t 3 >nul", // Command that takes 3 seconds
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process? process = Process.Start(psi))
                {
                    if (process == null)
                    {
                        Console.WriteLine("Failed to start timeout demo process");
                        return;
                    }

                    Console.WriteLine("Starting process with 2-second timeout (process needs 3 seconds)...");
                    bool finished = process.WaitForExit(2000); // 2 second timeout
                    
                    if (finished)
                    {
                        Console.WriteLine($"Process completed within timeout. Exit code: {process.ExitCode}");
                    }
                    else
                    {
                        Console.WriteLine("Process exceeded timeout and will be terminated");
                        process.Kill();
                        process.WaitForExit(); // Wait for kill to complete
                        Console.WriteLine("Process terminated successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Timeout demonstration failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Lists currently running processes
        /// Useful for system monitoring and diagnostics
        /// </summary>
        static void ListRunningProcesses()
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                Console.WriteLine($"Found {processes.Length} running processes. Showing first 10:");
                
                // Sort by memory usage and show top 10
                var topProcesses = processes
                    .Where(p => !p.HasExited)
                    .OrderByDescending(p => 
                    {
                        try { return p.WorkingSet64; }
                        catch { return 0; }
                    })
                    .Take(10);

                Console.WriteLine("Top 10 processes by memory usage:");
                Console.WriteLine("PID\tName\t\t\tMemory (MB)");
                Console.WriteLine("".PadRight(50, '-'));

                foreach (Process proc in topProcesses)
                {
                    try
                    {
                        long memoryMB = proc.WorkingSet64 / 1024 / 1024;
                        string name = proc.ProcessName.Length > 15 
                            ? proc.ProcessName.Substring(0, 12) + "..." 
                            : proc.ProcessName;
                        Console.WriteLine($"{proc.Id}\t{name.PadRight(15)}\t\t{memoryMB}");
                    }
                    catch
                    {
                        // Some processes might not be accessible
                        Console.WriteLine($"{proc.Id}\t{proc.ProcessName.PadRight(15)}\t\tN/A");
                    }
                }

                // Clean up
                foreach (Process proc in processes)
                {
                    proc.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing processes: {ex.Message}");
            }
        }
    /// <summary>
        /// Advanced process execution with proper handling of interleaved output and error streams
        /// This is the production-ready pattern mentioned in the material for handling both output and error streams
        /// </summary>
        static (string output, string errors) RunAdvancedProcess(string exePath, string args = "")
        {
            try
            {
                using var process = Process.Start(new ProcessStartInfo(exePath, args)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false, // Essential for stream redirection
                    CreateNoWindow = true
                })!;

                if (process == null)
                {
                    return ("", "Failed to start process");
                }

                var errors = new System.Text.StringBuilder();
                
                // Handle error stream asynchronously to prevent interleaving issues
                process.ErrorDataReceived += (sender, errorArgs) =>
                {
                    if (errorArgs.Data != null) 
                    {
                        lock (errors) // Thread-safe access to StringBuilder
                        {
                            errors.AppendLine(errorArgs.Data);
                        }
                    }
                };
                
                process.BeginErrorReadLine(); // Start asynchronous read for errors

                // Read output synchronously
                string output = process.StandardOutput.ReadToEnd();
                
                process.WaitForExit(); // Wait for the process to exit
                
                return (output, errors.ToString());
            }
            catch (Exception ex)
            {
                return ("", $"Process execution failed: {ex.Message}");
            }
        }

}