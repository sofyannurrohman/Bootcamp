using Classes;
object num = 42; // boxing

int realNum = (int)num; // unboxing
Console.WriteLine(realNum + 10); // 52

object[] store = {
            new Product { Name = "Laptop" },
            new Book ( "C# Fundamentals", "John Doe", 30.09f ),
            "Just a string",
            100
        };

foreach (var item in store)
{
    Console.WriteLine($"Item: {item.ToString()} and type : {item.GetType()}");
}