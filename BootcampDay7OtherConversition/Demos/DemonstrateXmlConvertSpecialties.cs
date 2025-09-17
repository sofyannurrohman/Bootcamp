using System.Xml;

namespace BootcampDay7.Demo;
public class DemoClassXCS{
    
public static void DemonstrateXmlConvertSpecialties()
    {
        Console.WriteLine("6. XMLCONVERT - XML STANDARDS COMPLIANCE");
        Console.WriteLine("=========================================");

        // XmlConvert ensures data conforms to XML/W3C standards
        // Different from standard .NET formatting in crucial ways
        // Essential for XML serialization, SOAP services, and web APIs

        Console.WriteLine("Why XmlConvert is different from standard ToString():");
        Console.WriteLine("- XML requires lowercase boolean values ('true'/'false' not 'True'/'False')");
        Console.WriteLine("- Culture-invariant by design (essential for data exchange)");
        Console.WriteLine("- Handles XML-specific datetime formats and timezone info");
        Console.WriteLine();

        // Boolean differences - this trips up many developers
        Console.WriteLine("Boolean conversion differences:");
        bool flag = true;

        Console.WriteLine($"  Standard ToString(): {flag.ToString()}");
        Console.WriteLine($"  XmlConvert.ToString(): {XmlConvert.ToString(flag)}");

        string xmlBool = XmlConvert.ToString(flag);
        bool parsedBool = XmlConvert.ToBoolean(xmlBool);
        Console.WriteLine($"  Round-trip test: {flag} -> \"{xmlBool}\" -> {parsedBool}");

        // DateTime handling - the real strength of XmlConvert
        Console.WriteLine("\nDateTime XML serialization modes:");
        DateTime now = DateTime.Now;
        DateTime utcNow = DateTime.UtcNow;
        DateTime unspecified = new DateTime(2024, 5, 29, 14, 30, 0, DateTimeKind.Unspecified);

        // Local mode - includes timezone offset
        string localXml = XmlConvert.ToString(now, XmlDateTimeSerializationMode.Local);
        Console.WriteLine($"  Local time with offset: {localXml}");

        // UTC mode - converts to UTC and adds 'Z'
        string utcXml = XmlConvert.ToString(utcNow, XmlDateTimeSerializationMode.Utc);
        Console.WriteLine($"  UTC time: {utcXml}");

        // Unspecified - strips timezone info
        string unspecifiedXml = XmlConvert.ToString(unspecified, XmlDateTimeSerializationMode.Unspecified);
        Console.WriteLine($"  Unspecified: {unspecifiedXml}");

        // RoundtripKind - preserves original DateTimeKind (safest option)
        string roundtripXml = XmlConvert.ToString(now, XmlDateTimeSerializationMode.RoundtripKind);
        Console.WriteLine($"  Roundtrip kind: {roundtripXml}");

        // Numeric conversions - culture invariant
        Console.WriteLine("\nNumeric XML conversions (always culture-invariant):");
        decimal price = 1234.56m;
        double scientific = 1.23e-4;
        float singlePrecision = 456.789f;

        Console.WriteLine($"  Decimal: {XmlConvert.ToString(price)}");
        Console.WriteLine($"  Scientific notation: {XmlConvert.ToString(scientific)}");
        Console.WriteLine($"  Float: {XmlConvert.ToString(singlePrecision)}");

        // Special floating-point values
        Console.WriteLine("\nSpecial numeric values (IEEE 754 compliance):");
        double[] specialValues = {
            double.PositiveInfinity,
            double.NegativeInfinity,
            double.NaN
        };

        foreach (double value in specialValues)
        {
            string xmlValue = XmlConvert.ToString(value);
            Console.WriteLine($"  {value} -> \"{xmlValue}\"");
        }

        // Parsing various XML date formats
        Console.WriteLine("\nParsing XML date strings:");
        string[] xmlDates = {
            "2024-05-29T14:30:00Z",           // UTC format
            "2024-05-29T14:30:00+07:00",     // With timezone offset
            "2024-05-29T14:30:00.123Z",      // With milliseconds
            "2024-05-29",                     // Date only
            "14:30:00"                        // Time only
        };

        foreach (string xmlDate in xmlDates)
        {
            try
            {
                DateTime parsed = XmlConvert.ToDateTime(xmlDate, XmlDateTimeSerializationMode.RoundtripKind);
                Console.WriteLine($"  \"{xmlDate}\" -> {parsed:yyyy-MM-dd HH:mm:ss.fff} (Kind: {parsed.Kind})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  \"{xmlDate}\" -> Parse error: {ex.Message}");
            }
        }

        // Demonstrating culture independence
        Console.WriteLine("\nCulture independence demonstration:");
        decimal testAmount = 1234.56m;

        // These will be the same regardless of system culture
        string xmlFormat = XmlConvert.ToString(testAmount);
        string invariantFormat = testAmount.ToString(System.Globalization.CultureInfo.InvariantCulture);

        Console.WriteLine($"  XmlConvert: {xmlFormat}");
        Console.WriteLine($"  InvariantCulture: {invariantFormat}");
        Console.WriteLine($"  Same result: {xmlFormat == invariantFormat}");

        Console.WriteLine();
    }
}