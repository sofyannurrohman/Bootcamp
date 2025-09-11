namespace Classes
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public float Price { get; set; }
        public string Description => $"Here the description of product {Name} with only $ {Price} you got a good value product.";

        public Product()
        {
            Console.WriteLine($"Created a product");
        }
        public Product(string name) : this()
        {
            Name = name;
            Console.WriteLine($"Created a product with name {name}");
        }
        public virtual void DisplayInfo()
        {
            Console.WriteLine($" Product Information");
            Console.WriteLine($" Product Name : {Name}");
            Console.WriteLine($" Product Price : {Price}");
            Console.WriteLine($" Product Description :{Description}");
        }
        public override string ToString()
        {
            return Description;
        }

    }

    public class Book : Product
    {
        public string Author { get; set; } = "";
        public Book(string name, string author, float price) : base(name)
        {
            Author = author;
            Price = price;
        }
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($" Book Author : {Author}");
        }
    }
}