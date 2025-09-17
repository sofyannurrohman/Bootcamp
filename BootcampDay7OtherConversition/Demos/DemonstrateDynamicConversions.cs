namespace BootcampDay7.Demo;

public class DemoClassDC
{
    static T ConvertValue<T>(string value)
    {
        return (T)Convert.ChangeType(value, typeof(T));
    }
    public static void DemonstrateDynamicConversions()
    {
        Console.WriteLine("4. DYNAMIC CONVERSIONS - RUNTIME TYPE CONVERSION");
        Console.WriteLine("================================================");

        // Convert.ChangeType is powerful when you don't know the target type at compile time
        // Common in reflection, serialization, and generic programming scenarios

        Console.WriteLine("Dynamic type conversion examples:");

        // Simulating data from a configuration file or database
        object[] configValues = { "42", "3.14", "true", "2024-05-29" };
        Type[] targetTypes = { typeof(int), typeof(double), typeof(bool), typeof(DateTime) };
        string[] descriptions = { "Port number", "Tax rate", "Debug mode", "Release date" };

        for (int i = 0; i < configValues.Length; i++)
        {
            try
            {
                object converted = Convert.ChangeType(configValues[i], targetTypes[i]);
                Console.WriteLine($"  {descriptions[i]}: \"{configValues[i]}\" -> {converted} ({targetTypes[i].Name})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  {descriptions[i]}: Failed to convert - {ex.Message}");
            }
        }

        // Generic method example
        Console.WriteLine("\nUsing dynamic conversion in a generic method:");

        string intString = "123";
        string doubleString = "45.67";

        int convertedInt = ConvertValue<int>(intString);
        double convertedDouble = ConvertValue<double>(doubleString);

        Console.WriteLine($"  ConvertValue<int>(\"{intString}\") -> {convertedInt}");
        Console.WriteLine($"  ConvertValue<double>(\"{doubleString}\") -> {convertedDouble}");

        // Handling nullable types
        Console.WriteLine("\nWorking with nullable types:");

        Type nullableIntType = typeof(int?);
        Type underlyingType = Nullable.GetUnderlyingType(nullableIntType) ?? nullableIntType;

        object nullableResult = Convert.ChangeType("456", underlyingType);
        Console.WriteLine($"  Converting to nullable int: {nullableResult}");

        Console.WriteLine();
    }
}
