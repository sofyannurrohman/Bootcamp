using BootcampDay1Task.Models;
using BootcampDay1Task.Services;

Console.Write("Enter a number (n): ");
if (!int.TryParse(Console.ReadLine(), out int n))
{
    Console.WriteLine("Invalid number");
    return;
}

var ruleClass = new RuleService();
ruleClass.AddRule(3, "foo");
ruleClass.AddRule(5, "bar");
ruleClass.AddRule(7, "jazz");
ruleClass.AddRule(4, "baz");
ruleClass.AddRule(9, "huzz");

ruleClass.PrintRange(n);