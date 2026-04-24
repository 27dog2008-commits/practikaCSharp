class Num3
{
    static void Main()
    {
        Employee[] staff = new Employee[]
        {
                new Director { Name = "Иван Иванович" },
                new Engineer { Name = "Алексей" },
                new Director { Name = "Мария Петровна" },
                new Engineer { Name = "Дмитрий" }
        };

        Console.WriteLine("Поиск всех управленцев:");
        foreach (var person in staff)
        {
            if (person is IManager manager)
            {
                Console.Write("[Менеджер найден] ");
                manager.Manage();
            }
        }
    }
}