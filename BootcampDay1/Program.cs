using System;

class Program
{
    static void Main()
    {
        DisplayWelcomeMessage();
        string userName = GetUserName();
        GreetUser(userName);
    }

    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Greeting Program!");
        Console.WriteLine("================================");
    }

    static string GetUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    static void GreetUser(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Hello, stranger!");
        }
        else
        {
            Console.WriteLine($"Hello, {name}!");
        }
    }
}