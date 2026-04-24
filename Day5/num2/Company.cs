class Company
{
    public string Name { get; set; }
    public Department Dept { get; private set; }
    public Employee[] Employees { get; set; }

    public Company(string name, string deptName)
    {
        Name = name;
        Dept = new Department(deptName);
    }

    public void WorkWithClient(Client client)
    {
        Console.WriteLine($"Компания {Name} обслуживает клиента {client.Name}");
    }

    public decimal CalculateTotalSalary() => Employees?.Sum(e => e.Salary) ?? 0;
}
