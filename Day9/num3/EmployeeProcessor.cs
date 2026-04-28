class EmployeeProcessor
{
    public void FindByDepartment(List<Employee> list, string dept)
    {
        var results = list.Where(e => e.Department.Equals(dept, StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"--- Сотрудники отдела {dept} ---");
        foreach (var e in results)
        {
            Console.WriteLine($"{e.Name} - {e.Salary}");
        }
    }
}
