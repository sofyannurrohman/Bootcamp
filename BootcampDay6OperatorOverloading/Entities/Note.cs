namespace BootcampDay6.Entities;
public struct Note : IComparable<Note>
{
    private readonly int value;  // Semitones from A

    public int SemitonesFromA => value;

    public Note(int semitonesFromA)
    {
        value = semitonesFromA;
    }

    // Arithmetic operators with expression-bodied syntax (C# 6+)
    // This is much cleaner than traditional method syntax for simple operations
    public static Note operator +(Note x, int semitones) => new Note(x.value + semitones);
    public static Note operator -(Note x, int semitones) => new Note(x.value - semitones);
    public static int operator -(Note x, Note y) => x.value - y.value;  // Returns interval
    public static Note operator *(Note x, int octaves) => new Note(x.value + (octaves - 1) * 12);

    // Equality operators - MUST implement both == and != as a pair
    // The C# compiler enforces this rule
    public static bool operator ==(Note x, Note y) => x.value == y.value;
    public static bool operator !=(Note x, Note y) => x.value != y.value;

    // Comparison operators - implement all four or none
    // This enables sorting, comparisons, and use with generic collections
    public static bool operator <(Note x, Note y) => x.value < y.value;
    public static bool operator >(Note x, Note y) => x.value > y.value;
    public static bool operator <=(Note x, Note y) => x.value <= y.value;
    public static bool operator >=(Note x, Note y) => x.value >= y.value;

    // Implicit conversion to frequency (safe - no data loss, commonly used)
    // Users can write: double freq = noteA; instead of noteA.ToFrequency()
    public static implicit operator double(Note note)
    {
        // Convert semitones to frequency using A4 = 440 Hz as reference
        return 440.0 * Math.Pow(2.0, (double)note.value / 12.0);
    }

    // Explicit conversion from frequency (potentially lossy - requires casting)
    // Users must write: Note note = (Note)440.0; to show intent
    public static explicit operator Note(double frequency)
    {
        // Convert frequency back to nearest semitone
        int semitones = (int)Math.Round(12.0 * Math.Log2(frequency / 440.0));
        return new Note(semitones);
    }

    // IComparable implementation - required when overloading comparison operators
    // This enables Array.Sort(), List.Sort(), and other framework sorting methods
    public int CompareTo(Note other) => this.value.CompareTo(other.value);

    // Object overrides - ALWAYS override these when implementing == and !=
    // This ensures consistency between operators and framework methods
    public override bool Equals(object? obj)
    {
        if (obj is Note note)
            return this == note;
        return false;
    }

    public override int GetHashCode() => value.GetHashCode();

    public override string ToString()
    {
        string[] noteNames = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
        int noteIndex = ((value % 12) + 12) % 12;  // Handle negative values
        int octave = 4 + (value / 12);
        return $"{noteNames[noteIndex]}{octave}";
    }
}