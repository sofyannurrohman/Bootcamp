using BootcampDay6.Entities;
namespace BootcampDay6.OperatorOverloading;
public class OperatorOverloadingClassRWS {
    
public static void RealWorldScenarioDemo()
        {
            Console.WriteLine("12. REAL WORLD SCENARIO - MONEY CALCULATION SYSTEM");
            Console.WriteLine("==================================================");

            // Create some money amounts
            Money salary = new Money(5000.00m, "USD");
            Money bonus = new Money(1000.00m, "USD");
            Money tax = new Money(900.00m, "USD");

            Console.WriteLine($"Monthly salary: {salary}");
            Console.WriteLine($"Performance bonus: {bonus}");
            Console.WriteLine($"Tax deduction: {tax}");

            // Use overloaded operators for calculations
            Money totalIncome = salary + bonus;
            Money netIncome = totalIncome - tax;

            Console.WriteLine($"\nCalculations:");
            Console.WriteLine($"Total income: {totalIncome}");
            Console.WriteLine($"Net income after tax: {netIncome}");

            // Comparison operations
            Console.WriteLine($"\nComparisons:");
            Console.WriteLine($"Salary > Tax: {salary > tax}");
            Console.WriteLine($"Bonus == Tax: {bonus == tax}");

            // Multiplication for projections
            Money yearlyProjection = netIncome * 12;
            Console.WriteLine($"Yearly projection: {yearlyProjection}");            // Percentage calculations
            Money raise = salary * 0.1m;  // 10% raise
            Money newSalary = salary + raise;
            Console.WriteLine($"After 10% raise: {newSalary}");

            // Currency conversion using method (better than operator overloading for this)
            Money eurAmount = netIncome.ConvertTo("EUR");
            Console.WriteLine($"Converted to EUR: {eurAmount}");

            // Conversion operators demonstration
            decimal rawAmount = (decimal)salary;  // Explicit conversion to decimal
            Money fromDecimal = 2500.00m;         // Implicit conversion from decimal
            Console.WriteLine($"Extracted amount: {rawAmount:C}");
            Console.WriteLine($"Created from decimal: {fromDecimal}");

            Console.WriteLine();
        }
}