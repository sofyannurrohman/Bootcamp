namespace Entities;
public class FileProcessor
{
    public void ProcessFile(string filePath)
    {
        Console.WriteLine($"Processing file: '{filePath}'");
        FileStream? fileStream = null;
        StreamReader? reader = null;

        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty", nameof(filePath));

            // Simulate different file conditions for demonstration
            if (filePath == "nonexistent.txt")
                throw new FileNotFoundException($"Could not find file: {filePath}");

            if (filePath == "corrupted.txt")
                throw new InvalidDataException("File appears to be corrupted or unreadable");

            // Simulate successful file processing
            Console.WriteLine("  ✓ File validation passed");
            Console.WriteLine("  ✓ File opened successfully");
            Console.WriteLine("  ✓ Data processed and validated");
            Console.WriteLine("  ✓ Processing completed successfully");

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  ✗ Input Validation Error: {ex.Message}");
            LogError("ARGUMENT_ERROR", ex);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"  ✗ File System Error: {ex.Message}");
            LogError("FILE_NOT_FOUND", ex);
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine($"  ✗ Data Processing Error: {ex.Message}");
            LogError("DATA_CORRUPTION", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"  ✗ Access Denied: {ex.Message}");
            LogError("ACCESS_DENIED", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Unexpected Error: {ex.Message}");
            LogError("UNEXPECTED_ERROR", ex);
            // In production, you might want to rethrow unexpected exceptions
            // throw;
        }
        finally
        {
            // Resource cleanup - this ALWAYS executes
            Console.WriteLine("  → Performing cleanup operations...");

            // Dispose resources in reverse order of acquisition
            reader?.Dispose();
            fileStream?.Dispose();

            Console.WriteLine("  → Resource cleanup completed");
        }

        Console.WriteLine(); // Add spacing between file processing attempts
    }

    private void LogError(string errorType, Exception ex)
    {
        // In a real application, this would write to a logging framework
        // like Serilog, NLog, or Microsoft.Extensions.Logging
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine($"  → [LOG] {timestamp} - {errorType}: {ex.Message}");

        // Could also log stack trace for debugging
        // Console.WriteLine($"  → [LOG] Stack Trace: {ex.StackTrace}");
    }

    // Alternative TryProcess method - no exceptions thrown
    // Returns success/failure and provides error details via out parameter
    public bool TryProcessFile(string filePath, out string? errorMessage)
    {
        errorMessage = null;

        try
        {
            ProcessFile(filePath);
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return false;
        }
    }
}