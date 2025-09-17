namespace BootcampDay7.Demo;
/// <summary>
/// AppContext Class Demonstration
/// Shows runtime information and feature switch management
/// Essential for modern .NET applications that need configuration flexibility
/// </summary>
public class DemoClassDACC{
    public static void DemonstrateAppContextClass()
    {
        Console.WriteLine("=== APPCONTEXT CLASS DEMONSTRATION ===");

        // Basic application context information
        Console.WriteLine("1. Application Context Information:");
        Console.WriteLine($"Base Directory: {AppContext.BaseDirectory}");
        Console.WriteLine($"Target Framework: {AppContext.TargetFrameworkName ?? "Unknown"}");

        // Feature switches - this is how you control features at runtime
        Console.WriteLine("\n2. Feature Switch Management:");
        DemonstrateFeatureSwitches();

        // Data directory management
        Console.WriteLine("\n3. Data Directory Management:");
        string dataDir = Path.Combine(AppContext.BaseDirectory, "Data");
        Console.WriteLine($"Application data directory: {dataDir}");
        Console.WriteLine($"Directory exists: {Directory.Exists(dataDir)}");

        // Configuration and settings scenarios
        Console.WriteLine("\n4. Real-world Usage Scenarios:");
        DemonstrateRealWorldScenarios();

        Console.WriteLine();
    }
        /// <summary>
        /// Demonstrates feature switch functionality
        /// Feature switches are crucial for A/B testing and gradual feature rollouts
        /// </summary>
        static void DemonstrateFeatureSwitches()
        {
            // Set up feature switches for different application features
            AppContext.SetSwitch("MyApp.NewUIEnabled", true);
            AppContext.SetSwitch("MyApp.AdvancedLogging", false);
            AppContext.SetSwitch("MyApp.BetaFeatures", true);
            AppContext.SetSwitch("MyApp.DebugMode", true);
            
            Console.WriteLine("Feature switches configured:");
            
            // Check and act on feature switches
            string[] features = { 
                "MyApp.NewUIEnabled", 
                "MyApp.AdvancedLogging", 
                "MyApp.BetaFeatures", 
                "MyApp.DebugMode",
                "MyApp.NonExistentFeature" 
            };
            
            foreach (string feature in features)
            {
                if (AppContext.TryGetSwitch(feature, out bool isEnabled))
                {
                    Console.WriteLine($"  {feature}: {(isEnabled ? "ENABLED" : "DISABLED")}");
                    
                    // Simulate feature-specific behavior
                    switch (feature)
                    {
                        case "MyApp.NewUIEnabled" when isEnabled:
                            Console.WriteLine("    → Loading new UI components");
                            break;
                        case "MyApp.AdvancedLogging" when isEnabled:
                            Console.WriteLine("    → Enabling detailed logging");
                            break;
                        case "MyApp.BetaFeatures" when isEnabled:
                            Console.WriteLine("    → Unlocking beta functionality");
                            break;
                        case "MyApp.DebugMode" when isEnabled:
                            Console.WriteLine("    → Debug mode active - extra diagnostics available");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"  {feature}: NOT SET (using default behavior)");
                }
            }
        }
        
}
