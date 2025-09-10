namespace ExpressionMethodHelper
{
    
public class MethodHelper{
    public static void PrintWelcomeMessage()
    {
        Console.WriteLine("Welcome to our application!");
    }
        
        public static void ProcessData(int count)
        {
            Console.WriteLine($"Processing {count} items...");
        }
        
        public static void PrintMorningGreeting()
        {
            Console.WriteLine("Good morning!");
        }
        
        public static void PrintAfternoonGreeting()
        {
            Console.WriteLine("Good afternoon!");
        }
        
        public static void PrintCountdown(int number)
        {
            Console.WriteLine($"  Countdown: {number}");
        }
}
}

        