using ExpressionMethodHelper;
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
        public static void DemonstrateUnaryOperators()
        {
            Console.WriteLine("=== UNARY OPERATORS ===");
            Console.WriteLine("One operand, powerful effects - the precision tools of programming\n");

            // Increment and decrement operators
            Console.WriteLine("--- Increment and Decrement Operators ---");
            int counter = 5;

            Console.WriteLine($"Starting counter: {counter}");
            Console.WriteLine($"counter++: {counter++} (post-increment, shows {counter - 1}, then increments)");
            Console.WriteLine($"Current counter: {counter}");
            Console.WriteLine($"++counter: {++counter} (pre-increment, increments first, then shows {counter})");
            Console.WriteLine($"Current counter: {counter}");

            Console.WriteLine($"counter--: {counter--} (post-decrement, shows {counter + 1}, then decrements)");
            Console.WriteLine($"Current counter: {counter}");
            Console.WriteLine($"--counter: {--counter} (pre-decrement, decrements first, then shows {counter})");
            Console.WriteLine($"Final counter: {counter}");

            // Unary plus and minus
            Console.WriteLine("\n--- Unary Plus and Minus ---");
            int positive = 42;
            int negative = -positive;  // Unary minus
            int stillPositive = +positive;  // Unary plus (rarely used)

            Console.WriteLine($"Original: {positive}");
            Console.WriteLine($"Negated: {negative}");
            Console.WriteLine($"Explicitly positive: {stillPositive}");

            // Logical NOT operator
            Console.WriteLine("\n--- Logical NOT Operator ---");
            bool isActive = true;
            bool isInactive = !isActive;  // Logical NOT

            Console.WriteLine($"Is active: {isActive}");
            Console.WriteLine($"Is inactive: {isInactive}");

            // Practical example with user status
            bool isLoggedIn = false;
            bool needsLogin = !isLoggedIn;

            Console.WriteLine($"User logged in: {isLoggedIn}");
            Console.WriteLine($"Needs to login: {needsLogin}");

            // Bitwise NOT operator (complement)
            Console.WriteLine("\n--- Bitwise NOT Operator ---");
            byte value = 5;  // Binary: 00000101
            byte complement = (byte)~value;  // Binary: 11111010

            Console.WriteLine($"Original value: {value} (binary: {Convert.ToString(value, 2).PadLeft(8, '0')})");
            Console.WriteLine($"Bitwise NOT: {complement} (binary: {Convert.ToString(complement, 2).PadLeft(8, '0')})");

            // Type casting operator
            Console.WriteLine("\n--- Type Casting (Conversion) Operators ---");
            double preciseValue = 123.789;
            int roundedDown = (int)preciseValue;  // Explicit cast

            Console.WriteLine($"Original double: {preciseValue}");
            Console.WriteLine($"Cast to int: {roundedDown} (truncated, not rounded)");

            // typeof operator
            Console.WriteLine("\n--- typeof Operator ---");
            Type stringType = typeof(string);
            Type intType = typeof(int);

            Console.WriteLine($"Type of string: {stringType}");
            Console.WriteLine($"Type of int: {intType}");

            Console.WriteLine("\nUnary operators are compact but powerful - master them for cleaner code\n");
        }
        public static void DemonstrateVoidExpressions()
        {
            Console.WriteLine("=== VOID EXPRESSIONS ===");
            Console.WriteLine("Operations that do work but don't return values\n");

            Console.WriteLine("--- Understanding Void Methods ---");
            Console.WriteLine("Methods like Console.WriteLine() perform actions but return nothing");

            // This is a void expression - it does something but returns no value
            Console.WriteLine("This line itself is a void expression!");

            // Demonstrating that void expressions can't be used as operands
            Console.WriteLine("\n--- Why Void Expressions Can't Be Operands ---");
            Console.WriteLine("The following would cause compile errors:");
            Console.WriteLine("// int x = 1 + Console.WriteLine(\"Hello\"); // ERROR!");
            Console.WriteLine("// string result = \"Result: \" + PrintMessage(); // ERROR!");

            // Valid uses of void expressions
            Console.WriteLine("\n--- Valid Uses of Void Expressions ---");
           MethodHelper.PrintWelcomeMessage();  // Called as a statement

            int count = 5;
            MethodHelper.ProcessData(count);     // Called as a statement

            // Void expressions in control structures
            if (DateTime.Now.Hour < 12)
            {
                MethodHelper.PrintMorningGreeting();  // Void expression in if block
            }
            else
            {
                MethodHelper.PrintAfternoonGreeting(); // Void expression in else block
            }

            // Void expressions in loops
            Console.WriteLine("\n--- Void Expressions in Loops ---");
            for (int i = 1; i <= 3; i++)
            {
                MethodHelper.PrintCountdown(i);  // Void expression in loop
            }

            Console.WriteLine("\nKey point: Void expressions do important work, they just don't give you a value back\n");
        }
    }

}