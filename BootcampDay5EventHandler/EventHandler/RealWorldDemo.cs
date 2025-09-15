namespace BootcampDay5.EventHandler;
public class EventHandlerClassREC{
    
public static void RealWorldECommerceDemo()
    {
        Console.WriteLine("7. REAL-WORLD SCENARIO - E-COMMERCE ORDER SYSTEM");
        Console.WriteLine("=================================================");

        // Set up the order processing system
        var orderSystem = new OrderProcessingSystem();
        var emailService = new EmailService();
        var inventorySystem = new InventorySystem();
        var auditLogger = new AuditLogger();
        var loyaltyProgram = new LoyaltyProgram();

        // Wire up all the event handlers - this is the beauty of events
        // Each service can independently subscribe to the events it cares about
        orderSystem.OrderPlaced += emailService.OnOrderPlaced;
        orderSystem.OrderPlaced += inventorySystem.OnOrderPlaced;
        orderSystem.OrderPlaced += auditLogger.OnOrderPlaced;
        orderSystem.OrderPlaced += loyaltyProgram.OnOrderPlaced;

        orderSystem.OrderCancelled += emailService.OnOrderCancelled;
        orderSystem.OrderCancelled += inventorySystem.OnOrderCancelled;
        orderSystem.OrderCancelled += auditLogger.OnOrderCancelled;

        // Process some orders - watch how all systems respond automatically
        Console.WriteLine("Processing customer orders...\n");

        var order1 = new CustomerOrder(1001, "john.doe@email.com", 299.99m, "Laptop");
        var order2 = new CustomerOrder(1002, "jane.smith@email.com", 89.50m, "Wireless Mouse");

        orderSystem.PlaceOrder(order1);
        Console.WriteLine();

        orderSystem.PlaceOrder(order2);
        Console.WriteLine();

        // Cancel an order
        orderSystem.CancelOrder(order1);
        Console.WriteLine();
    }
}
// Order data structure
public class CustomerOrder
{
    public int OrderId { get; }
    public string CustomerEmail { get; }
    public decimal Amount { get; }
    public string ProductName { get; }
    public DateTime OrderTime { get; }

    public CustomerOrder(int orderId, string customerEmail, decimal amount, string productName)
    {
        OrderId = orderId;
        CustomerEmail = customerEmail;
        Amount = amount;
        ProductName = productName;
        OrderTime = DateTime.Now;
    }
}

    // EventArgs for order events
    public class OrderEventArgs : EventArgs
    {
        public CustomerOrder Order { get; }
        public DateTime EventTime { get; }

        public OrderEventArgs(CustomerOrder order)
        {
            Order = order;
            EventTime = DateTime.Now;
        }
    }

    // Main order processing system - the event broadcaster
    public class OrderProcessingSystem
    {
        // Multiple events for different business scenarios
        public event EventHandler<OrderEventArgs>? OrderPlaced;
        public event EventHandler<OrderEventArgs>? OrderCancelled;

        public void PlaceOrder(CustomerOrder order)
        {
            Console.WriteLine($"Order System: Processing order #{order.OrderId} for {order.CustomerEmail}");
            Console.WriteLine($"  Product: {order.ProductName}, Amount: ${order.Amount}");
            
            // Fire the event - all interested systems will be notified automatically
            OnOrderPlaced(new OrderEventArgs(order));
        }

        public void CancelOrder(CustomerOrder order)
        {
            Console.WriteLine($"Order System: Cancelling order #{order.OrderId}");
            
            OnOrderCancelled(new OrderEventArgs(order));
        }

        protected virtual void OnOrderPlaced(OrderEventArgs e)
        {
            OrderPlaced?.Invoke(this, e);
        }

        protected virtual void OnOrderCancelled(OrderEventArgs e)
        {
            OrderCancelled?.Invoke(this, e);
        }
    }

    // Email service - independent subscriber
    public class EmailService
    {
        public void OnOrderPlaced(object? sender, OrderEventArgs e)
        {
            Console.WriteLine($"  Email Service: Sending confirmation to {e.Order.CustomerEmail}");
            Console.WriteLine($"    'Your order #{e.Order.OrderId} has been confirmed'");
        }

        public void OnOrderCancelled(object? sender, OrderEventArgs e)
        {
            Console.WriteLine($"  Email Service: Sending cancellation notice to {e.Order.CustomerEmail}");
            Console.WriteLine($"    'Your order #{e.Order.OrderId} has been cancelled'");
        }
    }

    // Inventory system - another independent subscriber
    public class InventorySystem
    {
        public void OnOrderPlaced(object? sender, OrderEventArgs e)
        {
            Console.WriteLine($"  Inventory System: Reserving {e.Order.ProductName} for order #{e.Order.OrderId}");
            Console.WriteLine($"    Inventory levels updated");
        }

        public void OnOrderCancelled(object? sender, OrderEventArgs e)
        {
            Console.WriteLine($"  Inventory System: Releasing reserved {e.Order.ProductName}");
            Console.WriteLine($"    Item returned to available inventory");
        }
    }

    // Audit logging - tracks all order activities
    public class AuditLogger
    {
        public void OnOrderPlaced(object? sender, OrderEventArgs e)
        {
            Console.WriteLine($"  Audit Logger: ORDER_PLACED - ID:{e.Order.OrderId}, " +
                            $"Customer:{e.Order.CustomerEmail}, Amount:${e.Order.Amount}");
        }

        public void OnOrderCancelled(object? sender, OrderEventArgs e)
        {
            Console.WriteLine($"  Audit Logger: ORDER_CANCELLED - ID:{e.Order.OrderId}");
        }
    }

    // Loyalty program - calculates reward points
    public class LoyaltyProgram
    {
        public void OnOrderPlaced(object? sender, OrderEventArgs e)
        {
            int points = (int)(e.Order.Amount / 10); // 1 point per $10 spent
            Console.WriteLine($"  Loyalty Program: Adding {points} points to {e.Order.CustomerEmail}");
            Console.WriteLine($"    Customer rewards balance updated");
        }
    }