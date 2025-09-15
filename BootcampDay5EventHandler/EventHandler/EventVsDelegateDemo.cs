namespace BootcampDay5.EventHandler;

public class EventHandlerClassEVD
{
    // Class using proper event (safe)
    public class EventPublisher
    {
        public event Action<string>? SafeNotification;

        public void TriggerEvent(string message)
        {
            Console.WriteLine($"Event publisher sending: {message}");
            SafeNotification?.Invoke(message);
        }
    }

    // Class using raw delegate (unsafe)
    public class DelegatePublisher
    {
        // This is just a public delegate field - dangerous!
        public Action<string>? UnsafeNotification;

        public void TriggerEvent(string message)
        {
            Console.WriteLine($"Delegate publisher sending: {message}");
            UnsafeNotification?.Invoke(message);
        }
    }
    public static void EventVsDelegateDemo()
    {
        Console.WriteLine("2. EVENT VS DELEGATE - SAFETY COMPARISON");
        Console.WriteLine("========================================");

        var eventPublisher = new EventPublisher();
        var delegatePublisher = new DelegatePublisher();

        // Subscribe to both
        eventPublisher.SafeNotification += msg => Console.WriteLine($"  Event received: {msg}");
        delegatePublisher.UnsafeNotification += msg => Console.WriteLine($"  Delegate received: {msg}");

        Console.WriteLine("Both subscribed successfully");

        // These work fine for both
        eventPublisher.TriggerEvent("Hello from event");
        delegatePublisher.TriggerEvent("Hello from delegate");

        Console.WriteLine("\nTesting safety differences:");

        // Try to do dangerous things - these will show the difference
        // eventPublisher.SafeNotification = null;        // Compile error - can't assign to event
        // eventPublisher.SafeNotification("hack");       // Compile error - can't invoke from outside

        // But with delegate, these dangerous operations are possible:
        Console.WriteLine("Delegate allows dangerous operations:");
        delegatePublisher.UnsafeNotification = null; // Wipes out all subscribers!
        delegatePublisher.TriggerEvent("This won't be received by anyone");

        Console.WriteLine("Event safety prevents subscriber interference\n");
    }
}