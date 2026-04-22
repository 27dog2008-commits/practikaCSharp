using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Employee[] employees = new Employee[]
        {
            new Employee { Name = "Иван", Salary = 50000 },
            new Employee { Name = "Мария", Salary = 70000 },
            new Employee { Name = "Петр", Salary = 60000 }
        };

        double avg = EmployeeOperations.CalculateSalary(employees);
        Console.WriteLine($"Средняя зарплата: {avg}");
    }
}