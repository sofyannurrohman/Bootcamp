
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

// Inheritance Casting and Conversion

// 1. Upcasting (Book → Product)
Product prod1 = new Book("C++ Programming", "Sofyan", 39.99f);
Console.WriteLine("Upcasting: Book assigned to Product");
prod1.DisplayInfo(); // polymorphism in action
Console.WriteLine();

// 2. Downcasting (Product → Book)
Console.WriteLine("Downcasting: Product back to Book");
Book book1 = (Book)prod1;
Console.WriteLine($"Book Author: {book1.Author}");
Console.WriteLine();

// 3. Try "as" for safe casting
Console.WriteLine("Safe Casting using 'as' ");
Product prod2 = new Product("Laptop"); // not a Book
Book? book2 = prod2 as Book;
if (book2 != null)
{
    Console.WriteLine($"Book Author: {book2.Author}");
}
else
{
    Console.WriteLine("prod2 is not a Book");
}
Console.WriteLine();

// 4. Type checking with 'is'
Console.WriteLine("Type checking with 'is'");
if (prod1 is Book b1)
{
    Console.WriteLine($"prod1 IS a Book, Author: {b1.Author}");
}
if (prod2 is Book)
{
    Console.WriteLine("prod2 IS a Book");
}
else
{
    Console.WriteLine("prod2 is NOT a Book");
}
Console.WriteLine();

// 5. Custom conversion (operator)
Console.WriteLine("Custom conversion operator");
Book book3 = new Book("Clean Code", "Robert C. Martin", 39.99f);
string bookInfo = (string)book3; // explicit conversion
Console.WriteLine(bookInfo);

Console.ReadLine();
