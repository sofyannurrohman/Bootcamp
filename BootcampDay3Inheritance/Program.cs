
using Classes;
// Create a regular Product
Product product = new Product("Laptop");
product.Price = 899.99f;
product.DisplayInfo();

Console.WriteLine();

// Using Inheritance Class to create Book Product
Book book = new Book("C# Programming", "Sofyan", 29.99f);
book.DisplayInfo();

Console.ReadLine();