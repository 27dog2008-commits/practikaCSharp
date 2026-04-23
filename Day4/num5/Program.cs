using System;

class Program
{
    static void Main()
    {
        Employee[] employees = new Employee[]
        {
            new Manager("Иван Иванов", 1, 120000),
            new Developer("Петр Петров", 2, 1000, 160),
            new Developer("Мария Смирнова", 3, 1200, 140)
        };

        foreach (Employee emp in employees)
        {
            emp.DisplayInfo();
            Console.WriteLine($"  Зарплата: {emp.CalculateSalary():C}");
            Console.WriteLine();
        }
    }
}