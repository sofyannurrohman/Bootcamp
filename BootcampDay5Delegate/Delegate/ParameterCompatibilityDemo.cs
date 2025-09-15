namespace DelegateDemo;
public class DelegateClassPC{
    
public static void ParameterCompatibilityDemo()
    {
        Console.WriteLine("9. PARAMETER COMPATIBILITY - CONTRAVARIANCE");
        Console.WriteLine("===========================================");

        // Method that takes a more general parameter type
        void ActOnObject(object obj) => Console.WriteLine($"  Processing object: {obj}");

        // Delegate that expects a more specific parameter type
        Action<string> stringAction;

        // This works! String is more specific than object (contravariance)
        // When we call stringAction("hello"), the string gets passed to ActOnObject
        // and is implicitly upcast to object
        stringAction = ActOnObject;

        Console.WriteLine("Calling Action<string> delegate that points to method expecting object:");
        stringAction("Hello contravariance!");

        // Real-world example: event handling
        Console.WriteLine("\nReal-world example - event handling:");

        // Generic event handler that can handle any EventArgs
        void GenericEventHandler(object sender, EventArgs e)
        {
            Console.WriteLine($"  Event from {sender?.GetType().Name ?? "unknown"} at {DateTime.Now:HH:mm:ss}");
        }

        // Specific event handler delegate type
        Action<object, EventArgs> eventHandler = GenericEventHandler;

        // Can be used for specific event types due to contravariance
        eventHandler(new Program(), new EventArgs());

    }
}