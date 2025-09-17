using System.Numerics;

namespace BootcampDay7.Demo;

public class DemoClassBO
{

    public static void DemonstrateBitOperations()
    {
        Console.WriteLine("10. BIT OPERATIONS - LOW-LEVEL MANIPULATION");
        Console.WriteLine("==========================================");

        // Bit operations are essential for performance optimization and low-level programming
        Console.WriteLine("Basic bitwise operations:");

        int a = 12;  // Binary: 1100
        int b = 10;  // Binary: 1010

        Console.WriteLine($"  a = {a} (binary: {Convert.ToString(a, 2).PadLeft(8, '0')})");
        Console.WriteLine($"  b = {b} (binary: {Convert.ToString(b, 2).PadLeft(8, '0')})");
        Console.WriteLine($"  a & b = {a & b} (AND)");
        Console.WriteLine($"  a | b = {a | b} (OR)");
        Console.WriteLine($"  a ^ b = {a ^ b} (XOR)");
        Console.WriteLine($"  ~a = {~a} (NOT)");
        Console.WriteLine($"  a << 1 = {a << 1} (left shift)");
        Console.WriteLine($"  a >> 1 = {a >> 1} (right shift)");

        // BitOperations class methods (.NET 6+)
        Console.WriteLine("\nBitOperations class methods (.NET 6+):");
        Console.WriteLine("  These methods are highly optimized, often using CPU intrinsics!");

        int[] testNumbers = { 16, 17, 32, 255, 1024 };

        foreach (int num in testNumbers)
        {
            Console.WriteLine($"\n  Number: {num} (binary: {Convert.ToString(num, 2)})");
            Console.WriteLine($"    Is power of 2: {BitOperations.IsPow2((uint)num)}");
            Console.WriteLine($"    Leading zero count: {BitOperations.LeadingZeroCount((uint)num)}");
            Console.WriteLine($"    Trailing zero count: {BitOperations.TrailingZeroCount((uint)num)}");
            Console.WriteLine($"    Population count (1s): {BitOperations.PopCount((uint)num)}");

            if (num > 0)
            {
                Console.WriteLine($"    Log2: {BitOperations.Log2((uint)num)}");
                Console.WriteLine($"    Round up to power of 2: {BitOperations.RoundUpToPowerOf2((uint)num)}");
            }
        }

        // Bit rotation - useful for cryptography and hash functions
        Console.WriteLine("\nBit rotation operations:");
        Console.WriteLine("  Rotation preserves all bits, unlike shifting which loses bits");

        uint rotateValue = 0b11110000_00000000_00000000_00001111;
        Console.WriteLine($"  Original: 0x{rotateValue:X8} (binary: {Convert.ToString(rotateValue, 2).PadLeft(32, '0')})");

        uint leftRotated = BitOperations.RotateLeft(rotateValue, 4);
        uint rightRotated = BitOperations.RotateRight(rotateValue, 4);

        Console.WriteLine($"  Rotate left 4:  0x{leftRotated:X8}");
        Console.WriteLine($"  Rotate right 4: 0x{rightRotated:X8}");

        // Demonstrate multiple rotations
        Console.WriteLine("\n  Multiple rotations (8 bits at a time):");
        uint currentValue = rotateValue;
        for (int i = 0; i < 4; i++)
        {
            currentValue = BitOperations.RotateLeft(currentValue, 8);
            Console.WriteLine($"    After {(i + 1) * 8,2} left rotations: 0x{currentValue:X8}");
        }

        // Practical examples
        Console.WriteLine("\nPractical bit manipulation examples:");

        // Flags enumeration simulation
        Console.WriteLine("  File permissions (using bit flags):");
        const int READ = 1;    // 001
        const int WRITE = 2;   // 010
        const int EXECUTE = 4; // 100

        int permissions = READ | WRITE; // Set read and write
        Console.WriteLine($"    Initial permissions: {permissions} (binary: {Convert.ToString(permissions, 2).PadLeft(3, '0')})");
        Console.WriteLine($"    Has read: {(permissions & READ) != 0}");
        Console.WriteLine($"    Has write: {(permissions & WRITE) != 0}");
        Console.WriteLine($"    Has execute: {(permissions & EXECUTE) != 0}");

        permissions |= EXECUTE; // Add execute permission
        Console.WriteLine($"    After adding execute: {permissions} (binary: {Convert.ToString(permissions, 2).PadLeft(3, '0')})");

        permissions &= ~WRITE; // Remove write permission
        Console.WriteLine($"    After removing write: {permissions} (binary: {Convert.ToString(permissions, 2).PadLeft(3, '0')})");

        // Fast power of 2 operations
        Console.WriteLine("\n  Fast power-of-2 operations:");

        int value = 100;
        Console.WriteLine($"    Multiply {value} by 8 (2^3): {value << 3}");
        Console.WriteLine($"    Divide {value} by 4 (2^2): {value >> 2}");
        Console.WriteLine($"    Check if {value} is even: {(value & 1) == 0}");

        // Bit field extraction
        Console.WriteLine("\n  Color component extraction (RGB):");

        uint color = 0xFF8040; // RGB color
        uint red = (color >> 16) & 0xFF;
        uint green = (color >> 8) & 0xFF;
        uint blue = color & 0xFF;

        Console.WriteLine($"    Color: 0x{color:X6}");
        Console.WriteLine($"    Red: {red} (0x{red:X2})");
        Console.WriteLine($"    Green: {green} (0x{green:X2})");
        Console.WriteLine($"    Blue: {blue} (0x{blue:X2})");

        Console.WriteLine();
    }

}