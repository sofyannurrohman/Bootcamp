
using System.Text;
using System.Xml;

namespace BootcampDay7.Demo;

public class DemoClassAS
{

    public void DemonstrateAdvancedScenarios()
    {
        Console.WriteLine("9. ADVANCED SCENARIOS - PROFESSIONAL APPLICATIONS");
        Console.WriteLine("==================================================");

        // These scenarios show how all the conversion mechanisms work together
        // in real enterprise software development situations

        // Scenario 1: Multi-format configuration system
        Console.WriteLine("Scenario 1: Enterprise configuration processor");
        Console.WriteLine("Processing configuration with mixed data types and formats:");

        var advancedConfig = new Dictionary<string, (string value, string format, Type targetType)>
            {
                {"server_port", ("8080", "decimal", typeof(int))},
                {"timeout_seconds", ("30.5", "decimal", typeof(double))},
                {"enable_ssl", ("true", "boolean", typeof(bool))},
                {"max_connections", ("FF", "hex", typeof(int))},
                {"buffer_size", ("10000000", "binary", typeof(int))}, // 128 in binary
                {"api_credentials", ("dXNlcjEyMzpzZWNyZXQ0NTY=", "base64", typeof(string))},
                {"maintenance_window", ("2024-05-29T02:00:00Z", "xml_datetime", typeof(DateTime))},
                {"license_key", ("12345678-ABCD-EFGH-IJKL-MNOPQRSTUVWX", "guid", typeof(Guid))},
                {"cpu_affinity", ("1010", "binary_flags", typeof(int))}
            };

        foreach (var config in advancedConfig)
        {
            Console.WriteLine($"\n  {config.Key} ({config.Value.format}): \"{config.Value.value}\"");

            try
            {
                object result = config.Value.format switch
                {
                    "decimal" => Convert.ChangeType(config.Value.value, config.Value.targetType),
                    "boolean" => Convert.ToBoolean(config.Value.value),
                    "hex" => Convert.ToInt32(config.Value.value, 16),
                    "binary" => Convert.ToInt32(config.Value.value, 2),
                    "base64" => ProcessBase64Config(config.Value.value),
                    "xml_datetime" => XmlConvert.ToDateTime(config.Value.value, XmlDateTimeSerializationMode.RoundtripKind),
                    "guid" => Guid.Parse(config.Value.value),
                    "binary_flags" => ProcessBinaryFlags(config.Value.value),
                    _ => config.Value.value
                };

                Console.WriteLine($"    -> {config.Value.targetType.Name}: {result}");

                // Add interpretation for specific fields
                if (config.Key == "cpu_affinity")
                {
                    Console.WriteLine($"    -> CPU cores enabled: {string.Join(", ", GetEnabledCores((int)result))}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    -> Error: {ex.Message}");
            }
        }

        // Scenario 2: Binary protocol with multiple data types
        Console.WriteLine("\n\nScenario 2: Complex binary protocol implementation");
        Console.WriteLine("Building and parsing a structured binary message:");

        // Define a complex message structure:
        // [Magic(4)][Version(2)][Type(1)][Flags(1)][Length(4)][Timestamp(8)][User ID(16)][Data(variable)]

        uint magic = 0xDEADBEEF;
        ushort version = 0x0102; // v1.2
        byte messageType = 0x05;
        byte flags = 0b10110001; // Binary flags
        DateTime timestamp = DateTime.UtcNow;
        Guid userId = Guid.NewGuid();
        string payload = "Complex binary message payload with unicode: 你好世界! 🌍";

        // Build the message
        byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);
        uint totalLength = (uint)(4 + 2 + 1 + 1 + 4 + 8 + 16 + payloadBytes.Length);

        List<byte> complexMessage = new List<byte>();
        complexMessage.AddRange(BitConverter.GetBytes(magic));
        complexMessage.AddRange(BitConverter.GetBytes(version));
        complexMessage.Add(messageType);
        complexMessage.Add(flags);
        complexMessage.AddRange(BitConverter.GetBytes(totalLength));
        complexMessage.AddRange(BitConverter.GetBytes(timestamp.ToBinary()));
        complexMessage.AddRange(userId.ToByteArray());
        complexMessage.AddRange(payloadBytes);

        Console.WriteLine($"  Built message: {complexMessage.Count} bytes");
        Console.WriteLine($"  Magic + Version: {string.Join(" ", complexMessage.Take(6).Select(b => $"{b:X2}"))}");

        // Parse the message back
        byte[] messageBytes = complexMessage.ToArray();
        int parseOffset = 0;

        uint parsedMagic = BitConverter.ToUInt32(messageBytes, parseOffset); parseOffset += 4;
        ushort parsedVersion = BitConverter.ToUInt16(messageBytes, parseOffset); parseOffset += 2;
        byte parsedType = messageBytes[parseOffset++];
        byte parsedFlags = messageBytes[parseOffset++];
        uint parsedLength = BitConverter.ToUInt32(messageBytes, parseOffset); parseOffset += 4;
        long parsedTimeBinary = BitConverter.ToInt64(messageBytes, parseOffset); parseOffset += 8;
        DateTime parsedTime = DateTime.FromBinary(parsedTimeBinary);

        byte[] userIdBytes = new byte[16];
        Array.Copy(messageBytes, parseOffset, userIdBytes, 0, 16);
        Guid parsedUserId = new Guid(userIdBytes); parseOffset += 16;

        int payloadLength = (int)(parsedLength - parseOffset);
        string parsedPayload = Encoding.UTF8.GetString(messageBytes, parseOffset, payloadLength);

        Console.WriteLine($"  Parsed results:");
        Console.WriteLine($"    Magic: 0x{parsedMagic:X8} (valid: {parsedMagic == magic})");
        Console.WriteLine($"    Version: {parsedVersion >> 8}.{parsedVersion & 0xFF}");
        Console.WriteLine($"    Type: 0x{parsedType:X2}");
        Console.WriteLine($"    Flags: 0b{Convert.ToString(parsedFlags, 2).PadLeft(8, '0')}");
        Console.WriteLine($"    Length: {parsedLength} bytes");
        Console.WriteLine($"    Timestamp: {parsedTime:yyyy-MM-dd HH:mm:ss.fff} UTC");
        Console.WriteLine($"    User ID: {parsedUserId}");
        Console.WriteLine($"    Payload: \"{parsedPayload}\"");

        // Scenario 3: Data export with multiple formats
        Console.WriteLine("\n\nScenario 3: Multi-format data export system");
        Console.WriteLine("Exporting the same data in different formats:");

        var exportData = new[]
        {
                new { Id = 1001, Name = "Alice Johnson", Score = 95.75, Active = true, LastLogin = DateTime.Now.AddDays(-2) },
                new { Id = 1002, Name = "Bob Smith", Score = 88.25, Active = false, LastLogin = DateTime.Now.AddDays(-10) },
                new { Id = 1003, Name = "Charlie Brown", Score = 92.50, Active = true, LastLogin = DateTime.Now.AddHours(-3) }
            };

        // CSV format (machine-readable, invariant culture)
        Console.WriteLine("\nCSV Export (InvariantCulture):");
        Console.WriteLine("ID,Name,Score,Active,LastLogin");
        foreach (var record in exportData)
        {
            string csvLine = string.Join(",",
                record.Id.ToString(),
                $"\"{record.Name}\"",
                record.Score.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                XmlConvert.ToString(record.Active),
                XmlConvert.ToString(record.LastLogin, XmlDateTimeSerializationMode.RoundtripKind)
            );
            Console.WriteLine(csvLine);
        }

        // JSON-like format with Base64 encoded metadata
        Console.WriteLine("\nJSON-like Export with Base64 metadata:");
        foreach (var record in exportData)
        {
            // Create metadata
            var metadata = new { ExportTime = DateTime.UtcNow, Version = "1.0", Source = "DemoSystem" };
            string metadataJson = $"{{\"exportTime\":\"{XmlConvert.ToString(metadata.ExportTime, XmlDateTimeSerializationMode.Utc)}\",\"version\":\"{metadata.Version}\",\"source\":\"{metadata.Source}\"}}";
            string metadataBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(metadataJson));

            Console.WriteLine($"{{");
            Console.WriteLine($"  \"id\": {record.Id},");
            Console.WriteLine($"  \"name\": \"{record.Name}\",");
            Console.WriteLine($"  \"score\": {record.Score.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)},");
            Console.WriteLine($"  \"active\": {XmlConvert.ToString(record.Active)},");
            Console.WriteLine($"  \"lastLogin\": \"{XmlConvert.ToString(record.LastLogin, XmlDateTimeSerializationMode.RoundtripKind)}\",");
            Console.WriteLine($"  \"_meta\": \"{metadataBase64}\"");
            Console.WriteLine($"}},");
        }

        // Binary format for high-performance scenarios
        Console.WriteLine("\nBinary Export (compact representation):");
        foreach (var record in exportData)
        {
            List<byte> binaryRecord = new List<byte>();
            binaryRecord.AddRange(BitConverter.GetBytes(record.Id));

            byte[] nameBytes = Encoding.UTF8.GetBytes(record.Name);
            binaryRecord.Add((byte)nameBytes.Length);
            binaryRecord.AddRange(nameBytes);

            binaryRecord.AddRange(BitConverter.GetBytes((float)record.Score));
            binaryRecord.Add((byte)(record.Active ? 1 : 0));
            binaryRecord.AddRange(BitConverter.GetBytes(record.LastLogin.ToBinary()));

            string hexDump = string.Join(" ", binaryRecord.Select(b => $"{b:X2}"));
            Console.WriteLine($"  Record {record.Id}: {binaryRecord.Count} bytes - {hexDump}");
        }

        Console.WriteLine("\nKey Insights:");
        Console.WriteLine("1. Different scenarios require different conversion approaches");
        Console.WriteLine("2. Combine multiple conversion mechanisms for complex requirements");
        Console.WriteLine("3. Always consider culture, endianness, and data integrity");
        Console.WriteLine("4. Choose the right tool: Convert, XmlConvert, BitConverter, TypeConverter");
        Console.WriteLine("5. Plan for error handling and data validation in production systems");

        Console.WriteLine();
    }

    // Helper methods for advanced scenarios
    static string ProcessBase64Config(string base64Value)
    {
        byte[] decoded = Convert.FromBase64String(base64Value);
        return Encoding.UTF8.GetString(decoded);
    }

    static int ProcessBinaryFlags(string binaryValue)
    {
        return Convert.ToInt32(binaryValue, 2);
    }

    static List<int> GetEnabledCores(int flags)
    {
        var cores = new List<int>();
        for (int i = 0; i < 32; i++)
        {
            if ((flags & (1 << i)) != 0)
            {
                cores.Add(i);
            }
        }
        return cores;
    }


}