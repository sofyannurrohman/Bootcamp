 /// <summary>
        /// Demonstrates console appearance control including colors and window properties
        /// Essential for creating user-friendly console applications
        /// </summary>
        static void DemonstrateConsoleAppearanceControl()
        {
            // Save original settings to restore later
            ConsoleColor originalForeground = Console.ForegroundColor;
            ConsoleColor originalBackground = Console.BackgroundColor;
            
            Console.WriteLine("Console color demonstrations:");
            
            // Different message types with appropriate colors
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SUCCESS: Operation completed successfully");
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: Something went wrong");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("WARNING: Proceed with caution");
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("INFO: General information message");
            
            // Background color demonstration
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Highlighted message with background color");
            
            // Reset to original colors
            Console.ForegroundColor = originalForeground;
            Console.BackgroundColor = originalBackground;
            
            // Window properties (be careful with these in production)
            try
            {
                Console.WriteLine($"\nCurrent console window properties:");
                Console.WriteLine($"  Window width: {Console.WindowWidth} characters");
                Console.WriteLine($"  Window height: {Console.WindowHeight} characters");
                Console.WriteLine($"  Buffer width: {Console.BufferWidth} characters");
                Console.WriteLine($"  Buffer height: {Console.BufferHeight} characters");
                Console.WriteLine($"  Largest possible window: {Console.LargestWindowWidth} x {Console.LargestWindowHeight}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Console window properties not available: {ex.Message}");
            }
        }