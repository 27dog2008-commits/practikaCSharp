class Program
{
    static void Main()
    {
        PaymentProcessor processor = new PaymentProcessor();

        ICreditPayment credit = processor;
        IDebitPayment debit = processor;

        Console.WriteLine("Демонстрация вызовов:");
        credit.ProcessPayment(1000m);
        debit.ProcessPayment(500m);

    }
}