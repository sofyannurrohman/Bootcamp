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
        public static void DemonstrateAssignmentExpressions()
        {
            Console.WriteLine("=== ASSIGNMENT EXPRESSIONS ===");
            Console.WriteLine("Storing values and building chains - the backbone of state management\n");

            // Basic assignment
            Console.WriteLine("--- Basic Assignment ---");
            int x = 10;  // Assignment expression that evaluates to 10
            Console.WriteLine($"x = 10 assigns and evaluates to: {x}");

            // Assignment as part of larger expressions
            Console.WriteLine("\n--- Assignment Within Expressions ---");
            int y = 5 * (x = 7);  // First x gets 7, then 5 * 7 = 35 goes to y
            Console.WriteLine($"After y = 5 * (x = 7):");
            Console.WriteLine($"x = {x}");
            Console.WriteLine($"y = {y}");

            // Chained assignments
            Console.WriteLine("\n--- Chained Assignments ---");
            int a, b, c, d;
            a = b = c = d = 100;  // All variables get 100

            Console.WriteLine("After a = b = c = d = 100:");
            Console.WriteLine($"a = {a}, b = {b}, c = {c}, d = {d}");

            // Compound assignment operators
            Console.WriteLine("\n--- Compound Assignment Operators ---");
            int value = 20;
            Console.WriteLine($"Starting value: {value}");

            value += 10;  // Equivalent to: value = value + 10
            Console.WriteLine($"After value += 10: {value}");

            value -= 5;   // Equivalent to: value = value - 5
            Console.WriteLine($"After value -= 5: {value}");

            value *= 2;   // Equivalent to: value = value * 2
            Console.WriteLine($"After value *= 2: {value}");

            value /= 3;   // Equivalent to: value = value / 3
            Console.WriteLine($"After value /= 3: {value}");

            value %= 7;   // Equivalent to: value = value % 7
            Console.WriteLine($"After value %= 7: {value}");

            // String compound assignment
            Console.WriteLine("\n--- String Compound Assignment ---");
            string message = "Hello";
            Console.WriteLine($"Starting message: '{message}'");

            message += " World";
            Console.WriteLine($"After message += \" World\": '{message}'");

            message += "!";
            Console.WriteLine($"After message += \"!\": '{message}'");

            // Real-world example: score tracking
            Console.WriteLine("\n--- Real-World Example: Game Score Tracking ---");
            int playerScore = 0;
            int round = 1;

            Console.WriteLine($"Game starts - Round {round}, Score: {playerScore}");

            playerScore += 150;  // Player scores points
            Console.WriteLine($"Round {round++} complete - Score: {playerScore}");

            playerScore += 200;  // Another round
            Console.WriteLine($"Round {round++} complete - Score: {playerScore}");

            playerScore -= 50;   // Penalty
            Console.WriteLine($"Penalty applied - Score: {playerScore}");

            playerScore *= 2;    // Bonus multiplier
            Console.WriteLine($"Bonus multiplier applied - Final Score: {playerScore}");

            Console.WriteLine("\nAssignment expressions are your primary tool for managing program state\n");
        }
        public static void DemonstrateNestedExpressions()
        {
            Console.WriteLine("=== NESTED EXPRESSIONS ===");
            Console.WriteLine("Building complex logic by combining simpler expressions\n");
            
            Console.WriteLine("--- Basic Nesting with Parentheses ---");
            // Simple nested expression from the material
            int result1 = 1 + (12 * 30);
            Console.WriteLine($"1 + (12 * 30) = {result1}");
            Console.WriteLine("The parentheses force multiplication to happen first");
            
            // More complex nesting
            Console.WriteLine("\n--- Complex Nested Expressions ---");
            int a = 5, b = 10, c = 2;
            
            int result2 = (a + b) * c;
            Console.WriteLine($"({a} + {b}) * {c} = {result2}");
            
            int result3 = a + (b * c);
            Console.WriteLine($"{a} + ({b} * {c}) = {result3}");
            
            // Deeply nested expression
            int result4 = ((a + b) * c) + (a * (b - c));
            Console.WriteLine($"(({a} + {b}) * {c}) + ({a} * ({b} - {c})) = {result4}");
            
            // Real-world example: calculating compound interest
            Console.WriteLine("\n--- Real-World Example: Compound Interest ---");
            double principal = 1000.0;  // Initial amount
            double rate = 0.05;         // 5% annual rate
            int years = 3;              // 3 years
            int compoundsPerYear = 12;  // Monthly compounding
            
            // A = P(1 + r/n)^(nt)
            double amount = principal * Math.Pow((1 + (rate / compoundsPerYear)), (compoundsPerYear * years));
            
            Console.WriteLine($"Principal: ${principal:F2}");
            Console.WriteLine($"Rate: {rate * 100}% annually");
            Console.WriteLine($"Time: {years} years, compounded {compoundsPerYear} times per year");
            Console.WriteLine($"Final amount: ${amount:F2}");
            Console.WriteLine($"Interest earned: ${amount - principal:F2}");
            
            // Boolean logic nesting
            Console.WriteLine("\n--- Nested Boolean Logic ---");
            int age = 25;
            int income = 45000;
            bool hasGoodCredit = true;
            bool isEmployed = true;
            
            // Complex eligibility check
            bool isEligibleForLoan = (age >= 18 && age <= 65) && 
                                   (income >= 30000) && 
                                   (hasGoodCredit || (isEmployed && income >= 40000));
            
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Income: ${income}");
            Console.WriteLine($"Good credit: {hasGoodCredit}");
            Console.WriteLine($"Employed: {isEmployed}");
            Console.WriteLine($"Loan eligible: {isEligibleForLoan}");
            
            Console.WriteLine("\nKey lesson: Use parentheses to make your intentions crystal clear\n");
        }
    }
    

}