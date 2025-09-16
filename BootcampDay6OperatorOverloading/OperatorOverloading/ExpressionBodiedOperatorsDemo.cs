using BootcampDay6.Entities;

namespace BootcampDay6.OperatorOverloading;
public class OperatorOverloadingEBO
{
    public static void ExpressionBodiedOperatorsDemo()
    {
        Console.WriteLine("3. EXPRESSION-BODIED OPERATORS");
        Console.WriteLine("===============================");

        Console.WriteLine("The operators in our Note struct use expression-bodied syntax:");
        Console.WriteLine("public static Note operator +(Note x, int semitones) => new Note(x.value + semitones);");
        Console.WriteLine("\nThis makes the code more concise and readable for simple operations.");

        // Demonstrate the clean syntax in action
        Vector2D vector1 = new Vector2D(3, 4);
        Vector2D vector2 = new Vector2D(1, 2);

        Console.WriteLine($"\nVector addition example:");
        Console.WriteLine($"Vector1: {vector1}");
        Console.WriteLine($"Vector2: {vector2}");

        Vector2D sum = vector1 + vector2;
        Console.WriteLine($"Sum: {sum}");

        Console.WriteLine();
    }
}