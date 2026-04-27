class Program
{
    static void Main()
    {
        Calculator calc = new Calculator();

        try
        {
            Console.WriteLine("Результат: " + calc.Divide(10, 0));
        }
        catch (DivisionByZeroException ex)
        {
            Console.WriteLine($"Ошибка калькулятора: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Завершение работы калькулятора.");
        }
    }
}