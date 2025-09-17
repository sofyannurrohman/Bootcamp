namespace BootcampDay7.Demo;

public class DemoClassDTSO
{
    public static void DemonstrateTimeSpanOperations()
    {
        Console.WriteLine("2. TIMESPAN OPERATIONS - MATH WITH TIME");
        Console.WriteLine("=======================================");

        // TimeSpan overloads operators <, >, +, and - for intuitive calculations
        TimeSpan duration1 = TimeSpan.FromHours(2);
        TimeSpan duration2 = TimeSpan.FromMinutes(30);
        TimeSpan totalDuration = duration1 + duration2;

        Console.WriteLine("Basic arithmetic:");
        Console.WriteLine($"  {duration1} + {duration2} = {totalDuration}");

        TimeSpan plannedTime = TimeSpan.FromHours(10);
        TimeSpan actualTime = TimeSpan.FromHours(8.5);
        TimeSpan timeDifference = plannedTime - actualTime;
        Console.WriteLine($"  {plannedTime} - {actualTime} = {timeDifference}");

        // Comparison operators
        Console.WriteLine($"  {duration1} > {duration2}? {duration1 > duration2}");
        Console.WriteLine($"  {actualTime} < {plannedTime}? {actualTime < plannedTime}");

        // Working with TimeSpan properties - Integer vs Total properties
        TimeSpan complexTime = TimeSpan.FromDays(10) - TimeSpan.FromSeconds(1); // Nearly 10 days

        Console.WriteLine($"\nProperty demonstration with {complexTime}:");
        Console.WriteLine("Integer properties (components only):");
        Console.WriteLine($"  Days: {complexTime.Days}");
        Console.WriteLine($"  Hours: {complexTime.Hours}");
        Console.WriteLine($"  Minutes: {complexTime.Minutes}");
        Console.WriteLine($"  Seconds: {complexTime.Seconds}");
        Console.WriteLine($"  Milliseconds: {complexTime.Milliseconds}");

        Console.WriteLine("Total properties (entire span as double):");
        Console.WriteLine($"  TotalDays: {complexTime.TotalDays}");
        Console.WriteLine($"  TotalHours: {complexTime.TotalHours:F2}");
        Console.WriteLine($"  TotalMinutes: {complexTime.TotalMinutes:F2}");
        Console.WriteLine($"  TotalSeconds: {complexTime.TotalSeconds:F2}");
        Console.WriteLine($"  TotalMilliseconds: {complexTime.TotalMilliseconds:F2}");

        // Practical example: Marathon time analysis
        TimeSpan marathonTime = new TimeSpan(3, 25, 30); // 3 hours, 25 minutes, 30 seconds
        Console.WriteLine($"\nMarathon analysis for time {marathonTime}:");
        Console.WriteLine($"  Finished in {marathonTime.Hours} hours and {marathonTime.Minutes} minutes");
        Console.WriteLine($"  Total time: {marathonTime.TotalMinutes:F1} minutes");
        Console.WriteLine($"  Average pace per mile: {marathonTime.TotalMinutes / 26.2:F2} minutes/mile");

        // TimeSpan as time of day
        DateTime currentTime = DateTime.Now;
        TimeSpan timeOfDay = currentTime.TimeOfDay;
        Console.WriteLine($"\nTime of day example:");
        Console.WriteLine($"  Current time: {currentTime}");
        Console.WriteLine($"  Time since midnight: {timeOfDay}");
        Console.WriteLine($"  Hours elapsed today: {timeOfDay.TotalHours:F2}");

        // Demonstrating the range and precision
        TimeSpan maxSpan = TimeSpan.MaxValue;
        TimeSpan minSpan = TimeSpan.MinValue;
        TimeSpan oneTick = new TimeSpan(1); // 1 tick = 100 nanoseconds

        Console.WriteLine($"\nTimeSpan limits and precision:");
        Console.WriteLine($"  Maximum span: {maxSpan.TotalDays:N0} days");
        Console.WriteLine($"  Minimum span: {minSpan.TotalDays:N0} days");
        Console.WriteLine($"  One tick duration: {oneTick.TotalMilliseconds * 1000000} nanoseconds");

        Console.WriteLine();
    }
}