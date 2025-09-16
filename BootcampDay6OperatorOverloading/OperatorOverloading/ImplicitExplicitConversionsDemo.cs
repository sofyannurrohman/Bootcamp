using BootcampDay6.Entities;
namespace BootcampDay6.OperatorOverloading;
public class OperatorOverloadingClassIEC{

public static void ImplicitExplicitConversionsDemo()
        {
            Console.WriteLine("7. IMPLICIT AND EXPLICIT CONVERSIONS");
            Console.WriteLine("====================================");

            Note noteA = new Note(0);     // A note
            Note noteC = new Note(3);     // C note

            Console.WriteLine($"Note A: {noteA.SemitonesFromA} semitones");
            Console.WriteLine($"Note C: {noteC.SemitonesFromA} semitones");

            // Implicit conversion to frequency (double)
            double freqA = noteA;  // No cast needed - implicit conversion
            double freqC = noteC;

            Console.WriteLine($"\nImplicit conversion to frequency:");
            Console.WriteLine($"A note frequency: {freqA:F2} Hz");
            Console.WriteLine($"C note frequency: {freqC:F2} Hz");

            // Explicit conversion back to Note
            double someFrequency = 523.25;  // C note frequency
            Note reconstructedNote = (Note)someFrequency;  // Explicit cast required

            Console.WriteLine($"\nExplicit conversion from frequency:");
            Console.WriteLine($"Frequency {someFrequency} Hz converts to: {reconstructedNote.SemitonesFromA} semitones");

            // Test conversion accuracy
            Console.WriteLine($"\nRound-trip conversion test:");
            Note original = new Note(7);  // E note
            double freq = original;       // To frequency
            Note backToNote = (Note)freq; // Back to note
            Console.WriteLine($"Original: {original.SemitonesFromA} semitones");
            Console.WriteLine($"Frequency: {freq:F2} Hz");
            Console.WriteLine($"Back to note: {backToNote.SemitonesFromA} semitones");

            Console.WriteLine();
        }
}