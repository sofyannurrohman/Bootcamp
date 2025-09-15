namespace BootcampDay5.EventHandler
{
    public class EventHandlerSEP
    {
        public static void StandardEventPatternDemo()
        {
            Console.WriteLine("3. STANDARD EVENT PATTERN - PROPER .NET CONVENTION");
            Console.WriteLine("==================================================");

            // Create a stock using the standard pattern
            var stock = new Stock("MSFT");
            var portfolio = new Portfolio("Tech Stocks");

            // Subscribe using the standard EventHandler<T> pattern
            stock.PriceChanged += portfolio.OnPriceChanged;
            stock.PriceChanged += (sender, e) =>
            {
                if (Math.Abs(e.PercentChange) > 5)
                    Console.WriteLine($"  ALERT: Large price movement detected!");
            };

            Console.WriteLine($"Created stock: {stock.Symbol}");
            Console.WriteLine("Subscribed portfolio manager and alert system");
            Console.WriteLine("Making price changes...\n");

            // Set initial price and then change it
            stock.Price = 300.00m;
            stock.Price = 315.50m;  // 5.17% increase
            stock.Price = 298.25m;  // 5.47% decrease
            stock.Price = 298.25m;  // No change - won't trigger event

            Console.WriteLine();
        }
    }

    // Stock class implementing the standard .NET event pattern
    public class Stock
    {
        private decimal _price;

        public string Symbol { get; }

        public Stock(string symbol)
        {
            Symbol = symbol;
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    decimal oldPrice = _price;
                    _price = value;

                    // Create proper EventArgs and fire the event
                    OnPriceChanged(new PriceChangedEventArgs(Symbol, oldPrice, value));
                }
            }
        }

        // Declare the event
        public event EventHandler<PriceChangedEventArgs>? PriceChanged;

        // Protected virtual method to raise the event
        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }
    }

    // Custom EventArgs class
    public class PriceChangedEventArgs : EventArgs
    {
        public string Symbol { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }
        public decimal PercentChange => (OldPrice == 0) ? 0 : ((NewPrice - OldPrice) / OldPrice) * 100;

        public PriceChangedEventArgs(string symbol, decimal oldPrice, decimal newPrice)
        {
            Symbol = symbol;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }

    // Example subscriber class
    public class Portfolio
    {
        public string Name { get; }

        public Portfolio(string name)
        {
            Name = name;
        }

        public void OnPriceChanged(object? sender, PriceChangedEventArgs e)
        {
            Console.WriteLine($"[{Name}] Stock {e.Symbol} changed from {e.OldPrice:C} to {e.NewPrice:C} ({e.PercentChange:F2}%).");
        }
    }
}
