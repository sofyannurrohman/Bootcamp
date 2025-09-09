// See https://aka.ms/new-console-template for more information
//Capture User Input
Console.Write("Enter a number (n): ");
int n = int.Parse(Console.ReadLine());
//Foo bar IMPL
for (int x = 1; x <= n; x++)
{
    if (x % 15 == 0)
        Console.Write("foobar");
    else if (x % 3 == 0)
        Console.Write("foo");
    else if (x % 5 == 0)
        Console.Write("bar");
    else
        Console.Write(x);
    if (x < n)
        Console.Write(", ");
}
//Write Output
Console.WriteLine();
