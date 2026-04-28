class EmployeeFileReader
{
    public List<Employee> ReadEmployees(string path, char separator)
    {
        List<Employee> list = new List<Employee>();
        if (!File.Exists(path))
        {
            return list;
        }

        string[] lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            string[] parts = line.Split(separator);
            if (parts.Length == 3)
            {
                list.Add(new Employee
                {
                    Name = parts[0],
                    Department = parts[1],
                    Salary = decimal.Parse(parts[2])
                });
            }
        }
        return list;
    }
}
