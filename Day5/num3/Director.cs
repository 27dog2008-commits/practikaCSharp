class Director : Employee, IManager
{
    public void Manage() => Console.WriteLine($"{Name} раздает указания.");
}
