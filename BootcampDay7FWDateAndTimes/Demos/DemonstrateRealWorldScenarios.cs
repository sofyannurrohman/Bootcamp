namespace BootcampDay7.Demo;

public class DemoClassDRWS
{
    public static void DemonstrateRealWorldScenarios()
    {
        Console.WriteLine("8. REAL-WORLD SCENARIOS - PRACTICAL APPLICATIONS");
        Console.WriteLine("================================================");

        // Scenario 1: Event scheduling system
        Console.WriteLine("Scenario 1: Global conference scheduling");

        DateTimeOffset conferenceStart = new DateTimeOffset(2024, 9, 15, 9, 0, 0, TimeSpan.FromHours(-7)); // Pacific Time
        TimeSpan sessionDuration = TimeSpan.FromMinutes(45);
        TimeSpan breakDuration = TimeSpan.FromMinutes(15);

        Console.WriteLine($"Conference starts: {conferenceStart}");

        // Schedule multiple sessions
        for (int i = 1; i <= 4; i++)
        {
            DateTimeOffset sessionStart = conferenceStart.Add(TimeSpan.FromMinutes((i - 1) * 60));
            DateTimeOffset sessionEnd = sessionStart.Add(sessionDuration);

            Console.WriteLine($"  Session {i}: {sessionStart:HH:mm} - {sessionEnd:HH:mm} (Pacific Time)");
        }

        // Scenario 2: SLA monitoring
        Console.WriteLine("\nScenario 2: Service Level Agreement monitoring");

        DateTime serviceStart = DateTime.UtcNow;
        TimeSpan slaTarget = TimeSpan.FromHours(4); // 4-hour SLA
        DateTime slaDeadline = serviceStart.Add(slaTarget);

        Console.WriteLine($"Service request: {serviceStart:yyyy-MM-dd HH:mm:ss} UTC");
        Console.WriteLine($"SLA deadline: {slaDeadline:yyyy-MM-dd HH:mm:ss} UTC");

        // Simulate service completion
        DateTime serviceComplete = serviceStart.AddHours(3.5);
        TimeSpan actualDuration = serviceComplete - serviceStart;
        bool slaMetric = actualDuration <= slaTarget;

        Console.WriteLine($"Service completed: {serviceComplete:yyyy-MM-dd HH:mm:ss} UTC");
        Console.WriteLine($"Duration: {actualDuration}");
        Console.WriteLine($"SLA met: {(slaMetric ? "YES" : "NO")}");

        // Scenario 3: Recurring appointment system
        Console.WriteLine("\nScenario 3: Weekly recurring meetings");

        DateOnly startDate = new DateOnly(2024, 6, 3); // First Monday
        TimeOnly meetingTime = new TimeOnly(10, 0);

        Console.WriteLine("Next 5 weekly meetings:");
        for (int week = 0; week < 5; week++)
        {
            DateOnly meetingDate = startDate.AddDays(week * 7);
            DateTime fullDateTime = meetingDate.ToDateTime(meetingTime);
            Console.WriteLine($"  Week {week + 1}: {fullDateTime:dddd, MMMM dd, yyyy} at {meetingTime}");
        }

        // Scenario 4: Age calculation and birthday tracking
        Console.WriteLine("\nScenario 4: Employee birthday system");

        var employees = new[]
        {
                new { Name = "Alice", Birthday = new DateOnly(1985, 3, 15) },
                new { Name = "Bob", Birthday = new DateOnly(1990, 8, 22) },
                new { Name = "Carol", Birthday = new DateOnly(1988, 11, 7) }
            };

        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);

        foreach (var employee in employees)
        {
            int employeeAge = CalculateAge(employee.Birthday, currentDate);
            DateOnly nextBirthday = GetNextBirthday(employee.Birthday, currentDate);
            int daysUntilBirthday = nextBirthday.DayNumber - currentDate.DayNumber;

            Console.WriteLine($"  {employee.Name}: {employeeAge} years old, birthday in {daysUntilBirthday} days");
        }

        Console.WriteLine();
    }

    // Helper methods for real-world scenarios
    static DateTime AddBusinessDays(DateTime startDate, int businessDays)
    {
        DateTime result = startDate;
        int daysAdded = 0;

        while (daysAdded < businessDays)
        {
            result = result.AddDays(1);
            if (result.DayOfWeek != DayOfWeek.Saturday && result.DayOfWeek != DayOfWeek.Sunday)
            {
                daysAdded++;
            }
        }

        return result;
    }

    static int CalculateAge(DateOnly birthDate, DateOnly currentDate)
    {
        int age = currentDate.Year - birthDate.Year;
        if (currentDate < birthDate.AddYears(age))
            age--;
        return age;
    }

    static DateOnly GetNextBirthday(DateOnly birthDate, DateOnly currentDate)
    {
        DateOnly nextBirthday = new DateOnly(currentDate.Year, birthDate.Month, birthDate.Day);
        if (nextBirthday <= currentDate)
            nextBirthday = nextBirthday.AddYears(1);
        return nextBirthday;
    }

}