using Classes;
public class Book : Product
{
    public string Author { get; set; } = "";
    public Book(string name, string author, float price) : base(name) // Implement base keyword
    {
        Author = author;
        Price = price;
    }
    // Polymorph : Override BaseDisplayInfo int proper Book Info
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($" Book Author : {Author}");
    }
    public static explicit operator string(Book b)
    {
        return $"{b.Name} by {b.Author}, price {b.Price}";
    }
}