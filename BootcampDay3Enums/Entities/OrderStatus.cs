namespace EnumDemo;

public enum OrderStatus
{
    Pending,    // 0
    Paid,       // 1
    Shipped,    // 2
    Delivered,  // 3
    Cancelled   // 4
}

public class Order
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }

    public void ShowStatus()
    {
        Console.WriteLine($"Order {Id} is {Status}");
    }
}