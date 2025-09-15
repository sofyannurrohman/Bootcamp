namespace BootcampDay5.EventHandler;

public class EventHandlerClassEAD
{

    public static void EventAccessorsDemo()
    {
        Console.WriteLine("4. CUSTOM EVENT ACCESSORS - ADVANCED CONTROL");
        Console.WriteLine("============================================");

        var smartNotifier = new SmartNotificationSystem();

        void Handler1(string msg) => Console.WriteLine($"  Handler 1: {msg}");
        void Handler2(string msg) => Console.WriteLine($"  Handler 2: {msg}");
        void Handler3(string msg) => Console.WriteLine($"  Handler 3: {msg}");

        // The custom accessors will track subscription changes
        Console.WriteLine("Adding subscribers (watch the custom accessor behavior):");
        smartNotifier.MessageReceived += Handler1;
        smartNotifier.MessageReceived += Handler2;
        smartNotifier.MessageReceived += Handler3;

        Console.WriteLine($"\nTotal subscribers: {smartNotifier.SubscriberCount}");
        smartNotifier.SendMessage("Hello everyone!");

        Console.WriteLine("\nRemoving one subscriber:");
        smartNotifier.MessageReceived -= Handler2;
        Console.WriteLine($"Remaining subscribers: {smartNotifier.SubscriberCount}");
        smartNotifier.SendMessage("Handler 2 should be gone");

        Console.WriteLine();
    }
}
// Class demonstrating explicit event accessors
    public class SmartNotificationSystem
    {
        // Private delegate field - the compiler normally generates this automatically
        private Action<string>? _messageReceived;
        private int _subscriberCount = 0;

        public int SubscriberCount => _subscriberCount;

        // Explicit event accessors - we control what happens during add/remove
        public event Action<string> MessageReceived
        {
            add
            {
                Console.WriteLine($"  Adding subscriber (current count: {_subscriberCount})");
                _messageReceived += value;
                _subscriberCount++;
            }
            remove
            {
                Console.WriteLine($"  Removing subscriber (current count: {_subscriberCount})");
                _messageReceived -= value;
                _subscriberCount--;
            }
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending message: {message}");
            // Invoke the private delegate field directly
            _messageReceived?.Invoke(message);
        }
    }