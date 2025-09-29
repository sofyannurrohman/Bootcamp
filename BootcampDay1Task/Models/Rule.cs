using BootcampDay1Task.Interfaces;
namespace BootcampDay1Task.Models;

public class Rule : IRule
{
    public int Divisor { get; }
    public string Word { get; }

    public Rule(int divisor, string word)
    {
        Divisor = divisor;
        Word = word;
    }

    public bool AppliesTo(int x)
    {
        return x % Divisor == 0;
    }
}