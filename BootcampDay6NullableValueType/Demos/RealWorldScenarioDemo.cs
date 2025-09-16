using Entities;
public class DemoClassRWS {
    
public static void RealWorldScenarioDemo()
        {
            Console.WriteLine("14. REAL WORLD SCENARIO - EMPLOYEE MANAGEMENT SYSTEM");
            Console.WriteLine("======================================================");

            Console.WriteLine("Let's see nullable types in action with a practical employee management system.");
            Console.WriteLine("This demonstrates all the concepts we've learned in a real-world context.\n");

            var employees = new[]
            {
                new Employee("John Doe", 30, 75000.50m),
                new Employee("Jane Smith", null, 82000.00m),  // Age is private/unknown
                new Employee("Bob Wilson", 45, null),          // Salary is confidential
                new Employee("Alice Brown", null, null),       // Both unknown (new hire?)
                new Employee("Charlie Davis", 28, 45000.00m)
            };

            Console.WriteLine("Employee Management System - Complete Employee List:");
            Console.WriteLine("====================================================");

            foreach (var emp in employees)
            {
                emp.DisplayInfo();
                Console.WriteLine();
            }

            // Demonstrate nullable arithmetic in action
            Console.WriteLine("BONUS CALCULATION DEMO:");
            Console.WriteLine("=======================");
            decimal? bonusPercentage = 5.5m; // 5.5% bonus

            foreach (var emp in employees)
            {
                decimal? bonus = emp.CalculateBonus(bonusPercentage);
                string bonusDisplay = bonus?.ToString("C") ?? "Cannot calculate (salary unknown)";
                Console.WriteLine($"{emp.Name}: Bonus = {bonusDisplay}");
            }

            Console.WriteLine("\nRETIREMENT ELIGIBILITY CHECK:");
            Console.WriteLine("==============================");
            foreach (var emp in employees)
            {
                bool eligible = emp.IsRetirementEligible();
                string status = eligible ? "Eligible for retirement" : "Not eligible (or age unknown)";
                Console.WriteLine($"{emp.Name}: {status}");
            }

            // Statistical analysis with nullable values
            Console.WriteLine("\nSTATISTICAL ANALYSIS:");
            Console.WriteLine("=====================");

            var stats = EmployeeStatistics.Calculate(employees);
            Console.WriteLine($"Total employees: {stats.TotalEmployees}");
            Console.WriteLine($"Employees with known age: {stats.EmployeesWithAge}");
            Console.WriteLine($"Employees with known salary: {stats.EmployeesWithSalary}");
            Console.WriteLine($"Average age: {stats.AverageAge?.ToString("F1") ?? "Cannot calculate (insufficient data)"}");
            Console.WriteLine($"Average salary: {stats.AverageSalary?.ToString("C") ?? "Cannot calculate (insufficient data)"}");
            Console.WriteLine($"Total payroll: {stats.TotalPayroll?.ToString("C") ?? "Cannot calculate (some salaries unknown)"}");

            Console.WriteLine("\nKey Takeaways from this real-world example:");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Nullable types elegantly handle missing/unknown data");
            Console.WriteLine("2. Operations propagate null appropriately (bonus calculation)");
            Console.WriteLine("3. GetValueOrDefault provides safe fallbacks (retirement eligibility)");
            Console.WriteLine("4. Null-coalescing operators create user-friendly displays");
            Console.WriteLine("5. Statistical calculations handle partial data gracefully");

            Console.WriteLine();
        }
}