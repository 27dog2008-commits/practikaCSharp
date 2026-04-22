public class Company
{
    private Employee[] employees;

    public Company(Employee[] employees)
    {
        this.employees = employees;
    }

    public Employee[] GetHighestPaidEmployees()
    {
        if (employees.Length == 0)
            return new Employee[0];

        double maxSalary = employees[0].Salary;
        foreach (var emp in employees)
        {
            if (emp.Salary > maxSalary)
                maxSalary = emp.Salary;
        }

        List<Employee> result = new List<Employee>();
        foreach (var emp in employees)
        {
            if (emp.Salary == maxSalary)
                result.Add(emp);
        }

            return result.ToArray();
    }

    public Employee[] GetEmployeesByPosition(string position)
    {
        List<Employee> result = new List<Employee>();
        foreach (var emp in employees)
        {
            if (emp.Position.Equals(position, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(emp);
            }
        }
        return result.ToArray();
    }
}
