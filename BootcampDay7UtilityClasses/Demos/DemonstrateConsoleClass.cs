/// <summary>
        /// Console Class Demonstration
        /// Shows input/output operations, formatting, console manipulation, and redirection
        /// The Console class is your primary tool for terminal-based user interaction
        /// </summary>
        static void DemonstrateConsoleClass()
        {
            Console.WriteLine("=== CONSOLE CLASS DEMONSTRATION ===");
            
            // Basic input and output operations
            Console.WriteLine("1. Basic Input/Output Operations:");
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine() ?? "Anonymous";
            Console.WriteLine($"Hello, {userName}! Welcome to the utility classes demo.");
            
            // Demonstrate different input methods
            Console.WriteLine("\n2. Different Input Methods:");
            Console.WriteLine("Press any key to continue...");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true = don't display the key
            Console.WriteLine($"You pressed: {keyInfo.Key}");
            
            // Composite formatting - this is how we format output in the real world
            Console.WriteLine("\n3. Composite Formatting:");
            int progress = 75;
            double percentage = 87.5;
            Console.WriteLine("Task progress: {0}% completed", progress);
            Console.WriteLine("Overall completion: {0:F2}%", percentage);
            Console.WriteLine("Status: {0} out of {1} items processed", 15, 20);
            
            // Console appearance and cursor manipulation
            Console.WriteLine("\n4. Console Appearance and Cursor Control:");
            DemonstrateConsoleAppearanceControl();
            
            // Cursor positioning - useful for creating interactive interfaces
            Console.WriteLine("\n5. Advanced Cursor Manipulation:");
            DemonstrateAdvancedCursorControl();
            
            // Console redirection demonstration
            Console.WriteLine("\n6. Console Stream Redirection:");
            DemonstrateConsoleRedirection();
            
            Console.WriteLine();
        }