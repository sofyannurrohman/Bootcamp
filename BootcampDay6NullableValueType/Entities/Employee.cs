namespace Entities;
public class Employee
{
    public string Name { get; }
    public int? Age { get; }           // Nullable - age might be private/unknown
    public decimal? Salary { get; }    // Nullable - salary might be confidential

    public Employee(string name, int? age, decimal? salary)
    {
        Name = name;
        Age = age;
        Salary = salary;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Employee: {Name}");

        // Using null-coalescing for display
        string ageDisplay = Age?.ToString() ?? "Unknown";
        string salaryDisplay = Salary?.ToString("C") ?? "Confidential";

        Console.WriteLine($"  Age: {ageDisplay}");
        Console.WriteLine($"  Salary: {salaryDisplay}");

        // Determine benefits eligibility (requires both age and salary)
        bool? eligibleForBenefits = IsEligibleForBenefits();
        string eligibilityStatus = eligibleForBenefits?.ToString() ?? "Cannot determine";
        Console.WriteLine($"  Benefits eligible: {eligibilityStatus}");
    }

    public bool? IsEligibleForBenefits()
    {
        // Need both age and salary to determine eligibility
        if (!Age.HasValue || !Salary.HasValue)
            return null; // Cannot determine without complete information

        // Eligible if age >= 21 and salary >= 40000
        return Age >= 21 && Salary >= 40000;
    }

    public decimal? CalculateBonus(decimal? bonusPercentage)
    {
        // Using nullable arithmetic - if any value is null, result is null
        return Salary * (bonusPercentage / 100);
    }

    public bool IsRetirementEligible()
    {
        // Using GetValueOrDefault for safe comparison
        return Age.GetValueOrDefault(0) >= 65;
    }
}

    public class EmployeeStatistics
    {
        public int TotalEmployees { get; set; }
        public int EmployeesWithAge { get; set; }
        public int EmployeesWithSalary { get; set; }
        public double? AverageAge { get; set; }
        public decimal? AverageSalary { get; set; }
        public decimal? TotalPayroll { get; set; }

        public static EmployeeStatistics Calculate(Employee[] employees)
        {
            var stats = new EmployeeStatistics
            {
                TotalEmployees = employees.Length
            };

            // Calculate statistics for non-null values only
            var knownAges = new System.Collections.Generic.List<int>();
            var knownSalaries = new System.Collections.Generic.List<decimal>();
            decimal? totalPayroll = 0;
            bool allSalariesKnown = true;

            foreach (var emp in employees)
            {
                if (emp.Age.HasValue)
                {
                    knownAges.Add(emp.Age.Value);
                    stats.EmployeesWithAge++;
                }

                if (emp.Salary.HasValue)
                {
                    knownSalaries.Add(emp.Salary.Value);
                    stats.EmployeesWithSalary++;
                    totalPayroll += emp.Salary.Value;
                }
                else
                {
                    allSalariesKnown = false;
                }
            }

            // Calculate averages (nullable results)
            stats.AverageAge = knownAges.Count > 0 ? knownAges.Average() : null;
            stats.AverageSalary = knownSalaries.Count > 0 ? knownSalaries.Average() : null;
            stats.TotalPayroll = allSalariesKnown ? totalPayroll : null;

            return stats;
        }
    }