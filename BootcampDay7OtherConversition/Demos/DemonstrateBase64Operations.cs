using System.Text;

namespace BootcampDay7.Demo;

public class DemoClassB64
{

    public static void DemonstrateBase64Operations()
    {
        Console.WriteLine("5. BASE64 OPERATIONS - BINARY DATA AS TEXT");
        Console.WriteLine("===========================================");

        // Base64 encoding represents binary data in ASCII text format
        // Crucial for email attachments, JSON APIs, data URLs, and embedded content
        // Every web developer needs to understand this!

        Console.WriteLine("Why Base64 matters:");
        Console.WriteLine("- Embeds binary data in text-based formats (JSON, XML, HTML)");
        Console.WriteLine("- Safe for transmission over text-based protocols");
        Console.WriteLine("- Used in data URIs, email attachments, and web APIs");
        Console.WriteLine();

        // Basic text encoding
        Console.WriteLine("Basic text encoding example:");
        string originalText = "Hello, World! 🌍 Special chars: äöü";
        byte[] textBytes = Encoding.UTF8.GetBytes(originalText);
        string base64Text = Convert.ToBase64String(textBytes);

        Console.WriteLine($"  Original: \"{originalText}\"");
        Console.WriteLine($"  UTF-8 bytes: [{textBytes.Length} bytes]");
        Console.WriteLine($"  Base64: {base64Text}");

        // Decode back to verify
        byte[] decodedBytes = Convert.FromBase64String(base64Text);
        string decodedText = Encoding.UTF8.GetString(decodedBytes);
        Console.WriteLine($"  Decoded: \"{decodedText}\"");
        Console.WriteLine($"  Match: {originalText == decodedText}");

        // Binary data example
        Console.WriteLine("\nBinary data encoding:");
        byte[] binaryData = { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46 };
        string binaryBase64 = Convert.ToBase64String(binaryData);

        Console.WriteLine($"  Binary: [{string.Join(", ", binaryData.Select(b => $"0x{b:X2}"))}]");
        Console.WriteLine($"  Base64: {binaryBase64}");

        // Simulate file embedding (common in web development)
        Console.WriteLine("\nWeb development example - Data URIs:");

        // Simulate a tiny PNG image (just the header bytes)
        byte[] pngHeader = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        string pngBase64 = Convert.ToBase64String(pngHeader);

        Console.WriteLine($"  PNG header bytes: {pngBase64}");
        Console.WriteLine($"  Data URI: data:image/png;base64,{pngBase64}");

        // Line breaks for readability (useful for large data)
        Console.WriteLine("\nFormatting options for large data:");
        byte[] largeData = new byte[100];
        new Random(12345).NextBytes(largeData); // Consistent seed for demo

        string compactBase64 = Convert.ToBase64String(largeData);
        string formattedBase64 = Convert.ToBase64String(largeData, Base64FormattingOptions.InsertLineBreaks);

        Console.WriteLine($"  Compact ({compactBase64.Length} chars):");
        Console.WriteLine($"    {compactBase64}");
        Console.WriteLine($"  With line breaks:");
        Console.WriteLine($"    {formattedBase64}");

        // Real-world scenario: API token encoding
        Console.WriteLine("\nAPI authentication example:");
        string apiKey = "user123";
        string apiSecret = "secret456";
        string credentials = $"{apiKey}:{apiSecret}";

        byte[] credentialBytes = Encoding.UTF8.GetBytes(credentials);
        string encodedCredentials = Convert.ToBase64String(credentialBytes);

        Console.WriteLine($"  Credentials: {credentials}");
        Console.WriteLine($"  Base64: {encodedCredentials}");
        Console.WriteLine($"  HTTP Header: Authorization: Basic {encodedCredentials}");

        // Handling invalid Base64 strings
        Console.WriteLine("\nError handling:");
        string[] invalidBase64 = { "invalid!", "almost=valid", "SGVsbG8gV29ybGQ!" };

        foreach (string invalid in invalidBase64)
        {
            try
            {
                byte[] result = Convert.FromBase64String(invalid);
                Console.WriteLine($"  \"{invalid}\" -> Valid ({result.Length} bytes)");
            }
            catch (FormatException)
            {
                Console.WriteLine($"  \"{invalid}\" -> Invalid Base64 format");
            }
        }

        Console.WriteLine();
    }
}