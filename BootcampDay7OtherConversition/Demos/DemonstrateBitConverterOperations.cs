using System.Text;

namespace BootcampDay7.Demo;

public class DemoClassBCO
{

    public static void DemonstrateBitConverterOperations()
    {
        Console.WriteLine("8. BITCONVERTER - RAW BINARY DATA MANIPULATION");
        Console.WriteLine("===============================================");

        // BitConverter works at the byte level - the lowest level of data representation
        // Essential for: network protocols, file formats, serialization, cryptography
        // Understanding endianness is crucial for cross-platform compatibility

        Console.WriteLine("Understanding BitConverter:");
        Console.WriteLine("- Converts primitive types to/from byte arrays");
        Console.WriteLine("- Platform-dependent endianness (byte order)");
        Console.WriteLine("- Essential for binary protocols and file formats");
        Console.WriteLine($"- This system is: {(BitConverter.IsLittleEndian ? "Little Endian" : "Big Endian")}");
        Console.WriteLine();

        // Basic type to bytes conversion
        Console.WriteLine("Converting values to byte arrays:");

        var testValues = new (string name, object value, byte[] bytes)[]
        {
                ("bool true", true, BitConverter.GetBytes(true)),
                ("int 305419896", 305419896, BitConverter.GetBytes(305419896)),  // 0x12345678
                ("float π", (float)Math.PI, BitConverter.GetBytes((float)Math.PI)),
                ("double e", Math.E, BitConverter.GetBytes(Math.E)),
                ("long max", long.MaxValue, BitConverter.GetBytes(long.MaxValue))
        };

        Console.WriteLine("Type/Value           Bytes (hex)");
        Console.WriteLine("----------           -----------");

        foreach (var (name, value, bytes) in testValues)
        {
            string hexBytes = string.Join(" ", bytes.Take(8).Select(b => $"{b:X2}"));
            if (bytes.Length > 8) hexBytes += "...";
            Console.WriteLine($"{name,-19}  {hexBytes}");
        }

        // Converting back from bytes
        Console.WriteLine("\nConverting byte arrays back to values:");

        // Use the same test data
        int intValue = 305419896;
        float floatValue = (float)Math.PI;
        double doubleValue = Math.E;
        bool boolValue = true;

        byte[] intBytes = BitConverter.GetBytes(intValue);
        byte[] floatBytes = BitConverter.GetBytes(floatValue);
        byte[] doubleBytes = BitConverter.GetBytes(doubleValue);
        byte[] boolBytes = BitConverter.GetBytes(boolValue);

        // Reconstruct the values
        int recoveredInt = BitConverter.ToInt32(intBytes, 0);
        float recoveredFloat = BitConverter.ToSingle(floatBytes, 0);
        double recoveredDouble = BitConverter.ToDouble(doubleBytes, 0);
        bool recoveredBool = BitConverter.ToBoolean(boolBytes, 0);

        Console.WriteLine($"  int: {intValue} -> {recoveredInt} (match: {recoveredInt == intValue})");
        Console.WriteLine($"  float: {floatValue:F6} -> {recoveredFloat:F6} (match: {recoveredFloat == floatValue})");
        Console.WriteLine($"  double: {doubleValue:F15} -> {recoveredDouble:F15} (match: {recoveredDouble == doubleValue})");
        Console.WriteLine($"  bool: {boolValue} -> {recoveredBool} (match: {recoveredBool == boolValue})");

        // Endianness demonstration
        Console.WriteLine("\nEndianness demonstration:");
        int testInt = 0x12345678;
        byte[] testBytes = BitConverter.GetBytes(testInt);

        Console.WriteLine($"  Value: 0x{testInt:X8}");
        Console.WriteLine($"  Bytes: [{string.Join(", ", testBytes.Select(b => $"0x{b:X2}"))}]");
        Console.WriteLine($"  Order: {(BitConverter.IsLittleEndian ? "Least significant byte first" : "Most significant byte first")}");

        // Working with DateTime - requires special handling
        Console.WriteLine("\nDateTime binary serialization:");
        DateTime timestamp = new DateTime(2024, 5, 29, 14, 30, 45, DateTimeKind.Utc);

        // DateTime to binary using ToBinary()
        long binaryTime = timestamp.ToBinary();
        byte[] timeBytes = BitConverter.GetBytes(binaryTime);

        Console.WriteLine($"  Original: {timestamp:yyyy-MM-dd HH:mm:ss} {timestamp.Kind}");
        Console.WriteLine($"  Binary: {binaryTime}");
        Console.WriteLine($"  Bytes: [{string.Join(", ", timeBytes.Take(8).Select(b => $"0x{b:X2}"))}]");

        // Recover the DateTime
        long recoveredBinary = BitConverter.ToInt64(timeBytes, 0);
        DateTime recoveredTime = DateTime.FromBinary(recoveredBinary);
        Console.WriteLine($"  Recovered: {recoveredTime:yyyy-MM-dd HH:mm:ss} {recoveredTime.Kind}");

        // Decimal requires special handling too
        Console.WriteLine("\nDecimal binary representation:");
        decimal testDecimal = 123.456m;

        // Decimal uses int[] representation
        int[] decimalBits = decimal.GetBits(testDecimal);
        Console.WriteLine($"  Decimal: {testDecimal}");
        Console.WriteLine($"  Bits: [{string.Join(", ", decimalBits)}]");

        // Convert each int to bytes
        Console.Write("  Bytes: [");
        for (int i = 0; i < decimalBits.Length; i++)
        {
            byte[] intAsBytes = BitConverter.GetBytes(decimalBits[i]);
            Console.Write(string.Join(", ", intAsBytes.Select(b => $"0x{b:X2}")));
            if (i < decimalBits.Length - 1) Console.Write(", ");
        }
        Console.WriteLine("]");

        // Practical example: Network packet structure
        Console.WriteLine("\nPractical example - Binary protocol message:");

        // Create a simple message: [Type][ID][Timestamp][DataLength][Data]
        byte messageType = 0x01;
        uint messageId = 0x12345678;
        long timestamp_binary = DateTime.UtcNow.ToBinary();
        string messageData = "Hello, Binary World!";
        byte[] dataBytes = Encoding.UTF8.GetBytes(messageData);

        // Build the packet
        List<byte> packet = new List<byte>();
        packet.Add(messageType);                                    // 1 byte
        packet.AddRange(BitConverter.GetBytes(messageId));          // 4 bytes
        packet.AddRange(BitConverter.GetBytes(timestamp_binary));   // 8 bytes
        packet.AddRange(BitConverter.GetBytes((uint)dataBytes.Length)); // 4 bytes
        packet.AddRange(dataBytes);                                 // variable

        Console.WriteLine($"  Built packet: {packet.Count} bytes");
        Console.WriteLine($"  Header bytes: [{string.Join(" ", packet.Take(17).Select(b => $"{b:X2}"))}]");

        // Parse the packet
        byte[] packetBytes = packet.ToArray();
        int offset = 0;

        byte parsedType = packetBytes[offset++];
        uint parsedId = BitConverter.ToUInt32(packetBytes, offset); offset += 4;
        long parsedTimestamp = BitConverter.ToInt64(packetBytes, offset); offset += 8;
        uint parsedDataLength = BitConverter.ToUInt32(packetBytes, offset); offset += 4;
        string parsedData = Encoding.UTF8.GetString(packetBytes, offset, (int)parsedDataLength);

        Console.WriteLine($"  Parsed type: 0x{parsedType:X2}");
        Console.WriteLine($"  Parsed ID: 0x{parsedId:X8}");
        Console.WriteLine($"  Parsed timestamp: {DateTime.FromBinary(parsedTimestamp):yyyy-MM-dd HH:mm:ss.fff}");
        Console.WriteLine($"  Parsed data: \"{parsedData}\" ({parsedDataLength} bytes)");

        Console.WriteLine();
    }
}