namespace Classes
{
    public struct Point
    {
        public int X;
        public int Y;

        // Constructor
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Method
        public void Display()
        {
            Console.WriteLine($"Point at ({X}, {Y})");
        }
    }
}