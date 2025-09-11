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
/* Personal Note:
> Overused nesting → Deeply nested types (ClassA.ClassB.ClassC) make code harder to read.
> Shared/reusable types → If multiple classes need to use the same type, don’t nest — put it at the namespace level.
> Large parent classes → Nesting makes them more bloated and less maintainable.
🔹 Best Practices
> Keep nested types small and specific.
> Use nested enums/structs for internal concepts of the parent.
> For reusable types, always make them top-level classes.
> Don’t nest just to "organize" — prefer folders/namespaces for that.
*/