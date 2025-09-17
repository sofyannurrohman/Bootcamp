using System.ComponentModel;
using System.Drawing;

namespace BootcampDay7.Demo;

public class DemoClassTCM
{

    public static void DemonstrateTypeConverterMagic()
    {
        Console.WriteLine("7. TYPE CONVERTERS - DESIGN-TIME INTELLIGENCE");
        Console.WriteLine("==============================================");

        // TypeConverters are mainly used in Visual Studio designers and XAML
        // They provide context-aware string-to-object conversion
        // Much more flexible than standard parsing methods

        Console.WriteLine("What makes TypeConverters special:");
        Console.WriteLine("- Context-aware parsing (can infer format from content)");
        Console.WriteLine("- Used by Visual Studio property editors");
        Console.WriteLine("- XAML attribute parsing relies on these");
        Console.WriteLine("- Can provide design-time services (dropdown lists, etc.)");
        Console.WriteLine();

        // Color converter demonstration
        Console.WriteLine("Color TypeConverter examples:");

        try
        {
            TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));

            // Converting various color representations
            string[] colorInputs = { "Red", "Blue", "Beige", "Transparent", "DarkSlateGray", "255, 128, 0" };

            Console.WriteLine("Input String      -> R    G    B    A    Name");
            Console.WriteLine("------------      -  ---  ---  ---  ---  ----");

            foreach (string colorInput in colorInputs)
            {
                if (colorConverter.CanConvertFrom(typeof(string)))
                {
                    try
                    {
                        object? converted = colorConverter.ConvertFromString(colorInput);
                        if (converted is Color color)
                        {
                            Console.WriteLine($"{colorInput,-16}  -> {color.R,3}  {color.G,3}  {color.B,3}  {color.A,3}  {color.Name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{colorInput,-16}  -> Error: {ex.Message}");
                    }
                }
            }

            // Converting back to string
            Console.WriteLine("\nColor to string conversion:");
            Color testColor = Color.FromArgb(255, 128, 64);
            string colorString = colorConverter.ConvertToString(testColor) ?? "null";
            Console.WriteLine($"  Color(255,128,64) -> \"{colorString}\"");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Color converter not available: {ex.Message}");
            Console.WriteLine("(This is normal in some environments without System.Drawing)");
        }

        // Exploring other built-in type converters
        Console.WriteLine("\nOther built-in type converters:");

        Type[] typesToTest = { typeof(int), typeof(DateTime), typeof(bool), typeof(decimal), typeof(TimeSpan) };

        foreach (Type type in typesToTest)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            bool canConvertFromString = converter.CanConvertFrom(typeof(string));
            bool canConvertToString = converter.CanConvertTo(typeof(string));

            Console.WriteLine($"  {type.Name}:");
            Console.WriteLine($"    Can convert from string: {canConvertFromString}");
            Console.WriteLine($"    Can convert to string: {canConvertToString}");

            if (canConvertFromString)
            {
                try
                {
                    // Test with appropriate sample values
                    string testValue = type.Name switch
                    {
                        "Int32" => "42",
                        "DateTime" => "2024-05-29 14:30:00",
                        "Boolean" => "true",
                        "Decimal" => "123.45",
                        "TimeSpan" => "01:30:45",
                        _ => "test"
                    };

                    object? converted = converter.ConvertFromString(testValue);
                    Console.WriteLine($"    Example: \"{testValue}\" -> {converted} ({converted?.GetType().Name})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"    Conversion failed: {ex.Message}");
                }
            }
            Console.WriteLine();
        }

        // Demonstrating advanced TypeConverter features
        Console.WriteLine("Advanced TypeConverter capabilities:");

        TypeConverter intConverter = TypeDescriptor.GetConverter(typeof(int));

        // Check what types it can convert from
        Type[] sourceTypes = { typeof(string), typeof(double), typeof(decimal), typeof(bool) };
        Console.WriteLine("  Int32 converter can convert from:");

        foreach (Type sourceType in sourceTypes)
        {
            bool canConvert = intConverter.CanConvertFrom(sourceType);
            Console.WriteLine($"    {sourceType.Name}: {canConvert}");

            if (canConvert && sourceType != typeof(bool)) // Skip bool for cleaner output
            {
                try
                {
                    object testValue = sourceType.Name switch
                    {
                        "String" => "123",
                        "Double" => 456.789,
                        "Decimal" => 789.123m,
                        _ => "test"
                    };

                    object? result = intConverter.ConvertFrom(testValue);
                    Console.WriteLine($"      Example: {testValue} -> {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"      Error: {ex.Message}");
                }
            }
        }

        Console.WriteLine();
    }
}