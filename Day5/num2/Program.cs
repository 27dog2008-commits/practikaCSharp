class Num2
{
    static void Main()
    {

        Company[] companies = new Company[2];

        companies[0] = new Company("Google", "Search Dept");
        companies[0].Employees = new[] {
                new Employee { Name = "Alice", Salary = 5000 },
                new Employee { Name = "Bob", Salary = 6000 }
            };

        companies[1] = new Company("Microsoft", "Cloud Dept");
        companies[1].Employees = new[] {
                new Employee { Name = "Charlie", Salary = 7000 }
            };

        foreach (var company in companies)
        {
            Console.WriteLine($"Компания: {company.Name}, Отдел: {company.Dept.Title}");
            Console.WriteLine($"зп: {company.CalculateTotalSalary():C}");
        }
    }
}