
using BootcampDay6.Entities;
namespace BootcampDay6.OperatorOverloading;
public class OperatorOverloadingClassComOper {
    
public static void ComparisonOperatorsDemo()
{
    Console.WriteLine("6. COMPARISON OPERATORS DEMONSTRATION");
    Console.WriteLine("=====================================");

    Note[] notes = {
                new Note(0),   // A
                new Note(2),   // B
                new Note(5),   // D
                new Note(7),   // E
                new Note(9)    // F#
            };

    Console.WriteLine("Original notes:");
    for (int i = 0; i < notes.Length; i++)
    {
        Console.WriteLine($"Note {i}: {notes[i].SemitonesFromA} semitones");
    }

    // Test comparison operators
    Console.WriteLine($"\nComparison tests:");
    Console.WriteLine($"notes[0] < notes[1]: {notes[0] < notes[1]}");  // True
    Console.WriteLine($"notes[2] > notes[1]: {notes[2] > notes[1]}");  // True
    Console.WriteLine($"notes[0] <= notes[0]: {notes[0] <= notes[0]}");  // True
    Console.WriteLine($"notes[4] >= notes[3]: {notes[4] >= notes[3]}");  // True

    // Sort the array using IComparable implementation
    Array.Sort(notes);
    Console.WriteLine($"\nSorted notes (using IComparable):");
    for (int i = 0; i < notes.Length; i++)
    {
        Console.WriteLine($"Note {i}: {notes[i].SemitonesFromA} semitones");
    }

    Console.WriteLine();
}
}