public class Developer : Employee
{
    public double HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public Developer(string name, int id, double hourlyRate, int hoursWorked) : base(name, id)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    public override double CalculateSalary()
    {
        return HourlyRate * HoursWorked;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Разработчик | ID: {Id}, Имя: {Name}, Зарплата: {CalculateSalary():C}");
    }
}
