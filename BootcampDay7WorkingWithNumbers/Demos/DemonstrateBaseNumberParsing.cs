namespace BootcampDay7.Demo;

public class DemoClassDBN
{

    public static void DemonstrateBaseNumberParsing()
    {
        Console.WriteLine("2. BASE NUMBER PARSING - BINARY, OCTAL, HEXADECIMAL");
        Console.WriteLine("====================================================");

        // Working with different number bases is crucial for systems programming
        Console.WriteLine("Parsing numbers in different bases:");

        // Hexadecimal parsing - common in color codes, memory addresses
        string[] hexNumbers = { "FF", "1E", "A0", "DEADBEEF" };

        Console.WriteLine("\nHexadecimal numbers:");
        foreach (string hex in hexNumbers)
        {
            try
            {
                if (hex.Length <= 8) // Standard int range
                {
                    int hexValue = Convert.ToInt32(hex, 16);
                    Console.WriteLine($"  0x{hex} -> {hexValue} (decimal)");
                }
                else // Large hex values
                {
                    long hexValue = Convert.ToInt64(hex, 16);
                    Console.WriteLine($"  0x{hex} -> {hexValue} (decimal)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  0x{hex} -> Error: {ex.Message}");
            }
        }

        // Binary parsing - useful for flags and bit manipulation
        string[] binaryNumbers = { "1010", "11110000", "101010101010" };

        Console.WriteLine("\nBinary numbers:");
        foreach (string binary in binaryNumbers)
        {
            try
            {
                int binaryValue = Convert.ToInt32(binary, 2);
                Console.WriteLine($"  {binary} (binary) -> {binaryValue} (decimal)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  {binary} -> Error: {ex.Message}");
            }
        }

        // Octal parsing - less common but still used in Unix permissions
        string[] octalNumbers = { "755", "644", "777" };

        Console.WriteLine("\nOctal numbers (Unix permissions example):");
        foreach (string octal in octalNumbers)
        {
            int octalValue = Convert.ToInt32(octal, 8);
            Console.WriteLine($"  {octal} (octal) -> {octalValue} (decimal) -> {ConvertToPermissionString(octalValue)}");
        }

        // Converting numbers back to different bases
        Console.WriteLine("\nConverting decimal to different bases:");
        int decimalNumber = 255;

        Console.WriteLine($"  Decimal {decimalNumber}:");
        Console.WriteLine($"    Binary: {Convert.ToString(decimalNumber, 2)}");
        Console.WriteLine($"    Octal: {Convert.ToString(decimalNumber, 8)}");
        Console.WriteLine($"    Hexadecimal: {Convert.ToString(decimalNumber, 16).ToUpper()}");

        // Formatting numbers to hexadecimal with ToString
        Console.WriteLine("\nFormatting to hexadecimal:");
        int[] numbers = { 15, 255, 4095 };

        foreach (int num in numbers)
        {
            Console.WriteLine($"  {num} -> 0x{num:X} (uppercase) or 0x{num:x} (lowercase)");
        }

        Console.WriteLine();
    }
    static string ConvertToPermissionString(int octalValue)
        {
            // Convert octal permission to rwx format
            string[] permissions = { "---", "--x", "-w-", "-wx", "r--", "r-x", "rw-", "rwx" };
            
            int owner = (octalValue >> 6) & 7;
            int group = (octalValue >> 3) & 7;
            int other = octalValue & 7;
            
            return permissions[owner] + permissions[group] + permissions[other];
        }
}
