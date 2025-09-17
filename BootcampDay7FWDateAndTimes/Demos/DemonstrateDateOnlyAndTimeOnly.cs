namespace BootcampDay7.Demo;

public class DemoClassDDOT
{
    public static void DemonstrateDateOnlyAndTimeOnly()
    {
        Console.WriteLine("8. DATEONLY AND TIMEONLY - SPECIALIZED TYPES (.NET 6+)");
        Console.WriteLine("=======================================================");

        // Introduced in .NET 6 to provide more specific and type-safe representations
        // DateOnly: just a date (year, month, day) without time component
        // TimeOnly: just a time of day without date component
        // Both lack DateTimeKind and have no concept of Local or UTC

        Console.WriteLine("DateOnly benefits - avoids DateTime pitfalls:");

        // DateOnly prevents bugs where DateTime intended as "just a date" 
        // accidentally gets non-zero time, causing equality comparisons to fail
        DateOnly birthday = new DateOnly(1990, 6, 15);
        DateOnly holiday = new DateOnly(2024, 12, 25);
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        Console.WriteLine($"  Birthday: {birthday}");
        Console.WriteLine($"  Christmas: {holiday}");
        Console.WriteLine($"  Today: {today}");

        // Safe date comparisons - no time component to worry about
        DateOnly date1 = new DateOnly(2024, 5, 29);
        DateOnly date2 = new DateOnly(2024, 5, 29);
        Console.WriteLine($"  Date equality works perfectly: {date1} == {date2} is {date1 == date2}");

        // Calculate age using DateOnly - more appropriate than DateTime
        int age = today.Year - birthday.Year;
        if (today < birthday.AddYears(age))
            age--;
        Console.WriteLine($"  Current age: {age} years");

        // Working with date arithmetic
        DateOnly nextBirthday = birthday.AddYears(today.Year - birthday.Year);
        if (nextBirthday < today)
            nextBirthday = nextBirthday.AddYears(1);

        int daysUntilBirthday = nextBirthday.DayNumber - today.DayNumber;
        Console.WriteLine($"  Days until next birthday: {daysUntilBirthday}");

        Console.WriteLine("\nTimeOnly benefits - ideal for schedules and recurring times:");

        // TimeOnly - perfect for store hours, alarms, recurring daily schedules
        TimeOnly storeOpen = new TimeOnly(9, 0);
        TimeOnly lunchStart = new TimeOnly(12, 0);
        TimeOnly lunchEnd = new TimeOnly(13, 0);
        TimeOnly storeClose = new TimeOnly(17, 30);
        TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

        Console.WriteLine($"  Store opens: {storeOpen}");
        Console.WriteLine($"  Lunch break: {lunchStart} - {lunchEnd}");
        Console.WriteLine($"  Store closes: {storeClose}");
        Console.WriteLine($"  Current time: {currentTime}");

        // Business logic with TimeOnly
        bool isOpen = currentTime >= storeOpen && currentTime <= storeClose &&
                     !(currentTime >= lunchStart && currentTime < lunchEnd);
        Console.WriteLine($"  Store is currently: {(isOpen ? "OPEN" : "CLOSED")}");

        // TimeOnly arithmetic
        TimeOnly meetingStart = new TimeOnly(14, 0);
        TimeSpan meetingDuration = TimeSpan.FromMinutes(90);
        TimeOnly meetingEnd = meetingStart.Add(meetingDuration);

        Console.WriteLine($"\n  Meeting schedule:");
        Console.WriteLine($"    Start: {meetingStart}");
        Console.WriteLine($"    Duration: {meetingDuration}");
        Console.WriteLine($"    End: {meetingEnd}");

        // Combining DateOnly and TimeOnly when needed
        DateOnly eventDate = new DateOnly(2024, 6, 15);
        TimeOnly eventTime = new TimeOnly(14, 30);
        DateTime fullEventDateTime = eventDate.ToDateTime(eventTime);

        Console.WriteLine($"\n  Combining DateOnly and TimeOnly:");
        Console.WriteLine($"    Event date: {eventDate}");
        Console.WriteLine($"    Event time: {eventTime}");
        Console.WriteLine($"    Full DateTime: {fullEventDateTime}");

        // Practical example: Recurring weekly schedule
        Console.WriteLine("\n  Weekly class schedule (TimeOnly for daily patterns):");
        var classSchedule = new[]
        {
                new { Day = "Monday", Time = new TimeOnly(9, 0), Subject = "Mathematics" },
                new { Day = "Wednesday", Time = new TimeOnly(11, 0), Subject = "Physics" },
                new { Day = "Friday", Time = new TimeOnly(14, 0), Subject = "Chemistry" }
            };

        foreach (var cls in classSchedule)
        {
            Console.WriteLine($"    {cls.Day} at {cls.Time}: {cls.Subject}");
        }

        Console.WriteLine();
    }
}