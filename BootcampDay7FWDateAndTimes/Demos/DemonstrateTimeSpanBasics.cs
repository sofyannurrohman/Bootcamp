namespace BootcampDay7.Demo;

public class DemoClassDTSB{
    public static void DemonstrateTimeSpanBasics()
    {
        Console.WriteLine("1. TIMESPAN BASICS - REPRESENTING TIME INTERVALS");
        Console.WriteLine("================================================");

        // TimeSpan represents a duration or interval of time
        // Resolution: 100 nanoseconds (ns) - very precise timing
        // Range: Approximately 10 million days, can be positive or negative

        Console.WriteLine("TimeSpan fundamentals:");
        Console.WriteLine($"  Resolution: 100 nanoseconds (1 tick)");
        Console.WriteLine($"  Range: ±{TimeSpan.MaxValue.TotalDays:N0} days approximately");
        Console.WriteLine($"  MinValue: {TimeSpan.MinValue}");
        Console.WriteLine($"  MaxValue: {TimeSpan.MaxValue}");

        // Constructor patterns - specify days, hours, minutes, seconds, milliseconds
        TimeSpan workingHours = new TimeSpan(8, 0, 0); // hours, minutes, seconds
        TimeSpan projectDuration = new TimeSpan(30, 8, 30, 0); // days, hours, minutes, seconds
        TimeSpan preciseInterval = new TimeSpan(5, 14, 30, 45, 500); // days, hours, minutes, seconds, milliseconds

        Console.WriteLine($"\nConstructor examples:");
        Console.WriteLine($"  Working day: {workingHours}");
        Console.WriteLine($"  Project duration: {projectDuration} ({projectDuration.TotalDays} days)");
        Console.WriteLine($"  Precise interval: {preciseInterval}");

        // Ticks constructor - each tick = 100 nanoseconds
        long ticksInSecond = TimeSpan.TicksPerSecond;
        TimeSpan oneSecond = new TimeSpan(ticksInSecond);
        Console.WriteLine($"  Ticks per second: {ticksInSecond:N0}");
        Console.WriteLine($"  One second from ticks: {oneSecond}");

        // Static From... methods - convenient for single units
        TimeSpan fromDays = TimeSpan.FromDays(2.5);
        TimeSpan fromHours = TimeSpan.FromHours(2.5);
        TimeSpan fromMinutes = TimeSpan.FromMinutes(90);
        TimeSpan fromSeconds = TimeSpan.FromSeconds(3600);
        TimeSpan fromMilliseconds = TimeSpan.FromMilliseconds(5000);
        TimeSpan fromMicroseconds = TimeSpan.FromMicroseconds(1000000);

        Console.WriteLine($"\nStatic factory methods:");
        Console.WriteLine($"  2.5 days: {fromDays}");
        Console.WriteLine($"  2.5 hours: {fromHours}");
        Console.WriteLine($"  90 minutes: {fromMinutes}");
        Console.WriteLine($"  3600 seconds: {fromSeconds}");
        Console.WriteLine($"  5000 milliseconds: {fromMilliseconds}");
        Console.WriteLine($"  1,000,000 microseconds: {fromMicroseconds}");

        // Negative TimeSpan - represents time going backwards
        TimeSpan negativeSpan = TimeSpan.FromHours(-2.5);
        Console.WriteLine($"  Negative span: {negativeSpan}");

        // Creating from DateTime subtraction - very common in practice
        DateTime start = new DateTime(2024, 1, 15, 9, 0, 0);
        DateTime end = new DateTime(2024, 1, 15, 17, 30, 0);
        TimeSpan workDuration = end - start;

        Console.WriteLine($"\nFrom DateTime subtraction:");
        Console.WriteLine($"  Start: {start:HH:mm}");
        Console.WriteLine($"  End: {end:HH:mm}");
        Console.WriteLine($"  Duration: {workDuration}");

        Console.WriteLine();
    }
}
