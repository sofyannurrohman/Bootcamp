
namespace BootcampDay7.Demo;

public class DemoClassDDTO
{
    public static void DemonstrateDateTimeOperations()
    {
        Console.WriteLine("5. DATETIME OPERATIONS - MANIPULATING DATES");
        Console.WriteLine("============================================");

        DateTime startDate = new DateTime(2024, 1, 15, 9, 0, 0);
        Console.WriteLine($"Starting date: {startDate}");

        // Adding time intervals - very common in business logic
        DateTime deadline = startDate.AddDays(30);
        DateTime reminder = deadline.AddDays(-7);
        DateTime urgentReminder = deadline.AddHours(-24);

        Console.WriteLine($"Project deadline: {deadline}");
        Console.WriteLine($"First reminder: {reminder}");
        Console.WriteLine($"Urgent reminder: {urgentReminder}");

        // Adding different time units
        DateTime scheduleExample = startDate
            .AddYears(1)
            .AddMonths(2)
            .AddDays(15)
            .AddHours(3)
            .AddMinutes(30);

        Console.WriteLine($"Complex schedule: {scheduleExample}");

        // Calculating business days (excluding weekends)
        DateTime businessStart = new DateTime(2024, 5, 27); // Monday
        int businessDaysToAdd = 10;
        DateTime businessEnd = AddBusinessDays(businessStart, businessDaysToAdd);

        Console.WriteLine($"Business days calculation:");
        Console.WriteLine($"  Start: {businessStart:yyyy-MM-dd} ({businessStart.DayOfWeek})");
        Console.WriteLine($"  Add {businessDaysToAdd} business days");
        Console.WriteLine($"  End: {businessEnd:yyyy-MM-dd} ({businessEnd.DayOfWeek})");

        // Finding the next occurrence of a specific day
        DateTime nextFriday = GetNextWeekday(DateTime.Today, DayOfWeek.Friday);
        Console.WriteLine($"Next Friday: {nextFriday:yyyy-MM-dd}");

        Console.WriteLine();
    }

    static DateTime GetNextWeekday(DateTime startDate, DayOfWeek targetDay)
    {
        int daysUntilTarget = ((int)targetDay - (int)startDate.DayOfWeek + 7) % 7;
        if (daysUntilTarget == 0) daysUntilTarget = 7; // If it's the same day, get next week
        return startDate.AddDays(daysUntilTarget);
    }
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

}