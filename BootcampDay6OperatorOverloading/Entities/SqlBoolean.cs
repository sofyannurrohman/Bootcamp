namespace BootcampDay6.Entities;
public struct SqlBoolean
{
    private readonly byte m_value;

    // Static instances for the three possible values
    public static readonly SqlBoolean Null = new SqlBoolean(0);
    public static readonly SqlBoolean False = new SqlBoolean(1);
    public static readonly SqlBoolean True = new SqlBoolean(2);

    private SqlBoolean(byte value)
    {
        m_value = value;
    }

    // The true operator - determines when the value should be considered "true" in conditionals
    public static bool operator true(SqlBoolean x)
        => x.m_value == True.m_value;

    // The false operator - determines when the value should be considered "false" in conditionals
    public static bool operator false(SqlBoolean x)
        => x.m_value == False.m_value;

    // Logical NOT operator
    public static SqlBoolean operator !(SqlBoolean x)
    {
        if (x.m_value == Null.m_value) return Null;
        if (x.m_value == False.m_value) return True;
        return False;
    }

    // Bitwise AND operator (works with && when true/false operators are defined)
    public static SqlBoolean operator &(SqlBoolean x, SqlBoolean y)
    {
        // SQL three-valued logic for AND
        if (x.m_value == False.m_value || y.m_value == False.m_value) return False;
        if (x.m_value == Null.m_value || y.m_value == Null.m_value) return Null;
        return True;
    }

    // Bitwise OR operator (works with || when true/false operators are defined)
    public static SqlBoolean operator |(SqlBoolean x, SqlBoolean y)
    {
        // SQL three-valued logic for OR
        if (x.m_value == True.m_value || y.m_value == True.m_value) return True;
        if (x.m_value == Null.m_value || y.m_value == Null.m_value) return Null;
        return False;
    }

    // Equality operators
    public static bool operator ==(SqlBoolean x, SqlBoolean y)
        => x.m_value == y.m_value;

    public static bool operator !=(SqlBoolean x, SqlBoolean y)
        => x.m_value != y.m_value;

    // Explicit conversion to bool (may throw for Null)
    public static explicit operator bool(SqlBoolean x)
    {
        if (x.m_value == Null.m_value)
            throw new InvalidOperationException("Cannot convert SqlBoolean.Null to bool");
        return x.m_value == True.m_value;
    }

    // Implicit conversion from bool
    public static implicit operator SqlBoolean(bool value)
        => value ? True : False;

    public override bool Equals(object? obj)
    {
        if (obj is SqlBoolean other)
            return this == other;
        return false;
    }

    public override int GetHashCode() => m_value.GetHashCode();

    public override string ToString()
    {
        if (m_value == Null.m_value) return "Null";
        if (m_value == False.m_value) return "False";
        return "True";
    }
}