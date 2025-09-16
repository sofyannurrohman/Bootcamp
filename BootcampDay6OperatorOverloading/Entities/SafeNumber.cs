namespace BootcampDay6.Entities;
public struct SafeNumber
{
    public int Value { get; }

    public SafeNumber(int value)
    {
        Value = value;
    }

    // Regular (unchecked) addition operator
    public static SafeNumber operator +(SafeNumber x, SafeNumber y)
        => new SafeNumber(x.Value + y.Value);

    // Checked addition operator (C# 11+)
    // This version is called when the operation is in a checked context
    public static SafeNumber operator checked +(SafeNumber x, SafeNumber y)
        => new SafeNumber(checked(x.Value + y.Value));

    // Regular subtraction
    public static SafeNumber operator -(SafeNumber x, SafeNumber y)
        => new SafeNumber(x.Value - y.Value);

    // Checked subtraction
    public static SafeNumber operator checked -(SafeNumber x, SafeNumber y)
        => new SafeNumber(checked(x.Value - y.Value));

    // Regular multiplication
    public static SafeNumber operator *(SafeNumber x, SafeNumber y)
        => new SafeNumber(x.Value * y.Value);

    // Checked multiplication
    public static SafeNumber operator checked *(SafeNumber x, SafeNumber y)
        => new SafeNumber(checked(x.Value * y.Value));

    public override string ToString() => Value.ToString();
}