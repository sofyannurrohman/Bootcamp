
public class Money
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    // Arithmetic operators
    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot add different currencies");
        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot subtract different currencies");
        return new Money(a.Amount - b.Amount, a.Currency);
    }

    public static Money operator *(Money money, decimal multiplier)
        => new Money(money.Amount * multiplier, money.Currency);

    public static Money operator *(decimal multiplier, Money money)
        => money * multiplier;

    // Comparison operators
    public static bool operator ==(Money a, Money b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
        return a.Currency == b.Currency && a.Amount == b.Amount;
    }

    public static bool operator !=(Money a, Money b) => !(a == b);

    public static bool operator >(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot compare different currencies");
        return a.Amount > b.Amount;
    }

    public static bool operator <(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot compare different currencies");
        return a.Amount < b.Amount;
    }
    public static bool operator >=(Money a, Money b) => a > b || a == b;
    public static bool operator <=(Money a, Money b) => a < b || a == b;

    // Explicit conversion to decimal (extract amount)
    public static explicit operator decimal(Money money) => money.Amount;

    // Implicit conversion from decimal (assumes USD)
    public static implicit operator Money(decimal amount) => new Money(amount, "USD");

    // Method for currency conversion (more appropriate than operator overloading)
    public Money ConvertTo(string targetCurrency)
    {
        // Simulated conversion rates from USD
        decimal rate = targetCurrency.ToUpper() switch
        {
            "USD" => 1.0m,
            "EUR" => 0.85m,
            "GBP" => 0.73m,
            "JPY" => 110.0m,
            _ => throw new NotSupportedException($"Currency {targetCurrency} not supported")
        };

        // Convert to USD first if needed, then to target currency
        decimal usdAmount = Currency.ToUpper() == "USD" ? Amount : Amount / GetRateFromUSD(Currency);
        return new Money(usdAmount * rate, targetCurrency.ToUpper());
    }

    private decimal GetRateFromUSD(string currency)
    {
        return currency.ToUpper() switch
        {
            "USD" => 1.0m,
            "EUR" => 0.85m,
            "GBP" => 0.73m,
            "JPY" => 110.0m,
            _ => 1.0m
        };
    }
    public override bool Equals(object? obj)
    {
        if (obj is Money money)
            return this == money;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public override string ToString() => $"{Amount:C} {Currency}";
}