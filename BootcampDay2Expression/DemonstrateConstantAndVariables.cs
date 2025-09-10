namespace ExpressionDemo
{
    public class ExpressionHelper
    {

        public static void DemonstrateConstantsAndVariables()
        {
            Console.WriteLine("=== CONSTANTS AND VARIABLES ===");
            Console.WriteLine("These are the simplest expressions - the atoms of your code\n");

            // Constants - values that never change
            Console.WriteLine("--- Constants (Fixed Values) ---");
            const int MaxUsers = 1000;        // Integer constant
            const double Pi = 3.14159;        // Double constant
            const string AppName = "MyApp";   // String constant
            const bool IsProduction = false; // Boolean constant

            Console.WriteLine($"Integer constant: {MaxUsers}");
            Console.WriteLine($"Double constant: {Pi}");
            Console.WriteLine($"String constant: {AppName}");
            Console.WriteLine($"Boolean constant: {IsProduction}");

            // Variables - values that can change during execution
            Console.WriteLine("\n--- Variables (Changeable Values) ---");
            int currentUsers = 250;           // Start with 250 users
            double radius = 5.0;              // Circle radius
            string userName = "Alice";        // Current user
            bool isLoggedIn = true;           // Login status

            Console.WriteLine($"Current users: {currentUsers}");
            Console.WriteLine($"Circle radius: {radius}");
            Console.WriteLine($"Username: {userName}");
            Console.WriteLine($"Logged in: {isLoggedIn}");

            // Variables can be changed - that's the point!
            currentUsers = 275;
            userName = "Bob";
            isLoggedIn = false;

            Console.WriteLine("\nAfter changes:");
            Console.WriteLine($"Current users: {currentUsers}");
            Console.WriteLine($"Username: {userName}");
            Console.WriteLine($"Logged in: {isLoggedIn}");

            Console.WriteLine("\nKey point: Constants provide stability, variables provide flexibility\n");
        }

    }
}