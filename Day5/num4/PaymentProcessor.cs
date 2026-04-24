class PaymentProcessor : ICreditPayment, IDebitPayment
{
    void ICreditPayment.ProcessPayment(decimal amount)
    {
        Console.WriteLine($"[Кредит] Списание с лимита: {amount:C}");
    }

    void IDebitPayment.ProcessPayment(decimal amount)
    {
        Console.WriteLine($"[Дебет] Списание собственных средств: {amount:C}");
    }
}
