using BootcampDay6.Entities;
public class OperatorOverloadingClassUO {
    
public static void UnaryOperatorsDemo()
        {
            Console.WriteLine("8. UNARY OPERATORS DEMONSTRATION");
            Console.WriteLine("=================================");

            Vector2D vector = new Vector2D(3, -4);
            Console.WriteLine($"Original vector: {vector}");
            Console.WriteLine($"Magnitude: {vector.Magnitude:F2}");

            // Unary minus (negation)
            Vector2D negated = -vector;
            Console.WriteLine($"Negated vector: {negated}");

            // Unary plus (identity, but could normalize)
            Vector2D normalized = +vector;
            Console.WriteLine($"Normalized vector: {normalized}");

            // Logical not (custom behavior)
            Vector2D perpendicular = !vector;
            Console.WriteLine($"Perpendicular vector: {perpendicular}");

            Console.WriteLine();
        }
}