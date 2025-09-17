using System.Globalization;

namespace BootcampDay7.Demo;

public class DemoClassDDTF
{
    public static void DemonstrateDateTimeFormatting()
    {
        Console.WriteLine("6. DATETIME FORMATTING - DISPLAYING AND PARSING DATES");
        Console.WriteLine("======================================================");

        DateTime sampleDate = new DateTime(2024, 5, 29, 14, 30, 45, 123);

        // Standard format strings - influenced by OS regional settings
        Console.WriteLine("Standard formats (culture-dependent):");
        Console.WriteLine($"  Short date (d): {sampleDate:d}");
        Console.WriteLine($"  Long date (D): {sampleDate:D}");
        Console.WriteLine($"  Short time (t): {sampleDate:t}");
        Console.WriteLine($"  Long time (T): {sampleDate:T}");
        Console.WriteLine($"  Full date/time (F): {sampleDate:F}");
        Console.WriteLine($"  General (G): {sampleDate:G}");
        Console.WriteLine($"  Round-trip (O): {sampleDate:O}"); // Critical for reliable parsing!

        // Round-trip format is crucial for data storage and transmission
        Console.WriteLine("\nRound-trip format importance:");
        string roundTripString = sampleDate.ToString("O");
        DateTime parsedBack = DateTime.Parse(roundTripString);
        Console.WriteLine($"  Original: {sampleDate}");
        Console.WriteLine($"  Round-trip string: '{roundTripString}'");
        Console.WriteLine($"  Parsed back: {parsedBack}");
        Console.WriteLine($"  Values equal: {sampleDate == parsedBack}");

        // Custom format strings - essential for APIs and databases
        Console.WriteLine("\nCustom formats for different purposes:");
        Console.WriteLine($"  Database: {sampleDate:yyyy-MM-dd HH:mm:ss.fff}");
        Console.WriteLine($"  ISO 8601: {sampleDate:yyyy-MM-ddTHH:mm:ss.fffZ}");
        Console.WriteLine($"  User display: {sampleDate:dddd, MMMM dd, yyyy 'at' h:mm tt}");
        Console.WriteLine($"  File naming: {sampleDate:yyyyMMdd_HHmmss}");
        Console.WriteLine($"  Log format: {sampleDate:[yyyy-MM-dd HH:mm:ss.fff]}");

        // Culture-specific formatting - critical for international applications
        CultureInfo usCulture = new CultureInfo("en-US");
        CultureInfo ukCulture = new CultureInfo("en-GB");
        CultureInfo germanCulture = new CultureInfo("de-DE");
        CultureInfo indonesianCulture = new CultureInfo("id-ID");

        Console.WriteLine("\nCultural formatting differences:");
        Console.WriteLine($"  US (en-US): {sampleDate.ToString("F", usCulture)}");
        Console.WriteLine($"  UK (en-GB): {sampleDate.ToString("F", ukCulture)}");
        Console.WriteLine($"  German (de-DE): {sampleDate.ToString("F", germanCulture)}");
        Console.WriteLine($"  Indonesian (id-ID): {sampleDate.ToString("F", indonesianCulture)}");

        // Parsing with different approaches
        Console.WriteLine("\nParsing strategies:");

        // Parsing dates from strings - essential for user input
        string dateString = "2024-05-29";
        string timeString = "14:30:45";
        string fullString = "May 29, 2024 2:30 PM";

        if (DateTime.TryParse(dateString, out DateTime parsedDate))
            Console.WriteLine($"  TryParse '{dateString}': {parsedDate}");

        if (DateTime.TryParseExact(timeString, "HH:mm:ss", null, DateTimeStyles.None, out DateTime parsedTime))
            Console.WriteLine($"  TryParseExact '{timeString}': {parsedTime:T}");

        if (DateTime.TryParse(fullString, out DateTime parsedFull))
            Console.WriteLine($"  TryParse '{fullString}': {parsedFull}");

        // Demonstrating parsing pitfalls without format specification
        Console.WriteLine("\nParsing pitfall demonstration:");
        string ambiguousDate = "01/02/2024"; // Is this Jan 2 or Feb 1?

        DateTime usInterpretation = DateTime.Parse(ambiguousDate, usCulture);
        DateTime ukInterpretation = DateTime.Parse(ambiguousDate, ukCulture);

        Console.WriteLine($"  '{ambiguousDate}' in US culture: {usInterpretation:yyyy-MM-dd} (MM/dd/yyyy)");
        Console.WriteLine($"  '{ambiguousDate}' in UK culture: {ukInterpretation:yyyy-MM-dd} (dd/MM/yyyy)");
        Console.WriteLine("  Solution: Use unambiguous formats like 'yyyy-MM-dd' or round-trip 'O'");

        // Safe parsing with specific format
        string safeFormat = "yyyy-MM-dd HH:mm:ss";
        string safeString = sampleDate.ToString(safeFormat);
        Console.WriteLine($"\n  Safe format '{safeFormat}': '{safeString}'");

        if (DateTime.TryParseExact(safeString, safeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime safeParsed))
            Console.WriteLine($"  Safely parsed: {safeParsed}");

        Console.WriteLine();
    }
}