using BootcampDay6.Entities;
namespace BootcampDay6.OperatorOverloading;

public class OperatorOverloadingClassCA
{
    public static void CompoundAssignmentDemo()
{
    Console.WriteLine("2. COMPOUND ASSIGNMENT OPERATORS");
    Console.WriteLine("=================================");

    Note currentNote = new Note(5);  // F note
    Console.WriteLine($"Starting note: {currentNote.SemitonesFromA} semitones (F)");

    // These work automatically when you overload the basic operators
    currentNote += 2;  // Move up 2 semitones (F to G)
    Console.WriteLine($"After += 2: {currentNote.SemitonesFromA} semitones (G)");

    currentNote -= 1;  // Move down 1 semitone (G to F#)
    Console.WriteLine($"After -= 1: {currentNote.SemitonesFromA} semitones (F#)");

    currentNote *= 2;  // Double the octave
    Console.WriteLine($"After *= 2: {currentNote.SemitonesFromA} semitones (F# high octave)");

    Console.WriteLine();
}

}
