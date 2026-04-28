class EmployeeFileWriter
{
    public void WriteEmployees(List<Employee> employees, string path, char separator)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            foreach (var emp in employees)
            {
                string line = $"{emp.Name}{separator}{emp.Department}{separator}{emp.Salary}";
                sw.WriteLine(line);
            }
        }
        Console.WriteLine("Данные успешно записаны в " + path);
    }
}
