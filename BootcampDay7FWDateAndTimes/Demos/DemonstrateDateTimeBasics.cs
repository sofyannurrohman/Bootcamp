namespace BootcampDay7.Demo;

public class DemoClassDDTB
{
    public static void DemonstrateDateTimeBasics()
    {
        Console.WriteLine("3. DATETIME BASICS - SPECIFIC POINTS IN TIME");
        Console.WriteLine("============================================");

        // Creating DateTime objects - different approaches for different needs
        DateTime newYear = new DateTime(2024, 1, 1); // Midnight on New Year
        DateTime meeting = new DateTime(2024, 5, 29, 14, 30, 0); // Today at 2:30 PM
        DateTime precise = new DateTime(2024, 5, 29, 14, 30, 15, 500); // Include milliseconds

        Console.WriteLine($"New Year: {newYear}");
        Console.WriteLine($"Meeting time: {meeting}");
        Console.WriteLine($"Precise timestamp: {precise}");

        // Getting current date and time - essential for logging and timestamps
        DateTime now = DateTime.Now; // Local time
        DateTime utcNow = DateTime.UtcNow; // UTC time - better for distributed systems
        DateTime today = DateTime.Today; // Today at midnight

        Console.WriteLine($"Current local time: {now}");
        Console.WriteLine($"Current UTC time: {utcNow}");
        Console.WriteLine($"Today (midnight): {today}");

        // Working with DateTime components
        Console.WriteLine($"Current year: {now.Year}");
        Console.WriteLine($"Current month: {now.Month} ({now:MMMM})");
        Console.WriteLine($"Day of week: {now.DayOfWeek}");
        Console.WriteLine($"Day of year: {now.DayOfYear}");

        // DateTime kind - important for timezone handling
        DateTime local = new DateTime(2024, 5, 29, 12, 0, 0, DateTimeKind.Local);
        DateTime utc = new DateTime(2024, 5, 29, 12, 0, 0, DateTimeKind.Utc);
        DateTime unspecified = new DateTime(2024, 5, 29, 12, 0, 0, DateTimeKind.Unspecified);

        Console.WriteLine($"Local time: {local} (Kind: {local.Kind})");
        Console.WriteLine($"UTC time: {utc} (Kind: {utc.Kind})");
        Console.WriteLine($"Unspecified: {unspecified} (Kind: {unspecified.Kind})");

        Console.WriteLine();
    }
}