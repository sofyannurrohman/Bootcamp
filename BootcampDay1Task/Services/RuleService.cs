using System;
using System.Collections.Generic;
using System.Text;
using BootcampDay1Task.Interfaces;
using BootcampDay1Task.Models;
namespace BootcampDay1Task.Services;
public class RuleService
{
    private readonly List<IRule> _rules = new();

    public void AddRule(int divisor, string word)
    {
        _rules.Add(new Rule(divisor, word));
    }

    public void AddRule(IRule rule)
    {
        _rules.Add(rule);
    }

    public string Generate(int x)
    {
        var sb = new StringBuilder();

        foreach (var rule in _rules)
        {
            if (rule.AppliesTo(x))
                sb.Append(rule.Word);
        }

        return sb.Length > 0 ? sb.ToString() : x.ToString();
    }

    public void PrintRange(int n)
    {
        for (int x = 1; x <= n; x++)
        {
            Console.Write(Generate(x));
            if (x < n) Console.Write(", ");
        }

        Console.WriteLine();
    }
}
