namespace NestedTypesDemo;

public class Product
{
    public string Name { get; set; }

    public class ProductDetail
    {
        public string Description { get; set; }
        public float Price { get; set; }
        public void ShowDetail()
        {
            Console.WriteLine($"Description: {Description}, Price: {Price}");
        }
    }
}