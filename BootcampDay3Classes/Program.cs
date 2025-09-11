using Classes;

//Create Product 1
Product product1 = new Product();
product1.Name = "Laptop";
product1.Price = 899.99f;

// Show product info
product1.DisplayInfo();

Console.WriteLine();

// Create Product 2 with name constructor
Product product2 = new Product("Smartphone");
product2.Price = 499.50f;

Console.WriteLine(product2.ToString());

Console.ReadLine();