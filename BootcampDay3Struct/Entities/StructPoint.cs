namespace Classes;

    public struct PointStruct
    {
        public int X;
        public int Y;

        // Constructor
        public PointStruct(int x, int y)
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
