namespace BootcampDay7.Demo;

public class DemoClassDDDTO
{
    public static void DemonstrateDateTimeVsDateTimeOffset()
    {
        Console.WriteLine("4. DATETIME VS DATETIMEOFFSET - TIMEZONE AWARENESS");
        Console.WriteLine("==================================================");

        // Key difference: how they handle equality and timezone information
        Console.WriteLine("DateTime - three-state DateTimeKind flag:");

        DateTime localTime = new DateTime(2024, 5, 29, 15, 30, 0, DateTimeKind.Local);
        DateTime utcTime = new DateTime(2024, 5, 29, 8, 30, 0, DateTimeKind.Utc);      // Same moment as local (assuming +7 offset)
        DateTime unspecified = new DateTime(2024, 5, 29, 15, 30, 0, DateTimeKind.Unspecified);

        Console.WriteLine($"  Local: {localTime} (Kind: {localTime.Kind})");
        Console.WriteLine($"  UTC: {utcTime} (Kind: {utcTime.Kind})");
        Console.WriteLine($"  Unspecified: {unspecified} (Kind: {unspecified.Kind})");

        // DateTime equality ignores DateTimeKind - can cause timezone bugs!
        Console.WriteLine($"\nDateTime equality problems:");
        Console.WriteLine($"  Local == Unspecified? {localTime == unspecified} (same components, different meaning!)");
        Console.WriteLine($"  UTC != Local? {utcTime != localTime} (same moment, different representation!)");

        Console.WriteLine("\nDateTimeOffset - explicit UTC offset storage:");

        // DateTimeOffset stores time + explicit offset from UTC
        DateTimeOffset jakartaTime = new DateTimeOffset(2024, 5, 29, 15, 30, 0, TimeSpan.FromHours(7));
        DateTimeOffset newYorkTime = new DateTimeOffset(2024, 5, 29, 4, 30, 0, TimeSpan.FromHours(-4));
        DateTimeOffset londonTime = new DateTimeOffset(2024, 5, 29, 9, 30, 0, TimeSpan.FromHours(1));
        DateTimeOffset utcTimeOffset = new DateTimeOffset(2024, 5, 29, 8, 30, 0, TimeSpan.Zero);

        Console.WriteLine($"  Jakarta (+7): {jakartaTime}");
        Console.WriteLine($"  New York (-4): {newYorkTime}");
        Console.WriteLine($"  London (+1): {londonTime}");
        Console.WriteLine($"  UTC (0): {utcTimeOffset}");

        // DateTimeOffset equality considers absolute time - much safer!
        Console.WriteLine($"\nDateTimeOffset equality benefits:");
        Console.WriteLine($"  Jakarta == New York? {jakartaTime == newYorkTime} (same absolute moment)");
        Console.WriteLine($"  Jakarta == London? {jakartaTime == londonTime} (same absolute moment)");
        Console.WriteLine($"  All represent UTC: {jakartaTime.UtcDateTime}");

        // Current time examples
        DateTime nowLocal = DateTime.Now;
        DateTime nowUtc = DateTime.UtcNow;
        DateTimeOffset nowWithOffset = DateTimeOffset.Now;
        DateTimeOffset nowUtcWithOffset = DateTimeOffset.UtcNow;

        Console.WriteLine($"\nCurrent time comparison:");
        Console.WriteLine($"  DateTime.Now: {nowLocal}");
        Console.WriteLine($"  DateTime.UtcNow: {nowUtc}");
        Console.WriteLine($"  DateTimeOffset.Now: {nowWithOffset}");
        Console.WriteLine($"  DateTimeOffset.UtcNow: {nowUtcWithOffset}");

        // Demonstrating the advantage of DateTimeOffset for global applications
        Console.WriteLine($"\nWhy DateTimeOffset is better for distributed systems:");

        // Scenario: User logs in from different timezones
        var loginEvents = new[]
        {
                new { User = "Alice", LoginTime = new DateTimeOffset(2024, 5, 29, 9, 0, 0, TimeSpan.FromHours(-8)) }, // Pacific
                new { User = "Bob", LoginTime = new DateTimeOffset(2024, 5, 29, 12, 0, 0, TimeSpan.FromHours(-5)) },   // Eastern  
                new { User = "Carol", LoginTime = new DateTimeOffset(2024, 5, 29, 18, 0, 0, TimeSpan.FromHours(1)) },  // Central Europe
            };

        Console.WriteLine("  Global login events (all at the same UTC moment):");
        foreach (var evt in loginEvents)
        {
            Console.WriteLine($"    {evt.User}: {evt.LoginTime} (UTC: {evt.LoginTime.UtcDateTime:HH:mm})");
        }

        // All login times are actually the same moment!
        bool allSameTime = loginEvents[0].LoginTime == loginEvents[1].LoginTime &&
                          loginEvents[1].LoginTime == loginEvents[2].LoginTime;
        Console.WriteLine($"    All logged in at same moment: {allSameTime}");

        // Recommendation summary
        Console.WriteLine($"\nRecommendations:");
        Console.WriteLine($"  • Use DateTimeOffset for: APIs, databases, distributed systems, logging");
        Console.WriteLine($"  • Use DateTime for: Local UI times, relative scheduling ('3 AM next Sunday')");
        Console.WriteLine($"  • DateTimeOffset prevents DST and timezone conversion bugs");
        Console.WriteLine($"  • DateTime is simpler for single-timezone applications");

        Console.WriteLine();
    }
}