using AccessModifierDemo;
public class Book : Product
{
    public string Author { get; set; }

    public Book(string name, float price, string author)
        : base(name, price)
    {
        Author = author;
    }

    public void ShowBookInfo()
    {
        // ✅ Can access protected, public, internal, protected internal
        Console.WriteLine($"Book: {Name}, Author: {Author}, Category: {Category}, Brand: {Brand}, Stock: {Stock}");

        // ❌ Can't access private price
        // ❌ Can't access private protected SecretCode (unless same assembly + subclass)
    }
}