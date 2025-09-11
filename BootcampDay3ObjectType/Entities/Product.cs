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
        // Define Constructor Product using name field for params
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

    
}