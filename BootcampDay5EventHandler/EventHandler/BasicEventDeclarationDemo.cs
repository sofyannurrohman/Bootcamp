namespace BootcampDay5.EventHandler;

public class EventHandlerClassBED
{
    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);
    public class BasicPriceMonitor
    {
        private decimal _currentPrice;

        public string Symbol { get; }

        // Event declaration - this is the key difference from a regular delegate field
        // Outside classes can only use += and -= on this event
        public event PriceChangedHandler? PriceChanged;

        public BasicPriceMonitor(string symbol)
        {
            Symbol = symbol;
            _currentPrice = 0;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (_currentPrice != newPrice)
            {
                decimal oldPrice = _currentPrice;
                _currentPrice = newPrice;

                // Inside the class, we can invoke the event like a regular delegate
                // This will call ALL subscribed methods in the order they were added
                PriceChanged?.Invoke(oldPrice, newPrice);
            }
        }
    }
    public static void BasicEventDeclarationDemo()
    {
        Console.WriteLine("1. BASIC EVENT DECLARATION - BROADCASTER/SUBSCRIBER PATTERN");
        Console.WriteLine("==========================================================");

        // Create a basic broadcaster using custom delegate
        var priceMonitor = new BasicPriceMonitor("AAPL");

        // Create subscribers - these are just methods that match our delegate signature
        void Trader1Handler(decimal oldPrice, decimal newPrice) =>
            Console.WriteLine($"  Trader 1: Price changed from ${oldPrice} to ${newPrice}");

        void Trader2Handler(decimal oldPrice, decimal newPrice) =>
            Console.WriteLine($"  Trader 2: Significant move! ${oldPrice} -> ${newPrice}");

        // Subscribe to the event - notice we can only use += and -= from outside the class
        priceMonitor.PriceChanged += Trader1Handler;
        priceMonitor.PriceChanged += Trader2Handler;

        Console.WriteLine("Subscribed two traders to price changes");
        Console.WriteLine("Triggering price changes...\n");

        // Trigger some price changes - this will notify all subscribers
        priceMonitor.UpdatePrice(150.00m);
        priceMonitor.UpdatePrice(155.50m);

        // Remove one subscriber
        priceMonitor.PriceChanged -= Trader1Handler;
        Console.WriteLine("\nTrader 1 unsubscribed. Only Trader 2 should receive this update:");
        priceMonitor.UpdatePrice(152.75m);

        Console.WriteLine();
    }

}