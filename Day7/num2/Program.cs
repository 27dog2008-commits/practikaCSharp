class Num2
{
    static void Main()
    {
        NumberProcessor processor = new NumberProcessor();

        try
        {
            processor.Process("не_число");
        }
        catch (InvalidNumberException ex)
        {
            Console.WriteLine($"Сообщение: {ex.Message}");
            Console.WriteLine($"Внутреннее исключение: {ex.InnerException?.Message}");
            Console.WriteLine("\nПолный стек вызовов:");
            Console.WriteLine(ex.StackTrace);
        }
    }
}