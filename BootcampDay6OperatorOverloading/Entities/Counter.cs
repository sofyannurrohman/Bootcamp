namespace BootcampDay6.Entities;
public class Counter
{
    public int Value { get; private set; }

    public Counter(int initialValue)
    {
        Value = initialValue;
    }

    // Pre-increment: returns the incremented value
    public static Counter operator ++(Counter c)
    {
        c.Value++;
        return c;
    }

    // Pre-decrement: returns the decremented value
    public static Counter operator --(Counter c)
    {
        c.Value--;
        return c;
    }

    // Note: C# doesn't allow separate post-increment operators
    // The compiler automatically handles the difference between ++c and c++
}
