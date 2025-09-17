using System.Globalization;

namespace BootcampDay7.Demo;

public class DemoClassDDTCC
{
    public static void DemonstrateDateTimeConstructionAndConversion()
    {
        Console.WriteLine("4.5 DATETIME CONSTRUCTION & CONVERSION - DETAILED MECHANICS");
        Console.WriteLine("===========================================================");

        // DateTime construction with ticks (100-nanosecond intervals from 01/01/0001)
        long ticks = DateTime.Now.Ticks;
        DateTime fromTicks = new DateTime(ticks);
        Console.WriteLine($"Current ticks: {ticks:N0}");
        Console.WriteLine($"DateTime from ticks: {fromTicks}");

        // Static Parse methods - essential for user input and data import
        string dateString = "2024-05-29";
        string dateTimeString = "2024-05-29 14:30:00";
        string isoString = "2024-05-29T14:30:00.000Z";

        if (DateTime.TryParse(dateString, out DateTime parsedDate))
            Console.WriteLine($"TryParse '{dateString}': {parsedDate}");

        // ParseExact requires specific format - safer for known formats
        if (DateTime.TryParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out DateTime exactParsed))
            Console.WriteLine($"ParseExact result: {exactParsed}");

        // ISO 8601 format parsing
        if (DateTime.TryParse(isoString, out DateTime isoParsed))
            Console.WriteLine($"ISO parsed: {isoParsed} (Kind: {isoParsed.Kind})");

        // DateTimeOffset construction examples
        Console.WriteLine("\nDateTimeOffset construction:");

        // From components with offset
        DateTimeOffset specificOffset = new DateTimeOffset(2024, 5, 29, 14, 30, 0, TimeSpan.FromHours(7));
        Console.WriteLine($"Specific offset: {specificOffset}");

        // From DateTime with inferred offset
        DateTime localTime = new DateTime(2024, 5, 29, 14, 30, 0, DateTimeKind.Local);
        DateTime utcTime = new DateTime(2024, 5, 29, 14, 30, 0, DateTimeKind.Utc);
        DateTime unspecifiedTime = new DateTime(2024, 5, 29, 14, 30, 0, DateTimeKind.Unspecified);

        DateTimeOffset fromLocal = new DateTimeOffset(localTime);
        DateTimeOffset fromUtc = new DateTimeOffset(utcTime);
        DateTimeOffset fromUnspecified = new DateTimeOffset(unspecifiedTime);

        Console.WriteLine($"From Local DateTime: {fromLocal}");
        Console.WriteLine($"From UTC DateTime: {fromUtc}");
        Console.WriteLine($"From Unspecified: {fromUnspecified}");

        // Converting between DateTimeOffset and DateTime
        Console.WriteLine("\nConversion between DateTimeOffset and DateTime:");

        DateTimeOffset sampleOffset = DateTimeOffset.Now;
        DateTime utcFromOffset = sampleOffset.UtcDateTime;
        DateTime localFromOffset = sampleOffset.LocalDateTime;
        DateTime unspecifiedFromOffset = sampleOffset.DateTime;

        Console.WriteLine($"Original DateTimeOffset: {sampleOffset}");
        Console.WriteLine($"UtcDateTime (Kind: {utcFromOffset.Kind}): {utcFromOffset}");
        Console.WriteLine($"LocalDateTime (Kind: {localFromOffset.Kind}): {localFromOffset}");
        Console.WriteLine($"DateTime (Kind: {unspecifiedFromOffset.Kind}): {unspecifiedFromOffset}");

        // Demonstrating DateTimeKind importance
        Console.WriteLine("\nDateTimeKind comparison behavior:");

        DateTime kind1 = new DateTime(2024, 5, 29, 12, 0, 0, DateTimeKind.Local);
        DateTime kind2 = new DateTime(2024, 5, 29, 12, 0, 0, DateTimeKind.Utc);
        DateTime kind3 = new DateTime(2024, 5, 29, 12, 0, 0, DateTimeKind.Unspecified);

        Console.WriteLine($"Local time: {kind1} (Kind: {kind1.Kind})");
        Console.WriteLine($"UTC time: {kind2} (Kind: {kind2.Kind})");
        Console.WriteLine($"Unspecified: {kind3} (Kind: {kind3.Kind})");

        // DateTime equality ignores Kind - this can cause timezone issues!
        Console.WriteLine($"Local == UTC? {kind1 == kind2} (both represent different actual moments!)");
        Console.WriteLine($"Local == Unspecified? {kind1 == kind3}");

        // DateTimeOffset equality comparison
        DateTimeOffset offset1 = new DateTimeOffset(2024, 5, 29, 15, 0, 0, TimeSpan.FromHours(7));  // Jakarta
        DateTimeOffset offset2 = new DateTimeOffset(2024, 5, 29, 8, 0, 0, TimeSpan.FromHours(0));   // UTC

        Console.WriteLine($"\nDateTimeOffset comparison:");
        Console.WriteLine($"Jakarta time: {offset1}");
        Console.WriteLine($"UTC time: {offset2}");
        Console.WriteLine($"Are they equal? {offset1 == offset2} (same absolute moment)");

        Console.WriteLine();
    }
}