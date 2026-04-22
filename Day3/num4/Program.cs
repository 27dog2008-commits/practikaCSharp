using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Employee[] employees = new Employee[]
        {
            new Employee("Иван Иванов", "Менеджер", 80000),
            new Employee("Мария Петрова", "Разработчик", 120000),
            new Employee("Петр Сидоров", "Разработчик", 120000),
            new Employee("Елена Смирнова", "Тестировщик", 60000)
        };

        Company company = new Company(employees);

        Employee[] highest = company.GetHighestPaidEmployees();
        Console.WriteLine("Сотрудники с самой высокой зарплатой:");
        foreach (var emp in highest)
        {
            emp.PrintInfo();
        }


        Employee[] developers = company.GetEmployeesByPosition("Разработчик");
        Console.WriteLine("\nВсе разработчики:");
        foreach (var emp in developers)
        { 
            emp.PrintInfo(); 
        }
    }
}
