using System;
using Entities.User;

class Program
{
    static void Main()
    {
        DisplayWelcomeMessage();

        User user = GetUserInfo();
        GreetUser(user);
    }

    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Greeting Program!");
        Console.WriteLine("================================");
    }

    static User GetUserInfo()
    {
        User user = new User();

        Console.Write("Please enter your first name: ");
        user.firstName = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Please enter your last name: ");
        user.lastName = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Please enter your birth date (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
        {
            user.BirthDate = birthDate;
        }
        else
        {
            user.BirthDate = DateTime.MinValue; // fallback if invalid
        }

        return user;
    }

    static void GreetUser(User user)
    {
        string fullName = $"{user.firstName} {user.lastName}".Trim();
        if (string.IsNullOrWhiteSpace(fullName))
        {
            Console.WriteLine("Hello, stranger!");
        }
        else
        {
            Console.WriteLine($"Hello, {fullName}!");
        }

        if (user.BirthDate != DateTime.MinValue)
        {
            Console.WriteLine($"Your birth date is: {user.BirthDate:yyyy-MM-dd}");
        }
    }
}
