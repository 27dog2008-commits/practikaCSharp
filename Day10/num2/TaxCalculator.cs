public class TaxCalculator
{
    private ITaxStrategy _strategy;

    public void SetStrategy(ITaxStrategy strategy)
    {
        _strategy = strategy;
    }

    public void Calculate(double amount)
    {
        if (_strategy == null)
        {
            Console.WriteLine("Ошибка: Стратегия расчета не установлена!");
            return;
        }
        double tax = _strategy.CalculateTax(amount);
        Console.WriteLine($"Применена стратегия: {_strategy.GetType().Name}. Налог: {tax}$");
    }
}
    