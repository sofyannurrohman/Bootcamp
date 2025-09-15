
namespace BootcampDay5.EventHandler;
public class EventHandlerClassEM{
    public static void EventModifiersDemo()
    {
        Console.WriteLine("5. EVENT MODIFIERS - VIRTUAL, OVERRIDE, STATIC");
        Console.WriteLine("===============================================");

        // Demonstrate static events
        Console.WriteLine("Static event demonstration:");
        SystemMonitor.SystemAlert += (msg) => Console.WriteLine($"  System Alert: {msg}");
        SystemMonitor.TriggerAlert("High CPU usage detected");

        // Demonstrate virtual/override events
        Console.WriteLine("\nVirtual/Override event demonstration:");
        BaseService baseService = new EnhancedService();

        baseService.StatusChanged += (sender, status) =>
            Console.WriteLine($"  Status update from {sender.GetType().Name}: {status}");

        // This will use the overridden event behavior
        baseService.ChangeStatus("Enhanced service is running");

        Console.WriteLine();
    }
}

// Static events example
public static class SystemMonitor
{
    // Static event belongs to the type, not an instance
    public static event Action<string>? SystemAlert;

    public static void TriggerAlert(string alertMessage)
    {
        SystemAlert?.Invoke(alertMessage);
    }
}

        // Base class with virtual event
        public class BaseService
        {
            // Virtual event can be overridden in derived classes
            public virtual event Action<object, string>? StatusChanged;

            public virtual void ChangeStatus(string status)
            {
                OnStatusChanged(status);
            }

            protected virtual void OnStatusChanged(string status)
            {
                StatusChanged?.Invoke(this, status);
            }
        }

        // Derived class overriding the virtual event
        public class EnhancedService : BaseService
        {
            // Override the virtual event with enhanced behavior
            public override event Action<object, string>? StatusChanged;

            protected override void OnStatusChanged(string status)
            {
                // Enhanced logging before firing the event
                Console.WriteLine($"  Enhanced service logging: Status changing to '{status}'");
                StatusChanged?.Invoke(this, $"[ENHANCED] {status}");
            }
        }