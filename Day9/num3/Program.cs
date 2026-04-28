using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Num3
{
    static void Main()
    {
        EmployeeFileReader reader = new EmployeeFileReader();
        var list = reader.ReadEmployees("D:\\Шарп Практика\\Day9\\num2\\bin\\Debug\\net8.0\\file.data", '|');

        EmployeeProcessor processor = new EmployeeProcessor();
        processor.FindByDepartment(list, "IT");
    }
}