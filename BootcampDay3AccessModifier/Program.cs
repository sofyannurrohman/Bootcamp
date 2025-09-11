using AccessModifierDemo;
var product = new Product("Laptop", 1500);
product.ShowInfo();

// ✅ Can access public
Console.WriteLine(product.Name);

// ❌ Can't access private
// Console.WriteLine(product.price);

// ❌ Can't access protected
// Console.WriteLine(product.Category);

// ✅ Can access internal (same project)
Console.WriteLine(product.Stock);

var book = new Book("C# Guide", 500, "John Doe");
book.ShowBookInfo();

/* 
Personal Note:
public → Accessible from anywhere.

private → Accessible only inside the same class/struct.

protected → Accessible inside the class and subclasses (inheritance).

internal → Accessible only within the same assembly (project).

protected internal → Accessible within the same assembly OR subclasses.

private protected → Accessible only inside the class OR subclasses within the same assembly.
*/