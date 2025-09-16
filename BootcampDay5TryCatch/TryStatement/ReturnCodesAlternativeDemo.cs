namespace BootcampDay5.TryStatment;

public class TryClassRCA
{
    public static void ReturnCodesAlternativeDemo()
    {
        Console.WriteLine("10. RETURN CODES VS EXCEPTIONS COMPARISON");
        Console.WriteLine("=========================================");
        Console.WriteLine("Return codes are an alternative to exceptions for error handling.");
        Console.WriteLine("They're faster but require more manual checking and are easier to ignore.\n");

        string[] filePaths = { "valid_file.txt", "", "nonexistent.txt" };

        foreach (string path in filePaths)
        {
            Console.WriteLine($"Testing file path: '{path}'");

            // Return code approach - faster, but easy to ignore errors
            int resultCode = OpenFileWithReturnCode(path);
            Console.WriteLine($"  Return code approach: {GetResultMessage(resultCode)}");

            // Exception approach - cannot be ignored, but has performance overhead
            try
            {
                OpenFileWithException(path);
                Console.WriteLine("  Exception approach: ✓ Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Exception approach: ✗ {ex.GetType().Name}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("When to use each approach:");
        Console.WriteLine("• Return codes: Performance-critical code, expected failures");
        Console.WriteLine("• Exceptions: Truly exceptional cases, when errors cannot be ignored");
        Console.WriteLine();
    }

    // Return code approach - uses integer codes to indicate success/failure
    static int OpenFileWithReturnCode(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return -1; // Error code: Invalid file path

        if (!File.Exists(filePath))
            return -2; // Error code: File not found

        // In a real application, this would actually open the file
        return 0; // Success code
    }

    static string GetResultMessage(int code)
    {
        return code switch
        {
            0 => "✓ Success",
            -1 => "✗ Error: Invalid file path",
            -2 => "✗ Error: File not found",
            _ => "✗ Unknown error"
        };
    }

    // Exception approach - throws specific exceptions for different error conditions
    static void OpenFileWithException(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be null or empty");

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        // In a real application, this would actually open the file
    }

}