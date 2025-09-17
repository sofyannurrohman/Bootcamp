namespace BootcampDay7.Demo;

/// <summary>
/// Demonstrates advanced cursor positioning and manipulation
/// Useful for creating interactive console interfaces and progress indicators
/// </summary>
public class DemoClassDACC{
    public static void DemonstrateAdvancedCursorControl()
    {
        try
        {
            // Save current position
            int startLeft = Console.CursorLeft;
            int startTop = Console.CursorTop;

            Console.WriteLine("Cursor positioning demonstration:");

            // Progress bar simulation using cursor manipulation
            Console.Write("Progress: [");
            int progressBarStart = Console.CursorLeft;
            int progressBarTop = Console.CursorTop;
            Console.Write("]   0%");

            // Simulate progress updates
            for (int i = 0; i <= 10; i++)
            {
                // Move cursor to progress bar position
                Console.SetCursorPosition(progressBarStart, progressBarTop);

                // Draw progress bar
                string progress = new string('█', i) + new string('░', 10 - i);
                Console.Write(progress);

                // Move cursor to percentage position
                Console.SetCursorPosition(progressBarStart + 12, progressBarTop);
                Console.Write($"{i * 10,3}%");

                // Small delay for visual effect
                System.Threading.Thread.Sleep(200);
            }

            Console.WriteLine(); // Move to next line

            // Demonstrate text replacement using cursor positioning
            Console.Write("Status: Initializing");
            int statusLeft = Console.CursorLeft - 12; // Position of "Initializing"
            int statusTop = Console.CursorTop;

            System.Threading.Thread.Sleep(500);
            Console.SetCursorPosition(statusLeft, statusTop);
            Console.Write("Processing  "); // Extra spaces to clear old text

            System.Threading.Thread.Sleep(500);
            Console.SetCursorPosition(statusLeft, statusTop);
            Console.Write("Complete    ");

            Console.WriteLine(); // Move to next line

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cursor manipulation failed: {ex.Message}");
            Console.WriteLine("This might happen in some console environments or when output is redirected");
        }
    }
}
