namespace BootcampDay6.Entities;
public struct BitSet
{
    private readonly int bits;

    public BitSet(int value)
    {
        bits = value;
    }

    // Bitwise operators
    public static BitSet operator &(BitSet a, BitSet b) => new BitSet(a.bits & b.bits);
    public static BitSet operator |(BitSet a, BitSet b) => new BitSet(a.bits | b.bits);
    public static BitSet operator ^(BitSet a, BitSet b) => new BitSet(a.bits ^ b.bits);
    public static BitSet operator ~(BitSet a) => new BitSet(~a.bits);
    public static BitSet operator <<(BitSet a, int shift) => new BitSet(a.bits << shift);
    public static BitSet operator >>(BitSet a, int shift) => new BitSet(a.bits >> shift);

    public override string ToString() => Convert.ToString(bits, 2).PadLeft(8, '0');
}
