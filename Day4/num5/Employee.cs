public abstract class Employee
{
    public string Name { get; set; }
    public int Id { get; set; }

    public Employee(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public abstract double CalculateSalary();

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}, Имя: {Name}");
    }
}
