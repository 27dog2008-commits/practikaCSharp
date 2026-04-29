using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Тестирование паттерна стратегия\n");

        TaxCalculator calculator = new TaxCalculator();
        double orderAmount = 1000.0;

        calculator.SetStrategy(new USTax());
        calculator.Calculate(orderAmount);

        calculator.SetStrategy(new EUTax());
        calculator.Calculate(orderAmount);

        calculator.SetStrategy(new AsiaTax());
        calculator.Calculate(orderAmount);
    }
}