using EnumDemo;

var order1 = new Order { Id = 101, Status = OrderStatus.Pending };
var order2 = new Order { Id = 102, Status = OrderStatus.Shipped };

order1.ShowStatus(); // Order 101 is Pending
order2.ShowStatus(); // Order 102 is Shipped

// ✅ Casting enum to int
Console.WriteLine((int)OrderStatus.Delivered); // 3

// ✅ Casting int to enum
var status = (OrderStatus)1;
Console.WriteLine(status); // Paid

// ✅ Checking enum value
if (order1.Status == OrderStatus.Pending)
{
    Console.WriteLine("Order is waiting for payment.");
}

/* 
Personal Notes: 
Enums are heavily used in web development for representing things like:
> Http status code
> Order status
> Payment method
> User roles
> Error codes
*/