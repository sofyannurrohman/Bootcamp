using System.Xml;

namespace BootcampDay7.Demo;

public class DemoClassDTSC
{
    public static void DemonstrateTimeSpanConversion()
    {
        Console.WriteLine("2.5 TIMESPAN CONVERSION - STRING AND XML HANDLING");
        Console.WriteLine("=================================================");

        // Converting TimeSpan to string representation
        TimeSpan duration = new TimeSpan(2, 30, 45); // 2 hours, 30 minutes, 45 seconds
        Console.WriteLine($"TimeSpan value: {duration}");
        Console.WriteLine($"ToString() result: '{duration.ToString()}'");

        // Parsing TimeSpan from strings - essential for configuration files
        string timeString1 = "02:30:45";        // Standard format
        string timeString2 = "1.12:30:45";      // Days.Hours:Minutes:Seconds
        string timeString3 = "1:23:45:30.500";  // Hours:Minutes:Seconds.Milliseconds

        if (TimeSpan.TryParse(timeString1, out TimeSpan parsed1))
            Console.WriteLine($"Parsed '{timeString1}' as: {parsed1}");

        if (TimeSpan.TryParse(timeString2, out TimeSpan parsed2))
            Console.WriteLine($"Parsed '{timeString2}' as: {parsed2} ({parsed2.TotalHours:F2} total hours)");

        if (TimeSpan.TryParse(timeString3, out TimeSpan parsed3))
            Console.WriteLine($"Parsed '{timeString3}' as: {parsed3}");

        // Using Parse method (throws exception on failure)
        try
        {
            TimeSpan exactParse = TimeSpan.Parse("00:45:30");
            Console.WriteLine($"Parse() result: {exactParse}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Parse error: {ex.Message}");
        }

        // XML conversion - important for web services and configuration
        TimeSpan xmlDuration = TimeSpan.FromHours(2.5);
        string xmlString = XmlConvert.ToString(xmlDuration);
        Console.WriteLine($"XML representation: '{xmlString}'");

        TimeSpan fromXml = XmlConvert.ToTimeSpan(xmlString);
        Console.WriteLine($"Converted back from XML: {fromXml}");

        // Default value demonstration
        TimeSpan defaultValue = default(TimeSpan);
        Console.WriteLine($"Default TimeSpan value: {defaultValue}");
        Console.WriteLine($"TimeSpan.Zero: {TimeSpan.Zero}");
        Console.WriteLine($"Are they equal? {defaultValue == TimeSpan.Zero}");

        // TimeOfDay example - treating TimeSpan as time since midnight
        DateTime now = DateTime.Now;
        TimeSpan timeOfDay = now.TimeOfDay;
        Console.WriteLine($"Current time of day: {timeOfDay}");
        Console.WriteLine($"Hours since midnight: {timeOfDay.TotalHours:F2}");

        Console.WriteLine();
    }
}