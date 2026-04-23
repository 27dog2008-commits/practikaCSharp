public class Manager : Employee
{
    public double FixedSalary { get; set; }

    public Manager(string name, int id, double fixedSalary) : base(name, id)
    {
        FixedSalary = fixedSalary;
    }

    public override double CalculateSalary()
    {
        return FixedSalary;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Менеджер | ID: {Id}, Имя: {Name}, Зарплата: {CalculateSalary():C}");
    }
}
