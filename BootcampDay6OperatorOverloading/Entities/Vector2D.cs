namespace BootcampDay6.Entities;
public struct Vector2D
{
    public double X { get; }
    public double Y { get; }

    public double Magnitude => Math.Sqrt(X * X + Y * Y);

    public Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    // Arithmetic operators
    public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
    public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);
    public static Vector2D operator *(Vector2D v, double scalar) => new Vector2D(v.X * scalar, v.Y * scalar);

    // Unary operators
    public static Vector2D operator -(Vector2D v) => new Vector2D(-v.X, -v.Y);
    public static Vector2D operator +(Vector2D v) => v.Magnitude == 0 ? v : new Vector2D(v.X / v.Magnitude, v.Y / v.Magnitude);
    public static Vector2D operator !(Vector2D v) => new Vector2D(-v.Y, v.X);  // 90-degree rotation

    public override string ToString() => $"({X:F1}, {Y:F1})";
}