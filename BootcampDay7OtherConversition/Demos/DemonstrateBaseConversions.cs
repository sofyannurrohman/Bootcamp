namespace BootcampDay7.Demo;

public class DemoClassDBC
{

    public static void DemonstrateBaseConversions()
    {
        Console.WriteLine("3. BASE CONVERSIONS - BEYOND DECIMAL NUMBERS");
        Console.WriteLine("=============================================");

        // Convert provides overloads for parsing numbers in different bases
        // Essential for low-level programming, embedded systems, and data analysis
        // Supports bases 2, 8, 10, and 16

        Console.WriteLine("Parsing numbers in different bases:");

        // Hexadecimal (base 16) - the programmer's friend
        Console.WriteLine("\nHexadecimal conversions (base 16):");
        string[] hexValues = { "FF", "1E", "A0", "DEADBEEF" };

        foreach (string hex in hexValues)
        {
            try
            {
                if (hex.Length <= 8) // Fits in int
                {
                    int fromHex = Convert.ToInt32(hex, 16);
                    Console.WriteLine($"  0x{hex} -> {fromHex} (decimal)");
                }
                else // Use long for larger values
                {
                    long fromHexLong = Convert.ToInt64(hex, 16);
                    Console.WriteLine($"  0x{hex} -> {fromHexLong} (decimal, long)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  0x{hex} -> Error: {ex.Message}");
            }
        }

        // Binary (base 2) - understanding the machine
        Console.WriteLine("\nBinary conversions (base 2):");
        string[] binaryValues = { "1010", "11110000", "10101010" };

        foreach (string binary in binaryValues)
        {
            int fromBinary = Convert.ToInt32(binary, 2);
            Console.WriteLine($"  {binary}₂ -> {fromBinary} (decimal)");
        }

        // Octal (base 8) - Unix permissions and more
        Console.WriteLine("\nOctal conversions (base 8):");
        string[] octalValues = { "755", "644", "777" };

        foreach (string octal in octalValues)
        {
            int fromOctal = Convert.ToInt32(octal, 8);
            Console.WriteLine($"  {octal}₈ -> {fromOctal} (decimal)");
        }

        // Converting decimal back to different bases
        Console.WriteLine("\nConverting decimal to different bases:");
        int[] decimalValues = { 255, 170, 15 };

        Console.WriteLine("Decimal  Hex   Binary     Octal");
        Console.WriteLine("-------  ---   -------    -----");

        foreach (int value in decimalValues)
        {
            string toHex = Convert.ToString(value, 16).ToUpper();
            string toBinary = Convert.ToString(value, 2);
            string toOctal = Convert.ToString(value, 8);

            Console.WriteLine($"{value,-7}  {toHex,-4}  {toBinary,-9}  {toOctal}");
        }

        // Real-world application: RGB color values
        Console.WriteLine("\nPractical example - RGB color processing:");
        string hexColor = "#FF6A7F"; // Remove the # prefix for parsing
        string cleanHex = hexColor.TrimStart('#');

        // Parse RGB components
        int red = Convert.ToInt32(cleanHex.Substring(0, 2), 16);
        int green = Convert.ToInt32(cleanHex.Substring(2, 2), 16);
        int blue = Convert.ToInt32(cleanHex.Substring(4, 2), 16);

        Console.WriteLine($"  Color {hexColor}:");
        Console.WriteLine($"    Red:   {cleanHex.Substring(0, 2)} -> {red}");
        Console.WriteLine($"    Green: {cleanHex.Substring(2, 2)} -> {green}");
        Console.WriteLine($"    Blue:  {cleanHex.Substring(4, 2)} -> {blue}");

        // Unix file permissions example
        Console.WriteLine("\nUnix file permissions (octal notation):");
        var permissions = new[] {
                ("644", "rw-r--r--", "Owner: read/write, Group: read, Others: read"),
                ("755", "rwxr-xr-x", "Owner: full, Group: read/execute, Others: read/execute"),
                ("777", "rwxrwxrwx", "Everyone: full permissions")
            };

        foreach (var (octal, symbolic, description) in permissions)
        {
            int decimal_perm = Convert.ToInt32(octal, 8);
            Console.WriteLine($"  {octal} (octal) = {decimal_perm} (decimal) = {symbolic} ({description})");
        }

        Console.WriteLine();
    }
}