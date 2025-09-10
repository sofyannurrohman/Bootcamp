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

        public static void DemonstrateBinaryOperators()
        {
            Console.WriteLine("=== BINARY OPERATORS ===");
            Console.WriteLine("Two operands, one operator - the workhorses of programming\n");

            // Arithmetic binary operators
            Console.WriteLine("--- Arithmetic Operators ---");
            int a = 12, b = 5;

            Console.WriteLine($"Starting values: a = {a}, b = {b}");
            Console.WriteLine($"Addition: {a} + {b} = {a + b}");
            Console.WriteLine($"Subtraction: {a} - {b} = {a - b}");
            Console.WriteLine($"Multiplication: {a} * {b} = {a * b}");
            Console.WriteLine($"Division: {a} / {b} = {a / b}");
            Console.WriteLine($"Remainder: {a} % {b} = {a % b}");

            // String concatenation - binary operator for strings
            Console.WriteLine("\n--- String Concatenation ---");
            string firstName = "John";
            string lastName = "Doe";
            string fullName = firstName + " " + lastName; // Binary + operator for strings

            Console.WriteLine($"First name: '{firstName}'");
            Console.WriteLine($"Last name: '{lastName}'");
            Console.WriteLine($"Full name: '{fullName}'");

            // Comparison operators
            Console.WriteLine("\n--- Comparison Operators ---");
            int x = 10, y = 20;

            Console.WriteLine($"Values: x = {x}, y = {y}");
            Console.WriteLine($"x == y: {x == y}");  // Equal to
            Console.WriteLine($"x != y: {x != y}");  // Not equal to
            Console.WriteLine($"x < y: {x < y}");    // Less than
            Console.WriteLine($"x > y: {x > y}");    // Greater than
            Console.WriteLine($"x <= y: {x <= y}");  // Less than or equal
            Console.WriteLine($"x >= y: {x >= y}");  // Greater than or equal

            // Logical operators
            Console.WriteLine("\n--- Logical Operators ---");
            bool isWeekend = true;
            bool hasFreetime = false;

            Console.WriteLine($"Is weekend: {isWeekend}");
            Console.WriteLine($"Has free time: {hasFreetime}");
            Console.WriteLine($"Can relax (AND): {isWeekend && hasFreetime}");
            Console.WriteLine($"Can enjoy (OR): {isWeekend || hasFreetime}");

            Console.WriteLine("\nRemember: Binary operators need exactly two operands to work\n");
        }
    }
}