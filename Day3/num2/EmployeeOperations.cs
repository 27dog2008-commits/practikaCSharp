public static class EmployeeOperations
{
    public static double CalculateSalary(Employee[] employees)
    {
        if (employees == null || employees.Length == 0)
        {
            return 0;
        }

        double sum = 0;
        foreach (Employee emp in employees)
        {
            sum += emp.Salary;
        }

        return sum / employees.Length;
    }
}
