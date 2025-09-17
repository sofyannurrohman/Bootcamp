namespace BootcampDay7.Demo;

public class DemoClassSI
{
    public static void DemonstrateStringInterpolationAndFormatting()
        {
            Console.WriteLine("6. STRING INTERPOLATION AND FORMATTING DEMONSTRATION");
            Console.WriteLine("====================================================");

            // String interpolation - modern and readable way to build strings
            string name = "Alice";
            int age = 25;
            DateTime today = DateTime.Now;

            string interpolated = $"Hello, my name is {name} and I'm {age} years old.";
            string withDate = $"Today is {today.DayOfWeek}, {today:yyyy-MM-dd}";
            
            Console.WriteLine(interpolated);
            Console.WriteLine(withDate);

            // Traditional string formatting - still useful for complex scenarios
            string template = "It's {0} degrees in {1} on this {2} morning";
            string formatted = string.Format(template, 25, "Jakarta", today.DayOfWeek);
            Console.WriteLine(formatted);

            // Format specifiers for numbers and dates
            double price = 19.99;
            Console.WriteLine($"Price: {price:C}"); // Currency format
            Console.WriteLine($"Percentage: {0.85:P}"); // Percentage format
            Console.WriteLine($"Date: {today:dddd, MMMM dd, yyyy}"); // Long date format

            Console.WriteLine();
        }
        }