namespace AccessModifierDemo;

public class Product
{
    // public → can be accessed anywhere
    public string Name { get; set; }

    // private → can only be used inside Product
    private float price;

    // protected → accessible inside Product and subclasses
    protected string Category;

    // internal → accessible only in the same assembly (project)
    internal int Stock;

    // protected internal → accessible in same assembly or subclasses
    protected internal string Brand;

    // private protected → accessible only in same assembly + subclasses
    private protected string SecretCode;

    public Product(string name, float price)
    {
        Name = name;
        this.price = price;
        Category = "General";
        Stock = 10;
        Brand = "NoBrand";
        SecretCode = "ABC123";
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}, Price: {price}, Category: {Category}");
    }
}