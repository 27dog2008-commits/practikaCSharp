using System;
using System.Collections.Generic;
using System.IO;

class Num3
{
    static void Main()
    {
        var employees = new List<Employee>
            {
                new Employee { Name = "Паша", Department = "IT", Salary = 5000 },
                new Employee { Name = "Максим", Department = "HR", Salary = 7500 },
                new Employee { Name = "Ваня", Department = "IT", Salary = 6000 }
            };

        EmployeeFileWriter writer = new EmployeeFileWriter();
        writer.WriteEmployees(employees, "file.data", '|');
    }
}
