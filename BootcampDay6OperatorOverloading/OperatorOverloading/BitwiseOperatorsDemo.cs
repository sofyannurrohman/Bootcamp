using BootcampDay6.Entities;
namespace BootcampDay6.OperatorOverloading;
public class OperatorOverloadingClassBO{
public static void BitwiseOperatorsDemo()
        {
            Console.WriteLine("10. BITWISE OPERATORS DEMONSTRATION");
            Console.WriteLine("==================================");

            BitSet set1 = new BitSet(0b1010);  // Binary: 1010
            BitSet set2 = new BitSet(0b1100);  // Binary: 1100

            Console.WriteLine($"Set1: {set1}");
            Console.WriteLine($"Set2: {set2}");

            // Bitwise AND
            BitSet andResult = set1 & set2;
            Console.WriteLine($"set1 & set2: {andResult}");

            // Bitwise OR
            BitSet orResult = set1 | set2;
            Console.WriteLine($"set1 | set2: {orResult}");

            // Bitwise XOR
            BitSet xorResult = set1 ^ set2;
            Console.WriteLine($"set1 ^ set2: {xorResult}");

            // Bitwise NOT
            BitSet notResult = ~set1;
            Console.WriteLine($"~set1: {notResult}");

            // Left shift
            BitSet leftShift = set1 << 2;
            Console.WriteLine($"set1 << 2: {leftShift}");

            // Right shift
            BitSet rightShift = set1 >> 1;
            Console.WriteLine($"set1 >> 1: {rightShift}");

            Console.WriteLine();
        }

}