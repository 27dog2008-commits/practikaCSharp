class Engineer : Employee, IWorker
{
    public void Work() => Console.WriteLine($"{Name} чинит сервер.");
}
