namespace BootcampDay7.Demo;

public class DemoClassDNDT
{
    public static void DemonstrateNullableDateTimes()
    {
        Console.WriteLine("7.5 NULLABLE DATETIME VALUES - HANDLING MISSING DATES");
        Console.WriteLine("======================================================");

        // DateTime and DateTimeOffset are structs (value types), so not intrinsically nullable
        // Two approaches to represent "null" or missing dates

        Console.WriteLine("Approach 1: Nullable<DateTime> (Recommended)");

        // Using nullable types - the safe and recommended approach
        DateTime? nullableDate = null;
        DateTime? validDate = new DateTime(2024, 5, 29);
        DateTimeOffset? nullableOffset = null;
        DateTimeOffset? validOffset = DateTimeOffset.Now;

        Console.WriteLine($"Nullable DateTime (null): {nullableDate?.ToString() ?? "NULL"}");
        Console.WriteLine($"Nullable DateTime (valid): {validDate}");
        Console.WriteLine($"Nullable DateTimeOffset (null): {nullableOffset?.ToString() ?? "NULL"}");
        Console.WriteLine($"Nullable DateTimeOffset (valid): {validOffset}");

        // Checking for null values
        if (nullableDate.HasValue)
            Console.WriteLine($"Date has value: {nullableDate.Value}");
        else
            Console.WriteLine("Date is null - no value assigned");

        // Getting value with fallback
        DateTime dateWithFallback = validDate ?? DateTime.Today;
        Console.WriteLine($"Date with fallback: {dateWithFallback}");

        // Practical example: Optional expiration date
        DateTime? subscriptionExpiry = null; // Lifetime subscription
        DateTime? trialExpiry = DateTime.Today.AddDays(30);

        Console.WriteLine($"\nSubscription scenarios:");
        Console.WriteLine($"Lifetime subscription expiry: {subscriptionExpiry?.ToString("yyyy-MM-dd") ?? "Never"}");
        Console.WriteLine($"Trial expiry: {trialExpiry?.ToString("yyyy-MM-dd") ?? "Never"}");

        // Check if subscription is expired
        bool isLifetimeExpired = subscriptionExpiry.HasValue && subscriptionExpiry.Value < DateTime.Today;
        bool isTrialExpired = trialExpiry.HasValue && trialExpiry.Value < DateTime.Today;

        Console.WriteLine($"Lifetime expired: {isLifetimeExpired}");
        Console.WriteLine($"Trial expired: {isTrialExpired}");

        Console.WriteLine("\nApproach 2: Using MinValue (Use with caution)");

        // Using default/MinValue - can cause issues with timezone conversions
        DateTime defaultDateTime = default(DateTime);
        DateTime minDateTime = DateTime.MinValue;
        DateTimeOffset defaultOffset = default(DateTimeOffset);
        DateTimeOffset minOffset = DateTimeOffset.MinValue;

        Console.WriteLine($"Default DateTime: {defaultDateTime}");
        Console.WriteLine($"DateTime.MinValue: {minDateTime}");
        Console.WriteLine($"Default DateTimeOffset: {defaultOffset}");
        Console.WriteLine($"DateTimeOffset.MinValue: {minOffset}");

        // Demonstrating MinValue conversion issue
        Console.WriteLine("\nMinValue conversion warning:");
        try
        {
            DateTime minValueLocal = DateTime.MinValue;
            Console.WriteLine($"MinValue before conversion: {minValueLocal}");

            // This might change the value when converting timezones!
            DateTime converted = minValueLocal.ToUniversalTime();
            Console.WriteLine($"After ToUniversalTime(): {converted}");
            Console.WriteLine($"Values equal? {minValueLocal == converted}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Conversion error: {ex.Message}");
        }

        // Safe pattern for checking "null" dates
        Console.WriteLine("\nSafe null checking patterns:");

        DateTime suspiciousDate = DateTime.MinValue;
        DateTime? safeSuspiciousDate = suspiciousDate == DateTime.MinValue ? null : suspiciousDate;

        Console.WriteLine($"Original date: {suspiciousDate}");
        Console.WriteLine($"Safe nullable: {safeSuspiciousDate?.ToString() ?? "NULL"}");

        Console.WriteLine();
    }
}