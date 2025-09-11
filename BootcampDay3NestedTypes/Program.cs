using NestedTypesDemo;
var product = new Product { Name = "Laptop" };
Console.WriteLine($"Product: {product.Name}");

// Call instance of nested type
var detail = new Product.ProductDetail
{
    Description = "Gaming Laptop",
    Price = 1500
};
detail.ShowDetail();